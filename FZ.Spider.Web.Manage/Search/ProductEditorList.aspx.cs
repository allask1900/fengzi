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
using FZ.Spider.DAL.Data.Common;
using FZ.Spider.Configuration;
using FZ.Spider.DAL.Data.Sys;
using FZ.Spider.BS;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class ProductEditorList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEditorType();
                BindCategory();
                txtRecommentProductConfigs.Text = DBConfig.GetValue(Configs.SysID.Manage, "search.manage.recommentproductspidersetting", "");
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
         
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        } 
        protected void btnSaveRecommentProductConfig_Click(object sender, EventArgs e)
        {
            DConfigItem.Update("search.manage.recommentproductspidersetting", txtRecommentProductConfigs.Text.Trim());
            txtRecommentProductConfigs.Text = string.Empty;
        }

        protected void btnGetCategoryRecommentProduct_Click(object sender, EventArgs e)
        {
            RecommentProduct rp = new RecommentProduct();
            gvDataList.DataSource = rp.GetRecommentProduct(CommonFun.StrToInt(dropFirstCategory.SelectedValue), ddlEditorType.SelectedItem.Text);
            gvDataList.DataBind();
        }

        protected void btnbtnGetAllCategoryRecommentProduct_Click(object sender, EventArgs e)
        {
            new RecommentProduct().Start();
            Alert("ok!");
        }

        protected void btnSysCategory_Click(object sender, EventArgs e)
        {
            EProductEditor eProductEditor = new EProductEditor();
            eProductEditor.CategoryID = CommonFun.StrToInt(dropFirstCategory.SelectedValue);
            eProductEditor.EditorTypeID = CommonFun.StrToInt(ddlEditorType.SelectedValue);
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvDataList.Rows[i].FindControl("ProductID");
                if (chk.Checked)
                {
                    eProductEditor.ProductID = CommonFun.StrToInt(chk.Text); ;
                    DProductEditor.Add(eProductEditor);
                }
            } 
        }
    }
}