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
 
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.Common;
using FZ.Spider.Web.WebControl;
using System.Collections.Generic;
using System.Text;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class Brand :  ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFirst();
            }
        }
 
        protected void BindFirst()
        {
            List<ECategory> categoryList = DCategory.GetList(1, 0);
            this.dropFirst.DataTextField = "CategoryName";
            this.dropFirst.DataValueField = "CategoryID";
            this.dropFirst.DataSource = categoryList;
            this.dropFirst.DataBind();
            this.dropFirst.Items.Insert(0, new ListItem("请选择", "0"));
        }
          
        #region SelectedIndexChanged
        protected void dropFirst_SelectedIndexChanged(object sender, EventArgs e)
        { 
            BindBrand();          
        }       
        #endregion

        protected void btnBrand_Click(object sender, EventArgs e)
        { 
            if (txtBrandName.Text == "")
            {
                Alert("名称不能为空");
                return;
            }             
            DBrand.Add(txtBrandName.Text);
            BindBrand();            
        }
        protected void BindBrand()
        {            
            int IsValid = -1;
            int IsCheck = -1;
 
            if (ddlIsCheck.SelectedValue != "-1")
            {
                IsCheck = Convert.ToInt32(ddlIsCheck.SelectedValue);
            }
            if (ddlIsValid.SelectedValue != "-1")
            {
                IsValid = Convert.ToInt32(ddlIsValid.SelectedValue);
            }
            StringBuilder sb = new StringBuilder("");
            if (IsValid > -1 || IsCheck > -1)
            {
                if(IsValid>-1)
                    sb.Append(" IsValid=" + IsValid);
                if (IsValid > -1)
                {
                    if (sb.Length > 0)
                        sb.Append(" and ");
                    sb.Append(" IsCheck=" + IsCheck);
                }
            }
            EQueryPage qe = new EQueryPage();
            qe.ResultColumns = " * ";
            qe.Conditions = sb.ToString();
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            qe.Orderby = " BrandName desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_Search_Brand ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DBrand.GetBrandList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 

        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindBrand();
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
            int brandid = CommonFun.StrToInt(((CheckBox)row.FindControl("BrandID")).Text);
            if (brandid > 0)
            { 
                string BrandName = ((TextBox)row.FindControl("BrandName")).Text.Trim();
                if (!DBrand.UpdateName(BrandName, brandid))
                {
                    Alert("编辑失败!");
                    return;
                }
                gvDataList.EditIndex = -1;
                BindBrand();
            }
        }
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int brandid = CommonFun.StrToInt(((CheckBox)row.FindControl("BrandID")).Text);
            if (brandid > 0)
            { 
                if (DBrand.GetEntity(brandid).IsValid)
                {
                    Alert("已启用品牌,不能删除!");
                    return;
                }
                DBrand.Delete(brandid);
                BindBrand();
            }
        }
        protected void gvDataList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDataList.EditIndex = e.NewEditIndex;
            BindBrand();
        }
        protected void gvDataList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDataList.EditIndex = -1;
            BindBrand();
        }
         

      
        protected void btnMerge_Click(object sender, EventArgs e)
        {
            int MergeID = Convert.ToInt32(txtMergeID.Text);
            int MergeToID = Convert.ToInt32(txtMergeToID.Text);
            DBrand.MergeBrand(MergeID, MergeToID);
        }
         
         

        protected void ddlIsValid_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBrand();
        }

        protected void ddlIsCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindBrand();
        }

        protected void btnIsValid_Click(object sender, EventArgs e)
        {
            string BrandIDS = GetGridViewCheckBoxList(gvDataList,"BrandID");
            DBrand.UpdateIsValid(BrandIDS, true);
            BindBrand();
        }

        protected void btnInvalid_Click(object sender, EventArgs e)
        {
            string BrandIDS = GetGridViewCheckBoxList(gvDataList, "BrandID");
            DBrand.UpdateIsValid(BrandIDS, false);
            BindBrand();
        }

        protected void btnIsCheck_Click(object sender, EventArgs e)
        {
            string BrandIDS = GetGridViewCheckBoxList(gvDataList, "BrandID");
            DBrand.UpdateIsCheck(BrandIDS, true);
            BindBrand();
        } 
    }
}
