using Presentation.ViewModel;
using System.Windows;

namespace Presentation.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private UserViewModel viewModel;
        private BackendController controller;
        public RegisterWindow(BackendController controller)
        {
            InitializeComponent();
            this.controller = controller;
            viewModel = new UserViewModel(controller);
            this.DataContext = viewModel;
        }


        /// <summary>
        /// register the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Register() != null)
            {
                this.Close();
            }
            else
                MessageBox.Show(viewModel.Message);
        }
    }
}
