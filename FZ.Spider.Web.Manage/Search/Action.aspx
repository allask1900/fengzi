<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="Action.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.Action" Title="搜索引擎系统\搜索引擎功能操作" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style11
        {
            height: 50px;
            width:250px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">搜索引擎系统\搜索引擎功能操作</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
                    <tr>
                        <td height="22" width="82%" style="width: 100%">
                            <span class="STYLE13"><strong>数据库操作</strong></span></td>
                    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td class="style11"><asp:Button ID="btnGetAlexaRank" runat="server" Text="更新所有站点Alexa排名" CssClass="btn" OnClick="btnGetAlexaRank_Click" /></td>
        <td class="style11"><asp:Button ID="btnGetAlexaRankAndDesc" runat="server" Text="更新所有站点Alexa排名和描述" CssClass="btn" OnClick="btnGetAlexaRankAndDesc_Click" Width="215px" /></td>
     
        <td class="style11"> <asp:Button ID="btnSetPriceTag" runat="server" Text="重新构造Price Tag" CssClass="btn" OnClick="btnSetPriceTag_Click" /> </td>
        <td class="style11"> <asp:Button ID="btnBackupDB" runat="server" Text="备份数据库(SearchSystem)" CssClass="btn" OnClick="btnBackupDB_Click"   Width="215px"/></td>
        <td class="style11"></td>
    </tr> 
</table>

<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%">
            <span class="STYLE13"><strong>产品站点数据更新操作</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
     <tr>
         <td class="style11"><asp:Button ID="Button1" runat="server" Text="重建XML缓存" CssClass="btn" OnClick="btnRebuildCache_Click" /></td>
         <td class="style11"><asp:Button ID="Button3" runat="server" Text="刷新Local缓存" CssClass="btn" OnClick="btnRefreshCache_Click" /></td>
         <td class="style11"><asp:Button ID="Button2" runat="server" Text="重建索引" CssClass="btn" OnClick="btnRebuildIndex_Click" /></td>
         <td class="style11"><asp:Button ID="Button4" runat="server" Text="全量重建产品XML文件" CssClass="btn" OnClick="btnRebuildXml_Click" /></td>
         <td class="style11"><asp:Button ID="Button5" runat="server" Text="增量创建产品XML文件" CssClass="btn" OnClick="btnCreateXml_Click" /></td>
    </tr> 
</table>
</asp:Content>