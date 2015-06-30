using System;
namespace FZ.Spider.DAL.Entity.Sys
{ 
   public class ESystem
   {
		#region column
		private  Int32 m_SysID=0;
		private  string m_SysName;
		private  string m_Description;
		private  Int32 m_Sort;
		private  DateTime m_CreateTime;
		private  string m_CreateUser;
		private  DateTime m_LastChangeTime;
		#endregion
		#region attribute
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
		public string SysName
		{
			get{ return m_SysName; }
			set{ m_SysName=value; }
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
		public Int32 Sort
		{
			get{ return m_Sort; }
			set{ m_Sort=value; }
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
		public ESystem()
		{
		}
		public ESystem(System.Data.IDataReader dr)
		{
			m_SysID =Convert.ToInt32(dr["SysID"]);
			m_SysName = dr["SysName"].ToString();
			m_Description = dr["Description"].ToString();
			m_Sort =Convert.ToInt32(dr["Sort"]);
			m_CreateTime =(DateTime)dr["CreateTime"];
			m_CreateUser = dr["CreateUser"].ToString();
			m_LastChangeTime =(DateTime)dr["LastChangeTime"];
		}
		#endregion
   }
}