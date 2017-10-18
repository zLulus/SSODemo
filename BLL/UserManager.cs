using BLL.Encrypting;
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
            user.Password = MD5Encrypting.MD5Encoding(user.Password);
            CommonDAL<User> dal = new CommonDAL<User>();
            return dal.Add(user);
        }

        public User QueryUserById(long Id)
        {
            using (SSOContext db = new SSOContext())
            {
                return db.Users.Where(x => x.Id == Id).FirstOrDefault();
            }
        }

        public User QueryUserByAccount(string account)
        {
            using (SSOContext db = new SSOContext())
            {
                return db.Users.Where(x => x.Account == account).FirstOrDefault();
            }
        }

        public List<User> GetAllUsers()
        {
            using (SSOContext db = new SSOContext())
            {
                return db.Users.ToList();
            }
        }
    }
}
