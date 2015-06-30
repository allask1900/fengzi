using System;
namespace FZ.Spider.DAL.Entity.Sys
{ 
   public class EAdministrator
   {
		#region column
		private  string m_UserName;
		private  string m_Password;
		#endregion
		#region attribute
		/// <summary>
		/// 管理用户名
		/// </summary>
		public string UserName
		{
			get{ return m_UserName; }
			set{ m_UserName=value; }
		}
		/// <summary>
		/// 管理用户密码
		/// </summary>
		public string Password
		{
			get{ return m_Password; }
			set{ m_Password=value; }
		}
		#endregion
		#region Construct
		public  EAdministrator()
		{
		}
		public EAdministrator(System.Data.IDataReader dr)
		{
			if (!dr.IsDBNull(dr.GetOrdinal("UserName"))) { m_UserName = dr["UserName"].ToString();} 
			if (!dr.IsDBNull(dr.GetOrdinal("Password"))) { m_Password = dr["Password"].ToString();} 
		}
		#endregion
   }
}