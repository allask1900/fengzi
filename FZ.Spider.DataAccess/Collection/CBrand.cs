using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using FZ.Spider.DAL.Data.Search;

namespace FZ.Spider.DAL.Collection
{
    public class CBrand : Dictionary<string, Brand>
    {
        private static ILog logger = LogManager.GetLogger(typeof(CBrand).FullName);
        private object ob = new object();
        public int CheckBrand(string BrandName)
        {
            int BrandID = 0;
            string key = BrandName.ToLower();
            lock (ob)
            {
                try
                {
                    if (this.ContainsKey(key))
                    {
                        BrandID = this[key].BrandID;
                    }
                    else
                    {
                        BrandID = DBrand.Add(BrandName);
                        this.Add(key, new Brand(BrandName, BrandID, BrandID));
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message, ex);
                }
            }
            return BrandID;
        }
    }
    public class Brand
    {
        public string BrandName;
        public int BrandID;
        public int IsValidBrandID;
        public Brand(string brandname, int brandid, int isvalidbrandid)
        {
            BrandName = brandname;
            BrandID = brandid;
            IsValidBrandID = isvalidbrandid;
        }
        public Brand() { }
    }
}
