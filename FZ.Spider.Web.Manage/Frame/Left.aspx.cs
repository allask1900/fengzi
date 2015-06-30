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
using System.Text;

using FZ.Spider.Web.WebControl;
using FZ.Spider.DAL.Entity.Sys;
using FZ.Spider.DAL.Data.Sys;

namespace FZ.Spider.Web.Manage
{
    public partial class Left : ManagePage
    {

        public ESystem mSystemEnt = null;
        public string MenList = string.Empty;

        int sysID = 0;

        /// <summary>
        /// 定义用户功能列表
        /// </summary>
        List<EModule> userMenuList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!String.IsNullOrEmpty(Request.QueryString["SysID"]))
                sysID = Convert.ToInt32(Request.QueryString["SysID"]);

            if (!Page.IsPostBack)
            {
                if (sysID != 0)
                    mSystemEnt = DSystem.GetEntity(sysID);
                else
                {
                    mSystemEnt = DSystem.GetList()[0];
                    if (mSystemEnt != null)
                        sysID = mSystemEnt.SysID;
                }

                this.CreateMenu();
            }
        }

        # region 加载菜单类别
        /// <summary>
        /// 加载菜单类别
        /// </summary>
        private void CreateMenu()
        {
            int parentID = 0;

            if (sysID != 0)
            {
                //检查变更排序权限，分配菜单
                userMenuList = DModule.GetList(sysID,-1);
                if (userMenuList != null && userMenuList.Count > 0)
                {
                    string header = string.Empty, footer = string.Empty;
                    bool flag = false;
                    StringBuilder sbContent = new StringBuilder();

                    for (int i = 0; i < userMenuList.Count; i++)
                    {
                        if (userMenuList[i].ParentModuleID==0)
                        {                            
                            StringBuilder sbChildContent = new StringBuilder();
                            sbChildContent.AppendLine("<tr>");
                            sbChildContent.AppendLine("<td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            sbChildContent.AppendLine("<tr>");
                            sbChildContent.AppendLine("<td height=\"23\" background=\"../images/main_47.gif\" id=\"imgmenu" + i.ToString() + "\" class=\"menu_title\" onMouseOver=\"this.className='menu_title2';\" onClick=\"showsubmenu(" + i.ToString() + ")\" onMouseOut=\"this.className='menu_title';\" style=\"cursor:hand\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            sbChildContent.AppendLine("<tr>");
                            sbChildContent.AppendLine("<td width=\"18%\">&nbsp;</td>");
                            sbChildContent.AppendLine("<td width=\"82%\" class=\"STYLE1\">" + userMenuList[i].ModuleName + "</td>");
                            sbChildContent.AppendLine(" </tr>");
                            sbChildContent.AppendLine("</table></td>");
                            sbChildContent.AppendLine("</tr>");


                            sbChildContent.AppendLine("<tr>");
                            sbChildContent.AppendLine("<td background=\"../images/main_51.gif\" id=\"submenu" + i.ToString() + "\">");
                            sbChildContent.AppendLine("<div class=\"sec_menu\" >  ");
                            sbChildContent.AppendLine("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                            sbChildContent.AppendLine("<tr>");
                            sbChildContent.AppendLine("<td>");


                            //生成子节点 
                            sbChildContent.AppendLine(this.CreateChildMemu(userMenuList[i].ModuleID, out flag));

                            sbChildContent.AppendLine("</td>");
                            sbChildContent.AppendLine("</tr>");

                            sbChildContent.AppendLine(" <tr>");
                            sbChildContent.AppendLine("<td height=\"5\"><img src=\"../images/main_52.gif\" width=\"151\" height=\"5\" /></td>");
                            sbChildContent.AppendLine("</tr>");
                            sbChildContent.AppendLine("</table></div></td>");
                            sbChildContent.AppendLine(" </tr>");

                            sbChildContent.AppendLine("</table></td>");
                            sbChildContent.AppendLine("</tr>");

                            if (flag)
                                sbContent.AppendLine(sbChildContent.ToString());
                        }

                        
                    }
                    header = "<table width=\"151\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">";
                    footer = "</table>";
                    if (!String.IsNullOrEmpty(sbContent.ToString()))
                        MenList = String.Format("{0}{1}{2}", header, sbContent, footer);
                }

            }
        }

        /// <summary>
        /// 加载子菜单
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        private string CreateChildMemu(int parentID, out bool flag)
        {
            int childNums = 0;
            StringBuilder sbMenu = new StringBuilder();

            //读取子菜单
            if (userMenuList != null)
            {
                sbMenu.AppendLine("<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");

                for (int i = 0; i < userMenuList.Count; i++)
                {
                    int ParentModuleID = userMenuList[i].ParentModuleID;
                    if (ParentModuleID != 0 &&ParentModuleID == parentID)
                    {
                        sbMenu.AppendLine("<tr>");
                        sbMenu.AppendLine("<td width=\"16%\" height=\"25\"><div align=\"center\"><img src=\"../images/left.gif\" width=\"10\" height=\"10\" /></div></td>");
                        sbMenu.AppendLine("<td width=\"84%\" height=\"23\"><table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                        sbMenu.AppendLine("<tr>");
                        sbMenu.AppendLine("<td height=\"20\" style=\"cursor:hand\" onMouseOver=\"this.style.borderStyle='solid';this.style.borderWidth='1';borderColor='#7bc4d3'; \" onmouseout=\"this.style.borderStyle='none'\"><a href=\"" + userMenuList[i].ModuleURL + "\" target='main'><span class=\"STYLE3\">" + userMenuList[i].ModuleName+ "</span></a></td>");
                        sbMenu.AppendLine("</tr>");
                        sbMenu.AppendLine("</table></td>");
                        sbMenu.AppendLine("</tr>");

                        childNums++;
                    }
                }

                sbMenu.AppendLine(" </table>");
            }

            flag = (childNums != 0);
            return sbMenu.ToString();

        }
        # endregion

    }
}
