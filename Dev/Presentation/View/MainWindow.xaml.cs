using Presentation.Model;
using Presentation.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackendController controller;
        private UserViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            this.controller = new BackendController();
            this.viewModel = new UserViewModel(this.controller);
            this.DataContext = viewModel;
        }


        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserModel user = viewModel.Login();
            if (user != null)
            {
                BoardWindow boardView = new BoardWindow(user.Email, this.controller);
                boardView.Show();
                this.Close();
            }
            else
                MessageBox.Show(viewModel.Message);
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(this.controller)
            {
                Owner = this
            };
            registerWindow.Show();
        }

        private void PasswordBox_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
