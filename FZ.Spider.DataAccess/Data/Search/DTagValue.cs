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
    public class DTagValue:DBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DTags).FullName);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool Add(ETagValue eTagValue)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_SEARCH_TagValues (TagID,TagValue,Remark,Sort,IsValid) VALUES (@TagID,@TagValue,@Remark,@Sort,@IsValid)");
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, eTagValue.TagID);
                db.AddInParameter(dbCommand, "@TagValue", DbType.String, eTagValue.TagValue);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, eTagValue.Remark);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, eTagValue.Sort);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, eTagValue.IsValid);
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
        public static bool Update(ETagValue eTagValue)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_SEARCH_TagValues SET TagID = @TagID,IsValid =@IsValid,TagValue=@TagValue,Remark =@Remark,Sort=@Sort,LastChangeTime=getdate()  WHERE OrdID=@OrdID");
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, eTagValue.OrdID);
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, eTagValue.TagID);
                db.AddInParameter(dbCommand, "@TagValue", DbType.String, eTagValue.TagValue);
                db.AddInParameter(dbCommand, "@Remark", DbType.String, eTagValue.Remark);
                db.AddInParameter(dbCommand, "@Sort", DbType.Int32, eTagValue.Sort);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, eTagValue.IsValid);
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
        public static bool Delete(int OrdID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("delete from TB_SEARCH_TagValues where OrdID=@OrdID");
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, OrdID);
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
        /// 删除单个Tag的所有value数据
        /// </summary>
        public static bool DeleteByTagID(int TagID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("delete from TB_SEARCH_TagValues where TagID=@TagID");
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
        public static ETagValue GetEntity(int OrdID)
        {
            ETagValue eTagValue = new ETagValue();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select * from TB_SEARCH_TagValues where OrdID=@OrdID");
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, OrdID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eTagValue = new ETagValue(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return eTagValue;
        }
        public static List<ETagValue> GetList(int TagID)
        {
            List<ETagValue> tagValueList = new List<ETagValue>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select tv.OrdID,tv.TagID,tv.TagValue,tv.Remark,tv.Sort,tv.IsValid,tv.CreateTime,tv.LastChangeTime from TB_SEARCH_TagValues as tv where TagID=@TagID");
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, TagID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    tagValueList.Add(new ETagValue(dr));
                }
                dr.Close();               
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return tagValueList;
        }

        public static List<ETagValue> GetList(int TagID,bool IsValid)
        {
            List<ETagValue> tagValueList = new List<ETagValue>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select tv.OrdID,tv.TagID,tv.TagValue,tv.Remark,tv.Sort,tv.IsValid,tv.CreateTime,tv.LastChangeTime from TB_SEARCH_TagValues as tv where TagID=@TagID and IsValid=@IsValid");
                db.AddInParameter(dbCommand, "@TagID", DbType.Int32, TagID);
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, IsValid);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    tagValueList.Add(new ETagValue(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return tagValueList;
        }      
       
        public static bool Exists(int tagid, string tagValue)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand("select count(1) from TB_SEARCH_TagValues where tagid=@tagid and TagValue=@TagValue");
            db.AddInParameter(dbCommand, "@tagid", DbType.Int32, tagid);
            db.AddInParameter(dbCommand, "@TagValue", DbType.String, tagValue);
            object ob=db.ExecuteScalar(dbCommand);
            if(ob!=null&&ob!=DBNull.Value&&Convert.ToInt32(ob)>0)
                return true;
            return false;
        }

    }
}
