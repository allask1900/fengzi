using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZ.Spider.Configuration
{
    public class Configs
    {
        private static readonly ILog logger = LogManager.GetLogger("Configs"); 

        public readonly static string DatabaseBackupPath = GetPath("DatabaseBackupPath");
        
        public static string GetPath(string key)
        {
            string path=GetAppSetting(key).Trim();
            if (path != string.Empty && path[path.Length - 1] != '\\')
            {
                path=path+"\\";
            }
            return path;
        }
        

        

        #region 读取常规配置
        /// <summary>
        /// 读取常规配置
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string Key)
        {
            if (System.Configuration.ConfigurationManager.AppSettings[Key] != null)
            {
                return System.Configuration.ConfigurationManager.AppSettings[Key].Trim();
            }
            else
            {
                logger.Info("配置出错:未读取到(key="+Key+") 值");
                return "";
            }
        }
        

        public static int GetIntAppSetting(string key)
        {
            return GetIntAppSetting(key, 0);
        }
        public static int GetIntAppSetting(string key,int defaultValue)
        {
            string value = GetAppSetting(key);
            int result = defaultValue;
            int.TryParse(value, out result);
            return result;
        }

        public static bool GetBoolAppSetting(string key)
        {
            return GetBoolAppSetting(key, false);
        }
        public static bool GetBoolAppSetting(string key, bool defaultValue)
        {
            string value = GetAppSetting(key);
            bool result = defaultValue;
            bool.TryParse(value, out result);
            return result;
        }
        #endregion 
    }
    
}