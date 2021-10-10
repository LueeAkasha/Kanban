using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class UserModel
    {
        public string Email { get; set; }
        public string NickName { get; set; }

        public UserModel(string email, string nickName)
        {
            Email = email;
            NickName = nickName;
        }
    }
}
