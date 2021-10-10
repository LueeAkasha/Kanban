using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Presentation.Model
{
    public class TaskModel
    {
        private string title;
        private string description;
        private DateTime dueDate;
        private DateTime creationDate;
        private int taskId;
        private string emailAssignee;
        public string Title { get { return title; } set { title = value; } }
        public string Description { get { return description; } set { description = value; } }
        public DateTime DueDate { get { return dueDate; } set { dueDate = value; } }
        public DateTime CreationDate { get { return creationDate; } set { creationDate = value; } }
        public int TaskId { get { return taskId; } set { taskId = value; } }
        public string EmailAssignee { get { return emailAssignee; } set { emailAssignee = value; } }

        private SolidColorBrush _background;
        private SolidColorBrush _border;
        public SolidColorBrush taskBorder { get => _border; set { _border = value; } }

        public SolidColorBrush TaskBackground { get => _background; set { _background = value; } }


        /// <summary>
        /// task model constructor.
        /// </summary>
        /// <param name="task"> service task object</param>
        public TaskModel(Task task)
        {
            this.Title = task.Title;
            this.Description = task.Description;
            this.DueDate = task.DueDate;
            this.CreationDate = task.CreationTime;
            this.TaskId = task.Id;
            this.EmailAssignee = task.emailAssignee;
            if (Percentage() >= 100)
                TaskBackground = new SolidColorBrush(Colors.Red);
            else if (Percentage() >= 75)
                TaskBackground = new SolidColorBrush(Colors.Orange);
            else
                TaskBackground = new SolidColorBrush(Colors.Green);

        }


        /// <summary>
        /// passed time percentage calculator.
        /// </summary>
        /// <returns> returns the percentage of the paased over time since task was created. </returns>
        public double Percentage() {
         
            return DateTime.Now.Subtract(CreationDate).TotalMilliseconds / DueDate.Subtract(CreationDate).TotalMilliseconds * 100;

        }
    }
}
