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

using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.Common;
using System.Collections.Generic;
using System.Text;
using FZ.Spider.DAL.Data.Common;
using FZ.Spider.BS;
namespace FZ.Spider.Web.Manage.Search
{
    public partial class SiteCategoryList : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 36000;
            if (!IsPostBack)
            { 
                BindSite();
                int siteid = CommonFun.GetQueryInt("siteid");
                if (siteid > 0)
                {
                    DropDownSelectItem(dropSite, siteid.ToString());
                    BindCategory_1();
                    BindList();
                }
            } 
        }
        #region bindData
        protected void BindSite()
        {
            List<ESite> siteList = DSite.GetList(0);
            this.dropSite.DataSource = siteList;
            this.dropSite.DataTextField =  "SiteName";
            this.dropSite.DataValueField = "SiteID";
            this.dropSite.DataBind();
            this.dropSite.Items.Insert(0, new ListItem("请选择", "0"));             
        }
        
        protected void BindCategory_1()
        {
            if (dropSite.SelectedItem != null && dropSite.SelectedValue != "0")
            { 
                this.dropCategory_1.DataTextField = "CategoryName";
                this.dropCategory_1.DataValueField = "CategoryID";
                this.dropCategory_1.DataSource = DCategory.GetList(Convert.ToInt32(dropSite.SelectedValue));
                this.dropCategory_1.DataBind();
                this.dropCategory_1.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindCategory_2()
        {
            string FirstID = this.dropCategory_1.SelectedValue;
            if (FirstID != null && FirstID != "0")
            { 
                this.dropCategory_2.DataTextField = "CategoryName";
                this.dropCategory_2.DataValueField = "CategoryID";
                List<ECategory> categoryList=DCategory.GetList(CommonFun.StrToInt(FirstID), 1);
                this.dropCategory_2.DataSource = categoryList;
                this.dropCategory_2.DataBind();
                this.dropCategory_2.Items.Insert(0, new ListItem("请选择", "0")); 
            } 
        }
        protected void BindCategory_3()
        {
            string ModuleID = this.dropCategory_2.SelectedValue;
            if (ModuleID != null && ModuleID != "0" && ModuleID != "")
            { 
                this.dropCategory_3.DataTextField = "CategoryName";
                this.dropCategory_3.DataValueField = "CategoryID";                 
                this.dropCategory_3.DataSource = DCategory.GetList(Convert.ToInt32(ModuleID), 2);
                this.dropCategory_3.DataBind();
                this.dropCategory_3.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void BindCategory_4()
        {
            string SubID = this.dropCategory_3.SelectedValue;
            if (SubID != null && SubID != "0" && SubID != "")
            { 
                this.dropCategory_4.DataTextField = "CategoryName";
                this.dropCategory_4.DataValueField = "CategoryID";
                this.dropCategory_4.DataSource = DCategory.GetList(Convert.ToInt32(SubID), 3);
                this.dropCategory_4.DataBind();
                this.dropCategory_4.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        #endregion
        #region SelectedIndexChanged
        protected void dropCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory_2();            
            BindList();
        }
        protected void dropCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory_3();
            BindList();
        }
        protected void dropCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategory_4();
            BindList();
        }
        protected void dropCategory_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }
        protected void dropFirstCategory_SelectedIndexChanged(object sender, EventArgs e)
        { 
            BindSite();       
        }
        #endregion

        protected void btnSiteCategory_Click(object sender, EventArgs e)
        {
            int CategoryID = 0;
            string scName ="";           
            if (dropCategory_1.SelectedItem!=null&& dropCategory_1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_1.SelectedValue);
                scName = dropCategory_1.SelectedItem.Text;
            }           
            if (dropCategory_2.SelectedItem != null && dropCategory_2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_2.SelectedValue);
                scName = scName+" << " + dropCategory_2.SelectedItem.Text;
            }            
            if (dropCategory_3.SelectedItem != null && dropCategory_3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_3.SelectedValue);
                scName = scName + " << " + dropCategory_3.SelectedItem.Text ;
            }
            if (dropCategory_4.SelectedItem != null && dropCategory_4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_4.SelectedValue);
                scName = scName + " << " + dropCategory_4.SelectedItem.Text;
            }
            if(CategoryID==0)
            {
                Alert("选择对应分类");
                return;
            }
            if(dropSite.SelectedValue=="0")
            {
                Alert("选择所属站点");
                return;
            }
            ESiteCategory eSiteCategory=new ESiteCategory();
            eSiteCategory.CategoryID=CategoryID;
            eSiteCategory.SCName = scName;
            eSiteCategory.CategoryName = scName;
            eSiteCategory.SiteID = Convert.ToInt32(dropSite.SelectedValue);
            string[] scurls = txtSCUrl.Text.Trim().Split(new string[] { "\r"," " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < scurls.Length; i++)
            {
                if (scurls[i].Trim() != "")
                {
                    eSiteCategory.SCUrl = scurls[i].Trim();
                    if (DSiteCategory.CheckSCUrl(eSiteCategory.SCUrl))
                    {
                        Alert("已存在该url");
                        continue;
                    }                    
                    DSiteCategory.Add(eSiteCategory);
                }
            }
            this.txtSCName.Text = "";
            this.txtSCUrl.Text = "";
            BindList();
        }
        public void BindList()
        {
            int CategoryID = 0;
            string categoryname = string.Empty;           
            if (dropCategory_1.SelectedItem != null && dropCategory_1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_1.SelectedValue);
                categoryname = dropCategory_1.SelectedItem.Text;
            }
           
            if (dropCategory_2.SelectedItem != null && dropCategory_2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_2.SelectedValue);
                categoryname =categoryname + " >> " + dropCategory_2.SelectedItem.Text;
            }
           
            if (dropCategory_3.SelectedItem != null && dropCategory_3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_3.SelectedValue);
                categoryname = categoryname+ " >> " +  dropCategory_3.SelectedItem.Text;
            }
            if (dropCategory_4.SelectedItem != null && dropCategory_4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(dropCategory_4.SelectedValue);
                categoryname = categoryname + " >> " + dropCategory_4.SelectedItem.Text;
            }
            if (dropSite.SelectedItem==null||dropSite.SelectedValue == "0")
            { 
                return;
            }
            if (categoryname != string.Empty)
            {
                this.txtSCName.Text = categoryname;
            }

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
            qe.Conditions = "where siteid=" + dropSite.SelectedValue;             
            qe.Conditions = qe.Conditions + " and categoryid like '" + CategoryID+"%'";            
            qe.Orderby = " SCName ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_Search_SiteCategory ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DSiteCategory.GetList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
        }

        protected void dropSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
            ESite eSite = DSite.GetEntity(Convert.ToInt32(dropSite.SelectedValue));
            string sitedomain =eSite.SiteDomain;
            this.labSiteDomain.Text = "<a href='http://" + sitedomain + "' target=_blank>" +sitedomain+"</a> ";
            BindCategory_1();
        }
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindList();
        }
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CheckBox cbox = (CheckBox)gvDataList.Rows[e.RowIndex].FindControl("SCID");
            int scid = CommonFun.StrToInt(cbox.Text);
            if (scid > 0)
            {
                DSiteCategory.Delete(scid);
            }
            BindList();
        }
            

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string scids = GetGridViewCheckBoxList(gvDataList, "SCID");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            }
            DSiteCategory.Delete(scids);
            BindList();
        } 
    }
}
