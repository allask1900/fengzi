<%@ Page Title="搜索引擎系统\获取页面代码" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="GetPageCode.aspx.cs" ValidateRequest="false" Inherits="FZ.Spider.Web.Manage.Search.GetPageCode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
         .style11
        {
            height: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">搜索引擎系统\获取页面代码</div>
<table>
    <tr>
        <td class="style11" width="80px">输入URL:</td>
        <td><asp:TextBox ID="txtPageUrl" runat="server" CssClass="Txt" Width="700px"></asp:TextBox></td>
        <td>
            <asp:DropDownList ID="ddlCharSet" runat="server"  CssClass="ddl"  Width="100px" >
                <asp:ListItem Value="UTF-8">默认</asp:ListItem>
                <asp:ListItem Value="UTF-8">UTF8</asp:ListItem>
                <asp:ListItem Value="GB2312">GB2312</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right"><asp:Button ID="btnRequest" runat="server" onclick="btnRequest_Click" Text="获取代码" Width="80px" CssClass="btn" /></td>
        
    </tr>
    <tr>
        <td class="style11" style="width:80px">提取正则:</td>
        <td colspan="4"><asp:TextBox ID="txtReg" runat="server"  CssClass="Txt" Width="700px" ></asp:TextBox></td>
    </tr>
    <tr>
        <td class="style11" style="width:80px">页面代码:</td>
        <td colspan="4"><asp:TextBox ID="txtPageCode" runat="server"  CssClass="Txt" Height="300px" Width="900px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
     <tr>
        <td class="style11" style="width:80px">提取结果:</td>
        <td colspan="4"><asp:TextBox ID="txtValues" runat="server"  CssClass="Txt" Height="200px" Width="900px" ReadOnly="True" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
</table>
</asp:Content>
