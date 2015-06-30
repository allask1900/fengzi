using System;
using System.Collections.Generic;
using System.Text;

namespace FZ.Spider.DAL.Entity.Common
{
    /// <summary>
    /// 查询条件实体
    /// </summary>
    public class EQueryPage
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Tablename;
        /// <summary>
        /// 条件
        /// </summary>
        public string Conditions;
        /// <summary>
        /// 主表字段
        /// </summary>
        public string ResultColumns;
        /// <summary>
        /// Row_Number生产临时表时需要查询的字段，通常用于 多表Join时使用
        /// </summary>
        public string TempTableColumns=string.Empty;
        /// <summary>
        /// 排序字段名称
        /// </summary>
        public string Orderby;
        /// <summary>
        /// 页大小
        /// </summary>
        public int Pagesize;
        /// <summary>
        /// 页码从1开始
        /// </summary>
        public int PageIndex;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecord;
        /// <summary>
        /// 是否返回总记录数
        /// </summary>
        public bool IsTotal;
    }
}
