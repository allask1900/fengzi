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
using FZ.Spider.DAL.Entity.Search;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class ProductEditorType : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEditorType();              
                BindData();
            }
        }
        
        protected void BindEditorType()
        {
            List<EProductEditorType> list=DProductEditorType.GetList();
            this.ddlEditorType.DataTextField = "EditorTypeName";
            this.ddlEditorType.DataValueField = "EditorTypeID";
            this.ddlEditorType.DataSource = list;
            this.ddlEditorType.DataBind();
            this.ddlEditorType.Items.Insert(0, new ListItem("请选择", "0")); 
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
            string Conditions = string.Empty;

           
            qe.Conditions = Conditions;
            qe.Orderby = " EditorTypeID desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = " TB_search_productEditorType ";
            qe.TotalRecord = 0;            
            gvDataList.DataSource =DProductEditorType.GetList(qe);
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
            EProductEditorType eProductEditorType = new EProductEditorType();
            eProductEditorType.EditorTypeName = txtEditorTypeName.Text.Trim();
            eProductEditorType.Remark = txtRemark.Text.Trim();
            eProductEditorType.EditorTypeID = CommonFun.StrToInt(ddlEditorType.SelectedValue);
            eProductEditorType.IsValid = Convert.ToBoolean(ddlIsValid.SelectedValue);
            if (DProductEditorType.Exist(eProductEditorType))
            {
                Alert("名称已存在");
                return;
            }
            if (eProductEditorType.EditorTypeID > 0)
            
                DProductEditorType.Update(eProductEditorType);
            
            else
                DProductEditorType.Add(eProductEditorType);           
            Cancel();
            BindData(); 
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }        
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int EditorTypeID = CommonFun.StrToInt(row.Cells[0].Text);
            if (EditorTypeID > 0)
            {
                DProductEditorType.Delete(EditorTypeID); 
                BindData();
            }
        }       
        protected void Cancel()
        {
            txtEditorTypeName.Text = "";
            txtRemark.Text = "";
            DropDownSelectItem(ddlEditorType, "0");
            this.btnSave.Text = "添加";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void ddlEditorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEditorType.SelectedValue != "0")
            {
                EProductEditorType eProductEditorType = DProductEditorType.Entity(CommonFun.StrToInt(ddlEditorType.SelectedValue));
                txtEditorTypeName.Text = eProductEditorType.EditorTypeName;
                txtRemark.Text = eProductEditorType.Remark;
                DropDownSelectItem(ddlIsValid, Convert.ToInt32(eProductEditorType.IsValid).ToString());
            }
        }
    }
}