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
    public partial class SpiderWorkQueue : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSite();
                BindSpiderWorkQueue();
            }
        }
         
        protected void BindSite()
        {
            dropSite.DataSource = DSite.GetList(0,1);
            dropSite.DataTextField = "SiteName";
            dropSite.DataValueField = "SiteID";
            dropSite.DataBind();
            dropSite.Items.Insert(0, new ListItem("请选择", "0"));
          
        }

        protected void BindSpiderWorkQueue()
        {
            EQueryPage qe = new EQueryPage();
            qe.ResultColumns = " * ";
            qe.TempTableColumns = "swq.*,site.SiteName,cc.CategoryName ";
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            if (dropSite.SelectedValue != "0")
            {
                qe.Conditions = " where  swq.SiteID= " + dropSite.SelectedValue;
            }
            qe.Orderby = " OrdID  desc";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_Search_SpiderWorkQueue  as swq left join TB_SEARCH_Site as site on swq.SiteID=site.SiteID left join TB_Search_Category as cc on swq.AnalysisCategoryID=cc.CategoryID ";
            qe.TotalRecord = 0;
            gvDataList.DataSource =DSpiderWorkQueue.GetSpiderWorkQueue(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
        }
        
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindSpiderWorkQueue();
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
            SetCheckBoxList(cbListCategoryList, "");
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
               
            }            
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int ordid = CommonFun.StrToInt(row.Cells[0].Text);
            if (ordid > 0)
            {
                DSpiderWorkQueue.Delete(ordid);
                BindSpiderWorkQueue();
            }
        }

        protected void dropFirstCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSite();
        }
        public string GetStatusName(int status)
        {
            string name="";
            switch (status)
            {
                case 0:
                    name = "Check In";
                    break;
                case 1:
                    name = "Add Queue";
                    break;
                case 2:
                    name = "Running";
                    break;
                case 3:
                    name = "Completed";
                    break;
                default:
                    break;
            }
            return name;
        }
        protected void BindCategoryList()
        {
            this.cbListCategoryList.DataSource = DCategory.GetSiteFirstCategorys(int.Parse(dropSite.SelectedValue));
            this.cbListCategoryList.DataTextField = "CategoryName";
            this.cbListCategoryList.DataValueField = "CategoryID";
            this.cbListCategoryList.DataBind(); 
        }

        protected void dropSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCategoryList();
            BindSpiderWorkQueue();
        }

        protected void btnAddSite_Click(object sender, EventArgs e)
        {
            string[] analysisCategoryIDs = GetCheckBoxList(cbListCategoryList).Split(',');
            int siteid = CommonFun.StrToInt(dropSite.SelectedValue);
            foreach (string analysisCategoryID in analysisCategoryIDs)
            {
                int acid=CommonFun.StrToInt(analysisCategoryID);
                if(!DSpiderWorkQueue.Exists(siteid, acid))
                {
                    DSpiderWorkQueue.Add(siteid, acid);
                }
            }
            BindSpiderWorkQueue();
        }
    }
}
