using System;
namespace FZ.Spider.DAL.Entity.Sys
{
	/// <summary>
	/// 实体类(ESYS_Logging)
	/// </summary>
   public class ELogging
   {
		#region column
		private  string m_LogID;
		private  Int32 m_AppID;
		private  DateTime m_LogDate;
		private  string m_Thread;
		private  string m_LogLevel;
		private  string m_Logger;
		private  string m_Class;
		private  string m_Method;
        private string m_SiteName;
		private  string m_Message;
		private  string m_Exception;
		#endregion
		#region attribute
		/// <summary>
		/// 
		/// </summary>
		public string LogID
		{
			get{ return m_LogID; }
			set{ m_LogID=value; }
		}
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
		public DateTime LogDate
		{
			get{ return m_LogDate; }
			set{ m_LogDate=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Thread
		{
			get{ return m_Thread; }
			set{ m_Thread=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string LogLevel
		{
			get{ return m_LogLevel; }
			set{ m_LogLevel=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Logger
		{
			get{ return m_Logger; }
			set{ m_Logger=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Class
		{
			get{ return m_Class; }
			set{ m_Class=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Method
		{
			get{ return m_Method; }
			set{ m_Method=value; }
		}
        public string SiteName
        {
            get { return m_SiteName; }
            set { m_SiteName=value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string Message
		{
			get{ return m_Message; }
			set{ m_Message=value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string Exception
		{
			get{ return m_Exception; }
			set{ m_Exception=value; }
		}
		#endregion
		#region Construct
		public ELogging()
		{
		}
		public ELogging(System.Data.IDataReader dr)
		{
			m_LogID = dr["LogID"].ToString();
			m_AppID =Convert.ToInt32(dr["AppID"]);
			m_LogDate =(DateTime)dr["LogDate"];
			m_Thread = dr["Thread"].ToString();
			m_LogLevel = dr["LogLevel"].ToString();
			m_Logger = dr["Logger"].ToString();
			m_Class = dr["Class"].ToString();
			m_Method = dr["Method"].ToString();
            m_SiteName = dr["SiteName"].ToString();
			m_Message = dr["Message"].ToString();
			m_Exception = dr["Exception"].ToString();
		}
		#endregion
   }
}