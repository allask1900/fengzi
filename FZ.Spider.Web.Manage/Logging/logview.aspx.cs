using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FZ.Spider.DAL.Data.SEM;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Entity.Common;
using FZ.Spider.DAL.Entity.SEM;
using FZ.Spider.Web.WebControl;
using FZ.Spider.Common;
using FZ.Spider.DAL.Entity.Sys;
using FZ.Spider.DAL.Data.Sys;
namespace FZ.Spider.Web.Manage.Logging
{
    public partial class Logview : ManagePage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            ELogging eLogging=DLogging.GetEntity(CommonFun.GetQueryInt("logid"));
            litAppID.Text = eLogging.AppID.ToString();
            litClass.Text = eLogging.Class;
            litException.Text = eLogging.Exception;
            litLogDate.Text = eLogging.LogDate.ToString("yyyy-MM-dd HH:mm:ss");
            litLogger.Text = eLogging.Logger;
            litLogID.Text = eLogging.LogID.ToString();
            litLogLevel.Text = eLogging.LogLevel;
            litMessage.Text = eLogging.Message;
            litMethod.Text = eLogging.Method;
            litSiteName.Text = eLogging.SiteName;
            litThread.Text = eLogging.Thread.ToString();
            
        }         
    }
}