using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using FZ.Spider.DAL.Entity.Search;

using Microsoft.Practices.EnterpriseLibrary.Data;
using log4net;

namespace FZ.Spider.DAL.Data.Search
{
	/// <summary>
	/// 数据访问类DCategory 。
	/// </summary>
	public class DCategory:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DCategory).FullName);
		public DCategory()
		{
		}

		#region  基本数据操作方法
		/// <summary>
		///  增加一条数据
		/// </summary>
		public static int Add(ECategory ecategory)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                StringBuilder sb = new StringBuilder("");                    
                if (ecategory.CategoryLevel == 0)
                {
                    sb.Append("  select @CategoryID=isnull(max(categoryid),10)+1 from TB_Search_Category where CategoryLevel=0  ");
                    
                }
                else
                {
                    sb.Append("  select @CategoryID=isnull(max(categoryid)," + ecategory.ParentCategoryID + "*100)+1 from TB_Search_Category where CategoryLevel=@CategoryLevel and  categoryid like '" + ecategory.ParentCategoryID + "%'  ");
                    
                }
                sb.Append("  INSERT TB_Search_Category(CategoryID,CategoryName,CategoryLevel,HasChild)VALUES(@CategoryID,@CategoryName,@CategoryLevel,@HasChild) ");
                DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());                 
                db.AddInParameter(dbCommand, "@CategoryName", DbType.String, ecategory.CategoryName);
                db.AddInParameter(dbCommand, "@CategoryLevel", DbType.Int32, ecategory.CategoryLevel);
                db.AddInParameter(dbCommand, "@HasChild", DbType.Boolean, ecategory.HasChild);
                db.AddParameter(dbCommand, "@CategoryID", DbType.Int32, ParameterDirection.InputOutput, null, DataRowVersion.Default, 0);
                db.ExecuteNonQuery(dbCommand);
                return Convert.ToInt32(dbCommand.Parameters["@CategoryID"].Value);                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message,ex);
                return 0;
            }
		}
        public static int ExistsCategoryNameForAdd(ECategory ecategory)
        {
            int categoryID = 0;
             Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
             string sql = "select categoryid from TB_Search_Category where categoryname=@CategoryName and categorylevel=@CategoryLevel ";
             if (ecategory.ParentCategoryID > 0)
                 sql = sql + " and  categoryid  like '"+ecategory.ParentCategoryID+"%' ";
             DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@CategoryName", DbType.String, ecategory.CategoryName);
            db.AddInParameter(dbCommand, "@CategoryLevel", DbType.Int32, ecategory.CategoryLevel);
            object ob=db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value)
                categoryID=Convert.ToInt32(ob);
            return categoryID;
        }
        public static bool ExistsCategoryNameForUpdate(ECategory ecategory)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            string sql = " select categoryid from TB_Search_Category where categoryname=@CategoryName and categorylevel=@CategoryLevel  and categoryid<>@Categoryid";
            if (ecategory.ParentCategoryID > 0)
                sql = sql + " and categoryid like '" + ecategory.ParentCategoryID + "%'";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@CategoryName", DbType.String, ecategory.CategoryName);
            db.AddInParameter(dbCommand, "@CategoryLevel", DbType.Int32, ecategory.CategoryLevel);
            db.AddInParameter(dbCommand, "@Categoryid", DbType.Int32, ecategory.CategoryID);
            object ob = db.ExecuteScalar(dbCommand);
            if (ob != null && ob != DBNull.Value)
                return true;
            return false;
        }
		/// <summary>
		///  更新一条数据
		/// </summary>
		public static bool Update(ECategory ecategory)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("	UPDATE TB_Search_Category SET [CategoryName]=@CategoryName,[CategoryLevel]=@CategoryLevel,[HasChild]=@HasChild,LastChangeTime=getdate()	WHERE [CategoryID] = @CategoryID");
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, ecategory.CategoryID);
                db.AddInParameter(dbCommand, "@CategoryName", DbType.String, ecategory.CategoryName);
                db.AddInParameter(dbCommand, "@CategoryLevel", DbType.Int32, ecategory.CategoryLevel);
                db.AddInParameter(dbCommand, "@HasChild", DbType.Boolean, ecategory.HasChild);
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
        ///  更新第一分类的页面信息
        /// </summary>
        public static bool UpdateMetaInfo(ECategory ecategory)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Category SET [Description]=@Description,HotWord=@HotWord,KeyWord=@KeyWord,PageTitle=@PageTitle WHERE [CategoryID] = @CategoryID");
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, ecategory.CategoryID);
                db.AddInParameter(dbCommand, "@Description", DbType.String, ecategory.Description);
                db.AddInParameter(dbCommand, "@HotWord", DbType.String, ecategory.HotWord);
                db.AddInParameter(dbCommand, "@KeyWord", DbType.String, ecategory.KeyWord);
                db.AddInParameter(dbCommand, "@PageTitle", DbType.String, ecategory.PageTitle); 
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
		public static bool Delete(int CategoryID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_Search_Category WHERE [CategoryID] = @CategoryID");
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
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
		public static ECategory GetEntity(int CategoryID)
		{
			ECategory ecategory=new ECategory();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Category WHERE [CategoryID] = @CategoryID");
                db.AddInParameter(dbCommand, "@CategoryID", DbType.Int32, CategoryID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ecategory = new ECategory(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
			return ecategory;
		}
		#endregion
        /// <summary>
        /// 得到一级的 ParentID=0 Level=0
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static List<ECategory> GetList(int ParentCategoryID, int Level)
        {
            return GetList(ParentCategoryID,Level,true);
        }

        /// <summary>
        /// 得到一级的 ParentID=0 Level=0
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static List<ECategory> GetList(int ParentCategoryID, int Level,bool mustIsValid)
        {
            List<ECategory> cCategory = new List<ECategory>();
            StringBuilder sb = new StringBuilder("");
            string isvalid = string.Empty;
            if (mustIsValid)
                isvalid = " and IsValid=1 ";

            if (ParentCategoryID == 0)
            {
                sb.Append("SELECT * FROM TB_Search_Category WHERE CategoryLevel=@Level ");
                sb.Append(isvalid);
            }
            else
            {
                sb.Append("SELECT * FROM TB_Search_Category WHERE CategoryID like '" + ParentCategoryID + "%'  and CategoryLevel=@Level  ");
                sb.Append(isvalid);
                sb.Append("order by CategoryName");
            }
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString());
                db.AddInParameter(dbCommand, "@Level", DbType.Int32, Level);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cCategory.Add(new ECategory(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cCategory;
        }
        /// <summary>
        /// 得到包含有效爬虫模板的一级分类
        /// </summary>       
        /// <returns></returns>
        public static List<ECategory> GetListHasValidConfig()
        {
            List<ECategory> cCategory = new List<ECategory>();           
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);

                DbCommand dbCommand = db.GetSqlStringCommand("select Categoryids from tb_search_siteconfig where TestStatus=1");                
                IDataReader dr = db.ExecuteReader(dbCommand);
                Dictionary<string, string> hasValidConfigCategoryIDS = new Dictionary<string , string>();
                while (dr.Read())
                {
                    string[] ids = dr["Categoryids"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach(string id in ids)
                    {
                        if (!hasValidConfigCategoryIDS.ContainsKey(id))
                            hasValidConfigCategoryIDS.Add(id,id);
                    }
                }
                dr.Close();

                if (hasValidConfigCategoryIDS.Count > 0)
                {
                    StringBuilder ids = new StringBuilder("");
                    foreach (KeyValuePair<String, string> kv in hasValidConfigCategoryIDS)
                    {
                        ids.Append(kv.Key);
                        ids.Append(",");
                    }
                    ids = ids.Remove(ids.Length-1,1);
                    DbCommand dbCommand_1 = db.GetSqlStringCommand("SELECT * FROM TB_Search_Category WHERE CategoryLevel=0 and CategoryID in ("+ids.ToString()+")");
                    IDataReader dr_1 = db.ExecuteReader(dbCommand_1);                     
                    while (dr_1.Read())
                    {                       
                        cCategory.Add(new ECategory(dr_1));
                    }
                    dr_1.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cCategory;
        }
        public static List<ECategory> GetList()
        {
            List<ECategory> cCategory = new List<ECategory>();            
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Category where  IsValid=1 ");
               
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cCategory.Add(new ECategory(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cCategory;
        }
        /// <summary>
        /// 得到一级的 ParentID=0 Level=0 产品数 ProductCount>0
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static List<ECategory> GetListForMenu(int ParentID, int Level)
        {
            List<ECategory> cCategory = new List<ECategory>();
            string Sql = "SELECT * FROM TB_Search_Category WHERE CategoryID like '" + ParentID + "%' and CategoryLevel=" + Level;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(Sql);               
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cCategory.Add(new ECategory(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }           
            return cCategory;
        }
        /// <summary>
        ///   CategoryLevel=0 and productcount>0
        /// </summary>
        /// <returns></returns>
        public static List<ECategory> GetCategoryListForSiteMap()
        {
            List<ECategory> cCategory = new List<ECategory>(); 
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Category WHERE CategoryLevel=0 and productcount>0");
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cCategory.Add(new ECategory(dr));
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }           
            return cCategory;
        }     
        /// <summary>
        /// 根据站点得到该站点一级分类集合
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static List<ECategory> GetList(int SiteID)
        {
            List<ECategory> cCategory = new List<ECategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select categoryids from tb_search_site where siteid=@SiteID");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                object ob=db.ExecuteScalar(dbCommand);
                if (ob != null && ob != DBNull.Value)
                {
                    DbCommand dbCommand_1 = db.GetSqlStringCommand("SELECT * FROM TB_Search_Category WHERE CategoryID in (" + ob.ToString() + ")");

                    IDataReader dr = db.ExecuteReader(dbCommand_1);
                    while (dr.Read())
                    {
                        cCategory.Add(new ECategory(dr));
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            } 
            return cCategory;
        }
        public static List<ECategory> GetList(string categoryids, int SiteID)
        {
            List<ECategory> cCategory = new List<ECategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select CategoryID,CategoryName+'('+cast((select count(1) from tb_search_sitecategory where SiteID=@SiteID and categoryid like cast(c1.CategoryID as varchar(10))+'%') as varchar(5))+')' as CategoryNameAndUrlCount from TB_Search_Category as c1 where CategoryID in (" + categoryids + ")");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ECategory eCategory = new ECategory();
                    eCategory.CategoryID = (int)dr["CategoryID"];
                    eCategory.CategoryNameAndUrlCount = dr["CategoryNameAndUrlCount"].ToString();
                    cCategory.Add(eCategory);
                }
                dr.Close();             
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cCategory;
        }
        /// <summary>
        /// 一级分类页面所需信息(标题、关键字、简介)
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, ECategory> GetFirstCategoryInfo()
        {
            Dictionary<int, ECategory> dic = new Dictionary<int, ECategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Category WHERE CategoryLevel=0  and IsValid=1"); 
                IDataReader dr = db.ExecuteReader(dbCommand);
                ECategory ecategory = new ECategory();
                while (dr.Read())
                {
                    ecategory = new ECategory(dr);
                    dic.Add(ecategory.CategoryID, ecategory);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return dic;
        }
        /// <summary>
        /// FirstCategoryUrlCode = key
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, SCategory> GetCategoryDictionaryCodeKey()
        {
            Dictionary<string, SCategory> dic = new Dictionary<string, SCategory>();
            string sql = "SELECT CategoryID,CategoryName FROM TB_Search_Category where categorylevel=0 and IsValid=1";
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                IDataReader dr = db.ExecuteReader(dbCommand);
                ECategory ecategory = new ECategory();
                while (dr.Read())
                {
                    SCategory sCategory = new SCategory(Convert.ToInt32(dr["CategoryID"]), dr["CategoryName"].ToString(), dr["CategoryName"].ToString());
                    dic.Add(sCategory.FirstCategoryUrlCode.ToLower().Trim(), sCategory);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return dic;
        }
        /// <summary>
        /// categoryid = key
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, SCategory> GetCategoryDictionaryIDKey()
        {
            Dictionary<int, SCategory> dic = new Dictionary<int, SCategory>();
            string sql = "SELECT c1.CategoryID,c1.CategoryName,c2.CategoryName as FirstCategoryName FROM TB_Search_Category as c1 left join TB_Search_Category c2 on cast(substring(cast(c1.CategoryID as varchar(10)),1,2) as int)=c2.CategoryID  where c1.IsValid=1";
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                IDataReader dr = db.ExecuteReader(dbCommand);
                ECategory ecategory = new ECategory();
                while (dr.Read())
                {
                    SCategory sCategory = new SCategory(Convert.ToInt32(dr["CategoryID"]),dr["CategoryName"].ToString(), dr["FirstCategoryName"].ToString());
                    dic.Add(sCategory.CategoryID, sCategory);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return dic;
        }
        /// <summary>
        /// 得到站点所拥有的分类(用于后台)
        /// </summary>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public static List<ECategory> GetSiteFirstCategorys(int SiteID)
        {
            List<ECategory> cCategory = new List<ECategory>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("select categoryids from tb_search_site where siteid=@SiteID");
                db.AddInParameter(dbCommand, "@SiteID", DbType.Int32, SiteID);
                object ob = db.ExecuteScalar(dbCommand);
                if (ob != null && ob != DBNull.Value)
                {
                    DbCommand dbCommand_1 = db.GetSqlStringCommand("select CategoryID,CategoryName,CategoryName+'('+cast((select count(1) from tb_search_sitecategory where SiteID=@SiteID and categoryid like cast(c1.CategoryID as varchar(10))+'%') as varchar(5))+')' as CategoryNameAndUrlCount from TB_Search_Category as c1 where IsValid=1 and  CategoryID in (" + ob.ToString() + ")");
                    db.AddInParameter(dbCommand_1, "@SiteID", DbType.Int32, SiteID);
                    IDataReader dr = db.ExecuteReader(dbCommand_1);
                    while (dr.Read())
                    {
                        ECategory eCategory = new ECategory();
                        eCategory.CategoryID = (int)dr["CategoryID"];
                        eCategory.CategoryNameAndUrlCount = dr["CategoryNameAndUrlCount"].ToString();
                        eCategory.CategoryName = dr["CategoryName"].ToString();
                        cCategory.Add(eCategory);
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return cCategory;
        }
        public static int SelectCategory(string CategoryName, int Level, int ParentID)
        {
            int categoryid=0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("PR_Category_selectCategory");
                db.AddInParameter(dbCommand, "@ParentID", DbType.Int32, ParentID);
                db.AddInParameter(dbCommand, "@CategoryName", DbType.String, CategoryName);
                db.AddInParameter(dbCommand, "@Level", DbType.Int32, Level);
                object ob = db.ExecuteScalar(dbCommand);
                if (ob != null && ob !=DBNull.Value )
                {
                    categoryid = (int)ob;
                } 
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
            }
            return categoryid;
        } 
	}
}

