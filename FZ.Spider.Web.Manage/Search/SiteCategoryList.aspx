<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SiteCategoryList.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.SiteCategoryList" Title="搜索引擎系统\站点分类入口" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
     .style11
        {
            height: 40px;
            width:100px;
        }
</style>
    <script type="text/javascript">
        function Check(ck) {
            var gv = document.getElementById('MainContent_gvDataList');
            var inp = gv.getElementsByTagName("input");
            if(ck){
                for (var i = 0; i < inp.length; i++) {
                    if (inp[i].type == 'checkbox')
                        inp[i].checked = true;
                }
            }
            else { 
                for (var i = 0; i < inp.length; i++) {
                    if (inp[i].type == 'checkbox') {
                        if (inp[i].checked)
                            inp[i].checked = false;
                        else
                            inp[i].checked = true;
                    }
                }
            }
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle">搜索引擎系统\站点分类入口</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、添加站点分类入口</strong></span></td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
    <tr>
        <td class="style11">筛选站点</td>
        <td style="width:210px"><asp:DropDownList ID="dropSite" runat="server" AutoPostBack="True" onselectedindexchanged="dropSite_SelectedIndexChanged" CssClass="ddl" Width="200px" ></asp:DropDownList></td>
        <td colspan="6"><asp:Label ID="labSiteDomain" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="style11">所属分类:</td>
        <td style="width:210px"><asp:DropDownList ID="dropCategory_1" runat="server" OnSelectedIndexChanged="dropCategory_1_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
        <td class="style11">二级分类</td>
        <td style="width:210px"><asp:DropDownList ID="dropCategory_2" runat="server" OnSelectedIndexChanged="dropCategory_2_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
        <td class="style11">三级分类</td>
        <td style="width:210px"><asp:DropDownList ID="dropCategory_3" runat="server" OnSelectedIndexChanged="dropCategory_3_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
        <td class="style11">四级分类</td>
        <td><asp:DropDownList ID="dropCategory_4" runat="server" onselectedindexchanged="dropCategory_4_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList>
            &nbsp;&nbsp;</td>
    </tr>
    
    <tr>
        <td class="style11">分类名称</td>
        <td style="width:210px"><asp:TextBox ID="txtSCName" runat="server" Width="195px" CssClass="Txt"></asp:TextBox></td>
        <td class="style11">分类入口</td>
        <td colspan="5"><asp:TextBox ID="txtSCUrl" runat="server" Width="650px" TextMode="MultiLine" CssClass="Txt" Height="35px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td colspan="7"><asp:Button ID="btnSiteCategory" runat="server" Text="确认添加" onclick="btnSiteCategory_Click" CssClass="btn"/></td>
    </tr>
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 2、站点分类入口列表</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
    <tr>
        <td style="width:180px;height: 30px;">&nbsp;<input type="submit"  value="全选" onclick="Check(true);" id="AllCheck" class="btn" />&nbsp;&nbsp;<input type="submit"  value="反选" onclick="    Check(false);" id="UnCheck" class="btn" />&nbsp;&nbsp;</td>       
        <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDeleteCategory" runat="server" Text="删除所有选定分类" onclick="btnDeleteCategory_Click" CssClass="btn"  OnClientClick="return confirm('您确认删除所有选定分类吗?');" /></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td align="left">
        <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" AutoGenerateColumns="False" EmptyDataText="没有相关数据！" EmptyDataRowStyle-ForeColor="red" OnRowDeleting="gvDataList_RowDeleting" OnRowDataBound="gvDataList_RowDataBound" EnableModelValidation="True">
            <HeaderStyle CssClass="STYLE13" />
            <AlternatingRowStyle BackColor="#EEEEEE" />
            <Columns>                
                <asp:TemplateField HeaderText="编号">
                    <ItemStyle HorizontalAlign="Center" Width="60px"/>
                    <HeaderStyle HorizontalAlign="Center" />                                            
                    <ItemTemplate><asp:CheckBox runat="server" ID="SCID" Text='<%# Bind("SCID")%>'></asp:CheckBox></ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="SiteID" ReadOnly="true" HeaderText="站点ID">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="CategoryID" ReadOnly="true" HeaderText="分类ID">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                 </asp:BoundField>                
                <asp:TemplateField HeaderText="原站点分类名称">
                    <HeaderStyle HorizontalAlign="Center" Width="650px"/> 
                    <ItemStyle Width="650px"/>                                                            
                    <ItemTemplate>
                        <a href='<%#Eval("SCUrl") %>' target="_blank"><%#Eval("SCName")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="对应系统信息分类">
                    <HeaderStyle HorizontalAlign="Center"/>                                                            
                    <ItemTemplate>
                        <%#Eval("CategoryName")%>
                    </ItemTemplate>
                </asp:TemplateField> 
                  <asp:TemplateField HeaderText="删除">
                    <HeaderStyle Width="50px"  HorizontalAlign="Center"/>                           
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
    <td align="right"><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="99%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="200" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>
</asp:Content>
