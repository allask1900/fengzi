<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SpiderWorkQueue.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.SpiderWorkQueue" Title="搜索引擎系统\爬虫任务管理" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style11
        {
            height: 40px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle">搜索引擎系统\爬虫任务管理</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
                    <tr>
                        <td height="22" width="82%" style="width: 100%">
                            <span class="STYLE13"><strong> 1、添加爬虫任务</strong></span></td>
                    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >     
    <tr>
        <td  style="width:80px">选择站点</td>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width:215px"  class="style11"> 
                        <asp:DropDownList ID="dropSite" runat="server" CssClass="ddl"  Width="200px" AutoPostBack="True" onselectedindexchanged="dropSite_SelectedIndexChanged"></asp:DropDownList>
                    </td>             
                </tr>
            </table>
    </tr> 
    <tr>
        <td  style="width:80px">资源分类</td>
        <td class="style11">
                <asp:CheckBoxList ID="cbListCategoryList" runat="server" RepeatColumns="9"  RepeatDirection="Horizontal" CssClass="ddl" Width="99%">
                </asp:CheckBoxList>
        </td>
    </tr> 
    <tr>
        <td>
            &nbsp;</td>
        <td>&nbsp;<asp:Button ID="btnAddSite"  CssClass="btn" style="WIDTH: 48px"  
                runat="server" Text="保存" onclick="btnAddSite_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnCancel"  CssClass="btn" runat="server" Text="取消" onclick="btnCancel_Click" />
        </td>
    </tr>
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" width="99%"  style="margin-left:5px;" >
    <tr>
        <td height="22" style="width: 100px"><span class="STYLE13"><strong> 2、任务列表</strong></span></td>
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
            <asp:BoundField DataField="OrdID" HeaderText="编号">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center"/>
            </asp:BoundField>

            <asp:BoundField DataField="SiteName" HeaderText="站点名称">
                    <HeaderStyle Width="110px" />
                    <ItemStyle Width="110px" HorizontalAlign="Center"/>
            </asp:BoundField>
                                           
            <asp:BoundField DataField="CategoryName" HeaderText="分类名称">
                    <HeaderStyle Width="110px" />
                    <ItemStyle Width="110px" />
            </asp:BoundField> 

             <asp:TemplateField HeaderText="添加时间">
                    <HeaderStyle  Width="110" />   
                    <ItemStyle Width="110px" />                                         
                    <ItemTemplate><%# Convert.ToDateTime(Eval("CheckInTime")).ToString("yyyy-MM-dd HH:mm") %></ItemTemplate>
            </asp:TemplateField>
           
             
             <asp:TemplateField HeaderText="状态">
                    <HeaderStyle  Width="100" />   
                    <ItemStyle Width="100px" Font-Bold="true" />                                         
                    <ItemTemplate><%# GetStatusName(Convert.ToInt32(Eval("status"))) %></ItemTemplate>
                </asp:TemplateField>


            <asp:TemplateField HeaderText="开始时间">
                    <HeaderStyle  Width="110" />   
                    <ItemStyle Width="110px" />                                         
                    <ItemTemplate><%# Convert.ToDateTime(Eval("BeginTime")).ToString("yyyy-MM-dd HH:mm") %></ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="完成时间">
                    <HeaderStyle  Width="110" />   
                    <ItemStyle Width="110px" />                                         
                    <ItemTemplate><%# Convert.ToDateTime(Eval("CompleteTime")).ToString("yyyy-MM-dd HH:mm") %></ItemTemplate>
            </asp:TemplateField>
           
            
             
             <asp:TemplateField HeaderText="统计信息">
                    <HeaderStyle HorizontalAlign="Center" />                                            
                    <ItemTemplate><%#Eval("statInfo").ToString()%></ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                <ItemTemplate>                        
                <asp:LinkButton ID="btnUpdate" runat="server" ForeColor="blue" CommandName="Update"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false">修改</asp:LinkButton> &nbsp;&nbsp;
                <asp:LinkButton ID="btnDelete" runat="server" ForeColor="blue" CommandName="Delete"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false"  OnClientClick="return confirm('您确认删除该记录吗?');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
    </asp:GridView>
</td>
</tr>
<tr> 
    <td><webdiyer:aspnetpager id="pager" runat="server"  Wrap="true" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="30" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>

 
 </asp:Content>