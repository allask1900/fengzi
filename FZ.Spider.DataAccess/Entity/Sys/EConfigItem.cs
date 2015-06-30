using System;
namespace FZ.Spider.DAL.Entity.Sys
{ 
   public class EConfigItem
   {
		#region column
		private  Int32 m_ItemID;
		private  Int32 m_ConfID;
		private  Int32 m_SysID;
		private  string m_KeyName;
		private  string m_Value;
		private  string m_Description;
	 
		private  DateTime m_CreateTime;
		private  string m_CreateUser;
		private  DateTime m_LastChangeTime;
        private string m_DataType;
		#endregion
		#region attribute
		/// <summary>
		/// 
		/// </summary>
		public Int32 ItemID
		{
			get{ return m_ItemID; }
			set{ m_ItemID=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int32 ConfID
		{
			get{ return m_ConfID; }
			set{ m_ConfID=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public Int32 SysID
		{
			get{ return m_SysID; }
			set{ m_SysID=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyName
		{
			get{ return m_KeyName; }
			set{ m_KeyName=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Value
		{
			get{ return m_Value; }
			set{ m_Value=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			get{ return m_Description; }
			set{ m_Description=value; }
		}
	 
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			get{ return m_CreateTime; }
			set{ m_CreateTime=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUser
		{
			get{ return m_CreateUser; }
			set{ m_CreateUser=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastChangeTime
		{
			get{ return m_LastChangeTime; }
			set{ m_LastChangeTime=value; }
		}
        public string DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }
		#endregion
		#region Construct
		public EConfigItem()
		{
		}
		public EConfigItem(System.Data.IDataReader dr)
		{
			m_ItemID =Convert.ToInt32(dr["ItemID"]);
			m_ConfID =Convert.ToInt32(dr["ConfID"]);
			m_SysID =Convert.ToInt32(dr["SysID"]);
			m_KeyName = dr["KeyName"].ToString();
			m_Value = dr["Value"].ToString();
			m_Description = dr["Description"].ToString();		 
			m_CreateTime =(DateTime)dr["CreateTime"];
			m_CreateUser = dr["CreateUser"].ToString();
			m_LastChangeTime =(DateTime)dr["LastChangeTime"];
            m_DataType = dr["DataType"].ToString();
		}
		#endregion
   }
}