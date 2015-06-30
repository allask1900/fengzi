using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FZ.Spider.Common
{
    public class RegexHelper
    {
        private static ILog logger = LogManager.GetLogger(typeof(RegexHelper).FullName);
        private static TimeSpan regexTimeout = TimeSpan.FromSeconds(5);
        /// <summary>
        /// 获取一段匹配代码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        public static string GetMatchValue(string input, string parttern)
        {
            return new Regex(parttern, RegexOptions.IgnoreCase | RegexOptions.Singleline, regexTimeout).Match(input).Value;
        }
        /// <summary>
        /// 获取多段匹配代码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        public static List<string> GetMatchValues(string input, string parttern)
        {
            List<string> list = new List<string>();
            try
            {
                MatchCollection mc = new Regex(parttern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace, regexTimeout).Matches(input);
                foreach (Match item in mc)
                {
                    list.Add(item.Value);
                }
            }
            catch (Exception ex)
            {
                logger.Error(input + parttern, ex);
            }
            return list;
        }

        /// <summary>
        /// 获取单个匹配
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        public static Match GetMatch(string input, string parttern)
        {
            return new Regex(parttern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace, regexTimeout).Match(input);
        }


        /// <summary>
        /// 获取一个匹配的一个groupname的值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static string GetMatchGroupValue(string input, string parttern, string groupName)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(parttern) || string.IsNullOrEmpty(groupName))
                return "";
            return new Regex(parttern, RegexOptions.IgnoreCase, regexTimeout).Match(input).Groups[groupName].Value.Trim();
        }

        /// <summary>
        /// 获取单个groupname的多个值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static List<string> GetMatchGroupValues(string input, string parttern, string groupName)
        {
            List<string> values = new List<string>();
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(parttern) || string.IsNullOrEmpty(groupName))
                return values;
            System.Text.RegularExpressions.MatchCollection matchs = MatchCollection(input, parttern);
            foreach (Match mc in matchs)
            {
                values.Add(mc.Groups[groupName].Value.Trim());
            }
            return values;
        }

        /// <summary>
        /// 匹配多个parttern获取一段匹配代码
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        public static string GetMatchValueByPartterns(string input, string partterns)
        {
            bool isAnd = false;
            if (partterns.IndexOf("&&&&&") > 0) isAnd = true;
            string[] parts=partterns.Split(new string[]{"&&&&&","|||||"},StringSplitOptions.RemoveEmptyEntries);
            string returnCode=string.Empty;
            foreach (string part in parts)
            {
                string code=new Regex(part, RegexOptions.IgnoreCase | RegexOptions.Singleline|RegexOptions.IgnorePatternWhitespace, regexTimeout).Match(input).Value;
                if (isAnd)
                {
                    returnCode = returnCode + code;
                }
                else
                { 
                    if(code!=string.Empty)
                        return  code;
                }
            }
            return returnCode;
        }
        /// <summary>
        /// 获取多个匹配集合
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parttern"></param>
        /// <returns></returns>
        public static MatchCollection MatchCollection(string input, string parttern)
        {
            Regex reg = new Regex(parttern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace, regexTimeout);
            MatchCollection mc = reg.Matches(input);
            return mc;
        }

    }


    /// <summary>
    /// 正则表达式集合
    /// </summary>
    public class ItemRegexList : List<string>
    {
        /// <summary>
        /// 正则表达式之间的逻辑关系(or / and )
        /// </summary>
        public bool IsAnd = false;

        public void AddItemRegexs(string[] regs)
        {
            foreach (string reg in regs)
            {
                this.Add(reg.Trim());
            }
        }
    }
}
