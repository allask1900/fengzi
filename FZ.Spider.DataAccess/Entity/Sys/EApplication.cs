using System;
namespace FZ.Spider.DAL.Entity.Sys
{ 
   public class EApplication
   {
		#region column
		private  Int32 m_AppID;
		private  string m_AppName;
		private  Int32 m_SysID;
		private  string m_Description;
		private  DateTime m_CreateTime;
		private  string m_CreateUser;
		private  DateTime m_LastChangeTime;
		#endregion
		#region attribute
		/// <summary>
		/// 
		/// </summary>
		public Int32 AppID
		{
			get{ return m_AppID; }
			set{ m_AppID=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string AppName
		{
			get{ return m_AppName; }
			set{ m_AppName=value; }
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
		#endregion
		#region Construct
		public EApplication()
		{
		}
		public EApplication(System.Data.IDataReader dr)
		{
			m_AppID =Convert.ToInt32(dr["AppID"]);
			m_AppName = dr["AppName"].ToString();
			m_SysID =Convert.ToInt32(dr["SysID"]);
			m_Description = dr["Description"].ToString();
			m_CreateTime =(DateTime)dr["CreateTime"];
			m_CreateUser = dr["CreateUser"].ToString();
			m_LastChangeTime =(DateTime)dr["LastChangeTime"];
		}
		#endregion
   }
}