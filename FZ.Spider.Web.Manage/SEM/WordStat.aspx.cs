using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class WordStat : ManagePage
    {
        int wordid = -1; 
        EAdGroup eAdGroup = new EAdGroup();
        EWords eWord = new EWords();
        EAds eAds = new EAds();
        protected void Page_Load(object sender, EventArgs e)
        {
            wordid = CommonFun.GetQueryInt("wordid");
            eWord = DWords.GetEntity(wordid);
            eAds = DAds.GetEntity(eWord.AdID);
            eAdGroup = DAdGroup.GetEntity(eAds.AdGroupID); 
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
            BindWords();
            BindStatData();
        }
        protected void BindAdCategory()
        {
            ddlAdCategory.DataSource = DAdCategory.GetList();
            ddlAdCategory.DataTextField = "AdCategoryName";
            ddlAdCategory.DataValueField = "AdCategoryID";
            ddlAdCategory.DataBind();
            ddlAdCategory.Items.Insert(0, new ListItem("请选择广告系列...", "0"));
            if (wordid > 0)
            {
                DropDownSelectItem(ddlAdCategory, eAdGroup.AdCategoryID.ToString());
                ddlAdCategory.Enabled = false;
            }
        }
        protected void BindAdGroup()
        {
            ddlAdGroup.DataSource = DAdGroup.GetAdGroupList(CommonFun.StrToInt(ddlAdCategory.SelectedValue));
            ddlAdGroup.DataTextField = "AdGroupName";
            ddlAdGroup.DataValueField = "AdGroupID";
            ddlAdGroup.DataBind();
            ddlAdGroup.Items.Insert(0, new ListItem("请选择广告组...", "0"));
            if (wordid > 0)
            {
                DropDownSelectItem(ddlAdGroup, eAdGroup.AdGroupID.ToString());
                ddlAdGroup.Enabled = false;
            }
        }
        protected void BindAdList()
        {
            ddlAdList.DataSource = DAds.GetAdsList(CommonFun.StrToInt(ddlAdGroup.SelectedValue));
            ddlAdList.DataTextField = "AdTitle";
            ddlAdList.DataValueField = "AdID";
            ddlAdList.DataBind();
            ddlAdList.Items.Insert(0, new ListItem("请选择广告...", "0"));
            if (wordid > 0)
            {
                DropDownSelectItem(ddlAdList, eAds.AdID.ToString());
                ddlAdGroup.Enabled = false;
            }
        }
        protected void BindWords()
        {
            if (ddlAdList.SelectedItem == null || ddlAdList.SelectedValue == "0")
                return;
            ddlWords.DataSource = DWords.GetWordList(CommonFun.StrToInt(ddlAdList.SelectedValue));
            ddlWords.DataTextField = "wordtext";
            ddlWords.DataValueField = "wordid";
            ddlWords.DataBind();
            ddlWords.Items.Insert(0, new ListItem("请选择关键词...", "0"));
            if (wordid > 0)
            {
                DropDownSelectItem(ddlWords, eWord.WordID.ToString());
                txtClickPrice.Text = eWord.ClickPrice.ToString();
                DropDownSelectItem(ddlStatus, eWord.Status.ToString());
                ddlWords.Enabled = false;
            }
        }
        protected void BindStatData()
        { 
             if (ddlWords.SelectedItem == null || ddlWords.SelectedValue == "0")
            { 
                return;
            }
            int wordid=CommonFun.StrToInt(ddlWords.SelectedValue);
            string fromDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            string toDay = DateTime.Now.ToString("yyyy-MM-dd");
            if (txtFromTime.Text.Trim() != string.Empty)
            {
                DateTime dtFrom;
                if (DateTime.TryParse(txtFromTime.Text.Trim(), out dtFrom))
                {
                   fromDay= dtFrom.ToString("yyyy-MM-dd");
                }
            }
            if (txtToTime.Text.Trim() != string.Empty)
            {
                DateTime dtTo;
                if (DateTime.TryParse(txtToTime.Text.Trim(), out dtTo))
                {
                    toDay = dtTo.ToString("yyyy-MM-dd");
                }
            }
            this.gvDataList.DataSource= DWordStat.GetWordPointStat(wordid, fromDay, toDay);
            this.gvDataList.DataBind();
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {            
            BindStatData();
        }

        protected void ddlAdCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdGroup();
        }

        protected void ddlAdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAdList();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlWords.SelectedItem == null || ddlWords.SelectedValue == "0")
            {
                Alert("请选择关键词");
                return;
            }
            if (!CommonFun.IsNumber(txtClickPrice.Text.Trim()))
            {
                Alert("价格必须为数值");
                return;
            }
            int wordid = CommonFun.StrToInt(ddlWords.SelectedValue);
            float price = Convert.ToSingle(txtClickPrice.Text.Trim());
            int status=CommonFun.StrToInt(ddlStatus.SelectedValue);
            DWords.Update(wordid, price, status);
        }

        protected void ddlWords_SelectedIndexChanged(object sender, EventArgs e)
        {
            eWord = DWords.GetEntity(CommonFun.StrToInt(ddlAdGroup.SelectedValue));
            txtClickPrice.Text = eWord.ClickPrice.ToString();
            DropDownSelectItem(ddlStatus, eWord.Status.ToString());
        }

        protected void ddlAdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindWords();
        }
    }
}