using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class AdList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAdCategory();
                BindData();
            }
        }
        protected void BindAdCategory()
        {
            ddlAdCategory.DataSource = DAdCategory.GetList();
            ddlAdCategory.DataTextField = "AdCategoryName";
            ddlAdCategory.DataValueField = "AdCategoryID";
            ddlAdCategory.DataBind();
            ddlAdCategory.Items.Insert(0, new ListItem("请选择广告系列", "0"));
        }

        protected void BindAdGroup()
        {
            ddlAdGroup.DataSource = DAdGroup.GetAdGroupList(CommonFun.StrToInt(ddlAdCategory.SelectedValue));
            ddlAdGroup.DataTextField = "AdGroupName";
            ddlAdGroup.DataValueField = "AdGroupID";
            ddlAdGroup.DataBind();
            ddlAdGroup.Items.Insert(0, new ListItem("请选择广告组...", "0"));
        }

        protected void BindData()
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
            if(ddlAdCategory.SelectedValue!="0")
                qe.Conditions = " where AdGroupID=" + ddlAdGroup.SelectedValue + " ";
            qe.Orderby = " AdID desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_SEM_Ads ";
            qe.TotalRecord = 0;
            
            gvDataList.DataSource =DAds.GetAdsList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
            
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindData();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EAds eAds = new EAds();
            eAds.AdGroupID = CommonFun.StrToInt(ddlAdGroup.SelectedValue);
            if (eAds.AdGroupID == 0)
            {
                Alert("请选择广告组");
                return;
            }    
            eAds.AdID = CommonFun.StrToInt(litAdID.Text.Trim());
            eAds.AdTitle = txtAdTitle.Text.Trim();

            if (eAds.AdTitle == string.Empty)
            {
                Alert("标题不能为空");
                return;
            }

            eAds.Remark = txtRemark.Text.Trim(); 
            eAds.PageName = txtPageLink.Text.Trim();
            if (eAds.PageName == string.Empty)
                eAds.PageName = UrlHelper.ConvertUrlName(eAds.AdTitle);
            eAds.AdType = CommonFun.StrToInt(ddlAdType.SelectedValue); 
            

            if (DAds.Exist(eAds.AdTitle, eAds.AdID))
            {
                Alert("标题不能重复");
                return;
            }
            if (eAds.AdID != 0)
            {
                DAds.Update(eAds);
            }
            else
            {
                DAds.Add(eAds);
            }
            Cancel();
            BindData();               
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EAds eAds = (EAds)e.Row.DataItem;
                Literal lit = (Literal)e.Row.FindControl("litPageName");

                lit.Text = "<a href=\"" + Configuration.Configs.SiteDomain + "/best/" + eAds.PageName + ".html" + "\" target=_blank >" + eAds.PageName + "</a>";
                
            }
        }
        protected void gvDataList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int AdID =CommonFun.StrToInt(row.Cells[0].Text);
            if (AdID > 0)
            {
                litAdID.Text = AdID.ToString();
                EAds eAds = DAds.GetEntity(AdID);
                txtAdTitle.Text = eAds.AdTitle;
                txtRemark.Text = eAds.Remark; 
                txtPageLink.Text = eAds.PageName;
                DropDownSelectItem(ddlAdType, eAds.AdType.ToString());
                this.btnSave.Text = "修改";
            }
        } 
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int AdID = CommonFun.StrToInt(row.Cells[0].Text);
            if (AdID > 0)
            {
                int wordcount = DWords.GetAdWordCount(AdID);
                if (wordcount > 0)
                {
                    Alert("该广告组中有"+wordcount+"个词,不能删除!");
                    return;
                }
                DAds.Delete(AdID); 
                BindData();
            }
        }
        protected void gvDataList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDataList.EditIndex = e.NewEditIndex;
            BindData();
        }
        protected void gvDataList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDataList.EditIndex = -1;
            BindData();
        }

        protected void ddlAdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdGroup();
        }
        protected void Cancel()
        {
            txtRemark.Text = "";
            txtAdTitle.Text = "";
          
            txtPageLink.Text = "";
            litAdID.Text = ""; 
            DropDownSelectItem(ddlAdGroup, "0");
            DropDownSelectItem(ddlAdCategory, "0");
            this.btnSave.Text = "添加";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void ddlAdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}