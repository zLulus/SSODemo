using IdentityServerWithAspNetIdentity.Data;
using IdentityServerWithAspNetIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public class SendMessageLogService: ISendMessageLogService
    {
        public bool InsertSendMessageLog(SendMessageLog sendMessageLog)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(null))
            {
                dbContext.SendMessageLogs.Add(sendMessageLog);
                return dbContext.SaveChanges() > 0;
            }
        }

        public bool IsSmsCodeRight(string phoneNumber,string smsCode)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext(null))
            {
                DateTime now = DateTime.Now;
                var model= dbContext.SendMessageLogs.Where(x => x.PhoneNumber == phoneNumber
                        && x.SmsCode == smsCode
                        && !x.IsChecked
                        && x.InvalidTime >= now
                        && x.Sucess).OrderByDescending(x => x.Id).FirstOrDefault();
                if (model != null)
                {
                    model.IsChecked = true;
                    dbContext.SendMessageLogs.Update(model);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
