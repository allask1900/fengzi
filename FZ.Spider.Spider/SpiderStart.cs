using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FZ.Spider.DAL.Table;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Collection;
using FZ.Spider.Configuration;
using FZ.Spider.Common;
using log4net;
using FZ.Spider.Logging;
using System.Threading;
using FZ.Spider.DAL.Data.Common;
using System.Collections.Concurrent;

namespace FZ.Spider.Spider
{
    public class SpiderStart
    {
        private static readonly ILog logger = LogManager.GetLogger("SpiderLog");
        public readonly static ProductIDManage MaxProductID = ProductIDManage.Instance();
        /// <summary>
        /// 正在分析中的站点(key= "siteid")
        /// </summary>
        public static ConcurrentDictionary<int, ESite> siteListAnalyzing = new ConcurrentDictionary<int, ESite>(1,1000);
        /// <summary>
        /// 待分析的站点分类
        /// </summary>
        private Queue<ESite> SiteAnalysisCategoryQueque = new Queue<ESite>();

        //每分钟检查一次是否有新任务
        System.Timers.Timer timer = new System.Timers.Timer(60000);


        public SpiderStart()
        { 
            
        }
        /// <summary>
        /// 分析全部网站(当FristCategoryID==0时，分析全部任务，否则分析一类)
        /// </summary>
        public void AllSite(int FristCategoryID)
        {
            logger.Info(new LogMessage("","开始运行爬虫程序"));
            if (FristCategoryID == 0)
            {
                List<ECategory> cCategory = DCategory.GetList(0, 0);
                //排除暂不需要分析的分类
                string NoAnalysisIDS =  DBConfig.GetValue(Configs.SysID.Search, "Search.Spider.NoAnalysisCategoryIDS","");
                //只分析那些分类
                string analysisIDS =    DBConfig.GetValue(Configs.SysID.Search, "Search.Spider.AnalysisCategoryIDS","");
                for (int i = 0; i < cCategory.Count; i++)
                {
                    ECategory eCategory = (ECategory)cCategory[i];                    
                    if (NoAnalysisIDS.IndexOf(eCategory.CategoryID.ToString()) == -1)
                    {                        
                        if(!string.IsNullOrEmpty(analysisIDS)) //如果设定只分析某些分类
                        {
                            if(analysisIDS.IndexOf(eCategory.CategoryID.ToString())>-1)
                                AnalysisOneCategory(DSite.GetListForAnalysis(eCategory.CategoryID), false);
                        }
                        else
                            AnalysisOneCategory(DSite.GetListForAnalysis(eCategory.CategoryID), false);
                    }
                }
            }
            else
            {
                AnalysisOneCategory(DSite.GetListForAnalysis(FristCategoryID), false);
            }
            logger.Info(new LogMessage("", "有" + SiteAnalysisCategoryQueque.Count + "个任务(站点/分类)等待分析。"));


            timer.Elapsed += new System.Timers.ElapsedEventHandler(CheckWorkQueue);
            timer.Enabled = true;

            while (SiteAnalysisCategoryQueque.Count > 0 || siteListAnalyzing.Count > 0)
            {
                if (SiteAnalysisCategoryQueque.Count > 0)
                {
                    if (siteListAnalyzing.Count() < Configuration.Configs.MaxSipderSite)
                    {
                        ESite esite = SiteAnalysisCategoryQueque.Dequeue();

                        if (!siteListAnalyzing.ContainsKey(esite.SiteID) && siteListAnalyzing.TryAdd(esite.SiteID, esite))
                        {
                            //为每个站点开启一个线程
                            AddRuningQueue(esite);
                            new Thread(new ThreadStart(new SpiderSite(esite, false).Start)).Start();                            
                            logger.Info(new LogMessage(esite.SiteName, "将站点(" + esite.SiteName + "),分类(" + esite.AnalysisCategoryID + ")添加到Spider队列"));
                        }
                        else
                        {
                            //移到结尾处
                            SiteAnalysisCategoryQueque.Enqueue(esite);
                            logger.Info(new LogMessage(esite.SiteName, "同一个站点同时只能启动一个线程池(即一个分类)!"));
                        }
                    }
                    Thread.Sleep(300000);
                    logger.Info(new LogMessage("", "任务等待队列中有" + SiteAnalysisCategoryQueque.Count + "个任务(站点/分类)等待分析。"));
                }
                else
                {
                    logger.Info(new LogMessage("", "已无等待任务，进行中的任务有" + siteListAnalyzing.Count + "个(每个站点同时只能有一个分类)"));
                    Thread.Sleep(300000);
                }
            }
            
            logger.Info(new LogMessage("", "所有站点都已经添加到Spider队列"));

            timer.Close();
            timer.Dispose();
            
        }
        /// <summary>
        /// 分析一类站点
        /// </summary>
        /// <param name="cSite"></param>
        public void AnalysisOneCategory(CSite cSite, bool IsOnlyUpdatePrice)
        {
            foreach (ESite eSite in cSite)
            {
                //不分析的站点ID
                string NoAnalysisSiteIDS = ","+DBConfig.GetValue(Configs.SysID.Search, "Search.Spider.NoAnalysisSiteIDS", "")+",";
                if (NoAnalysisSiteIDS.IndexOf("," + eSite.SiteID.ToString() + ",") == -1)
                { 
                    SiteAnalysisCategoryQueque.Enqueue(eSite);
                    AddWorkQueue(eSite);
                }
            }
        }
        /// <summary>
        /// 添加到Work队列
        /// </summary>
        /// <param name="eSite"></param>
        private void AddWorkQueue(ESite eSite)
        {
            if(!DSpiderWorkQueue.Exists(eSite.SiteID, eSite.AnalysisCategoryID))
                DSpiderWorkQueue.Add(eSite.SiteID, eSite.AnalysisCategoryID);            
             DSpiderWorkQueue.AddWorkQueue(eSite.SiteID, eSite.AnalysisCategoryID);
        }

        private void AddRuningQueue(ESite eSite)
        {
            DSpiderWorkQueue.AddRuningQueue(eSite.SiteID, eSite.AnalysisCategoryID);
        }
         
        private void CheckWorkQueue(object source, System.Timers.ElapsedEventArgs e)
        {
            AnalysisOneCategory(DSite.GetListForAnalysisBySpiderWorkQueue(), false);
        }
    } 
}
