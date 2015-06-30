using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using log4net;
using FZ.Spider.DAL.Entity.Search;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using FZ.Spider.Common;
 

namespace FZ.Spider.DAL.Data.Search
{
    public class DSiteShow:DBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(DSiteShow).FullName);
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static bool UpdateToShow(int SiteID, string SiteName, string SiteUrl, string SiteDescription)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("if exists(select siteid from tb_search_SiteShow where SiteID=@SiteID) begin 		update tb_search_SiteShow set  iteName=@SiteName,SiteUrl=@SiteUrl,SiteDescription=@SiteDescription where SiteID=@SiteID 		end 	else 		insert tb_search_SiteShow (SiteID,SiteName,SiteUrl,SiteDescription) values (@SiteID,@SiteName,@SiteUrl,@SiteDescription)");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                db.AddInParameter(dbCommand, "@SiteName", DbType.String, SiteName);
                db.AddInParameter(dbCommand, "@SiteUrl", DbType.String, SiteUrl);
                db.AddInParameter(dbCommand, "@SiteDescription", DbType.String, SiteDescription);                
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            }
        }
        public static bool  AddCommentToShow(ESiteShow eSiteShow)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("update tb_search_SiteShow set Overall =Overall+ @Overall,Price = Price+@Price ,Purchase =Purchase+ @Purchase ,Service = Service+@Service ,Delivery = Delivery+@Delivery ,Shipping = Shipping+@Shipping,CommentCount=CommentCount+1,OverallCount=OverallCount+1	where  SiteID=@SiteID");

                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, eSiteShow.SiteID);
                db.AddInParameter(dbCommand, "@Overall", DbType.Int32, eSiteShow.Overall);
                db.AddInParameter(dbCommand, "@Price", DbType.Int32, eSiteShow.Price);
                db.AddInParameter(dbCommand, "@Purchase", DbType.Int32, eSiteShow.Purchase);
                db.AddInParameter(dbCommand, "@Service", DbType.Int32, eSiteShow.Service);
                db.AddInParameter(dbCommand, "@Delivery", DbType.Int32, eSiteShow.Delivery);
                db.AddInParameter(dbCommand, "@Shipping", DbType.Int32, eSiteShow.Shipping);
         
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            }       
        }
        public static bool AddOverallToShow(int SiteID, int Overall)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("update tb_search_SiteShow set Overall =Overall+ @Overall,OverallCount=OverallCount+1	where  SiteID=@SiteID");

                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                db.AddInParameter(dbCommand, "@Overall", DbType.Int32, Overall); 

                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            }
        }      
        public static bool AddShowCount(int SiteID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("update tb_search_SiteShow set showCount=showCount+1 where  SiteID=@SiteID");
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
        public static ESiteShow GetEntityForShow(int SiteID)
        {
            ESiteShow eSiteShow = new ESiteShow();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("update tb_search_SiteShow set ShowCount=ShowCount+1 where SiteID=@SiteID  select * from tb_search_SiteShow where  SiteID=@SiteID");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    eSiteShow = new ESiteShow(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                
            } 
            return eSiteShow;
        }
        public static Dictionary<int, string> GetAllSiteCommentCount()
        {
            Dictionary<int, string> AllSiteCommentCount = new Dictionary<int, string>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select siteid,Overall,OverallCount,commentcount from tb_search_SiteShow");
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    int SiteID = (int)dr["siteid"];
                    int commentcount = (int)dr["commentcount"];
                    StringBuilder CommentCount = new StringBuilder("");
                    CommentCount.Append(CommonFun.GetStarString((int)dr["Overall"], (int)dr["OverallCount"]));
                    CommentCount.Append("<br><a href=\"");
                    CommentCount.Append(UrlHelper.GetSiteCommentUrl(SiteID));
                    CommentCount.Append("\" target=\"_blank\">");
                    CommentCount.Append(commentcount.ToString());
                    CommentCount.Append("Review");
                    AllSiteCommentCount.Add(SiteID, CommentCount.ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return AllSiteCommentCount;
        }
    }
}