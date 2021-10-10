using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.Objects
{
   class Column
    {
        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<int, Task> _Tasks;
        public string Name { get; set; }
        public int Limit { get; set; }
        public Dictionary<int, Task> Tasks { get { return _Tasks; } set { _Tasks = value; } }   
        public int ColumnOrdinal { get;  set; }
        public Column(string Name , int Ordinal, Dictionary<int, Task> Tasks)//constructor
        {
            this.Name = Name;
            this.ColumnOrdinal = Ordinal;
            this.Tasks = Tasks;
            this.Limit = 100;
        }
        public Column(DataAccessLayer.Objects.Column column)
        {//reloading column from database constructor helper
            this.Name = column.Name;
            this.ColumnOrdinal = column.ColumnOrdinal;
            this.Limit = column.Limit;
            this.Tasks = new Dictionary<int, Task>();
            foreach (DataAccessLayer.Objects.Task task in column.Tasks) {
                this.Tasks.Add(task.Id, new Task(task));
            }
        }

        /// <summary>
        /// copy constructor.
        /// </summary>
        /// <param name="column"></param>
        public Column(Column column) {
            this.Name = column.Name;
            this.Limit = column.Limit;
            this.ColumnOrdinal = column.ColumnOrdinal;
            this.Tasks = new Dictionary<int, Task>();
            foreach (KeyValuePair<int, Task> task in column.Tasks)
                Tasks.Add(task.Key, task.Value);
        }
        public void Save(int BoardId) {
            
                DataAccessLayer.Controllers.ColumnController saver = new DataAccessLayer.Controllers.ColumnController();
                saver.Save(this.ToDalObject(), BoardId);            
        }
        public Task RemoveTask(int taskId)//to remove tasks and advancing them (help function)
        {
            if (Tasks.ContainsKey(taskId))//if we have this task or nor 
            {
                Task task = Tasks[taskId];//catching this task 
                Tasks.Remove(taskId);//remove the task.

                return task;//return the removed task
            }
            else
                throw new Exception("The task does not exist in this column!");
        }
        public bool AddTask(int taskId, Task task)//adding new task to current column.
        {
            if (CanAdd())//checking if can add new task to current column.
            {
                Tasks.Add(taskId, task);//try to add the task to current column
                log.Info("Task has been added successfully!");
                return true;
            }
            else
                return false;
        }

        public void SetLimit(int Limit)//setting a limit for the current column.
        {
            if (Limit >= Tasks.Count)//if number of tasks smaller or equals the new limit so we can accept the new limit, else not.
                this.Limit = Limit;
            else
                throw new Exception("The number of tasks is more than: " + Limit);
            log.Info("Column has been limited successfully!");
        }
        private bool CanAdd()//check if we can add new task in current column.
        {
            return Tasks.Count < this.Limit;
        }
        public Task GetTask(int taskId)
        {//task getter function to help in editing tasks.
            if (Tasks.ContainsKey(taskId))
                return Tasks[taskId];
            else
                throw new Exception("The task does not exist!");
        }
        public DataAccessLayer.Objects.Column ToDalObject() {//converting to dal object in order to deal with it in the dal.
            List<DataAccessLayer.Objects.Task> dalTasks = new List<DataAccessLayer.Objects.Task>();
            foreach (Task task in this.Tasks.Values)
            {
                dalTasks.Add(task.ToDalObject());
            }
            DataAccessLayer.Objects.Column column = new DataAccessLayer.Objects.Column
            {
                Name = this.Name,
                ColumnOrdinal = this.ColumnOrdinal,
                Limit = this.Limit,
                Tasks = dalTasks
            };
            return column;
        }
        public void DeleteTask(int taskId, string email, int BoardId)
        {
            if (Tasks.ContainsKey(taskId))
            {
                if (GetTask(taskId).EmailAssignee == email)
                {
                    Tasks.Remove(taskId);
                    DataAccessLayer.Controllers.TaskController deletor = new DataAccessLayer.Controllers.TaskController();
                    deletor.DeleteTask(taskId, BoardId);
                }
                else
                    throw new Exception("User does not own this task!");
            }
            else
                throw new Exception("Task does not exist in current column!");
        }

       
    }
}
