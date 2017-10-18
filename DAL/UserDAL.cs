using Model.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UserDAL
    {
        //todo 之后改成泛型
        public bool Add(User user)
        {
            try
            {
                SSOContext db = new SSOContext();
                db.Users.Add(user);
                int changeRecordCount = db.SaveChanges();
                return changeRecordCount > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
