using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;
namespace FZ.Spider.Common
{
    public class UrlEncrypt
    {
        private const string m_key = "wwwallasknet";
        #region DES�����ַ���
        /// <summary> 
        /// �����ַ��� 
        /// ע��:��Կ����Ϊ��λ 
        /// </summary> 
        /// <param name="strText">�ַ���</param> 
        /// <param name="encryptKey">��Կ</param> 
        public static string DesEncrypt(string strText)
        {
            string decEncrypt = string.Empty;
            string encryptKey = m_key;
            byte[] byKey = null;
            byte[] IV = { 0x45, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                decEncrypt = Convert.ToBase64String(ms.ToArray());
                decEncrypt = System.Web.HttpUtility.UrlEncode(decEncrypt);
                return decEncrypt;
            }
            catch (System.Exception error)
            {
                throw new System.Exception(error.Message);
            }
        }
        #endregion

        #region DES�����ַ���
        /// <summary> 
        /// �����ַ��� 
        /// </summary> 
        /// <param name="inputString">�����ܵ��ַ���</param> 
        /// <param name="decryptKey">��Կ</param> 
        public static string DesDecrypt(string inputString)
        {
            string decryptKey = m_key;
            byte[] byKey = null;

            //byte[] IV = { 0x45, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] IV = { 0x45, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byte[] inputByteArray = new Byte[inputString.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(inputString);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = new System.Text.UTF8Encoding();
                return encoding.GetString(ms.ToArray());
            }
            catch (System.Exception error)
            {
                throw new System.Exception(error.Message);
            }
        }
        #endregion
    }
}
