using DAL;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public class UserManager
    {
        public bool AddUser(User user)
        {
            UserDAL dal = new UserDAL();
            return dal.Add(user);
        }
    }
}
