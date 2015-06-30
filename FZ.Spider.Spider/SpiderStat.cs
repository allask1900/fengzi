using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZ.Spider.Spider
{
    public class SpiderStat
    {
        /// <summary>
        /// 产品页(对比)连续读取错误数
        /// </summary>
        public int ProductReadPageMaxError = 0;
        /// <summary>
        /// 产品页(对比)连续正则匹配错误数
        /// </summary>
        public int ProductRegMaxError = 0;
        ///<summary>
        /// 列表页(对比)连续读取错误数
        /// </summary>
        public int ListReadPageMaxError = 0;
        /// <summary>
        /// 列表页(对比)连续正则匹配错误数
        /// </summary>
        public int ListRegMaxError = 0;
        /// <summary>
        /// 价格转换(对比)连续错误
        /// </summary>
        public int PriceMaxError = 0;
        /// <summary>
        /// 添加 1  
        /// </summary>
        /// <returns>是否到达最大对比连续错误数</returns>
        public bool AddPriceMaxError()
        {
            PriceMaxError++;
            if (PriceMaxError > 20) return true;
            return false;
        }
        /// <summary>
        /// 对比错误数减 1
        /// </summary>
        public void SubPriceMaxError()
        {
            if (PriceMaxError > 0)
                PriceMaxError--;
        }
        /// <summary>
        /// 读取列表页面数
        /// </summary>
        public int ReadListPageCount = 0;
        /// <summary>
        /// 读取内容页面数
        /// </summary>
        public int ReadContentPageCount = 0;
        /// <summary>
        /// 站点分类添加产品页面数
        /// </summary>
        public int SiteCategoryUrlAddProductCount = 0;


    }
}
