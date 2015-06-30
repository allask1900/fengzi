using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Sys;

using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using log4net;
namespace FZ.Spider.DAL.Data.Sys
{
	/// <summary>
	/// 数据访问类DApplication 。
	/// </summary>
	public class DApplication:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DApplication).FullName);
		public DApplication()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static bool Add(EApplication eapplication)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_Sys_Application([AppID],[AppName],[SysID],[Description],[CreateUser])VALUES(@AppID,@AppName,@SysID,@Description,@CreateUser)");
                db.AddInParameter(dbCommand, "@AppID", DbType.Int32, eapplication.AppID);
                db.AddInParameter(dbCommand, "@AppName", DbType.String, eapplication.AppName);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, eapplication.SysID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, eapplication.Description);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, eapplication.CreateUser);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public static bool Update(EApplication eapplication)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Sys_Application SET [AppName]=@AppName,[SysID]=@SysID,[Description]=@Description,[CreateUser]=@CreateUser WHERE [AppID] = @AppID");
                db.AddInParameter(dbCommand, "@AppID", DbType.Int32, eapplication.AppID);
                db.AddInParameter(dbCommand, "@AppName", DbType.String, eapplication.AppName);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, eapplication.SysID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, eapplication.Description);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, eapplication.CreateUser);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            } 
		}

        /// <summary>
		///  更新一条数据
		/// </summary>
		public static bool UpdateByID(EApplication eapplication,int appID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Sys_Application SET AppID=@AppID,[AppName]=@AppName,[SysID]=@SysID,[Description]=@Description,[CreateUser]=@CreateUser WHERE [AppID] = @updateAppID");
                db.AddInParameter(dbCommand, "@AppID", DbType.Int32, eapplication.AppID);
                db.AddInParameter(dbCommand, "@AppName", DbType.String, eapplication.AppName);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, eapplication.SysID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, eapplication.Description);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, eapplication.CreateUser);
                db.AddInParameter(dbCommand, "@updateAppID", DbType.Int32, appID);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int AppID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_Sys_Application WHERE [AppID] = @AppID");
                db.AddInParameter(dbCommand, "@AppID", DbType.Int32, AppID);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
        }

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static EApplication GetEntity(int AppID)
		{
			EApplication eapplication=new EApplication();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Sys_Application WHERE [AppID] = @AppID");
                db.AddInParameter(dbCommand, "@AppID", DbType.Int32, AppID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eapplication = new EApplication(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return eapplication;
        }

		#endregion

        public static List<EApplication> GetList(int sysID)
        {              
            List<EApplication> cApplication = new List<EApplication>();
            StringBuilder sb = new StringBuilder("select * from TB_Sys_Application ");
            if (sysID != 0)
            {
                sb.Append(" where sysid=");
                sb.Append(sysID);
            }
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cApplication.Add(new EApplication(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return cApplication;
        }
	}
}

