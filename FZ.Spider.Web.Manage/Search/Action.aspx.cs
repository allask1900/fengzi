using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;


using FZ.Spider.DAL.Collection;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
using FZ.Spider.BS;
using System.Collections.Generic;
using FZ.Spider.Search.Rebuild;
using FZ.Spider.DAL.Data.Common;
using FZ.Spider.Configuration;
using System.Text;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class Action : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 36000;
        }  
        protected void btnGetAlexaRank_Click(object sender, EventArgs e)
        {
            UpdateAlexa(false);
        }

        protected void btnGetAlexaRankAndDesc_Click(object sender, EventArgs e)
        {
            UpdateAlexa(true);
        }
        protected void UpdateAlexa(bool updateDesc)
        {
            List<ESite> siteList = DSite.GetList(0);
            foreach (ESite es in siteList)
            {
                if (es.Rank == 0 || es.Rank == 99999999)
                 {
                    string desc = string.Empty;

                    es.Rank = AlexaHelper.GetAlexaRank(es.SiteDomain, ref desc);
                    if (updateDesc)
                    {
                        es.SiteDescription = desc;
                        DSite.UpdateRankAndDesc(es);
                    }
                    else
                    {
                        DSite.UpdateRank(es);
                    }
                }
            }
            Alert("完成");
        }
         
        protected void btnSetPriceTag_Click(object sender, EventArgs e)
        {
            List<ECategory> categoryList = DCategory.GetList(0, 1);
            foreach (ECategory ec in categoryList)
            {
                int tagid=0;
                if (!DTags.Exists("Price", ec.CategoryID))
                {
                    ETags eTags = new ETags();
                    eTags.CategoryID = ec.CategoryID;
                    eTags.IsValid = true;
                    eTags.Remark = "MiddlePrice";
                    eTags.ShowType = 2;
                    eTags.Sort = 1;
                    eTags.TagName = "Price";
                    tagid = DTags.Add(eTags);
                }
                else
                {
                    tagid = DTags.GetEntity("Price", ec.CategoryID).TagID;
                }
                DTagValue.DeleteByTagID(tagid);
                float MiddlePrice = DCommon.GetCategoryMiddlePrice(ec.CategoryID);
                if (MiddlePrice == 0)
                    continue;
                List<int[]> prices = CommonFun.PriceFilterRange(MiddlePrice);
                for (int i = 0; i < prices.Count; i++)
                {
                    ETagValue eTagValue = new ETagValue();
                    eTagValue.IsValid = true;
                    eTagValue.Sort = i;
                    eTagValue.TagID = tagid;                      
                    if (i == 0)
                    {
                        eTagValue.Remark = "price:Below-" + prices[i][1];
                        eTagValue.TagValue = "Below $" + prices[i][1];

                    }
                    else if(i==prices.Count-1)
                    {
                        eTagValue.Remark = "price:Above-" + prices[i][0];
                        eTagValue.TagValue = "Above $" + prices[i][0];
                    }
                    else
                    {
                        eTagValue.Remark = "price:" + prices[i][0] + "-" + prices[i][1];
                        eTagValue.TagValue = "$" + prices[i][0] + " - $" + prices[i][1];
                    }
                    DTagValue.Add(eTagValue);
                }
            }
        }

        protected void btnRebuildCache_Click(object sender, EventArgs e)
        {
            RequestActionHandler("RebuildCache");
        }

        protected void btnRefreshCache_Click(object sender, EventArgs e)
        {
            RequestActionHandler("RefreshCache");
        }

        protected void btnRebuildIndex_Click(object sender, EventArgs e)
        {
            RequestActionHandler("RebuildIndex");
        }

        protected void btnRebuildXml_Click(object sender, EventArgs e)
        {
            RequestActionHandler("RebuildXml");
        }

        protected void btnCreateXml_Click(object sender, EventArgs e)
        {
            RequestActionHandler("CreateXml");
        }
        public void RequestActionHandler(string action)
        {
            StringBuilder  sbUrl=new StringBuilder("");
            sbUrl.Append(Configs.SiteDomain);
            sbUrl.Append("/tools/actionhandler.aspx?action=");
            sbUrl.Append(action);
            sbUrl.Append("&key=");
            sbUrl.Append(CommonFun.RequestAuthorizationCode());
            string code = PageHelper.ReadUrl(sbUrl.ToString());
            Alert(code); 
        }

        protected void btnBackupDB_Click(object sender, EventArgs e)
        {
            DCommon.BackupDatabase("SearchSystem");
        }
    }
}
