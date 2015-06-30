using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Entity.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using log4net;

namespace FZ.Spider.DAL.Data.Search
{
	/// <summary>
	/// 数据访问类DSiteCategory 。
	/// </summary>
	public class DSiteCategory:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DSiteCategory).FullName);
		public DSiteCategory()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static bool Add(ESiteCategory esitecategory)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_SEARCH_SiteCategory ([SiteID],[CategoryID],[SCUrl],[SCName],[CategoryName] ) VALUES(@SiteID,@CategoryID,@SCUrl,@SCName,@CategoryName )");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, esitecategory.SiteID);
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, esitecategory.CategoryID);
                db.AddInParameter(dbCommand, "@SCUrl", DbType.String, esitecategory.SCUrl);
                db.AddInParameter(dbCommand, "@SCName", DbType.String, esitecategory.SCName);
                db.AddInParameter(dbCommand, "@CategoryName", DbType.String, esitecategory.CategoryName);
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
		public static bool Delete(int SCID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_SEARCH_SiteCategory WHERE [SCID] = @SCID");
                db.AddInParameter(dbCommand, "@SCID", DbType.Int32, SCID); 
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
        public static bool Delete(string SCIDS)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_SEARCH_SiteCategory WHERE  scid in (" + SCIDS + ")");               
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
        /// 删除一个战队的所有未转换的分类(categoryid=0)
        /// </summary>
        public static bool DeleteBySite(int siteid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_SEARCH_SiteCategory WHERE categoryid=0 and [siteid] = @siteid");
                db.AddInParameter(dbCommand, "@siteid", DbType.Int32, siteid);
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
        /// 删除一个战队的所有未转换的分类(categoryid=0)
        /// </summary>
        public static bool DeleteByIDS(string scids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_SEARCH_SiteCategory WHERE categoryid<100 and scid in (" + scids + ") ");          
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
		/// 得到一个对象实体
		/// </summary>
		public static ESiteCategory GetEntity(int SCID)
		{
			ESiteCategory esitecategory=new ESiteCategory();            
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_SEARCH_SiteCategory WHERE [SCID] = @SCID");
                db.AddInParameter(dbCommand, "@SCID", DbType.Int32, SCID); 
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    esitecategory = new ESiteCategory(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return esitecategory;
		}

		#endregion
        /// <summary>
        /// 更新站点入口，使之与系统分类对应
        /// </summary> 
        /// <returns></returns>
        public static bool UpdateSiteCategory(int sysCategory, string categoryName, string scids)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("update TB_SEARCH_SiteCategory set CategoryID=" + sysCategory + ",CategoryName=@categoryName where scid in (" + scids + ")");
                db.AddInParameter(dbCommand, "@categoryName", DbType.String, categoryName);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            }
        } 

        public static List<ESiteCategory> GetSiteCategory(int SiteID)
        {
            List<ESiteCategory> csitecategory = new List<ESiteCategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_SEARCH_SiteCategory  WHERE  SiteID = @SiteID ");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    csitecategory.Add(new ESiteCategory(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }            
            return csitecategory;
        }
        public static List<ESiteCategory> GetSiteCategory(int SiteID, int CategoryID)
        {
            string sql = "SELECT * FROM TB_SEARCH_SiteCategory  WHERE SiteID = @SiteID  and  CategoryID like '" + CategoryID + "%' order by categoryid desc";
            List<ESiteCategory> csitecategory = new List<ESiteCategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    csitecategory.Add(new ESiteCategory(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return csitecategory;
        }
        public static List<ESiteCategory> GetList(EQueryPage qe)
        {
            List<ESiteCategory> siteCategoryList = new List<ESiteCategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(GetPageSql(qe));
                if (qe.IsTotal) db.AddOutParameter(dbCommand, "@TotalRecord", DbType.Int32, 4);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    siteCategoryList.Add(new ESiteCategory(dr));
                }
                dr.Close();
                if (qe.IsTotal)
                {
                    object ob = db.GetParameterValue(dbCommand, "@TotalRecord");
                    int count = 0;
                    if (ob != null && ob != DBNull.Value)
                    {
                        Int32.TryParse(ob.ToString(), out count);
                        qe.TotalRecord = count;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return siteCategoryList;
        }
        public static bool CheckSCUrl(string SCUrl)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("select categoryid from TB_SEARCH_SiteCategory where scurl='@SCUrl'");
            db.AddInParameter(dbCommand, "@SCUrl", DbType.String, SCUrl);
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value)
            {
                return true;
            }
            return false;
        }
	}
}

