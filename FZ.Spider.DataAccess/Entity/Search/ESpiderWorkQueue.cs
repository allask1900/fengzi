using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZ.Spider.DAL.Entity.Search
{
    public class ESpiderWorkQueue
    {
        public int OrdID
        {
            get;
            set;
        }
        public int SiteID
        {
            get;
            set;
        }
        public string SiteName
        {
            get;
            set;
        }
        public int AnalysisCategoryID
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
        public DateTime CheckInTime
        {
            get;
            set;
        }
        public DateTime BeginTime
        {
            get;
            set;
        }
        public DateTime CompleteTime
        {
            get;set;
        }
        /// <summary>
        /// 任务状态 0 创建，1 入队列， 2 开始分析，3 分析完成)
        /// </summary>
        public int Status
        {
            get;set;
        }
        public string StatInfo
        {
            get;
            set;
        }

        public ESpiderWorkQueue(System.Data.IDataReader dr)
        {
            if (!dr.IsDBNull(dr.GetOrdinal("OrdID"))) { OrdID = (int)dr["OrdID"]; }
            if (!dr.IsDBNull(dr.GetOrdinal("SiteID"))) { SiteID = (int)dr["SiteID"]; }
            if (!dr.IsDBNull(dr.GetOrdinal("AnalysisCategoryID"))) { AnalysisCategoryID = (int)dr["AnalysisCategoryID"]; }
            if (!dr.IsDBNull(dr.GetOrdinal("Status"))) { Status = Convert.ToInt32(dr["Status"]); }
            if (!dr.IsDBNull(dr.GetOrdinal("StatInfo"))) { StatInfo = dr["StatInfo"].ToString(); }

            if (!dr.IsDBNull(dr.GetOrdinal("CheckInTime"))) { CheckInTime = Convert.ToDateTime(dr["CheckInTime"]); }
            if (!dr.IsDBNull(dr.GetOrdinal("BeginTime"))) { BeginTime = Convert.ToDateTime(dr["BeginTime"]); }
            if (!dr.IsDBNull(dr.GetOrdinal("CompleteTime"))) { CompleteTime = Convert.ToDateTime(dr["CompleteTime"]); }           
        }
    }
}
