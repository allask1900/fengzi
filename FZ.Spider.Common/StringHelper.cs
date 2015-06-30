using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FZ.Spider.Configuration; 
namespace FZ.Spider.Common
{
    public class StringHelper
    {
        #region 字符串<==>数组
        /// <summary>
        /// 给ID字符串加上分割符如("12"转化为"|12|")
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>|ID|</returns>
        public static string AddSplitCharForID(int ID)
        {
            return AddSplitCharForIDS(ID.ToString());
        }
        /// <summary>
        /// 给字符串加上分割符如("12|24"转化为"|12|24|")
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public static string AddSplitCharForIDS(string IDS)
        {
            return AddSplitCharForIDS(IDS, Configs.Constant.SplitCharForIDS);
        }
        private static string AddSplitCharForIDS(string IDS, char ch)
        {
            if (IDS == null || IDS == string.Empty) return string.Empty;
            if (IDS[0] != ch) IDS = ch.ToString() + IDS;
            if (IDS[IDS.Length - 1] != ch) IDS = IDS + ch.ToString();
            return IDS;
        }
        
        /// <summary>
        /// 使用空格将分隔符 | 替换("|12|24|"替换为" 12 24 ")
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public static string UseSpaceReplaceSplitCharForIDS(string IDS)
        {
            return IDS.Replace(Configs.Constant.SplitCharForIDS.ToString(), " ");
        }
        
        /// <summary>
        /// 用符号 "====>"分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForReplace(string Str)
        {
            string[] ss= Str.Split(new String[] { Configs.Constant.SplitStringForReplace }, StringSplitOptions.RemoveEmptyEntries);
            return ArrayItemTrim(ss);
        }
        public static string[] ArrayItemTrim(string[] ss)
        {
            for (int i = 0; i < ss.Length; i++)
            {
                ss[i] = ss[i].Trim();
            }
            return ss;
        }
        /// <summary>
        /// 用符号 "====>"分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForReplace(string Str, bool hasEmpty)
        {
            string[] ss;
            if (hasEmpty)
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForReplace }, StringSplitOptions.None);
            }
            else
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForReplace }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ArrayItemTrim(ss);
        }
        /// <summary>
        /// 用符号 "---->"分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForReplace_Child(string Str)
        {
            string[] ss = Str.Split(new String[] { Configs.Constant.SplitStringForReplace_Child }, StringSplitOptions.None);
            return ArrayItemTrim(ss);
        }
        /// <summary>
        /// 用符号 "---->"分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForReplace_Child(string Str, bool hasEmpty)
        {
            string[] ss;
            if (hasEmpty)
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForReplace_Child }, StringSplitOptions.None);
            }
            else
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForReplace_Child }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ArrayItemTrim(ss);
        }
        
        /// <summary>
        /// 用符号 "====="分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForArray(string Str)
        {
            return SplitForArray(Str, false); 
        }
        /// <summary>
        /// 用符号 "-----"分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForArray_Child(string Str)
        {
            return SplitForArray_Child(Str, false); 
        }
        /// <summary>
        /// 用符号 "-----"分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForArray_Child(string Str, bool hasEmpty)
        {
            string[] ss;
            if (hasEmpty)
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForArray_Child }, StringSplitOptions.None);
            }
            else
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForArray_Child }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ArrayItemTrim(ss);
        }
        /// <summary>
        /// 分割符 ===
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SpiderForKeyValue(string Str)
        {
            string[] ss = Str.Split(new String[] { Configs.Constant.SplitStringForKeyValue }, StringSplitOptions.None);
            return ArrayItemTrim(ss);
        }
        public static string[] SpiderForKeyValue(string Str,string Separator)
        {
            string[] ss = Str.Split(new String[] { Separator }, StringSplitOptions.None);
            return ArrayItemTrim(ss);
        }
        public static string[] SplitForArray(string Str, string SplitString)
        {
            string[] ss = Str.Split(new String[] { SplitString }, StringSplitOptions.RemoveEmptyEntries);
            
            return ArrayItemTrim(ss);
        }
        /// <summary>
        /// 用符号 "====="分隔的字符串数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForArray(string Str,bool hasEmpty)
        {
            string[] ss;
            if (hasEmpty)
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForArray }, StringSplitOptions.None);
            }
            else
            {
                 ss = Str.Split(new String[] { Configs.Constant.SplitStringForArray }, StringSplitOptions.RemoveEmptyEntries);
            }
            return ArrayItemTrim(ss);
        }
        /// <summary>
        /// 用符号 "|"分隔的数值数组
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static int[] SplitForInts(string Str)
        {
            string[] IDS =ArrayItemTrim(Str.Split(new char[] { Configs.Constant.SplitCharForIDS }, StringSplitOptions.RemoveEmptyEntries));
            int[] ids = new int[IDS.Length];
            for (int i = 0; i < IDS.Length; i++)
            {
                ids[i] = Convert.ToInt32(IDS[i]);
            }
            return ids;
        }
        #endregion        

        #region 字符转换为数值
        /// <summary>
        /// 为转换为数值准备
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetLineTextForNumber(string Str)
        {
            if (Str == null || Str == "")
            {
                return string.Empty;
            }
            Str = HtmlCode(Str).Replace(",", "").Replace("$", "");
            return GetLineTextStringNoSpace(Str);
        }

        #endregion

        /// <summary>
        /// 得到行文本(不包括任何标签及空格、换行),用于获取名称等不包含空格的字符串
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetLineTextStringNoSpace(string Str)
        {
            return GetLineTextString(Str).Replace(" ", "");
        }

        /// <summary>
        /// 对文本内容过滤，删除链接、删除Image 删除tag的样式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDescription(string str,string sitename)
        {
            String desc=Regex.Replace(str,@"<a[^>]*?href=[^>]*>[\s\S]*?</a>","");
            desc = Regex.Replace(desc, @"<img[^>]*?>", "");
            desc = Regex.Replace(desc, @"<(?<t>[\w]*?)\s[^>]*?>", "<${t}>");
            desc = Regex.Replace(desc, @"<h3\s[^>]*?>", "<h3>");
            desc = Regex.Replace(desc, @"<h2\s[^>]*?>", "<h2>");
            desc = Regex.Replace(desc, @"<h1\s[^>]*?>", "<h1>");
            desc = Regex.Replace(desc, sitename, "");
            int index=sitename.IndexOf('.');
            if(index>0)
                desc = Regex.Replace(desc, sitename.Substring(0,index), "");
            return desc;
        }
        /// <summary>
        /// 得到行文本(不包括任何标签及首尾空格、换行)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetLineTextString(string Str)
        {
            return DelSpaceStr(GetTextString(Str));
        }              
        /// <summary>
        /// 得到文本字符串(不包括任何标签)
        /// </summary>
        /// <returns></returns>
        public static string GetTextString(string Str)
        {
            string str = DelHtmlTag(Str);
            str = WipeScript(str);
            return str;
        }
        /// <summary>
        /// 得到包含Html代码的字符串(不包含js代码)
        /// </summary>
        /// <returns></returns>
        public static string GetCodeString(string HTML)
        {
            return WipeScript(HTML);
        }        
        /// <summary>
        /// html代码转换
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string HtmlCode(string Str)
        {
            Str = Str.Replace("&amp;", "&");
            Str = Str.Replace("&gt;", ">");
            Str = Str.Replace("&lt;", "<");
            Str = Str.Replace("&nbsp;", " ");
            Str = Str.Replace("&#160;", " ");
            Str = Str.Replace("&quot;", "\"");
            Str = Str.Replace("&#39;", "'");
            Str = Str.Replace("amp;", "");
            Str = Str.Replace("gt;", "");
            Str = Str.Replace("lt;", "");
            Str = Str.Trim();
            return Str;
        }
        /// <summary>
        /// 删除回车和换行符及前后的空格
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        private static string DelSpaceStr(string Str)
        {
            if(String.IsNullOrEmpty(Str)) return string.Empty;
            Str = Str.Replace("\t", "");
            Str = Str.Replace("\r", "");
            Str = Str.Replace("\n", "");
            Str = Str.Trim();
            return Str;
        }
        /// <summary>
        /// 删除js脚本 及样式代码
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string DelScriptStlye(string html)
        {
            return WipeScript(Regex.Replace(html, @"<style[^>]*?>[\s\S]*?<[\s]*?/[\s]*?style>", ""));
        }
        /// <summary>
        /// 过滤JS脚本          
        /// </summary>
        /// <param name="html">要过滤的内容</param>
        /// <returns></returns>
        /// <applet> <body> <embed> <frame> <script><frameset> <html><iframe><img><style><layer><link> <ilayer><meta><object>
        /// 这些标签都可能进行注入攻击.在 HTML 4.01 中，不赞成使用 applet 元素        
        /// img的攻击样式为 <img src="javascript: alert('hello');">等，所以去掉所有的javascript代码       
        private static string WipeScript(string html)
        {
            if (string.IsNullOrEmpty(html)) return html;
            //过滤<script></script>标记
            html = Regex.Replace(html, @"<script[\s\S]*?</script[^>]*>", "", RegexOptions.IgnoreCase);

            //过滤href=javascript: (<A>) 属性
            html = Regex.Replace(html, @"\shref[\s]*=[^>]*script[\s]*:", "", RegexOptions.IgnoreCase);

            //过滤其它控件的on...事件
            html = Regex.Replace(html, @"\son[mouse|click|key|blur|dblclick|focus][^>]*?=", "", RegexOptions.IgnoreCase);

            //过滤iframe
            html = Regex.Replace(html, @"<iframe[^>]*?>[\s]*?</iframe[^>]*?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<iframe[^>]*?>", "", RegexOptions.IgnoreCase);

            //过滤frameset
            html = Regex.Replace(html, @"<frameset[^>]*?>[\s\S]*?</frameset[^>]*?>", "", RegexOptions.IgnoreCase);


            //过滤所有javascript
            html = Regex.Replace(html, @"javascript:", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @":*expression", "", RegexOptions.IgnoreCase);

            //过滤所有HTML说明标签
            html = Regex.Replace(html, @"<!--[^>]*?-->", "", RegexOptions.IgnoreCase);

            //过滤src=javascript: (<img><embed>) 属性
            html = Regex.Replace(html, @"\ssrc[\s]*=[^>]*?script[\s]*:", "", RegexOptions.IgnoreCase);

            //过滤applet
            html = Regex.Replace(html, @"<applet[\s\S]+</applet[\s]*>", "", RegexOptions.IgnoreCase);

            return html;
        }
        /// <summary>
        /// 排除HTML标签
        /// </summary>
        /// <param name="HtmlStr"></param>
        /// <returns></returns>
        public static string DelHtmlTag(string HtmlStr)
        {
            HtmlStr = Regex.Replace(HtmlStr, @"<br[^>]*?>", "\r\n");
            HtmlStr = Regex.Replace(HtmlStr, @"<style[^>]*?>[\s\S]*?<[\s]*?/[\s]*?style>", "");
            HtmlStr= Regex.Replace(HtmlStr, "<[^>]*?>", "");
            return Regex.Replace(HtmlStr, "\r\n", "<br>").Trim();
        }
        /// <summary>
        /// 判断字符中英文或数字(中文 1 英文 2 数字 3 其它 4)
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int IsChar(char ch)
        {
            if (System.Math.Abs(Convert.ToInt32(ch)) > 255)
            {
                return 1;
            }
            else if (System.Char.IsNumber(ch))
            {
                return 3;
            }
            else if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }      
        /// <summary>
        ///  判断字符串是否能转换为数字(int)
        /// </summary>       
        public static bool IsNumberByStr(string str)
        {
            bool bnlResult = true;
            if (str.Length <= 10 && str.Length > 0)
            {
                for (int i = 0; i < str.Length && bnlResult == true; ++i)
                {
                    bnlResult = Char.IsNumber(str, i);
                }
            }
            else
            {
                bnlResult = false;
            }
            return bnlResult;
        }
       
        public static string GetTitleByLen(string title, int len,bool IsHz)
        {
            if (title.Length <= len)
            {
                return title;
            }
            else if (IsHz)
            {
                return title.Substring(0, len) + "..";
            }
            else
            {
                return title.Substring(0, len);
            }
        }
        public static string GetShortDescription(string desc, int len, bool IsLink)
        {
            if (desc.Length <= len)
            {
                return  desc ;
            }
            else if (IsLink)
            {
                return desc.Substring(0, len) + "...";
            }
            else
            {
                return  desc.Substring(0, len);
            }
        }
        public static string GetPageDescription(string desc)
        {
            string Description =desc.Replace("\"", "'").Substring(0,120);
            for (int i = 120; i < 200; i++)
            {
                if (StringHelper.IsChar(desc[i]) == 2 || StringHelper.IsChar(desc[i]) == 3 || desc[i] == '\'')
                {
                    Description = Description + desc[i].ToString();
                }
                else
                {
                    break;
                }
            }
            return Description;
        }      
        public static string Substring(string Str, int len,string s)
        {
            if (Str.Length <= len)
            {
                return Str;
            }
            else
            {
                return Str.Substring(0, len)+s;
            }
        } 
        /// <summary> 
        /// 转全角的函数(SBC case)  全角空格为12288,半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是:均相差65248
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角</returns>        
        public static string ToSBC(string input)
        { //半角转全角:
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288; continue;
                }
                if (c[i] < 127) c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        } 
        /// <summary>
        /// 转半角的函数(DBC case)
        /// 全角空格为12288,半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是:均相差65248
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32; continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #region 关键词过滤
        /// <summary>
        /// 检查是否包含非法字符(用于紧急更新)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static bool CheckStringUrgent(string Str)
        {
            if (Configs.CheckStringUrgent != null && Configs.CheckStringUrgent.Length > 0 && Str != null && Str != "")
            {
                for (int i = 0; i < Configs.CheckStringUrgent.Length; i++)
                {
                    if (Str.IndexOf(Configs.CheckStringUrgent[i]) >= 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
