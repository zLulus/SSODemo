using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServerWithAspNetIdentity.Services
{
    public class MessageSender: IMessageSender
    {
        #region 发送短信变量
        // 设置为您的apikey(https://www.yunpian.com)可查
        private static string apikey = "636dcd2a7689747a1d0cb0b8312aa2f9";
        // 智能模板发送短信url
        private static string url_send_sms = "https://sms.yunpian.com/v2/sms/single_send.json";
        // 指定模板发送短信url
        string url_tpl_sms = "https://sms.yunpian.com/v2/sms/tpl_single_send.json";
        #endregion

        private string title = "我call你";

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string GetRandomNums(int length=6)
        {
            var ints = new int[length];
            for (var i = 0; i < length; i++)
            {
                Random random = new Random();
                ints[i] = random.Next(0, 9);
            }
            string code = "";
            for(int i = 0; i < ints.Count(); i++)
            {
                code += ints[i];
            }
            return code;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="account"></param>
        /// <param name="finishTradeNumber"></param>
        public bool SendVerificationCode(string phoneNumber, string code)
        {
            // 发送的手机号
            string mobile = HttpUtility.UrlEncode(phoneNumber, Encoding.UTF8);
            string text = $"【{title}】您的验证码是{code}。如非本人操作，请忽略本短信。";
            string data_send_sms = "apikey=" + apikey + "&mobile=" + mobile + "&text=" + text;
            return SendMsgToOneTarget(url_send_sms, data_send_sms);
        }

        /// <summary>
        /// 智能模板推送方式-发短信通用方法
        /// </summary>
        /// <param name="postDataStr"></param>
        public static bool SendMsgToOneTarget(string url, string postDataStr)
        {
            byte[] dataArray = Encoding.UTF8.GetBytes(postDataStr);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(dataArray, 0, dataArray.Length);
            dataStream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                String res = reader.ReadToEnd();
                reader.Close();
                Console.Write("Response Content:\n" + res + "\n");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
