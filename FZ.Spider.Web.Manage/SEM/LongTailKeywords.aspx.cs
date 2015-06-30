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
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Data.Search; 
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class LongTailKeywords : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                BindCategory();
                BindAdCategory();
                BindData();
            }
        }
        protected void BindCategory()
        {
            BindSysCategory_1();
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4(); 
        }
        protected void BindAdCategory()
        {
            ddlAdCategory.DataSource = DAdCategory.GetList();
            ddlAdCategory.DataTextField = "AdCategoryName";
            ddlAdCategory.DataValueField = "AdCategoryID";
            ddlAdCategory.DataBind();
            ddlAdCategory.Items.Insert(0, new ListItem("选择广告系列", "0"));
        }

        protected void BindAdGroup()
        {
            ddlAdGroup.DataSource = DAdGroup.GetAdGroupList(CommonFun.StrToInt(ddlAdCategory.SelectedValue));
            ddlAdGroup.DataTextField = "AdGroupName";
            ddlAdGroup.DataValueField = "AdGroupID";
            ddlAdGroup.DataBind();
            ddlAdGroup.Items.Insert(0, new ListItem("选择广告组", "0"));
        }
        protected void BindAdList()
        {
            ddlAds.DataSource = DAds.GetAdsList(CommonFun.StrToInt(ddlAdGroup.SelectedValue));
            ddlAds.DataTextField = "AdTitle";
            ddlAds.DataValueField = "AdID";
            ddlAds.DataBind();
            ddlAds.Items.Insert(0, new ListItem("选择广告", "0"));
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

            if(CategoryID>0)
                sqlConditions.Append("and CategoryID=" + CategoryID);
            if(txtLikeLongTailKeywords.Text.Trim()!=string.Empty)
                sqlConditions.Append("and LongTailKeywords like '%" + txtLikeLongTailKeywords.Text.Trim() + "%'");
            if (txtLikeSourceKeywords.Text.Trim() != string.Empty)
                sqlConditions.Append("and SourceKeywords like '%" + txtLikeSourceKeywords.Text.Trim() + "%'");

            qe.Conditions = sqlConditions.ToString();
            qe.Orderby = " ordid desc ";
            qe.PageIndex    = pager.CurrentPageIndex;
            qe.Pagesize     = pager.PageSize;
            qe.Tablename = "  TB_SEM_LongTailKeywords ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DLongTailKeywords.GetList(qe);
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
            ELongTailKeywords eltk = new ELongTailKeywords();
            int CategoryID = 0;
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            eltk.CategoryID = CategoryID;
            eltk.SourceKeywords = txtSourceKeywords.Text.Trim();

            string[] LongTailKeywords =txtLongTailKeywords.Text.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ltkw in LongTailKeywords)
            {
                eltk.LongTailKeywords = ltkw.Trim();
                if (!DLongTailKeywords.Exist(eltk))
                    DLongTailKeywords.Add(eltk);                
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
                DLongTailKeywords.Delete(ordid); 
                BindData();
            }
        }       
        protected void Cancel()
        {
            txtSourceKeywords.Text = "";
            txtLongTailKeywords.Text = ""; 
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

       

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string scids = GetGridViewCheckBoxList(gvDataList, "ordid");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            }
            DLongTailKeywords.Delete(scids);
            BindData();
        }

        protected void btnAddToAds_Click(object sender, EventArgs e)
        {
            if (ddlAds.SelectedItem == null || ddlAds.SelectedValue == "0")
            {
                Alert("请选择广告");
                return;
            }
            int adid = CommonFun.StrToInt(ddlAds.SelectedValue);
            for (int i = 0; i < gvDataList.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvDataList.Rows[i].FindControl("ordid");
                if (chk.Checked)
                {
                    string words=gvDataList.Rows[i].Cells[3].Text.Trim();
                    if (string.IsNullOrEmpty(words))
                        continue;
                    if (!DWords.Exists(adid,words))
                        DWords.Add(words, adid);
                }
            }
        } 
        protected void ddlAdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdGroup();
        }

        protected void ddlAdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        } 
    }
}