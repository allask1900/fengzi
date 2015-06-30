using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using FZ.Spider.DAL.Collection;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.Common;
using FZ.Spider.Spider;
 
namespace FZ.Spider.Web.Manage.Search
{
    public partial class TestSpider : FZ.Spider.Web.WebControl.ManagePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 1000;      
            if (!IsPostBack)
            {
                BindCategory();
            }
        }
        protected void BindCategory()
        {
            this.ddlCategory_1.DataTextField  = "CategoryName";
            this.ddlCategory_1.DataValueField = "CategoryID";
            this.ddlCategory_1.DataSource = DCategory.GetListHasValidConfig();
            this.ddlCategory_1.DataBind();
            this.ddlCategory_1.Items.Insert(0, "全部分类");
        }
        protected void BindSite()
        {
            this.ddlSite.DataTextField = "sitename";
            this.ddlSite.DataValueField = "siteid";
            this.ddlSite.DataSource = DSite.GetListForAnalysis(CommonFun.StrToInt(ddlCategory_1.SelectedValue));
            this.ddlSite.DataBind();
            this.ddlSite.Items.Insert(0, "全部站点");
        }
        protected void btnTest_Click(object sender, EventArgs e)
        {
            SpiderTest ftestSpider = new SpiderTest();
            int categoryid = CommonFun.StrToInt(ddlCategory_1.SelectedValue);
            int siteid=CommonFun.StrToInt(ddlSite.SelectedValue);
            if (categoryid==0)
                ftestSpider.TestAll();
            else if(categoryid!=0&&siteid==0)
                ftestSpider.TestCategory(DCategory.GetEntity(categoryid));
            if (categoryid!=0&&siteid != 0)
                ftestSpider.TestSite(siteid,categoryid);
            this.litTestLog.Text = ftestSpider.testLog.ToString();
        }

        protected void ddlCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSite();
        } 
    }
}