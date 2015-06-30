using System;
namespace FZ.Spider.DAL.Entity.Search
{
	/// <summary>
	/// 实体类(ESiteCategory)
	/// </summary>
   public class ESiteCategory
   {
		#region column
		private  int m_SCID=0;
		private  int m_SiteID;
		private  int m_CategoryID=0;
		private  string m_SCUrl;
		private  string m_SCName=string.Empty;
		private  string m_LastAnalysisItem;
        private string m_CategoryName=string.Empty;
		private  DateTime m_LastAnalysisDate;

		#endregion
		#region attribute
		/// <summary>
		/// 站点分类ID
		/// </summary>
		public int SCID
		{
			get{ return m_SCID; }
			set{ m_SCID=value; }
		}
		/// <summary>
		/// 所属站点
		/// </summary>
		public int SiteID
		{
			get{ return m_SiteID; }
			set{ m_SiteID=value; }
		}
		/// <summary>
		/// 所属分类
		/// </summary>
		public int CategoryID
		{
			get{ return m_CategoryID; }
			set{ m_CategoryID=value; }
		}
		/// <summary>
		/// 站点分类入口Url
		/// </summary>
		public string SCUrl
		{
			get{ return m_SCUrl; }
			set{ m_SCUrl=value; }
		}
		/// <summary>
		/// 站点分类名称
		/// </summary>
		public string SCName
		{
			get{ return m_SCName; }
			set{ m_SCName=value; }
		}
		/// <summary>
		/// 最后分析标识
		/// </summary>
		public string LastAnalysisItem
		{
			get{ return m_LastAnalysisItem; }
			set{ m_LastAnalysisItem=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastAnalysisDate
		{
			get{ return m_LastAnalysisDate; }
			set{ m_LastAnalysisDate=value; }
		}
        public string CategoryName
        {
            get { return m_CategoryName; }
            set { m_CategoryName = value; }
        }
        public int CategoryLevel
        {
            get;
            set;
        }
		#endregion
		#region Construct
		public  ESiteCategory()
		{
		}
		public ESiteCategory(System.Data.IDataReader dr)
		{
			if (!dr.IsDBNull(dr.GetOrdinal("SCID"))) { m_SCID =(int)dr["SCID"];} 
			if (!dr.IsDBNull(dr.GetOrdinal("SiteID"))) { m_SiteID =(int)dr["SiteID"];} 
			if (!dr.IsDBNull(dr.GetOrdinal("CategoryID"))) { m_CategoryID =(int)dr["CategoryID"];} 
			if (!dr.IsDBNull(dr.GetOrdinal("SCUrl"))) { m_SCUrl = dr["SCUrl"].ToString();} 
			if (!dr.IsDBNull(dr.GetOrdinal("SCName"))) { m_SCName = dr["SCName"].ToString();}
            if (!dr.IsDBNull(dr.GetOrdinal("CategoryName"))) { m_CategoryName = dr["CategoryName"].ToString(); } 
			if (!dr.IsDBNull(dr.GetOrdinal("LastAnalysisItem"))) { m_LastAnalysisItem = dr["LastAnalysisItem"].ToString();} 
			if (!dr.IsDBNull(dr.GetOrdinal("LastAnalysisDate"))) { m_LastAnalysisDate =(DateTime)dr["LastAnalysisDate"];} 
		}
		#endregion
   }
}