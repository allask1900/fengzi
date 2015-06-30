using System;
namespace FZ.Spider.DAL.Entity.Search
{
	/// <summary>
	/// 实体类(ESiteComment)
	/// </summary>
   public class ESiteComment
   {
      public int OrdID
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
       /// <summary>
       /// 评论标题
       /// </summary>
       public string Title
       {
           get;
           set;
       }
   
      
		 
		public int SiteID
		{
			get ;
			set;
		}
        public string UserName
        {
            get ;
            set;
        }
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			get;
			set;
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP
		{
			get;
			set;
		}
		/// <summary>
		/// 评论内容
		/// </summary>
		public string Comment
		{
			get;
			set;
		}
       /// <summary>
       /// 反对数
       /// </summary>
        public int Against
        {
            get;
            set;
        }
       /// <summary>
       /// 支持数
       /// </summary>
        public int Support
        {
            get;
            set;
        }      
		/// <summary>
		/// 
		/// </summary>
		public DateTime CheckInTime
		{
			get;
			set;
		}
		 
		#region Construct
		public  ESiteComment()
		{
		}
		public ESiteComment(System.Data.SqlClient.SqlDataReader dr)
		{
            OrdID =(int)dr["OrdID"];
            SiteID =(int)dr["SiteID"]; 
            UserID =(int)dr["UserID"];
            IP = dr["IP"].ToString(); 
            UserName = dr["UserName"].ToString(); 
            Overall =(int)dr["Overall"];
            Price =(int)dr["Price"];
            Purchase =(int)dr["Purchase"];
            Service =(int)dr["Service"];
            Delivery =(int)dr["Delivery"];
            Shipping =(int)dr["Shipping"];
            Against = (int)dr["Against"];
            Support = (int)dr["Support"];
            Title = dr["Title"].ToString(); 
            Comment = dr["Comment"].ToString(); 
            CheckInTime =(DateTime)dr["CheckInTime"]; 
		}
		#endregion
   }
}