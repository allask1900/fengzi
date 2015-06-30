<%@ Page Title="SEM管理\关键词添加" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WordAdd.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.WordAdd" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">SEM管理\关键词添加</div>
    <table align="center"  border="0" cellpadding="0" cellspacing="0" width="99%">
         
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.选择广告</strong></span></td>
        </tr> 
    </table>
    <table align="center" border="0" cellpadding="0" cellspacing="2" width="99%">
                    <tr>
                        <td style="vertical-align:middle; width:80px" class="style3">广告系列:</td>
                        <td  style="vertical-align:middle; width:250px" class="style3">                             
                            <asp:DropDownList ID="ddlAdCategory" runat="server" CssClass="ddl"  
                                style="vertical-align:middle;WIDTH: 215px" 
                                onselectedindexchanged="ddlAdCategory_SelectedIndexChanged"  AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style ="vertical-align:middle; width:80px">选择广告组:</td>
                        <td style="width:250px">
                            <asp:DropDownList ID="ddlAdGroup" runat="server" CssClass="ddl" style="vertical-align:middle;WIDTH: 215px" OnSelectedIndexChanged="ddlAdGroup_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="vertical-align:middle; width:80px;" class="style3">选择广告:</td>
                         <td>
                            <asp:DropDownList ID="ddlAdList" runat="server" CssClass="ddl" style="vertical-align:middle;WIDTH: 315px" OnSelectedIndexChanged="ddlAdList_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>     
    <table bgcolor="#eeeeee" align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
        <tr>
            <td height="22" width="82%" style="width: 100%">
                <span class="STYLE13"><strong>2.关键词</strong></span></td>
        </tr>
    </table>
    <table border="0" align="center" cellpadding="0" cellspacing="2" width="99%"> 
    <tr>
        <td style="width:80px">输入词<br />
            (每词一行)*</td>
        <td><asp:TextBox ID="txtWords" CssClass="Txt" runat="server" TextMode="MultiLine"  style="WIDTH: 400px; HEIGHT:150px"></asp:TextBox>
        </td>                     
    </tr> 
    <tr>
        <td style="width:80px">&nbsp;</td>
        <td >&nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btn" Text="保存" onclick="btnSave_Click" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="取消" onclick="btnCancel_Click" />&nbsp;&nbsp;&nbsp;</td>                     
    </tr> 
</table>

    <table bgcolor="#eeeeee" align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
        <tr>
            <td height="22" width="82%" style="width: 100%">
                <span class="STYLE13"><strong>3.关键词搜索</strong></span></td>
        </tr>
    </table>

    <table  align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
        <tr>
        <td class="style3" style="width:80px;">选择分类:</td>
        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_1" runat="server" OnSelectedIndexChanged="ddlSysCategory_1_SelectedIndexChanged" CssClass="ddl"  Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_2" runat="server" OnSelectedIndexChanged="ddlSysCategory_2_SelectedIndexChanged" CssClass="ddl" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_3" runat="server" OnSelectedIndexChanged="ddlSysCategory_3_SelectedIndexChanged" CssClass="ddl" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_4" runat="server" CssClass="ddl" Width="150px"></asp:DropDownList></td>
        <td style="width:80px">输入关键词:</td> 
        <td style="width:250px"><asp:TextBox ID="txtSearchWord" CssClass="Txt" runat="server" style="WIDTH: 230px;"></asp:TextBox></td> 
        <td><asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="搜索" onclick="btnSearch_Click" /></td>         
    </tr>
    </table>
    <table  align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" AutoGenerateColumns="False" EmptyDataText="没有相关数据！" EmptyDataRowStyle-ForeColor="red"  OnRowDataBound="gvDataList_RowDataBound" EnableModelValidation="True">
                    <HeaderStyle CssClass="STYLE13" />
                    <AlternatingRowStyle BackColor="#EEEEEE" />
                    <Columns>                
                        <asp:TemplateField HeaderText="产品ID">
                            <ItemStyle HorizontalAlign="Center" Width="60px"/>
                            <HeaderStyle HorizontalAlign="Center" />                                            
                            <ItemTemplate><asp:CheckBox runat="server" ID="ProductID" Text='<%# Bind("ProductID")%>'></asp:CheckBox></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CategoryID" ReadOnly="true" HeaderText="分类ID">
                            <HeaderStyle Width="60px" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                         </asp:BoundField>
                        <asp:TemplateField HeaderText="产品名称">
                            <HeaderStyle HorizontalAlign="Center" Width="650px"/>
                            <ItemStyle Width="650px"/>                                      
                            <ItemTemplate>
                                <%#Eval("FullName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>