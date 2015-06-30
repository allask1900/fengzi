using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Search;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
 
namespace FZ.Spider.DAL.Data.Search
{
	/// <summary>
	/// 数据访问类DSiteComment 。
	/// </summary>
	public class DSiteComment:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DSiteComment).FullName);
		/// <summary>
		///  增加一条数据 返回 OrdID
		/// </summary>
		public static int  Add(ESiteComment esitecomment)
		{
            int ordid = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("INSERT INTO TB_SEARCH_SiteComment( SiteID, UserID, IP, Overall, Price, Purchase, Service, Delivery, Shipping, Title, Comment, CheckInTime) VALUES                      (@SiteID,@UserID,@IP,@Overall,@Price,@Purchase,@Service,@Delivery,@Shipping,@Title,@Comment,getdate())  set @OrdID=@@IDENTITY");
                db.AddOutParameter(dbCommand, "@OrdID", DbType.Int32,0);
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, esitecomment.SiteID);
                db.AddInParameter(dbCommand, "@UserID", DbType.Int32, esitecomment.UserID);
                db.AddInParameter(dbCommand, "@IP", DbType.String, esitecomment.IP);
                db.AddInParameter(dbCommand, "@Overall", DbType.Int32, esitecomment.Overall);
                db.AddInParameter(dbCommand, "@Price", DbType.Int32, esitecomment.Price);
                db.AddInParameter(dbCommand, "@Purchase", DbType.Int32, esitecomment.Purchase);
                db.AddInParameter(dbCommand, "@Service", DbType.Int32, esitecomment.Service);
                db.AddInParameter(dbCommand, "@Delivery", DbType.Int32, esitecomment.Delivery);
                db.AddInParameter(dbCommand, "@Shipping", DbType.Int32, esitecomment.Shipping);
                db.AddInParameter(dbCommand, "@Title", DbType.String, esitecomment.Title);
                db.AddInParameter(dbCommand, "@Comment", DbType.String, esitecomment.Comment);
                db.ExecuteNonQuery(dbCommand);

                ordid = Convert.ToInt32(dbCommand.Parameters["@OrdID"].Value);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);                
            }
            return ordid;
		}

        public static void AddCommentSupport(int OrdID, bool IsSupport)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                string sql = "update TB_SEARCH_SiteComment set Support=Support+1 where OrdID=@OrdID ";
                if (!IsSupport)
                       sql = "update TB_SEARCH_SiteComment set against=against+1 where OrdID=@OrdID ";

                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "@OrdID", DbType.Int32, OrdID);
                db.ExecuteNonQuery(dbCommand); 
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
        }
	}
}

