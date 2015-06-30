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
    public partial class SpiderTestConfig : FZ.Spider.Web.WebControl.ManagePage
    {
        protected ESite eSite =new ESite();
        protected ESiteConfig eSiteConfig = new ESiteConfig();
        protected ESiteCategory eSiteCategory = new ESiteCategory();            
        protected int OrdID = 0;   
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 1000;
            OrdID = CommonFun.GetQueryInt("OrdID");        
            eSiteConfig = DSiteConfig.GetEntity(OrdID);
            eSite = DSite.GetEntity(eSiteConfig.SiteID);
            this.litSite.Text = "<a href='http://"+eSite.SiteDomain+"' target='_brank'>"+eSite.SiteName+"</a>  测试模板ID:"+OrdID;
            if (!IsPostBack)
            {
                this.cbListCategoryList.DataSource = DCategory.GetList(eSiteConfig.CategoryIDS, eSiteConfig.SiteID);
                this.cbListCategoryList.DataTextField = "CategoryNameAndUrlCount";
                this.cbListCategoryList.DataValueField = "CategoryID";
                this.cbListCategoryList.DataBind();
                SetCheckBoxList(cbListCategoryList, eSiteConfig.CategoryIDS);
            } 
            if (eSiteConfig.TestStatus)
            {
                this.btnTestOk.Enabled = false;
            }
            else
            {
                this.btnTestFail.Enabled = false;
            } 
        }

        
        /// <summary>
        /// 测试成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTestOk_Click(object sender, EventArgs e)
        {
            DSiteConfig.UpdateTestStatus(OrdID, true);
        }
        /// <summary>
        /// 测试失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTestFail_Click(object sender, EventArgs e)
        {
            DSiteConfig.UpdateTestStatus(OrdID, false);
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            SpiderTest ftestSpider = new SpiderTest();
            ftestSpider.TestSite(OrdID);
            this.litTestLog.Text = ftestSpider.testLog.ToString();
        } 
    }
}