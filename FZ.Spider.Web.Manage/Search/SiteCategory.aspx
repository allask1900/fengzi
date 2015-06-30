<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SiteCategory.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.SiteCategory" Title="搜索引擎系统\站点分类入口" %>
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
        function CheckedRange() {
          
            var gv = document.getElementById('MainContent_gvDataList');
            var inp = gv.getElementsByTagName("input");
            
            var chk = false;
            for (var i = 0; i < inp.length; i++) {
                if (inp[i].type == 'checkbox') {
                    if (inp[i].checked && chk) {
                        chk = false;
                        return false;
                    }
                    if (chk)
                        inp[i].checked = true;

                    if (inp[i].checked) {
                        chk = true;
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
        <td class="style11">选择站点</td>
        <td style="width:210px"><asp:DropDownList ID="dropSite" runat="server" AutoPostBack="True" onselectedindexchanged="dropSite_SelectedIndexChanged" CssClass="ddl" Width="200px" ></asp:DropDownList></td>
        <td style="width:210px"><asp:Label ID="labSiteDomain" runat="server"></asp:Label></td>
        <td style="width:210px">对应分类(只用于转换一级分类)</td>
        <td style="width:210px"><asp:DropDownList ID="ddlFirstCategory" runat="server" CssClass="ddl"  Width="200px" ></asp:DropDownList></td>       
        <td style="width:130px" ><asp:Button ID="btnSpiderSiteCategory" runat="server" Width="120px" Text="抓取该站点分类" onclick="btnSpiderSiteCategory_Click"   OnClientClick="return confirm('您确认要抓取该站点分类吗?');" CssClass="btn"/></td>       
        <td ><asp:Button ID="btnReSpiderSiteCategory" Width="200px" runat="server" Text="重新抓取该站点分类(删除历史)" onclick="btnReSpiderSiteCategory_Click"  OnClientClick="return confirm('您确认删除该站点所有分类吗?');" CssClass="btn"/></td>       
    </tr>
    <tr>
        <td class="style11">对应分类</td>
        <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_1" runat="server" OnSelectedIndexChanged="ddlSysCategory_1_SelectedIndexChanged" CssClass="ddl"  Width="200px" AutoPostBack="True"></asp:DropDownList></td>
        <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_2" runat="server" OnSelectedIndexChanged="ddlSysCategory_2_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
        <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_3" runat="server" OnSelectedIndexChanged="ddlSysCategory_3_SelectedIndexChanged" CssClass="ddl" Width="200px" AutoPostBack="True"></asp:DropDownList></td>
        <td style="width:210px"><asp:DropDownList ID="ddlSysCategory_4" runat="server" CssClass="ddl" Width="200px"></asp:DropDownList></td>
        <td style="width:130px" > &nbsp;</td>
        <td> &nbsp;</td>
    </tr>
    </table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 2、站点分类入口列表</strong></span></td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
    <tr>
        <td style="width:120px;height: 30px;">&nbsp;<input type="submit"  value="全选" onclick="Check(true); return false;" id="AllCheck" class="btn" />&nbsp;&nbsp;<input type="submit"  value="反选" onclick="    Check(false); return false;" id="UnCheck" class="btn" /></td> 
        <td style="width:120px;height: 30px;"><input id="chkCheckedRange" type="checkbox"  onchange="CheckedRange()" />范围内全选定</td>        
        <td><asp:Button ID="btnSysCategory" runat="server" Text="确认对应系统分类" onclick="btnSysCategory_Click" CssClass="btn"/>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDeleteCategory" runat="server" Text="删除所有选定分类" onclick="btnDeleteCategory_Click" CssClass="btn"  OnClientClick="return confirm('您确认删除所有选定分类吗?');" />
            </td>
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
    <td align="right"><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="99%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="100" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>
</asp:Content>
