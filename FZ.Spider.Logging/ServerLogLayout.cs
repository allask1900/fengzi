using System;
using System.Collections;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;
using log4net.Util;
using log4net.Layout;

namespace FZ.Spider.Logging
{
    public class ServerLogLayout: PatternLayout
    {
        public ServerLogLayout()
        {  
            this.AddConverter("sitename", typeof(SiteNamePatternConverter));
            this.AddConverter("message", typeof(MessagePatternConverter));
        }
    }
  
}