using IdentityServerWithAspNetIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public interface ISendMessageLogService
    {
        bool InsertSendMessageLog(SendMessageLog sendMessageLog);
        bool IsSmsCodeRight(string phoneNumber, string smsCode);
    }
}
