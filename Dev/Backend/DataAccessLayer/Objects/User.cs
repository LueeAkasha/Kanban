using System.Data.SQLite;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using IntroSE.Kanban.Backend.DataAccessLayer.Controllers;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Objects
{

    class User : UserController
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}
