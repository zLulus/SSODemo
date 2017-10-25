using IdentityServerWithAspNetIdentity.Data;
using IdentityServerWithAspNetIdentity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public class SendMessageLogService: ISendMessageLogService
    {
        private ApplicationDbContext dbContext { get; set; }
        public SendMessageLogService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public bool InsertSendMessageLog(SendMessageLog sendMessageLog)
        {
            dbContext.SendMessageLogs.Add(sendMessageLog);
            return dbContext.SaveChanges() > 0;
        }

        public bool IsSmsCodeRight(string phoneNumber,string smsCode)
        {
            DateTime now = DateTime.Now;
            var model = dbContext.SendMessageLogs.Where(x => x.PhoneNumber == phoneNumber
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
