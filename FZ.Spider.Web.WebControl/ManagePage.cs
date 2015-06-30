using System;
using System.Data;
using System.Configuration; 
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace FZ.Spider.Web.WebControl
{
    public class ManagePage:Page
    {
        protected string usreName = "";
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
#if (!DEBUG)
            if (Request.Cookies["AdminName"] == null || Request.Cookies["AdminName"].ToString() == "")
            {

                Response.Redirect("/Login.aspx");
            }
            usreName=Request.Cookies["AdminName"].Value;
#endif
        }
        /// <summary>
        /// js弹出信息
        /// </summary>
        /// <param name="AlertInfo"></param>
        protected void Alert(string AlertInfo)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Object), "alert", "<script>alert('" + AlertInfo + "!')</script>");
        }
        protected void DropDownSelectItem(DropDownList ddl, string value)
        {            
            ListItem li=ddl.Items.FindByValue(value);
            if (li != null)
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
        }
        protected void SetCheckBoxList(CheckBoxList cbl, string listValue)
        {
            listValue = "," + listValue + ",";
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (listValue.IndexOf(","+cbl.Items[i].Value+",") > -1)
                {
                    cbl.Items[i].Selected = true;
                }
                else
                {
                    cbl.Items[i].Selected = false;
                }
            }
        }
        protected string GetCheckBoxList(CheckBoxList cbl)
        {
            string listValue = "";
            for (int i = 0; i < cbl.Items.Count; i++)
            {
                if (cbl.Items[i].Selected)
                {
                    if (listValue == "")
                    {
                        listValue = cbl.Items[i].Value;
                    }
                    else
                    {
                        listValue = listValue + "," + cbl.Items[i].Value;
                    }
                }
            }
            return listValue;
        }
        /// <summary>
        /// 得到GridView中checkBox的选定值
        /// </summary>
        /// <param name="dv"></param>
        /// <param name="checkBoxID"></param>
        /// <returns></returns>
        protected string GetGridViewCheckBoxList(GridView dv, string checkBoxID)
        {
            string itemids = "";
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)dv.Rows[i].FindControl(checkBoxID);
                if (chk.Checked)
                {
                    if (itemids == "")
                    {
                        itemids = chk.Text;
                    }
                    else
                    {
                        itemids = itemids + "," + chk.Text;
                    }
                }
            }
            return itemids;
        }
    }
}
