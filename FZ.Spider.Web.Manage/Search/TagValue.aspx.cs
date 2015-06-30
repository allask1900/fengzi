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
    public partial class TagValue : FZ.Spider.Web.WebControl.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSysCategory_1(); 
            }
        }
        protected void BindTagValue()
        { 
            gvDataList.DataSource = DTagValue.GetList(CommonFun.StrToInt(ddlTags.SelectedValue));
            gvDataList.DataBind();           
        }
        protected void btnAddSave_Click(object sender, EventArgs e)
        {
            ETagValue eTagValue = new ETagValue();
            eTagValue.TagID = CommonFun.StrToInt(ddlTags.SelectedValue);
            eTagValue.TagValue = txtTagValue.Text;

            if (eTagValue.TagID == 0)
            {
                Alert("未选标签");
                return;
            }

            eTagValue.IsValid = Convert.ToBoolean(dropIsValid.SelectedValue);
            eTagValue.Sort = CommonFun.StrToInt(txtSort.Text);
            eTagValue.Remark = txtRemark.Text;
            eTagValue.OrdID = CommonFun.StrToInt(litOrdID.Text);
            if (eTagValue.OrdID > 0)
            {
                DTagValue.Update(eTagValue);
            }
            else
            {
                if (DTagValue.Exists(eTagValue.TagID, eTagValue.TagValue))
                {
                    Alert("属性已存在");
                    return;
                }
                DTagValue.Add(eTagValue);
            }           
            Cancel();
            BindTagValue();
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
            txtRemark.Text ="";
            txtTagValue.Text = ""; 
            txtSort.Text = "";
            litOrdID.Text = "";
            btnAddSave.Text = "添加";
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
                ETagValue eTagValue = DTagValue.GetEntity(ordid);
                litOrdID.Text = eTagValue.OrdID.ToString();
                txtRemark.Text = eTagValue.Remark;
                txtTagValue.Text = eTagValue.TagValue; 
                txtSort.Text = eTagValue.Sort.ToString();
                DropDownSelectItem(dropIsValid, eTagValue.IsValid.ToString());
                btnAddSave.Text= "修改";
            }
        }
        protected void gvDataList_RowDeleteing(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDataList.Rows[e.RowIndex];
            int ordid = CommonFun.StrToInt(row.Cells[0].Text);
            if (ordid > 0)
            {
                if (DTagValue.GetEntity(ordid).IsValid)
                {
                    Alert("有效标签值,不能删除!");
                    return;
                }
                DTagValue.Delete(ordid);
                BindTagValue();
            }
        }


        protected void ddlSysCategory_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_2();
            BindSysCategory_3();
            BindSysCategory_4();
            BindTag();
        }

        protected void ddlSysCategory_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_3();
            BindSysCategory_4();
            BindTag();
        }

        protected void ddlSysCategory_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindSysCategory_4();
            BindTag();
        }
        protected void ddlSysCategory_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindTag();
        } 
        protected void BindTag()
        {
            int CategoryID = 0;
            if (ddlSysCategory_1.SelectedItem != null && ddlSysCategory_1.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_1.SelectedValue);
            }

            if (ddlSysCategory_2.SelectedItem != null && ddlSysCategory_2.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_2.SelectedValue);
            }
            if (ddlSysCategory_3.SelectedItem != null && ddlSysCategory_3.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_3.SelectedValue);
            }
            if (ddlSysCategory_4.SelectedItem != null && ddlSysCategory_4.SelectedValue != "0")
            {
                CategoryID = Convert.ToInt32(ddlSysCategory_4.SelectedValue);
            }
            if (CategoryID == 0)
            {
                Alert("选择对应分类");
                return;
            }
            List<ETags> list = DTags.GetList(CategoryID);
            this.ddlTags.DataTextField = "TagName";
            this.ddlTags.DataValueField = "TagID";
            this.ddlTags.DataSource = list;
            this.ddlTags.DataBind();
            this.ddlTags.Items.Insert(0, new ListItem("请选择", "0"));
        }
        protected void BindSysCategory_1()
        {
            List<ECategory> list = DCategory.GetList(0, 0);
            this.ddlSysCategory_1.DataTextField = "CategoryName";
            this.ddlSysCategory_1.DataValueField = "CategoryID";
            this.ddlSysCategory_1.DataSource = list;
            this.ddlSysCategory_1.DataBind();
            this.ddlSysCategory_1.Items.Insert(0, new ListItem("请选择", "0"));
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

        protected void ddlTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTags.SelectedItem != null && ddlTags.SelectedValue != "0")
                litTagName.Text =" ("+ ddlTags.SelectedItem.Text+") ";
            BindTagValue();
        }

       

    }
}
