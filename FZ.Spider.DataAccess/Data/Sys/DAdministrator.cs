using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Sys;

using Microsoft.Practices.EnterpriseLibrary.Data;
using log4net;
namespace FZ.Spider.DAL.Data.Sys
{
	/// <summary>
	/// 数据访问类DAdministrator 。
	/// </summary>
	public class DAdministrator:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DAdministrator).FullName);
		public DAdministrator()
		{
		} 
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public static EAdministrator GetEntity(string UserName)
		{ 
			EAdministrator eadministrator=new EAdministrator();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_SYS_Administrator where UserName=@UserName");
                db.AddInParameter(dbCommand, "@UserName", DbType.String, UserName);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eadministrator = new EAdministrator(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return eadministrator;
		} 
	}
}

