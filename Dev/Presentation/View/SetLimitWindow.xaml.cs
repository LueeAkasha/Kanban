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
    /// Interaction logic for SetLimitWindow.xaml
    /// </summary>
    public partial class SetLimitWindow : Window
    {
        private BoardViewModel vm;
        private string email;
        private BackendController controller;
        private string ColumnName;
        public SetLimitWindow(string email, BackendController controller, string ColumnName)
        {
            InitializeComponent();
            this.ColumnName = ColumnName;
            this.vm = new BoardViewModel(email, controller);
            this.DataContext = vm;
            this.email = email;
            this.controller = controller;
        }



        private void Cancel_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (vm.LimitColumn(ColumnName) )
            {
                new BoardWindow(email, controller).Show();
                Owner.Close();
                this.Close();
            }
            else
                MessageBox.Show(vm.Message);
        }
    }
}
