using System;
namespace FZ.Spider.DAL.Entity.Sys
{
	 
   public class ESystemConfig
   {
		#region column
		private  Int32 m_ConfID;
		private  Int32 m_SysID;
		private  string m_ConfName;
		private  string m_Description;
		private  bool m_Isvalid;
		private  DateTime m_CreateTime;
		private  string m_CreateUser;
		private  DateTime m_LastChangeTime;
		#endregion
		#region attribute
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
            get { return m_SysID; }
            set { m_SysID = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ConfName
		{
			get{ return m_ConfName; }
			set{ m_ConfName=value; }
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
        public bool Isvalid
		{
			get{ return m_Isvalid; }
			set{ m_Isvalid=value; }
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
		#endregion
		#region Construct
		public ESystemConfig()
		{
		}
		public ESystemConfig(System.Data.IDataReader dr)
		{
			m_ConfID =Convert.ToInt32(dr["ConfID"]);
            m_SysID = Convert.ToInt32(dr["SysID"]);
			m_ConfName = dr["ConfName"].ToString();
			m_Description = dr["Description"].ToString();
			m_Isvalid =Convert.ToBoolean(dr["Isvalid"]);
			m_CreateTime =(DateTime)dr["CreateTime"];
			m_CreateUser = dr["CreateUser"].ToString();
			m_LastChangeTime =(DateTime)dr["LastChangeTime"];
		}
		#endregion
   }
}