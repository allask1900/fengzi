using System;
using System.Text.RegularExpressions;
using log4net;
namespace FZ.Spider.DAL.Entity.Search
{
	/// <summary>
	/// 实体类(ECategory)
	/// </summary>
   public class ECategory
   {       
		#region column
       private int m_ParentCategoryID = 0;
		private  int m_CategoryID=0;
		private  string m_CategoryName;
        
        private string m_PageTitle;
        private string m_Description;
        private string m_KeyWord;
        private string m_HotWord; 
		private  Int32 m_CategoryLevel;
		private  bool m_HasChild;
        private string m_CategoryNameAndUrlCount;
        private Int32 m_ProductCount = 0;
		#endregion
		#region attribute
       /// <summary>
       /// 父分类
       /// </summary>
        public int ParentCategoryID
        {
            get { return m_ParentCategoryID; }
            set { m_ParentCategoryID = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int CategoryID
		{
			get{ return m_CategoryID; }
			set{ m_CategoryID=value; }
		}
		/// <summary>
		/// 分类名称
		/// </summary>
		public string CategoryName
		{
			get{ return m_CategoryName; }
			set{ m_CategoryName=value; }
		}		 
		/// <summary>
		/// 分类级别
		/// </summary>
		public Int32 CategoryLevel
		{
			get{ return m_CategoryLevel; }
			set{ m_CategoryLevel=value; }
		}
		/// <summary>
		/// 是否有子类
		/// </summary>
        public bool HasChild
		{
			get{ return m_HasChild; }
			set{ m_HasChild=value; }
		}
       /// <summary>
       /// 一级分类的页面标题
       /// </summary>
        public string PageTitle
        {
            get { return m_PageTitle; }
            set { m_PageTitle = value; }
        }
        /// <summary>
        /// 一级分类的页面简介
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        /// <summary>
        /// 一级分类的页面关键字
        /// </summary>
        public string KeyWord
        {
            get { return m_KeyWord; }
            set { m_KeyWord = value; }
        }
       /// <summary>
       /// 一级分类搜索关键字
       /// </summary>
        public string HotWord
        {
            get { return m_HotWord; }
            set { m_HotWord = value; }
        }
       /// <summary>
       /// 分类及该分类Url资源数(如:图书(10))
       /// </summary>
        public string CategoryNameAndUrlCount
        {
            get { return m_CategoryNameAndUrlCount; }
            set { m_CategoryNameAndUrlCount = value; }
        }
        public Int32 ProductCount
        {
            get { return m_ProductCount; }
            set { m_ProductCount = value; }
        }
		#endregion
		#region Construct
		public  ECategory()
		{
		}
		public ECategory(System.Data.IDataReader dr)
		{
			if (!dr.IsDBNull(dr.GetOrdinal("CategoryID"))) { m_CategoryID =(int)dr["CategoryID"];} 
			if (!dr.IsDBNull(dr.GetOrdinal("CategoryName"))) { m_CategoryName = dr["CategoryName"].ToString();}
           
			if (!dr.IsDBNull(dr.GetOrdinal("CategoryLevel"))) { m_CategoryLevel =Convert.ToInt32(dr["CategoryLevel"]);} 
			if (!dr.IsDBNull(dr.GetOrdinal("HasChild"))) { m_HasChild =Convert.ToBoolean(dr["HasChild"]);}
            if (!dr.IsDBNull(dr.GetOrdinal("ProductCount"))) { m_ProductCount = Convert.ToInt32(dr["ProductCount"]); }

            if (!dr.IsDBNull(dr.GetOrdinal("KeyWord"))) { m_KeyWord = dr["KeyWord"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("PageTitle"))) { m_PageTitle = dr["PageTitle"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("HotWord"))) { m_HotWord = dr["HotWord"].ToString(); }
            if (!dr.IsDBNull(dr.GetOrdinal("Description"))) { m_Description = dr["Description"].ToString(); }
		}        
		#endregion
   }
   public struct SCategory
   {
       private static ILog logger = LogManager.GetLogger(typeof(SCategory).FullName);
       /// <summary>
       /// 一级分类ID
       /// </summary>
       public int FirstCategoryID;
       /// <summary>
       /// 第一级分类名称
       /// </summary>
       public string FirstCategoryName;
       /// <summary>
       /// 产品Url代码(第一级CategoryUrlCode)
       /// </summary>
       public string FirstCategoryUrlCode;
       /// <summary>
       /// 分类ID
       /// </summary>
       public int CategoryID;
       /// <summary>
       /// 分类代码
       /// </summary>
       public string CategoryUrlCode;
       /// <summary>
       /// 分类名称
       /// </summary>
       public string CategoryName;
       public SCategory(int categoryid, string categoryname, string firstcategoryname)
       {
           CategoryID = categoryid;
           CategoryUrlCode =FZ.Spider.Common.UrlHelper.ConvertUrlName(categoryname);
           FirstCategoryUrlCode = FZ.Spider.Common.UrlHelper.ConvertUrlName(firstcategoryname);
           if (categoryid > 0)
               FirstCategoryID = Convert.ToInt32(categoryid.ToString().Substring(0, 2));
           else
               FirstCategoryID = 0;
           CategoryName = categoryname;
           FirstCategoryName = firstcategoryname;
       } 
   }
  
}