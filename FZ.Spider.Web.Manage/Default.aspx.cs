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
using log4net;
using FZ.Spider.DAL.Data.Sys;
namespace FZ.Spider.Web.Manage
{
    public partial class Default : System.Web.UI.Page
    {
        private static ILog logger = LogManager.GetLogger(typeof(Default).FullName);
        /// <summary>
        /// ҳ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        { 
            this.txtUser.Focus();
        }

        # region ����ϵͳ����ƽ̨��¼
        /// <summary>
        /// ����ϵͳ����ƽ̨��¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            string userName =this.txtUser.Text.Trim();
            string password = this.txtPwd.Text.Trim();
            try
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    if (DAdministrator.GetEntity(userName).Password == password)
                    {
                        Response.Cookies["AdminName"].Value = userName;
                        Response.Redirect("Frame/MainFrame.aspx",false);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoginFailed", "alert('�û������������')", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoginFailed", "alert('��¼�û��������벻��Ϊ�գ�')", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("��¼�쳣",ex);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "LoginFailed", "alert('��¼�쳣��')", true);
            }
            logger.Info("�û�" + userName + "��¼");

        }
        # endregion 

        # region ��ȡ�ͻ���IP
        /// <summary>
        /// ��ȡ�ͻ���IP
        /// </summary>
        /// <returns>�ͻ���IP��ַ</returns>
        protected string GetClientIP()
        {
            string ip = String.Empty;

            if (Request.ServerVariables["HTTP_VIA"] == null ||
                string.IsNullOrEmpty(Request.ServerVariables["HTTP_VIA"].ToString()))
            {
                ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            else
            {
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null ||
                    string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString()))
                    ip = Request.ServerVariables["REMOTE_ADDR"].ToString();
                else
                    ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }

            return ip;
        }
        # endregion
    }
}
