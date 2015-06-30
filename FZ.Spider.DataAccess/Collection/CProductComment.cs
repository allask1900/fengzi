using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using log4net;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Entity.Search;

namespace FZ.Spider.DAL.Collection
{
    public class CProductComment : ConcurrentDictionary<string,bool>
    {
        private static ILog logger = LogManager.GetLogger(typeof(CProductComment).FullName);
        /// <summary>
        /// 当前集合下的Min OrdID 主要用来区分ordid<=2666348 的产品评论
        /// </summary>
        public int MinOrdID = 2666349;
        /// <summary>
        /// 新增评论数
        /// </summary>
        public int NewCommentCount=0;
        public bool Exist(EProductComment eProductComment)
        {
            return this.ContainsKey(eProductComment.Key);
        }
        /// <summary>
        /// 先检测是否已存在,不存在则添加
        /// </summary>
        /// <param name="eProduct"></param>
        public void AddProductComment(EProductComment eProductComment)
        {
            try
            { 
                if (!this.ContainsKey(eProductComment.Key))
                {
                    this.TryAdd(eProductComment.Key, true);
                    NewCommentCount++;
                    DProductComment.Add(eProductComment);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// 先检测是否已存在,不存在则添加
        /// </summary>
        /// <param name="eProduct"></param>
        public void AddProductComment(EProduct eProduct)
        {
            try
            {
                if (eProduct.CommentList.Count == 0) return;
                //由于服务器内存不足，检查评论是否已抓取，每个产品先从数据库中提取，再进行更新操作
                CProductComment cProductComment = DProductComment.GetProductComment(eProduct.ProductID);
                //临时code 已存在的这次更新到数据，先删除原有评论
                //////////////////////////////////////////////////////////////
                if (eProduct.IsExist && cProductComment.Count > 0 && cProductComment.MinOrdID < 2666348)
                {
                    DProductComment.DeleteProductComment(eProduct.ProductID);
                    logger.Info("删除产品(" + eProduct.ProductID + ")评论(" + cProductComment.Count + "),添加评论(" + eProduct.CommentList.Count+ ")");
                    cProductComment.Clear();
                }
                bool hasAdd = false;
                //////////////////////////////////////////////////////////////
                foreach (EProductComment epc in eProduct.CommentList)
                {
                    if (!cProductComment.ContainsKey(epc.Key))
                    {
                        NewCommentCount++;
                        DProductComment.Add(epc);
                        hasAdd = true;
                    }
                }
                cProductComment.Clear();
                cProductComment = null;

                if (hasAdd)
                    DProduct.UpdateStatus(eProduct.ProductID, false, eProduct.CategoryID);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
        } 

    }     
}
