using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.Search.ProductQuery;
using System.Text;
namespace FZ.Spider.Web.Manage.SEM
{
    /// <summary>
    /// 词
    /// </summary>
    public partial class WordAdd : ManagePage
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
            BindSysCategory_1();
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4();
        }
        protected void BindAdCategory()
        {
            ddlAdCategory.DataSource = DAdCategory.GetList();
            ddlAdCategory.DataTextField  = "AdCategoryName";
            ddlAdCategory.DataValueField = "AdCategoryID";
            ddlAdCategory.DataBind();
            ddlAdCategory.Items.Insert(0, new ListItem("请选择广告系列...", "0"));
        } 
        protected void BindAdGroup()
        {
            ddlAdGroup.DataSource = DAdGroup.GetAdGroupList(CommonFun.StrToInt(ddlAdCategory.SelectedValue));
            ddlAdGroup.DataTextField  = "AdGroupName";
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

        protected void btnSave_Click(object sender, EventArgs e)
        { 

            int adID= CommonFun.StrToInt(ddlAdList.SelectedValue);

            if (adID == 0)
            {
                Alert("广告组不能为空!");
                return;
            } 
            string[] words = txtWords.Text.Trim().Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                string wordText = word.Trim();
                if (string.IsNullOrEmpty(wordText) || DWords.Exists(adID, wordText))
                {
                    continue;
                }

                if (!DWords.Add(wordText, adID))
                {
                    Alert("添加失败:" + word);
                    return;
                }
            }
            Cancel();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        protected void Cancel()
        {
            txtWords.Text = ""; 
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
            if (ddlAdList.SelectedValue != "0")
            {
                List<EWords> wordsList =DWords.GetWordList(CommonFun.StrToInt(ddlAdList.SelectedValue));
                StringBuilder sb = new StringBuilder("");
                foreach (EWords ew in wordsList)
                {
                    sb.AppendLine(ew.WordText);
                }
                this.txtWords.Text = sb.ToString();
            }
        }



        protected void ddlSysCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4();
        }

        protected void ddlSysCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_3();
            BindSysCategory_4();
        }

        protected void ddlSysCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_4();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int CategoryID = 0;
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")            
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);            
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")            
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            string keyword = txtSearchWord.Text.Trim();
            if (CategoryID==0||keyword == "")
                return;
            SearchCondition sc = new SearchCondition();
            sc.CategoryID = CategoryID;            
            sc.PageCurrent = 1;
            sc.PageSize = 40;
            sc.Word = keyword;
            List<EProduct> productList = SearchHandler.Search(sc);
            gvDataList.DataSource = productList;
            gvDataList.DataBind();
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
    }
}