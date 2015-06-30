using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using FZ.Spider.Configuration;

namespace FZ.Spider.Common
{
    public class CommonFun
    {      
        /// <summary>
        /// ͼƬ���ת��Ϊ����
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        public static int GetImageType(string ImagePath)
        {
            string ImageType = ImagePath.Substring(ImagePath.LastIndexOf(".") + 1).ToLower();
            int Type;
            switch (ImageType)
            {
                case "jpg":
                    Type = 1;
                    break;
                case "gif":
                    Type = 2;
                    break;
                default:
                    Type = 1;
                    break;
            }
            return Type;
        }

        public static string CheckDirectory(string strDir)
        {
            if (!Directory.Exists(strDir))
            {
                Directory.CreateDirectory(strDir);
            }
            return strDir;
        }

        /// <summary>
        /// ����Ƿ���SqlΣ���ַ�
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeSqlString(string str)
        {

            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// ����Ƿ���Σ�յĿ����������ӵ��ַ���
        /// </summary>
        /// <param name="str">Ҫ�ж��ַ���</param>
        /// <returns>�жϽ��</returns>
        public static bool IsSafeUserInfoString(string str)
        {
            return !Regex.IsMatch(str, @"^\s*$|^c:\\con\\con$|[%,\*" + "\"" + @"\s\t\<\>\&]|�ο�|^Guest");
        }
        /// <summary>
        /// �Ƿ��ǹ��˵��û���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="stringarray"></param>
        /// <returns>bool</returns>
        public static bool IsBanUsername(string str)
        {
            string[] stringarray ={"Administrator","Admin","����Ա","����"};
            Regex r;
            for (int i=0;i<stringarray.Length;i++)
            {
                r = new Regex(string.Format("^{0}$", stringarray[i]), RegexOptions.IgnoreCase);
                if (r.IsMatch(str))
                {
                    return true;
                }
            }
            return false; 
        }
        /// <summary>
        /// MD5����
        /// </summary>
        /// <param name="str">ԭʼ�ַ���</param>
        /// <returns>MD5���</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
        /// <summary>
        /// ��õ�ǰҳ��ͻ��˵�IP
        /// </summary>
        /// <returns>��ǰҳ��ͻ��˵�IP</returns>
        public static string GetIP()
        {


            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (string.IsNullOrEmpty(result) || !IsIP(result))
            {
                return "127.0.0.1";
            }

            return result;

        }
        /// <summary>
        /// IP��ַ�������һλ(123.23.56.*)
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public static string GetShowIP(string IP)
        {
            int i = IP.LastIndexOf(".");
            if (i > 0)
            {
                return IP.Substring(0, i+1)+"*";
            }
            return IP;
        }
        /// <summary>
        /// �Ƿ�Ϊip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");

        }
        /// <summary>
        /// �����̳cookieֵ
        /// </summary>
        /// <param name="strName">��</param>
        /// <returns>ֵ</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies["view"] != null && HttpContext.Current.Request.Cookies["view"][strName] != null)
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["view"][strName].ToString());
            }
            return "";
        }
        /// <summary>
        /// ����Cookies
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="NickName"></param>
        /// <param name="expires">����ʱ��(����)</param>
        /// <returns></returns>
        public static void SetCookie(int UserID, string UserName, int expires)
        {
            HttpCookie cookie = new HttpCookie("view");
            cookie.Values["userid"] = UserID.ToString();
            cookie.Values["name"] = HttpUtility.UrlEncode(UserName);
            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddMinutes(expires);
            }
            cookie.Domain = Configs.Domain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// �����¼�û���cookie
        /// </summary>
        public static void ClearUserCookie(string cookieName)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            string cookieDomain =Configs.Domain;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// �Ƿ�Ϊ��Ч��
        /// </summary>
        /// <param name="host">����</param>
        /// <returns></returns>
        public static bool IsValidDomain(string host)
        {
            Regex r = new Regex(@"^\d+$");
            if (host.IndexOf(".") == -1)
            {
                return false;
            }
            return r.IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }
        /// <summary>
        /// ���ָ��Url������ֵ
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static string GetQueryString(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.QueryString[strName];
        }
        /// <summary>
        /// ���ָ��Url����������ֵ(�������򷵻�-1)
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static int GetQueryInt(string strName)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return -1;
            }
            int result = -1;
            int.TryParse(HttpContext.Current.Request.QueryString[strName],out result);
            return result;
        }
        /// <summary>
        /// ���ָ��Url����������ֵ(�������򷵻�-1)
        /// </summary>
        /// <param name="strName">Url����</param>
        /// <returns>Url������ֵ</returns>
        public static int GetQueryInt(string strName,int defaultValue)
        {
            if (HttpContext.Current.Request.QueryString[strName] != null)
            {
                int.TryParse(HttpContext.Current.Request.QueryString[strName], out defaultValue);
            } 
            return defaultValue;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="defaultValue">false</param>
        /// <returns></returns>
        public static bool GetQueryBool(string strName, bool defaultValue)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
            {
                return defaultValue;
            }
            return bool.Parse(HttpContext.Current.Request.QueryString[strName]);
        }
        /// <summary>
        /// ���ָ����������ֵ
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������ֵ</returns>
        public static string GetFormString(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return "";
            }
            return HttpContext.Current.Request.Form[strName];
        }
        /// <summary>
        /// ���ָ��������������ֵ(�������򷵻�-1)
        /// </summary>
        /// <param name="strName">������</param>
        /// <returns>��������ֵ</returns>
        public static int GetFormInt(string strName)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
            {
                return -1;
            }
            return int.Parse(HttpContext.Current.Request.Form[strName]);
        }
        /// <summary>
        /// ת��ʧ�ܷ���0
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static int StrToInt(string Str)
        {
            int i = 0;
            if (Str != null && Str != string.Empty)
            {
                int.TryParse(Str, out i);
            } 
            return i;

        } 
        /// <summary>
        /// ת��ʧ�ܷ���Ĭ��ֵ
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="defaultInt"></param>
        /// <returns></returns>
        public static int StrToInt(string Str,int defaultInt)
        {
            int i = defaultInt;
            if (Str != null && Str != string.Empty)
            {
                if (!int.TryParse(Str, out i)) i = defaultInt;
            }
            return i;

        }
        /// <summary>
        /// �жϵ�ַ�Ƿ����ڱ�վ��
        /// </summary>
        /// <param name="referrer"></param>
        /// <returns></returns>
        public static bool ReferrerIsDomain(string referrer)
        {
            if (referrer == null || referrer == string.Empty)
            {
                return false;
            }
            if (referrer.IndexOf(Configs.Domain) > -1)
            {
                return true;
            }
            return false;
        }
        public static string GetStarString(int Score, int userCount)
        {
            if (userCount == 0)
            {
                userCount = 1;
                Score = 0;
            }
            int starcount =System.Math.Min(5,Score / userCount);
            int starhalf = System.Math.Min(1,Score % userCount);
            int starbrdr=5-System.Math.Min(starcount+starhalf,5);
            StringBuilder sbStar = new StringBuilder("");
            for (int i = 0; i < starcount; i++)
            {
                sbStar.Append("<span class=\"sprite3 star star-full\"></span>");
            }
            if (starhalf > 0) sbStar.Append("<span class=\"sprite3 star star-half\"></span>");
            for (int j = 0; j < starbrdr; j++)
            {
                sbStar.Append("<span class=\"sprite3 star star-empty\"></span>");
            }
            return sbStar.ToString();
        }         
        public static string GetStarLevel(int Score, int userCount)
        {
            string level = "0";
            int ScoreStar=0;
            if (userCount == 0)
            {
                return level;
            }
            ScoreStar = Score / userCount;
            level = ScoreStar.ToString();
            if (Score % userCount > 0) level = level+"_5";
            return level;
        }
        public static string GetHeaderCss(int FirstCategoryID)
        {
            string cssname = "other";
            switch (FirstCategoryID)
            {
                case 1:
                    cssname = "default";
                    break;
                case 10:
                    cssname = "computers";
                    break;
                case 11:
                    cssname = "electronics";
                    break;
                case 12:
                    cssname = "book";
                    break;
                case 13:
                    cssname = "cosmetics";
                    break;
                case 24:
                    cssname = "shoesbags";
                    break;
                case 18:
                    cssname = "toys";
                    break;
                case 19:
                    cssname = "baby";
                    break;
                case 20:
                    cssname = "home";
                    break;
                case 21:
                    cssname = "sports";
                    break;
                case 22:
                    cssname = "clothing";
                    break;
                case 23:
                    cssname = "office";
                    break;
                default:
                    cssname = "other";
                    break;
            }
            return cssname;
        }
        /// <summary>
        /// SEM�ؼ���״̬
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static  string GetStatusName(int status)
        {
            string statusName = "";
            switch (status)
            {
                case 0:
                    statusName = "�½�";
                    break;
                case 1:
                    statusName = "�����";
                    break;
                case 2:
                    statusName = "������";
                    break;
                case 3:
                    statusName = "��ʽʹ��";
                    break;
                case 4:
                    statusName = "����";
                    break;
                default:
                    break;
            }
            return statusName;
        }
        public static bool IsNumber(string str)
        {
            string temp="0123456789.";
            if(string.IsNullOrEmpty(str))
                return false;
            for(int i=0;i<str.Length;i++)
            {
                if(temp.IndexOf(str[i])==-1)
                    return false;
            }
            return true;
        }

        public static List<int[]> PriceFilterRange(float medianPrice)
        {
            List<int[]> pfr = new List<int[]>();
            int pool = Convert.ToInt32(medianPrice/2.5);
            if (10<=pool&&pool < 100)
                pool = (pool / 5) * 5;
            else if (pool>=100&&pool < 1000)
                pool = (pool / 50) * 50;
            else if (pool >= 1000 && pool < 10000)
                pool = (pool / 500) * 500;
            for (int i = 0; i < 5; i++)
            {
                int[] fr = new int[2] { i * pool, (i + 1) * pool };
                
                pfr.Add(fr);
            }            
            return pfr;
        }
        /// <summary>
        /// ������Ȩ�룬����url��������
        /// </summary>
        /// <returns></returns>
        public static string RequestAuthorizationCode()
        {
            return MD5("zzfu" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff").Substring(0,18) + "allask1900");
        }
    }
}
