using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using FZ.Spider.Common;

namespace FZ.Spider.DAL.Data.Common
{
    public class DCommon : DBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DCommon).FullName);
        /// <summary>
        /// 得到表的数据行数
        /// </summary>
        /// <returns></returns>
        public static int GetAllCount(string TableName, string strWhere, int CategoryID)
        {
            Database db = DatabaseFactory.CreateDatabase(GetDatabaseByCategory(CategoryID));
            DbCommand dbCommand = db.GetSqlStringCommand(" SELECT COUNT(1) from " + TableName +" "+ strWhere);
            dbCommand.CommandTimeout = 360000;
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value)
            {
                return Convert.ToInt32(ob);
            }
            else
            {
                return 0;
            }
        }

        public static DataTable GetDataTable(string Sql)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand(Sql);
            return db.ExecuteDataSet(dbCommand).Tables[0];
        }

        public static bool SetDefaultPic(int SiteID, string SiteName, int width, int height, int BitLength)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("PR_DefaultPic_SetParameter ");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                db.AddInParameter(dbCommand, "@SiteName", DbType.String, SiteName);
                db.AddInParameter(dbCommand, "@width", DbType.Int32, width);
                db.AddInParameter(dbCommand, "@height", DbType.Int32, height);
                db.AddInParameter(dbCommand, "@BitLength", DbType.Int32, BitLength);
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
        /// 返回格式 BitLength  width  height LastID,;
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public static int[] GetDefaultPicParameter(int SiteID)
        {
            int[] Pic = new int[4];
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("select * from tb_DefaultPic where SiteID=" + SiteID);
            IDataReader dr = db.ExecuteReader(dbCommand);
            while (dr.Read())
            {
                Pic[0] = (int)dr["BitLength"];
                Pic[1] = (int)dr["width"];
                Pic[2] = (int)dr["height"];
                Pic[3] = (int)dr["LastProductID"];
                break;
            }
            dr.Close();
            return Pic;
        }
        public static void DefaultPicProcessSetLastProductID(int SiteID, int LastProductID)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("update tb_defaultpic set LastProductID=" + LastProductID + " where SiteID=" + SiteID);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 将图书图片类型更改为 ImageType=0 and IsCreateInfoXml=0
        /// </summary>
        /// <param name="ProductID"></param>
        public static void ChangeImageType(int ProductID)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("update TB_Product set ImageType=0,IsCreateInfoXml=0 where ProductID=" + ProductID);
            dbCommand.CommandTimeout = 600000;
            db.ExecuteNonQuery(dbCommand);
        }

        public static float GetCategoryMiddlePrice(int CategoryID)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand(" select @AllCount=count(1)/2 from TB_SEARCH_Product where CategoryID like '" + CategoryID + "%' and IsValid=1 and MaxPrice>0  select max(MaxPrice) as Price from (select top (@AllCount) MaxPrice from TB_SEARCH_Product where CategoryID like '" + CategoryID + "%' and IsValid=1 and MaxPrice>0 order by MaxPrice) as t");
            db.AddOutParameter(dbCommand, "@AllCount", DbType.Int32, 0);
           
            object ob= db.ExecuteScalar(dbCommand);

            int allcount = 0;
            int.TryParse(dbCommand.Parameters["@AllCount"].Value.ToString(), out allcount);
            float middlePrice = 0;
            if (allcount < 10)
                return middlePrice;

            if (ob != null && ob != DBNull.Value)
                middlePrice = Convert.ToSingle(ob);
            return middlePrice;
        }


        public static bool BackupDatabase(string dbName)
        {
            if (Configuration.Configs.DatabaseBackupPath == string.Empty)
            {
                logger.Error("Config Key 'DatabaseBackupPath' is null ");
                return false;
            }
            string filePath = Configuration.Configs.DatabaseBackupPath + dbName+"_"+DateTime.Now.ToString("yyyyMMddHHmmss")+".bak";
            try
            {
                
                long startTime = DateTime.Now.Ticks;
                Database db ;
                if (dbName.ToLower() == "searchsystem")
                    db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                else
                    db = DatabaseFactory.CreateDatabase(Database_SearchSystemLog);

                DbCommand dbCommand = db.GetStoredProcCommand("PR_Common_BackupDB");
                dbCommand.CommandTimeout = 3600000;
                db.AddInParameter(dbCommand, "@filePath", DbType.String, filePath);
                db.ExecuteNonQuery(dbCommand);
                long endBackuptime = DateTime.Now.Ticks;
                logger.Info("backup database (" + dbName + "),times(" + (endBackuptime - startTime) / 10000000 + "s)"); 
                ZipHelper zip = new ZipHelper();
                zip.CompressFile(filePath, filePath + ".7z", true);
                logger.Info("CompressFile backupfile (" + filePath + "),times(" + (DateTime.Now.Ticks - endBackuptime) / 10000000 + "s)");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }
    }
}
