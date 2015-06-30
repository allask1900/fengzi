using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class WordEdit : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
        protected void BindData()
        {
            BindAdCategory();
            BindAdGroup();
            BindAdList();
        }
        protected void BindAdCategory()
        {
            ddlAdCategory.DataSource = DAdCategory.GetList();
            ddlAdCategory.DataTextField = "AdCategoryName";
            ddlAdCategory.DataValueField = "AdCategoryID";
            ddlAdCategory.DataBind();
            ddlAdCategory.Items.Insert(0, new ListItem("请选择广告系列...", "0"));
        }
        protected void BindAdGroup()
        {
            ddlAdGroup.DataSource = DAdGroup.GetAdGroupList(CommonFun.StrToInt(ddlAdCategory.SelectedValue));
            ddlAdGroup.DataTextField = "AdGroupName";
            ddlAdGroup.DataValueField = "AdGroupID";
            ddlAdGroup.DataBind();
            ddlAdGroup.Items.Insert(0, new ListItem("请选择广告组...", "0"));
        }
        protected void BindAdList()
        {
            ddlAdList.DataSource = DAds.GetAdsList(CommonFun.StrToInt(ddlAdGroup.SelectedValue));
            ddlAdList.DataTextField = "AdTitle";
            ddlAdList.DataValueField = "AdID";
            ddlAdList.DataBind();
            ddlAdList.Items.Insert(0, new ListItem("请选择广告...", "0"));
        }
        protected void BindWordList()
        {
            EQueryPage qe = new EQueryPage();
            qe.ResultColumns = " * ";
            StringBuilder sbCondition=new StringBuilder("");
            if (ddlAdList.SelectedItem == null || ddlAdList.SelectedValue == "0")
                return;
            sbCondition.Append(" AdID=" + ddlAdList.SelectedValue); 
                     
            if (txtKeyWord.Text.Trim() != string.Empty)
            { 
                sbCondition.Append(" and WordText like '%");
                sbCondition.Append(txtKeyWord.Text.Trim());
                sbCondition.Append("%'");
            }
            qe.Conditions = sbCondition.ToString();
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            qe.Orderby = " WordText ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = " tb_sem_words ";
            qe.TotalRecord = 0;
            gvDataList.DataSource =  DWords.GetWordList(qe);
            gvDataList.DataBind(); 
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
        }   
        

        
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EWords wse = (EWords)e.Row.DataItem;
                TextBox txtEditWordText = (TextBox)e.Row.FindControl("txtEditWordText");
                if (txtEditWordText != null)
                {
                    txtEditWordText.Text = wse.WordText.ToString();                            
                }                    
            }
        }
        protected void gvDataList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int WordID = CommonFun.StrToInt(row.Cells[0].Text);
            if (WordID > 0)
            {
                EWords we = new EWords();
                we.WordID = WordID;
                we.WordText = ((TextBox)row.FindControl("txtEditWordText")).Text.Trim();               
                if (!DWords.Update(we))
                {
                    Alert("编辑失败!");
                    return;
                }
                gvDataList.EditIndex = -1;
                BindWordList();
            }

        }
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int WordID = CommonFun.StrToInt(row.Cells[0].Text);
            if (WordID > 0)
            {
                EWords eWord=DWords.GetEntity(WordID);
                if (eWord.Status != 0||DAds.GetEntity(eWord.AdID).AdID>0)
                {
                    Alert("不能删除!");
                    return;
                }
                DWords.Delete(WordID);
            }
            BindWordList();
        }
        protected void gvDataList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvDataList.EditIndex = e.NewEditIndex;            
            BindWordList();
        }
        protected void gvDataList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvDataList.EditIndex = -1;
            BindWordList();
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindWordList();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            this.BindWordList();
        }

        protected void ddlAdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdGroup();
        }

        protected void ddlAdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdList();
        }

        protected void ddlAdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWordList();
        } 
    }
}