using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FZ.Spider.Configuration; 
namespace FZ.Spider.Common
{
    public class StringHelper
    {
        #region �ַ���<==>����
        /// <summary>
        /// ��ID�ַ������Ϸָ����("12"ת��Ϊ"|12|")
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>|ID|</returns>
        public static string AddSplitCharForID(int ID)
        {
            return AddSplitCharForIDS(ID.ToString());
        }
        /// <summary>
        /// ���ַ������Ϸָ����("12|24"ת��Ϊ"|12|24|")
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
        /// ʹ�ÿո񽫷ָ��� | �滻("|12|24|"�滻Ϊ" 12 24 ")
        /// </summary>
        /// <param name="IDS"></param>
        /// <returns></returns>
        public static string UseSpaceReplaceSplitCharForIDS(string IDS)
        {
            return IDS.Replace(Configs.Constant.SplitCharForIDS.ToString(), " ");
        }
        
        /// <summary>
        /// �÷��� "====>"�ָ����ַ�������
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
        /// �÷��� "====>"�ָ����ַ�������
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
        /// �÷��� "---->"�ָ����ַ�������
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForReplace_Child(string Str)
        {
            string[] ss = Str.Split(new String[] { Configs.Constant.SplitStringForReplace_Child }, StringSplitOptions.None);
            return ArrayItemTrim(ss);
        }
        /// <summary>
        /// �÷��� "---->"�ָ����ַ�������
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
        /// �÷��� "====="�ָ����ַ�������
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForArray(string Str)
        {
            return SplitForArray(Str, false); 
        }
        /// <summary>
        /// �÷��� "-----"�ָ����ַ�������
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string[] SplitForArray_Child(string Str)
        {
            return SplitForArray_Child(Str, false); 
        }
        /// <summary>
        /// �÷��� "-----"�ָ����ַ�������
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
        /// �ָ�� ===
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
        /// �÷��� "====="�ָ����ַ�������
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
        /// �÷��� "|"�ָ�����ֵ����
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

        #region �ַ�ת��Ϊ��ֵ
        /// <summary>
        /// Ϊת��Ϊ��ֵ׼��
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
        /// �õ����ı�(�������κα�ǩ���ո񡢻���),���ڻ�ȡ���ƵȲ������ո���ַ���
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetLineTextStringNoSpace(string Str)
        {
            return GetLineTextString(Str).Replace(" ", "");
        }

        /// <summary>
        /// ���ı����ݹ��ˣ�ɾ�����ӡ�ɾ��Image ɾ��tag����ʽ
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
        /// �õ����ı�(�������κα�ǩ����β�ո񡢻���)
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string GetLineTextString(string Str)
        {
            return DelSpaceStr(GetTextString(Str));
        }              
        /// <summary>
        /// �õ��ı��ַ���(�������κα�ǩ)
        /// </summary>
        /// <returns></returns>
        public static string GetTextString(string Str)
        {
            string str = DelHtmlTag(Str);
            str = WipeScript(str);
            return str;
        }
        /// <summary>
        /// �õ�����Html������ַ���(������js����)
        /// </summary>
        /// <returns></returns>
        public static string GetCodeString(string HTML)
        {
            return WipeScript(HTML);
        }        
        /// <summary>
        /// html����ת��
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
        /// ɾ���س��ͻ��з���ǰ��Ŀո�
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
        /// ɾ��js�ű� ����ʽ����
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string DelScriptStlye(string html)
        {
            return WipeScript(Regex.Replace(html, @"<style[^>]*?>[\s\S]*?<[\s]*?/[\s]*?style>", ""));
        }
        /// <summary>
        /// ����JS�ű�          
        /// </summary>
        /// <param name="html">Ҫ���˵�����</param>
        /// <returns></returns>
        /// <applet> <body> <embed> <frame> <script><frameset> <html><iframe><img><style><layer><link> <ilayer><meta><object>
        /// ��Щ��ǩ�����ܽ���ע�빥��.�� HTML 4.01 �У����޳�ʹ�� applet Ԫ��        
        /// img�Ĺ�����ʽΪ <img src="javascript: alert('hello');">�ȣ�����ȥ�����е�javascript����       
        private static string WipeScript(string html)
        {
            if (string.IsNullOrEmpty(html)) return html;
            //����<script></script>���
            html = Regex.Replace(html, @"<script[\s\S]*?</script[^>]*>", "", RegexOptions.IgnoreCase);

            //����href=javascript: (<A>) ����
            html = Regex.Replace(html, @"\shref[\s]*=[^>]*script[\s]*:", "", RegexOptions.IgnoreCase);

            //���������ؼ���on...�¼�
            html = Regex.Replace(html, @"\son[mouse|click|key|blur|dblclick|focus][^>]*?=", "", RegexOptions.IgnoreCase);

            //����iframe
            html = Regex.Replace(html, @"<iframe[^>]*?>[\s]*?</iframe[^>]*?>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<iframe[^>]*?>", "", RegexOptions.IgnoreCase);

            //����frameset
            html = Regex.Replace(html, @"<frameset[^>]*?>[\s\S]*?</frameset[^>]*?>", "", RegexOptions.IgnoreCase);


            //��������javascript
            html = Regex.Replace(html, @"javascript:", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @":*expression", "", RegexOptions.IgnoreCase);

            //��������HTML˵����ǩ
            html = Regex.Replace(html, @"<!--[^>]*?-->", "", RegexOptions.IgnoreCase);

            //����src=javascript: (<img><embed>) ����
            html = Regex.Replace(html, @"\ssrc[\s]*=[^>]*?script[\s]*:", "", RegexOptions.IgnoreCase);

            //����applet
            html = Regex.Replace(html, @"<applet[\s\S]+</applet[\s]*>", "", RegexOptions.IgnoreCase);

            return html;
        }
        /// <summary>
        /// �ų�HTML��ǩ
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
        /// �ж��ַ���Ӣ�Ļ�����(���� 1 Ӣ�� 2 ���� 3 ���� 4)
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
        ///  �ж��ַ����Ƿ���ת��Ϊ����(int)
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
        /// תȫ�ǵĺ���(SBC case)  ȫ�ǿո�Ϊ12288,��ǿո�Ϊ32
        /// �����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ��:�����65248
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <returns>ȫ��</returns>        
        public static string ToSBC(string input)
        { //���תȫ��:
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
        /// ת��ǵĺ���(DBC case)
        /// ȫ�ǿո�Ϊ12288,��ǿո�Ϊ32
        /// �����ַ����(33-126)��ȫ��(65281-65374)�Ķ�Ӧ��ϵ��:�����65248
        /// </summary>
        /// <param name="input">�����ַ���</param>
        /// <returns>����ַ���</returns>
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
        #region �ؼ��ʹ���
        /// <summary>
        /// ����Ƿ�����Ƿ��ַ�(���ڽ�������)
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
