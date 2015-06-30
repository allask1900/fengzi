using System;
namespace FZ.Spider.DAL.Entity.Search
{
	/// <summary>
	/// 实体类(ESite)
	/// </summary>
   public class ESite
   {
		#region column
		private  int m_SiteID;
		private  string m_SiteName; 
		private  string m_SiteDomain;
        private string m_CategoryIDS;
        private int m_AnalysisCategoryID;
		private  DateTime m_LastAnalysisDate;
		private  string m_LastAnalysisItem;
        private int m_SiteLogo = 0;
        private int m_IsPartner = 0;
        private int m_ProductSequency = 0;
        private string m_CharSet = string.Empty;
        private string m_SiteDescription;
        private int m_SiteProductInfoLevel;
        private string m_Reg_GetSiteCategoryUrl="";
        private System.Text.Encoding m_SiteEncoding = System.Text.Encoding.Default;
        private string m_RootCategoryUrl = "";
       /// <summary>
       /// 
       /// </summary>
        private int m_Rank;
		#endregion
		#region attribute
		/// <summary>
		/// 
		/// </summary>
		public int SiteID
		{
			get{ return m_SiteID; }
			set{ m_SiteID=value; }
		}
        public int IsPartner
        {
            get { return m_IsPartner; }
            set { m_IsPartner = value; }
        }
       /// <summary>
       /// 产品列表排序
       /// </summary>
        public int ProductSequency
        {
            get { return m_ProductSequency; }
            set { m_ProductSequency = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string SiteName
		{
			get{ return m_SiteName; }
			set{ m_SiteName=value; }
		} 
		/// <summary>
		/// 
		/// </summary>
		public string SiteDomain
		{
			get{ return m_SiteDomain; }
			set{ m_SiteDomain=value; }
		}
        /// <summary>
        /// 
        /// </summary>
        public string CategoryIDS
        {
            get { return m_CategoryIDS; }
            set { m_CategoryIDS = value; }
        }
        public int AnalysisCategoryID
        {
            get { return m_AnalysisCategoryID; }
            set { m_AnalysisCategoryID = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastAnalysisDate
		{
			get{ return m_LastAnalysisDate; }
			set{ m_LastAnalysisDate=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string LastAnalysisItem
		{
			get{ return m_LastAnalysisItem; }
			set{ m_LastAnalysisItem=value; }
		}
        public int SiteLogo
        {
            get { return m_SiteLogo; }
            set { m_SiteLogo = value; }
        }
       /// <summary>
       /// 网站编码
       /// </summary>
        public string CharSet
        {
            get { return m_CharSet; }
            set
            {
                m_CharSet = value;

                if (m_CharSet != string.Empty)
                {
                    if (m_CharSet.ToLower() == "utf-8")
                    {
                        m_SiteEncoding = System.Text.Encoding.UTF8;
                    }
                    else if (m_CharSet.ToLower() == "gb2312")
                    {
                        m_SiteEncoding = System.Text.Encoding.Default;
                    }
                }
                else
                {
                    m_SiteEncoding = System.Text.Encoding.Default;
                }

            }
        }
        public string SiteDescription
        {
            get { return m_SiteDescription; }
            set { m_SiteDescription = value; }
        }
       /// <summary>
       /// 站点信息使用级别
       /// </summary>
        public int SiteProductInfoLevel
        {
            get { return m_SiteProductInfoLevel; }
            set { m_SiteProductInfoLevel = value; }
        }
        public System.Text.Encoding SiteEncoding
        {
            get { return m_SiteEncoding; }             
        }
       /// <summary>
       /// 排名，初始使用Alexa排名
       /// </summary>
        public int Rank
        {
            get { return m_Rank; }
            set { m_Rank = value; }
        }
       /// <summary>
        /// 提取网站分类的正则表达式集合(格式:GetPageCode_1----reg_GetCategory_1----RegexReplace_ChangeCategoryUrl_1====reg_GetPageCode_2----reg_GetCategory_2----RegexReplace_ChangeCategoryUrl_2)
       /// </summary>
        public string Reg_GetSiteCategoryUrl
        {
            get { return m_Reg_GetSiteCategoryUrl; }
            set { m_Reg_GetSiteCategoryUrl = value; }
        }
       /// <summary>
       /// 网站分类根入口
       /// </summary>
       public string RootCategoryUrl
       {
           get { return m_RootCategoryUrl; }
           set { m_RootCategoryUrl = value; }
       }
       public int SpiderReadCount
       {
           get;
           set;
       }
       public int SpiderSleepTime
       {
           get;
           set;
       }
       /// <summary>
       /// 站点状态 0 初始状态；1 通过；2 不抓取
       /// </summary>
       public int Status
       {
           get;
           set;
       }
		#endregion
		#region Construct
		public  ESite()
		{
		}
		public ESite(System.Data.IDataReader dr)
		{
			if (!dr.IsDBNull(dr.GetOrdinal("SiteID"))) { m_SiteID =(int)dr["SiteID"];}
            if (!dr.IsDBNull(dr.GetOrdinal("SiteLogo"))) { m_SiteLogo = (int)dr["SiteLogo"]; } 
			if (!dr.IsDBNull(dr.GetOrdinal("SiteName"))) { m_SiteName = dr["SiteName"].ToString();}
            if (!dr.IsDBNull(dr.GetOrdinal("SiteDomain"))) { m_SiteDomain = dr["SiteDomain"].ToString();}
            if (!dr.IsDBNull(dr.GetOrdinal("CategoryIDS"))) { m_CategoryIDS = dr["CategoryIDS"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("SiteDescription"))) { m_SiteDescription = dr["SiteDescription"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("SiteProductInfoLevel"))) { m_SiteProductInfoLevel = Convert.ToInt32(dr["SiteProductInfoLevel"]); }
            if (!dr.IsDBNull(dr.GetOrdinal("IsPartner"))) { m_IsPartner = Convert.ToInt32(dr["IsPartner"]); }
            if (!dr.IsDBNull(dr.GetOrdinal("ProductSequency"))) { m_ProductSequency = (int)dr["ProductSequency"]; } 
			if (!dr.IsDBNull(dr.GetOrdinal("LastAnalysisDate"))) { m_LastAnalysisDate =(DateTime)dr["LastAnalysisDate"];} 
			if (!dr.IsDBNull(dr.GetOrdinal("LastAnalysisItem"))) { m_LastAnalysisItem = dr["LastAnalysisItem"].ToString();}
         
          
            CharSet = dr["CharSet"].ToString();
            if (!dr.IsDBNull(dr.GetOrdinal("SiteProductInfoLevel"))) m_SiteProductInfoLevel = Convert.ToInt32(dr["SiteProductInfoLevel"]);
            if (!dr.IsDBNull(dr.GetOrdinal("Rank"))) m_Rank = Convert.ToInt32(dr["Rank"]);
            if (!dr.IsDBNull(dr.GetOrdinal("Reg_GetSiteCategoryUrl"))) { m_Reg_GetSiteCategoryUrl = dr["Reg_GetSiteCategoryUrl"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("RootCategoryUrl"))) { m_RootCategoryUrl = dr["RootCategoryUrl"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("SpiderReadCount"))) SpiderReadCount = Convert.ToInt32(dr["SpiderReadCount"]);
            if (!dr.IsDBNull(dr.GetOrdinal("SpiderSleepTime"))) SpiderSleepTime = Convert.ToInt32(dr["SpiderSleepTime"]);
            Status = Convert.ToInt32(dr["Status"]);
		}
		#endregion
   }
}