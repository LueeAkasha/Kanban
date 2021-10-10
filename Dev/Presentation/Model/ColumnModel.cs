using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Presentation.Model
{
    public class ColumnModel : NotifiableModelObject
    {
        private string columnName;
        private int columnLimit;
        private ObservableCollection<TaskModel> tasks;
        public string ColumnName { get { return columnName; } set { columnName = value; } }
        public int ColumnLimit { get { return columnLimit; } set { columnLimit = value; } }
        public ObservableCollection<TaskModel> Tasks { get { return tasks; } set { tasks = value; RaisePropertyChanged("Tasks"); } }
        /// <summary>
        /// column model constructor.
        /// </summary>
        /// <param name="controller"> backend controller</param>
        /// <param name="column"> service column object</param>
        public ColumnModel(BackendController controller, Column column) :base(controller) {
            this.ColumnName = column.Name;
            this.ColumnLimit = column.Limit;
            Console.WriteLine(this.ColumnLimit);
            this.Tasks = new ObservableCollection<TaskModel>();
            foreach (Task task in column.Tasks) {
                this.Tasks.Add(new TaskModel(task));
            }
            
        }

        
        /// <summary>
        /// sorting the tasks by their due-date.
        /// </summary>
        public void sortTasks() {
            Tasks = new ObservableCollection<TaskModel>(Tasks.OrderBy(task=>task.DueDate).ToList());
        }


        /// <summary>
        /// filtering the tasks by text.
        /// </summary>
        /// <param name="searchString"> text filter </param>
        /// <returns> filtered column</returns>
        public ObservableCollection<TaskModel> Filter(string searchString) {
            return new ObservableCollection<TaskModel>(Tasks.Where(p => p.Title.Contains(searchString) | p.Description.Contains(searchString)));
        }

    }
}
