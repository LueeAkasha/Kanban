using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class UserController
    {
        private readonly string path;
        private readonly string connectionString;

        /// <summary>
        /// dal user controlller constructor.
        /// </summary>
        public UserController()
        {
            path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "database.db"));//database place
            connectionString = $"Data Source={path}; Version=3;";//making connection with database
        }


        /// <summary>
        /// saving a new user to the database.
        /// </summary>
        /// <param name="user"></param>
        public void Save(User user)//saving single user into database
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"INSERT INTO [users] ([Email], [Nickname], [Password])" +
                        $"VALUES (@EmailVal,@NicknameVal,@PasswordVal);";//inserting the user's fields
                    SQLiteParameter emailParam = new SQLiteParameter(@"EmailVal", user.Email);//email as param
                    SQLiteParameter nicknameParam = new SQLiteParameter(@"NicknameVal", user.Nickname);//nickname as param
                    SQLiteParameter passwordParam = new SQLiteParameter(@"PasswordVal", user.Password);//pass as param

                    command.Parameters.Add(emailParam);//add email param
                    command.Parameters.Add(nicknameParam);//add nickname param
                    command.Parameters.Add(passwordParam);//add pass param

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
                    connection.Close();//closing the connection with database
                }
            }
        }

        /// <summary>
        /// delting all users from the database.
        /// </summary>
        public void DeleteAll()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"DELETE  FROM users";//deletion of all users in database
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
                    connection.Close();//closing the connection with database
                }
            }
        }

        /// <summary>
        /// reloading all users from the database.
        /// </summary>
        /// <returns> returns a list of the whole users on the system.</returns>
        public List<User> LoadData()
        {
            List<User> users = new List<User>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                SQLiteDataReader reader = null;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"SELECT * FROM users";//loading all users from database
                    command.Prepare();//preparing the schema
                    reader = command.ExecuteReader();
                    while (reader.Read())//adding every user to the list
                        users.Add(new User { Email = (string)reader.GetValue(0), Nickname = (string)reader.GetValue(1), Password = (string)reader.GetValue(2)});
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
                        reader.Close();//closing the reader
                    command.Dispose();
                    connection.Close();//closing the connection with database
                }
            }
            return users;//returning list of users
        }

        

    }
}
