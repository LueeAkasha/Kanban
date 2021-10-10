using IntroSE.Kanban.Backend.BusinessLayer.Objects;
using IntroSE.Kanban.Backend.DataAccessLayer.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.BusinessLayer.Objects.Task;

namespace IntroSE.Kanban.Backend.BusinessLayer.Controllers
{
    class BoardController
    {

        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        private Dictionary<string, Board> boards;//saving all boards here.
       

        /// <summary>
        /// board controller constructor.
        /// </summary>
        public BoardController()
        {
            this.boards = new Dictionary<string, Board>();

            
        }

        /// <summary>
        /// getting a board of given user.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <returns> board </returns>
        public Board GetBoard(string email)// getting user's board by his mail
        {
             return boards[email];
            
        }


        /// <summary>
        /// setting a limit for given column
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> column's ordinal on the board</param>
        /// <param name="limit"> new limit for the given column</param>
        public void SetLimit(string email, int columnOrdinal, int limit) {//setting a limit for specific column
          
            try
            {
                Board board = GetBoard(email);//getting the board
                board.LimitColumn(email, limit, GetBoard(email).GetColumn(columnOrdinal));//calling the limiting function 

            }
            catch (Exception e)
            {
                log.Error(e.Message);//logging
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// advancing a task in specific column
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal"> given column's ordinal</param>
        /// <param name="taskId"> task id to advance</param>
        public void AdvanceTask(string email, int columnOrdinal, int taskId) {
            try
            {
                GetBoard(email).AdvanceTask(email, columnOrdinal, taskId);//calling the advancing task function
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// getting column by its ordinal
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal"> column's ordinal on the board</param>
        /// <returns> column as BL object</returns>
        public Column GetColumn(string email, int columnOrdinal) {//getting user's column by number.
            try
            {
                return GetBoard(email).GetColumn(columnOrdinal);//getting the column by its number from the board
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// getting a column by its email.
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnName"> name of wanted column on the board</param>
        /// <returns> column as BL object</returns>
        public Column GetColumn(string email, string columnName) {//getting user's column by name.
            try
            {
                if (columnName == null || columnName.Length == 0) throw new Exception("Column's name cannot be neither null nor empty!");
                return GetBoard(email).GetColumn(columnName);//getting the column by its name from the board
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// shifting column right on the board
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal">current column's ordian on the board</param>
        public void MoveColumnRight(string email, int columnOrdinal)
        {
            try
            {
                GetBoard(email).MoveColumnRight(columnOrdinal);//moving the column right in its board
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// updating a duedate of a task
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal">column's ordinal of the task</param>
        /// <param name="taskId"> task's id</param>
        /// <param name="DueDate">new duedate for the task</param>
        public void UpdateTaskDueDate(string email, int columnOrdinal, int taskId, DateTime DueDate)//Update the due date of a task
        {

            if (columnOrdinal == GetBoard(email).Columns.Count - 1)//cannot update a task which was done(in last column).
                throw new Exception("This task was already done!");//handle this in board update function.!!!PLKJkcha;dugcsa
            try
            {
                GetBoard(email).GetColumn(columnOrdinal).GetTask(taskId).UpdateTaskDueDate(email, DueDate);//calling the updating taskduedate function.
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// shifting column left on the board
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> the column's ordinal on the board</param>
        public void MoveColumnLeft(string email, int columnOrdinal)
        {
            try
            {
                GetBoard(email).MoveColumnLeft(columnOrdinal);//moving the column left in its board
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// updating a task's title
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal"> column's ordinal</param>
        /// <param name="taskId"> task's id </param>
        /// <param name="title"> new title for the task</param>
        public void UpdateTaskTitle(string email, int columnOrdinal, int taskId, string title)//Update task title
        {

            if (columnOrdinal == GetBoard(email).Columns.Count - 1)//cannot update a task which was done(in last column).
                throw new Exception("This task was already done!");//handle this in board update function.!!!PLKJkcha;dugcsa
            try
            {
                GetBoard(email).GetColumn(columnOrdinal).GetTask(taskId).UpdateTaskTitle(email, title);//calling the updating tasktitle function.
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// updating a description of a task
        /// </summary>
        /// <param name="email">user's email</param>
        /// <param name="columnOrdinal">column's ordinal of the task</param>
        /// <param name="taskId"> task's id</param>
        /// <param name="description"> new description for the task</param>
        public void UpdateTaskDescription(string email, int columnOrdinal, int taskId, string description)//Update the description of a task
        {

            if (columnOrdinal == GetBoard(email).Columns.Count - 1)//cannot update a task which was done(in last column).
                throw new Exception("This task was already done!");//handle this in board update function.!!!PLKJkcha;dugcsa
            try
            {
                GetBoard(email).GetColumn(columnOrdinal).GetTask(taskId).UpdateTaskDescription(email, description);//calling the updating taskdescription function.
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// generaing new task id.
        /// </summary>
        /// <returns> task id</returns>
        private int TaskIdGenerator() {
            int output = 0;
            foreach (Board board in boards.Values) {
                foreach (Column column in board.Columns) {
                    foreach (int taskId in column.Tasks.Keys) {
                        if (output < taskId)
                            output = taskId;
                    }
                }
            }
            output++;
            return output;
        }

        /// <summary>
        /// generating new id for new board
        /// </summary>
        /// <returns> new board id</returns>
        private int BoardIdGenerator() {
            int output = 0;
            foreach(Board board in boards.Values) {
                if (output < board.BoardId)
                    output = board.BoardId;
            }
            output++;
            return output;
        }

        /// <summary>
        /// creating new board for new user on the system.
        /// </summary>
        /// <param name="email"> user's email</param>
        public void CreateNewBoard(string email) {
            try
            {
                Board board = new Board(email, BoardIdGenerator());
                boards.Add(email, board);
                board.Save(email);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// loading the all the boards 
        /// </summary>
        public void LoadBoards()//loading the boards from database
        {
           
                DataAccessLayer.Controllers.BoardController loader = new DataAccessLayer.Controllers.BoardController();//BoardsLoader
                Dictionary<string, DataAccessLayer.Objects.Board> boardsfromDB = loader.LoadData();//all dal boards from database
                foreach (KeyValuePair<string, DataAccessLayer.Objects.Board> board in boardsfromDB)//passing over all dal boards
                {
                    if (!this.boards.ContainsKey(board.Key))
                        this.boards.Add(board.Key, new Board(board.Value));//converting every single board from dal to bl the add to system.
                }
           
        }


        /// <summary>
        /// removing a specific column 
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnordinal"> column's ordinal to remove</param>
        public void RemoveColumn(string email, int columnordinal) {
            try
            {
               
                GetBoard(email).RemoveColumn(email, columnordinal);//calling the remove column function in its board
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// adding new column to the board
        /// </summary>
        /// <param name="email">user's email </param>
        /// <param name="columnOrdinal"> column's ordinal to remove</param>
        /// <param name="Name"></param>
        public void AddColumn(string email, int columnOrdinal, string Name)
        {
            try
            {
                
                if (Name == null || Name.Length == 0) throw new Exception("Column's name cannot be neither null nor empty!");
                GetBoard(email).AddColumn(email, columnOrdinal, Name);//calling the add column function in its board
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// deleting all the boards from the database
        /// </summary>
        public void DeleteBoards()//deleting all stored boards data.
        {
            try
            {
                DataAccessLayer.Controllers.BoardController deletor = new DataAccessLayer.Controllers.BoardController();//BoardsDeletor
                deletor.DeleteAll();//calling the deleteall function
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// bind new user to exist board
        /// </summary>
        /// <param name="email"> new user's email</param>
        /// <param name="emailHost"> host user's email</param>
        public void JoinBoard(string email, string emailHost) {
            try
            {
                if (boards.ContainsKey(emailHost))
                {
                    boards.Add(email, boards[emailHost]);
                    DataAccessLayer.Controllers.BoardController BoardSaver = new DataAccessLayer.Controllers.BoardController();
                    BoardSaver.Save(email, boards[emailHost].ToDalObject());
                }

                else
                    throw new Exception("Host email does not exist!");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// assignee a task to different user.
        /// </summary>
        /// <param name="email"> old user's email of the task</param>
        /// <param name="columnOrdinal"> column's ordinal of the task</param>
        /// <param name="taskId"> task's id </param>
        /// <param name="emailAssignee"> new user's email for the task</param>
        public void AssignTask(string email, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                
                GetBoard(email).GetColumn(columnOrdinal).GetTask(taskId).AssignTask(email, emailAssignee);//calling the updating tasktitle function.
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// delete a task from a column on the board
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="columnOrdinal">column's ordinal</param>
        /// <param name="taskId"> task's id</param>
        public void DeleteTask(string email, int columnOrdinal, int taskId) {
            try
            {
                GetBoard(email).GetColumn(columnOrdinal).DeleteTask(taskId, email, GetBoard(email).BoardId);//calling the updating tasktitle function.
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// changing column name 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="columnordinal"> column's ordinal on the board</param>
        /// <param name="newName"> new name for the given column</param>
        public void ChangeColumnName(string email, int columnordinal, string newName) {
            try
            {
                if (newName == null) throw new Exception("New name already exists, try different name!");
                GetBoard(email).ChangeColumnName(columnordinal, newName);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }

        }


        /// <summary>
        /// ading a new task to the board
        /// </summary>
        /// <param name="Title"> task's title</param>
        /// <param name="Description"> task's description</param>
        /// <param name="EmailAssignee">task's holder</param>
        /// <param name="DueDate"> task's duedate</param>
        /// <param name="CreationTime"> task's creation time</param>
        /// <returns> task id</returns>
        public int AddTask(string Title, string Description, string EmailAssignee, DateTime DueDate, DateTime CreationTime) {
            try {
                return GetBoard(EmailAssignee).AddTask(TaskIdGenerator(), EmailAssignee, Title, CreationTime, Description, DueDate);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
