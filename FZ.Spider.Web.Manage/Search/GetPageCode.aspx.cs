using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FZ.Spider.DAL.Collection;
using FZ.Spider.DAL.Entity;
using FZ.Spider.DAL.Data;
using FZ.Spider.Common;
using System.Text.RegularExpressions;
using System.Text;
 

namespace FZ.Spider.Web.Manage.Search
{
    public partial class GetPageCode : FZ.Spider.Web.WebControl.ManagePage
    {
        private const string regGetGroupName = @"\(\?\<(?<name>[^>]*?)\>";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRequest_Click(object sender, EventArgs e)
        {
            string url =txtPageUrl.Text.Trim();
            this.txtPageCode.Text = PageHelper.ReadUrl(url, System.Text.Encoding.GetEncoding(ddlCharSet.SelectedValue), "");
            string reg = txtReg.Text.Trim();
            if (reg == string.Empty)
                return;

            List<string> groupName = new List<string>();
            MatchCollection mc_name = RegexHelper.MatchCollection(reg, regGetGroupName);
            foreach (Match ma in mc_name)
            {
                groupName.Add(ma.Groups["name"].Value.Trim());
            }
            MatchCollection mcValue = RegexHelper.MatchCollection(txtPageCode.Text, reg);
            StringBuilder sb = new StringBuilder("");
            foreach (Match ma in mcValue)
            {
                foreach(string gn in groupName)
                {
                    sb.Append(ma.Groups[gn].Value);
                    sb.Append(" ");
                }
                sb.AppendLine(",");
            }
            txtValues.Text = sb.ToString();
        } 
    }
}
