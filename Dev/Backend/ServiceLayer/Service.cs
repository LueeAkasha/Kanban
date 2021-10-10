using IntroSE.Kanban.Backend.BusinessLayer.Controllers;
using System;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// The service for using the Kanban board.
    /// It allows executing all of the required behaviors by the Kanban board.
    /// You are not allowed (and can't due to the interfance) to change the signatures
    /// Do not add public methods\members! Your client expects to use specifically these functions.
    /// You may add private, non static fields (if needed).
    /// You are expected to implement all of the methods.
    /// Good luck.
    /// </summary>
    public class Service : IService
    {
        private UserController Users;
        private BoardController Boards;

        /// <summary>
        /// Simple public constructor.
        /// </summary>
        public Service()
        {
            Users = new UserController();
            Boards = new BoardController();
        }
               
        /// <summary>        
        /// Loads the data. Intended be invoked only when the program starts
        /// </summary>
        /// <returns>A response object. The response should contain a error message in case of an error.</returns>
        public Response LoadData()
        { 
            try
            {
                Users.LoadUsers();
                Boards.LoadBoards();
            }
            catch (Exception e) {
                Console.WriteLine(e.ToString());
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }


        ///<summary>Remove all persistent data.</summary>
        public Response DeleteData()
        {
            try
            {
                Users.DeleteUsers();
                Boards.DeleteBoards();
            }
            catch (Exception e) {
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }


		/// <summary>
			/// Registers a new user and creates a new board for him.
		/// </summary>
		/// <param name="email">The email address of the user to register</param>
		/// <param name="password">The password of the user to register</param>
		/// <param name="nickname">The nickname of the user to register</param>
		/// <returns>A response object. The response should contain a error message in case of an error<returns>
		public Response Register(string email, string password, string nickname)
        {
            try
            {
                Users.Register(nickname, email, password);
                Boards.CreateNewBoard(email);
            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response<bool>(true);

        }
		

		/// <summary>
		/// Registers a new user and joins the user to an existing board.
		/// </summary>
		/// <param name="email">The email address of the user to register</param>
		/// <param name="password">The password of the user to register</param>
		/// <param name="nickname">The nickname of the user to register</param>
		/// <param name="emailHost">The email address of the host user which owns the board</param>
		/// <returns>A response object. The response should contain a error message in case of an error<returns>
		public Response Register(string email, string password, string nickname, string emailHost)
        {
            try
            {
                if (Users.IsUser(email)) throw new Exception("User already exists!");
                else if (!Users.IsUser(emailHost)) throw new Exception("Host user does not exist!");
                Users.Register(nickname, email, password);
                Boards.JoinBoard(email, emailHost);
                

            }
            catch (Exception e)
            {
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }
				

		
		/// <summary>
        /// Assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        
		/// <param name="emailAssignee">Email of the user to assign to task to</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            try {
                if (email == null && emailAssignee == null)
                    throw new Exception("emails must not be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsUser(emailAssignee)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.AssignTask(email, columnOrdinal, taskId, emailAssignee);
            }
            catch (Exception e) {
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }		
		
		/// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>        		
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response DeleteTask(string email, int columnOrdinal, int taskId)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.DeleteTask(email, columnOrdinal, taskId);
            }
            catch (Exception e) {
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }	

        /// <summary>
        /// Log in an existing user
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns>A response object with a value set to the user, instead the response should contain a error message in case of an error</returns>
        public Response<User> Login(string email, string password)
        {
            try {
                Users.Login(email, password);
            }
            catch (Exception e) {
                return new Response<User>(e.Message);
            }
            return new Response<User>(new User(email, Users.GetNickName(email)));
        }

        /// <summary>        
        /// Log out an logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response Logout(string email)
        {
            try
            {
                Users.Logout(email);
            }
            catch (Exception e)
            {
                return new Response<User>(e.ToString());
            }
            return new Response<bool>(true);
        }

        /// <summary>
        /// Returns the board of a user. The user must be logged in
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<Board> GetBoard(string email)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                return new Response<Board>(new Board(Boards.GetBoard(email).GetColumnsNames(), Boards.GetBoard(email).EmailCreator));
            }
            catch (Exception e) {
                return new Response<Board>(e.Message);
            }
        }

        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumnTasks(string email, int columnOrdinal, int limit)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.SetLimit(email, columnOrdinal, limit);
            }
            catch (Exception e) {
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }

        /// <summary>
        /// Change the name of a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="newName">The new name.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response ChangeColumnName(string email, int columnOrdinal, string newName)
		{
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.ChangeColumnName(email, columnOrdinal, newName);
            }
            catch (Exception e) {
                return new Response(e.Message);
            }
            return new Response<bool>(true);
        }


        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string title, string description, DateTime dueDate)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                DateTime CreationTime = DateTime.Now;
                int taskId = Boards.AddTask(title, description, email, dueDate, CreationTime);
                return new Response<Task>(new Task(taskId, CreationTime, dueDate, title, description, email));
            }
            catch (Exception e) {
                return new Response<Task>(e.Message);
            }
            
        }

        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
            }
            catch (Exception e) { return new Response(e.Message); }
            return new Response<bool>(true);
        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.UpdateTaskTitle(email, columnOrdinal, taskId, title);
            }
            catch (Exception e) { return new Response(e.Message); }
            return new Response<bool>(true);
        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.UpdateTaskDescription(email, columnOrdinal, taskId, description);
            }
            catch (Exception e) { return new Response(e.Message); }
            return new Response<bool>(true);
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string email, int columnOrdinal, int taskId)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.AdvanceTask(email, columnOrdinal, taskId);
            }
            catch (Exception e) { return new Response(e.Message); }
            return new Response<bool>(true);
        }


        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnName">Column name</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<Column> GetColumn(string email, string columnName)
        {
            try
            {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                BusinessLayer.Objects.Column BLColumn = Boards.GetColumn(email, columnName);
                List<Task> tasks = new List<Task>();
                foreach (BusinessLayer.Objects.Task task in BLColumn.Tasks.Values)
                {
                    tasks.Add(new Task(task.Id, task.CreationTime, task.DueDate, task.Title, task.Description, task.EmailAssignee));
                }
                return new Response<Column>(new Column(tasks, BLColumn.Name, BLColumn.Limit));

            }
            catch (Exception e) { return new Response<Column>(e.Message); }
        }

        /// <summary>
        /// Returns a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>

        public Response<Column> GetColumn(string email, int columnOrdinal)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                BusinessLayer.Objects.Column BLColumn = Boards.GetColumn(email, columnOrdinal);
                List<Task> tasks = new List<Task>();
                foreach (BusinessLayer.Objects.Task task in BLColumn.Tasks.Values) {
                    tasks.Add(new Task(task.Id, task.CreationTime, task.DueDate, task.Title, task.Description, task.EmailAssignee));
                }
                return new Response<Column>(new Column(tasks, BLColumn.Name, BLColumn.Limit));

            }
            catch (Exception e) { return new Response<Column>(e.Message); }
        }

        /// <summary>
        /// Removes a column given it's identifier.
        /// The first column is identified by 0, the ID increases by 1 for each column
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Column ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveColumn(string email, int columnOrdinal)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.RemoveColumn(email, columnOrdinal);
            }
            catch (Exception e) { return new Response<Column>(e.Message); }
            return new Response<bool>(true);
        }


        /// <summary>
        /// Adds a new column, given it's name and a location to place it.
        /// The first column is identified by 0, the ID increases by 1 for each column        
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Location to place to column</param>
        /// <param name="Name">new Column name</param>
        /// <returns>A response object with a value set to the new Column, the response should contain a error message in case of an error</returns>
        public Response<Column> AddColumn(string email, int columnOrdinal, string Name)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.AddColumn(email, columnOrdinal, Name);
                return new Response<Column>(new Column(new List<Task>(), Name, 100));
            }
            catch (Exception e) { return new Response<Column>(e.Message); }

        }

        /// <summary>
        /// Moves a column to the right, swapping it with the column wich is currently located there.
        /// The first column is identified by 0, the ID increases by 1 for each column        
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Current location of the column</param>
        /// <returns>A response object with a value set to the moved Column, the response should contain a error message in case of an error</returns>
        public Response<Column> MoveColumnRight(string email, int columnOrdinal)
        {
            try {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.MoveColumnRight(email, columnOrdinal);
                BusinessLayer.Objects.Column BLColumn = Boards.GetColumn(email, columnOrdinal + 1);
                List<Task> tasks = new List<Task>();
                foreach (BusinessLayer.Objects.Task task in BLColumn.Tasks.Values)
                {
                    tasks.Add(new Task(task.Id, task.CreationTime, task.DueDate, task.Title, task.Description, task.EmailAssignee));
                }
                return new Response<Column>(new Column(tasks, BLColumn.Name, BLColumn.Limit));
            }
            catch (Exception e) { return new Response<Column>(e.Message); }

        }

        /// <summary>
        /// Moves a column to the left, swapping it with the column wich is currently located there.
        /// The first column is identified by 0, the ID increases by 1 for each column.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="columnOrdinal">Current location of the column</param>
        /// <returns>A response object with a value set to the moved Column, the response should contain a error message in case of an error</returns>
        public Response<Column> MoveColumnLeft(string email, int columnOrdinal)
        {
            try
            {
                if (email == null) throw new Exception("Email cannot be null!");
                else if (!Users.IsUser(email)) throw new Exception("User does not exist!");
                else if (!Users.IsLogged(email)) throw new Exception("User is not logged in!");
                Boards.MoveColumnLeft(email, columnOrdinal);
                BusinessLayer.Objects.Column BLColumn = Boards.GetColumn(email, columnOrdinal - 1);
                List<Task> tasks = new List<Task>();
                foreach (BusinessLayer.Objects.Task task in BLColumn.Tasks.Values)
                {
                    tasks.Add(new Task(task.Id, task.CreationTime, task.DueDate, task.Title, task.Description, task.EmailAssignee));
                }
                return new Response<Column>(new Column(tasks, BLColumn.Name, BLColumn.Limit));
            }
            catch (Exception e) { return new Response<Column>(e.Message); }

        }

    }
}
