using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using FZ.Spider.Configuration;
using log4net;
namespace FZ.Spider.Common
{
    public class DownHelper
    { 
        private static ILog logger = LogManager.GetLogger(typeof(DownHelper).FullName);
        /// <summary>
        /// 下载文件(将二进制文件保存到磁盘)
        /// </summary>
        /// <param name="FileUrl"></param>
        /// <returns></returns>
        public static bool SaveBinaryFile(string FileUrl, int SiteID)
        {
            bool Result = false;
            try
            {
                byte[] buffer = new byte[1024];
                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

                string strFileSavePath = UrlHelper.GetSiteLogoPath(SiteID);
                Stream outStream = File.Create(strFileSavePath);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(FileUrl));
                request.Timeout = 15000;

                ////在公司临时使用==================
                //WebProxy myProxy = WebProxy.GetDefaultProxy();
                //myProxy.Credentials = CredentialCache.DefaultCredentials;
                //request.Proxy = myProxy;
                ////==================

                 
                WebResponse response = request.GetResponse();
                Stream inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);
                outStream.Flush();
                outStream.Close();
                inStream.Flush();
                inStream.Close();
                Result = true;
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message, ex);
                Result = false;
            }
            return Result;
        } 
        /// <summary>
        /// 下载文件(将二进制文件保存到磁盘)
        /// </summary>
        /// <param name="FileUrl"></param>
        /// <returns></returns>
        public static bool SaveBinaryFile(string FileUrl,int ProductID,int ImageType)
        {
            bool Result = false;
            try
            {

                byte[] buffer = new byte[1024];
                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

                string strFileSavePath = GetImagePath(ProductID,ImageType);
                Stream outStream = File.Create(strFileSavePath);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(FileUrl));

                ////在公司临时使用==================
                //WebProxy myProxy = WebProxy.GetDefaultProxy();
                //myProxy.Credentials = CredentialCache.DefaultCredentials;
                //request.Proxy = myProxy;
                ////==================

                request.Timeout = 15000;
                WebResponse response = request.GetResponse();
                Stream inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0)
                        outStream.Write(buffer, 0, l);
                }
                while (l > 0);
                outStream.Flush();
                outStream.Close();
                inStream.Flush();
                inStream.Close();
                Result = true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                Result= false;
            }
            return Result;
        } 
        static bool ThumbnailCallback() { return true; }
        #region 图片路径转换
        /// <summary>
        /// 得到图片存放路径(只有一张 大图 小图)
        /// </summary>
        /// <param name="ProductID">产品ID</param>
        /// <param name="ImageType">0: 只有一张; 1:小图; 2:大图</param>
        /// <returns></returns>
        public static string GetImagePath(int ProductID, int ImageType)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            first = ProductID / 1000000;
            second = (ProductID % 1000000) / 1000;
            third = ProductID % 1000;
            string path =CommonFun.CheckDirectory( Configs.ImagesPath+ first.ToString() + "\\" + second.ToString() + "\\" + third.ToString() + "\\"); 
            if (ImageType == 0)
            {
                path = path + ProductID + ".jpg";
            }
            else if (ImageType == 1)
            {
                path = path + ProductID + "_s.jpg";
            }
            else if (ImageType == 2)
            {
                path= path + ProductID + "_b.jpg";
            }
            return path;
        } 
        #endregion      
    }
}
