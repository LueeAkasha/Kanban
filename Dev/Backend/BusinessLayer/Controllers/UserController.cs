using IntroSE.Kanban.Backend.BusinessLayer.Objects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.Controllers
{
    class UserController
    {
        private static readonly log4net.ILog log
    = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Dictionary<string, User> users;//saving all users here
        private static UserController instance;
        /// <summary>
        /// user controller constructor.
        /// </summary>
        private UserController()
        {
            this.users = new Dictionary<string, User>();
        }

        public static UserController getInstance()
        {
            if(instance == null)
            {
                instance = new UserController();
            }
            return instance;
        }

        /// <summary>
        /// getting a specific user from the system
        /// </summary>
        /// <param name="Email"> user's email</param>
        /// <returns> user as BL object</returns>
        public User GetUser(string Email)//getting the user by his/her mail.
        {
            return users[Email];//return user by its email.
        }

        /// <summary>
        /// checking if this is a user
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <returns> true if it is user else false</returns>
        public bool IsUser(string email)// checking if this is user in the system
        {
            return users.ContainsKey(email);//return if this email belongs to user or not.
                
        }

        /// <summary>
        /// registering a new user to the system
        /// </summary>
        /// <param name="NickName"> user's nickname</param>
        /// <param name="Email"> user's email</param>
        /// <param name="Password"> user's password</param>
        public void Register(string NickName, string Email, string Password)//adding new user to the system
        {
            if (NickName == null || Email == null || Password == null)//edge case
            {
                log.Error("new user tried to register without entering all fields");
                throw new Exception("Complete all fields, please!");
            }
            else if (!IsValidEmail(Email))//isvalid email or not
            {
                log.Error("new user tried to register with invalid email.");
                throw new Exception("not valid email!");
            }
            else if (NickName.Length == 0)//empty nickname.
            {
                log.Error("new user tried to register with empty nickname.");
                throw new Exception("non valid nickname!");
            }
            else if (!IsUser(Email))//has never been registered
            {
                User user = new User(NickName, Email, Password);//create new user.
                user.Save();//store the user into database
                users.Add(Email, user);//add the user to the system
            }
            else//if we cannot add it as new user.
            {
                log.Error("new user tried to register with exist email");
                throw new Exception("There is already a user with this email");
            }

        }

        /// <summary>
        /// checking out if We got a valid password
        /// </summary>
        /// <param name="password"> the password content</param>
        /// <returns> true if valid else false</returns>
        private bool IsValidPassword(string password) {
            bool UpperCase = false, DownCase = false, Number = false;
            if (password != null && (password.Length >= 5 & password.Length <= 25))//checking password's length
            {
                foreach (char c in password)
                {
                    if (c >= 'A' & c <= 'Z')//Capital letter
                        UpperCase = true;
                    else if (c >= 'a' & c <= 'z')//Small letter
                        DownCase = true;
                    else if (c >= '0' & c <= '9')//number
                        Number = true;
                }
                if (UpperCase & DownCase & Number)//Capital, Small and number
                    return true;
                else
                    return false;
            }
            else
                return false;

        }


        /// <summary>
        /// loggin a user to system
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="password"> user's password</param>
        public void Login(string email, string password) {//logging in user by email and password
            if (email == null | password == null)//edge case
                throw new Exception("You should enter email and password!");
            else if (!IsUser(email)) throw new Exception("User does not exist!");//check is this a user.
            GetUser(email).Login(email, password);//calling his login function.
        }


        /// <summary>
        /// logging a user out
        /// </summary>
        /// <param name="email"> user's email</param>
        public void Logout(string email)//logging out user by email
        {
            if (email == null) throw new Exception("User does not exist!");
            else if (!IsUser(email)) throw new Exception("User does not exist!");//check is this a user.
            GetUser(email).Logout();//getting the user and calling his logout function
        }


        /// <summary>
        /// checking if this a user in the system
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <returns> true if yes else false</returns>
        public bool IsLogged(string email) {//checking if user is logged by email.
            if (!IsUser(email)) throw new Exception("User does not exist!");
            return GetUser(email).IsLogged();//getting the user and checking if he is online or not
        }


        /// <summary>
        /// getting a user's nickname.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <returns> user's nickname</returns>
        public string GetNickName(string email) {//getting nickname of user by his email.
            if (!IsUser(email)) throw new Exception("User does not exist!");
            return GetUser(email).Nickname;//getting the user's nickname
        }


        /// <summary>
        /// reloading all users from the database to system.
        /// </summary>
        public void LoadUsers()// reloading all users from the database  to the system
        {
            DataAccessLayer.Controllers.UserController loader = new DataAccessLayer.Controllers.UserController();//UsersLoader
            List<DataAccessLayer.Objects.User> usersfromDB = loader.LoadData();//All users as dal users
            List<User> usersBL = new List<User>();
            foreach (DataAccessLayer.Objects.User user in usersfromDB) {
                usersBL.Add(new User(user));
            }
            foreach (User user in usersBL) {//passing over all dal users
                if (!this.users.ContainsKey(user.Email))
                this.users.Add(user.Email, user);//converting every single user from dal to bl the adding him to the system.
            }
           
        }


        /// <summary>
        /// function from microsoft to check if this a valid mail.
        /// </summary>
        /// <param name="email"> user's email </param>
        /// <returns> true if yes else false</returns>
        private bool IsValidEmail(string email)//from Microsoft
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// deleting all users from the system
        /// </summary>
        public void DeleteUsers()//Deleting all users from database.
        {
            DataAccessLayer.Controllers.UserController deletor = new DataAccessLayer.Controllers.UserController();//UsersDeletor
            deletor.DeleteAll();//calling the deletion function.
        }
        

    }

}
