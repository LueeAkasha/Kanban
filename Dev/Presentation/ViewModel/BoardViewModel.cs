using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Presentation.ViewModel
{
    class BoardViewModel : NotifiableObject
    {
        private string searchString = "";
        public string SearchString
        {
            get { return searchString; }
            set { searchString = value; RaisePropertyChanged("SearchString"); }
        }
        public BackendController controller;
        private ColumnModel selectedColumn;
        public ColumnModel SelectedColumn { get { return selectedColumn; } set { selectedColumn = value;  RaisePropertyChanged("SelectedColumn"); } }
        private TaskModel selectedTask;
        public TaskModel SelectedTask { get { return selectedTask; } set { selectedTask = value; RaisePropertyChanged("SelectedTask"); } }
        private string email;
        public string Email { get { return email; } set { email = value; } }
        private ObservableCollection<ColumnModel> columns;
        public ObservableCollection<ColumnModel> Columns { get { return columns; } set { columns = value; RaisePropertyChanged("SelectedTask"); } }
        private string message = "";
        public string Message { get { return message; } set { message = value; RaisePropertyChanged("Message"); } }
        private int columnLimit;
        public int ColumnLimit { get { return columnLimit; } set { columnLimit = value;  RaisePropertyChanged("ColumnLimit"); } }
        private int columnOrdinal;
        public int ColumnOrdinal { get { return columnOrdinal; } set { columnOrdinal = value; RaisePropertyChanged("ColumnOrdinal"); } }
        private string columnName;
        public string ColumnName { get { return columnName; } set { columnName = value; RaisePropertyChanged("ColumnName"); } }
        

        /// <summary>
        /// board view model constructor.
        /// </summary>
        /// <param name="email"> looged in user's email</param>
        /// <param name="controller"> backend controller.</param>
        public BoardViewModel(string email, BackendController controller)
        {
            this.email = email;
            this.controller = controller;
            this.Columns = controller.GetBoard(email).Columns;
            foreach (ColumnModel col in Columns) {
                foreach (TaskModel t in col.Tasks) {
                    if(Email == t.EmailAssignee)
                     t.taskBorder = new SolidColorBrush(Colors.Blue);
                }
            }
        }

        /// <summary>
        /// logging user out.
        /// </summary>
        public void Logout()
        {
            controller.Logout(Email);
        }

        /// <summary>
        /// removing a column from the board.
        /// </summary>
        /// <returns>returns true if succeeded else false.</returns>
        public bool RemoveColumn() {
            try
            {
                for (int i = 0; i < Columns.Count; i++)
                    if (Columns[i] == SelectedColumn)
                    {
                        controller.RemoveColumn(email, i);
                        return true;
                    }
                throw new Exception("No selected column!");
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }
        /// <summary>
        /// helper function to find a column ordinal of a specific task
        /// </summary>
        /// <param name="TaskId">task's id to find</param>
        /// <returns>
        /// return the position of the task on the board. 
        /// </returns>
        public int findTaskOrdinal(int TaskId) {
            for (int i = 0; i < this.Columns.Count; i++)
                foreach (TaskModel task in Columns[i].Tasks)
                    if (task.TaskId == TaskId)
                        return i;
            return -1;
        }

        

        /// <summary>
        /// deleting a task from the system.
        /// </summary>
        /// <returns>returns true if succeeded else false.</returns>
        public bool DeleteTask() {
            try {
                if (SelectedTask == null) throw new Exception("No task selected!");
                controller.DeleteTask(Email, findTaskOrdinal(SelectedTask.TaskId), SelectedTask.TaskId);
                return true;
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// shifting a column to the left.
        /// </summary>
        /// <returns> returns true if succeeded else false.</returns>
        public bool MoveColumnLeft() {
            try
            {
                for (int i = 0; i < Columns.Count; i++)
                    if (Columns[i] == SelectedColumn)
                    {
                        controller.MoveColumnLeft(Email, i);
                        return true;
                    }
                throw new Exception("No selected column!");
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
        }


        /// <summary>
        /// shifting a column to the right
        /// </summary>
        /// <returns> returns true if succeeded else false.</returns>
        public bool MoveColumnRight()
        {
            try
            {
                for (int i = 0; i < Columns.Count; i++)
                    if (Columns[i] == SelectedColumn)
                    {
                        controller.MoveColumnRight(Email, i);
                        return true;
                    }
                throw new Exception("No selected column!");
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// filtering the board due to text.
        /// </summary>
        /// <param name="searchString"> the filtering text</param>
        private  void Filter(string searchString)
        {
            ObservableCollection<ColumnModel> output = new ObservableCollection<ColumnModel>();
            foreach (ColumnModel column in this.Columns)
            {
                column.Tasks = column.Filter(searchString);
                output.Add(column);
            }
            this.Columns = output;
        }
        public bool Filter() {
            try {
                Filter(searchString);
                return true;
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
        }
        /// <summary>
        /// adding a new column to the system.
        /// </summary>
        /// <returns>returns true if succeeded else false.</returns>
        public bool AddColumn() {
            try
            {
                controller.AddColumn(Email, ColumnOrdinal, ColumnName);
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// changing the name of specific column.
        /// </summary>
        /// <param name="ColumnName"> the old name of the column to change.</param>
        /// <returns> returns true if succeeded else false.</returns>
        public bool ChangeColumnName(string ColumnName) {
            try {
                for (int i=0; i<Columns.Count; i++)
                {
                    if (Columns[i].ColumnName == ColumnName)
                    {
                        controller.ChangeColumnName(email, i, this.ColumnName);
                        return true;
                    }

                }
                throw new Exception("No column selected!");
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// limitng the number of tasks in the specific column.
        /// </summary>
        /// <param name="ColumnName"> the name of the column to limit.</param>
        /// <returns> returns true iif succeeded else false.</returns>
        public bool LimitColumn(string ColumnName) {
            try
            {
                if (ColumnName == null) throw new Exception("No column selected!");
                for (int i = 0; i < Columns.Count; i++)
                    if (Columns[i].ColumnName == ColumnName)
                    {
                        controller.LimitColumnTasks(Email, i, ColumnLimit);
                        return true;
                    }
                throw new Exception("No selected column!");
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// sorting the board's tasks by their due-date.
        /// </summary>
        public void SortByDueDate() {
            foreach (ColumnModel column in Columns) {
                column.sortTasks();
            }
        }

       
    }
}
