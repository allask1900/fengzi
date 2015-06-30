using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FZ.Spider.Configuration;

namespace FZ.Spider.Common
{
    public class StatCookie
    {
        private static readonly DateTime dt_min = new DateTime(2013, 1, 1);
        private static Random rd = new Random();
         
        private const string StaticCookieKey = "dvid";

        public static UserCookie CheckCookie()
        {
            UserCookie uc = new UserCookie();
            if (HttpContext.Current.Request.Cookies == null || HttpContext.Current.Request.Cookies.Get(StaticCookieKey) == null)
            {
                AddStaticCookie();
            }
            uc.DVID = HttpContext.Current.Request.Cookies.Get(StaticCookieKey).Value;
            uc.FirstAccessTime = new DateTime(Convert.ToInt64(uc.DVID.Substring(0, uc.DVID.Length - 3)));

            UpdateCookie();

            uc.LastAccessTime = GetCookieTime(HttpContext.Current.Request.Cookies.Get("last").Value);

            return uc;
        }        
        /// <summary>
        /// 为访问用户设置一个长久Cookie
        /// </summary> 
        public static void AddStaticCookie()
        {
            HttpCookie cookie = new HttpCookie(StaticCookieKey);         
            cookie.Value = DateTime.Now.Ticks.ToString() + rd.Next(100, 999).ToString();
            cookie.Expires = DateTime.Now.AddYears(10);
            if (Configs.Domain != string.Empty)
                cookie.Domain = Configs.Domain;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        
        
        

        public static string GetCookieTime(DateTime dt)
        {            
            TimeSpan ts = new TimeSpan(DateTime.Now.Ticks).Subtract(new TimeSpan(dt_min.Ticks)).Duration();
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        /// <summary>
        /// 转换最后访问Cookie时间
        /// </summary>
        /// <param name="cookieValue"></param>
        /// <returns></returns>
        public static DateTime GetCookieTime(string cookieValue)
        {
            long ts_int = Convert.ToInt64(cookieValue); 
            TimeSpan ts = new TimeSpan(ts_int*10000).Add(new TimeSpan(dt_min.Ticks));
            return new DateTime(ts.Ticks);
        }

        public static void UpdateCookie()
        {
            HttpCookie cookie = new HttpCookie("last");
            cookie.Value = GetCookieTime(DateTime.Now);
            cookie.Expires = DateTime.Now.AddYears(10);
            if (Configs.Domain != string.Empty)
                cookie.Domain = Configs.Domain;

            if (HttpContext.Current.Request.Cookies != null &&
                HttpContext.Current.Request.Cookies.Get("last") != null)
            {

                HttpContext.Current.Response.SetCookie(cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        } 
    }
    public class UserCookie
    {
        public string DVID;
        public DateTime FirstAccessTime;
        public DateTime LastAccessTime;
    }
}
