<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CategoryManage.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.CategoryManage" Title="搜索引擎系统\分类管理" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <style type="text/css">
        .style11
        {
            height: 40px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle">搜索引擎系统\分类管理</div>
    <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
                    <tr>
                        <td height="22" width="82%" style="width: 100%">
                            <span class="STYLE13"><strong> 1、编辑分类</strong></span></td>
                    </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="1100px"  style="margin-left:5px;" >        
        <tr>
          <td class="style11">大类</td>
          <td>
              <asp:DropDownList ID="dropCategory_1" runat="server"  CssClass="ddl" Width="250px" OnSelectedIndexChanged="dropCategory_1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
          </td>
          <td class="style11">分类名称</td>
          <td><asp:TextBox ID="txtCategoryName_1" runat="server" Width="300px" CssClass="Txt"></asp:TextBox></td>
          <td class="style11">&nbsp;</td>
          <td>
                <asp:Button ID="btnCategory_1" runat="server" Text="提交" OnClick="btnCategory_1_Click" CssClass="btn" Width="80px" />(未选定时为添加,否则为修改)
          </td>
        </tr>
        <tr>
          <td class="style11">二级分类</td>
          <td>
              <asp:DropDownList ID="dropCategory_2" runat="server"  CssClass="ddl" Width="250px" OnSelectedIndexChanged="dropCategory_2_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
          </td>
          <td class="style11">分类名称</td>
          <td> 
                <asp:TextBox ID="txtCategoryName_2"   runat="server" Width="300px" CssClass="Txt" Height="25px" TextMode="MultiLine"></asp:TextBox>
          </td>
          <td class="style11">
            <asp:CheckBox ID="cBoxCategory_2" runat="server" Text="子级" Checked="false" /></td>
        <td>
            <asp:Button ID="btnCategory_2" runat="server" Text="提交" OnClick="btnCategory_2_Click" CssClass="btn"  Width="80px"/>(未选定时为添加,否则为修改)
        </td>
        </tr>
        <tr>
          <td class="style11">三级分类</td>
          <td>
              <asp:DropDownList ID="dropCategory_3" runat="server"  CssClass="ddl" Width="250px" OnSelectedIndexChanged="dropCategory_3_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
          </td>
          <td class="style11">分类名称</td>
          <td> 
            <asp:TextBox ID="txtCategoryName_3" runat="server" Width="300px" Height="45px" TextMode="MultiLine" CssClass="Txt"></asp:TextBox>
          </td>
          <td>
            <asp:CheckBox ID="cBoxCategory_3" runat="server" Text="子级" Checked="false" />
              <br />
              批量以|分隔</td>
        <td>
            <asp:Button ID="btnCategory_3" runat="server" Text="提交" CssClass="btn" OnClick="btnCategory_3_Click"  Width="80px"/>(未选定时为添加,否则为修改)
        </td>
        </tr>
        <tr>
          <td class="style11">四级分类</td>
          <td>
              <asp:DropDownList ID="dropCategory_4" runat="server"  CssClass="ddl" Width="250px" OnSelectedIndexChanged="dropCategory_4_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
          </td>
          <td class="style11">分类名称</td>
          <td> 
            <asp:TextBox ID="txtCategoryName_4" runat="server" Width="300px" CssClass="Txt" Height="45px" TextMode="MultiLine"></asp:TextBox></td>
          <td>批量以|分隔</td>
        <td>
            <asp:Button ID="btnCategory_4" runat="server" Text="提交" CssClass="btn" OnClick="btnCategory_4_Click"  Width="80px"/>(未选定时为添加,否则为修改)
         </td>
        </tr>
  </table>
    
     <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" width="99%"  style="margin-left:5px;" >
        <tr>
            <td height="22" width="82%" style="width: 100%">
                <span class="STYLE13"><strong> 2、一级分类资料编辑</strong></span></td>
        </tr>
    </table>

    <table border="0" cellpadding="0" cellspacing="2" width="800px"  style="margin-left:5px;" >         
        <tr>
            <td class="style11">选择分类:</td>
            <td>
                <asp:DropDownList ID="dropFirstCategory" runat="server" Width="250px" OnSelectedIndexChanged="dropFirstCategory_SelectedIndexChanged" AutoPostBack="True" CssClass="ddl"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style11">页面标题:</td>
            <td><asp:TextBox ID="txtPageTitle" runat="server" Width="600px" CssClass="Txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style11">页面描述:</td>
            <td><asp:TextBox ID="txtPageDescription" runat="server" Height="100px" TextMode="MultiLine" Width="600px" CssClass="Txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style11">页面关键字:</td>
            <td><asp:TextBox ID="txtKeyWord" runat="server" Width="600px" CssClass="Txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="style11">搜索关键字:</td>
            <td><asp:TextBox ID="txtHotWord" runat="server" Width="600px" CssClass="Txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td><asp:Button ID="bntMetaInfo" runat="server" CssClass="btn" onclick="bntMetaInfo_Click" Text="确认" Width="100px" /></td>
        </tr>
             
      </table>
</asp:Content>
