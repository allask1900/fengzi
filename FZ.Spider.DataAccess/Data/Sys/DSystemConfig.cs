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
	/// 数据访问类DSubSystemConfig 。
	/// </summary>
	public class DSystemConfig:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DSystemConfig).FullName);
		public DSystemConfig()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static bool Add(ESystemConfig esystemconfig)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_sys_SystemConfig([SysID],[ConfName],[Description],[Isvalid], [CreateUser]) VALUES (@SysID,@ConfName,@Description,@Isvalid,@CreateUser)");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, esystemconfig.SysID);
                db.AddInParameter(dbCommand, "@ConfName", DbType.String, esystemconfig.ConfName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, esystemconfig.Description);
                db.AddInParameter(dbCommand, "@Isvalid", DbType.Int32, esystemconfig.Isvalid);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, esystemconfig.CreateUser);
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
        public static bool Update(ESystemConfig esystemconfig)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_sys_SystemConfig SET [SysID]=@SysID,[ConfName]=@ConfName,[Description]=@Description,[Isvalid]=@Isvalid,[CreateUser]=@CreateUser,[LastChangeTime]=GETDATE()  WHERE [ConfID] = @ConfID");
                db.AddInParameter(dbCommand, "@ConfID", DbType.Int32, esystemconfig.ConfID);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, esystemconfig.SysID);
                db.AddInParameter(dbCommand, "@ConfName", DbType.String, esystemconfig.ConfName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, esystemconfig.Description);
                db.AddInParameter(dbCommand, "@Isvalid", DbType.Int32, esystemconfig.Isvalid);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, esystemconfig.CreateUser);
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
		public static bool Delete(int ConfID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_sys_SystemConfig  WHERE [ConfID] = @ConfID");
                db.AddInParameter(dbCommand, "@ConfID", DbType.Int32, ConfID);
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
		public static ESystemConfig GetEntity(int ConfID)
		{
			ESystemConfig esubsystemconfig=new ESystemConfig();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_sys_SystemConfig WHERE [ConfID] = @ConfID");
                db.AddInParameter(dbCommand, "@ConfID", DbType.Int32, ConfID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    esubsystemconfig = new ESystemConfig(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
			return esubsystemconfig;
		}

		#endregion

        public static List<ESystemConfig> GetList(int sysid)
        {
            List<ESystemConfig> cSystemConfig = new List<ESystemConfig>();
            string sql = "select * from tb_sys_SystemConfig";
            if (sysid != 0)
                sql = sql + " where sysid=" + sysid;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cSystemConfig.Add(new ESystemConfig(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return cSystemConfig;
        }

        public static bool CheckConfDelete(int ConfID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select count(1) from tb_sys_configitem where ConfID=" + ConfID);
                object ob = db.ExecuteScalar(dbCommand);
                if (ob != DBNull.Value && Convert.ToInt32(ob) > 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
        }
	}
}

