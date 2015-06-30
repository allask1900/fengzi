using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using PS.DAL.Collection;
using PS.Configuration;
using PS.DAL.Data.Search;
using PS.DAL.Data.Sys;
using PS.DAL.Entity.Search;
using PS.DAL.Entity.Common;
using PS.Common;

namespace PS.Spider
{
    public class SpiderPrice
    {  
        public StringBuilder LogSingleSite = new StringBuilder("");
        /// <summary>
        /// 更新价格成功数
        /// </summary>
        public static int UpdatePriceAllCount = 0;
        /// <summary>
        /// 更新价格失败次数
        /// </summary>
        public static int UpdatePriceErrorCount = 0;
        #region 构造函数 
        public FUpdatePrice(ESite eSite):base(eSite,"UpdatePrice")
        {  
            UpdatePriceAllCount = 0;
            UpdatePriceErrorCount = 0;            
            IsTest = false;
            eLogTemplet = new ELogTemplet();           
        }
        
        #endregion
 
        public void Start()
        {  
            if (eSiteConfig.UpdatePrice_Reg_GetPrice == string.Empty)
            {
                eLogTemplet.FatalError="UpdatePrice_Reg_GetPrice未配置";
                return;
            }
            List<EProductList> cProductList = DProductList.GetSiteListForUpdatePrice(eSite.SiteID, eSite.AnalysisCategoryID,IsTest);

            if (cProductList.Count > 0)
            {
                eLogTemplet.ProductCount=cProductList.Count;
                AnalysisTaskThead.Start();
            }
            else
            {
                 eLogTemplet.FatalError="FUpdatePrice.Start 无数据!";
                return;
            }
            IsSiteContinue = true;
            int analysisCount = 0;
            try
            {
                for (int i = 0; i < cProductList.Count && IsSiteContinue; i++)
                {
                    EProductList eProductList = (EProductList)cProductList[i];
                    eProductList.SiteName = eSite.SiteName;

                    cWork.AddWork(eProductList);
                    
                    if (ProductReadPageMaxError >= 300||ProductRegMaxError>=600)
                    {
                        LogHelper.WriteAnalyzingLog("连续错误数:ProductReadPageMaxError=" + ProductReadPageMaxError + " ProductRegMaxError" + ProductRegMaxError+"过多导致程序退出");
                        IsSiteContinue = false;
                        FBase.cWork.WorkEnd = true;
                    }
                    analysisCount++;
                    if (analysisCount >= 1000)
                    {
                        analysisCount = 0;
                        Thread.Sleep(60000);                        
                    }
                }
                while (true)
                {
                    if (cWork.Count == 0)
                    { 
                        int canWorkThead = 0;
                        int canworkIO = 0;
                        ThreadPool.GetAvailableThreads(out canWorkThead, out canworkIO);
                        if (cWork.Count == 0 && canWorkThead == 20)
                        {
                            Thread.Sleep(2000);
                            if (cWork.Count == 0 && canWorkThead == 20)
                            {
                                break;
                            }
                        }
                    }
                    Thread.Sleep(300000);
                }              
                cWork.WorkEnd = true;
                eSite = null;               
                LogHelper.WriteAnalyzingLog("站点分析完毕!");
                LogHelper.WriteAnalyzingLog("更新价格成功数:" + UpdatePriceAllCount);
                LogHelper.WriteAnalyzingLog("更新价格失败数:" + UpdatePriceErrorCount);

                     
            }
            catch (Exception ex)
            {
                LogHelper.WriteAnalyzingLog("分析出错位置Start" + ex.Message + " ;错误对象:" + ex.StackTrace);
            }
        } 
        /// <summary>
        /// 分析产品页面
        /// </summary>
        /// <returns>返回值(0 正常,无需更新数据 1 出错,但无需更新数据 2 出错,需更新数据 3 正常,更新数值价格  )</returns>
        public static int AnalysisProductPage(EProductList eProductList)
        {
            #region 截取分析内容
            string ItemPageCode = PageHelper.ReadUrl(eProductList.ResourceUrl, eSite.SiteEncoding, eSite.SiteName);
            if (ItemPageCode == "")
            {
                ProductReadPageMaxError++;
                if (ProductReadPageMaxError > 30)
                {
                    WriteLog(LogType.Site, "Error:分析产品页面 读取页面连续出错30次! ");
                }
                WriteLog(LogType.Site, "Error:分析产品页面(" + eProductList.ResourceUrl + ")读取页面出错! ");
                //2 出错,需更新数据 错误数加1
                return 2;
            }
            if (ProductReadPageMaxError > 0) ProductReadPageMaxError--;

            if (eSiteConfig.UpdatePrice_Str_GetCodeBeginAndEnd != string.Empty)
            {
                string[] UpdatePrice_Str_GetCodeBeginAndEnd = StringHelper.SplitForArray(eSiteConfig.UpdatePrice_Str_GetCodeBeginAndEnd, true);
                if (UpdatePrice_Str_GetCodeBeginAndEnd[0] != string.Empty)
                {
                    int BeginIndex = ItemPageCode.IndexOf(UpdatePrice_Str_GetCodeBeginAndEnd[0]);
                    if (BeginIndex > 0)
                    {
                        ItemPageCode = ItemPageCode.Substring(BeginIndex + UpdatePrice_Str_GetCodeBeginAndEnd[0].Length);
                    }
                    else
                    {
                        WriteLog(LogType.Site, "Error:分析产品页面(" + eProductList.ResourceUrl + ") 截取(" + UpdatePrice_Str_GetCodeBeginAndEnd[0] + ")出错!");
                        ProductRegMaxError++;
                        return 2;
                    }
                }
                if (UpdatePrice_Str_GetCodeBeginAndEnd.Length == 2)
                {
                    int EndIndex = ItemPageCode.IndexOf(UpdatePrice_Str_GetCodeBeginAndEnd[1]);
                    if (EndIndex > 0)
                    {
                        ItemPageCode = ItemPageCode.Substring(0, EndIndex + UpdatePrice_Str_GetCodeBeginAndEnd[1].Length + 1);
                    }
                    else
                    {
                        WriteLog(LogType.Site, "Error:分析产品页面(" + eProductList.ResourceUrl + ") 截取(" + UpdatePrice_Str_GetCodeBeginAndEnd[1] + ")出错!");
                        ProductRegMaxError++;
                        //2 出错,需更新数据 错误数加1
                        return 2;
                    }
                }                 
            } 
            if (ProductRegMaxError > 0) ProductRegMaxError--;
            #endregion


            string[] GetPrice =StringHelper.SplitForArray(eSiteConfig.UpdatePrice_Reg_GetPrice);
            int reslut = 0;
            for (int i = 0; i < GetPrice.Length; i++)
            {
                #region 读取价格链接
                if (GetPrice[i].IndexOf("<updatepriceurl>") > 0)
                {
                    Regex regPriceUrl = new Regex(GetPrice[i], RegexOptions.IgnoreCase);
                    Match mcPriceUrl = regPriceUrl.Match(ItemPageCode);
                    string PriceUrl = UrlProcess.ChangeUrl(eProductList.ResourceUrl, mcPriceUrl.Groups["updatepriceurl"].Value);
                    ItemPageCode = PageHelper.ReadUrl(PriceUrl, eSite.SiteEncoding, eSite.SiteName);
                }
                #endregion
                if (GetPrice[i].IndexOf("<updateprice>") > 0)
                {
                    Regex regPrice = new Regex(eSiteConfig.UpdatePrice_Reg_GetPrice, RegexOptions.IgnoreCase);
                    Match mcPrice = regPrice.Match(ItemPageCode);
                    if (UpdatePriceStyle == 0)
                    {
                        return reslut;
                    }
                    string Price =StringHelper.GetLineTextForNumber(mcPrice.Groups["updateprice"].Value);
                    if (string.IsNullOrEmpty(Price))
                    {
                        WriteLog(LogType.Site, "Error:分析产品页面(" + eProductList.ResourceUrl + ")无法提取产品价格!");
                        //2 出错,需更新数据 错误数加1
                        reslut = 2;
                        return reslut;
                    }
                    try
                    {
                        double p = Convert.ToDouble(Price);
                        if (p <= 0)
                        {
                            reslut = 2;
                            return reslut;
                        }
                        if (eProductList.Price != p)
                        {
                            eProductList.Price = p;
                            //价格提取正常 并且价格有变化 3 正常,更新数值价格 错误数可能需要减1
                            reslut = 3;
                        }
                        else
                        {
                            //价格提取正常 并且价格无变化 0 正常,无需更新数据 
                            reslut = 0;
                        }
                    }
                    catch
                    {
                        WriteLog(LogType.Site, "Error:分析产品页面(" + eProductList.ResourceUrl + ")产品价格转换出错!");
                        //2 出错,需更新数据 错误数加1
                        reslut = 2;
                    }
                }
            }
            return reslut;
        }
    }

    public class AnalysisPrice
    {
        EProductList ep; 
        public AnalysisPrice(EProductList ePList)
        {
            ep = ePList; 
        }

        public void Start(object ob)
        {
            if (ep == null) return;
            int result = FUpdatePrice.AnalysisProductPage(ep);
            if (result == 2)//2 出错,需更新数据
            {
                DProductList.UpdatePriceErrorCount(ep.OrderID, ep.CategoryID,ep.ErrorCount+1,ep.ProductID);
                FUpdatePrice.UpdatePriceErrorCount++;
                LogHelper.WriteAnalyzingLog(ep.OrderID.ToString() + " 更新价格出错:(" + ep.ResourceUrl + ") 出错数:(" + (ep.ErrorCount + 1) + ")", ep.SiteName);
            }
            else if (result == 3)//3 正常,更新数值价格
            {
                if(ep.ErrorCount>0) ep.ErrorCount--;
                DProductList.UpdatePrice(ep.OrderID, ep.ErrorCount, ep.ProductID, ep.Price,ep.UsedPrice, ep.CategoryID);
                LogHelper.WriteAnalyzingLog(ep.OrderID.ToString() + "成功更新价格:(" + ep.ResourceUrl + ")", ep.SiteName);
                FUpdatePrice.UpdatePriceAllCount++;
            }             
            else//无需更新数据
            {

            }
        }
    }
}