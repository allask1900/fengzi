using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using FZ.Spider.Logging;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Table;
using FZ.Spider.Common;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Concurrent;
using FZ.Spider.DAL.Collection; 

namespace FZ.Spider.Spider
{
    public class SpiderSite
    {
        private static ILog logger = LogManager.GetLogger("SpiderLog"); 
        /// <summary>
        /// 判断是否继续对整个站点进行分析的阀值
        /// </summary>
        public bool IsSiteContinue = true;
        /// <summary>
        /// 网站价格显示方式  (0 无; 1 列表页 数值; 2 产品页 数值)
        /// </summary>
        public int PriceStyle = 0;
        public ESite eSite;
        public ESiteConfig eSiteConfig;
        /// <summary>
        /// 是否为测试
        /// </summary>
        public bool IsTest = false;
        /// <summary>
        /// 爬虫测试的信息K/V对
        /// </summary>
        public TestInfo testInfo = new TestInfo();
        /// <summary>
        /// 已存在的产品
        /// </summary>
        public CProduct tSiteResource=new CProduct();
        /// <summary>
        /// 一抓取的产品评论
        /// </summary>
        public CProductComment cProductComment=new CProductComment();
        public SpiderWork spiderWork;
        public SpiderStat spiderStat;
        public List<ItemConfig> ProductParameter;
        public CommentConfig ProductCommentConfig;
        //每5分钟更新一次配置
        System.Timers.Timer timer = new System.Timers.Timer(300000);

        public SpiderSite(ESite esite, bool isTest)
        { 
            IsTest = isTest;
            Init(esite);
        }
        public SpiderSite(ESite esite)
        {
            Init(esite);           
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="es"></param>
        private void Init(ESite es)
        {           
            eSite = es;
            InitSiteConfig();           
            spiderStat = new SpiderStat();
            if (!IsTest)
            {
                //tSiteResource = DProduct.GetProductListForSpider(eSite.SiteID, eSite.AnalysisCategoryID);
                //无需对评论做是否已经抓取的验证，
                //如果开放该验证(多次抓取评论时)，
                //还需要更改CProductComment的AddProductComment方法被注释的部分。
                //cProductComment = DProductComment.GetSiteProductComment(eSite.SiteID);
            }
            spiderWork = new SpiderWork(IsTest);
            
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;
          
        }
        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            eSite = DSite.GetSiteForAnalysis(eSite.SiteID,eSite.AnalysisCategoryID);
            InitSiteConfig();
        }
        private void InitSiteConfig()
        {
            try
            {
                PageHelper.SetSpiderFreq(eSite.SiteName, eSite.SpiderReadCount, eSite.SpiderSleepTime);
                ESiteConfig updateESiteConfig = DSiteConfig.GetEntity(eSite.SiteID, eSite.AnalysisCategoryID);
                if (eSiteConfig != null && updateESiteConfig != null && updateESiteConfig.LastChangeTime != eSiteConfig.LastChangeTime)
                {
                    StringBuilder info = new StringBuilder("");
                    info.Append("站点爬虫配置更新:(siteName=");
                    info.Append(eSite.SiteName);
                    info.Append(",siteID=");
                    info.Append(eSite.SiteID);
                    info.Append(",AnalysisCategoryID=");
                    info.Append(eSite.AnalysisCategoryID);
                    info.Append(",OrdID=");
                    info.Append(updateESiteConfig.OrdID);
                    info.Append(",LastChangeTime=");
                    info.Append(updateESiteConfig.LastChangeTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    info.Append(")");
                    logger.Info(new LogMessage(eSite.SiteName,info.ToString()));
                }
                eSiteConfig = updateESiteConfig;
                ProductParameter = ItemConfig.ItemConfigParser(eSite.SiteName, eSiteConfig.Product_Reg_GetProductParameter);
                ProductCommentConfig = CommentConfig.CommentConfigParser(eSite.SiteName, eSiteConfig.Comment_Reg_GetProductCommentParameter);
                PriceStyle = SpiderCommon.GetPriceStyle(eSiteConfig);
            }
            catch (Exception ex)
            {
                logger.Error(new LogMessage(eSite.SiteName,ex.Message));
            }
        }
        /// <summary>
        /// 开始分析
        /// </summary>
        public void Start()
        {
            List<ESiteCategory> cSiteCategory =  DSiteCategory.GetSiteCategory(eSite.SiteID, eSite.AnalysisCategoryID);
            if (PriceStyle == 0)
            {
                logger.Error(new LogMessage(eSite.SiteName, "关于价格的正则表达式设置错误"));
                return;
            }
            if (cSiteCategory.Count == 0)
            {
                logger.Error(new LogMessage(eSite.SiteName, "未提取到模块!"));  
            }            
            #region 只配置入口需要提取分类Url的站点
            if (eSiteConfig.GetList_Reg_GetCategoryUrl != string.Empty)
            {
                List<ESiteCategory> realSiteCategory = new List<ESiteCategory>();
                for (int i = 0; i < cSiteCategory.Count; i++)
                {
                    if (IsTest && i > 1)
                        break;
                    #region 读取页面代码
                    ESiteCategory e = (ESiteCategory)cSiteCategory[i];
                    string ListPageCode = PageHelper.ReadUrl(e.SCUrl, eSite.SiteEncoding, eSite.SiteName);
                    if (ListPageCode == string.Empty)
                    {
                        logger.Error(new LogMessage(eSite.SiteName, "分析列表页面(" + e.SCUrl + ") 读取页面出错出错!"));
                        return;
                    }
                    if (eSiteConfig.GetList_Str_GetCodeBeginAndEnd != string.Empty)
                    {
                        string[] GetList_Str_GetCodeBeginAndEnd = StringHelper.SplitForArray(eSiteConfig.GetList_Str_GetCodeBeginAndEnd, true);
                        if (GetList_Str_GetCodeBeginAndEnd[0] != string.Empty)
                        {
                            int BeginIndex = ListPageCode.IndexOf(GetList_Str_GetCodeBeginAndEnd[0]);
                            if (BeginIndex > 0)
                            {
                                ListPageCode = ListPageCode.Substring(BeginIndex);
                            }
                            else
                            {
                                logger.Error(new LogMessage(eSite.SiteName, "分析列表页面(" + e.SCUrl + ") 截取(" + GetList_Str_GetCodeBeginAndEnd[0] + ")出错!"));
                                return;
                            }
                        }
                        if (GetList_Str_GetCodeBeginAndEnd.Length == 2)
                        {
                            int EndIndex = ListPageCode.IndexOf(GetList_Str_GetCodeBeginAndEnd[1]);
                            if (EndIndex > 0)
                            {
                                ListPageCode = ListPageCode.Substring(0, EndIndex + GetList_Str_GetCodeBeginAndEnd[1].Length + 1);
                            }
                            else
                            {
                                logger.Error(new LogMessage(eSite.SiteName, "分析列表页面(" + e.SCUrl + ") 截取(" + GetList_Str_GetCodeBeginAndEnd[1] + ")出错!"));
                                return;
                            }
                        }
                    }
                    if (eSiteConfig.GetList_Reg_GetCode != string.Empty)
                    {                      
                        string code = RegexHelper.GetMatchValue(ListPageCode, eSiteConfig.GetList_Reg_GetCode);
                        if (code != string.Empty)
                            ListPageCode = code;
                    }
                    #endregion                     
                    MatchCollection mcCategoryUrl = RegexHelper.MatchCollection(ListPageCode, eSiteConfig.GetList_Reg_GetCategoryUrl);
                    foreach (Match m in mcCategoryUrl)
                    {
                        ESiteCategory eSiteCategory = new ESiteCategory();
                        eSiteCategory.SCID = realSiteCategory.Count + 1;
                        eSiteCategory.CategoryID = eSite.AnalysisCategoryID;
                        eSiteCategory.SiteID = eSite.SiteID;
                        eSiteCategory.SCUrl = Common.UrlProcess.ChangeUrl(e.SCUrl, StringHelper.GetLineTextStringNoSpace(m.Groups["categoryurl"].Value));
                        realSiteCategory.Add(eSiteCategory);
                    }
                }
                logger.Error(new LogMessage(eSite.SiteName, "提示:共有分类列表页面" + realSiteCategory.Count + "项!"));
                cSiteCategory = realSiteCategory;
            }
            #endregion
            try
            {
                string errorInfo = string.Empty;
                try
                {
                    for (int sm = 0; sm < cSiteCategory.Count && IsSiteContinue; sm++)
                    {
                        ESiteCategory eSiteCategory = (ESiteCategory)cSiteCategory[sm];

                        spiderStat.SiteCategoryUrlAddProductCount = 0;
                        if (IsTest)
                        {
                            if (sm == 0)
                                testInfo.CategoryUrl = eSiteCategory.SCUrl;
                            else //测试时只需要分析第一个入口即可
                                break;
                        }
                        new SpiderListPage(this, eSiteCategory.SCUrl, eSiteCategory.CategoryID);
                        logger.Info(new LogMessage(eSite.SiteName, "站点入口 (" + eSiteCategory.SCName + ":" + eSiteCategory.CategoryID + ":" + eSiteCategory.SCUrl + ")分析完成,分析产品页面数:" + spiderStat.SiteCategoryUrlAddProductCount + ";完成价格更新" + tSiteResource.UpdateItemCount + "个"));
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(new LogMessage(eSite.SiteName, ex.Message), ex);
                    errorInfo = ex.Message;

                }
                #region 主线程等待分析完成
                //主线程分析完List后，等待产品分析队列的完成。
                if (!IsTest)
                {
                    while (true)
                    {
                        int count = spiderWork.workQueue.Count;
                        if (count == 0)
                        {
                            break;
                        }
                        else
                        {
                            Thread.Sleep(60000);
                            logger.Info(new LogMessage(eSite.SiteName, ",站点(" + eSite.SiteID + ")列表分析完成,产品页分析队列中还有(" + count + ")个任务未完成"));
                        }
                    }
                }
                #endregion
                StringBuilder sb = new StringBuilder("");
                sb.Append("任务分析完成! 本次分析共新抓取");
                sb.Append(eSite.SiteName);
                sb.Append("网,分类(");
                sb.Append(eSite.AnalysisCategoryID);
                sb.Append("),产品");
                sb.Append(tSiteResource.NewItemCount);
                sb.Append("个;更新价格");
                sb.Append(tSiteResource.UpdateItemCount);
                sb.Append("个");
                if (errorInfo != string.Empty)
                {
                    sb.Append("由于异常导致任务失败,异常信息:");
                    sb.Append(errorInfo);
                }
                logger.Info(new LogMessage(eSite.SiteName, sb.ToString()));
                DSpiderWorkQueue.AddCompleteQueue(eSite.SiteID, eSite.AnalysisCategoryID, sb.ToString());
            }
            catch (Exception ex)
            {
               logger.Error(new LogMessage(eSite.SiteName,ex.Message),ex);
            }
            //本网站的该分类分析完成,线程结束。
            if (!IsTest)
            {
                ESite es;
                spiderWork.spiderWorkIsEnd = true;
                if (SpiderStart.siteListAnalyzing.TryRemove(eSite.SiteID, out es))
                    logger.Info(new LogMessage(eSite.SiteName, "站点分析完成! 从SpiderStart.siteListAnalyzing中移除"));
                else
                    logger.Error(new LogMessage(eSite.SiteName, "(" + eSite.SiteID + "),从spiderStatus.SiteWorkCount中移除站点失败"));
            }
            //timer close 
            timer.Close();
            timer.Dispose();
            
        }  
    }
    public class TestInfo
    {
        public int PageCount = 0;
        public string CategoryUrl = string.Empty;
        public StringBuilder Log = new StringBuilder();
    }


    public class ItemConfig
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName = string.Empty;
        /// <summary>
        /// 获取代码正则表达式 <div[^>]*?id="result_[\d]*"[^>]*?>((?<Nested><div[^>]*>)|</div>(?<-Nested>)|.*?)*</div>
        /// </summary>
        public RegexStrings Reg_GetCode = new RegexStrings();
        /// <summary>
        /// 提取字段值正则表达式
        /// </summary>
        public RegexStrings Reg_GetValue = new RegexStrings();
        public string Reg_GetValue_Url = string.Empty;
        public RegexStrings Reg_GetValue_Replace = new RegexStrings();

        public ItemConfig()
        {

        }

        public static List<ItemConfig> ItemConfigParser(String SiteName, String ProductParameter)
        {
            List<ItemConfig> ics = new List<ItemConfig>();
            string[] Parameters = ProductParameter.Trim().Split(new string[] { "====================" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string para in Parameters)
            {
                ItemConfig itemconfig = new ItemConfig();
                string[] items = para.Trim().Split(new string[] { "*****" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in items)
                {
                    string[] kv = item.Trim().Split(new string[] { "====" }, StringSplitOptions.RemoveEmptyEntries);
                    if (kv.Length == 2)
                    {
                        string key = kv[0].Trim().ToLower();
                        string value = kv[1].Trim();

                        switch (key)
                        {
                            case "fieldname":
                                itemconfig.FieldName = value;
                                break;
                            case "getcode":
                                itemconfig.Reg_GetCode = RegexStrings.Parser(value);
                                break;
                            case "getvaule_url":
                                itemconfig.Reg_GetValue_Url = value;
                                break;
                            case "getvalue":
                                itemconfig.Reg_GetValue = RegexStrings.Parser(value);
                                break;
                            case "getvalue_replace":
                                itemconfig.Reg_GetValue_Replace = RegexStrings.Parser(value);
                                break;
                            default:
                                break;
                        }
                    }
                }
                ics.Add(itemconfig);
            }
            return ics;
        }
        public class RegexStrings : List<RegexString>
        {
            /// <summary>
            /// 两个正则之间的与或关系
            /// </summary>
            public bool IsAnd = false;

            public static RegexStrings Parser(string RegStr)
            {
                RegexStrings rs = new RegexStrings();
                if (RegStr.IndexOf("&&&&&") > -1)
                    rs.IsAnd = true;
                string[] regs = RegStr.Split(new string[] { "&&&&&", "|||||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string reg in regs)
                {
                    rs.Add(RegexString.Parser(reg.Trim()));
                }
                return rs;
            }
        }

        public class RegexString : List<string>
        {
            /// <summary>
            /// 两个正则之间的与或关系
            /// </summary>
            public bool IsAnd = false;

            public static RegexString Parser(string RegStr)
            {
                RegexString rs = new RegexString();
                if (RegStr.IndexOf("&&&&") > -1)
                    rs.IsAnd = true;
                string[] regs = RegStr.Split(new string[] { "&&&&", "||||" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string reg in regs)
                {
                    rs.Add(reg.Trim());
                }
                return rs;
            }
        }

    }
    /// <summary>
    /// 产品评论爬虫配置
    /// </summary>
    public class CommentConfig
    {
        /// <summary>
        /// 是否更新产品评论
        /// </summary>
        public bool IsUpdateComment = false;
        /// <summary>
        /// 产品评论入口URL
        /// </summary>
        public string Reg_GetCommentPageUrl = string.Empty;
        /// <summary>
        /// 产品评论页面begin or and  code
        /// </summary>
        public string Str_CommentPageBeginOrAndCode = string.Empty;
        /// <summary>
        /// 获取评论项的正则表达式
        /// </summary>
        public string Reg_GetCommentCode = string.Empty;
        /// <summary>
        /// 获取产品评论正则表达式
        /// </summary>
        public string Reg_GetComment = string.Empty;
        /// <summary>
        /// 得到最大分页表达式
        /// </summary>
        public string Reg_GetCommentMaxPageCount = string.Empty;
        /// <summary>
        /// 得到页面号替换正则
        /// </summary>
        public string Replace_CommentPageReplacePno = string.Empty;

        public static CommentConfig CommentConfigParser(string sitename, string Comment_Reg_GetProductCommentParameter)
        {
            CommentConfig commentConfig = new CommentConfig();
            string[] Parameters = Comment_Reg_GetProductCommentParameter.Trim().Split(new string[] { "====================" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string para in Parameters)
            { 
                string[] kv = para.Trim().Split(new string[] { "====" }, StringSplitOptions.RemoveEmptyEntries);
                if (kv.Length == 2)
                {
                    string key = kv[0].Trim().ToLower();
                    string value = kv[1].Trim();

                    switch (key)
                    {
                        case "isupdatecomment":
                            commentConfig.IsUpdateComment =Convert.ToBoolean(value);
                            break;
                        case "reg_getcommentpageurl":
                            commentConfig.Reg_GetCommentPageUrl = value;
                            break;
                        case "str_commentpagebeginorandcode":
                            commentConfig.Str_CommentPageBeginOrAndCode = value;
                            break;
                        case "reg_getcommentcode":
                            commentConfig.Reg_GetCommentCode = value;
                            break;
                        case "reg_getcomment":
                            commentConfig.Reg_GetComment = value;
                            break;
                        case "reg_getcommentmaxpagecount":
                            commentConfig.Reg_GetCommentMaxPageCount = value;
                            break;
                        case "replace_commentpagereplacepno":
                            commentConfig.Replace_CommentPageReplacePno = value;
                            break;
                        default:
                            break;
                    }
                } 
            }
            return commentConfig;
        }
    }
}
