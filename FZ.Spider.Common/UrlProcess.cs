using System;
using System.Collections.Generic;
using System.Text;

namespace FZ.Spider.Common
{
    public class UrlProcess
    {
        /// <summary>
        /// 相对路径到绝对路径的转换
        /// </summary>
        /// <returns></returns>
        public static string ChangeUrl(string ParentUrl, string Url)
        {
            string ResultUrl = Url.Trim().Replace("&amp;", "&");
            if (Url.IndexOf("http://")!=0)
            {
                Uri baseUri = new Uri(ParentUrl);
                Uri absoluteUri = new Uri(baseUri,Url);
                ResultUrl = absoluteUri.ToString();                
            }
            return ResultUrl;
        }

        #region 转换Url中的中文
        /// <summary>
        /// 转换Url中的中文
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            return Encoding.UTF8.GetString(UrlEncodeBytesToBytesInternalNonAscii(System.Text.Encoding.GetEncoding(936).GetBytes(url)));
        }
        private static byte[] UrlEncodeBytesToBytesInternalNonAscii(byte[] bytes)
        {
            int cNonAscii = 0;
            int count = bytes.Length;
            int offset = 0; 
            for (int i = 0; i < count; i++)
            {
                if (IsNonAsciiByte(bytes[offset + i]))
                    cNonAscii++;
            } 
            byte[] expandedBytes = new byte[count + cNonAscii * 2];
            int pos = 0;

            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];

                if (IsNonAsciiByte(b))
                {
                    expandedBytes[pos++] = (byte)'%';
                    expandedBytes[pos++] = (byte)IntToHex((b >> 4) & 0xf);
                    expandedBytes[pos++] = (byte)IntToHex(b & 0x0f);
                }
                else
                {
                    expandedBytes[pos++] = b;
                }
            }

            return expandedBytes;
        }
        private static bool IsNonAsciiByte(byte b)
        {
            return (b >= 0x7F || b < 0x20);
        }
        private static char IntToHex(int n)
        {

            if (n <= 9)
                return (char)(n + (int)'0');
            else
                return (char)(n - 10 + (int)'a');
        }
        #endregion
    }
}
