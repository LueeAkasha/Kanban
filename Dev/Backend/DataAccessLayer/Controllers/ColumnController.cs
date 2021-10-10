using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.DataAccessLayer.Objects.Task;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class ColumnController
    {
        private readonly string path;
        private readonly string connectionString;

        public ColumnController()
        {
            path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "database.db"));
            connectionString = $"Data Source={path}; Version=3;";
        }
        public void DeleteAll()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE  FROM columns";
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

        public void Save(Column column, int BoardId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO [columns] ([ColumnName], [columnOrdinal], [BoardId], [Limit])" +
                        $"VALUES (@NameVal,@columnOrdinalVal,@BoardIdVal,@LimitVal)";
                    SQLiteParameter NameParam = new SQLiteParameter(@"NameVal", column.Name);
                    SQLiteParameter columnOrdinalParam = new SQLiteParameter(@"columnOrdinalVal", column.ColumnOrdinal);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    SQLiteParameter LimitParam = new SQLiteParameter(@"LimitVal", column.Limit);

                    command.Parameters.Add(NameParam);
                    command.Parameters.Add(columnOrdinalParam);
                    command.Parameters.Add(BoardIdParam);
                    command.Parameters.Add(LimitParam);

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
        public List<Column> LoadData(int BoardId) {
            List<Column> columns = new List<Column>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                SQLiteDataReader reader = null;
                try
                {
                    connection.Open();
                    command.CommandText = $"SELECT * FROM columns WHERE BoardId =@BoardIdVal";
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(BoardIdParam);
                    command.Prepare();//preparing the schema
                    reader = command.ExecuteReader();
                    TaskController loader = new TaskController();
                    while (reader.Read())
                    {
                        
                        List<Task> dalTasks = loader.LoadData(BoardId, reader.GetString(0));
                        columns.Add(new Column { Name = reader.GetString(0), ColumnOrdinal = (int)(long)reader.GetValue(1), Limit = (int)(long)reader.GetValue(2), Tasks = dalTasks });

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
            return columns;
        }
        public void DeleteColumn(string ColumnName, int BoardId) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM [columns] WHERE ColumnName=@ColumnNameVal AND BoardId = @BoardIdVal";
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", ColumnName);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(ColumnNameParam);
                    command.Parameters.Add(BoardIdParam);
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
        public void UpdateColumnOrdinal(Column column, int BoardId) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"UPDATE columns SET [ColumnOrdinal] = @ColumnOrdinalVal WHERE ColumnName = @ColumnNameVal AND BoardId =@BoardIdVal";
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", column.Name);
                    SQLiteParameter ColumnOrdinalParam = new SQLiteParameter(@"ColumnOrdinalVal", column.ColumnOrdinal);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(ColumnNameParam);
                    command.Parameters.Add(ColumnOrdinalParam);
                    command.Parameters.Add(BoardIdParam);
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
        }public void UpdateColumnName(Column column, int BoardId) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"UPDATE columns SET [ColumnName] = @ColumnNameVal WHERE ColumnOrdinal = @ColumnOrdinalVal AND BoardId =@BoardIdVal";
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", column.Name);
                    SQLiteParameter ColumnOrdinalParam = new SQLiteParameter(@"ColumnOrdinalVal", column.ColumnOrdinal);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(ColumnNameParam);
                    command.Parameters.Add(ColumnOrdinalParam);
                    command.Parameters.Add(BoardIdParam);
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
        public void UpdateColumnLimit(string ColumnName, int Limit, int BoardId) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"UPDATE columns SET [Limit] = @LimitVal WHERE ColumnName = @ColumnNameVal AND BoardId =@BoardIdVal";
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", ColumnName);
                    SQLiteParameter ColumnLimitParam = new SQLiteParameter(@"LimitVal", Limit);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(ColumnNameParam);
                    command.Parameters.Add(ColumnLimitParam);
                    command.Parameters.Add(BoardIdParam);
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
    }
}
