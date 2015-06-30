<%@ Page Title="搜索引擎系统\爬虫模板测试" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SpiderTestConfig.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.SpiderTestConfig" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .item
        {
            width:500px;
            float:left;
            border:1px solid #a16ecf;
            margin:10px 10px 10px 10px;
            padding-right:10px;
        }
        .item_t
        {
            width:75px;
            height:20px
        }
        .line_even
        {
            background-color:#eeeeee
        }
        
        .p_desc
        {
	        width:100%;
	        overflow:auto;
	        height:60px;
            word-break:break-all;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">搜索引擎系统\爬虫模板测试</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、测试模板</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td class="style11" Width="60px">站点:</td>
    <td  Width="250px"> 
        <asp:Literal ID="litSite" runat="server"></asp:Literal>
    </td>
    <td class="style11" width="60px">分类:</td>
    <td> 
         <asp:CheckBoxList ID="cbListCategoryList" runat="server" RepeatColumns="7" CssClass="Txt" RepeatDirection="Horizontal"></asp:CheckBoxList>
    </td>
    <td><asp:Button ID="btnTest" runat="server" Text="开始测试" onclick="btnTest_Click" Width="120px" CssClass="btn" /></td>    
</tr>
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 2、测试结果</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td><asp:Literal ID="litTestLog" runat="server"></asp:Literal></td>     
</tr>
<tr>
    <td><asp:Button ID="btnTestOk" runat="server"  Text="测试成功" CssClass="btn" onclick="btnTestOk_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnTestFail" runat="server"  Text="测试失败"  CssClass="btn" onclick="btnTestFail_Click" />
    </td>     
</tr>
</table>
</asp:Content>