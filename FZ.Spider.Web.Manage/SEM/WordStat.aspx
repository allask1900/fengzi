<%@ Page Title="SEM工具\关键词统计报表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WordStat.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.WordStat" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style11
        {
            height:30px;
        }

    </style> 
    <script src="../javascript/calendar.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        bUseTime = false;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">SEM工具\关键词统计报表</div>
    <table align="center"  border="0" cellpadding="0" cellspacing="0" width="99%">
         
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.广告组</strong></span></td>
        </tr> 
    </table>
    <table  border="0" cellpadding="0" cellspacing="2" width="99%" align="center">
        <tr>
            <td class="style11" width="70px">广告系列</td>
            <td width="220px">                             
                <asp:DropDownList ID="ddlAdCategory" runat="server" CssClass="ddl" WIDTH="215px" onselectedindexchanged="ddlAdCategory_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
            </td>
            <td  width="70px" style="text-align: right">广告组:</td>       
            <td  width="220px"><asp:DropDownList ID="ddlAdGroup" CssClass="ddl" runat="server" Width="215px"  AutoPostBack="true" onselectedindexchanged="ddlAdGroup_SelectedIndexChanged"></asp:DropDownList></td>
            <td style="vertical-align:middle; width:70px;" class="style3">选择广告</td>
                         <td>
                            <asp:DropDownList ID="ddlAdList" runat="server" CssClass="ddl" style="vertical-align:middle;WIDTH: 315px" OnSelectedIndexChanged="ddlAdList_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
        </tr>
    </table> 
    <table align="center"  border="0" cellpadding="0" cellspacing="0" width="99%">         
        <tr>
            <td bgcolor="#eeeeee" height="22"><span class="STYLE13"><strong>2.关键词</strong></span></td>
        </tr> 
    </table>
    <table border="0" cellpadding="0" cellspacing="2"   width="99%" align="center">                      
        <tr>
            <td height="30" width="70px">词文本: </td>
            <td style="width:220px"><asp:DropDownList ID="ddlWords" CssClass="ddl" runat="server" Width="215px"  AutoPostBack="true"  onselectedindexchanged="ddlWords_SelectedIndexChanged"></asp:DropDownList></td>                         
            <td height="30" style=" text-align:right; width:70px;">单 价: </td>
            <td  width="220px">
                <asp:TextBox ID="txtClickPrice" runat="server" Width="210px" CssClass="Txt"></asp:TextBox>
            </td> 
            <td  style=" text-align:right; width:70px;">状态:&nbsp;&nbsp;</td>
            <td width="90px">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="80px" CssClass="ddl">
                    <asp:ListItem Value="0">新建</asp:ListItem>
                    <asp:ListItem Value="1">已审核</asp:ListItem>
                    <asp:ListItem Value="2">试用中</asp:ListItem>
                    <asp:ListItem Value="3">正式使用</asp:ListItem>
                    <asp:ListItem Value="4">废弃</asp:ListItem>
                </asp:DropDownList>
            </td>   
            <td><asp:Button ID="btnUpdate" runat="server" Text=" 修 改 "  CssClass="btn" onclick="btnUpdate_Click"/></td>                    
        </tr>
    </table>
    <table align="center"  border="0" cellpadding="0" cellspacing="0" width="99%">         
        <tr>
            <td bgcolor="#eeeeee" height="22"><span class="STYLE13"><strong>3.统计时间</strong></span></td>
        </tr> 
    </table>
    <table border="0" cellpadding="0" cellspacing="2" width="99%" align="center">
        <tr>
            <td height="30" style=" width:100px;">时间范围:</td>
            <td  width="220px">
                <asp:TextBox ID="txtFromTime" runat="server" onFocus="setday(this);" Width="210px" CssClass="Txt"></asp:TextBox>
            </td>
            <td style="width:100px; text-align:center">-至-</td>
            <td  width="220px"> 
                <asp:TextBox ID="txtToTime" runat="server" onFocus="setday(this);" 
                    Width="210px" CssClass="Txt" ></asp:TextBox>
            </td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text=" 查 询 "  CssClass="btn" onclick="btnSearch_Click"/>
            </td>
        </tr>
     </table>
    <table align="center"  border="0" cellpadding="0" cellspacing="0" width="99%">         
        <tr>
            <td bgcolor="#eeeeee" height="22"><span class="STYLE13"><strong>4.统计数据</strong></span></td>
        </tr> 
    </table>     
    <table   border="0" cellpadding="0" cellspacing="0" width="460px">
        <tr>
            <td>
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！"  
                      EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                         
                            <asp:BoundField DataField="PointID" HeaderText="监测点" ReadOnly="true">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>
                              <asp:BoundField DataField="pointName" HeaderText="监测点名称" ReadOnly="true">
                                 <HeaderStyle Width="150px" />
                                 <ItemStyle Width="150px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:TemplateField HeaderText="统计数">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("StatCount")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="IP数">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px"/>
                                <ItemTemplate>
                                    <%# Eval("IPCount")%>
                                </ItemTemplate>                        
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="用户数">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%# Eval("UserCount")%>
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