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
using FZ.Spider.DAL.Entity.Sys;
using FZ.Spider.DAL.Data.Sys;
using System.Text;
namespace FZ.Spider.Web.Manage.Logging
{
    public partial class SysLogList : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindApplication();
                BindList();
            }
        }
        #region bindData
        protected void BindApplication()
        {
            List<EApplication> applicationList = DApplication.GetList(0);
            this.dropApplication.DataSource = applicationList;
            this.dropApplication.DataTextField = "AppName";
            this.dropApplication.DataValueField = "AppID";
            this.dropApplication.DataBind();
            this.dropApplication.Items.Insert(0, new ListItem("请选择应用", "0"));
             
        }
        
        #endregion


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pager.CurrentPageIndex = 1;
            BindList();
        }
        public void BindList()
        {
            int appid = CommonFun.StrToInt(dropApplication.SelectedValue);

            

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
            StringBuilder sbConditions=new StringBuilder();
            if (dropApplication.SelectedValue!="0")
               sbConditions.Append(" and appid = " + dropApplication.SelectedValue);
            if (dropLevel.SelectedValue != "ALL")
                sbConditions.Append(" and loglevel='" + dropLevel.SelectedValue+"'");
            if(txtSearchClass.Text.Trim()!="")
                sbConditions.Append(" and Class ='" + txtSearchClass.Text.Trim() + "'");
            if (txtSearchMethod.Text.Trim() != "")
                sbConditions.Append(" and Method ='" + txtSearchMethod.Text.Trim() + "'");

            if (txtSiteName.Text.Trim() != "")
                sbConditions.Append(" and SiteName ='" + txtSiteName.Text.Trim() + "'");

            if (txtFromTime.Text.Trim() != string.Empty)
            {
                DateTime dtFrom;
                if (DateTime.TryParse(txtFromTime.Text.Trim(), out dtFrom))
                {
                    sbConditions.Append(" and logdate>='" + dtFrom.ToString("yyyy-MM-dd") + "'");
                }
            }
            if (txtToTime.Text.Trim() != string.Empty)
            {
                DateTime dtTo;
                if (DateTime.TryParse(txtToTime.Text.Trim(), out dtTo))
                { 
                    sbConditions.Append(" and logdate<='" + dtTo.ToString("yyyy-MM-dd") + "'");
                }
            }
            qe.Conditions = sbConditions.ToString();            
            qe.Orderby = " logid desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  TB_SYS_Logging  with(nolock) ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DLogging.GetList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
        }

        
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ELogging elog = (ELogging)e.Row.DataItem;
                HyperLink hlf = (HyperLink)e.Row.Cells[7].FindControl("linkException");
                if (String.IsNullOrEmpty(elog.Exception))
                    hlf.Visible = false;
                else
                {
                    hlf.Visible = true; 
                }                        
            }
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindList();
        }        
    }
}
