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
    public class CProduct : ConcurrentDictionary<string, Resource>
    {
        private static ILog logger = LogManager.GetLogger(typeof(CProduct).FullName);
        /// <summary>
        /// 新增产品数
        /// </summary>
        public int NewItemCount=0;
       /// <summary>
       /// 更新价格数
       /// </summary>
        public int UpdateItemCount = 0;

        public int UpdateCategoryIDS = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eProduct"></param>
        /// <returns></returns>
        public bool Exist(EProduct eProduct)
        {

            //if (this.ContainsKey(eProduct.ResourceMD5Url))
            //{                
            //    Resource rs = this[eProduct.ResourceMD5Url];
            //    eProduct.ProductID = rs.ProductID;
            //    if (    (eProduct.Price > 0     && rs.Price != eProduct.Price) 
            //        ||  (eProduct.UsedPrice > 0 &&rs.UsedPrice!= eProduct.UsedPrice)
            //        ||  (eProduct.OrgPrice>0    &&rs.OrgPrice!=eProduct.OrgPrice)
            //        ||  (eProduct.RentPrice>0   &&rs.RentPrice!=eProduct.RentPrice))
            //    {
            //        DProductList.UpdatePrice(eProduct);
            //        UpdateItemCount++;
            //    }
            //    if (rs.CategoryIDS.IndexOf("|" + eProduct.CategoryID + "|") == -1)
            //    {
            //        rs.CategoryIDS = rs.CategoryIDS + eProduct.CategoryID + "|";
            //        DProductList.UpdateCategoryIDS(eProduct.ProductID, rs.CategoryIDS);
            //        UpdateCategoryIDS++;
            //    }
            //    return true;
            //}

            //由于服务器内存不足，采用数据库实时查找的方式验证产品是否已经抓取

            
            Resource rs =DProduct.GetResourceForSpider(eProduct.ResourceMD5Url);
            if (rs != null)
            {
                eProduct.ProductID = rs.ProductID;
                if ((eProduct.Price > 0 && rs.Price != eProduct.Price)
                    || (eProduct.UsedPrice > 0 && rs.UsedPrice != eProduct.UsedPrice)
                    || (eProduct.OrgPrice > 0 && rs.OrgPrice != eProduct.OrgPrice)
                    || (eProduct.RentPrice > 0 && rs.RentPrice != eProduct.RentPrice))
                {
                    DProductList.UpdatePrice(eProduct);
                    UpdateItemCount++;
                }
                if (rs.CategoryIDS.IndexOf("|" + eProduct.CategoryID + "|") == -1)
                {
                    rs.CategoryIDS = rs.CategoryIDS + eProduct.CategoryID + "|";
                    DProductList.UpdateCategoryIDS(eProduct.ProductID, rs.CategoryIDS);
                    UpdateCategoryIDS++;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 先检测是否已存在,不存在则添加
        /// </summary>
        /// <param name="eProduct"></param>
        public void AddProduct(EProduct eProduct)
        {
            try
            {
                //if (!this.ContainsKey(eProduct.ResourceMD5Url))
                //{
                //    Resource rs = new Resource();
                //    rs.CategoryIDS = "|"+eProduct.CategoryID.ToString()+"|";
                //    rs.Price = eProduct.Price;
                //    rs.UsedPrice = eProduct.UsedPrice;
                //    rs.OrgPrice = eProduct.OrgPrice;
                //    rs.RentPrice = eProduct.RentPrice;
                //    rs.ProductID = eProduct.ProductID;
                //    this.TryAdd(eProduct.ResourceMD5Url, rs);
                //    NewItemCount++;
                //    DProduct.Add(eProduct);
                //}
                if (DProduct.GetResourceForSpider(eProduct.ResourceMD5Url)==null)
                {
                    DProduct.Add(eProduct);
                    NewItemCount++; 
                }
                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
        } 
    }
    public class Resource
    {
        public int ProductID; 
        public string CategoryIDS;
        public float Price;
        public float UsedPrice;
        public float OrgPrice;
        public float RentPrice;
    }
    public class ProductIDManage
    {
        private static ProductIDManage PIM = null;
        private static readonly object pimLock = new object();

        public static ProductIDManage Instance()
        {
            if (PIM == null)
            {
                lock (pimLock)
                {
                    if (PIM == null)
                    {
                        PIM = new ProductIDManage();
                    }
                }
            }
            return PIM;
        }
        private object idLock = new object();
        /// <summary>
        /// 产品最大ID
        /// </summary>
        private int p_MaxProductID;
        private ProductIDManage()
        {
            p_MaxProductID = DProduct.GetMaxProductID();
        }
        /// <summary>
        /// PRODUCTID++
        /// </summary>
        /// <returns></returns>
        public int AddMaxProductID()
        {
            int id = 0;
            lock (idLock)
            {
                p_MaxProductID = p_MaxProductID + 1;
                id = p_MaxProductID;
            }
            return id;
        }
    }
}
