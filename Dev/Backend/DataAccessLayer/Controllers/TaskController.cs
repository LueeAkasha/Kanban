using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class TaskController
    {
        private readonly string path;
        private readonly string connectionString;

        /// <summary>
        /// dal task controller constructor.
        /// </summary>
        public TaskController()
        {
            path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())), "database.db"));//the path od database
            connectionString = $"Data Source={path}; Version=3;";
        }

       /// <summary>
       /// reloading colum's tasks
       /// </summary>
       /// <param name="BoardId"> the board id that tasks belong to </param>
       /// <param name="ColumnName"> the column name that tasks belong to</param>
       /// <returns></returns>
        public List<Task> LoadData(int BoardId, string ColumnName)
        {
            List<Task> tasks = new List<Task>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                SQLiteDataReader reader = null;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"SELECT * FROM tasks WHERE BoardId=@BoardIdVal AND ColumnName = @ColumnNameVal";//getting every task with this column name and user id.
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);//user id as param  
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", ColumnName);//column name as param
                    command.Parameters.Add(BoardIdParam);//adding user id params
                    command.Parameters.Add(ColumnNameParam);//adding column name param
                    command.Prepare();//preparing the schema
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // object desc = reader.GetValue(2);
                        tasks.Add(new Task
                        {
                            
                          
                            Title = reader.GetString(1),
                            Description =(string) reader.GetValue(2),
                            DueDate = reader.GetString(3),
                            CreationTime = reader.GetString(4),
                            Id = (int)(long)reader.GetValue(5),
                            EmailAssignee = reader.GetString(0)
                        });
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
                        reader.Close();//closing the reader
                    command.Dispose();
                    connection.Close();//closing the connection with database
                }
            }
            return tasks; //returning the list of tasks
        }


        /// <summary>
        /// delteing whole tasks on the database.
        /// </summary>
        public void DeleteAll() {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"DELETE FROM tasks";//deletion of all tasks in the database
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
        /// saving a new task to the database.
        /// </summary>
        /// <param name="task"> the task as dal object</param>
        /// <param name="ColumnName"> the column that cointains the new task</param>
        /// <param name="BoardId"> the board id of the task</param>
        public void Save(Task task, string ColumnName, int BoardId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"INSERT INTO [tasks] ([EmailAssignee], [ColumnName], [Title], [Description], [CreationTime], [DueDate], [taskId], [BoardId])" +
                        $"VALUES (@EmailAssigneeVal,@ColumnNameVal,@TitleVal,@DescriptionVal,@CreationTimeVal,@DueDateVal,@taskIdVal,@BoardIdVal);";//storing the fields of task to database
                    SQLiteParameter EmailAssigneeParam = new SQLiteParameter(@"EmailAssigneeVal", task.EmailAssignee);//user id as param
                    SQLiteParameter TitleParam = new SQLiteParameter(@"TitleVal", task.Title);//title as param
                    SQLiteParameter DescriptionParam = new SQLiteParameter(@"DescriptionVal", task.Description);//descreption as param
                    SQLiteParameter CreationTimeParam = new SQLiteParameter(@"CreationTimeVal", task.CreationTime);//creation time as param
                    SQLiteParameter DueDateParam = new SQLiteParameter(@"DueDateVal", task.DueDate);//duedate as param
                    SQLiteParameter TaskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);//task id as param
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", ColumnName);//column name as param
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);//column name as param

                    command.Parameters.Add(EmailAssigneeParam);//adding user id param
                    command.Parameters.Add(TitleParam);//adding title param
                    command.Parameters.Add(DescriptionParam);//adding descreption param
                    command.Parameters.Add(CreationTimeParam);//adding the creation time param
                    command.Parameters.Add(DueDateParam);//adding the duedate param
                    command.Parameters.Add(TaskIdParam);//adding the task id param
                    command.Parameters.Add(ColumnNameParam);//adding column name param
                    command.Parameters.Add(BoardIdParam);//adding column name param

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
        /// delete a task by task id from the database.
        /// </summary>
        /// <param name="taskId"> task's id to delete</param>
        /// <param name="BoardId"> board id of the task </param>
        public void DeleteTask(int taskId, int BoardId) {  //we think that we will have to add this function in coming milestones****
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM tasks WHERE taskId=@taskIdVal AND BoardId=@BoardIdVal";
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", taskId);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(taskIdParam);
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


        /// <summary>
        /// write task title updating on the databse 
        /// </summary>
        /// <param name="task"> the task to edit</param>
        public void UpdateTaskTitle(Task task) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"UPDATE tasks SET Title = @TitleVal WHERE taskId = @taskIdVal";
                    SQLiteParameter TitleParam = new SQLiteParameter(@"TitleVal", task.Title);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);
                    command.Parameters.Add(TitleParam);
                    command.Parameters.Add(taskIdParam);
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
        /// write task owner updating on the databse 
        /// </summary>
        /// <param name="task">task to update</param>
        public void UpdateTaskOwner(Task task) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"UPDATE tasks SET EmailAssignee = @EmailAssigneeVal WHERE taskId = @taskIdVal";
                    SQLiteParameter EmailAssigneeParam = new SQLiteParameter(@"EmailAssigneeVal", task.EmailAssignee);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);
                    command.Parameters.Add(EmailAssigneeParam);
                    command.Parameters.Add(taskIdParam);
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
        /// write task description updating on the databse 
        /// </summary>
        /// <param name="task"> task to update</param>
        public void UpdateTaskDescription(Task task)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"UPDATE tasks SET Description = @DescriptionVal WHERE taskId = @taskIdVal";
                    SQLiteParameter DescriptionParam = new SQLiteParameter(@"DescriptionVal", task.Description);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);
                    command.Parameters.Add(DescriptionParam);
                    command.Parameters.Add(taskIdParam);
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
        /// write task duedate updating on the databse 
        /// </summary>
        /// <param name="task">task to update</param>
        public void UpdateTaskDueDate(Task task)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"UPDATE tasks SET DueDate = @DueDateVal WHERE taskId = @taskIdVal";
                    SQLiteParameter DueDateParam = new SQLiteParameter(@"DueDateVal", task.DueDate);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);
                    command.Parameters.Add(DueDateParam);
                    command.Parameters.Add(taskIdParam);
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
        /// write task position updating on the databse 
        /// </summary>
        /// <param name="task"> the task to update</param>
        /// <param name="ColumnName"> the name of the new column of the task on board</param>
        public void UpdateTaskColumn(Task task, string ColumnName)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"UPDATE tasks SET ColumnName = @ColumnNameVal WHERE taskId = @taskIdVal";
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", ColumnName);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);
                    command.Parameters.Add(ColumnNameParam);
                    command.Parameters.Add(taskIdParam);
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
        /// advancing task 
        /// </summary>
        /// <param name="newColumnName">  </param>
        /// <param name="task"></param>
        /// <param name="BoardId"> board id of the task to advance</param>
        public void AdvanceTask(string newColumnName, Task task , int BoardId) {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                int res = -1;
                try
                {
                    connection.Open();//opening the connection with database
                    command.CommandText = $"UPDATE tasks SET ColumnName = @ColumnNameVal WHERE taskId = @taskIdVal AND BoardId =@BoardIdVal";//updating task's column name
                    SQLiteParameter ColumnNameParam = new SQLiteParameter(@"ColumnNameVal", newColumnName);
                    SQLiteParameter taskIdParam = new SQLiteParameter(@"taskIdVal", task.Id);
                    SQLiteParameter BoardIdParam = new SQLiteParameter(@"BoardIdVal", BoardId);
                    command.Parameters.Add(ColumnNameParam);
                    command.Parameters.Add(taskIdParam);
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
                    connection.Close();//closing the connection with database
                }
            }
        }
    }
}
