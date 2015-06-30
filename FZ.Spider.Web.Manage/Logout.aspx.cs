using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace FZ.Spider.Web.Manage
{
    /// <summary>
    /// 治理平台退出页面代码
    /// </summary>
    public partial class Logout : System.Web.UI.Page
    {
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //清空数据处理
            Session.Clear();

            //清除用户登录Cookie
            //UserCookieBL.RemoveUserLoginCookie();

            Response.Write("<script>window.parent.location='Default.aspx';target='_blank';</script>");
            Response.End();
        }

    }
}
