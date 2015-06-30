using FZ.Spider.DAL.Entity.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace FZ.Spider.DAL.Data
{
    public class DBase
    {
        /// <summary>
        /// 主数据库
        /// </summary>
        protected readonly static string Database_SearchSystem = "SearchSystem";
        protected readonly static string Database_SearchSystemLog = "SearchSystemLog";
        protected static string GetDatabaseByCategory(int CategoryID)
        {
            return Database_SearchSystem;
        }
        /// <summary>
        /// 列表分页默认大小
        /// </summary>
        protected const int PageSize = 30;
        /// <summary>
        /// 分页Sql语句
        /// </summary>
        /// <param name="qe">查询实体</param>
        /// <returns></returns>
        protected static string GetPageSql(EQueryPage qe)
        {
            StringBuilder sbSql = new StringBuilder("");
            if (!string.IsNullOrEmpty(qe.Conditions))
            {
                qe.Conditions = qe.Conditions.Trim().ToLower();
                if (qe.Conditions.IndexOf("and ") == 0) qe.Conditions = qe.Conditions.Remove(0,3);
                if (qe.Conditions.IndexOf("where") != 0) qe.Conditions = " where " + qe.Conditions;
            }

            if (qe.IsTotal)
            {
                sbSql.Append("  SELECT @TotalRecord=count(*) from ");
                sbSql.Append(qe.Tablename);
                sbSql.Append(" ");
                if (!string.IsNullOrEmpty(qe.Conditions))
                { 
                    sbSql.Append(qe.Conditions);
                }

            }
            sbSql.Append("  SELECT "); 
            sbSql.Append(qe.ResultColumns);           
            sbSql.Append(" FROM (select ROW_NUMBER() Over( order by ");
            sbSql.Append(qe.Orderby);
            sbSql.Append(") as rowNum,");
            if(qe.TempTableColumns!=string.Empty)
                sbSql.Append(qe.TempTableColumns);
            else
                sbSql.Append(qe.ResultColumns);
            sbSql.Append(" from ");
            sbSql.Append(qe.Tablename);
            sbSql.Append(" ");
            if (!string.IsNullOrEmpty(qe.Conditions))
            { 
                sbSql.Append(qe.Conditions);
            }
            sbSql.Append(" ) as t where rowNum between ");
            int StartRecord = (qe.PageIndex - 1) * qe.Pagesize + 1;
            int EndRecord = StartRecord + qe.Pagesize - 1;
            sbSql.Append(StartRecord);
            sbSql.Append(" and ");
            sbSql.Append(EndRecord);
            return sbSql.ToString();
        }
    }
}
