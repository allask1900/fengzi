<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SysLogList.aspx.cs" Inherits="FZ.Spider.Web.Manage.Logging.SysLogList" Title="搜索引擎系统\系统日志列表" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
     .style11
        {
            height: 40px;
        }
</style>
    <script src="../javascript/calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        bUseTime = false;
        function openWindow(url) {
            window.open(url, 'window', 'height=900,width=1200, left=150,toolbar=no,menubar=no,scrollbars=yes, resizable=no,location=no, status=no')
            return false;
        }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NavTitle">搜索引擎系统\系统日志列表</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 1、选择查询条件</strong></span></td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
    <tr>
        <td class="style11" width="60px">选择应用:</td>
        <td width="100px"><asp:DropDownList ID="dropApplication" runat="server" CssClass="ddl" Width="95px"></asp:DropDownList></td>
        <td  style=" width:40px;">级别:</td>
        <td  style=" width:65px;"><asp:DropDownList ID="dropLevel" runat="server"  CssClass="ddl" Width="60px">
            <asp:ListItem>ALL</asp:ListItem>
            <asp:ListItem>ERROR</asp:ListItem>
            <asp:ListItem>INFO</asp:ListItem>
            <asp:ListItem>WARN</asp:ListItem>
            <asp:ListItem>DEBUG</asp:ListItem>
            </asp:DropDownList></td>
       
        <td  style=" width:40px;">时间:</td>
        <td  width="165px"><asp:TextBox ID="txtFromTime" runat="server" onFocus="setday(this);" Width="160px" CssClass="Txt"></asp:TextBox></td>
        <td style="width:40px; text-align:center">-至-</td>
        <td  width="165px"><asp:TextBox ID="txtToTime" runat="server" onFocus="setday(this);" Width="160px" CssClass="Txt" ></asp:TextBox></td>
        <td  style=" width:40px;">类型:</td>
        <td  style=" width:100px;"><asp:TextBox ID="txtSearchClass" runat="server" Width="100px" CssClass="Txt"></asp:TextBox></td>
        <td  style=" width:40px;">方法:</td>
        <td  style=" width:100px;"><asp:TextBox ID="txtSearchMethod" runat="server" Width="100px" CssClass="Txt"></asp:TextBox></td>
        <td  style=" width:40px;">站点:</td>
        <td  style=" width:100px;"><asp:TextBox ID="txtSiteName" runat="server" Width="100px" CssClass="Txt"></asp:TextBox></td>
        <td> <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " onclick="btnSearch_Click" CssClass="btn"/></td>
    </tr>
    
    
</table>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
    <tr>
        <td height="22" width="82%" style="width: 100%"><span class="STYLE13"><strong> 2、系统日志列表</strong></span></td>
    </tr>
</table>

<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
<tr>
    <td align="left">
        <asp:GridView ID="gvDataList" CssClass="TB_Grid_log" runat="server" Width="99%" AutoGenerateColumns="False" EmptyDataText="没有相关数据！" EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound" EnableModelValidation="True">
            <HeaderStyle CssClass="STYLE13" />
            <AlternatingRowStyle BackColor="#EEEEEE" />
            <Columns>
                <asp:BoundField DataField="LogID" ReadOnly="true" HeaderText="编号">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:BoundField DataField="APPID" ReadOnly="true" HeaderText="应用">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                 </asp:BoundField>               
                <asp:TemplateField HeaderText="日期">
                    <HeaderStyle Width="140px" />
                    <ItemStyle Width="140px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("LogDate")).ToString("yyyy-MM-dd HH:mm:ss") %>                
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:BoundField DataField="LogLevel" ReadOnly="true" HeaderText="级别">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px"/>
                 </asp:BoundField>
                <asp:BoundField DataField="Class" ReadOnly="true" HeaderText="类型">
                    <HeaderStyle Width="260px" />
                    <ItemStyle Width="260px"/>
                 </asp:BoundField>
                <asp:BoundField DataField="Method" ReadOnly="true" HeaderText="方法">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px"/>
                 </asp:BoundField>
                <asp:BoundField DataField="SiteName" ReadOnly="true" HeaderText="站点">
                    <HeaderStyle Width="100px" />
                    <ItemStyle Width="100px"/>
                 </asp:BoundField>
                 <asp:BoundField DataField="Message" ReadOnly="true" HeaderText="信息" ItemStyle-CssClass="WordsBreak" ItemStyle-Wrap="true"/>   
                <asp:TemplateField HeaderText="详细异常">
                    <HeaderStyle Width="90px" />
                    <ItemStyle Width="90px" HorizontalAlign="Center"/>
                    <ItemTemplate>
                        <asp:HyperLink ID="linkException" onClick= <%# "return openWindow('logview.aspx?logid=" + Eval("logid") + "')" %>  runat="server" NavigateUrl="#" Text="Exception"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>              
            </Columns>
            <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </td>
</tr>
<tr> 
    <td align=right><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="99%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="50" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>
</asp:Content>
