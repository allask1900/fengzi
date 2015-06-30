using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FZ.Spider.DAL.Table;
using FZ.Spider.DAL.Entity.Search;
using FZ.Spider.DAL.Data.Search;
using FZ.Spider.DAL.Collection;
using FZ.Spider.Configuration;
using FZ.Spider.Common;

namespace FZ.Spider.Spider
{
    public class SpiderTest
    {
        public StringBuilder testLog = new StringBuilder("");     
        
        public SpiderTest()
        { 
        }
        /// <summary>
        /// 测试所有网站
        /// </summary>
        public void TestAll()
        {
            List<ECategory> cCategory = DCategory.GetList(0, 0);
            for (int i = 0; i < cCategory.Count; i++)
            {
                ECategory eCategory=(ECategory)cCategory[i];
                TestCategory(eCategory);
            }
        }
        
        /// <summary>
        /// 测试一类网站
        /// </summary>
        public void TestCategory(ECategory eCategory)
        {            
            CSite cSite = DSite.GetListForAnalysis(eCategory.CategoryID);
            testLog.AppendLine("<table class=\"TB_Grid\" cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"width:99%;border-collapse:collapse;\">");
            testLog.AppendLine("<tr>");
            testLog.AppendLine("<td  bgcolor=\"#eeeeee\"><span class=\"STYLE13\"><strong>分类:" + eCategory.CategoryName + "，共分析站点"+cSite.Count+"个</strong></span></td>");
            testLog.AppendLine("</tr>");
            testLog.AppendLine("<tr style=\"height:25px;\">");
            testLog.AppendLine("<td>");
            for (int i = 0; i < cSite.Count; i++)
            {
                ESite eSite = (ESite)cSite[i];
                TestSite(eSite.SiteID, eCategory.CategoryID);           
            }
            testLog.AppendLine("</td></tr></table>");
        }
        /// <summary>
        /// 测试一个模板(单个站点多个分类)
        /// </summary>
        public void TestSite(int ConfigID)
        {
            ESiteConfig eSiteConfig=DSiteConfig.GetEntity(ConfigID);        
            string[] ids = eSiteConfig.CategoryIDS.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in ids)
            {
                TestSite(eSiteConfig.SiteID, CommonFun.StrToInt(id));
            }
        }         
        /// <summary>
        /// 测试单个网站单个分类
        /// </summary>
        public void TestSite(int SiteID, int CategoryID)
        { 
            ESite eSite =DSite.GetEntity(SiteID);
            eSite.AnalysisCategoryID = CategoryID; 
            SpiderSite spiderSite = new SpiderSite( eSite,true);
            spiderSite.Start();


            testLog.AppendLine("<table class=\"TB_Grid_2\" cellspacing=\"0\" border=\"1\" style=\"width:100%;border-collapse:collapse;\" align=\"center\">");
            testLog.AppendLine("<tr>");
            testLog.AppendLine("<td colspan=\"2\" style=\"height: 30px; background-color: #DFD9F7;\" align=\"left\">");
            testLog.AppendLine("<table width=\"100%\">");
            testLog.AppendLine("<tr  class=\"STYLE13\">");
            testLog.AppendLine("<td>网站：<a href=\"http://" + eSite.SiteDomain + "\" target=\"_blank\">" + eSite.SiteName + "</a></td>");
            testLog.AppendLine("<td>分类(" + CategoryID + "): "+DCategory.GetEntity(CategoryID).CategoryName+"</td>");
            testLog.AppendLine("<td>列表：<a href=\"" + spiderSite.testInfo.CategoryUrl + "\" target=\"_blank\">" + spiderSite.testInfo.CategoryUrl + "</a></td>");
            testLog.AppendLine("<td>共 " + spiderSite.testInfo.PageCount + " 页</td>");
            testLog.AppendLine("<td>共提取列样产品(" + spiderSite.spiderWork.workQueue.Count + ")项</td>");
            testLog.AppendLine("</tr>");
            testLog.AppendLine("</table>");                     
            testLog.AppendLine("</td>");	
            testLog.AppendLine("</tr>");	
            testLog.AppendLine("<tr>");	                 
            testLog.AppendLine("<td style=\"height: 200px;\" valign=\"top\">");	
            testLog.AppendLine(" <div style=\"width:100%;\">");            
            int count = spiderSite.spiderWork.workQueue.Count;
            for (int i = 0; i < count&&i<9; i++)
            {
                EProduct eproduct_1;
                spiderSite.spiderWork.workQueue.TryDequeue(out eproduct_1);
                SpiderContentPage spiderContentPage = new SpiderContentPage(eproduct_1);
                spiderContentPage.Start(0);
                testLog.AppendLine("<div class=\"item\">");
                testLog.AppendLine("<table style=\"width:100%\" class=\"TB_Grid_2\">");
                testLog.AppendLine("<tr>");
                testLog.AppendLine("<td class=\"item_t\">产品标题</td>");
                testLog.AppendLine("<td><a href=" + eproduct_1.ResourceUrl + " target=\"_blank\" title=\"" + eproduct_1.FullName + "\" >" + StringHelper.Substring(eproduct_1.FullName, 65, "...") + "</a></td>");
                testLog.AppendLine("</tr>");
                testLog.AppendLine("<tr class=\"line_even\">");
                testLog.AppendLine("<td class=\"item_t\">产品价格</td>");
                testLog.AppendLine("<td>Price:" + eproduct_1.Price + "&nbsp;&nbsp;&nbsp;&nbsp; UsedPrice:" + eproduct_1.UsedPrice + " &nbsp;&nbsp;&nbsp;&nbsp; OrgPrice:" + eproduct_1.OrgPrice + "</td>");
                testLog.AppendLine("</tr>");
                testLog.AppendLine("<tr>");
                testLog.AppendLine("<td class=\"item_t\">产品图片</td>");
                testLog.AppendLine("<td>");
                if(eproduct_1.SamllImageUrl!=string.Empty)
                    testLog.AppendLine("<a href=" + eproduct_1.SamllImageUrl + " target=\"_blank\" alt=\"" + eproduct_1.SamllImageUrl + "\">SamllImageUrl</a>&nbsp;&nbsp;&nbsp;&nbsp;");
                if (eproduct_1.ImageUrl != string.Empty)
                    testLog.AppendLine("<a href=" + eproduct_1.ImageUrl + " target=\"_blank\" alt=\"" + eproduct_1.ImageUrl + "\">BigImageUrl</a></td>");
                testLog.AppendLine("</tr>");
                testLog.AppendLine("<tr class=\"line_even\">");
                testLog.AppendLine("<td class=\"item_t\">产品品牌</td>");
                testLog.AppendLine("<td>" + eproduct_1.BrandName + "</td>");
                testLog.AppendLine("</tr>");
                testLog.AppendLine("<tr>");
                testLog.AppendLine("<td class=\"item_t\">产品描述</td>");
                testLog.AppendLine("<td style=\"height: 46px\" valign=\"top\">");
                testLog.AppendLine("<div class=\"p_desc\">");
                testLog.AppendLine(eproduct_1.Description);
                testLog.AppendLine(" </div>");
                testLog.AppendLine("</td>");
                testLog.AppendLine(" </tr>");
                testLog.AppendLine("<tr class=\"line_even\">");
                testLog.AppendLine("<td class=\"item_t\">产品型号</td>");
                testLog.AppendLine("<td>Model:" + eproduct_1.Model + "  UPCOrISBN:"+eproduct_1.UPCOrISBN+"</td>");
                testLog.AppendLine("</tr>");
                testLog.AppendLine("<tr>");
                testLog.AppendLine("<td class=\"item_t\">产品属性</td>	");
                testLog.AppendLine("<td valign=\"top\">");
                testLog.AppendLine("<div class=\"p_desc\">");
                testLog.AppendLine(eproduct_1.Specifications);
                testLog.AppendLine("</div>");
                testLog.AppendLine("</td>");
                testLog.AppendLine("</tr>");

                testLog.AppendLine("<tr>");
                testLog.AppendLine("<td class=\"item_t\">产品评论(" + eproduct_1.CommentList.Count + ")</td>	");
                testLog.AppendLine("<td valign=\"top\">");
                testLog.AppendLine("<div class=\"p_desc\">");
                for (int l = 0; l < 10 && l < eproduct_1.CommentList.Count; l++)
                {
                    EProductComment epc=eproduct_1.CommentList[l];
                    testLog.AppendLine(" <br>User:" + epc.UserName);
                    testLog.AppendLine(" <br>Title:" + epc.Title);
                    testLog.AppendLine(" <br>CheckInTime:" + epc.CheckInTime.ToString("yyyy-MM-dd"));
                    testLog.AppendLine(" <br>Comment:" + epc.Comment);
                }
                testLog.AppendLine("</div>");
                testLog.AppendLine("</td>");
                testLog.AppendLine("</tr>");

                testLog.AppendLine("</table>");
                testLog.AppendLine("</div>");
            }
            testLog.AppendLine(" </div>");
            testLog.AppendLine("</td>");
            testLog.AppendLine("</tr>");
            testLog.AppendLine("</table>");            
        }       
    }
}
