using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using log4net;

namespace FZ.Spider.Common
{
    public class Email
    {
        private static ILog logger = LogManager.GetLogger(typeof(Email).FullName);
        /// <summary>
        /// 发送邮件函数
        /// </summary>
        /// <param name="ee"></param>
        public static void SendEmail(EmailEntity ee)
        {            
            MailAddress from = new MailAddress(ee.UserEmail);            
            MailAddress to = new MailAddress(ee.ToMail);
            MailMessage mailobj = new MailMessage(from, to);
            
            mailobj.Subject = ee.Subject;            
            mailobj.Body = ee.MailBody;
            if (ee.AttachFiles != null)
            {
                foreach (string attach in ee.AttachFiles)
                {
                    mailobj.Attachments.Add(new Attachment(attach));
                }
            }
            //邮件不是html格式
            mailobj.IsBodyHtml = false;
            //邮件编码格式
            mailobj.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //邮件优先级
            mailobj.Priority = MailPriority.High;            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;//经过ssl加密
            //不使用默认凭据访问服务器
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ee.UserEmail, ee.UserPswd);
            //使用network发送到smtp服务器
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                //开始发送邮件
                smtp.Send(mailobj);
            }
            catch(Exception ex)
            {

                logger.Error("发送至" + ee.UserEmail + "失败",ex);
            }

        }
    }
    public class EmailEntity
    {
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        public string UserEmail
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱帐号密码
        /// </summary>
        public string UserPswd
        {
            get;
            set;
        }
        /// <summary>
        /// 收件人邮箱
        /// </summary>
        public string ToMail
        { get; set; }
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string MailServer
        { get; set; }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string MailBody
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件附件
        /// </summary>
        public string[] AttachFiles
        {
            get;
            set;
        }
    }
}