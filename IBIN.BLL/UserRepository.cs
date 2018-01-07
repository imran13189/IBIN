using IBIN.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBIN.BLL
{
    public class UserRepository : Connection
    {
        public User Login(string Username,string Password)
        {
           return _db.Users.FirstOrDefault(x => x.UserName == Username && x.Password == Password);
        }
    }
}
