using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using log4net;
using FZ.Spider.Common;
using FZ.Spider.Configuration;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.Logging;

namespace FZ.Spider.Spider
{
    public class SpiderListPage
    {
        private static ILog logger = LogManager.GetLogger("SpiderLog");
        /// <summary>
        /// 判断是否继续对现分类进行分析的阀值
        /// </summary>
        protected bool IsCategoryContinue = true;

        public SpiderSite spiderSite;

        private string CategoryUrl;
        private int CategoryID;
        public SpiderListPage(SpiderSite spSite, string categoryurl, int categoryid)
        {
            spiderSite=spSite;
            CategoryUrl = categoryurl;
            CategoryID = categoryid;
            Start();
        }
        public void Start()
        {
            int PageCount = 1;
            int beginpage = 1;
            
            if (spiderSite.eSiteConfig.List_Str_PostData == string.Empty)
            {
                //只有当pageno时beginpage才可能是0或者1 ,当itemno时分页号永远从1开始，但是beginitemno可以使1或者0
                if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.PageUrlStyle.ToLower() == "pageno")
                {
                    beginpage = spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.BeginNo;
                    PageCount = beginpage;
                }
                for (int p = beginpage; p <= PageCount && spiderSite.IsSiteContinue && IsCategoryContinue; p++)
                {
                    
                    #region 读取页面连续错误20次以上则暂停
                    if (spiderSite.spiderStat.ProductReadPageMaxError >= 20 || spiderSite.spiderStat.ListReadPageMaxError >= 20)
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "读取页面连续出现20次错误启动休眠60秒!"));
                        Thread.Sleep(60000);
                    }
                    #endregion

                    long begintime = DateTime.Now.Ticks;
                    string p_Str = p.ToString();
                    //列表页为项目数时
                    if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.PageUrlStyle.ToLower() == "itemno")
                    {
                        int beginitemno = spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.BeginNo;
                        int pageitemsize =spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.PageSize;
                        p_Str = (((p - 1) * pageitemsize) + beginitemno).ToString();
                    }
                    string[] RepalcePno = StringHelper.SplitForReplace(spiderSite.eSiteConfig.PageNo_Reg_RepalcePno);
                    if (!AnaysisListPage((Regex.Replace(CategoryUrl, RepalcePno[0], RepalcePno[1],RegexOptions.IgnoreCase,TimeSpan.FromSeconds(30))).Replace("[pno]", p_Str), CategoryID, p, beginpage, ref PageCount))
                    {
                        IsCategoryContinue = false;
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "分析列表( "+CategoryUrl+" ),页( "+p+" )出错，该分类停止!"));
                    }                    
                    logger.Debug(new LogMessage(spiderSite.eSite.SiteName, "分析列表分页(" + p + " )耗时("+Convert.ToInt32((DateTime.Now.Ticks-begintime)/10000)+")毫秒, " + CategoryUrl + "!"));
                    //如果需要分析内容页,则列表页分析要Sleep.
                    //if (spiderSite.eSiteConfig.Product_Bool_IsAnalysis)
                    //{
                    //    Thread.Sleep(5000);
                    //}
                    if (!spiderSite.IsTest)
                    {
                        while (spiderSite.spiderWork.smartThreadPool.WaitingCallbacks > 60)
                        {
                            logger.Info(new LogMessage(spiderSite.eSite.SiteName, "产品分析线程池中等待队列数超过60个,启动休眠10秒!"));
                            Thread.Sleep(10000);
                        }
                    }
                }
            }
            else
            {
                string PrevPostData = "";
                for (int p = 1; p <= PageCount && spiderSite.IsSiteContinue && IsCategoryContinue; p++)
                {
                    if (!StartPost(CategoryUrl, CategoryID, p, ref PageCount, ref PrevPostData))
                    {
                        IsCategoryContinue = false;
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"analysis list page error!this stop!"));
                    }
                    Thread.Sleep(200);
                }
            }
            logger.Info(new LogMessage(spiderSite.eSite.SiteName, "分析完成列表( " + CategoryUrl + " ),共有列表分页数("+PageCount+")"));
        }
        /// <summary>
        /// 分析列表页面
        /// </summary>
        /// <param name="PageUrl"></param>
        private bool AnaysisListPage(string ListPageUrl, int CategoryID, int CurrentPage, int BeginPageNumber, ref int PageCount)
        {
            #region 读取页面代码
            string ListPageCode = PageHelper.ReadUrl(ListPageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
            for (int i = 0; i < 4; i++)
            {
                if (ListPageCode == "")
                {
                    ListPageCode = PageHelper.ReadUrl(ListPageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                }
                else
                {
                    break;
                }
            }
            if (ListPageCode == "")
            {
                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "分析列表页(" + ListPageUrl + ") 读取页面错误!"));
                return true;
            }
            spiderSite.spiderStat.ReadListPageCount++;
            if (ListPageCode == "")
            {
                spiderSite.spiderStat.ListReadPageMaxError++;
                if (spiderSite.spiderStat.ListReadPageMaxError >= 20)
                {
                    logger.Error(new LogMessage(spiderSite.eSite.SiteName, "分析列表页(" + ListPageUrl + ") 读取页面对比连续错误20次,导致分析退出!"));
                    return false;
                }
                return true;
            }
            if (spiderSite.spiderStat.ListReadPageMaxError > 0) spiderSite.spiderStat.ListReadPageMaxError--;
            if (spiderSite.eSiteConfig.List_Str_GetCodeBeginAndEnd != string.Empty)
            {
                string[] List_Str_GetCodeBeginAndEnd = StringHelper.SplitForArray(spiderSite.eSiteConfig.List_Str_GetCodeBeginAndEnd, true);
                if (List_Str_GetCodeBeginAndEnd[0] != string.Empty)
                { 
                    int BeginIndex = ListPageCode.IndexOf(List_Str_GetCodeBeginAndEnd[0]);
                    if (BeginIndex > 0)
                    {
                        ListPageCode = ListPageCode.Substring(BeginIndex);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"分析列表页(" + ListPageUrl + ") SubString 出错 (" + List_Str_GetCodeBeginAndEnd[0] + ") 导致分析退出!"));
                        return false;
                    }
                }
                if (List_Str_GetCodeBeginAndEnd.Length == 2)
                {
                    int EndIndex = ListPageCode.IndexOf(List_Str_GetCodeBeginAndEnd[1]);
                    if (EndIndex > 0)
                    {
                        ListPageCode = ListPageCode.Substring(0, EndIndex + List_Str_GetCodeBeginAndEnd[1].Length + 1);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "分析列表页(" + ListPageUrl + ") SubString(" + List_Str_GetCodeBeginAndEnd[1] + ")导致分析退出!"));
                        return false;
                    }
                }
            }
            #endregion

            long listPageAnalysisTime = DateTime.Now.Ticks;
            #region 得到分类页数
            if (CurrentPage == BeginPageNumber)
            {
                PageCount = GetPageCount(ListPageCode, ListPageUrl, BeginPageNumber);
                #region 测试代码段
                if (spiderSite.IsTest)
                {
                    spiderSite.testInfo.PageCount= PageCount;                    
                    PageCount = Math.Min(PageCount, 2);
                }
                #endregion
            }
            #endregion
            #region 列表代码
            if (spiderSite.eSiteConfig.List_Reg_GetCode != string.Empty)
            { 
                string code=RegexHelper.GetMatchValue(ListPageCode,spiderSite.eSiteConfig.List_Reg_GetCode);
                if (code != string.Empty)
                {
                    ListPageCode = code;
                }
            }
            #endregion
            bool result= AnalysisListPageCode(ListPageUrl, ListPageCode, CurrentPage);             
            logger.Debug(new LogMessage(spiderSite.eSite.SiteName, "分析列表页面耗时" +( DateTime.Now.Ticks - listPageAnalysisTime)/10000+"毫秒"));
            return result;

        }
        private bool AnalysisListPageCode(string ListPageUrl, string itemPageCode)
        {
            MatchCollection mcItems = RegexHelper.MatchCollection(itemPageCode, spiderSite.eSiteConfig.List_Reg_GetProductList);
            if (mcItems.Count < 1)
            { 
                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "List_Reg_GetProductList(" + ListPageUrl + ") Can't Get Data!"));
                spiderSite.spiderStat.ListRegMaxError++;
                if (spiderSite.spiderStat.ListRegMaxError >= 30)
                {
                    logger.Error(new LogMessage(spiderSite.eSite.SiteName, "List_Reg_GetProductList(" + ListPageUrl + ")  Can't Get Data! and continuous errors>30 ! this category analysis stop!"));
                    return false;
                }
                return true;
            }
            if (spiderSite.spiderStat.ListRegMaxError > 0) spiderSite.spiderStat.ListRegMaxError--;
            foreach (Match Item in mcItems)
            {
                EProduct eProduct = new EProduct();
                //添加SpiderSite引用 
                eProduct.SpiderSite = spiderSite;
                eProduct.SiteID = spiderSite.eSite.SiteID;
                eProduct.CategoryID = CategoryID;
                eProduct.ResourceUrl = UrlProcess.ChangeUrl(ListPageUrl, StringHelper.GetLineTextStringNoSpace(Item.Groups["producturl"].Value));
                #region 更改产品Url
                if (spiderSite.eSiteConfig.List_Reg_ChangeProductUrl != string.Empty)
                {
                    string[] ChangeProductUrl = StringHelper.SplitForReplace(spiderSite.eSiteConfig.List_Reg_ChangeProductUrl,true);
                    if (ChangeProductUrl.Length == 2)
                    {
                        eProduct.ResourceUrl = Regex.Replace(eProduct.ResourceUrl, ChangeProductUrl[0], ChangeProductUrl[1],RegexOptions.IgnoreCase,TimeSpan.FromSeconds(30));
                    }
                    else
                    {
                        spiderSite.IsSiteContinue = false;
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "List_Reg_ChangeProductUrl setting error,this category analysis stop!"));
                        return false;
                    }
                }
                if (string.IsNullOrEmpty(eProduct.ResourceUrl))
                {
                    logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis list page (" + ListPageUrl + ") ResourceUrl  is null    "));
                    continue;
                }
                #endregion
                if (Item.Groups["fullname"].Value.Trim() != string.Empty)
                {
                    eProduct.FullName = StringHelper.GetLineTextString(Item.Groups["fullname"].Value);
                }
                if (Item.Groups["smallimage"].Value.Trim() != string.Empty)
                {
                    eProduct.SamllImageUrl = UrlProcess.ChangeUrl(ListPageUrl, StringHelper.GetLineTextStringNoSpace(Item.Groups["smallimage"].Value));
                }
                if (Item.Groups["model"].Value.Trim() != string.Empty)
                {
                    eProduct.Model = StringHelper.GetLineTextString(Item.Groups["model"].Value);
                }
                if (Item.Groups["isbn10"].Value.Trim() != string.Empty)
                {
                    eProduct.UPCOrISBN = StringHelper.GetLineTextStringNoSpace(Item.Groups["isbn10"].Value);
                }
                if (Item.Groups["upcorisbn"].Value.Trim() != string.Empty)
                {
                    if (eProduct.UPCOrISBN != string.Empty)
                        eProduct.UPCOrISBN = eProduct.UPCOrISBN + " ";
                    eProduct.UPCOrISBN = eProduct.UPCOrISBN+StringHelper.GetLineTextStringNoSpace(Item.Groups["upcorisbn"].Value);
                }
                if (Item.Groups["brandname"].Value.Trim() != string.Empty)
                {
                    eProduct.BrandName = StringHelper.GetLineTextString(Item.Groups["brandname"].Value);
                }
                if (Item.Groups["author"].Value.Trim() != string.Empty)
                {
                    eProduct.Author = StringHelper.GetLineTextString(Item.Groups["author"].Value);
                }

                string price = StringHelper.GetLineTextForNumber(Item.Groups["price"].Value);
                if (price != string.Empty)
                {
                    
                    try
                    {
                        eProduct.Price = Convert.ToSingle(price);
                    }
                    catch
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage (" + ListPageUrl + ") Convert Price(" + price + ") error,this category analysis stop!"));
                        if(spiderSite.spiderStat.AddPriceMaxError())
                            return false;
                    }
                }
                string usedprice = StringHelper.GetLineTextForNumber(Item.Groups["usedprice"].Value);
                if (usedprice != string.Empty)
                {
                    try
                    {
                        eProduct.UsedPrice = Convert.ToSingle(usedprice);
                    }
                    catch
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert usedprice(" + usedprice + ") error,this category analysis stop!"));
                        if (spiderSite.spiderStat.AddPriceMaxError())
                            return false;
                    }
                }
                if (Item.Groups["orgprice"].Value != string.Empty)
                {
                    string orgprice = StringHelper.GetLineTextForNumber(Item.Groups["orgprice"].Value);
                    try
                    {
                        eProduct.OrgPrice = Convert.ToSingle(orgprice);
                    }
                    catch
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert OrgPrice(" + orgprice + ") error,this category analysis stop!"));
                        if (spiderSite.spiderStat.AddPriceMaxError())
                            return false;
                    }
                }
                if (Item.Groups["rentprice"].Value != string.Empty)
                {
                    string rentprice = StringHelper.GetLineTextForNumber(Item.Groups["rentprice"].Value);
                    try
                    {
                        eProduct.RentPrice = Convert.ToSingle(rentprice);
                    }
                    catch
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert rentprice(" + rentprice + ") error,this category analysis stop!"));
                        if (spiderSite.spiderStat.AddPriceMaxError())
                            return false;
                    }
                }

                if (Item.Groups["scorecount"].Value != string.Empty)
                {
                    eProduct.ScoreCount = CommonFun.StrToInt(StringHelper.GetLineTextForNumber(Item.Groups["scorecount"].Value));                    
                }
                if (Item.Groups["score"].Value != string.Empty)
                {
                    try
                    {
                        eProduct.Score = Convert.ToInt32(Convert.ToSingle(StringHelper.GetLineTextForNumber(Item.Groups["score"].Value)) * eProduct.ScoreCount);
                    }
                    catch
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert score(" + Item.Groups["score"].Value + ") error!"));
                    }
                }
                if (spiderSite.PriceStyle == 1 && eProduct.Price == 0 && eProduct.UsedPrice == 0&&eProduct.RentPrice==0)
                {
                    logger.Info(new LogMessage(spiderSite.eSite.SiteName, "analysis list page (" + ListPageUrl + ") Can't get Price or UsedPrice!"));
                    continue;
                }
                spiderSite.spiderStat.SubPriceMaxError();

                eProduct.IsExist = spiderSite.tSiteResource.Exist(eProduct);
                //如果存在则  eProduct.ProductID=Resource.ProductID
                if (!eProduct.IsExist)
                {
                    eProduct.ProductID = SpiderStart.MaxProductID.AddMaxProductID();
                }
                if (eProduct.IsExist && spiderSite.PriceStyle == 2)//产品已存在，且产品价格需要在产品页抓取，将IsUpdatePrice设置为true
                {
                    eProduct.IsUpdatePrice = true;
                }
                if (spiderSite.IsTest || spiderSite.PriceStyle == 2 || !eProduct.IsExist ||(spiderSite.ProductCommentConfig.IsUpdateComment&&eProduct.IsExist&&DAL.Data.Search.DProductComment.MinOrdID(eProduct.ProductID)<2666348))
                {
                    spiderSite.spiderStat.SiteCategoryUrlAddProductCount++;
                    spiderSite.spiderWork.workQueue.Enqueue(eProduct);
                }
            }
            return true;
        }
        
        private bool AnalysisListPageCode(string ListPageUrl, string ListPageCode, int CurrentPage)
        {
            if (spiderSite.eSiteConfig.List_Reg_GetProductCode != string.Empty)
            {
                #region 列表页面先提取代码再正则匹配
                List<string> list = RegexHelper.GetMatchValues(ListPageCode, spiderSite.eSiteConfig.List_Reg_GetProductCode);
                foreach (string code in list)
                {
                    EProduct eProduct = new EProduct();
                    eProduct.SpiderSite = spiderSite;
                    eProduct.SiteID = spiderSite.eSite.SiteID;
                    eProduct.CategoryID = CategoryID;
                    string[] listRegs = spiderSite.eSiteConfig.List_Reg_GetProductList.Split(new string[] { "=====" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string reg in listRegs)
                    {
                        Match Item = RegexHelper.GetMatch(code, reg);
                        #region 产品URL
                        if (Item.Groups["producturl"].Value != string.Empty)
                        {
                            eProduct.ResourceUrl = UrlProcess.ChangeUrl(ListPageUrl, StringHelper.GetLineTextStringNoSpace(Item.Groups["producturl"].Value));
                            #region 更改产品Url
                            if (spiderSite.eSiteConfig.List_Reg_ChangeProductUrl != string.Empty)
                            {
                                string[] ChangeProductUrl = StringHelper.SplitForReplace(spiderSite.eSiteConfig.List_Reg_ChangeProductUrl, true);
                                if (ChangeProductUrl.Length == 2)
                                {
                                    eProduct.ResourceUrl = Regex.Replace(eProduct.ResourceUrl, ChangeProductUrl[0], ChangeProductUrl[1],RegexOptions.IgnoreCase,TimeSpan.FromSeconds(30));
                                }
                                else
                                {
                                    spiderSite.IsSiteContinue = false;
                                    logger.Error(new LogMessage(spiderSite.eSite.SiteName, "List_Reg_ChangeProductUrl setting error,this category analysis stop!"));
                                    return false;
                                }
                            }
                            #endregion
                        }
                        if (string.IsNullOrEmpty(eProduct.ResourceUrl))
                        {
                            logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis list page (" + ListPageUrl + ") ResourceUrl  is null    " + code));
                            continue;
                        }
                        #endregion
                        if (Item.Groups["fullname"].Value.Trim() != string.Empty)
                        {
                            eProduct.FullName = StringHelper.GetLineTextString(Item.Groups["fullname"].Value);
                        }
                        if (Item.Groups["smallimage"].Value.Trim() != string.Empty)
                        {
                            eProduct.SamllImageUrl = UrlProcess.ChangeUrl(ListPageUrl, StringHelper.GetLineTextStringNoSpace(Item.Groups["smallimage"].Value));
                        }
                        if (Item.Groups["model"].Value.Trim() != string.Empty)
                        {
                            eProduct.Model = StringHelper.GetLineTextString(Item.Groups["model"].Value);
                        }
                        if (Item.Groups["isbn10"].Value.Trim() != string.Empty)
                        {
                            eProduct.UPCOrISBN = StringHelper.GetLineTextStringNoSpace(Item.Groups["isbn10"].Value);
                        }
                        if (Item.Groups["upcorisbn"].Value.Trim() != string.Empty)
                        {
                            if (eProduct.UPCOrISBN != string.Empty)
                                eProduct.UPCOrISBN = eProduct.UPCOrISBN + " ";
                            eProduct.UPCOrISBN = eProduct.UPCOrISBN + StringHelper.GetLineTextStringNoSpace(Item.Groups["upcorisbn"].Value);
                        }
                        if (Item.Groups["brandname"].Value.Trim() != string.Empty)
                        {
                            eProduct.BrandName = StringHelper.GetLineTextString(Item.Groups["brandname"].Value);
                        }
                        if (Item.Groups["author"].Value.Trim() != string.Empty)
                        {
                            eProduct.Author = StringHelper.GetLineTextString(Item.Groups["author"].Value);
                        }
                        #region 价格提取
                        if (eProduct.Price==0&&Item.Groups["price"].Value != string.Empty)
                        {
                            string price = StringHelper.GetLineTextForNumber(Item.Groups["price"].Value);
                            try
                            {
                                eProduct.Price = Convert.ToSingle(price);
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage (" + ListPageUrl + ") Convert Price(" + price + ") error"));
                                if (spiderSite.spiderStat.AddPriceMaxError())
                                return false;
                            }
                        }

                        if (eProduct.UsedPrice==0&&Item.Groups["usedprice"].Value != string.Empty)
                        {
                            string usedprice = StringHelper.GetLineTextForNumber(Item.Groups["usedprice"].Value);
                            try
                            {
                                eProduct.UsedPrice = Convert.ToSingle(usedprice);
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert usedprice(" + usedprice + ") error"));
                                if (spiderSite.spiderStat.AddPriceMaxError())
                                return false;
                            }
                        }
                        if (eProduct.OrgPrice==0&&Item.Groups["orgprice"].Value != string.Empty)
                        {
                            string orgprice = StringHelper.GetLineTextForNumber(Item.Groups["orgprice"].Value);
                            try
                            {
                                eProduct.OrgPrice = Convert.ToSingle(orgprice);
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert OrgPrice(" + orgprice + ") error"));
                                if (spiderSite.spiderStat.AddPriceMaxError())
                                return false;
                            }
                        }
                        if (eProduct.RentPrice==0&&Item.Groups["rentprice"].Value != string.Empty)
                        {
                            string rentprice = StringHelper.GetLineTextForNumber(Item.Groups["rentprice"].Value);
                            try
                            {
                                eProduct.RentPrice = Convert.ToSingle(rentprice);
                            }
                            catch
                            {
                                logger.Error(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert rentprice(" + rentprice + ") error,this category analysis stop!"));
                                if (spiderSite.spiderStat.AddPriceMaxError())
                                return false;
                            }
                        }
                        #endregion

                        #region 产品评分
                        if (Item.Groups["scorecount"].Value != string.Empty)
                        {
                            eProduct.ScoreCount = CommonFun.StrToInt(StringHelper.GetLineTextForNumber(Item.Groups["scorecount"].Value));
                        }
                        if (Item.Groups["score"].Value != string.Empty)
                        {
                            try
                            {
                                eProduct.Score = Convert.ToInt32(Convert.ToSingle(StringHelper.GetLineTextForNumber(Item.Groups["score"].Value)) * eProduct.ScoreCount);
                            }
                            catch
                            {
                                logger.Warn(new LogMessage(spiderSite.eSite.SiteName, "analysis listpage(" + ListPageUrl + ")Convert score(" + Item.Groups["score"].Value + ") error!"));
                            }
                        }
                        #endregion
                    }
                   
                    if (spiderSite.PriceStyle == 1 && eProduct.Price == 0 && eProduct.UsedPrice == 0&&eProduct.RentPrice==0)
                    {

                        logger.Warn(new LogMessage(spiderSite.eSite.SiteName, "Can't get Price or UsedPrice or RentPrice ----- " + code + " ----- "+ListPageUrl));
                        continue;
                    }
                    spiderSite.spiderStat.SubPriceMaxError();
                    eProduct.IsExist= spiderSite.tSiteResource.Exist(eProduct);
                    //如果存在则  eProduct.ProductID=Resource.ProductID；不存在则ProductID生成器
                    if (!eProduct.IsExist)
                    {
                        eProduct.ProductID = SpiderStart.MaxProductID.AddMaxProductID();
                    }
                    if (eProduct.IsExist && spiderSite.PriceStyle == 2)//产品已存在，且产品价格需要在产品页抓取，将IsUpdatePrice设置为true
                    {
                        eProduct.IsUpdatePrice = true;
                    }
                    if (spiderSite.IsTest || spiderSite.PriceStyle == 2 || !eProduct.IsExist || (spiderSite.ProductCommentConfig.IsUpdateComment && eProduct.IsExist && DAL.Data.Search.DProductComment.MinOrdID(eProduct.ProductID) < 2666348))
                    {
                        spiderSite.spiderStat.SiteCategoryUrlAddProductCount++;
                        spiderSite.spiderWork.workQueue.Enqueue(eProduct);
                    }
                }
                #endregion
            }
            else
            {
                return AnalysisListPageCode(ListPageUrl, ListPageCode);
            } 
            return true;
        }
        /// <summary>
        /// Post分页
        /// </summary>
        /// <param name="ListPageUrl"></param>
        /// <param name="CategoryID"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="PageCount"></param>
        /// <param name="PrevPostData"></param>
        /// <returns></returns>
        private bool StartPost(string ListPageUrl, int CategoryID, int CurrentPage, ref int PageCount, ref string PrevPostData)
        {
            string ListPageCode = "";
            #region 读取页面代码
            if (CurrentPage == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (ListPageCode == "")
                    {
                        ListPageCode = PageHelper.ReadUrl(ListPageUrl, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                    }
                    else
                    {
                        break;
                    }
                }
                if (ListPageCode == "")
                {
                    return true;
                }
                PrevPostData = spiderSite.eSiteConfig.List_Str_PostData + GetPostData(spiderSite.eSite.SiteID, ListPageCode, ListPageUrl, CurrentPage);
            }
            else
            {
                if (spiderSite.eSiteConfig.List_Str_PostData != string.Empty && PrevPostData != string.Empty)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (ListPageCode == "")
                        {
                            ListPageCode = PageHelper.PostUrl(ListPageUrl, PrevPostData, spiderSite.eSite.SiteEncoding, spiderSite.eSite.SiteName);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (ListPageCode == "")
                    {
                        return true;
                    }
                    string pdata = GetPostData(spiderSite.eSite.SiteID, ListPageCode, ListPageUrl, CurrentPage);                    
                    PrevPostData = spiderSite.eSiteConfig.List_Str_PostData + pdata;
                }
            }
            spiderSite.spiderStat.ReadListPageCount++;
            if (spiderSite.eSiteConfig.List_Str_GetCodeBeginAndEnd != string.Empty)
            {
                string[] List_Str_GetCodeBeginAndEnd = StringHelper.SplitForArray(spiderSite.eSiteConfig.List_Str_GetCodeBeginAndEnd, true);
                if (List_Str_GetCodeBeginAndEnd[0] != string.Empty)
                {
                    int BeginIndex = ListPageCode.IndexOf(List_Str_GetCodeBeginAndEnd[0]);
                    if (BeginIndex > 0)
                    {
                        ListPageCode = ListPageCode.Substring(BeginIndex);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "Analysis List Page(" + ListPageUrl + ") SubString (" + List_Str_GetCodeBeginAndEnd[0] + ") Error,this category analysis stop!"));
                        return false;
                    }
                }
                if (List_Str_GetCodeBeginAndEnd.Length == 2)
                {
                    int EndIndex = ListPageCode.IndexOf(List_Str_GetCodeBeginAndEnd[1]);
                    if (EndIndex > 0)
                    {
                        ListPageCode = ListPageCode.Substring(0, EndIndex + List_Str_GetCodeBeginAndEnd[1].Length + 1);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName, "Analysis List Page(" + ListPageUrl + ") SubString(" + List_Str_GetCodeBeginAndEnd[1] + ")Error,this category analysis stop!"));
                        return false;
                    }
                }
            }
            #endregion

            #region 得到分类页数
            if (CurrentPage == 1)
            {
                PageCount = GetPageCount(ListPageCode, ListPageUrl, 1);
                #region 调试代码段
                if (spiderSite.IsTest)
                {
                    spiderSite.testInfo.PageCount = PageCount;              
                    PageCount = Math.Min(PageCount, 2);
                }
                #endregion
            }
            #endregion
            return AnalysisListPageCode(ListPageUrl, ListPageCode, CurrentPage);              
        }
        /// <summary>
        /// 提取页码
        /// </summary>
        /// <param name="PageCode"></param>
        /// <param name="ListPageUrl"></param>
        /// <returns></returns>
        private int GetPageCount(string PageCode, string ListPageUrl,int defaultPageCount)
        {
            int pcount = defaultPageCount;
            if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.Reg_GetMaxNoOrMaxCount == string.Empty 
                && spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.DefaultMaxPageNo>0)
                return spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.DefaultMaxPageNo;
            #region 提取内容
            if (spiderSite.eSiteConfig.PageNo_Str_GetCodeBeginAndEnd != string.Empty)
            {
                string[] PageNo_Str_GetCodeBeginAndEnd = StringHelper.SplitForArray(spiderSite.eSiteConfig.PageNo_Str_GetCodeBeginAndEnd, true);
                if (PageNo_Str_GetCodeBeginAndEnd[0] != string.Empty)
                {
                    int BeginIndex = PageCode.IndexOf(PageNo_Str_GetCodeBeginAndEnd[0]);
                    if (BeginIndex > 0)
                    {
                        PageCode = PageCode.Substring(BeginIndex);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"分析列表页面(" + ListPageUrl + ") 截取(" + PageNo_Str_GetCodeBeginAndEnd[0] + ")出错!"));
                        return pcount;
                    }
                }
                if (PageNo_Str_GetCodeBeginAndEnd.Length == 2)
                {
                    int EndIndex = PageCode.IndexOf(PageNo_Str_GetCodeBeginAndEnd[1]);
                    if (EndIndex > 0)
                    {
                        PageCode = PageCode.Substring(0, EndIndex + PageNo_Str_GetCodeBeginAndEnd[1].Length + 1);
                    }
                    else
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"分析列表页面(" + ListPageUrl + ") 截取(" + PageNo_Str_GetCodeBeginAndEnd[1] + ")出错!"));
                        return pcount;
                    }
                }
            }
            if (spiderSite.eSiteConfig.PageNo_Reg_GetCode != string.Empty)
            {
                string code = RegexHelper.GetMatchValue(PageCode, spiderSite.eSiteConfig.PageNo_Reg_GetCode);
                if (code != string.Empty)
                {
                    PageCode = code;
                }
            }
            #endregion
            if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.Reg_GetMaxNoOrMaxCount != string.Empty)
            {
                string PageCount = RegexHelper.GetMatchGroupValue(PageCode, spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.Reg_GetMaxNoOrMaxCount, "pagecount");
                PageCount = StringHelper.GetLineTextForNumber(PageCount);
                if (PageCount != null && PageCount != string.Empty)
                {
                    try
                    {
                        pcount = Convert.ToInt32(PageCount);                        
                    }
                    catch(Exception ex)
                    {
                        logger.Error(new LogMessage(spiderSite.eSite.SiteName,"Error:analysis list page(" + ListPageUrl + ") Convert PageCount Error!"),ex);
                        return pcount;
                    }
                }
                else
                {
                    logger.Error(new LogMessage(spiderSite.eSite.SiteName,"Error:analysis list page(" + ListPageUrl + ") Can't get PageCount!"));
                    return pcount;
                } 
            } 
            if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.MatchNumberType.ToLower() == "maxitemno")//pcount此时为分类总产品数
            { 
                int maxPageNo = pcount / spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.PageSize;
                if (pcount % spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.PageSize > 0) maxPageNo++;
                pcount = maxPageNo;
            }
            if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.PageUrlStyle.ToLower() == "pageno")
                pcount = pcount + spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.BeginNo - 1;
            if (spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.DefaultMaxPageNo > 0)
                pcount = Math.Min(pcount, spiderSite.eSiteConfig.PageNo_Reg_PagingConfig.DefaultMaxPageNo);
            return pcount;
        }
       
        public string GetPostData(int SiteID, string ListPageCode, string ListPageUrl, int CurrentPage)
        {
            string PostData = string.Empty;
            string GetPageViewRegStr_VIEWSTATE = @"id=""__VIEWSTATE""[\s]*?value=""(?<__VIEWSTATE>[^""]*?)""";

            Regex Reg_VIEWSTATE = new Regex(GetPageViewRegStr_VIEWSTATE);
            Match Mch_VIEWSTATE = Reg_VIEWSTATE.Match(ListPageCode);

            string __VIEWSTATE = Mch_VIEWSTATE.Groups["__VIEWSTATE"].Value;

            string GetPageViewRegStr_EVENTVALIDATION = @"id=""__EVENTVALIDATION""[\s]*?value=""(?<__EVENTVALIDATION>[^""]*?)""";

            Regex Reg_EVENTVALIDATION = new Regex(GetPageViewRegStr_EVENTVALIDATION);
            Match Mch_EVENTVALIDATION = Reg_EVENTVALIDATION.Match(ListPageCode);



            string __EVENTVALIDATION = Mch_EVENTVALIDATION.Groups["__EVENTVALIDATION"].Value;

            if (__VIEWSTATE == string.Empty)
            {
                logger.Error(new LogMessage(spiderSite.eSite.SiteName,"Error:分析列表页面(" + ListPageUrl + ") 提取页面状态出错!"));
                return string.Empty;
            }
            PostData = "&__VIEWSTATE=" + HttpUtility.UrlEncode(__VIEWSTATE);
            PostData = PostData + "&__EVENTVALIDATION=" + HttpUtility.UrlEncode(__EVENTVALIDATION);
            if (SiteID == 9)
            {
                int cp;
                if (CurrentPage < 11)
                {
                    cp = CurrentPage;

                }
                else
                {
                    cp = CurrentPage % 10 + 1;
                    if (CurrentPage % 10 == 0)
                    {
                        cp = 11;
                    }
                }
                string cpage = cp.ToString();

                if (cp < 10) cpage = "0" + cpage;
                PostData = "ctl00$ctl00$MainContent$Content$GridCategoryBestSelling$ctl14$ctl" + cpage + "&__EVENTARGUMENT=" + PostData;
            }
            return PostData;
        }
    }
}
