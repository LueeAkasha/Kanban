using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
namespace IntroSE.Kanban.Backend.BusinessLayer.Objects
{
   class Board
    {
        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private List<Column> _Columns;
        public int BoardId { get; set; }
        public List<Column> Columns { get { return _Columns; } set { _Columns = value; } }
        public string EmailCreator { get; set; }
        public Board(string Email, int BoardId)//constructor
        {
            this.EmailCreator = Email;
            this.BoardId = BoardId;
            this.Columns = new List<Column>();
            Columns.Add(new Column("backlog", 0, new Dictionary<int, Task>()));//Default column.
            Columns.Add(new Column("in progress", 1, new Dictionary<int, Task>()));//Default column.
            Columns.Add(new Column("done", 2, new Dictionary<int, Task>()));//Default column.
        }
        public Board(DataAccessLayer.Objects.Board dalboard)//neeeeed to chaangee it.................
        {//reloading constructor helper
            this.EmailCreator = dalboard.EmailAssignee;
            this.Columns = new List<Column>();
            this.BoardId = dalboard.BoardId;
            foreach (DataAccessLayer.Objects.Column column in dalboard.Columns)
            {
                this.Columns.Add(new Column(column));
            }

        }
        public void LimitColumn(string email, int limit, Column column)//limiting the number of tasks in column from current board.
        {
            try
            {
                if (this.EmailCreator != email) throw new Exception("User does not have permission!");
                else if (limit < 0) throw new Exception("Invalid column limit!");
                column.SetLimit(limit);
                DataAccessLayer.Controllers.ColumnController updator = new DataAccessLayer.Controllers.ColumnController();
                updator.UpdateColumnLimit(column.Name, limit, this.BoardId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            log.Info("column has been successfully!");
        }
        public int AddTask(int taskId , string EmailAssignee, string Title, DateTime Ceationtime, string Description, DateTime DueDate)//adding new tasks to the board, would be always to the Backlog column.
        {
            if (Title == null || Title.Length > 50)
                throw new Exception("Title cannot be empty or larger than 50 characters");
            else if (DueDate == null)
                throw new Exception("Duedate cannot be empty!");
            else if (Description != null && Description.Length > 300)
                throw new Exception("Description cannot be larger than 300 characters!");
            else if  (DueDate.CompareTo(DateTime.Today) < 0)//in-valid duedate
                    throw new Exception("Not a valid date!");
            Task task = new Task(taskId, Ceationtime, Title, Description, DueDate, EmailAssignee);
            Column column = GetColumn(0);
            if (column.AddTask(task.Id, task))
            {
                task.Save(column.Name, BoardId);
                return task.Id;
            }
            else
                throw new Exception("Cannot add new task!");

        }

        public void AdvanceTask(string email, int columnOrdinal, int taskId)//advancing task to next column
        {
            if (columnOrdinal < Columns.Count - 1)
            {
                Task task = GetColumn(columnOrdinal).RemoveTask(taskId);
                if (task.EmailAssignee != email) throw new Exception("You are not the task's owner!");
                try
                {
                    if (!GetColumn(columnOrdinal + 1).AddTask(task.Id, task))//if I could not advance i want to get back to the prev situation.
                        GetColumn(columnOrdinal).AddTask(task.Id, task);
                    else
                    {
                        string newColumnName = GetColumn(columnOrdinal + 1).Name;
                        task.ToDalObject().AdvanceTask(newColumnName, task.ToDalObject(), this.BoardId);
                        log.Info("task has been advanced successfully!");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Next column is full!");
                }

            }
            else if (columnOrdinal == Columns.Count - 1)
                throw new Exception("This task was already done!");
            else
                throw new Exception("non-legal column!");

        }
        public Column GetColumn(int columnOrdinal)
        {//getting column by number
         /* if (Columns.Count() < columnOrdinal)//if we have this column return it.
              throw new Exception("Wrong Column number!");*/
            foreach (Column column in Columns)
                if (column.ColumnOrdinal == columnOrdinal)
                    return column;
            throw new Exception("There is no column with this ordinal");

        }
        public Column GetColumn(string columnName)//getting column by name
        { 
            if (columnName == null)//edge case
                throw new Exception("columnName cannot be null!");
            foreach (Column column in Columns)//looking for this column by its name.
                if (column.Name.Equals(columnName))//if we got it 
                    return column;//return it.
            throw new Exception("Wrong Column name!");//if we did not get it.
        }
        public void Save(string email)
        {//saving the board to the database
            DataAccessLayer.Controllers.BoardController BoardSaver = new DataAccessLayer.Controllers.BoardController();//BoardSaver
            BoardSaver.Save(email, this.ToDalObject());//calling the saving function to database
            foreach (Column column in this.Columns) {
                column.Save(this.BoardId);
            }
            log.Info("board has been saved sucessfuly!");
        }
        public DataAccessLayer.Objects.Board ToDalObject()
        {//converting to dal object in order to deal with it in the dal.
            List<DataAccessLayer.Objects.Column> columns = new List<DataAccessLayer.Objects.Column>();
            foreach (Column column in Columns)
            {
                columns.Add(column.ToDalObject());
            }
            DataAccessLayer.Objects.Board board = new DataAccessLayer.Objects.Board
            {
                Columns = columns,
                EmailAssignee = this.EmailCreator,
                BoardId = this.BoardId
            };
            return board;
        }
        
        public void AddColumn(string email, int columnOrdinal, string Name)
        {
            if (this.EmailCreator != email) throw new Exception("Only board's owner can add new column to the board!");
            if (Name == null)//edge case
                throw new Exception("Name cannot be null!");
            else if (Name.Length == 0 || Name.Length > 15) throw new Exception("not suitabl name length");
            else if (Columns.Count < columnOrdinal || columnOrdinal < 0)// check if we do not have prev-order column.
                throw new Exception("Can not add column at this columnOrdinal!");

            foreach (Column column in Columns)
            {
                if (column.Name.Equals(Name))
                    throw new Exception("There is a column with this name!");
            }
            DataAccessLayer.Controllers.ColumnController saver = new DataAccessLayer.Controllers.ColumnController();
            //adding the column at its colomnordinal then adding the old column with the same columnordinal.
            for (int i = 0; i < Columns.Count; i++)
            {
                if (Columns[i].ColumnOrdinal >= columnOrdinal)//updating the ordinal for the effected columns
                {
                    Columns[i].ColumnOrdinal += 1;
                   saver.UpdateColumnOrdinal(Columns[i].ToDalObject(), BoardId);//storing the new ordinals into database
                }
            }
            Column newColumn = new Column(Name, columnOrdinal, new Dictionary<int, Task>());//creating the new column
            Columns.Add(newColumn);//adding the new column
            newColumn.Save(this.BoardId);//storing the new column into the database
        }
        public void RemoveColumn(string email, int columnOrdinal)
        {
            if (this.EmailCreator != email) throw new Exception("Only board's owner can remove a column from the board!");
            if (Columns.Count() == 2)
                throw new Exception("Cannot remove columns anymore, minimum number of columns is 2!");
            else if (Columns.Count <= columnOrdinal || columnOrdinal < 0)// if the column does not exist.
                throw new Exception("Column does not exist!");
            Column columnToRemove = null;
            Column tempColumn = null;
            foreach (Column column in Columns) { if (column.ColumnOrdinal == columnOrdinal) columnToRemove = column; }
            int neighbourColumn = -1;
            DataAccessLayer.Controllers.TaskController taskUpdator = new DataAccessLayer.Controllers.TaskController();
            try//we expect to get an exception about adding tasks to neighbour column because of its limit.
            {
                if (columnOrdinal == 0)//we want to delete first column.
                {
                    neighbourColumn = 1;
                }
                else
                    neighbourColumn = columnOrdinal - 1;
                foreach (Column column in Columns)
                {

                    if (column.ColumnOrdinal == neighbourColumn)
                    {
                        tempColumn = new Column(column);
                        if (column.Tasks.Count + columnToRemove.Tasks.Count <= column.Limit)
                        {

                            foreach (Task task in columnToRemove.Tasks.Values)
                            {//copying its tasks to the right column.
                                column.AddTask(task.Id, task);
                                taskUpdator.UpdateTaskColumn(task.ToDalObject(), column.Name);

                            }
                            Columns.Remove(columnToRemove);//delete it from board.
                            columnToRemove.ToDalObject().DeleteColumn(columnToRemove.Name, this.BoardId);
                            for (int i = 0; i < Columns.Count; i++)
                            {
                                if (Columns[i].ColumnOrdinal >= columnOrdinal)
                                {
                                    Columns[i].ColumnOrdinal -= 1;
                                    new DataAccessLayer.Controllers.ColumnController().UpdateColumnOrdinal(Columns[i].ToDalObject(), this.BoardId);
                                }
                            }
                            return;
                        }
                        else
                            throw new Exception("Neighbour column cannot accept all tasks in column to remove!");
                    }
                }
            }
            catch (Exception e)
            {//catching the exception of adding tasks.
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (Columns[i].ColumnOrdinal == neighbourColumn)
                        Columns[i] = new Column(tempColumn);
                }
                throw new Exception(e.Message);
            }
        }
        public void MoveColumnRight(int columnOrdinal)
        {
            if (columnOrdinal == Columns.Count - 1)//cannot move the most right to right
                throw new Exception("Cannot move right this column!");
            int rightOrdinal = columnOrdinal + 1;//the right ordinal
            DataAccessLayer.Controllers.ColumnController saver = new DataAccessLayer.Controllers.ColumnController();
            for (int i = 0; i < Columns.Count; i++)//looking for both of the columns and swaping their ordinal
            {
                if (Columns[i].ColumnOrdinal == columnOrdinal)
                {
                    Columns[i].ColumnOrdinal = rightOrdinal;
                    saver.UpdateColumnOrdinal(Columns[i].ToDalObject(), this.BoardId);//updating ordinal in database
                }
                else if (Columns[i].ColumnOrdinal == rightOrdinal)
                {
                    Columns[i].ColumnOrdinal = columnOrdinal;
                    saver.UpdateColumnOrdinal(Columns[i].ToDalObject(), this.BoardId);//updating ordinal in database
                }
            }
        }
        public void MoveColumnLeft(int columnOrdinal)
        {
            if (columnOrdinal == 0)//cannot move the most left to left
                throw new Exception("Cannot move left this column!");
            int leftOrdinal = columnOrdinal - 1;//the left ordinal
            DataAccessLayer.Controllers.ColumnController saver = new DataAccessLayer.Controllers.ColumnController();
            for (int i = 0; i < Columns.Count; i++)//looking for both of the columns and swaping their ordinal
            {
                if (Columns[i].ColumnOrdinal == columnOrdinal)
                {
                    Columns[i].ColumnOrdinal = leftOrdinal;
                    
                    saver.UpdateColumnOrdinal(Columns[i].ToDalObject(), this.BoardId);//updating ordinal in database
                }
                else if (Columns[i].ColumnOrdinal == leftOrdinal)
                {
                    Columns[i].ColumnOrdinal = columnOrdinal;
                    saver.UpdateColumnOrdinal(Columns[i].ToDalObject(), this.BoardId);//updating ordinal in database
                }
            }
        }
        public List<string> GetColumnsNames() {
            List<string> output = new List<string>();
            SortedDictionary<int, string> sortedColumns = new SortedDictionary<int, string>();
            foreach (Column column in Columns)
                sortedColumns.Add(column.ColumnOrdinal, column.Name);
            foreach(string columnName in sortedColumns.Values){
                output.Add(columnName);
            }

            return output;
        }
        public void ChangeColumnName(int columnOrdinal, string newName) {
            if (Columns.Count() <= columnOrdinal && columnOrdinal < 0)
                throw new Exception("Wrong column ordinal!");
            else if (GetColumn(columnOrdinal).Name == newName)
                return;
            foreach (Column column in Columns) {
                if (column.Name == newName)
                    throw new Exception("New name already exists, try different column name!");
            }
            GetColumn(columnOrdinal).Name = newName;
            Column ChangedColumn = GetColumn(columnOrdinal);
            DataAccessLayer.Controllers.ColumnController updator = new DataAccessLayer.Controllers.ColumnController();
            updator.UpdateColumnName(ChangedColumn.ToDalObject(), this.BoardId);
            DataAccessLayer.Controllers.TaskController taskUpdator = new DataAccessLayer.Controllers.TaskController();
            foreach (Task task in ChangedColumn.Tasks.Values)
            {
                taskUpdator.UpdateTaskColumn(task.ToDalObject(), newName);
            }

        }
    }
}

