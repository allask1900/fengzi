using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Search;

using Microsoft.Practices.EnterpriseLibrary.Data;
using log4net;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Collection;

namespace FZ.Spider.DAL.Data.Search
{
	/// <summary>
	/// 数据访问类DBrand 。
	/// </summary>
	public class DSpiderWorkQueue:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DSpiderWorkQueue).FullName);
      
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static void Add(int SiteID,int AnalysisCategoryID)
        { 
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(" INSERT INTO TB_SEARCH_SpiderWorkQueue(SiteID,AnalysisCategoryID) VALUES (@SiteID,@AnalysisCategoryID)");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                db.AddInParameter(dbCommand, "@AnalysisCategoryID", DbType.Int32, AnalysisCategoryID);              
                db.ExecuteNonQuery(dbCommand);                
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex); 
            }
            
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static void Delete(int OrdID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(" delete from TB_SEARCH_SpiderWorkQueue where OrdID=@OrdID ");
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, OrdID);
                db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }

        }

        public static bool AddWorkQueue(int SiteID, int AnalysisCategoryID)
        {
            return UpdateWorkStatus(SiteID, AnalysisCategoryID, 1, "");
        }
        public static bool AddRuningQueue(int SiteID, int AnalysisCategoryID)
        {
            return UpdateWorkStatus(SiteID, AnalysisCategoryID, 2, "");
        }
        public static bool AddCompleteQueue(int SiteID, int AnalysisCategoryID, string statInfo)
        {
            return UpdateWorkStatus(SiteID, AnalysisCategoryID, 3, statInfo);
        }
        /// <summary>
        ///  更新一条数据(status:任务状态 0 创建，1 入队列， 2 开始分析，3 分析完成)
        /// </summary>
        public static bool UpdateWorkStatus(int SiteID,int AnalysisCategoryID,int status,string statInfo)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                string sql = "";
                if (status == 1)
                    sql = "UPDATE TB_SEARCH_SpiderWorkQueue SET status=1  WHERE SiteID= @SiteID and AnalysisCategoryID=@AnalysisCategoryID and Status=0 ";
                else if(status==2)
                    sql = "UPDATE TB_SEARCH_SpiderWorkQueue SET status=2,begintime=getdate() WHERE SiteID= @SiteID and AnalysisCategoryID=@AnalysisCategoryID and Status=1 ";
                else if (status == 3)
                    sql = "UPDATE TB_SEARCH_SpiderWorkQueue SET status=3,CompleteTime=getdate(),statInfo=@statInfo  WHERE SiteID= @SiteID and AnalysisCategoryID=@AnalysisCategoryID and Status=2 ";
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                db.AddInParameter(dbCommand, "@AnalysisCategoryID", DbType.Int32, AnalysisCategoryID);
                db.AddInParameter(dbCommand, "@statInfo", DbType.String, statInfo);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
        }

        public static List<ESpiderWorkQueue> GetSpiderWorkQueue(bool status)
        {
            List<ESpiderWorkQueue> cSpiderWorkQueue = new List<ESpiderWorkQueue>();
            StringBuilder sb = new StringBuilder("");
            string sql = "SELECT * FROM TB_Search_SpiderWorkQueue where status=@status order  by  ordid desc ";          
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "@status", DbType.Int32, status); 
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cSpiderWorkQueue.Add(new ESpiderWorkQueue(dr)); 
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return cSpiderWorkQueue;
        }
        
        
        /// <summary>
        /// 检查是否有相同任务在等待队列或分析队列
        /// </summary>
        /// <param name="BrandName"></param>
        /// <returns></returns>
        public static bool Exists(int SiteID,int AnalysisCategoryID)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand(" select OrdID from TB_Search_SpiderWorkQueue where SiteID=@SiteID and AnalysisCategoryID=@AnalysisCategoryID and status < 3 ");
            db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
            db.AddInParameter(dbCommand, "@AnalysisCategoryID", DbType.Int32, AnalysisCategoryID);
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value)
            {
                return true;//已存在
            }
            else
            {
                return false;
            }
        }

        public static List<ESpiderWorkQueue> GetSpiderWorkQueue(EQueryPage qe)
        {
            List<ESpiderWorkQueue> list = new List<ESpiderWorkQueue>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(GetPageSql(qe));
                if (qe.IsTotal) db.AddOutParameter(dbCommand, "@TotalRecord", DbType.Int32, 4);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ESpiderWorkQueue eswq = new ESpiderWorkQueue(dr);
                    eswq.SiteName = dr["SiteName"].ToString();
                    eswq.CategoryName = dr["CategoryName"].ToString();
                    list.Add(eswq);
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
            return list;
        }
	}
}