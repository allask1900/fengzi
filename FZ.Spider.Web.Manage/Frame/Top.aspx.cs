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

using FZ.Spider.Common;
using FZ.Spider.DAL.Data.Sys;
using FZ.Spider.DAL.Collection;
using FZ.Spider.DAL.Entity.Sys;
 
using FZ.Spider.Web.WebControl;

namespace FZ.Spider.Web.Manage
{
    public partial class Top : ManagePage
    {
        public List<ESystem> systemList = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                systemList = DSystem.GetList();
            }
        }

        # region ÍË³ö
        /// <summary>
        /// ÍË³ö
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExit_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../Logout.aspx");
        }
        # endregion

    }
}
