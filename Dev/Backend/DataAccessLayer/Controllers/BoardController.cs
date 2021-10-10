using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class BoardController
    {
        private readonly string path;
        private readonly string connectionString;

        public BoardController()
        {
            path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "database.db"));
            connectionString = $"Data Source={path}; Version=3;";
        }

        /// <summary>
        /// deleteing all boards from database.
        /// </summary>
        public void DeleteAll()
        {
            ColumnController columnsDeletor = new ColumnController();
            TaskController tasksDeletor = new TaskController();
            columnsDeletor.DeleteAll();
            tasksDeletor.DeleteAll();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE  FROM boards";
                    command.Prepare();//preparing the schema

                    res = command.ExecuteNonQuery(); //running the schema

                }
                catch (Exception e)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(e.ToString());
                    //loogingggg!
                }
                finally
                {//closing the database.
                    command.Dispose();
                    connection.Close();
                }
            }


        }
    
        /// <summary>
        /// saving a new board to the database.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="board"> the board as board dal object</param>
        public void Save(string email, Board board) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO [boards] ([Email], [BoardCreator],[BoardId])" +
                        $"VALUES (@EmailVal, @BoardCreatorVal, @BoardIdVal);";
                    SQLiteParameter emailParam = new SQLiteParameter(@"EmailVal", email);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", board.BoardId);
                    SQLiteParameter BoardCreatorParam = new SQLiteParameter(@"BoardCreatorVal", board.EmailAssignee);

                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(BoardIdParam);
                    command.Parameters.Add(BoardCreatorParam);


                    command.Prepare();//preparing the schema
                    res = command.ExecuteNonQuery(); //running the schema

                }
                catch (Exception e)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(e.ToString());
                }
                finally
                {//closing the database.
                    command.Dispose();
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// reloading all boards from database.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Board> LoadData()
        {
            Dictionary<string, Board> boards = new Dictionary<string, Board>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                SQLiteDataReader reader = null;
                try
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM boards";
                    command.Prepare();//preparing the schema
                    reader = command.ExecuteReader();
                    ColumnController loader = new ColumnController();
                    while (reader.Read())
                    {
                        List<Column> columns = loader.LoadData((int)(long)reader.GetValue(1));

                        Board board = new Board { EmailAssignee = (string)reader.GetValue(0), BoardId = (int)reader.GetInt32(1), Columns = columns };
                        boards.Add(reader.GetString(0), board);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(command.CommandText);
                    Console.WriteLine(e.ToString());
                    //loogingggg!
                }
                finally
                {//closing the database.
                    if (reader != null)
                        reader.Close();
                    command.Dispose();
                    connection.Close();
                }
            }
            return boards;
        }
        
    }
}
