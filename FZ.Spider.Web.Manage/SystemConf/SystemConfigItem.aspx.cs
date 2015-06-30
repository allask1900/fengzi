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
    public partial class SystemConfigItem : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                BindSys();
                BindSysConf();
            }
        }
        protected void BindSys()
        {
            this.ddlSystem.DataSource = DSystem.GetList();
            this.ddlSystem.DataTextField = "SysName";
            this.ddlSystem.DataValueField = "SysID";
            this.ddlSystem.DataBind();
            this.ddlSystem.Items.Insert(0, new ListItem("选择应用...", "0"));
            
        }
        protected void BindSysConf()
        {
            this.ddlSysConf.DataSource = DSystemConfig.GetList(CommonFun.StrToInt(this.ddlSystem.SelectedValue));
            this.ddlSysConf.DataTextField = "ConfName";
            this.ddlSysConf.DataValueField = "ConfID";
            this.ddlSysConf.DataBind();
            this.ddlSysConf.Items.Insert(0, new ListItem("请选择配置单", "0"));
            
        }
        protected void BindList()
        {
            this.gvDataList.DataSource = DConfigItem.GetList(CommonFun.StrToInt(this.ddlSysConf.SelectedValue));
            this.gvDataList.DataBind();
            Cancel();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            EConfigItem ce = new EConfigItem();
            ce.Description = txtDescription.Text.Trim();
            ce.ConfID = CommonFun.StrToInt(this.ddlSysConf.SelectedValue);
            ce.SysID = CommonFun.StrToInt(this.ddlSystem.SelectedValue);
            ce.KeyName = txtConfKey.Text.Trim();
            ce.Value =  txtConfValue.Text.Trim() ;
            if (ce.SysID == 0)
            {
                Alert("请选择系统");
                return;
            }
            if (ce.ConfID == 0)
            {
                Alert("请选择配置单");
                return;
            }
            if (ce.KeyName == string.Empty)
            {
                Alert("Key不能为空");
                return;
            }
             
            
            ce.DataType = ddlDataType.SelectedValue;
            int ItemID = CommonFun.StrToInt(litItemID.Text);
            
            if (ItemID != 0)
            {
                ce.ItemID = ItemID;
                if (!DConfigItem.ExistsKeyForUpdate(ce.SysID, ce.KeyName,ce.ItemID))
                {
                    DConfigItem.Update(ce);
                    
                    Cancel();
                }
                else
                {
                    Alert("同一配置单不能有相同Key!");
                }
            }
            else
            {

                if (!DConfigItem.ExistsKeyForAdd(ce.SysID,ce.KeyName))
                {
                    DConfigItem.Add(ce);
                   
                    Cancel();
                }
                else
                {
                    Alert("同一配置单不能有相同Key!");
                }
            }
            BindList();
        }
        protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysConf();
        }

        protected void ddlSysConf_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
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
            int ItemID =CommonFun.StrToInt(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            EConfigItem ce = DConfigItem.GetEntity(ItemID);
            if (ce.ItemID > 0)
            {
                txtDescription.Text = ce.Description;
                txtConfKey.Text = ce.KeyName;
                txtConfValue.Text =  ce.Value ;
                litItemID.Text = ItemID.ToString();
                DropDownSelectItem(ddlDataType, ce.DataType);
                DropDownSelectItem(ddlSystem,ce.SysID.ToString());
                DropDownSelectItem(ddlSysConf,ce.ConfID.ToString());
            }
            else
            {
                //Alert(this, "该记录已不存在!");
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            int itemid = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            if (itemid > 0)
            {
                DConfigItem.Delete(itemid);
                Cancel();
            }
            BindList();
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
            litItemID.Text = "";
            txtDescription.Text = "";
            txtConfKey.Text = "";
            txtConfValue.Text = "";            
        }
    }
}