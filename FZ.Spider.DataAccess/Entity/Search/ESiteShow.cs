using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FZ.Spider.DAL.Entity.Search
{
    public class ESiteShow
    {
        

		#region attribute
		/// <summary>
		/// 
		/// </summary>
        public int SiteID
        {
            get;
            set;
        }
        public string SiteUrl
        {
            get;
            set;
        }
        public string SiteName
        {
            get;
            set;
        }
		/// <summary>
		/// 
		/// </summary>
        public string SiteDescription
        {
            get;
            set;
        }
		/// <summary>
		/// 
		/// </summary>
        public int CommentCount
        {
            get;
            set;
        }
		/// <summary>
		/// 
		/// </summary>
        public int ShowCount
        {
            get;
            set;
        }



        /// <summary>
        /// 总评分
        /// </summary>
        public int Overall
        {
            get;
            set;
        }
        /// <summary>
        /// 总评分次数
        /// </summary>
        public int OverallCount
        {
            get;
            set;
        }
        /// <summary>
        /// 价格评分
        /// </summary>
        public int Price
        {
            get;
            set;
        }
        /// <summary>
        /// 易用性评分 Ease of Purchase
        /// </summary>
        public int Purchase
        {
            get;
            set;
        }
        /// <summary>
        /// 客户服务评分Customer Service
        /// </summary>
        public int Service
        {
            get;
            set;
        }
        /// <summary>
        /// 支付评分
        /// </summary>
        public int Delivery
        {
            get;
            set;
        }
        /// <summary>
        /// 快递评分
        /// </summary>
        public int Shipping
        {
            get;
            set;
        }
		#endregion

		#region Construct
		public  ESiteShow()
		{
		}
        public  ESiteShow(IDataReader dr)
		{
			SiteID =(int)dr["SiteID"];
            SiteUrl = dr["SiteUrl"].ToString();
            SiteName = dr["SiteName"].ToString();
            SiteDescription = dr["SiteDescription"].ToString();
            CommentCount = (int)dr["CommentCount"];
            ShowCount = (int)dr["ShowCount"];

            Overall = (int)dr["Overall"];
            OverallCount = (int)dr["OverallCount"];
            Price = (int)dr["Price"];
            Purchase = (int)dr["Purchase"];
            Service = (int)dr["Service"];
            Delivery = (int)dr["Delivery"];
            Shipping = (int)dr["Shipping"];
        }
		#endregion
    }  
}