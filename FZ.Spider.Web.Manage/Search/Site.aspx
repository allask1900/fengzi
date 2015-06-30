<%@ Page Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="Site.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.Site" Title="搜索引擎系统\网站管理" %>
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
<div class="NavTitle">搜索引擎系统\网站管理</div>
<table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0"  style="margin-left:5px;" width="99%">
                    <tr>
                        <td height="22" width="82%" style="width: 100%">
                            <span class="STYLE13"><strong> 1、添加站点</strong></span></td>
                    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="2" width="99%"  style="margin-left:5px;" >
    <tr>
        <td class="style11" style="width:80px">主域</td>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width:215px" class="style11"><asp:TextBox ID="txtSiteDomain" runat="server"  CssClass="Txt"  style="WIDTH: 210px" ></asp:TextBox></td>
                    <td style="width:75px;" align="center" valign="middle"><asp:Button ID="btnCheck" runat="server" Text="检查" Width="50px" CssClass="btn" OnClick="btnCheck_Click" /></td>                    
                    <td style="width:60px" align="center">站点名称</td>
                    <td style="width:215px"><asp:TextBox ID="txtSiteName" runat="server"  CssClass="Txt"  style="WIDTH: 210px"></asp:TextBox></td>
                    
                    <td style="width:65px">站点编码</td>
                    <td style="width:125px"> 
                        <asp:DropDownList ID="ddlCharSet" runat="server"  CssClass="ddl"  Width="110px" >
                            <asp:ListItem Value="UTF-8">默认</asp:ListItem>
                            <asp:ListItem Value="UTF-8">UTF8</asp:ListItem>
                            <asp:ListItem Value="GB2312">GB2312</asp:ListItem>
                        </asp:DropDownList>            
                    </td>
                    <td style="width:65px">信息等级</td>
                    <td>
                        <asp:DropDownList ID="ddlLevel" runat="server"  CssClass="ddl"  Width="65px">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                        </asp:DropDownList>&nbsp;<asp:Literal ID="litSiteID" runat="server" Visible="False"></asp:Literal>
                    </td>
                   
                </tr>
            </table>
       </td>
    </tr>
    
    <tr>
        <td  style="width:80px; height:35px">LOGO(URL)</td>
        <td class="style11">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width:215px" class="style11"><asp:TextBox ID="txtSiteLogo" runat="server"  CssClass="Txt"  style="WIDTH: 210px" ></asp:TextBox></td>
                    <td style="width:85px" align="center">或者上传</td>
                    <td valign="middle" style="width:230px"><asp:FileUpload ID="FileUpload1" runat="server" Width="225px" CssClass="btn" /></td>
                    <td style="width:180px;" align="center">站点抓取频率设置(页面数)</td>
                    <td style="width:85px"><asp:TextBox ID="txtSpiderReadCount" runat="server"  CssClass="Txt"  style="WIDTH: 80px" ></asp:TextBox></td>
                    <td style="width:140px;" align="center">停顿时间(单位秒)</td>
                    <td><asp:TextBox ID="txtSpiderSleepTime" runat="server"  CssClass="Txt"  style="WIDTH: 80px" ></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>       
    <tr>
        <td  style="width:80px">合作站点</td>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width:215px"  class="style11"> 
                        <asp:DropDownList ID="ddlIsPartner" runat="server" Width="210px" CssClass="ddl">                            
                            <asp:ListItem Value="0">不是</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width:230px; text-align:right">产品排序分值(整数,越大排名越前):  </td>
                    <td style="text-align:left"> <asp:TextBox ID="txtProductSequency" runat="server"></asp:TextBox>
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
        <td  style="width:80px">简介</td>
        <td>
            <asp:TextBox ID="txtSiteDescription" runat="server" CssClass="Txt" TextMode="MultiLine" 
                style="WIDTH: 1000px; HEIGHT:40px"></asp:TextBox>
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
        <td height="22" style="width: 100px">
            <span class="STYLE13"><strong> 2、网站列表</strong></span></td>
        <td style="width: 280px"><asp:DropDownList ID="dropFirstCategory" runat="server" Width="250px" OnSelectedIndexChanged="dropFirstCategory_SelectedIndexChanged" AutoPostBack="True" CssClass="ddl"></asp:DropDownList></td>
        <td><asp:RadioButtonList ID="rblStatus" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblStatus_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="notuse">排除弃用站点</asp:ListItem>
            <asp:ListItem Value="all">全部</asp:ListItem>
            <asp:ListItem Value="1">通过</asp:ListItem>
            <asp:ListItem Value="0">初始状态</asp:ListItem>
            <asp:ListItem Value="2">不抓取</asp:ListItem>
            
            </asp:RadioButtonList></td>
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
            <asp:BoundField DataField="SiteID" HeaderText="站点ID">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center"/>
                </asp:BoundField>                                         
            <asp:BoundField DataField="SiteName" HeaderText="名称">
            <HeaderStyle Width="150px" />
                    <ItemStyle Width="150px" />
                </asp:BoundField>           
            <asp:TemplateField HeaderText="主域">
                    <HeaderStyle HorizontalAlign="Center" /> 
                    <ItemStyle Width="200px" />                                           
                    <ItemTemplate>
                        <a href='http://<%#Eval("SiteDomain") %>' target="_blank"><%#Eval("SiteDomain")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="SiteProductInfoLevel" HeaderText="信息等级">
            <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:BoundField>   
             <asp:BoundField DataField="Rank" HeaderText="排名">
            <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" />
                </asp:BoundField> 
             <asp:TemplateField HeaderText="站点描述">
                    <HeaderStyle HorizontalAlign="Center" />                                            
                    <ItemTemplate><%#FZ.Spider.Common.StringHelper.GetTitleByLen(Eval("SiteDescription").ToString(),120,true) %></ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <HeaderStyle Width="150px" />
                    <ItemStyle Width="150px"  HorizontalAlign="Center"/>
                <ItemTemplate> 
                    <asp:Button ID="btnStatusToNotUse" runat="server" Text="不抓取" CommandArgument='<%# Eval("SiteID")%>' OnClick="btnStatusToNotUse_Click" />
                    &nbsp;&nbsp; 
                    <asp:Button ID="btnStatusToUse" runat="server" Text="通过" CommandArgument='<%# Eval("SiteID")%>' OnClick="btnStatusToUse_Click" />
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
    <td><webdiyer:aspnetpager id="pager" runat="server"  Wrap="true" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="30" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
</tr>
</table>

 
 </asp:Content>