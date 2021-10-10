using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer.Objects;
namespace IntroSE.Kanban.Backend.BusinessLayer.Objects
{
    class User 
    {
        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private string _Email;
        private string _Password;
        private string _Nickname;
        private bool _Connection;
        
        public bool Connection { get { return _Connection; } set { _Connection = value; } }
        public string Nickname { get { return _Nickname; } set { _Nickname = value; } }
        public string Email { get { return _Email; }
            set {
                    _Email = value;
            }
        }
       
        public string Password { get { return _Password; }
            set
            {
                bool UpperCase = false, DownCase = false, Number = false;
                if (value != null && (value.Length >= 5 & value.Length <= 25))//checking password's length
                {
                    foreach (char c in value)
                    {
                        if (c >= 'A' & c <= 'Z')//Capital letter
                            UpperCase = true;
                        else if (c >= 'a' & c <= 'z')//Small letter
                            DownCase = true;
                        else if (c >= '0' & c <= '9')//number
                            Number = true;
                    }
                    if (UpperCase & DownCase & Number)//Capital, Small and number
                        _Password = value;
                    else
                        throw new Exception("Not valid password");
                }
                else
                    throw new Exception("the length of password must be between 5 to 25 characters");

            }
        }

        

        public User(string NickName, string Email, string Password)//constructor
        {
            try
            {
                this.Nickname = NickName;
                this.Email = Email;
                this.Password = Password;
                this.Connection = false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public User(DataAccessLayer.Objects.User user) {//reloading users from database constructor helper
            this.Email = user.Email;
            this.Nickname = user.Nickname;
            this.Password = user.Password;
            this.Connection = false;
        }
        public void Login(string Email, string Password)//logging in
        {
            if (this.Email.Equals(Email) & this.Password.Equals(Password))//validation
            {
                this.Connection = true;//getting access
                log.Info("user logged in successfully!");
            }
            else
            {
                log.Error("user tried to login with wrong details");
                throw new Exception("Wrong password!");
            }
        }

        public void Logout()//logging out.
        {
            this.Connection = false;//get offline
            log.Info("user logged out successfully!");

        }
        public bool IsLogged()//if user logged in or not.
        {
            return this.Connection;//getting user's status
        }
        public void Save() {//saving to database.
            DataAccessLayer.Controllers.UserController saver = new DataAccessLayer.Controllers.UserController();//User Saver
            saver.Save(this.ToDalObject());//calling the saving function
            log.Info("user has been saved sucessfuly!");
        }
        public DataAccessLayer.Objects.User ToDalObject() {//converting to dal object to deal it in database.
            DataAccessLayer.Objects.User user = new DataAccessLayer.Objects.User
            {
                Nickname = this.Nickname, 
                Email = this.Email, 
                Password = this.Password
            };
            return user;
        
        }
    }
}
