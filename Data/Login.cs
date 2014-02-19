using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swove.Data
{
    class Login
    {
        private List<User> users_ = new List<User>();

        public bool addLogin(String U, String P) {
            if (!loginExist(U)) {
                users_.Add(new User { Username = U, Password = P });
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool loginExist(String U)
        {
            foreach (User user in users_)
            {
                if (user.Username == U)
                {
                    return true;
                }
            }
            return false;
        }

        public bool doLogin(String U, String P) {
            foreach (User user in users_)
            {
                if (user.Username == U && user.Password == P)
                {
                    return true;
                }
            }
            return false;
        }

        //Get List of users
        //Get amount of users
    }
}
