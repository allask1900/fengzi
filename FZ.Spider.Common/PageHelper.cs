using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using log4net;
using System.Collections.Concurrent;
using System.Threading;
namespace FZ.Spider.Common
{
    public class PageHelper
    {
        private static ILog logger = LogManager.GetLogger(typeof(PageHelper).FullName);
        private static ConcurrentDictionary<string, int> SiteReadCount = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<string, int> SiteReadErrorCount = new ConcurrentDictionary<string, int>();
        private static ConcurrentDictionary<string, long> ReadPageAvgTime = new ConcurrentDictionary<string, long>();
        /// <summary>
        /// 站点爬虫的频率 int[0]=SpiderReadCount;int[1]=SpiderSleepTime(单位秒)
        /// </summary>
        private static Dictionary<string, int[]> spiderFreq = new Dictionary<string, int[]>();
        /// <summary>
        /// 设置站点爬虫频率(单个站点爬虫启动时设置)
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="spiderReadCount"></param>
        /// <param name="spiderSleepTime"></param>
        public static void SetSpiderFreq(string siteName, int spiderReadCount, int spiderSleepTime)
        {
            if (spiderReadCount == 0 || spiderSleepTime == 0)
                return;

            int[] sf= new int[2] { spiderReadCount, spiderSleepTime };
            if (spiderFreq.ContainsKey(siteName))
                spiderFreq[siteName] = sf;
            else
                spiderFreq.Add(siteName, sf);
        }
        /// <summary>
        /// 检查站点是否有爬虫频率设置,以判断当前线程是否需要Sleep
        /// </summary>
        /// <param name="siteName"></param>
        private static void CheckSpiderFreq(string siteName)
        {
            int ReadCount = GetSiteReadCount(siteName);

             long time=0;
             if(ReadPageAvgTime.ContainsKey(siteName))
                  time=ReadPageAvgTime[siteName];

           

            if (ReadCount == 0) return;

            long avgTime = time / ReadCount;

            if (spiderFreq.ContainsKey(siteName))
            {
                int[] sf = spiderFreq[siteName];
                
                if (ReadCount % sf[0] == 0)
                {


                    logger.Info("站点(" + siteName + "),ReadPage总数(" + ReadCount + "),读取页面总耗时(" + time + "),平均耗时(" + avgTime + "),启动休眠(时间:" + sf[1] + "s)");
                    Thread.Sleep(sf[1] * 1000);

                }
            }
            else
            { 
                if (ReadCount % 1000 == 0)
                {
                    logger.Info("站点(" + siteName + "),ReadPage总数(" + ReadCount + "),读取页面总耗时(" + time + "),平均耗时(" + avgTime + "),启动休眠(时间: 60s)");
                    Thread.Sleep(30000);
                }
            }

        }
        /// <summary>
        /// 获取站点readpage count
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static int GetSiteReadCount(string siteName)
        {
            if (!SiteReadCount.ContainsKey(siteName))
                return 0;
            return SiteReadCount[siteName];
        }
        /// <summary>
        /// 获取站点ReadPage error count
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public static int GetSiteReadErrorCount(string siteName)
        {
            if (!SiteReadErrorCount.ContainsKey(siteName))
                return 0;
            return SiteReadErrorCount[siteName];
        }

        public static void SetSiteReadErrorCount(string siteName)
        {

            SiteReadErrorCount.AddOrUpdate(siteName, 1, (k, v) => v=v+1);
           
        }

        public static void SetSiteReadCount(string siteName)
        {

            SiteReadCount.AddOrUpdate(siteName, 1, (k, v) => v=v+1);

        }

        public static string ReadUrl(string Url, System.Text.Encoding ed,string SiteName)
        {
           return ReadUrl(Url, ed, SiteName, null);
        }
        public static string ReadUrl(string Url, System.Text.Encoding ed, string SiteName,CookieContainer cc)
        {
            long startTime = DateTime.Now.Ticks;
            CheckSpiderFreq(SiteName);
            SetSiteReadCount(SiteName);
            string strRead = "";
            if (string.IsNullOrEmpty(Url))
                return "";
            try
            {
                
                Url = UrlProcess.UrlEncode(Url);
                System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url);
                wr.Timeout = 15000;
                wr.KeepAlive = false;
                wr.ProtocolVersion = HttpVersion.Version10;
                //在公司临时使用==================
                //WebProxy myProxy = WebProxy.GetDefaultProxy();
                //myProxy.Credentials = CredentialCache.DefaultCredentials;
                //wr.Proxy = myProxy;
                //==================
                wr.Referer = Url;
                wr.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/6.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E; InfoPath.2) ";
                if (cc != null)
                    wr.CookieContainer = cc;           
                System.IO.Stream rc = wr.GetResponse().GetResponseStream();
                System.IO.StreamReader read = new System.IO.StreamReader(rc, ed);
                strRead = read.ReadToEnd();               
            }
            catch (Exception e)
            {
                logger.Error(" (SiteReadCount=" + GetSiteReadCount(SiteName) + ",SiteReadErrorCount="+GetSiteReadErrorCount(SiteName)+")" + Url + "   " + e.Message, e);
                SetSiteReadErrorCount(SiteName);
            }
            long time= (DateTime.Now.Ticks - startTime)/10000;
            ReadPageAvgTime.AddOrUpdate(SiteName, time, (k, v) => v = v + time);
            return strRead;
        }
        public static string ReadTemplet(string FilePath)
        {
            string TempletHtml="";
            if (File.Exists(FilePath))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(FilePath, System.Text.Encoding.UTF8);
                TempletHtml = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            return TempletHtml;
        } 
 
        public static string PostUrl(string Url, string postData,System.Text.Encoding ed,string SiteName)
        {
            string strRead = "";
            if (Url.Length > 1)
            {
                System.Net.HttpWebRequest wr = (HttpWebRequest)System.Net.HttpWebRequest.Create(Url);
                wr.Method = "POST";
                wr.ContentType = "application/x-www-form-urlencoded";
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] byte1 = encoding.GetBytes(postData);
                wr.ContentLength = byte1.Length;
                Stream newStream = wr.GetRequestStream();
                newStream.Write(byte1, 0, byte1.Length);
                try
                {
                    System.IO.Stream rc = wr.GetResponse().GetResponseStream();
                    System.IO.StreamReader read = new System.IO.StreamReader(rc,ed);
                    strRead = read.ReadToEnd();
                    SetSiteReadCount(SiteName);
                }
                catch (Exception e)
                {
                    logger.Error(" (SiteReadCount=" + GetSiteReadCount(SiteName) + ",SiteReadErrorCount=" + GetSiteReadErrorCount(SiteName) + ") PostUrl(" + Url + ") " + e.Message, e);
                    SetSiteReadErrorCount(SiteName);
                }
            }
            return strRead;
        }

        public static string ReadUrl(string Url)
        {
            string strRead = "";           
            try
            {                 
                System.Net.HttpWebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Url);
                wr.Timeout =10*3600*1000;                
                System.IO.Stream rc = wr.GetResponse().GetResponseStream();
                System.IO.StreamReader read = new System.IO.StreamReader(rc,Encoding.UTF8);
                strRead = read.ReadToEnd();
            }
            catch (Exception e)
            {
                strRead = "read url error!";
            } 
            return strRead;
        }
    }
}