<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SiteConfig.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.SiteConfig" Title="搜索引擎系统\站点配置" ValidateRequest="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
            function toggle(targetid) {
                if (document.getElementById) {
                    target = document.getElementById(targetid);
                    if (target.style.display == "block") {
                        target.style.display = "none";
                    } else {
                        target.style.display = "block";
                    }
                }
            }

    </script>
    <style type="text/css">
         .style8
        {
            height: 30px;
            width:150px;
        }
        .style11
        {
            height: 50px;
            width:150px;
        }
        #thisHelp{width:450px;border:solid #8696CC;}
        #thisHelp .title{ background-color:#7B80C6; font-weight:bold; color:#FFFFFF;height:20px;}
        #thisHelp .content{ background-color:#E2EAF5; padding:10px;}
        #thisHelp .foot{ text-align:center; background-color:#BBB;cursor:pointer; height:25px; color:#EEE; font-weight:bold;  vertical-align:bottom;  } 
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="NavTitle">网站管理/网站模板配置</div>

<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%">
            <span class="STYLE13"><strong> 1、选择资源</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >  
    <tr>
        <td class="style8">选择分类</td>
        <td style=" width:470px">
            <asp:DropDownList ID="dropFirstCategory" runat="server" CssClass="ddl"  Width="300px" OnSelectedIndexChanged="dropFirstCategory_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

        </td>
        <td class="style8">选择站点</td>
        <td  style=" width:470px">
            <asp:DropDownList ID="dropSite" runat="server" CssClass="ddl"  Width="200px" AutoPostBack="True" onselectedindexchanged="dropSite_SelectedIndexChanged"></asp:DropDownList>                  

            <asp:Label ID="labSiteDomain" runat="server"></asp:Label>              

        </td>
        <td> &nbsp;</td>
    </tr>
    <tr>
        <td class="style8">选择模板</td>
        <td colspan="4">
            <asp:DropDownList ID="dropSiteConfig" runat="server" CssClass="ddl"  Width="300px" OnSelectedIndexChanged="dropSiteConfig_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="style8">资源类型</td>
        <td colspan="4">
            <asp:CheckBoxList ID="cbListCategoryList" runat="server" RepeatColumns="9" CssClass="Txt" RepeatDirection="Horizontal" Width="99%"></asp:CheckBoxList>
        </td>
    </tr>                    
</table>

<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%">
            <span class="STYLE13"><strong> 2、配置模板</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td class="style8">爬虫配置文件</td>
    <td><asp:TextBox ID="txtSpiderTemplet" runat="server"  CssClass="TEXTAREA" TextMode="MultiLine" Width="99%" Height="500px" ></asp:TextBox></td>
</tr>
<tr>
    <td class="style11"> </td>
    <td>&nbsp;<asp:Button ID="btnAddApp"  CssClass="btn" style="WIDTH: 48px" runat="server" Text="保存" onclick="btnAddApp_Click" />&nbsp;&nbsp;
       <asp:Button ID="btnCancel"  CssClass="btn" runat="server" Text="取消" onclick="btnCancel_Click" />
   </td>
 </tr>
</table>
</asp:Content>
