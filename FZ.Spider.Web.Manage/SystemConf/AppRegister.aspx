<%@ Page Title="系统管理-应用管理" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="AppRegister.aspx.cs" Inherits="FZ.Spider.Web.Manage.SystemConf.AppRegister" %>
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
        .style11
        {
            height: 30px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="NavTitle">系统管理/应用服务管理</div>
    <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
        <tr>
            <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、添加应用/服务</strong></span></td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="700"  style="margin-left:5px;" >  
                    <tr>
                        <td class="style11">选择子系统* </td>
                        <td>
                            <asp:DropDownList ID="ddlSystems" runat="server" CssClass="ddl"  Width="140px" 
                                onselectedindexchanged="ddlSystems_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            </td>
                    </tr> 
                    <tr>
                        <td>应用编号*</td>
                        <td class="style11"> 
                            <asp:TextBox ID="txtAppID" CssClass="Txt"  style="WIDTH: 210px" runat="server"></asp:TextBox>
                            <asp:Literal ID="litAppID" runat="server" Visible="False"></asp:Literal>
                        </td>
                    </tr> 
                    <tr>
                        <td>应用名称*</td>
                        <td class="style11">
                            <asp:TextBox ID="txtAppName" CssClass="Txt"  style="WIDTH: 210px" runat="server"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td>
                    备注</td>
                        <td>
                            <asp:TextBox ID="txtAppDesc" runat="server" CssClass="Txt" TextMode="MultiLine" 
                                style="WIDTH: 545px; HEIGHT: 45px" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;<asp:Button ID="btnAddApp"  CssClass="btn" style="WIDTH: 48px"  
                                runat="server" Text="保存" onclick="btnAddApp_Click" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancel"  CssClass="btn" runat="server" Text="取消"                        onclick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
    
     <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" width="99%"  style="margin-left:5px;" >
        <tr>
            <td height="22" width="82%" style="width: 100%">
                <span class="STYLE13"><strong> 2、应用/服务列表</strong></span></td>
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
                    <asp:BoundField DataField="AppID" HeaderText="应用编号">
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px"/>
                        </asp:BoundField>                                     
                    <asp:BoundField DataField="AppName" HeaderText="名称">
                    <HeaderStyle Width="150px" />
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                    <asp:BoundField DataField="description" HeaderText="备注"/>
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
     </table>
</asp:Content>
