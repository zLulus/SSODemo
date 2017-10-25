using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Phone]
        public string UserName { get; set; }
        /// <summary>
        /// Token(用于更新密码)
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 短信验证码
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
