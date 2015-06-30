using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Entity.Sys;
using log4net;
namespace FZ.Spider.DAL.Data.Sys
{
    public class DLogging : DBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DLogging).FullName);

        public static List<ELogging> GetList(EQueryPage qe)
        {
            List<ELogging> logList = new List<ELogging>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystemLog);
                DbCommand dbCommand = db.GetSqlStringCommand(GetPageSql(qe));
                if (qe.IsTotal) db.AddOutParameter(dbCommand, "@TotalRecord", DbType.Int32, 4);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    logList.Add(new ELogging(dr));
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
               logger.Error(ex.Message,ex);
            }
            return logList;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static ELogging GetEntity(int logid)
        {
            ELogging elogging = new ELogging();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystemLog);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM tb_sys_logging WHERE [logid] = @logid");
                db.AddInParameter(dbCommand, "@logid", DbType.Int32, logid);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    elogging = new ELogging(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return elogging;
        }

        
    }     
}
