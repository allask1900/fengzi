<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.Tags" Title="搜索引擎系统\标签管理" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style11
        {
            height: 30px; 
            width:60px;
        }
        .style12
        { 
            width:200px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle">搜索引擎系统\标签管理</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
                    <tr>
                        <td height="22" width="82%" style="width: 100%">
                            <span class="STYLE13"><strong> 1、编辑标签</strong></span><asp:Literal ID="litTagID" runat="server" Visible="False"></asp:Literal></td>
                    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;">
    <tr>
        <td>
            <table>
                <tr>
                <td class="style11">所属分类:</td>
                <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_1" runat="server" OnSelectedIndexChanged="ddlSysCategory_1_SelectedIndexChanged" CssClass="ddl"  Width="200px" AutoPostBack="True"></asp:DropDownList></td>
                <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_2" runat="server" OnSelectedIndexChanged="ddlSysCategory_2_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
                <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_3" runat="server" OnSelectedIndexChanged="ddlSysCategory_3_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
                <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_4" runat="server" CssClass="ddl" Width="200px" OnSelectedIndexChanged="ddlSysCategory_4_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            </table>

        </td>      
    </tr>   
    <tr>
        <td>
            <table>
                <tr>
                    <td class="style11">标签名称:</td>
                    <td class="style12"><asp:TextBox ID="txtTagName" runat="server"  CssClass="Txt"  style="WIDTH: 200px"></asp:TextBox></td>                   
                    <td class="style11">标签备注:</td>
                    <td><asp:TextBox ID="txtRemark" runat="server"   CssClass="Txt"  style="WIDTH: 200px"></asp:TextBox></td>
                    <td class="style11">是否有效:</td>
                    <td><asp:DropDownList ID="dropIsValid" runat="server" Width="80px" CssClass="ddl">
                        <asp:ListItem Value="True">有效</asp:ListItem>
                        <asp:ListItem Value="False">无效</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="style11">排&nbsp;&nbsp;&nbsp;&nbsp;序:</td>
                    <td><asp:TextBox ID="txtSort" runat="server"   CssClass="Txt"  style="WIDTH:80px"></asp:TextBox></td>
                    <td class="style11" style="width:80px">限定或继承:</td>
                    <td><asp:DropDownList ID="dropShowType" runat="server" Width="120px" CssClass="ddl">
                        <asp:ListItem Value="0">选择限定或继承</asp:ListItem>
                        <asp:ListItem Value="1">限定在该类别</asp:ListItem>
                        <asp:ListItem Value="2">子类别将继承</asp:ListItem>
                        </asp:DropDownList>
                    </td>                   
                </tr>
            </table>
        </td>        
    </tr>     
    <tr> 
        <td>
            <table>
                <tr>
                    <td class="style11"></td>
                    <td><asp:Button ID="btnAddSave"  CssClass="btn" style="WIDTH: 48px" runat="server" Text="保存" onclick="btnAddSave_Click" />&nbsp;&nbsp;
                         <asp:Button ID="btnCancel"  CssClass="btn" runat="server" Text="取消" onclick="btnCancel_Click" />
                    </td>
                    </tr>
            </table>
        </td>        
    </tr>
</table>

<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" width="99%"  style="margin-left:5px;" >
    <tr>
        <td height="22" width="82%" style="width: 100%">
            <span class="STYLE13"><strong> 2、标签列表</strong></span></td>
    </tr>
</table>

<table border="0" cellspacing="0"  style="WIDTH: 100%;">
<tr>
    <td>                     
<asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
        AutoGenerateColumns="False" EmptyDataText="没有相关数据！"  
        EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound"  OnRowUpdating="gvDataList_RowUpdating" OnRowDeleting="gvDataList_RowDeleteing">
        <HeaderStyle CssClass="STYLE13" />
        <AlternatingRowStyle BackColor="#EEEEEE" />
        <Columns>                        
            <asp:BoundField DataField="TagID" HeaderText="序号">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField DataField="CategoryID" HeaderText="分类ID">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center"/>
            </asp:BoundField>                                        
            <asp:BoundField DataField="TagName" HeaderText="标签名称">
                    <HeaderStyle Width="120px" />
                    <ItemStyle Width="120px" />
            </asp:BoundField>           
            <asp:BoundField DataField="Remark" HeaderText="备注">
                    <HeaderStyle Width="150px" />
                    <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="Sort" HeaderText="排序">
                    <HeaderStyle Width="40px" />
                    <ItemStyle Width="40px" />
            </asp:BoundField>

            <asp:TemplateField HeaderText="限定或继承">
                <HeaderStyle Width="80px" />
                <ItemStyle  HorizontalAlign="Center" Width="80px" />
                <ItemTemplate>
                    <%# Eval("ShowType").ToString() == "1" ? "限定" : "继承"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <HeaderStyle Width="50px" />
                <ItemStyle  HorizontalAlign="Center" Width="50px" />
                <ItemTemplate>
                    <%# Eval("isvalid").ToString() == "True" ? "已生效" : "未生效"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <HeaderStyle Width="80px" />
                <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                <ItemTemplate>                        
                <asp:LinkButton ID="btnUpdate" runat="server" ForeColor="blue" CommandName="Update"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false">修改</asp:LinkButton> &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnDelete" runat="server" ForeColor="blue" CommandName="Delete"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false"  OnClientClick="return confirm('您确认删除该记录吗?');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
    </asp:GridView>
</td>
</tr>
<tr> 
    <td><webdiyer:aspnetpager id="pager" runat="server"  Wrap="true" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="15" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>

 
 </asp:Content>