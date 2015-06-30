<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Brand.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.Brand" Title="搜索引擎系统\品牌管理" %>
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
<div class="NavTitle">搜索引擎系统\品牌管理</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、添加品牌</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="900px"  style="margin-left:5px;" >
<tr>
    <td class="style11">产品分类:</td>
    <td>
        <asp:DropDownList ID="dropFirst" runat="server" Width="250px" OnSelectedIndexChanged="dropFirst_SelectedIndexChanged" AutoPostBack="True" CssClass="ddl"></asp:DropDownList>
    </td>
    <td class="style11">产品品牌名称:</td>
    <td>
        <asp:TextBox ID="txtBrandName" runat="server" Width="300px" CssClass="Txt"></asp:TextBox>
    </td>
    <td>
        <asp:Button ID="btnBrand" runat="server" Text="确认添加" Width="80px" onclick="btnBrand_Click" CssClass="btn" />
    </td>
</tr>
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 2、产品合并品牌</strong></span></td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="2" width="900px"  style="margin-left:5px;" >
    <tr>
        <td class="style11"> 品牌ID&nbsp;&nbsp;&nbsp; </td>
        <td><asp:TextBox ID="txtMergeID" runat="server" Width="250px" CssClass="Txt"></asp:TextBox></td>
        <td class="style11">合并到品牌ID:</td>
        <td><asp:TextBox ID="txtMergeToID" runat="server" Width="300px" CssClass="Txt"></asp:TextBox></td>
        <td><asp:Button ID="btnMerge" runat="server" Text="确认合并" Width="80px" onclick="btnMerge_Click" CssClass="btn"/></td>
    </tr>
</table>

<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 3、品牌列表</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="600px"  style="margin-left:5px;" > 
    <tr>
        <td>选择有效状态</td>
        <td class="style3">
            <asp:DropDownList ID="ddlIsValid" runat="server" CssClass="ddl"  Width="140px" onselectedindexchanged="ddlIsValid_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="-1">全部</asp:ListItem>
                <asp:ListItem Value="1">有效</asp:ListItem>
                <asp:ListItem Value="0">无效</asp:ListItem>
            </asp:DropDownList>           
        </td>
        <td>选择审核状态</td>
        <td>
            <asp:DropDownList ID="ddlIsCheck" runat="server" CssClass="ddl"  Width="140px" onselectedindexchanged="ddlIsCheck_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="-1">全部</asp:ListItem>
                <asp:ListItem Value="1">已审核</asp:ListItem>
                <asp:ListItem Value="0">未审核</asp:ListItem>
            </asp:DropDownList>           
        </td>
        <td>
            <asp:Literal ID="litBrandCount" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Button ID="btnIsValid" runat="server" Text="所选有效"   Width="100px" onclick="btnIsValid_Click" CssClass="btn"/>&nbsp;&nbsp;&nbsp; 
            <asp:Button ID="btnIsCheck" runat="server" Text="所选已审核" Width="100px" onclick="btnIsCheck_Click" CssClass="btn"/>&nbsp;&nbsp;&nbsp; 
            <asp:Button ID="btnInvalid" runat="server" Text="所选无效"   Width="100px" OnClick="btnInvalid_Click" CssClass="btn"/>&nbsp;&nbsp;&nbsp; 
        </td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="900px"  style="margin-left:5px;" >
<tr>
    <td align="left">
        <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" AutoGenerateColumns="False" EmptyDataText="没有相关数据！" EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound"   
                    OnRowUpdating="gvDataList_RowUpdating" 
                    OnRowDeleting="gvDataList_RowDeleting"
                    OnRowCancelingEdit="gvDataList_RowCancelingEdit"
                    OnRowEditing="gvDataList_RowEditing"
                     EnableModelValidation="True">
            <HeaderStyle CssClass="STYLE13" />
            <AlternatingRowStyle BackColor="#EEEEEE" />
            <Columns>
                <asp:TemplateField HeaderText="品牌编号">
                    <ItemStyle HorizontalAlign="Center" Width="80px"/>
                    <HeaderStyle HorizontalAlign="Center" />                                            
                    <ItemTemplate><asp:CheckBox runat="server" ID="BrandID" Text='<%# Bind("BrandID")%>'></asp:CheckBox></ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="品牌名称">
                    <ItemStyle HorizontalAlign="Center"  width=250px/>
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemTemplate><%#Eval("BrandName")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="BrandName" runat ="server" Text='<%# Bind("BrandName")%>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                                        
                <asp:TemplateField HeaderText="是否启用">
                    <ItemStyle HorizontalAlign="Center"  width=60px/>
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemTemplate><%#Eval("IsValid")%></ItemTemplate> 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="真实品牌编号">
                    <ItemStyle HorizontalAlign="Center"  width=160px/>
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemTemplate><%#Eval("IsValidBrandID")%></ItemTemplate> 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="查看产品">
                    <ItemStyle HorizontalAlign="Center"  width="80px"/>
                    <HeaderStyle HorizontalAlign="Center"  />
                    <ItemTemplate><a href="../Product/ProductList.aspx?BrandID=<%#Eval("BrandID")%>" target="_blank">查看产品</a></ItemTemplate> 
                </asp:TemplateField>

                <asp:TemplateField HeaderText="品牌类别">
                    <ItemStyle HorizontalAlign="Center"/>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate><%#Eval("CategoryID")%></ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ItemStyle-HorizontalAlign="Center" 
                                ShowEditButton="True" EditText="编辑" HeaderText="更新" 
                                CancelText="取消"  UpdateText="更新" DeleteText=""> 
                                <HeaderStyle Width="90px" />                           
                                <ItemStyle Width="90px" HorizontalAlign="Center"/>
                 </asp:CommandField>  
                <asp:TemplateField HeaderText="删除">
                    <HeaderStyle Width="50px" />                           
                    <ItemStyle Width="50px" HorizontalAlign="Center"/>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="删除" OnClientClick="return confirm('您确认删除该记录吗?');"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>                                        
            </Columns>
            <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </td>
</tr>
<tr> 
    <td align=right><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="99%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="15" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>
</asp:Content>