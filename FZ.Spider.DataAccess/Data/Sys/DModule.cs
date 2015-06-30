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
	/// 数据访问类DApplicationModule 。
	/// </summary>
	public class DModule:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DModule).FullName);
		public DModule()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static bool Add(EModule module)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO tb_sys_module([ModuleID],[SysID],[ModuleName],[ParentModuleID],[ModuleURL],[Description],[Sort],[CreateUser])VALUES(@ModuleID,@SysID,@ModuleName,@ParentModuleID,@ModuleURL,@Description,@Sort,@CreateUser)");
                db.AddInParameter(dbCommand, "@ModuleID", DbType.Int32, module.ModuleID);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, module.SysID);
                db.AddInParameter(dbCommand, "@ModuleName", DbType.String, module.ModuleName);
                db.AddInParameter(dbCommand, "@ParentModuleID", DbType.Int32, module.ParentModuleID);
                db.AddInParameter(dbCommand, "@ModuleURL", DbType.String, module.ModuleURL);
                db.AddInParameter(dbCommand, "@Description", DbType.String, module.Description);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, module.Sort);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, module.CreateUser);            
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
		public static bool Update(EModule module)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE tb_sys_module SET [SysID]=@SysID,[ModuleName]=@ModuleName,[ParentModuleID]=@ParentModuleID,[ModuleURL]=@ModuleURL,[Description]=@Description,[Sort]=@Sort, [CreateUser]=@CreateUser,[LastChangeTime]=GETDATE() WHERE [ModuleID] = @ModuleID");
                db.AddInParameter(dbCommand, "@ModuleID", DbType.Int32, module.ModuleID);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, module.SysID);
                db.AddInParameter(dbCommand, "@ModuleName", DbType.String, module.ModuleName);
                db.AddInParameter(dbCommand, "@ParentModuleID", DbType.Int32, module.ParentModuleID);
                db.AddInParameter(dbCommand, "@ModuleURL", DbType.String, module.ModuleURL);
                db.AddInParameter(dbCommand, "@Description", DbType.String, module.Description);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, module.Sort);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, module.CreateUser);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }  
		}
        public static bool UpdateByID(EModule module,int updateModuleID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE tb_sys_module SET [ModuleID] = @ModuleID,[SysID]=@SysID,[ModuleName]=@ModuleName,[ParentModuleID]=@ParentModuleID,[ModuleURL]=@ModuleURL,[Description]=@Description,[Sort]=@Sort, [CreateUser]=@CreateUser,[LastChangeTime]=GETDATE() WHERE [ModuleID]=@updateModuleID ");
                db.AddInParameter(dbCommand, "@ModuleID", DbType.Int32, module.ModuleID);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, module.SysID);
                db.AddInParameter(dbCommand, "@ModuleName", DbType.String, module.ModuleName);
                db.AddInParameter(dbCommand, "@ParentModuleID", DbType.Int32, module.ParentModuleID);
                db.AddInParameter(dbCommand, "@ModuleURL", DbType.String, module.ModuleURL);
                db.AddInParameter(dbCommand, "@Description", DbType.String, module.Description);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, module.Sort);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, module.CreateUser);
                db.AddInParameter(dbCommand, "@updateModuleID", DbType.Int32, updateModuleID);
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
		public static bool Delete(int ModuleID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE tb_sys_module WHERE [ModuleID] = @ModuleID");
                db.AddInParameter(dbCommand, "@ModuleID", DbType.Int32, ModuleID);
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
		public static EModule GetEntity(int ModuleID)
		{
			EModule module=new EModule();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM tb_sys_module WHERE [ModuleID] = @ModuleID");
                db.AddInParameter(dbCommand, "@ModuleID", DbType.Int32, ModuleID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    module = new EModule(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
			return module;
		}

		#endregion 

        public static List<EModule> GetList(int sysID,int ParentModuleID)
        {
            List<EModule> cModule = new List<EModule>();
            StringBuilder sb = new StringBuilder("select * from tb_sys_module ");
            
            if (sysID != 0 || ParentModuleID != -1)
                sb.Append(" where ");
            if (sysID != 0)
            {
                sb.Append(" sysID=");
                sb.Append(sysID);
            }
            if (ParentModuleID != -1)               
            {
                if (sysID != 0)
                    sb.Append(" and ");
                sb.Append(" parentModuleID=");
                sb.Append(ParentModuleID);
            }
            sb.Append(" order by sort ");
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cModule.Add(new EModule(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return cModule;
        }
        
        public static bool CheckModuleDelete(int moduleid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select count(1) from tb_sys_module where parentmoduleid=" + moduleid);
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

