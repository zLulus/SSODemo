using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public interface IMessageSender
    {
        string GetRandomNums(int length = 6);
        bool SendVerificationCode(string phoneNumber, string code);
    }
}
