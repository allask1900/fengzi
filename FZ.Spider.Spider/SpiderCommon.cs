using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using log4net;
using FZ.Spider.Common;
using FZ.Spider.DAL.Collection;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Table;
using FZ.Spider.Logging;

namespace FZ.Spider.Spider
{
    public class SpiderCommon
    {
        private static ILog logger = LogManager.GetLogger("SpiderLog"); 
        private readonly static CBrand cBrand =  DBrand.GetCBrand();
        /// <summary>
        ///根据网站的配置得到价格样式(0 无;1 列表页获取 ;2 产品页获取<Price>或者<UsedPrice>)
        /// </summary>
        /// <param name="eSiteConfig"></param>
        /// <returns></returns>
        public static int GetPriceStyle(ESiteConfig eSiteConfig)
        {
            int style = 0;
            if (eSiteConfig.List_Reg_GetProductList.IndexOf("<price>") > 0)
            {
                style = 1;
            }
            else if (eSiteConfig.Product_Reg_GetProductParameter.IndexOf("<price>") > 0)
            {
                style = 2;
            }
            return style;
        }

        /// <summary>
        /// 检查品牌
        /// </summary>
        /// <param name="eProduct"></param>
        public static void CheckBrand(EProduct eProduct)
        {
            eProduct.BrandID = cBrand.CheckBrand(eProduct.BrandName);
        }

        public static void CheckModel(EProduct eProduct)
        {
            //图书 isbn
            if (CategoryHelper.GetCategoryIDByLevel(eProduct.CategoryID, 0) == "12")
            {
                eProduct.Model = eProduct.Model.Replace("-", "").Replace(":", "").Replace(" ", "");
                eProduct.UPCOrISBN = eProduct.UPCOrISBN.Replace("-", "").Replace(":", "").Replace(" ", "");
            } 
        } 
    }
}
