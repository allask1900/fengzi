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
using FZ.Spider.DAL.Entity.Common; 
 
namespace FZ.Spider.Web.Manage.Search
{
    public partial class SiteCategoryConfig : FZ.Spider.Web.WebControl.ManagePage
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
            this.ddlCategory_1.DataSource = DCategory.GetList(0,0);
            this.ddlCategory_1.DataBind();
            this.ddlCategory_1.Items.Insert(0, "全部分类");
        }
       
        protected void ddlCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            BindSiteList();
        }

        protected void btnSiteCategoryUrl_Click(object sender, EventArgs e)
        {
            if (txtReg_GetSiteCategoryUrl.Text.Trim() == "")
            {
                Alert("提取分类的正则表达式集合不能为空");
                return;
            }
            if(litSiteID.Text=="")
            {
                 Alert("请选择站点");
                return;
            }
            if (txtRootCategoryUrl.Text.Trim() == "")
            {
                Alert("分类入口不能为空");
                return;
            }
            DSite.UpdateRegSiteCategory(CommonFun.StrToInt(litSiteID.Text), txtReg_GetSiteCategoryUrl.Text.Trim(),txtRootCategoryUrl.Text.Trim());
            Cancel();
            BindSiteList();
        }
        public void Cancel()
        {
             litSiteID.Text = "";
            litSiteName.Text = "";
            txtRootCategoryUrl.Text = "";
            txtReg_GetSiteCategoryUrl.Text = "";
        }
       
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindSiteList();
        }
        protected void gvDataList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int siteid = CommonFun.StrToInt(row.Cells[0].Text);
            if (siteid > 0)
            {
                ESite eSite = DSite.GetEntity(siteid);
                if(!string.IsNullOrEmpty(eSite.Reg_GetSiteCategoryUrl))
                {
                    txtReg_GetSiteCategoryUrl.Text = eSite.Reg_GetSiteCategoryUrl;
                }
                txtRootCategoryUrl.Text = eSite.RootCategoryUrl;
                litSiteID.Text = siteid.ToString();
                litSiteName.Text = eSite.SiteName;
            }
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void BindSiteList()
        {
            EQueryPage qe = new EQueryPage();
            qe.ResultColumns = " * ";
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            if (ddlCategory_1.SelectedValue != "0")
            {
                qe.Conditions = " where  categoryids like '%" + ddlCategory_1.SelectedValue + "%'";
            }
            qe.Orderby = " Rank ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_Search_Site ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DSite.GetList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        } 
    }
}