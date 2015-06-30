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
using Amib.Threading;
using FZ.Spider.DAL.Entity.Common;
using log4net;
using FZ.Spider.Logging;
using System.Threading;
using System.Collections.Concurrent;

namespace FZ.Spider.Spider
{
    /// <summary>
    /// 每个站点包含一个SpiderWork对象
    /// </summary>
    public class SpiderWork
    {
        private static ILog logger = LogManager.GetLogger("SpiderLog");
        /// <summary>
        /// 工作管理列表
        /// </summary>
        public ConcurrentQueue<EProduct> workQueue = new ConcurrentQueue<EProduct>();
        /// <summary>
        /// 站点SpiderWork是否结束
        /// </summary>
        public bool spiderWorkIsEnd = false;
        /// <summary>
        /// 工作线程池
        /// </summary>
        public SmartThreadPool smartThreadPool;
        
        /// <summary>
        /// isTest=true 时用于测试只是为了实例workQueue
        /// </summary>
        /// <param name="isTest"></param>
        public SpiderWork(bool isTest)
        {
            if (!isTest)
            {
                smartThreadPool = new SmartThreadPool(300000,5);
                //为线程池分配工作任务的线程
                new Thread(new ThreadStart(this.DoWork)).Start();
            }
        }
        private void DoWork()
        { 
            bool log = true;
            while (true)
            {
                if (DateTime.Now.Minute==59&&DateTime.Now.Second == 59)
                {
                    if (log)
                    {
                        log = false;
                        logger.Info(new LogMessage("", smartThreadPool.ToString()));
                    }
                }
                else
                    log = true;


                if (workQueue.Count > 0)
                {
                    EProduct eProduct;
                    bool get=workQueue.TryDequeue(out eProduct);
                    if (get&&eProduct != null)
                    {
                        SpiderContentPage scp = new SpiderContentPage(eProduct);
                        smartThreadPool.QueueWorkItem(new WorkItemInfo() { Timeout = 5*1000 }, new WorkItemCallback(scp.Start));                        
                    }
                }
                else
                {
                    if (spiderWorkIsEnd)
                    {
                        logger.Info(new LogMessage("", "工作队列完成!"));
                        break;
                    }
                }
                Thread.Sleep(100);
            }
            smartThreadPool.WaitForIdle();
            smartThreadPool.Shutdown();
        }
    }
}
