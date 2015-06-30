using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PS.DAL.Entity.Search;

using PS.DAL.Collection;
using PS.Common;
using PS.DAL.Data.Search;
namespace PS.Spider
{
    /// <summary>
    /// 分析产品页面
    /// </summary>
    public class SpiderPriceStart
    {
        private object checkproduct = new object();
        /// <summary>
        /// 分析全部网站
        /// </summary>
        public static void AnalysisAllSite()
        {
            List<ECategory> cCategory= DCategory.GetList(0, 0); 
            for(int i=0;i<cCategory.Count;i++)
            {                 
                ECategory eCategory=(ECategory)cCategory[i];
                //排除暂不需要分析的分类
                if (Configuration.Configs.NoAnalysisCategoryIDS.IndexOf(eCategory.CategoryID.ToString()) == -1)
                {
                    AnalysisSite(DSite.GetListForUpdatePrice(eCategory.CategoryID));
                }
            }
        }
       
        /// <summary>
        /// 更新图书价格(Site=0 则全部 否则单个站点)
        /// </summary>
        public static void AnalysisBook(int SiteID)
        {
            List<ECategory> cCategory = DCategory.GetList(0, 0);
            for (int i = 0; i < cCategory.Count; i++)
            {
                ECategory eCategory = (ECategory)cCategory[i];
                 
                if (eCategory.CategoryID==12)
                {
                    LogHelper.WriteAnalyzingLog("********开始更新分类 图书 产品价格");
                    CSite cSite = DSite.GetListForUpdatePrice(eCategory.CategoryID);
                    if (SiteID == 0)
                    {
                        AnalysisSite(cSite);
                    }
                    else
                    {
                        for (int s = 0; s < cSite.Count; s++)
                        {                            
                            ESite eSite = (ESite)cSite[s];
                            if (eSite.SiteID == SiteID)
                            {
                                AnalysisSite(eSite);
                            }
                        }
                    }
                    LogHelper.WriteAnalyzingLog("********结束更新分类 图书 产品价格");
                }
            }
        }
        /// <summary>
        /// 更新非图书产品价格
        /// </summary>
        public static void AnalysisNoBook()
        {
            LogHelper.WriteAnalyzingLog("********开始更新非图书产品价格");
            List<ECategory> cCategory = DCategory.GetList(0, 0);
            for (int i = 0; i < cCategory.Count; i++)
            {
                ECategory eCategory = (ECategory)cCategory[i];
                if (eCategory.CategoryID != 12 && eCategory.CategoryID != 10)
                {
                    LogHelper.WriteAnalyzingLog("*******************开始更新分类 "+eCategory.CategoryName+" 产品价格");
                    AnalysisSite(DSite.GetListForUpdatePrice(eCategory.CategoryID));
                    LogHelper.WriteAnalyzingLog("*******************结束更新分类 " + eCategory.CategoryName + " 产品价格");
                }
            }
            LogHelper.WriteAnalyzingLog("********结束更新非图书产品价格");
        }
        /// <summary>
        /// 分析一类站点
        /// </summary>
        /// <param name="cSite"></param>
        public static void AnalysisSite(CSite cSite)
         {
             for (int i = 0; i < cSite.Count ; i++)
             {                 
                 ESite eSite=(ESite)cSite[i];
                 AnalysisSite(eSite);
             }
         }
         /// <summary>
         /// 分析单个站点
         /// </summary>
         /// <param name="eSite"></param>
         public static void AnalysisSite(ESite eSite)
         {
             LogHelper.WriteAnalyzingLog("...........开始分析站点:" + eSite.SiteName + "...............");
             FUpdatePrice fUpdatePrice = new PS.Spider.FUpdatePrice(eSite);
             fUpdatePrice.Start();       
             LogHelper.WriteAnalyzingLog("...........分析站点结束:" + eSite.SiteName + "...............");
         }
    }
}