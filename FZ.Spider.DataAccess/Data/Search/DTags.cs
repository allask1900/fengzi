using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FZ.Spider.DAL.Entity.Search;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using log4net;
using FZ.Spider.DAL.Entity.Common;
namespace FZ.Spider.DAL.Data.Search
{
    public class DTags : DBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DTags).FullName);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int Add(ETags eTags)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_SEARCH_Tags (CategoryID,TagName,ShowType,Remark,Sort,IsValid) VALUES (@CategoryID,@TagName,@ShowType,@Remark,@Sort,@IsValid)  select @TagID=@@IDEntity");
                db.AddOutParameter(dbCommand, "@TagID", DbType.Int32,0);
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, eTags.CategoryID);
                db.AddInParameter(dbCommand, "@TagName", DbType.String, eTags.TagName);
                db.AddInParameter(dbCommand, "@ShowType", DbType.Int32, eTags.ShowType);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, eTags.Remark);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, eTags.Sort);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, eTags.IsValid);
                db.ExecuteNonQuery(dbCommand);
                int TagID = Convert.ToInt32(dbCommand.Parameters["@TagID"].Value);
                return TagID;
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
        public static bool Update(ETags eTags)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_SEARCH_Tags SET TagName = @TagName,CategoryID =@CategoryID,ShowType =@ShowType,IsValid =@IsValid,Remark =@Remark,Sort=@Sort,LastChangeTime=getdate()  WHERE TagID=@TagID");
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, eTags.TagID);
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, eTags.CategoryID);
                db.AddInParameter(dbCommand, "@TagName", DbType.String, eTags.TagName);
                db.AddInParameter(dbCommand, "@ShowType", DbType.Int32, eTags.ShowType);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, eTags.Remark);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, eTags.Sort);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, eTags.IsValid);
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
        public static bool Delete(int TagID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("delete from TB_SEARCH_Tags where TagID=@TagID");
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, TagID);
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
        public static ETags GetEntity(int TagID)
        {
            ETags eTags = new ETags();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_SEARCH_Tags where TagID=@TagID");
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, TagID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eTags = new ETags(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return eTags;
        }
        public static ETags GetEntity(string tagName, int CategoryID)
        {            
            ETags eTags = new ETags();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_SEARCH_Tags where CategoryID=@CategoryID and TagName=@TagName ");
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
                db.AddInParameter(dbCommand, "@TagName", DbType.String, tagName);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eTags = new ETags(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return eTags;
        }
        public static List<ETags> GetList()
        {
            List<ETags> tagList = new List<ETags>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_SEARCH_Tags");
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    tagList.Add(new ETags(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return tagList;
        }
        public static List<ETags> GetList(int CategoryID)
        {
            List<ETags> tagList = new List<ETags>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_SEARCH_Tags where categoryid=" + CategoryID + " or (ShowType=2 and CategoryID like '"+CategoryID+"%' )");
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    tagList.Add(new ETags(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return tagList;
        }
        public static List<ETags> GetTagList()
        {
            List<ETags> tagList = new List<ETags>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from tb_search_tags where isvalid=1 order by sort");
                IDataReader dr = db.ExecuteReader(dbCommand);
                ECategory ecategory = new ECategory();
                while (dr.Read())
                {
                    Database db_1 = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                    ETags eTags = new ETags(dr);
                    DbCommand dbCommand_1 = db_1.GetSqlStringCommand("select * from tb_search_tagvalues where TagID=" + eTags.TagID + " and isvalid=1  order by sort");
                    IDataReader dr_1 = db_1.ExecuteReader(dbCommand_1);
                    List<ETagValue> tagValueList=new List<ETagValue>();
                    while (dr_1.Read())
                    {
                        tagValueList.Add(new ETagValue(dr_1));
                    }
                    if (tagValueList.Count == 0)
                        continue;
                    eTags.tagValueList = tagValueList;
                    tagList.Add(eTags);
                    dr_1.Close();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return tagList;
        }
        public static List<ETags> GetList(EQueryPage qe)
        {
            List<ETags> tagList = new List<ETags>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(GetPageSql(qe));
                if (qe.IsTotal) db.AddOutParameter(dbCommand, "@TotalRecord", DbType.Int32, 4);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    tagList.Add(new ETags(dr));
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
            return tagList;
        }
        public static bool Exists(string tagName, int CategoryID)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("select count(1) from TB_SEARCH_Tags where CategoryID=@CategoryID and TagName=@TagName ");
            db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
            db.AddInParameter(dbCommand, "@TagName", DbType.String, tagName);
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value && Convert.ToInt32(ob) > 0)
                return true;
            return false;
        }

    }
}
