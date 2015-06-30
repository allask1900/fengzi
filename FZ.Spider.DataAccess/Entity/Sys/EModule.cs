using System;
namespace FZ.Spider.DAL.Entity.Sys
{ 
   public class EModule
   {
		#region column
		private  Int32 m_ModuleID=0;
		private  Int32 m_SysID=0;
		private  string m_ModuleName;
        private Int32 m_ParentModuleID=0;
		private  string m_ModuleURL;
		private  string m_Description;
		private  Int32 m_Sort=0;
		private  DateTime m_CreateTime;
		private  string m_CreateUser;
		private  DateTime m_LastChangeTime;
		#endregion
		#region attribute
		/// <summary>
		/// 
		/// </summary>
		public Int32 ModuleID
		{
			get{ return m_ModuleID; }
			set{ m_ModuleID=value; }
		}
		/// <summary>
		/// 
		/// </summary>
        public Int32 SysID
		{
			get{ return m_SysID; }
            set { m_SysID = value; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModuleName
		{
			get{ return m_ModuleName; }
			set{ m_ModuleName=value; }
		}
        /// <summary>
        /// 
        /// </summary>
        public Int32 ParentModuleID
        {
            get { return m_ParentModuleID; }
            set { m_ParentModuleID = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string ModuleURL
		{
			get{ return m_ModuleURL; }
			set{ m_ModuleURL=value; }
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
		public EModule()
		{
		}
		public EModule(System.Data.IDataReader dr)
		{
			m_ModuleID =Convert.ToInt32(dr["ModuleID"]);
			m_SysID =Convert.ToInt32(dr["SysID"]);
			m_ModuleName = dr["ModuleName"].ToString();
            m_ParentModuleID = Convert.ToInt32(dr["ParentModuleID"]);
			m_ModuleURL = dr["ModuleURL"].ToString();
			m_Description = dr["Description"].ToString();
			m_Sort =Convert.ToInt32(dr["Sort"]);
			m_CreateTime =(DateTime)dr["CreateTime"];
			m_CreateUser = dr["CreateUser"].ToString();
			m_LastChangeTime =(DateTime)dr["LastChangeTime"];
		}
		#endregion


   }
}