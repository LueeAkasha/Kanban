using Presentation.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for UpdateDescriptionWindow.xaml
    /// </summary>
    public partial class UpdateDescriptionWindow : Window
    {
        private BackendController controller;
        private TaskViewModel vm;
        private string email;
        private int charCount = 0;
        public UpdateDescriptionWindow(BackendController controller, string email, int ColumnOrdinal, object SelectedTask)
        {
            InitializeComponent();
            this.controller = controller;
            this.email = email;
            vm = new TaskViewModel(controller, email, ColumnOrdinal, SelectedTask);
            this.DataContext = vm;
        }

        private void Canel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (vm.UpdateTaskDescription())
            {
                new BoardWindow(email, controller).Show();
                Owner.Close();
                this.Close();
            }
            else
                MessageBox.Show(vm.Message);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            charCount = DescriptionBox.Text.Length;
            descOutput.Text = charCount + "/300";
        }
    }
}
