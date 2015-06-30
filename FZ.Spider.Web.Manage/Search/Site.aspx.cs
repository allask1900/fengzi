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
using System.Collections.Generic;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class Site : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();                
            }
        }
        protected void BindData()
        {                 
            BindSite();
            List<ECategory> eCategoryList=DCategory.GetList(0, 0,false);
            this.cbListCategoryList.DataSource = eCategoryList;
            this.cbListCategoryList.DataTextField = "CategoryName";
            this.cbListCategoryList.DataValueField = "CategoryID";
            this.cbListCategoryList.DataBind();

            this.dropFirstCategory.DataSource = eCategoryList;
            this.dropFirstCategory.DataTextField = "CategoryName";
            this.dropFirstCategory.DataValueField = "CategoryID"; 
            this.dropFirstCategory.DataBind();
            this.dropFirstCategory.Items.Insert(0, new ListItem("请选择", "0"));
        }
       
        protected void BindSite()
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
           
            if (rblStatus.SelectedValue != "all")
            {

                if (rblStatus.SelectedValue == "notuse")
                    qe.Conditions = " where status<>2";
                else
                {
                    int status = CommonFun.StrToInt(rblStatus.SelectedValue);
                    qe.Conditions = " where status=" + status;
                }
            }
            if (dropFirstCategory.SelectedValue != "0")
            {
                if(string.IsNullOrEmpty(qe.Conditions))               
                    qe.Conditions =  " where  categoryids like '%" + dropFirstCategory.SelectedValue + "%'";
                else
                    qe.Conditions = qe.Conditions + " and  categoryids like '%" + dropFirstCategory.SelectedValue + "%'"; 
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
        protected void btnAddSite_Click(object sender, EventArgs e)
        {
            ESite eSite = new ESite();
            eSite.SiteName =txtSiteName.Text;
            eSite.SiteDomain = txtSiteDomain.Text; 
            eSite.CategoryIDS =GetCheckBoxList(cbListCategoryList);
            if (eSite.CategoryIDS == string.Empty)
            {
                Alert("未选分类");
                return;
            }
            eSite.CharSet = this.ddlCharSet.SelectedValue;
            eSite.SiteLogo = 1;
            eSite.SiteDescription = txtSiteDescription.Text;
            eSite.SiteProductInfoLevel =Convert.ToInt32(this.ddlLevel.SelectedValue);
            eSite.SpiderReadCount = CommonFun.StrToInt(txtSpiderReadCount.Text.Trim());
            eSite.SpiderSleepTime = CommonFun.StrToInt(txtSpiderSleepTime.Text.Trim());
            eSite.IsPartner = CommonFun.StrToInt(ddlIsPartner.SelectedValue);
            eSite.ProductSequency = CommonFun.StrToInt(txtProductSequency.Text.Trim());

           

            if (litSiteID.Text != null && litSiteID.Text != "")
            {
                eSite.SiteID = Convert.ToInt32(litSiteID.Text);
            }          

            if (eSite.SiteID > 0)
            {
                DSite.Update(eSite);
                //if (CheckBox1.Checked)
                //{
                //    FSiteShow.Update(eSite.SiteID, eSite.SiteName, eSite.SiteDomain, eSite.SiteDescription);
                //}
            }
            else
            {
                if (DSite.Exists(eSite.SiteDomain,eSite.SiteName))
                {
                    Alert("该网站已存在");
                    return;
                }
                eSite.SiteLogo = 1;
                int SiteID = DSite.Add(eSite);
                eSite.SiteID = SiteID;
                ////更新AllAsk_Show 数据库
                //if (CheckBox1.Checked)
                //{
                //    FSiteShow.Update(SiteID, eSite.SiteName, eSite.SiteDomain, eSite.SiteDescription);
                //}

            }
            //上传Logo
            if (txtSiteLogo.Text.Trim() != "")
            {
                Common.DownHelper.SaveBinaryFile(txtSiteLogo.Text.Trim(), eSite.SiteID);
            }
            else if (FileUpload1.FileName != "")
            {
                string strFileSavePath = UrlHelper.GetSiteLogoPath(eSite.SiteID);
                FileUpload1.SaveAs(strFileSavePath);
            }
            Cancel();
            BindSite();
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindSite();
        }
        
        

        protected void btnCheck_Click(object sender, EventArgs e)
        { 
            if (DSite.Exists(txtSiteDomain.Text.Trim(), txtSiteName.Text.Trim()))
            {
                Alert("该网站已存在");
                return;
            }
            else
            {
                Alert("可以添加");
                return;
            }
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            litSiteID.Text ="";
            txtSiteName.Text ="";
            txtSiteDomain.Text = "";
            txtSiteDescription.Text ="";
            btnAddSite.Text = "添加";
            txtSiteLogo.Text = "";
            txtSpiderReadCount.Text = "";
            txtSpiderSleepTime.Text = "";
            txtProductSequency.Text = "";
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ESite eSite = (ESite)e.Row.DataItem;
                Button use = (Button)e.Row.FindControl("btnStatusToUse");
                Button notuse = (Button)e.Row.FindControl("btnStatusToNotUse");
                if (eSite.Status == 1)
                {
                    use.Enabled = false;
                }
                if (eSite.Status == 2)
                {
                    notuse.Enabled = false;
                }
            }
        }
        protected void gvDataList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int siteid = CommonFun.StrToInt(row.Cells[0].Text);
            if (siteid > 0)
            {
                ESite eSite =DSite.GetEntity(siteid);
                litSiteID.Text = eSite.SiteID.ToString();
                txtSiteName.Text = eSite.SiteName;
                txtSiteDomain.Text = eSite.SiteDomain;
                txtSiteDescription.Text = eSite.SiteDescription;
                txtSpiderReadCount.Text = eSite.SpiderReadCount.ToString();
                txtSpiderSleepTime.Text = eSite.SpiderSleepTime.ToString();
                txtProductSequency.Text = eSite.ProductSequency.ToString();
                DropDownSelectItem(ddlCharSet, eSite.CharSet);
                DropDownSelectItem(ddlLevel, eSite.SiteProductInfoLevel.ToString());
                DropDownSelectItem(ddlIsPartner, eSite.IsPartner.ToString());
               
                SetCheckBoxList(cbListCategoryList, eSite.CategoryIDS);
                btnAddSite.Text = "修改";
            }            
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int siteid = CommonFun.StrToInt(row.Cells[0].Text);
            if (siteid > 0)
            {
                int siteCategoryCount =DSiteCategory.GetSiteCategory(siteid).Count;
                if (siteCategoryCount > 0)
                {
                    Alert("该网站存在站点入口,不能删除!");
                    return;
                }
                DSite.Delete(siteid);
                BindData();
            }
        }

        protected void dropFirstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSite();
        }

        protected void rblStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSite();
        }
        protected void btnStatusToUse_Click(object sender, EventArgs e)
        {
            int siteid = CommonFun.StrToInt(((Button)sender).CommandArgument.ToString());
            DSite.Update(siteid, 1);
            BindSite();
        }
        protected void btnStatusToNotUse_Click(object sender, EventArgs e)
        {
            int siteid = CommonFun.StrToInt(((Button)sender).CommandArgument.ToString());
            DSite.Update(siteid,2);
            BindSite();
        }
    }
}
