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
    public partial class SystemRegister : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSys();
            }
        }       
        /// <summary>
        /// 绑定系统
        /// </summary>
        protected void BindSys()
        {
            this.gvDataList.DataSource = DSystem.GetList();            
            this.gvDataList.DataBind();           
        }
       
        
        /// <summary>
        /// 添加或修改应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddApp_Click(object sender, EventArgs e)
        {
            if (txtSysID.Text.Trim() == string.Empty)
            {
                Alert("系统编号不能为空!");
                return;
            }
            if (txtSysName.Text.Trim() == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            ESystem se = new ESystem();
            se.SysID =CommonFun.StrToInt(txtSysID.Text.Trim());
            if (se.SysID == 0 || txtSysID.Text.Trim().Length!=4)
            {
                Alert("编号不正确(四位整数)!");
                return;
            }
            se.SysName = txtSysName.Text.Trim();
            se.Sort =CommonFun.StrToInt(txtSort.Text.Trim());
            se.Description = txtSysDesc.Text.Trim();

            int updateSysID = CommonFun.StrToInt(litSysID.Text.Trim());
            int checkSysID=DSystem.GetEntity(se.SysID).SysID;
            if (updateSysID != 0)
            {
                if (updateSysID != se.SysID && checkSysID == se.SysID)
                {
                    Alert("编号已存在!");
                    return;
                }
                DSystem.UpdateByID(se, updateSysID);
                Cancel();                
            }
            else
            {
                if (checkSysID == 0)
                {
                    DSystem.Add(se);
                    Cancel();                    
                }
                else
                {
                    Alert("编号已存在!");
                    return;
                }
            }             
            BindSys();            
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
            int sysID = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            ESystem se = DSystem.GetEntity(sysID);
            if (se.SysID>0)
            { 
                this.txtSysID.Text =sysID.ToString();
                this.txtSysName.Text = se.SysName;
                this.txtSysDesc.Text = se.Description;
                this.txtSort.Text = se.Sort.ToString();
                litSysID.Text = sysID.ToString();
            }
            else
            {
                Alert("无法编辑!");
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            int sysID = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            if (sysID > 0)
            {
                if (!DSystem.CheckSystemDelete(sysID))
                {
                    Alert("无法删除!");
                    return;
                }
                {
                    DSystem.Delete(sysID);
                    Cancel();
                    BindSys();
                }
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
            txtSysDesc.Text = "";
            txtSysName.Text = "";
            txtSysID.Text = "";
            txtSort.Text = "";
            litSysID.Text = "";
        } 
    }
}