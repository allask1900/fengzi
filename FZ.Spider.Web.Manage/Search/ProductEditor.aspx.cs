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
    public partial class ProductEditor : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEditorType();
                BindCategory();
                BindData();
            }
        }
        protected void BindCategory()
        {
            this.dropFirstCategory.DataTextField = "CategoryName";
            this.dropFirstCategory.DataValueField = "CategoryID";
            List<ECategory> firstCategory = DCategory.GetList(0, 0);
            this.dropFirstCategory.DataSource = firstCategory;
            this.dropFirstCategory.DataBind();
            this.dropFirstCategory.Items.Insert(0, new ListItem("请选择", "0"));


            this.dropCategory.DataTextField = "CategoryName";
            this.dropCategory.DataValueField = "CategoryID";
            this.dropCategory.DataSource = firstCategory;
            this.dropCategory.DataBind();
            this.dropCategory.Items.Insert(0, new ListItem("请选择", "0"));   
        }
        protected void BindEditorType()
        {
            List<EProductEditorType> list=DProductEditorType.GetList();


            this.dropEditorType.DataTextField = "EditorTypeName";
            this.dropEditorType.DataValueField = "EditorTypeID";
            this.dropEditorType.DataSource = list;
            this.dropEditorType.DataBind();
            this.dropEditorType.Items.Insert(0, new ListItem("请选择", "0"));

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
            qe.TempTableColumns = " pe.*,pet.EditorTypeName,pro.FullName ";
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            string Conditions = string.Empty;

            if(dropCategory.SelectedValue!="0")
                Conditions = "  CategoryID=" + dropCategory.SelectedValue;
            if (dropEditorType.SelectedValue != "0")
            {
                if (Conditions != string.Empty)
                    Conditions = Conditions + " and ";
                Conditions = Conditions + " pe.EditorTypeID='" + dropEditorType.SelectedValue + "'";
            }
            qe.Conditions = Conditions;
            qe.Orderby = " ordid desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_search_productEditor as pe left join TB_search_productEditorType pet on pe.EditorTypeID=pet.EditorTypeID left join TB_search_product pro on pe.ProductID =pro.ProductID ";
            qe.TotalRecord = 0;            
            gvDataList.DataSource =DProductEditor.GetList(qe);
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
            EProductEditor eProductEditor = new EProductEditor();
            eProductEditor.CategoryID=CommonFun.StrToInt(dropFirstCategory.SelectedValue);
            eProductEditor.EditorTypeID =CommonFun.StrToInt( ddlEditorType.SelectedValue);
            if (eProductEditor.EditorTypeID ==0)
            {
                Alert("推荐类别不能为空");
                return;
            }
            string[] PIDS = txtProductIDS.Text.Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pid in PIDS)
            {
                string id = pid.Trim();
                if (StringHelper.IsNumberByStr(id))
                {
                    eProductEditor.ProductID = Convert.ToInt32(id);
                    if(!DProductEditor.Exist(eProductEditor))
                        DProductEditor.Add(eProductEditor);
                }
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
        
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int ordid = CommonFun.StrToInt(row.Cells[0].Text);
            if (ordid > 0)
            {
                DProductEditor.Delete(ordid); 
                BindData();
            }
        }
        
        
        protected void Cancel()
        { 
            txtProductIDS.Text = "";         
            DropDownSelectItem(dropFirstCategory, "0");
            this.btnSave.Text = "添加";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        protected void dropEditorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
           
        }

        protected void dropCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(); 
        }
    }
}