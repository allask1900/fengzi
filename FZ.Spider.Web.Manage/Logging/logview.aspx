<%@ Page Title="搜索引擎系统\查看系统日志" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="logview.aspx.cs" Inherits="FZ.Spider.Web.Manage.Logging.Logview" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style11
        {
            height:40px;
            width:100px;
        }
        .style12
        { 
            background-color:#eeeeee;
        }

        #Button1 {
            width: 94px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">搜索引擎系统\查看系统日志</div> 
    <table  align="center" class="TB_Grid" cellspacing="0" rules="all" border="1" style="width:99%;border-collapse:collapse;">
        <tr>
            <td class="style11" >LogID</td>
            <td><asp:Literal ID="litLogID" runat="server"></asp:Literal></td>
        </tr>
        <tr class="style12">
            <td class="style11" >AppID</td>
            <td><asp:Literal ID="litAppID" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="style11" >SiteName</td>
            <td><asp:Literal ID="litSiteName" runat="server"></asp:Literal></td>
        </tr>
        <tr class="style12">
            <td class="style11" >LogDate</td>
            <td><asp:Literal ID="litLogDate" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="style11" >Thread</td>
            <td><asp:Literal ID="litThread" runat="server"></asp:Literal></td>
        </tr>
        <tr class="style12">
            <td class="style11" >LogLevel</td>
            <td><asp:Literal ID="litLogLevel" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="style11" >Logger</td>
            <td><asp:Literal ID="litLogger" runat="server"></asp:Literal></td>
        </tr>
        <tr class="style12">
            <td class="style11" >Class</td>
            <td><asp:Literal ID="litClass" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="style11" >Method</td>
            <td><asp:Literal ID="litMethod" runat="server"></asp:Literal></td>
        </tr>
        <tr class="style12">
            <td class="style11" >Message</td>
            <td><asp:Literal ID="litMessage" runat="server"></asp:Literal></td>
        </tr>
        <tr>
            <td class="style11" >Exception</td>
            <td><asp:Literal ID="litException" runat="server"></asp:Literal></td>
        </tr>        
    </table>
    <table width="99%">
       <tr> 
            <td style="text-align: center" >
                <input id="Button1" type="button" value="关闭窗口" onclick="window.close()" class="btn" /></td>
        </tr>
    </table>
</asp:Content>
