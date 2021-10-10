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
    /// Interaction logic for ChangeColumnNameWindow.xaml
    /// </summary>
    public partial class ChangeColumnNameWindow : Window
    {
        private BoardViewModel vm;
        private BackendController controller;
        private string email;
        private string ColumnName;
        private int charCount = 0;
        public ChangeColumnNameWindow(string email, BackendController controller, string ColumnName)
        {
            InitializeComponent();
            this.controller = controller;
            this.email = email;
            this.ColumnName = ColumnName;
            this.vm = new BoardViewModel(email, controller);
            this.DataContext = vm;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ChangeColumnName(ColumnName))
            {
                new BoardWindow(email, controller).Show();
                Owner.Close();
                this.Close();
            }

            else
                MessageBox.Show(vm.Message);
        }

        private void NewColumnName_TextChanged(object sender, TextChangedEventArgs e)
        {
            charCount = NewColumnName.Text.Length;
            ColumnNameoutput.Text = charCount + "/15";
        }
    }
}
