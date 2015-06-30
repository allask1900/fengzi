using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Search;

using Microsoft.Practices.EnterpriseLibrary.Data;

using log4net;
using System.Collections.Generic;

namespace FZ.Spider.DAL.Data.Search
{
    public class DSiteConfig:DBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DSiteConfig).FullName);
        public static int Add(ESiteConfig eSiteConfig)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);           
                StringBuilder sbSql = new StringBuilder("");
                if (eSiteConfig.OrdID > 0)
                {
                    sbSql.Append("update TB_Search_SiteConfig set SiteID=@SiteID,CategoryIDS=@CategoryIDS,SpiderTemplet=@SpiderTemplet ,lastChangeTime=getdate() where OrdID=@OrdID");
                }
                else
                {
                    sbSql.Append(" insert TB_Search_SiteConfig (SiteID, CategoryIDS,SpiderTemplet) values  (@SiteID,@CategoryIDS,@SpiderTemplet)   set  @OrdID=@@IDENTITY");
                }
                DbCommand dbCommand = db.GetSqlStringCommand(sbSql.ToString());
                db.AddParameter(dbCommand, "@OrdID", DbType.Int32,ParameterDirection.InputOutput,string.Empty,DataRowVersion.Default, eSiteConfig.OrdID);

                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, eSiteConfig.SiteID);
                db.AddInParameter(dbCommand, "@CategoryIDS", DbType.String, eSiteConfig.CategoryIDS);
                db.AddInParameter(dbCommand, "@SpiderTemplet", DbType.String, eSiteConfig.SpiderTemplet);
                db.ExecuteNonQuery(dbCommand);
                return Convert.ToInt32(dbCommand.Parameters["@OrdID"].Value);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return 0;
            }
        }
        public static List<ESiteConfig> GetList(int SiteID)
        {
            List<ESiteConfig> siteConfigList = new List<ESiteConfig>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_Search_SiteConfig where SiteID=@SiteID ");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    siteConfigList.Add(new ESiteConfig(dr));                    
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return siteConfigList;
        }
        public static ESiteConfig GetEntity(int SiteID,int CategoryID)
        {
            ESiteConfig eSiteConfig = new ESiteConfig();            
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_Search_SiteConfig where SiteID=@SiteID and ','+CategoryIDS+',' like  '%," + CategoryID + ",%'");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID); 
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eSiteConfig = new ESiteConfig(dr, CategoryID);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }                
            eSiteConfig.SiteID = SiteID;            
            return eSiteConfig;
        }
        public static ESiteConfig GetEntity(int configID)
        {
            ESiteConfig eSiteConfig = new ESiteConfig();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_Search_SiteConfig where OrdID=@OrdID ");
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, configID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eSiteConfig = new ESiteConfig(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return eSiteConfig;
        }
        public static bool UpdateTestStatus(int OrdID, bool TestStatus)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("update tb_search_siteconfig set TestStatus=@TestStatus where OrdID=@OrdID");
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, OrdID);
                db.AddInParameter(dbCommand, "@TestStatus", DbType.Boolean, TestStatus);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return false;
            
        }
    }
}
