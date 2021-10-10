using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Presentation.ViewModel;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class AddTaskWindow : Window
    {
        private BackendController controller;
        private TaskViewModel vm;
        private string email;
        private int charCount = 0;
        public AddTaskWindow(string email, BackendController controller)
        {
            InitializeComponent();
            this.controller = controller;
            this.email = email;
            vm = new TaskViewModel(controller, email, 0, null);
            this.DataContext = vm;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (vm.AddTask())
            {
                new BoardWindow(email, vm.controller).Show();
                Owner.Close();
                this.Close();

            }
            else
                MessageBox.Show(vm.Message);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DescriptionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            charCount = DescriptionBox.Text.Length;
            descOutput.Text = charCount+"/300";
        }

        private void TitleBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            charCount = TitleBox.Text.Length;
            titleOutput.Text = charCount + "/50";
        }
    }
}
