using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FZ.Spider.DAL.Entity.Common
{
    public class ELogTemplet
    {
        public int CategoryID = 0;
        public int SiteID = 0;
        public int PageCount = 0;
        public int ProductCount = 0;
        public string CategoryName = string.Empty;
        public string CategoryUrl = string.Empty;
        public string SiteName = string.Empty;
        public string SiteDomain = string.Empty;
        public StringBuilder ProductList = new StringBuilder("");
        /// <summary>
        /// 错误日志
        /// </summary>
        public StringBuilder ErrorLog = new StringBuilder("");
        /// <summary>
        /// 致命错误日志
        /// </summary>
        public string FatalError = string.Empty;
       
    } 
    public enum LogType
        {
            /// <summary>
            /// 产品商家列表XML(保存值为生成产品商家列表XML的最大产品ID)
            /// </summary>
            ProductListXml,
            /// <summary>
            /// 单个产品信息XML(保存值为生成单个产品信息XML的最大产品ID)
            /// </summary>
            ProductInfoXml,
            /// <summary>
            /// 单个产品属性XML(保存值为索引的最大产品ID)
            /// </summary>
            ProductAttribute,
            /// <summary>
            /// 生成产品索引(保存值为最后生成的最大产品ID)
            /// </summary>
            ProductIndex, Site, Status, System
        }
}
