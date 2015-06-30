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
    public partial class SiteCategory : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 36000;
            if (!IsPostBack)
            {
                BindSysCategory_1();
                BindSite();
               
            }
            if (dropSite.SelectedItem != null && dropSite.SelectedValue != "0")
            {
                this.btnSpiderSiteCategory.Visible = true;
                this.btnReSpiderSiteCategory.Visible = true;
            }
            else
            {
                this.btnSpiderSiteCategory.Visible = false;
                this.btnReSpiderSiteCategory.Visible = false;
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
        protected void BindSysCategory_1()
        {
            List<ECategory> list = DCategory.GetList(0, 0);
            this.ddlSysCategory_1.DataTextField = "CategoryName";
            this.ddlSysCategory_1.DataValueField = "CategoryID";
            this.ddlSysCategory_1.DataSource = list;
            this.ddlSysCategory_1.DataBind();
            this.ddlSysCategory_1.Items.Insert(0, new ListItem("请选择", "0"));

            this.ddlFirstCategory.DataTextField = "CategoryName";
            this.ddlFirstCategory.DataValueField = "CategoryID";
            this.ddlFirstCategory.DataSource = list;
            this.ddlFirstCategory.DataBind();
            this.ddlFirstCategory.Items.Insert(0, new ListItem("请选择", "0"));    
        }         
        #endregion        
        
        public void BindList()
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
            qe.Conditions = "where siteid=" + dropSite.SelectedValue;
            string categoryid = "0";

            if(ddlSysCategory_1.SelectedItem!=null)
                categoryid=ddlSysCategory_1.SelectedValue;
            qe.Conditions = qe.Conditions + " and categoryid =" + categoryid;
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

        protected void ddlSysCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
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

        protected void btnSysCategory_Click(object sender, EventArgs e)
        {

            int CategoryID = 0;
            string categoryName = "";

            if (ddlFirstCategory.SelectedItem != null && ddlFirstCategory.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlFirstCategory.SelectedValue);
                categoryName = ddlFirstCategory.SelectedItem.Text;
            }

            

            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
                categoryName = ddlSysCategory_1.SelectedItem.Text;
            }

            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
                categoryName = categoryName + " << " + ddlSysCategory_2.SelectedItem.Text;
            }
            else
            {
                if (ddlFirstCategory.SelectedItem != null && ddlFirstCategory.SelectedValue != "0")
                {
                    CategoryID = Convert.ToInt32(ddlFirstCategory.SelectedValue);
                    categoryName = ddlFirstCategory.SelectedItem.Text;
                }
            }
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
                categoryName = categoryName+  " << " + ddlSysCategory_3.SelectedItem.Text;
            }
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
                categoryName =categoryName+ " << " + ddlSysCategory_4.SelectedItem.Text;
            }
            if (dropSite.SelectedValue == "0")
            {
                Alert("选择站点");
                return;
            }
            if (CategoryID == 0)
            {
                Alert("选择对应分类");
                return;
            }
            string scids = GetGridViewCheckBoxList(gvDataList, "SCID");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            }
            ESite esite = DSite.GetEntity(CommonFun.StrToInt(dropSite.SelectedValue));
            string firstCategory = CategoryHelper.GetCategoryIDByLevel(CategoryID, 0);
            if (esite.CategoryIDS.IndexOf(firstCategory) == -1)
            {
                DSite.UpdateCategoryIDS(esite.SiteDomain, esite.CategoryIDS + "," + firstCategory);
            }
            if (!DSiteCategory.UpdateSiteCategory(CategoryID, categoryName, scids))
            {
                Alert("更新失败");
                return;
            }
            BindList();
        }

        protected void btnSpiderSiteCategory_Click(object sender, EventArgs e)
        {
            if (dropSite.SelectedItem == null || dropSite.SelectedValue == "0")
            {
                Alert("抓取站点不能为空");
                return;
            }            
            SpiderSiteCategory();
        }

        protected void btnReSpiderSiteCategory_Click(object sender, EventArgs e)
        {
            if (dropSite.SelectedItem == null || dropSite.SelectedValue == "0")
            {
                Alert("重新抓取站点不能为空");
                return;
            }
            if (DCommon.GetAllCount("tb_search_sitecategory", " where categoryid>0 and siteid=" + dropSite.SelectedValue, 0) > 0)
            {
                Alert("已存已转换的分类，不能删除该站点所有分类");
                return;
            }
            DSiteCategory.DeleteBySite(CommonFun.StrToInt(dropSite.SelectedValue));
            SpiderSiteCategory();

        }
        protected void SpiderSiteCategory()
        {
            SpiderSiteCategory ssc=new SpiderSiteCategory(CommonFun.StrToInt(dropSite.SelectedValue));
            ssc.Run();
            Alert("抓取完成!");
        }

        protected void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string scids = GetGridViewCheckBoxList(gvDataList, "SCID");
            if (scids == "")
            {
                Alert("请选中分类");
                return;
            }
            DSiteCategory.DeleteByIDS(scids);
            BindList();
        } 
    }
}
