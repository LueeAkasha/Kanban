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
    /// Interaction logic for AddColumnWindow.xaml
    /// </summary>
    public partial class AddColumnWindow : Window
    {
        private BoardViewModel vm;
        private string email;
        private int charCount = 0;
        public AddColumnWindow(BackendController controller, string email)
        {
            InitializeComponent();
            this.email = email;
            vm = new BoardViewModel(email, controller);
            this.DataContext = vm;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (vm.AddColumn())
            {

                new BoardWindow(email, vm.controller).Show();
                Owner.Close();
                this.Close();

            }
            else
                MessageBox.Show(vm.Message);
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            charCount = Name.Text.Length;
            NameOutput.Text = charCount + "/15";
        }
    }
}
