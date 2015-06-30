using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FZ.Spider.DAL.Data.Sys;

namespace FZ.Spider.DAL.Data.Common
{
    public class DBConfig
    {
        public static string GetValue(int sysID, string key)
        {
            return DConfigItem.GetConfigValue(sysID, key);
        }
        /// <summary>
        /// 如果没配置则使用默认值
        /// </summary>
        /// <param name="sysID"></param>
        /// <param name="key"></param>
        /// <param name="defautValue"></param>
        /// <returns></returns>
        public static string GetValue(int sysID, string key,string defautValue)
        {
            string v=DConfigItem.GetConfigValue(sysID, key);
            if (v == null)
                return defautValue;
            return v;
        }
    }
}
