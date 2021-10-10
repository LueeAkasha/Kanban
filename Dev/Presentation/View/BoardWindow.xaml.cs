using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Presentation.ViewModel;
using System;
using System.Linq;



namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for BoardWindow.xaml
    /// </summary>
    public partial class BoardWindow : Window
    {
        private BoardViewModel vm;
        BackendController controller;
        private string email;
        public BoardWindow(string email, BackendController controller)
        {
            InitializeComponent();
            this.controller = controller;
            this.email = email;
            vm = new BoardViewModel(email, controller);
            this.DataContext = vm;


        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTaskWindow view = new AddTaskWindow(email, controller)
            {
                Owner = this
            };
            view.Show();

        }

        private void AdvanceTask_Click(object sender, RoutedEventArgs e)
        { 
            TaskViewModel taskAdvancer = new TaskViewModel(controller, email, 0, vm.SelectedTask);
            if (taskAdvancer.AdvanceTask())
            {
                new BoardWindow(email, controller).Show();
                this.Close();
            }
            else
                MessageBox.Show(taskAdvancer.Message);
        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            AddColumnWindow view = new AddColumnWindow(controller, email);
            view.Owner = this;
            view.Show();
        }

        

        private void MoveColumnLeft_Click(object sender, RoutedEventArgs e)
        {
            if (vm.MoveColumnLeft())
            {
                new BoardWindow(email, controller).Show();
                this.Close();
            }
            else
                MessageBox.Show(vm.Message);
        }

        private void MoveColumnRight_Click(object sender, RoutedEventArgs e)
        {
            if (vm.MoveColumnRight())
            {
                new BoardWindow(email, controller).Show();
                this.Close();
            }
            else
                MessageBox.Show(vm.Message);
        }

        private void RemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            if (vm.RemoveColumn())
            {
                new BoardWindow(email, controller).Show();
                this.Close();
            }
            else
                MessageBox.Show(vm.Message);

        }


        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            

            

                if (vm.DeleteTask())
                {
                    new BoardWindow(email, controller).Show();
                    this.Close();
                }
                else
                    MessageBox.Show(vm.Message);
          
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            vm.Logout();
            (new MainWindow()).Show();
            this.Close();
        }

        private void SetLimit_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedTask == null) MessageBox.Show("No column selected!");
            else
            {
                SetLimitWindow view = new SetLimitWindow(email, controller, vm.SelectedColumn.ColumnName)
                {
                    Owner = this
                };
                view.Show();
            }
        }

        private void AssigneeTask_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedTask == null) MessageBox.Show("No task selected!");
            else
            {
                AssigneeTaskWindow view = new AssigneeTaskWindow(controller, email, vm.findTaskOrdinal(vm.SelectedTask.TaskId), vm.SelectedTask)
                {
                    Owner = this
                };
                view.Show();
            }
        }

        private void ChangeColumnName_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedColumn != null)
            {
                ChangeColumnNameWindow view = new ChangeColumnNameWindow(email, controller, vm.SelectedColumn.ColumnName)
                {
                    Owner = this
                };
                view.Show();
            }
            else
                MessageBox.Show("No column selected!");
        }

        private void SortByDueDate_Click(object sender, RoutedEventArgs e)
        {
            vm.SortByDueDate();
        }
        

      
        
        private void UpdateTaskTitle_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedTask == null) MessageBox.Show("No task selected!");
            else
            {
                UpdateTitleWindow view = new UpdateTitleWindow(controller, email, vm.findTaskOrdinal(vm.SelectedTask.TaskId), vm.SelectedTask)
                {
                    Owner = this
                };
                view.Show();
            }
        }

        private void UpdateTaskDescription_Click(object sender, RoutedEventArgs e)
        {

            if (vm.SelectedTask == null) MessageBox.Show("No task selected!");
            else
            {
                UpdateDescriptionWindow view = new UpdateDescriptionWindow(controller, email, vm.findTaskOrdinal(vm.SelectedTask.TaskId), vm.SelectedTask)
                {
                    Owner = this
                };
                view.Show();
            }
        }

        private void UpdateTaskDueDate_Click(object sender, RoutedEventArgs e)
        {
            if (vm.SelectedTask == null) MessageBox.Show("No task selected!");
            else
            {
                UpdateDueDateWindow view = new UpdateDueDateWindow(controller, email, vm.findTaskOrdinal(vm.SelectedTask.TaskId), vm.SelectedTask)
                {
                    Owner = this
                };
                view.Show();
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            if (!vm.Filter())
                MessageBox.Show(vm.Message);

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            new BoardWindow(email, controller).Show();
            this.Close();
        }
    }
}
