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
    public partial class Module : ManagePage
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
            BindParentModule();
            BindModule();
        }
        protected void BindParentModule()
        {
            int sysID = CommonFun.StrToInt(ddlSystems.SelectedValue);
            if (sysID != 0)
            {
                this.ddlParentModuleID.DataSource = DModule.GetList(sysID, 0);
                this.ddlParentModuleID.DataValueField = "ModuleID";
                this.ddlParentModuleID.DataTextField = "ModuleName";
                this.ddlParentModuleID.DataBind();
                this.ddlParentModuleID.Items.Insert(0, new ListItem("请选择父模块", "-1"));
            }
            else
            {
                this.ddlParentModuleID.Items.Clear();
                this.ddlParentModuleID.Items.Add(new ListItem("请选择父模块", "-1"));
            }
            
        }
        /// <summary>
        /// 绑定模块
        /// </summary>
        protected void BindModule()
        {
            int sysID = CommonFun.StrToInt(ddlSystems.SelectedValue);
            int parentModuleID = CommonFun.StrToInt(this.ddlParentModuleID.SelectedValue,-1);            
            this.gvDataList.DataSource = DModule.GetList(sysID, parentModuleID);            
            this.gvDataList.DataBind();
            Cancel();
        }
       
        protected void BindSystems()
        {
            this.ddlSystems.DataSource = DSystem.GetList();
            this.ddlSystems.DataValueField = "SysID";
            this.ddlSystems.DataTextField = "SysName";
            this.ddlSystems.DataBind();            
            this.ddlSystems.Items.Insert(0, new ListItem("请选择子系统", "0"));
        }
        /// <summary>
        /// 添加或修改应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddApp_Click(object sender, EventArgs e)
        {
            if (txtModuleID.Text.Trim() == string.Empty)
            {
                Alert("编号不能为空!");
                return;
            }
            if (txtModuleName.Text.Trim() == string.Empty)
            {
                Alert("名称不能为空!");
                return;
            }
            if (ddlSystems.SelectedValue == string.Empty)
            {
                Alert("请选择系统!");
                return;
            }
            EModule se = new EModule();
            se.ModuleID =CommonFun.StrToInt(txtModuleID.Text.Trim());
            if (se.ModuleID == 0)
            {
                Alert("编号不正确!");
                return;
            }
            se.ModuleName = txtModuleName.Text.Trim();
            se.ModuleURL = txtUrl.Text.Trim();
            se.Description = txtSysDesc.Text.Trim();
            se.SysID = Convert.ToInt32(ddlSystems.SelectedValue);
            int pmid=CommonFun.StrToInt(ddlParentModuleID.SelectedValue);
            if(pmid==-1)
                pmid=0;
            se.ParentModuleID =pmid;
            se.Sort = CommonFun.StrToInt(txtSort.Text);
            if (se.ParentModuleID > 0 && se.ModuleURL == string.Empty)
            {
                Alert("请求路径不能为空");
                return;
            }
            int updateModuleID =CommonFun.StrToInt(litModuleID.Text.Trim());
            int checkModuleID = DModule.GetEntity(se.ModuleID).ModuleID;
            if (updateModuleID != 0)
            {
                if (updateModuleID != se.ModuleID && checkModuleID!=0)
                {
                    Alert("编号已存在!");
                    return;
                }
                DModule.UpdateByID(se, updateModuleID);
                Cancel();                
            }
            else
            {
                if (checkModuleID==0)
                {
                    DModule.Add(se);
                    Cancel();                    
                }
                else
                {
                    Alert("编号已存在!");
                    return;
                }
            }
            if (se.ParentModuleID == 0)
            {
                BindParentModule();
            }             
            BindModule();
            Cancel();
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
            int moduleID = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            EModule se = DModule.GetEntity(moduleID);
            if (se.ModuleID>0)
            {
                this.txtModuleID.Text = moduleID.ToString();
                this.txtModuleName.Text = se.ModuleName;
                this.txtSysDesc.Text = se.Description;
                this.txtUrl.Text = se.ModuleURL;
                this.txtSort.Text = se.Sort.ToString();
                litModuleID.Text = moduleID.ToString();
                DropDownSelectItem(ddlSystems, se.SysID.ToString());
                BindParentModule();
                DropDownSelectItem(ddlParentModuleID, se.ParentModuleID.ToString());
            }
            else
            {
                Alert("无法编辑!");
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            int moduleID = int.Parse(this.gvDataList.Rows[e.RowIndex].Cells[0].Text);
            if (moduleID > 0)
            {
                if (!DModule.CheckModuleDelete(moduleID))
                {
                    Alert("无法删除!");
                    return;
                }
                {
                    DModule.Delete(moduleID);
                    Cancel();
                    BindModule();
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
            txtModuleName.Text = "";
            txtModuleID.Text = "";
            txtUrl.Text = "";
            litModuleID.Text = "";
            txtSort.Text = "";
        }
        /// <summary>
        /// 绑定应用的所有应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindParentModule();
            BindModule();            
        }

        protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindModule();
        }
    }
}