using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Data.Search;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class AdCategory : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSysCategory_1();
                BindData();
            }
        }
        protected void BindSysCategory_1()
        {
            List<ECategory> list = DCategory.GetList(0, 0);
            this.ddlSysCategory_1.DataTextField = "CategoryName";
            this.ddlSysCategory_1.DataValueField = "CategoryID";
            this.ddlSysCategory_1.DataSource = list;
            this.ddlSysCategory_1.DataBind();
            this.ddlSysCategory_1.Items.Insert(0, new ListItem("请选择", "0"));
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
            qe.Orderby = " AdCategoryID desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_SEM_AdCategory ";
            qe.TotalRecord = 0;
            gvDataList.DataSource =DAdCategory.GetAdCategoryList(qe);
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
            EAdCategory eAdCategory = new EAdCategory();
            eAdCategory.AdCategoryName =txtCategoryName.Text.Trim();
            if (eAdCategory.AdCategoryName == string.Empty)
            {
                Alert("类别名称不能为空");
                return;
            }
            eAdCategory.Remark = txtRemark.Text;
            eAdCategory.CategoryID = CommonFun.StrToInt(ddlSysCategory_1.SelectedValue);
            if (DAdCategory.Add(eAdCategory))
            {
                txtCategoryName.Text = "";
                txtRemark.Text = "";               
                BindData();
            }
            else
            {
                Alert("添加失败!");
                return;
            }            
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
            int adcategoryid = CommonFun.StrToInt(row.Cells[0].Text);
            if (adcategoryid > 0)
            {
                EAdCategory eAdCategory = new EAdCategory();
                eAdCategory.AdCategoryID = adcategoryid;
                eAdCategory.CategoryID = CommonFun.StrToInt(((TextBox)row.FindControl("txtEditCategoryID")).Text.Trim());
                eAdCategory.AdCategoryName = ((TextBox)row.FindControl("txtEditAdCategoryName")).Text.Trim();
                eAdCategory.Remark = ((TextBox)row.FindControl("txtEditRemark")).Text.Trim();
                if (!DAdCategory.Update(eAdCategory))
                {
                    Alert("编辑失败!");
                    return;
                }
                gvDataList.EditIndex = -1;
                BindData();
            }
        } 
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int adcategoryid = CommonFun.StrToInt(row.Cells[0].Text);
            if (adcategoryid > 0)
            {
                int adgroupcount = DAdGroup.GetAdGroupList(adcategoryid).Count;
                if (adgroupcount > 0)
                {
                    Alert("该系列中存在广告组,不能删除!");
                    return;
                }
                DAdCategory.Delete(adcategoryid);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCategoryName.Text = "";
            txtRemark.Text = "";
        }
    }
}