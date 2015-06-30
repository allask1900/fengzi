<%@ Page Title="系统管理-配置单管理" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true" CodeBehind="SystemConfig.aspx.cs" Inherits="FZ.Spider.Web.Manage.SystemConf.SystemConfig" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">系统管理/配置单管理</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">         
        <tr>
            <td bgcolor="#eeeeee" height="22"><span class="STYLE13"><strong>1.选择系统</strong></span></td>
        </tr>
        <tr>
            <td height="22">
                <table border="0" cellpadding="0"  cellspacing="2"  width="100%">
                    <tr>
                        <td style="vertical-align:middle" class="style3"  width="90" >所属系统*</td>
                        <td  style="vertical-align:middle" class="style3">
                            <asp:DropDownList ID="ddlSystem" runat="server"   CssClass="ddl"
                                style="vertical-align:middle;WIDTH: 215px" 
                                onselectedindexchanged="ddlSystem_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>                             
                            <asp:Literal ID="litConfID" runat="server" Visible="false"></asp:Literal>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table> 
    
  
    <table align="center" border="0" cellpadding="0" cellspacing="0"  width="99%">
        <tr><td  bgcolor="#eeeeee"><span class="STYLE13"><strong>2.配置单编辑</strong></span></td></tr>
        <tr>
            <td>
                <table border="0" cellpadding="0"  cellspacing="2"  width="100%">
                    <tr>
                        <td width="90" style="vertical-align:middle; height:25px">配置单名称*</td>
                        <td style="vertical-align:middle">
                            <asp:TextBox ID="txtConfName" CssClass="Txt" runat="server" 
                                style="WIDTH: 210px" ></asp:TextBox>
                        (名称格式:"系统.应用",如:"search.spider")</td>
                    </tr>
                    <tr>
                        <td width="90" style="vertical-align:middle; height:25px">状态</td>
                        <td style="vertical-align:middle">
                                                
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" 
                                style="vertical-align:middle;WIDTH: 215px"> 
                            <asp:ListItem Text="未生效" Value="False"></asp:ListItem>
                            <asp:ListItem Text="已生效" Value="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                        <td width="90" style="vertical-align:middle; height:25px">说明</td>
                        <td style="vertical-align:middle">
                            <asp:TextBox ID="txtDescription" CssClass="Txt" style="WIDTH: 545px; HEIGHT: 45px" 
                                runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td> 
                <table border="0" cellpadding="0"  cellspacing="2"  width="100%">
                              <tr>
                                <td  width="90" >
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnSave" CssClass="btn" runat="server" style="WIDTH: 48px"  
                                        Text="保存" onclick="btnSave_Click" /> &nbsp;&nbsp;
                            <asp:Button ID="btnCancel"  CssClass="btn" runat="server" Text="取消"                        onclick="btnCancel_Click" />
                                </td>
                              </tr>
                              </table> 
            </td>
        </tr>
        <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" width="99%"  style="margin-left:5px;" >        
            <tr>
                <td  bgcolor="#eeeeee"><span class="STYLE13"><strong>3.配置单列表及编辑</strong></span></td>
            </tr>
        </table>
       
        <table border="0" cellspacing="0"  style="WIDTH: 100%;">
                    <tr>
                        <td>
                   <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！"  
                      EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound"  OnRowUpdating="gvDataList_RowUpdating"  OnRowDeleting="gvDataList_RowDeleteing">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                        
                            <asp:BoundField DataField="ConfID" HeaderText="配置单编号">
                                 <HeaderStyle Width="70px" />
                                 <ItemStyle Width="70px"/>
                             </asp:BoundField>
                             <asp:BoundField DataField="ConfName" HeaderText="名称">
                                 <HeaderStyle Width="150px" />
                                 <ItemStyle Width="150px"/>
                             </asp:BoundField>

                             <asp:BoundField DataField="SysID" HeaderText="系统编号">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" />
                             </asp:BoundField>

                            <asp:TemplateField HeaderText="状态">
                            <HeaderStyle Width="50px" />
                                 <ItemStyle  HorizontalAlign="Center" Width="50px" />
                                 <ItemTemplate>
                                     <%# Eval("isvalid").ToString() == "True" ? "已生效" : "未生效"%>
                                 </ItemTemplate>
                             </asp:TemplateField>

                             <asp:BoundField DataField="LastChangeTime" HeaderText="更新时间">
                                <HeaderStyle Width="120px" />
                                 <ItemStyle  HorizontalAlign="Center" Width="120px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="说明"/>
                            <asp:TemplateField HeaderText="操作">
                             <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center"  />
                              <ItemTemplate> 
                                <asp:LinkButton ID="btnUpdate" runat="server" ForeColor="blue" CommandName="Update"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false">修改</asp:LinkButton>                              &nbsp;&nbsp;&nbsp;&nbsp;
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