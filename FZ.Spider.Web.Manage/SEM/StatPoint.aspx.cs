using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
namespace FZ.Spider.Web.Manage.SEM
{
    public partial class StatPoint : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {  
                BindList();
            }
        }
        
        protected void BindList()
        {
            EQueryPage qe = new EQueryPage();
            qe.ResultColumns = " * ";

            StringBuilder sbCondition = new StringBuilder(""); 
            qe.Conditions = sbCondition.ToString();
            if (pager.CurrentPageIndex == 1)
            {
                qe.IsTotal = true;
            }
            else
            {
                qe.IsTotal = false;
            }
            qe.Orderby = " pointid desc ";
            qe.PageIndex = pager.CurrentPageIndex;
            qe.Pagesize = pager.PageSize;
            qe.Tablename = "  tb_sem_StatPoint ";
            qe.TotalRecord = 0;
            gvDataList.DataSource = DStatPoint.GetStatPointList(qe);
            gvDataList.DataBind();
            if (pager.CurrentPageIndex == 1) { pager.RecordCount = qe.TotalRecord; }
            pager.CustomInfoHTML = "总记录：<font color=\"blue\"><b>" + pager.RecordCount.ToString() + "</b></font>"; 
            
        }
        protected void pager_PageChanged(object sender, EventArgs e)
        {
            this.BindList();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EStatPoint eStatPoint = new EStatPoint(); 
            eStatPoint.PointName = txtPointName.Text.Trim();
            if (eStatPoint.PointName == string.Empty)
            {
                Alert("监测点名称不能为空");
                return;
            }
            eStatPoint.PointCode = txtPointCode.Text.Trim();
            eStatPoint.Remark = txtRemark.Text.Trim();
            int pointid =CommonFun.StrToInt(litPointID.Text.Trim());
            if (pointid > 0)
            {
                eStatPoint.PointID = pointid;
                DStatPoint.Update(eStatPoint);
            }
            else
                DStatPoint.Add(eStatPoint);                             
            BindList();            
            Cancel();
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
            int pointid = CommonFun.StrToInt(row.Cells[0].Text);
            if (pointid > 0)
            {
                EStatPoint eStatPoint =DStatPoint.GetEntity(pointid);
                txtPointCode.Text = eStatPoint.PointCode;
                txtPointName.Text = eStatPoint.PointName;
                txtRemark.Text = eStatPoint.Remark;
                
                litPointID.Text = pointid.ToString();
                this.btnSave.Text = "修改";
            }
        } 
        protected void gvDataList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int pointid = CommonFun.StrToInt(row.Cells[0].Text);
            if (pointid > 0)
            {
                DStatPoint.Delete(pointid);
                BindList();
            }
        }  
        protected void Cancel()
        {
            txtPointCode.Text = "";
            txtRemark.Text = "";
            txtPointName.Text = "";
            litPointID.Text = ""; 
            this.btnSave.Text = "添加";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
    }
}