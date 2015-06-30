using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Collection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using log4net;
using System.Collections.Generic;

namespace FZ.Spider.DAL.Data.Search
{
	/// <summary>
	/// 数据访问类DSite 。
	/// </summary>
	public class DSite:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DSite).FullName);
		public DSite()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据 返回新添加网站ID
		/// </summary>
		public static int Add(ESite esite)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("	INSERT INTO TB_Search_Site([SiteName], [SiteDomain],[CategoryIDS] ,SiteLogo,CharSet,SiteProductInfoLevel,SiteDescription,SpiderReadCount,SpiderSleepTime,Ispartner,ProductSequency) VALUES( @SiteName, @SiteDomain,@CategoryIDS ,@SiteLogo,@CharSet,@SiteProductInfoLevel,@SiteDescription,@SpiderReadCount,@SpiderSleepTime,@Ispartner,@ProductSequency) set @SiteID=@@IDentity");
                db.AddInParameter(dbCommand, "@SiteName", DbType.String, esite.SiteName);
                db.AddInParameter(dbCommand, "@SiteDomain", DbType.String, esite.SiteDomain);
                db.AddInParameter(dbCommand, "@CategoryIDS", DbType.String, esite.CategoryIDS);
                db.AddInParameter(dbCommand, "@SiteLogo", DbType.Int32, esite.SiteLogo);
                db.AddInParameter(dbCommand, "@CharSet", DbType.String, esite.CharSet);
                db.AddOutParameter(dbCommand, "@SiteID", DbType.Int32,0);
                db.AddInParameter(dbCommand, "@SiteProductInfoLevel", DbType.Int32, esite.SiteProductInfoLevel);
                db.AddInParameter(dbCommand, "@SiteDescription", DbType.String, esite.SiteDescription);
                db.AddInParameter(dbCommand, "@SpiderReadCount", DbType.Int32, esite.SpiderReadCount);
                db.AddInParameter(dbCommand, "@SpiderSleepTime", DbType.Int32, esite.SpiderSleepTime);
                db.AddInParameter(dbCommand, "@Ispartner", DbType.Int32, esite.IsPartner);
                db.AddInParameter(dbCommand, "@ProductSequency", DbType.Int32, esite.ProductSequency); 
                db.ExecuteNonQuery(dbCommand);
                int siteid = Convert.ToInt32(dbCommand.Parameters["@SiteID"].Value);
                return siteid;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return 0;
            }
        }

		/// <summary>
		///  更新一条数据
		/// </summary>
        public static bool Update(ESite esite)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Site SET [SiteName]=@SiteName,[SiteDomain]=@SiteDomain,[CategoryIDS]=@CategoryIDS ,CharSet=@CharSet,SiteLogo=@SiteLogo,	SiteProductInfoLevel=@SiteProductInfoLevel,SiteDescription=@SiteDescription	,SpiderReadCount=@SpiderReadCount,SpiderSleepTime=@SpiderSleepTime,Ispartner=@Ispartner,ProductSequency=@ProductSequency  WHERE [SiteID] = @SiteID");
                db.AddInParameter(dbCommand, "@SiteName", DbType.String, esite.SiteName);
                db.AddInParameter(dbCommand, "@SiteDomain", DbType.String, esite.SiteDomain);
                db.AddInParameter(dbCommand, "@CategoryIDS", DbType.String, esite.CategoryIDS);
                db.AddInParameter(dbCommand, "@SiteLogo", DbType.Int32, esite.SiteLogo);
                db.AddInParameter(dbCommand, "@CharSet", DbType.String, esite.CharSet);
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, esite.SiteID);
                db.AddInParameter(dbCommand, "@SiteProductInfoLevel", DbType.Int32, esite.SiteProductInfoLevel);
                db.AddInParameter(dbCommand, "@SiteDescription", DbType.String, esite.SiteDescription);
                db.AddInParameter(dbCommand, "@SpiderReadCount", DbType.Int32, esite.SpiderReadCount);
                db.AddInParameter(dbCommand, "@SpiderSleepTime", DbType.Int32, esite.SpiderSleepTime);
                db.AddInParameter(dbCommand, "@Ispartner", DbType.Int32, esite.IsPartner);
                db.AddInParameter(dbCommand, "@ProductSequency", DbType.Int32, esite.ProductSequency);
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
		///  更新一条数据
		/// </summary>
		public static bool Update(int SiteID,int Status)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Site set  Status=@Status  WHERE [SiteID] = @SiteID");               
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);                
                db.AddInParameter(dbCommand, "@Status", DbType.Int32, Status);
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
        ///  更新一条数据
        /// </summary>
        public static bool UpdateRegSiteCategory(int siteid,string reg_GetSiteCategoryUrl,string rootCategoryUrl)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Site SET [reg_GetSiteCategoryUrl]=@reg_GetSiteCategoryUrl,RootCategoryUrl=@RootCategoryUrl WHERE [SiteID] = @SiteID");
                db.AddInParameter(dbCommand, "@reg_GetSiteCategoryUrl", DbType.String, reg_GetSiteCategoryUrl);
                db.AddInParameter(dbCommand, "@RootCategoryUrl", DbType.String, rootCategoryUrl);
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, siteid);               
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
        ///  更新一条数据
        /// </summary>
        public static bool UpdateCategoryIDS(string siteDomain,string categoryIDS)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Site SET  CategoryIDS=@CategoryIDS WHERE [SiteDomain] = @SiteDomain");

                db.AddInParameter(dbCommand, "@SiteDomain", DbType.String, siteDomain);
                db.AddInParameter(dbCommand, "@CategoryIDS", DbType.String, categoryIDS);
               
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
        ///  更新站点排名(Alexa Rank)
        /// </summary>
        public static bool UpdateRank(ESite es)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Site SET  Rank=@Rank WHERE [SiteDomain] = @SiteDomain");
                db.AddInParameter(dbCommand, "@SiteDomain", DbType.String, es.SiteDomain);
                db.AddInParameter(dbCommand, "@Rank", DbType.Int32, es.Rank);
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
        ///  更新站点排名及描述(Alexa Rank Desc)
        /// </summary>
        public static bool UpdateRankAndDesc(ESite es)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Site SET  Rank=@Rank,SiteDescription=@SiteDescription WHERE [SiteDomain] = @SiteDomain");
                db.AddInParameter(dbCommand, "@SiteDomain", DbType.String, es.SiteDomain);
                db.AddInParameter(dbCommand, "@Rank", DbType.Int32, es.Rank);
                db.AddInParameter(dbCommand, "@SiteDescription", DbType.String, es.SiteDescription);

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
		public static bool Delete(int SiteID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_Search_Site 	 WHERE [SiteID] = @SiteID");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
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
        public static bool Exists(string domain,string siteName)
        {            
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("select * from tb_search_site Where siteDomain='@domain' or SiteDomain like '%siteName'");
            db.AddInParameter(dbCommand, "@domain", DbType.String, domain);
            db.AddInParameter(dbCommand, "@siteName", DbType.String, siteName);
            object ob=db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value)
                return true;
            return false;            
        }
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public static ESite GetEntity(int SiteID)
		{
			ESite esite=new ESite();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Site WHERE [SiteID] = @SiteID");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    esite = new ESite(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
			return esite;
		}

		#endregion

        public static int GetMaxSiteID()
        {           
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("select max(siteid) from TB_Search_Site");
            return Convert.ToInt32(db.ExecuteScalar(dbCommand));          
        }

        public static List<ESite> GetList(EQueryPage qe)
        {
            List<ESite> siteList = new List<ESite>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(GetPageSql(qe));
                if (qe.IsTotal) db.AddOutParameter(dbCommand, "@TotalRecord", DbType.Int32, 4);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    siteList.Add(new ESite(dr));
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
            return siteList;
        }        
        /// <summary>
        /// 站点列表,当categoryID=0时提取全部站点
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static List<ESite> GetList(int categoryID)
        {
            List<ESite> siteList = new List<ESite>();          
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                string sql = "select * from tb_search_site ";
                if (categoryID > 0)
                    sql = sql + " where ','+categoryids+',' like '%," + categoryID + ",%'";
                sql = sql + " order by rank ";
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ESite eSite = new ESite(dr);
                    siteList.Add(eSite);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return siteList;
        }
        /// <summary>
        /// 站点列表,当categoryID=0时提取全部站点
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static List<ESite> GetList(int categoryID,int status)
        {
            List<ESite> siteList = new List<ESite>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                string sql = "select * from tb_search_site where status="+status;
                if (categoryID > 0)
                    sql = sql + " and ','+categoryids+',' like '%," + categoryID + ",%'";
                sql = sql + " order by rank ";
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ESite eSite = new ESite(dr);
                    siteList.Add(eSite);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return siteList;
        }
        public static CSite GetListForAnalysis(int AnalysisCategoryID)
        {
            CSite cSite = new CSite();
            cSite.CategoryID = AnalysisCategoryID;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select st.* from tb_search_site st,tb_search_siteconfig sc where sc.CategoryIDS like '%'+convert(varchar(2),@AnalysisCategoryID)+'%' and st.siteid=sc.siteid and sc.teststatus=1  ");
                db.AddInParameter(dbCommand, "@AnalysisCategoryID", DbType.Int32, AnalysisCategoryID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ESite eSite = new ESite(dr);
                    eSite.AnalysisCategoryID = AnalysisCategoryID;
                    cSite.Add(eSite);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            } 
            return cSite;
        }

        public static ESite GetSiteForAnalysis(int siteid,int AnalysisCategoryID)
        {
            ESite eSite = null;
            
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select st.* from tb_search_site st,tb_search_siteconfig sc where sc.CategoryIDS like '%'+convert(varchar(2),@AnalysisCategoryID)+'%' and st.siteid=sc.siteid and sc.teststatus=1 and sc.siteid=@siteid");
                db.AddInParameter(dbCommand, "@AnalysisCategoryID", DbType.Int32, AnalysisCategoryID);
                db.AddInParameter(dbCommand, "@siteid", DbType.Int32, siteid);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eSite = new ESite(dr);
                    eSite.AnalysisCategoryID = AnalysisCategoryID;
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return eSite;
        }

        /// <summary>
        /// 从手动添加的任务队列中获得任务
        /// </summary>
        /// <returns></returns>
        public static CSite GetListForAnalysisBySpiderWorkQueue()
        {
            CSite cSite = new CSite(); 
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select swq.AnalysisCategoryID, st.* from TB_SEARCH_SpiderWorkQueue swq  left join tb_search_site st on swq.SiteID=st.SiteID left join tb_search_siteconfig sc  on sc.CategoryIDS like '%'+convert(varchar(2),swq.AnalysisCategoryID)+'%' and swq.siteid=sc.siteid and sc.teststatus=1  where	swq.Status=0 "); 
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ESite eSite = new ESite(dr);
                    eSite.AnalysisCategoryID = Convert.ToInt32(dr["AnalysisCategoryID"]);
                    cSite.Add(eSite);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cSite;
        }
        public static CSite GetListForUpdatePrice(int AnalysisCategoryID)
        {
            CSite cSite = new CSite();
            cSite.CategoryID = AnalysisCategoryID;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select st.* from tb_search_site st,tb_search_siteconfig sc where sc.CategoryIDS like '%'+convert(varchar(2),@AnalysisCategoryID)+'%' and st.siteid=sc.siteid and sc.teststatus=1 and isnull(UpdatePrice_Reg_GetPrice,'')<>''");
                db.AddInParameter(dbCommand, "@AnalysisCategoryID", DbType.Int32, AnalysisCategoryID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ESite eSite = new ESite(dr);
                    eSite.AnalysisCategoryID = AnalysisCategoryID;
                    cSite.Add(eSite);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            } 
            return cSite;
        }         
	}
}

