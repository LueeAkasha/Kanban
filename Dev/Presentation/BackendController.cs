using Presentation.Model;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;

namespace Presentation
{
    public class BackendController
    {
        public IService Service { get; private set; }
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="service"> service object from service layer.</param>
        public BackendController(IService service)
        {
            this.Service = service;
            service.LoadData();
        }
        /// <summary>
        /// simple constructor initialize the service.
        /// </summary>
        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }

        /// <summary>
        /// calling the login function from the service
        /// </summary>
        /// <param name="Email"> user's email </param>
        /// <param name="Password">user's password</param>
        /// <returns> returns user model object if succeeded</returns>
        public UserModel Login(string Email, string Password) {
            Response<User> user = Service.Login(Email, Password);
            if (user.ErrorOccured)
            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(user.Value.Email, user.Value.Nickname);
        }

       
        /// <summary>
        /// calling the registering function from the service.
        /// </summary>
        /// <param name="Email"> new user email</param>
        /// <param name="Password"> new user password</param>
        /// <param name="NickName"> new user nickname</param>
        public void Register(string Email, string Password, string NickName)
        {
            Response res = Service.Register(Email, Password, NickName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling registering new user and join exist board function from service.
        /// </summary>
        /// <param name="email"> new user email</param>
        /// <param name="password"> new user password</param>
        /// <param name="nickname"> new user nickname</param>
        /// <param name="emailHost"> the exists host board's user email</param>
        public void Register(string email, string password, string nickname, string emailHost) {
            Response res = Service.Register(email, password, nickname, emailHost);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the assigneeing task function from the service.
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <param name="columnOrdinal"> position of the task on the board</param>
        /// <param name="taskId"> task's id</param>
        /// <param name="emailAssignee"> new user email to assignee the task to</param>
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee) {
            Response res = Service.AssignTask(email, columnOrdinal, taskId, emailAssignee);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the deleting task function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> position of the task on the board</param>
        /// <param name="taskId"> task's id</param>
        public void DeleteTask(string email, int columnOrdinal, int taskId) {
            Response res = Service.DeleteTask(email, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the log out function from the service.
        /// </summary>
        /// <param name="email">user's email to logout from the board</param>
        public void Logout(string email) {
            Response res = Service.Logout(email);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the get board function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <returns> returns board mode object if succeeded</returns>
        public BoardModel GetBoard(string email) {
            Response<Board> res = Service.GetBoard(email);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new BoardModel(email, res.Value.ColumnsNames, this);
        }

        /// <summary>
        /// calling the limiting column tasks function from the service.
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <param name="columnOrdinal"> ordinal of the column on the board </param>
        /// <param name="limit"> the new limit of the column</param>
        public void LimitColumnTasks(string email, int columnOrdinal, int limit) {
            Response res = Service.LimitColumnTasks(email, columnOrdinal, limit);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the changing column's name function from the service.
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal"> ordinal of the column on the board </param>
        /// <param name="newName"> the new name of the column </param>
        public void ChangeColumnName(string email, int columnOrdinal, string newName) {
            Response res = Service.ChangeColumnName(email, columnOrdinal, newName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the adding task function from the service.
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="title"> title of new task</param>
        /// <param name="description">description of the new task</param>
        /// <param name="dueDate"> due-date of the new task</param>
        /// <returns>returns the new task as task model object if succeeded to add</returns>
        public TaskModel AddTask(string email, string title, string description, DateTime dueDate)
        {
            Response<Task> res = Service.AddTask(email, title, description, dueDate);
            if (res.ErrorOccured)
            {   
                throw new Exception(res.ErrorMessage);
            }
            return new TaskModel(res.Value);
        }

        /// <summary>
        /// calling the update task due-date from service.
        /// </summary>
        /// <param name="email">user's email </param>
        /// <param name="columnOrdinal"> the position of the task on the board</param>
        /// <param name="taskId"> task id </param>
        /// <param name="dueDate"> new deadline to the task.</param>
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime dueDate) {
            Response res = Service.UpdateTaskDueDate(email, columnOrdinal, taskId, dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            
        }

        /// <summary>
        /// calling the update task title function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> the position of task on the board</param>
        /// <param name="taskId">task's id</param>
        /// <param name="title">new title for the task</param>
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title) {
            Response res = Service.UpdateTaskTitle(email, columnOrdinal, taskId, title);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling the updating task description function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> position of the task on the board</param>
        /// <param name="taskId">task's id</param>
        /// <param name="description"> new description for the task.</param>
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description) {
            Response res = Service.UpdateTaskDescription(email, columnOrdinal, taskId, description);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        /// <summary>
        /// calling advancing task function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> position of the task on the board.</param>
        /// <param name="taskId">task's id</param>
        public void AdvanceTask(string email, int columnOrdinal, int taskId) {
            Response res = Service.AdvanceTask(email, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }
        

        /// <summary>
        /// calling the get column by its name function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnName"> the column's name</param>
        /// <returns> returns the column as column model object if succeeded to get it</returns>
        public ColumnModel GetColumn(string email, string columnName) {
            Response<Column> res = Service.GetColumn(email, columnName);
            
                if (res.ErrorOccured)
                {
                    throw new Exception(res.ErrorMessage);
                }
            return new ColumnModel(this, res.Value);
        }


        /// <summary>
        /// calling the get column by its ordinal function from the service.
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal"> the ordinal of the column on the board</param>
        /// <returns> returns the column as column model object if succeeded to get</returns>
        public ColumnModel GetColumn(string email, int columnOrdinal) {
            Response<Column> res = Service.GetColumn(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new ColumnModel(this, res.Value);// this in ()
        }


        /// <summary>
        /// calling the removing column function from the service.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> the ordinal of the column to remove</param>
        public void RemoveColumn(string email, int columnOrdinal) {
            Response res = Service.RemoveColumn(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }


        /// <summary>
        /// calling the adding new column function in service layer.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal">the new column's ordinal</param>
        /// <param name="Name">the new column's Name</param>
        /// <returns> return column model after adding it to board</returns>
        public ColumnModel AddColumn(string email, int columnOrdinal, string Name) {
            Response<Column> res = Service.AddColumn(email, columnOrdinal, Name);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new ColumnModel(this, res.Value); 
        }


        /// <summary>
        /// calling the moving column right function in service layer.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal">the column's ordinal</param>
        /// <returns> return column model after moving it right</returns>
        public ColumnModel MoveColumnRight(string email, int columnOrdinal) {
            Response<Column> res = Service.MoveColumnRight(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new ColumnModel(this, res.Value);// this in ()
        }


        /// <summary>
        /// calling the moving column left function in service layer.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal">the column's ordinal</param>
        /// <returns> return column model after moving it left</returns>
        public ColumnModel MoveColumnLeft(string email, int columnOrdinal) {
            Response<Column> res = Service.MoveColumnLeft(email, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return new ColumnModel(this, res.Value);// this in ()
        }
    }
}
