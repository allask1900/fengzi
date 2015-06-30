using System;
namespace FZ.Spider.DAL.Entity.Search
{
	/// <summary>
	/// 实体类(EBrand)
	/// </summary>
   public class EBrand
   {
		#region column
		private  int m_BrandID;
		private  string m_BrandName; 
        private bool m_IsValid;
        private int m_IsValidBrandID;
        private int m_ProductCount=0;
		#endregion
		#region attribute
		/// <summary>
		/// 品牌ID
		/// </summary>
		public int BrandID
		{
			get{ return m_BrandID; }
			set{ m_BrandID=value; }
		}
		/// <summary>
		/// 品牌名称
		/// </summary>
		public string BrandName
		{
			get{ return m_BrandName; }
			set{ m_BrandName=value; }
		} 
       /// <summary>
       /// 是否启用
       /// </summary>
        public bool IsValid
        {
            get { return m_IsValid; }
            set { m_IsValid = value; }
        }
       /// <summary>
       /// 真实品牌ID
       /// </summary>
        public int IsValidBrandID
        {
            get { return m_IsValidBrandID; }
            set { m_IsValidBrandID = value; }
        }
        public int ProductCount
        {
            get { return m_ProductCount; }
            set { m_ProductCount = value; }
        }
		#endregion
		#region Construct
		public  EBrand()
		{
		}
		public EBrand(System.Data.IDataReader dr)
		{
			m_BrandID =(int)dr["BrandID"];
			m_BrandName = dr["BrandName"].ToString(); 
            m_IsValid = Convert.ToBoolean(dr["IsValid"]);
            m_IsValidBrandID = (int)dr["IsValidBrandID"];
		}
        public EBrand(int brandid,string brandname)
        {
            m_BrandID = brandid;
            m_BrandName = brandname;           
        }
		#endregion
   }
}