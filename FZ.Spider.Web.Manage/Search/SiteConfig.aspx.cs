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
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.Common;
using System.Collections.Generic;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class SiteConfig :FZ.Spider.Web.WebControl.ManagePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFirstCategory(); 
            }
        }
        protected void btnAddApp_Click(object sender, EventArgs e)
        {
            if (dropSite.SelectedItem != null && dropSite.SelectedValue != "0")
            {
                ESiteConfig eSiteConfig = new ESiteConfig();
                eSiteConfig.OrdID =CommonFun.StrToInt(dropSiteConfig.SelectedValue);
                eSiteConfig.SiteID = int.Parse(dropSite.SelectedValue); 
                eSiteConfig.CategoryIDS =GetCheckBoxList(cbListCategoryList);
                if (eSiteConfig.CategoryIDS == string.Empty)
                {
                    Alert("未选分类");
                    return;
                }
                eSiteConfig.SpiderTemplet = txtSpiderTemplet.Text.Trim();
                eSiteConfig.OrdID = DSiteConfig.Add(eSiteConfig);
                BindSiteConfig();
                DropDownSelectItem(dropSiteConfig, eSiteConfig.OrdID.ToString());
                ESite eSite = DSite.GetEntity(int.Parse(dropSite.SelectedValue));
                this.labSiteDomain.Text = "<a href='http://" + eSite.SiteDomain + "' target='_blank'>" + eSite.SiteName + "</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href='SpiderTestConfig.aspx?ordid=" + eSiteConfig.OrdID + "' target='_blank'>测试</a>";                 
            }
        }
        protected void BindCategoryList()
        {
            this.cbListCategoryList.DataSource = DCategory.GetSiteFirstCategorys(int.Parse(dropSite.SelectedValue));
            this.cbListCategoryList.DataTextField = "CategoryNameAndUrlCount";
            this.cbListCategoryList.DataValueField = "CategoryID";
            this.cbListCategoryList.DataBind(); 
        }
        protected void BindSite()
        {
            List<ESite> siteList = DSite.GetList(Convert.ToInt32(dropFirstCategory.SelectedValue));//全部站点
            this.dropSite.DataSource = siteList;
            this.dropSite.DataTextField = "SiteName";
            this.dropSite.DataValueField = "SiteID";
            this.dropSite.DataBind();
            this.dropSite.Items.Insert(0, new ListItem("请选择", "0"));
        }
        protected void dropFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropSite.SelectedItem != null && dropSite.SelectedValue != "0")
            {
                BindSiteConfig();
            } 
        }
        protected void BindConfig()
        {
            ESiteConfig eSiteConfig = DSiteConfig.GetEntity(CommonFun.StrToInt(dropSiteConfig.SelectedValue));
            txtSpiderTemplet.Text = eSiteConfig.SpiderTemplet;            
            ESite eSite = DSite.GetEntity(int.Parse(dropSite.SelectedValue));
            string test = "测试";
            if (eSiteConfig.TestStatus) test = "已通过测试";
            this.labSiteDomain.Text = "<a href='http://" + eSite.SiteDomain + "' target='_blank'>" + eSite.SiteName + "</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href='SpiderTestConfig.aspx?ordid=" + eSiteConfig.OrdID + "' target='_blank'>" + test + "</a>";
            this.cbListCategoryList.Items.Clear();
            BindCategoryList();
            SetCheckBoxList(cbListCategoryList,eSiteConfig.CategoryIDS);
        }
        protected void dropSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSiteConfig();
            BindCategoryList();
        }
        protected void BindSiteConfig()
        {
            this.dropSiteConfig.DataSource = DSiteConfig.GetList(CommonFun.StrToInt(dropSite.SelectedValue));
            this.dropSiteConfig.DataTextField = "CategoryIDS";
            this.dropSiteConfig.DataValueField = "OrdID";
            this.dropSiteConfig.DataBind();
            this.dropSiteConfig.Items.Insert(0, new ListItem("选择模板", "0"));
        }
        
        protected void BindFirstCategory()
        { 
            this.dropFirstCategory.DataTextField = "CategoryName";
            this.dropFirstCategory.DataValueField = "CategoryID";
            this.dropFirstCategory.DataSource = DCategory.GetList(0, 0);
            this.dropFirstCategory.DataBind();
            this.dropFirstCategory.Items.Insert(0, new ListItem("请选择", "0"));
        }
        protected void dropFirstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSite();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        { 
            txtSpiderTemplet.Text = "";
        }

        protected void dropSiteConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindConfig();
        }
    }
}
