using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZ.Spider.Logging.Appender
{
    public class AdoNetAppender:log4net.Appender.AdoNetAppender
    {
        /// <summary>
        /// 定时flush频率
        /// </summary>
        public string Interval
        {
            get
            {
                if (string.IsNullOrEmpty(m_interval))
                    return "60000";
                return m_interval;
            }
            set { m_interval = value; }
        }
        private string m_interval;
        /// <summary>
        /// 定时flush
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerFlush(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Flush();
        }
        override public void ActivateOptions()
        {
            base.ActivateOptions(); 
            System.Timers.Timer timer = new System.Timers.Timer(Convert.ToDouble(Interval));
            timer.Elapsed += TimerFlush;
            timer.Start();
        }
    }
}
