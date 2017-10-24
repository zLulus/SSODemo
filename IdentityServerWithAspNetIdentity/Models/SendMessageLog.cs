using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Models
{
    public class SendMessageLog
    {
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// 发送电话
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 短信代码(验证码)
        /// </summary>
        public string SmsCode { get; set; }


        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime InvalidTime { get; set; }

        /// <summary>
        /// 是否发送成功
        /// </summary>
        public bool Sucess { get; set; }

        /// <summary>
        /// 是否已检测过
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
