using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZ.Spider.DAL.Entity.Search
{
    public class ETagValue
    {
        public int OrdID
        {
            get;
            set;
        }
        public int TagID
        {
            get;
            set;
        }
        public string TagName
        {
            get;
            set;
        } 
        public string TagValue
        {
            get;
            set;
        }
        public string Remark
        {
            get;
            set;
        }
        public bool IsValid
        {
            get;
            set;
        }
        public DateTime CreateTime
        {
            get;
            set;
        }
        public DateTime LastChangeTime
        {
            get;
            set;
        }
        public int Sort
        {
            get;
            set;
        }
        public ETagValue()
        {
        }
        public ETagValue(System.Data.IDataReader dr)
        {
            OrdID = (int)dr["OrdID"];
            TagID = (int)dr["TagID"]; 
            TagValue = dr["TagValue"].ToString();
            IsValid = Convert.ToBoolean(dr["IsValid"]);
            Remark = dr["Remark"].ToString();
            Sort = (int)dr["Sort"];
            CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            LastChangeTime = Convert.ToDateTime(dr["LastChangeTime"]);
        }
    }
}
