﻿using IBIN.DAL;
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

        public User ChangePassword(string OldPassword,string NewPassword)
        {
            User user=_db.Users.FirstOrDefault(x => x.UserName == "Admin" && x.Password == OldPassword);
            if (user != null)
            {
                user.Password = NewPassword;
                _db.SaveChanges();
            }
            return user;
        }
    }
}
