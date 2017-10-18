using DAL;
using Model.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class UserManager
    {
        public bool AddUser(User user)
        {
            //todo 密码加密
            CommonDAL<User> dal = new CommonDAL<User>();
            return dal.Add(user);
        }

        public User QueryUserById(int Id)
        {
            using (SSOContext db = new SSOContext())
            {
                return db.Users.Where(x => x.Id == Id).FirstOrDefault();
            }
        }
    }
}
