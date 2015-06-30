<%@ Page Title="搜索引擎系统\爬虫测试" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SpiderTest.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.TestSpider" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle"  width="80%">搜索引擎系统\爬虫测试</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、选择分类</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="1200px"  style="margin-left:5px;" >
<tr>
    <td class="style11">选择分类:</td>
    <td><asp:DropDownList ID="ddlCategory_1" Width="250px" CssClass="ddl" runat="server" OnSelectedIndexChanged="ddlCategory_1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
    <td class="style11">选择站点:</td>
    <td><asp:DropDownList ID="ddlSite" Width="250px" CssClass="ddl" runat="server"></asp:DropDownList></td>
    <td><asp:Button ID="btnTest" runat="server" Text="开始测试" onclick="btnTest_Click" Width="120px" CssClass="btn" />&nbsp;(测试全部或者测试单个分类或者测试单个站点)</td>    
</tr>
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 2、测试结果</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="1200px"  style="margin-left:5px;" >
<tr>
    <td><asp:Literal ID="litTestLog" runat="server"></asp:Literal></td>     
</tr>
</table>
</asp:Content>