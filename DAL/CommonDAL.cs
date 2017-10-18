using Model.EntityFramework;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class CommonDAL<T> where T:class
    {
        //eg.https://code.msdn.microsoft.com/How-to-using-Entity-1464feea
        public bool Add(T entity)
        {
            using (SSOContext db = new SSOContext())
            {
                db.Add<T>(entity);
                int changeRecordCount = db.SaveChanges();
                return changeRecordCount > 0;
            } 
        }

        public bool Update(T entity)
        {
            using (SSOContext db = new SSOContext())
            {
                db.Update<T>(entity);
                int changeRecordCount = db.SaveChanges();
                return changeRecordCount > 0;
            }
        }

        public bool Delete(T entity)
        {
            using (SSOContext db = new SSOContext())
            {
                db.Remove<T>(entity);
                int changeRecordCount = db.SaveChanges();
                return changeRecordCount > 0;
            }
        }

        public T Query(T entity)
        {
            //SSOContext db = new SSOContext();
            //return db.CreateObjectSet<T>().
            return null;
        }
    }
}
