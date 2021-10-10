using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.Objects
{
    class Task
    {
        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string EmailAssignee { get; set; }
        private int _Id;
        private DateTime _CreationTime;
        private DateTime _DueDate;
        private string _Title;
        private string _Description;
        public int Id { get { return _Id; } set { _Id = value; } }
        public DateTime CreationTime { get { return _CreationTime; } set { _CreationTime = value; } }
        public DateTime DueDate { get { return _DueDate; } set {
                
                    _DueDate = value;
            }
        }
        public string Title { get { return _Title; }
            set {
                if (value == null || value.Length == 0)// invalid title
                    throw new Exception("Title cannot be empty!");
                else if (value.Length > 50)// long title
                    throw new Exception("the title is too long!");
                else
                    _Title = value;
            }
        }

        public string Description { get { return _Description; }
            set
            {
                if (value == null)
                    _Description = "";
                else if (value.Length <= 300)
                    _Description = value; 
                else
                    throw new Exception("Description is too length!");

            }
        }

        public Task(int Id, DateTime CreationTime, string Title, string Description, DateTime DueDate, string emailAssignee)//Constructor.
        {
            try
            {
                this.Id = Id;
                this.CreationTime = CreationTime;
                this.Title = Title;
                this.Description = Description;
                this.DueDate = DueDate;
                this.EmailAssignee = emailAssignee;
                
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
        public Task(DataAccessLayer.Objects.Task task)
        {//reloading tasks from database constructor helper
            this.Id = task.Id;
            this.CreationTime = DateTime.Parse(task.CreationTime);
            this.DueDate = DateTime.Parse(task.DueDate);
            this.Title = task.Title;
                this.Description = task.Description;
            this.EmailAssignee = task.EmailAssignee;


        }
        public void Save(string ColumnName, int BoardId) {
            DataAccessLayer.Controllers.TaskController saver = new DataAccessLayer.Controllers.TaskController();
            saver.Save(this.ToDalObject(), ColumnName, BoardId);
        }
        public void UpdateTaskDueDate(string email, DateTime DueDate)//Update the due date of a task
        {
            try
            {
                if (this.EmailAssignee != email) throw new Exception("You are not the task holder!");
                this.DueDate = DueDate;//trying to set the new duedate
                ToDalObject().UpdateTaskDueDate(ToDalObject());//updating it in the database
                log.Info("Task's duedate has been updated sucessfuly!");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
        public void UpdateTaskTitle(string email, string title)//Update task title
        {
            try
            {
                if (this.EmailAssignee != email) throw new Exception("You are not the task holder!");
                this.Title = title;//trying to set the new title
                ToDalObject().UpdateTaskTitle(ToDalObject());//updating it int the database
                log.Info("Task's title has been updated sucessfuly!");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
        public void UpdateTaskDescription(string email, string description)//Update the description of a task
        {
            try
            {
                if (this.EmailAssignee != email) throw new Exception("You are not the task holder!");
                this.Description = description;// trying to ser the new desccription
                ToDalObject().UpdateTaskDescription(ToDalObject());//updating it in the database
                log.Info("Task's description has been updated sucessfuly!");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw new Exception(e.Message);
            }
        }
        public DataAccessLayer.Objects.Task ToDalObject() {// converting to dal object in order to deal it in dal
            DataAccessLayer.Objects.Task task = new DataAccessLayer.Objects.Task {
                Id = this.Id,
                CreationTime = this.CreationTime.ToString(),
                DueDate = this.DueDate.ToString(),
                Title = this.Title,
                Description = this.Description,
                EmailAssignee = this.EmailAssignee
            };
            return task;
        }
        public void AssignTask(string email, string emailAssignee) {
            if (this.EmailAssignee.Equals(email))
            {
                this.EmailAssignee = emailAssignee;
                DataAccessLayer.Controllers.TaskController updator = new DataAccessLayer.Controllers.TaskController();
                updator.UpdateTaskOwner(this.ToDalObject());
                log.Info("Task Has been Assigneed to " +emailAssignee+" successfully!");
            }
            else
                throw new Exception("This is not the current user assigned email of this task!");
        }
    }
}
