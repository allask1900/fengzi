<%@ Page Title="搜索引擎系统\站点分类提取" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SiteCategoryConfig.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.SiteCategoryConfig" ValidateRequest="false" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style11
        {
            height: 30px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle"  width="80%">搜索引擎系统\站点分类提取</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、分类正则</strong></span><asp:Literal ID="litSiteID" Visible="false" runat="server"></asp:Literal></td>
    </tr>
</table> 
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td style="width:85px" class="style11">分类入口:</td>
    <td><asp:TextBox ID="txtRootCategoryUrl" runat="server" CssClass="Txt" Width="550px" Height="20px"></asp:TextBox>
        <asp:Literal ID="litSiteName" runat="server"></asp:Literal>
    </td>    
</tr>
<tr>
    <td style="width:85px" class="style11">分类正则:</td>
    <td>
        <asp:TextBox ID="txtReg_GetSiteCategoryUrl" runat="server" TextMode="MultiLine" CssClass="Txt" Width="99%" Height="120px">CategoryLevel===0----IsSaveParent===false----Reg_GetCategory===<p><h2><a[\s]*?href=""(?<CategoryUrl>[^""]*?)"">(?<CategoryName>[^<]*?)</a></h2>----RegexReplace_ChangeCategoryUrl===reg1---->reg2====
CategoryLevel===1----IsSaveParent===true----Reg_GetCategory===<li[^>]*?><a\shref=""(?<CategoryUrl>[^""]*?)"">(?<CategoryName>[^<]*?)</a></li>----RegexReplace_ChangeCategoryUrl===reg1---->reg2====
CategoryLevel===2----IsSaveParent===true----Reg_GetCategory===<li[^>]*?><a\shref=""(?<CategoryUrl>[^""]*?)"">(?<CategoryName>[^<]*?)</a></li>----RegexReplace_ChangeCategoryUrl===reg1---->reg2</asp:TextBox>
    </td>    
</tr>
<tr>
    <td style="width:85px" class="style11">&nbsp;</td>
    <td><asp:Button ID="btnSiteCategoryUrl" runat="server" Text=" 确 认 " onclick="btnSiteCategoryUrl_Click" Width="80px" CssClass="btn" />&nbsp;&nbsp; <asp:Button ID="btnCancel" runat="server" Text=" 取 消 " onclick="btnCancel_Click" Width="80px" CssClass="btn" /></td>    
</tr>
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td style="width:85px" class="style11"><span class="STYLE13"><strong> 2、站点列表</strong></span></td>
        <td><asp:DropDownList ID="ddlCategory_1" Width="250px" CssClass="ddl" runat="server" OnSelectedIndexChanged="ddlCategory_1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>                
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
        AutoGenerateColumns="False" EmptyDataText="没有相关数据！"  
        EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound"  OnRowUpdating="gvDataList_RowUpdating">
        <HeaderStyle CssClass="STYLE13" />
        <AlternatingRowStyle BackColor="#EEEEEE" />
        <Columns>                        
            <asp:BoundField DataField="SiteID" HeaderText="站点ID">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center"/>
                </asp:BoundField>      
            <asp:TemplateField HeaderText="名称">
                    <HeaderStyle HorizontalAlign="Center" /> 
                    <ItemStyle Width="160px" />                                           
                    <ItemTemplate>
                        <a href='http://<%#Eval("SiteDomain") %>' target="_blank"><%#Eval("SiteName")%></a>
                    </ItemTemplate>
                </asp:TemplateField>       
             <asp:TemplateField HeaderText="分类入口">
                    <HeaderStyle HorizontalAlign="Center" /> 
                    <ItemStyle Width="300px" />                                           
                    <ItemTemplate>
                        <a href='<%#Eval("RootCategoryUrl") %>' target="_blank"><%#Eval("RootCategoryUrl")%></a>
                    </ItemTemplate>
                </asp:TemplateField>              
             <asp:BoundField DataField="Rank" HeaderText="排名">
            <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:BoundField> 
             <asp:TemplateField HeaderText="提取分类正则">
                    <HeaderStyle HorizontalAlign="Center" /> 
                                                             
                    <ItemTemplate><%#System.Net.WebUtility.HtmlEncode(Eval("Reg_GetSiteCategoryUrl").ToString()) %></ItemTemplate>
                </asp:TemplateField>
           
            <asp:TemplateField HeaderText="操作">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px"  HorizontalAlign="Center"/>
                <ItemTemplate>                        
                <asp:LinkButton ID="btnUpdate" runat="server" ForeColor="blue" CommandName="Update"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false">修改</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="查看分类">
                    <HeaderStyle HorizontalAlign="Center" Width="70px" />  
                <ItemStyle Width="70px"  HorizontalAlign="Center"/>                                            
                    <ItemTemplate><a href='SiteCategory.aspx?siteid=<%#Eval("SiteID") %>'>查看分类</a></ItemTemplate>
                </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
    </asp:GridView>
                </td>
            </tr>
        </table>   
   </td>     
</tr>
    <tr> 
    <td><webdiyer:aspnetpager id="pager" runat="server"  Wrap="true" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="30" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>
</asp:Content>