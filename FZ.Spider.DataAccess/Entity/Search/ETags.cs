using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZ.Spider.DAL.Entity.Search
{
    public class ETags
    {
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
        public int CategoryID
        {
            get;
            set;
        }
        /// <summary>
        /// 1 限定在本分类;  2 可以被子分类继承
        /// </summary>
        public int ShowType
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
        public List<ETagValue> tagValueList;
        public ETags()
        {
        }
        public ETags(System.Data.IDataReader dr)
        {
            TagID = (int)dr["TagID"];
            TagName = dr["TagName"].ToString();
            CategoryID = Convert.ToInt32(dr["CategoryID"]);
            ShowType = (int)dr["ShowType"];
            IsValid = Convert.ToBoolean(dr["IsValid"]);
            Remark = dr["Remark"].ToString();
            Sort = (int)dr["Sort"];
            CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            LastChangeTime = Convert.ToDateTime(dr["LastChangeTime"]);
        }
    }
}
