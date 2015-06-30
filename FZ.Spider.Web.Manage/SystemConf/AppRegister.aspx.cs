using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FZ.Spider.DAL.Data.Sys;
using FZ.Spider.DAL.Entity.Sys;
using FZ.Spider.Common;
using FZ.Spider.Web.WebControl;
namespace FZ.Spider.Web.Manage.SystemConf
{
    public partial class AppRegister : ManagePage
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
            BindSystems();
            BindApp();
        }
        /// <summary>
        /// 绑定应用
        /// </summary>
        protected void BindApp()
        {
            int sysID = CommonFun.StrToInt(ddlSystems.SelectedValue);
            if (sysID > 0)
            {
                this.gvDataList.DataSource = DApplication.GetList(sysID);
                this.gvDataList.DataBind();
            }
            
            Cancel();
        }
       
        protected void BindSystems()
        {
            this.ddlSystems.DataSource = DSystem.GetList();
            this.ddlSystems.DataValueField = "SysID";
            this.ddlSystems.DataTextField = "SysName";
            this.ddlSystems.DataBind();            
            this.ddlSystems.Items.Insert(0, new ListItem("请选择系统", "0"));
        }
        /// <summary>
        /// 添加或修改应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddApp_Click(object sender, EventArgs e)
        {
            if (txtAppID.Text.Trim() == string.Empty)
            {
                Alert("编号不能为空!");
                return;
            }
            if (txtAppName.Text.Trim() == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            EApplication se = new EApplication();
            se.AppID =CommonFun.StrToInt(txtAppID.Text.Trim());
            if (se.AppID == 0 || txtAppID.Text.Trim().Length!=6)
            {
                Alert("编号不正确!");
                return;
            }
            se.AppName = txtAppName.Text.Trim();           
            se.Description = txtAppDesc.Text.Trim();
            se.SysID = Convert.ToInt32(ddlSystems.SelectedValue);           
            int updateAppID=CommonFun.StrToInt(litAppID.Text.Trim());

            int checkAppID=DApplication.GetEntity(se.AppID).AppID;
            if (updateAppID != 0)
            {
                if (updateAppID != se.AppID && checkAppID != 0)
                {
                    Alert("编号已存在!");
                    return;
                }
                DApplication.UpdateByID(se, updateAppID);
                Cancel();                
            }
            else
            {
                if (checkAppID==0)
                {
                    DApplication.Add(se);
                    Cancel();                    
                }
                else
                {
                    Alert("编号已存在!");
                    return;
                }
            }
            BindApp();            
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
            int appid = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            EApplication se = DApplication.GetEntity(appid);
            if (se.AppID>0)
            {
                this.txtAppID.Text = appid.ToString();
                this.txtAppName.Text = se.AppName;
                this.txtAppDesc.Text = se.Description;
                litAppID.Text = appid.ToString();
                DropDownSelectItem(ddlSystems, se.SysID.ToString());
            }
            else
            {
                Alert("无法编辑!");
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            int appid = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            if (appid > 0)
            {
                  
                    DApplication.Delete(appid);
                    Cancel();
                    BindApp();
                
            } 
        }
        #endregion
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
            txtAppDesc.Text = "";
            txtAppName.Text = "";
            txtAppID.Text = ""; 
            litAppID.Text = "";
        }
        /// <summary>
        /// 绑定应用的所有应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindApp();
        }
    }
}