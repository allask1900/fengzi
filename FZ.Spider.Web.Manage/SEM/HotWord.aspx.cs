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
    public partial class HotWord : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                BindCategory();
                BindData();
            }
        }
        protected void BindCategory()
        {
            BindSysCategory_1();
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4();

            BindDropDownList_1();
            BindDropDownList_2();
            BindDropDownList_3();
            BindDropDownList_4();
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
            string Conditions = " HotType=" + dropHotType.SelectedValue;
            int CategoryID = 0;
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);

            StringBuilder sqlConditions = new StringBuilder("");

            if (CategoryID > 0)
                sqlConditions.Append("and CategoryID=" + CategoryID);
            if (txtLikeWord.Text.Trim() != string.Empty)
                sqlConditions.Append("and word like '%" + txtLikeWord.Text.Trim() + "%'");
            if (txtLikeRemark.Text.Trim() != string.Empty)
                sqlConditions.Append("and remark like '%" + txtLikeRemark.Text.Trim() + "%'");
            qe.Conditions = sqlConditions.ToString(); 
            qe.Orderby = " ordid desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_search_HotWord ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DHotWord.GetList(qe);
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
            EHotWord eHotWord = new EHotWord();
            int CategoryID = 0;
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            eHotWord.CategoryID = CategoryID;
            eHotWord.HotType = CommonFun.StrToInt(dropHotType.SelectedValue);

            string[] Words = txtWords.Text.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string wd in Words)
            {
                 eHotWord.Word = wd.Trim();
                 if(!DHotWord.Exist(eHotWord))
                     DHotWord.Add(eHotWord.HotType, eHotWord.CategoryID, eHotWord.Word);
                
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

            CheckBox cbox = (CheckBox)gvDataList.Rows[e.RowIndex].FindControl("OrdID");
            int ordid = CommonFun.StrToInt(cbox.Text); 
            if (ordid > 0)
            {
                DHotWord.Delete(ordid); 
                BindData();
            }
        }
        
        
        protected void Cancel()
        { 
            txtWords.Text = "";
           
            DropDownSelectItem(dropHotType, "0");
            this.btnSave.Text = "添加";
        }
        protected void ddlSysCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4();
            BindData(); 
        }

        protected void ddlSysCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_3();
            BindSysCategory_4();
            BindData(); 
        }

        protected void ddlSysCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_4();
            BindData(); 
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        } 
        protected void ddlHotType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnSysCategory_Click(object sender, EventArgs e)
        {

            int CategoryID = 0;
            if (DropDownList1.SelectedItem != null && DropDownList1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(DropDownList1.SelectedValue);                
            }

            if (DropDownList2.SelectedItem != null && DropDownList2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(DropDownList2.SelectedValue);                
            }
            if (DropDownList3.SelectedItem != null && DropDownList3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(DropDownList3.SelectedValue);                
            }
            if (DropDownList4.SelectedItem != null && DropDownList4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(DropDownList4.SelectedValue);                
            }
             
            if (CategoryID == 0)
            {
                Alert("选择对应分类");
                return;
            }
            string scids = GetGridViewCheckBoxList(gvDataList, "ordid");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            } 
            
            if (!DHotWord.UpdateCategory(CategoryID,scids))
            {
                Alert("更新失败");
                return;
            }
            BindData();
        }

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string scids = GetGridViewCheckBoxList(gvDataList, "ordid");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            }
            DHotWord.Delete(scids);
            BindData();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDownList_2();
            BindDropDownList_3();
                BindDropDownList_4();
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDownList_3();
            BindDropDownList_4();
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        { 
            BindDropDownList_4();
        }
        protected void BindDropDownList_1()
        {
            List<ECategory> list = DCategory.GetList(0, 0,false);
            this.DropDownList1.DataTextField = "CategoryName";
            this.DropDownList1.DataValueField = "CategoryID";
            this.DropDownList1.DataSource = list;
            this.DropDownList1.DataBind();
            this.DropDownList1.Items.Insert(0, new ListItem("请选择", "0"));
        }
        protected void BindDropDownList_2()
        {
            string DropDownList_1_ID = this.DropDownList1.SelectedValue;
            if (DropDownList_1_ID != "0")
            {
                this.DropDownList2.DataTextField = "CategoryName";
                this.DropDownList2.DataValueField = "CategoryID";
                this.DropDownList2.DataSource = DCategory.GetList(Convert.ToInt32(DropDownList_1_ID), 1, false);
                this.DropDownList2.DataBind();
                this.DropDownList2.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                this.DropDownList2.Items.Clear();
                this.DropDownList2.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindDropDownList_3()
        {
            string DropDownList_2_ID = this.DropDownList2.SelectedValue;
            if (DropDownList_2_ID != "0")
            {
                this.DropDownList3.DataTextField = "CategoryName";
                this.DropDownList3.DataValueField = "CategoryID";
                this.DropDownList3.DataSource = DCategory.GetList(Convert.ToInt32(DropDownList_2_ID), 2, false);
                this.DropDownList3.DataBind();
                this.DropDownList3.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                this.DropDownList3.Items.Clear();
                this.DropDownList3.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindDropDownList_4()
        {
            string DropDownList_3_ID = this.DropDownList3.SelectedValue;
            if (DropDownList_3_ID != "0")
            {
                this.DropDownList4.DataTextField = "CategoryName";
                this.DropDownList4.DataValueField = "CategoryID";
                this.DropDownList4.DataSource = DCategory.GetList(Convert.ToInt32(DropDownList_3_ID), 3, false);
                this.DropDownList4.DataBind();
                this.DropDownList4.Items.Insert(0, new ListItem("请选择", "0"));
            }
            else
            {
                this.DropDownList4.Items.Clear();
                this.DropDownList4.Items.Insert(0, new ListItem("请选择", "0"));
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
    }
}