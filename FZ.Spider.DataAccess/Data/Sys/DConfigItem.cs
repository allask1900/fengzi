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
	/// 数据访问类DConfigItem 。
	/// </summary>
	public class DConfigItem:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DConfigItem).FullName);
		public DConfigItem()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static bool Add(EConfigItem econfigitem)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_Sys_ConfigItem([ConfID],[SysID],[KeyName],Value,[Description],[CreateUser],datatype )VALUES(@ConfID,@SysID,@KeyName,@Value,@Description,@CreateUser,@DataType)");
                db.AddInParameter(dbCommand, "@ConfID", DbType.Int32, econfigitem.ConfID);
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, econfigitem.SysID);
                db.AddInParameter(dbCommand, "@KeyName", DbType.String, econfigitem.KeyName);
                db.AddInParameter(dbCommand, "@Value", DbType.String, econfigitem.Value);
                db.AddInParameter(dbCommand, "@Description", DbType.String, econfigitem.Description);
                db.AddInParameter(dbCommand, "@CreateUser", DbType.String, econfigitem.CreateUser);
                db.AddInParameter(dbCommand, "@DataType", DbType.String, econfigitem.DataType); 
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
		public static bool Update(EConfigItem econfigitem)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Sys_ConfigItem SET [KeyName]=@KeyName,[Value]=@Value,[Description]=@Description,DataType=@DataType,LastChangeTime=GETDATE() WHERE [ItemID] = @ItemID");
                db.AddInParameter(dbCommand, "@ItemID", DbType.Int32, econfigitem.ItemID);
                db.AddInParameter(dbCommand, "@KeyName", DbType.String, econfigitem.KeyName);
                db.AddInParameter(dbCommand, "@Value", DbType.String, econfigitem.Value); 
                db.AddInParameter(dbCommand, "@Description", DbType.String, econfigitem.Description);
                db.AddInParameter(dbCommand, "@DataType", DbType.String, econfigitem.DataType); 
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
        /// 更新数据 key  ---> value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Update(string key,string value)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Sys_ConfigItem SET [Value]=@Value LastChangeTime=GETDATE() WHERE KeyName= @KeyName");
             
                db.AddInParameter(dbCommand, "@KeyName", DbType.String, key);
                db.AddInParameter(dbCommand, "@Value", DbType.String,value);
               
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            }
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public static bool Delete(int ItemID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("delete from TB_Sys_ConfigItem where ItemID=@ItemID");
                db.AddInParameter(dbCommand, "@ItemID", DbType.Int32, ItemID);
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
		public static EConfigItem GetEntity(int ItemID)
		{
			EConfigItem econfigitem=new EConfigItem();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Sys_ConfigItem WHERE [ItemID] = @ItemID");
                db.AddInParameter(dbCommand, "@ItemID", DbType.Int32, ItemID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    econfigitem = new EConfigItem(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return econfigitem;
		}
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static EConfigItem GetEntity(int SysID,string keyName)
        {
            EConfigItem econfigitem = new EConfigItem();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Sys_ConfigItem WHERE [SysID] = @SysID and KeyName=@KeyName");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, SysID);
                db.AddInParameter(dbCommand, "@KeyName", DbType.String, keyName);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    econfigitem = new EConfigItem(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return econfigitem;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static string  GetConfigValue(int SysID, string keyName)
        {
            string value=null;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT value FROM TB_Sys_ConfigItem WHERE [SysID] = @SysID and KeyName=@KeyName");
                db.AddInParameter(dbCommand, "@SysID", DbType.Int32, SysID);
                db.AddInParameter(dbCommand, "@KeyName", DbType.String, keyName);
                object ob = db.ExecuteScalar(dbCommand);
                if (ob != null && ob != DBNull.Value)
                    value = ob.ToString();
                else
                    return null;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return value;
        }
		#endregion

       
        public static List<EConfigItem> GetList(int confid)
        {
            List<EConfigItem> cConfigItem = new List<EConfigItem>();
            StringBuilder sb = new StringBuilder(" select * from tb_sys_ConfigItem ");            
            if (confid != 0)
            {
                sb.Append(" where confid= ");
                sb.Append(confid.ToString());
            }
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cConfigItem.Add(new EConfigItem(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cConfigItem;
        }
        
        public static bool ExistsKeyForUpdate(int sysid, string keyname,int itemid)
        {
            StringBuilder sb = new StringBuilder("select count(1) from tb_sys_configitem where sysid=");
            sb.Append(sysid.ToString());
    
            sb.Append(" and ItemID<>");
            sb.Append(itemid.ToString());
            sb.Append(" and keyname='");
            sb.Append(keyname);
            sb.Append("' ");
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != DBNull.Value && Convert.ToInt32(ob) > 0)
                return true;
            return false;
        }

        public static bool ExistsKeyForAdd(int sysid,  string keyname)
        {
            StringBuilder sb = new StringBuilder("select count(1) from tb_sys_configitem where sysid=");
            sb.Append(sysid.ToString());           
            sb.Append(" and keyname='");
            sb.Append(keyname);
            sb.Append("' ");
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != DBNull.Value && Convert.ToInt32(ob) > 0)
                return true;
            return false;
        }


    }
}

