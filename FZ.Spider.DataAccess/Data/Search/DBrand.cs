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
	public class DBrand:DBase
	{
        private static ILog logger = LogManager.GetLogger(typeof(DBrand).FullName);
		public DBrand()
		{
		}

		#region  基本数据操作方法 
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public static int Add(string BrandName)
        {
            int brandID = 0;
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("	INSERT INTO TB_Search_Brand([BrandName])VALUES(@BrandName)	set @BrandID=@@IDENTITY	update TB_Search_Brand set IsValidBrandID=BrandID where BrandID=@BrandID");
                db.AddInParameter(dbCommand, "@BrandName", DbType.String, BrandName);
                db.AddOutParameter(dbCommand, "@BrandID", DbType.Int32,0);
                db.ExecuteNonQuery(dbCommand);
                if (dbCommand.Parameters["@BrandID"].Value != DBNull.Value)
                {
                    brandID= Convert.ToInt32(dbCommand.Parameters["@BrandID"].Value);
                } 
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex); 
            }
            return brandID; 
        } 
        /// <summary>
        ///  更新一条数据
        /// </summary>
        public static bool UpdateName(string BrandName,int BrandID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Brand SET [BrandName]=@BrandName,LastChangeTime=getdate() WHERE [BrandID] = @BrandID");
                db.AddInParameter(dbCommand, "@BrandName", DbType.String, BrandName);
                 db.AddInParameter(dbCommand, "@BrandID", DbType.Int32, BrandID); 
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
		public static bool Delete(int BrandID)
		{
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("DELETE TB_Search_Brand WHERE [BrandID] = @BrandID");
                db.AddInParameter(dbCommand, "@BrandID", DbType.Int32, BrandID);
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
		/// 得到一个对象实体
		/// </summary>
		public static EBrand GetEntity(int BrandID)
		{
			EBrand ebrand=new EBrand();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Brand  WHERE [BrandID] = @BrandID");
                db.AddInParameter(dbCommand, "@BrandID", DbType.Int32, BrandID);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    ebrand = new EBrand(dr);
                    break;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            } 
			return ebrand;
		}

		#endregion 
        public static bool UpdateIsValid(string BrandIDS, bool IsValid)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Brand SET IsValid= @IsValid,IsCheck=1 ,LastChangeTime=getdate() WHERE [BrandID] in ('" + BrandIDS + "')");
                db.AddInParameter(dbCommand, "@IsValid", DbType.Boolean, IsValid); 
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
        }

        public static bool UpdateIsCheck(string BrandIDS, bool IsCheck)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("UPDATE TB_Search_Brand SET IsCheck= @IsCheck ,LastChangeTime=getdate()  WHERE [BrandID] in ('" + BrandIDS + "')");
                db.AddInParameter(dbCommand, "@IsCheck", DbType.Boolean, IsCheck);
                db.AddInParameter(dbCommand, "@BrandIDS", DbType.String, BrandIDS);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
                return false;
            }
        }
        public static List<EBrand> GetCategoryBrand(int IsValid,int IsCheck)
        {
            List<EBrand> cbrand = new List<EBrand>();
            StringBuilder sb = new StringBuilder("");
            string sql = "SELECT * FROM TB_Search_Brand ";
            if(IsCheck!=-1) 
                sb.Append(" IsCheck=@IsCheck");
            if(IsValid!=-1)
            {
                if(sb.Length>9)
                    sb.Append(" and ");
                sb.Append(" IsValid=@IsValid");
            }
            if (sb.Length > 0)
                sql = sql +" where "+ sb.ToString() + " order by brandname ";
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);                
                db.AddInParameter(dbCommand, "@IsCheck", DbType.Int32, IsCheck);
                db.AddInParameter(dbCommand, "@BrandIDS", DbType.Int32, IsValid);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    cbrand.Add(new EBrand(dr)); 
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            } 
            return cbrand;
        }
        public static CBrand GetCBrand()
        {
            CBrand cBrand = new CBrand();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT * FROM TB_Search_Brand");
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    Brand brand=new Brand();
                    brand.BrandName=dr["Brandname"].ToString();
                    brand.BrandID = Convert.ToInt32(dr["BrandID"]);
                    brand.IsValidBrandID=Convert.ToInt32(dr["IsValidBrandID"]);
                    cBrand.Add(brand.BrandName.ToLower(), brand);
                }
                dr.Close();              
            }
            catch (Exception ex)
            {
               logger.Error(ex.Message,ex);
            }
            return cBrand;
        }
        
        /// <summary>
        /// 检查品牌名称是否已存在
        /// </summary>
        /// <param name="BrandName"></param>
        /// <returns></returns>
        public static bool Exists(string BrandName)
        {
            Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
            DbCommand dbCommand = db.GetSqlStringCommand(" select brandid from TB_Search_Brand where brandname=@brandName ");
            db.AddInParameter(dbCommand, "@BrandName", DbType.String, BrandName);
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
        /// <summary>
        /// 合并品牌
        /// </summary>
        /// <param name="MergeBrandID"></param>
        /// <param name="MergeToBrandID"></param>
        /// <returns></returns>
        public static bool MergeBrand(int MergeBrandID, int MergeToBrandID)
        {
            string sql = "UPDATE TB_Search_Product SET [BrandID]=@MergeToBrandID WHERE [BrandID] = @MergeBrandID   update TB_Search_Brand set IsValidbrandid=@MergeToBrandID ,IsCheck=1,IsValid=0 ,LastChangeTime=getdate() where IsValidbrandid=@MergeBrandID";
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                db.AddInParameter(dbCommand, "@MergeBrandID", DbType.Int32, MergeBrandID);
                db.AddInParameter(dbCommand, "@MergeToBrandID", DbType.Int32, MergeToBrandID);
                db.ExecuteNonQuery(dbCommand);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message, ex);
                return false;
            } 
        }
        public static Dictionary<int, string> GetIsVaildBrand()
        {
            Dictionary<int, string> IsVaildBrands = new Dictionary<int, string>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand("SELECT BrandName,BrandID FROM TB_Search_Brand WHERE  IsValid=1"); 
                IDataReader dr = db.ExecuteReader(dbCommand);                
                while (dr.Read())
                {
                    IsVaildBrands.Add((int)dr["brandid"], dr["brandname"].ToString());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message,ex);
            }
            return IsVaildBrands;
        }

        public static List<EBrand> GetBrandList(EQueryPage qe)
        {
            List<EBrand> brandList = new List<EBrand>();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(Database_SearchSystem);
                DbCommand dbCommand = db.GetSqlStringCommand(GetPageSql(qe));
                if (qe.IsTotal) db.AddOutParameter(dbCommand, "@TotalRecord", DbType.Int32, 4);
                IDataReader dr = db.ExecuteReader(dbCommand);
                while (dr.Read())
                {
                    brandList.Add(new EBrand(dr));
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
            return brandList;
        }
	}
}

