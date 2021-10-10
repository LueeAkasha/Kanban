using Presentation.Model;
using Presentation.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class TaskViewModel : NotifiableObject
    {
        public BackendController controller { get; private set; }
        private TaskModel selectedTask;
        public TaskModel SelectedTask { get { return selectedTask; } set { selectedTask = value; RaisePropertyChanged("SelectedTask"); } }
        private string email;
        public string Email { get { return email; } set { email = value; RaisePropertyChanged("Email"); } }
        private string title = "";
        public string Title { get { return title; } set { title = value; RaisePropertyChanged("Title"); } }
        private string description = "";
        public string Description { get { return description; } set { description = value; RaisePropertyChanged("Description"); } }
        private DateTime dueDate;
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; RaisePropertyChanged("DueDate"); } }

        private int columnOrdinal;
        public int ColumnOrdinal
        {
            get { return columnOrdinal; }
            set { columnOrdinal = value; RaisePropertyChanged("ColumnOrdianl"); }
        }
        private int taskId;
        public int TaskId
        {
            get { return taskId; }
            set { taskId = value; RaisePropertyChanged("TaskId"); }
        }
        private string emailAssignee;
        public string EmailAssignee
        {
            get { return emailAssignee; }
            set { emailAssignee = value; RaisePropertyChanged("EmailAssignee"); }
        }
        private string message;
        public string Message { get { return message; } set { this.message = value; RaisePropertyChanged("Message"); } }
        
        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="controller">backend coontroller to contact with the service layer</param>
        /// <param name="email"> the online user on the system</param>
        /// <param name="ColumnOrdinal"> the task's column</param>
        /// <param name="selectedTask">the task itself as an object</param>
        public TaskViewModel(BackendController controller, string email,int ColumnOrdinal, object selectedTask)
        {
            this.ColumnOrdinal = ColumnOrdinal;
            this.SelectedTask = (TaskModel) selectedTask;
            this.controller = controller;
            this.Email = email;
            this.DueDate = DateTime.Now;
        }
        /// <summary>
        /// this function addes a new task to user's board.
        /// </summary>
        /// <returns> returns true if succeeded else false. </returns>
        public bool AddTask()
        {
            try
            {
                controller.AddTask(Email, Title, Description, DueDate);
                return true;
            }
            catch (Exception e)
            {
                this.Message = e.Message;
                return false;
            }
        }

        /// <summary>
        /// this function updates the non-done task's title.
        /// </summary>
        /// <returns> returns true if succeeded else false. </returns>
        public bool UpdateTaskTitle() {
            try {
                if (SelectedTask == null) throw new Exception("No task selected!");
                controller.UpdateTaskTitle(Email, ColumnOrdinal, SelectedTask.TaskId, Title);
                return true;
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
        }
        

        /// <summary>
        /// advancing the chosen task to the next column.
        /// </summary>
        /// <returns> returns true if succeeded else false. </returns>
        public bool AdvanceTask()
        {
            try
            {
                if (SelectedTask == null) throw new Exception("No task selected!");
                controller.AdvanceTask(Email, new BoardViewModel(Email, controller).findTaskOrdinal(SelectedTask.TaskId), SelectedTask.TaskId);
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }


        /// <summary>
        /// this function updates the non-done task's description.
        /// </summary>
        /// <returns> returns true if succeeded else false. </returns>
        public bool UpdateTaskDescription()
        {
            try
            {
                if (SelectedTask == null) throw new Exception("No task selected!");
                controller.UpdateTaskDescription(Email, ColumnOrdinal, SelectedTask.TaskId, Description);
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }
        /// <summary>
        /// this function updates the non-done task's due-date.
        /// </summary>
        /// <returns> returns true if succeeded else false. </returns>
        public bool UpdateTaskDueDate()
        {
            try
            {
                if (SelectedTask == null) throw new Exception("No task selected!");
                controller.UpdateTaskDueDate(Email, ColumnOrdinal, SelectedTask.TaskId, DueDate);
                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }
        /// <summary>
        /// assigneing a task to other user.
        /// </summary>
        /// <returns>returns true if succeeded else false.</returns>
        public bool AssigneeTask() {
            try {
                if (SelectedTask == null) throw new Exception("No task selected!");
                controller.AssignTask(Email, ColumnOrdinal, SelectedTask.TaskId, EmailAssignee);
                return true;
            }
            catch (Exception e) {
                Message = e.Message;
                return false;
            }
        }
    }
}
