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
    public partial class Tags : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSysCategory_1(); 
            }
        }
        protected void BindTags()
        {
            EQueryPage qe = new EQueryPage();
            int CategoryID = 0;
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            }

            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            }
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            }
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            }
            if (CategoryID == 0)
            {
                Alert("选择对应分类");
                return;
            }

            qe.ResultColumns = " * ";
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            qe.Orderby = " TagName,Sort ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_Search_Tags ";
            qe.TotalRecord = 0;
            qe.Conditions = "CategoryID=" + CategoryID + " or (ShowType=2 and CategoryID like '" + CategoryID + "%' )";
            gvDataList.DataSource = DTags.GetList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
        }
        protected void btnAddSave_Click(object sender, EventArgs e)
        {
            ETags eTags = new ETags();
            eTags.TagName = txtTagName.Text.Trim();
            int CategoryID = 0;           
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            }

            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            }
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            }
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            }           
            if (CategoryID == 0)
            {
                Alert("选择对应分类");
                return;
            }
            eTags.CategoryID = CategoryID;
            if (eTags.CategoryID == 0)
            {
                Alert("未选分类");
                return;
            }
            eTags.IsValid = Convert.ToBoolean(dropIsValid.SelectedValue);
            eTags.Sort = CommonFun.StrToInt(txtSort.Text);
            eTags.Remark = txtRemark.Text;
            eTags.TagID = CommonFun.StrToInt(litTagID.Text);
            eTags.ShowType = CommonFun.StrToInt(dropShowType.SelectedValue);
            if (eTags.TagID > 0)
            {
                DTags.Update(eTags);
            }
            else
            {
                if (DTags.Exists(eTags.TagName,eTags.CategoryID))
                {
                    Alert("属性已存在");
                    return;
                }
                DTags.Add(eTags);
            }           
            Cancel();
            BindTags();
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindTags();
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
            txtRemark.Text ="";
            txtTagName.Text = "";
            txtSort.Text = "";
            litTagID.Text = "";
            btnAddSave.Text = "添加";
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
            int ordid = CommonFun.StrToInt(row.Cells[0].Text);
            if (ordid > 0)
            {
                ETags eTags = DTags.GetEntity(ordid);
                litTagID.Text = eTags.TagID.ToString();
                txtRemark.Text = eTags.Remark;
                txtTagName.Text = eTags.TagName;
                txtSort.Text = eTags.Sort.ToString();                
                DropDownSelectItem(dropIsValid, eTags.IsValid.ToString());
                DropDownSelectItem(dropShowType, eTags.ShowType.ToString());
                btnAddSave.Text= "修改";
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int tagid = CommonFun.StrToInt(row.Cells[0].Text);
            if (tagid > 0)
            {
                if (DTags.GetEntity(tagid).IsValid)
                {
                    Alert("有效标签,不能删除!");
                    return;
                }
                DTags.Delete(tagid);
                BindTags();
            }
        } 
        protected void ddlSysCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4();
            BindTags();
        }

        protected void ddlSysCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_3();
            BindSysCategory_4();
            BindTags();
        }

        protected void ddlSysCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_4();
            BindTags();
            
        }
        protected void ddlSysCategory_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTags();
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
        protected void BindSysCategory_2()
        {
            string SysCategory_1_ID = this.ddlSysCategory_1.SelectedValue;
            if (SysCategory_1_ID != "0")
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
