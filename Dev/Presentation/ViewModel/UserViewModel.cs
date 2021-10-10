using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ViewModel
{
    class UserViewModel : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        private string message = "";
        public string Message { get { return message; } set { message = value; RaisePropertyChanged("Message"); } }
        private string password = "";
        public string Password { get { return password; } set { password = value; RaisePropertyChanged("Password"); } }
        private string email="";
        public string Email { get { return email; } set { email = value; RaisePropertyChanged("Email"); } }
        private string hostEmail = "";
        public string HostEmail { get { return hostEmail; } set { hostEmail = value; RaisePropertyChanged("HostEmail"); } }
        private string nickName = "";
        public string NickName { get { return nickName; } set { nickName = value; RaisePropertyChanged("NickName"); } }

        public UserViewModel(BackendController controller) {
            this.Controller = controller;
        }

        /// <summary>
        /// registering a new user to system.
        /// </summary>
        /// <returns> return a user after registering him by calling the service registering function</returns>
        public UserModel Register()
        {
            try
            {
                if (HostEmail == "")
                {
                    Controller.Register(Email, Password, NickName);
                }
                else
                {
                    Controller.Register(Email, Password, NickName, HostEmail);
                }

                return new UserModel(Email, NickName);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }

        /// <summary>
        /// logging a exist user to system.
        /// </summary>
        /// <returns> return logged in user.</returns>
        public UserModel Login()
        {
            try
            {
                return Controller.Login(Email, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }


    }
}
