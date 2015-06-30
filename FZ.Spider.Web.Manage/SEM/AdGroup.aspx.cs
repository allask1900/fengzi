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
using FZ.Spider.DAL.Data.Search;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class AdGroup : ManagePage
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
                qe.Conditions = " where AdCategoryID=" + ddlAdCategory.SelectedValue + " ";
            qe.Orderby = " AdGroupID desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_SEM_AdGroup ";
            qe.TotalRecord = 0;
            
            gvDataList.DataSource =DAdGroup.GetAdGroupList(qe);
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
            EAdGroup eAdGroup = new EAdGroup();
            eAdGroup.AdCategoryID = CommonFun.StrToInt(ddlAdCategory.SelectedValue);
            if (eAdGroup.AdCategoryID==0)
            {
                Alert("请选择广告系列");
                return;
            }
            int CategoryID = DAdCategory.GetEntity(CommonFun.StrToInt(this.ddlAdCategory.SelectedValue)).CategoryID;
             
            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            eAdGroup.CategoryID = CategoryID;


            eAdGroup.AdGroupID = CommonFun.StrToInt(litAdGroupID.Text.Trim());
            eAdGroup.AdGroupName = txtAdGroupName.Text.Trim();
            eAdGroup.Remark = txtRemark.Text.Trim();
            if (eAdGroup.AdGroupName == string.Empty)
            {
                Alert("名称不能为空");
                return;
            }


            if (eAdGroup.AdGroupID != 0)
            {
                DAdGroup.Update(eAdGroup);
            }
            else
            {
                DAdGroup.Add(eAdGroup);
            }
            Cancel();
            BindData();               
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void gvDataList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int adGroupID =CommonFun.StrToInt(row.Cells[0].Text);
            if (adGroupID > 0)
            {
                litAdGroupID.Text = adGroupID.ToString();
                EAdGroup eAdGroup = DAdGroup.GetEntity(adGroupID);
                txtAdGroupName.Text = eAdGroup.AdGroupName;
                txtRemark.Text = eAdGroup.Remark;
                DropDownSelectItem(ddlAdCategory, eAdGroup.AdCategoryID.ToString());
                this.btnSave.Text = "修改";
            }
        } 
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int adGroupID = CommonFun.StrToInt(row.Cells[0].Text);
            if (adGroupID > 0)
            {
                int adcount = DAds.GetAdsCount(adGroupID);
                if (adcount > 0)
                {
                    Alert("该广告组中有" + adcount + "个广告,不能删除!");
                    return;
                }
                DAdGroup.Delete(adGroupID); 
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
            BindSysCategory_2();
            BindData();
        }

        protected void Cancel()
        {
            txtRemark.Text = "";
            txtAdGroupName.Text = "";
            litAdGroupID.Text = "";
           
            this.btnSave.Text = "添加";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void ddlSysCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_3(); 
        }

        protected void ddlSysCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_4(); 
        }
        protected void BindSysCategory_2()
        {
            int SysCategory_1_ID =DAdCategory.GetEntity(CommonFun.StrToInt(this.ddlAdCategory.SelectedValue)).CategoryID;
            if (SysCategory_1_ID >0)
            {
                this.ddlSysCategory_2.DataTextField = "CategoryName";
                this.ddlSysCategory_2.DataValueField = "CategoryID";
                this.ddlSysCategory_2.DataSource = DCategory.GetList(Convert.ToInt32(SysCategory_1_ID), 1);
                this.ddlSysCategory_2.DataBind();
                this.ddlSysCategory_2.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                this.ddlSysCategory_2.Items.Clear();
                this.ddlSysCategory_2.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindSysCategory_3()
        {
            string SysCategory_2_ID = this.ddlSysCategory_2.SelectedValue;
            if (SysCategory_2_ID != "0")
            {
                this.ddlSysCategory_3.DataTextField = "CategoryName";
                this.ddlSysCategory_3.DataValueField = "CategoryID";
                this.ddlSysCategory_3.DataSource = DCategory.GetList(Convert.ToInt32(SysCategory_2_ID), 2);
                this.ddlSysCategory_3.DataBind();
                this.ddlSysCategory_3.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                this.ddlSysCategory_3.Items.Clear();
                this.ddlSysCategory_3.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindSysCategory_4()
        {
            string SysCategory_3_ID = this.ddlSysCategory_3.SelectedValue;
            if (SysCategory_3_ID != "0")
            {
                this.ddlSysCategory_4.DataTextField = "CategoryName";
                this.ddlSysCategory_4.DataValueField = "CategoryID";
                this.ddlSysCategory_4.DataSource = DCategory.GetList(Convert.ToInt32(SysCategory_3_ID), 3);
                this.ddlSysCategory_4.DataBind();
                this.ddlSysCategory_4.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                this.ddlSysCategory_4.Items.Clear();
                this.ddlSysCategory_4.Items.Insert(0, new ListItem("请选择", "0"));
            }

        }
    }
}