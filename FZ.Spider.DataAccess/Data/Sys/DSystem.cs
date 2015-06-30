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
	/// 数据访问类DSystem 。
	/// </summary>
	public class DSystem:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DSystem).FullName);
		public DSystem()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static bool Add(ESystem esystem)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_sys_System([SysID],[SysName],[Description],[Sort],[CreateUser])VALUES(@SysID,@SysName,@Description,@Sort,@CreateUser)");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, esystem.SysID);
                db.AddInParameter(dbCommand, "@SysName", DbType.String, esystem.SysName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, esystem.Description);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, esystem.Sort);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, esystem.CreateUser);
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
		public static bool Update(ESystem esystem)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_sys_System SET [SysName]=@SysName,[Description]=@Description,[Sort]=@Sort,Lastchangetime=GETDATE()	WHERE [SysID] = @SysID");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, esystem.SysID);
                db.AddInParameter(dbCommand, "@SysName", DbType.String, esystem.SysName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, esystem.Description);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, esystem.Sort); 
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
        public static bool UpdateByID(ESystem esystem,int updateSysID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_sys_System SET [SysID] = @SysID,[SysName]=@SysName,[Description]=@Description,[Sort]=@Sort,Lastchangetime=GETDATE() WHERE SysID=@updateSysID");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, esystem.SysID);
                db.AddInParameter(dbCommand, "@SysName", DbType.String, esystem.SysName);
                db.AddInParameter(dbCommand, "@Description", DbType.String, esystem.Description);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, esystem.Sort);
                db.AddInParameter(dbCommand, "@UpdateSysID", DbType.Int32, updateSysID);
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
		public static bool Delete(int SysID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_sys_System WHERE [SysID] = @SysID");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, SysID);
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
		public static ESystem GetEntity(int SysID)
		{
			ESystem esystem=new ESystem();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_sys_System WHERE [SysID] = @SysID");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, SysID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    esystem = new ESystem(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            } 
			return esystem;
		}

		#endregion
         
        public static List<ESystem> GetList()
        {
            List<ESystem> cSystem = new List<ESystem>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from tb_sys_system  order by sort");
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cSystem.Add(new ESystem(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }            
            return cSystem;
        }
        public static bool CheckSystemDelete(int sysID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select COUNT(1) from (select count(1) as c from tb_sys_systemconfig where SysID=");
            sb.Append(sysID);
            sb.Append(" union all select COUNT(1) as c from TB_sys_Application where SysID=");
            sb.Append(sysID);
            sb.Append(" union all select COUNT(1) as c from TB_sys_Module where SysID=");
            sb.Append(sysID);
            sb.Append(" ) as t");
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
                object ob =db.ExecuteScalar(dbCommand);
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

