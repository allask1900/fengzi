using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FZ.Spider.DAL.Entity.Sys;
using FZ.Spider.DAL.Data.Sys;

using FZ.Spider.Common;
using FZ.Spider.Web.WebControl;
namespace FZ.Spider.Web.Manage.SystemConf
{
    public partial class SystemConfig : ManagePage
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
             BindSys();
             BindSysConf();

        }
        protected void BindSys()
        {
            this.ddlSystem.DataSource = DSystem.GetList();
            this.ddlSystem.DataTextField = "SysName";
            this.ddlSystem.DataValueField = "SysID";
            this.ddlSystem.DataBind();
            this.ddlSystem.Items.Insert(0, new ListItem("请选择系统", "0"));
        }
        
        
        protected void BindSysConf()
        {
            this.gvDataList.DataSource =DSystemConfig.GetList(CommonFun.StrToInt(this.ddlSystem.SelectedValue));
            this.gvDataList.DataBind();
            Cancel();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ESystemConfig sce = new ESystemConfig();
            sce.Description = txtDescription.Text.Trim();
            sce.ConfName = txtConfName.Text.Trim();
            sce.SysID = CommonFun.StrToInt(this.ddlSystem.SelectedValue);
            if (sce.SysID == 0)
            {
                Alert("请选择应用");
                return;
            }
            if (sce.ConfName == string.Empty)
            {
                Alert("配置单名称不能为空");
                return;
            }
            sce.Isvalid = Convert.ToBoolean(ddlStatus.SelectedValue);
            int ConfID = CommonFun.StrToInt(litConfID.Text);
            if (ConfID != 0)
            {
                sce.ConfID=ConfID;
                DSystemConfig.Update(sce);
                Cancel();
                
            }
            else
            {
                DSystemConfig.Add(sce);
                Cancel();
                
            } 
            BindSysConf();
        } 

        protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysConf();
        }
        #region dataview 生成行事件
        protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        #endregion

        #region 修改事件
        protected void gvDataList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string confid = this.gvDataList.Rows[e.RowIndex].Cells[0].Text;
            ESystemConfig sce = DSystemConfig.GetEntity(Convert.ToInt32(confid));
            if (sce.ConfID > 0)
            {
                litConfID.Text = sce.ConfID.ToString();
                txtDescription.Text = sce.Description;
                txtConfName.Text = sce.ConfName;
                DropDownSelectItem(this.ddlSystem, sce.SysID.ToString());
                DropDownSelectItem(this.ddlStatus, sce.Isvalid.ToString());
            }
            else
            {
                Alert("无法编辑!");
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            int confid = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            if (confid > 0)
            {
                if (!DSystemConfig.CheckConfDelete(confid))
                {
                    Alert("无法删除!");
                    return;
                }
                {
                    DSystemConfig.Delete(confid);
                    Cancel();
                    BindSysConf();
                }
            }

        }
        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            litConfID.Text = "";
            txtDescription.Text = "";
            txtConfName.Text = ""; 
        }
    }
}