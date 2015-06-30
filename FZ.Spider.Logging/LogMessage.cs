using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace FZ.Spider.Logging
{
    public class LogMessage
    {
        public LogMessage() { }
        
       
        public string SiteName
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public LogMessage(string siteName, string message)
        {
            SiteName = siteName;
            Message = message;
        }
    }
}
