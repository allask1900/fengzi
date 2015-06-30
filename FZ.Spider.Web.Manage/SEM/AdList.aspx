<%@ Page Title="SEM工具\广告管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdList.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.AdList" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">SEM工具\广告管理</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
    <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.广告添加</strong></span> </td>
        </tr>
    <tr> 
            <td >
                <table  width="100%">
                    <tr>
                        <td style="width:85px" class="style3">选择广告系列:</td>                          
                        <td style="width:265px">
                            <asp:DropDownList ID="ddlAdCategory" runat="server" CssClass="ddl" Width="255px" onselectedindexchanged="ddlAdCategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td style="width:85px">选择广告组:</td>
                        <td style="width:265px">
                            <asp:DropDownList ID="ddlAdGroup" runat="server" CssClass="ddl" Width="255px" AutoPostBack="true" OnSelectedIndexChanged="ddlAdGroup_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td style="width:85px">广告类型:</td>
                        <td>
                            <asp:DropDownList ID="ddlAdType" runat="server" CssClass="ddl" Width="85px">
                                <asp:ListItem Value="1">编辑</asp:ListItem>
                                <asp:ListItem Value="2">搜索</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:85px" class="style3">广告名称:</td>                          
                        <td style="width:265px">
                            <asp:TextBox ID="txtAdTitle" runat="server" CssClass="Txt" Width="250px"></asp:TextBox>
                            
                        </td>
                        <td style="width:85px">页面名称:</td>                          
                        <td style="width:265px">
                            <asp:TextBox ID="txtPageLink" runat="server" CssClass="Txt" Width="250px"></asp:TextBox>                       
                        </td>
                        <td style="width:85px">备 注:</td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="Txt"  Width="250px"></asp:TextBox><asp:Literal ID="litAdID" runat="server" Visible="false"></asp:Literal>   
                        </td> 
                    </tr>                   
                    <tr>
                        <td style="width:85px">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text=" 添加 "  onclick="btnSave_Click" /> &nbsp; <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="取消" 
                onclick="btnCancel_Click" />
                        </td>
                       
                    </tr>
                </table>
            </td>             
        </tr>
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>2.广告列表</strong></span></td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！" 
                      EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound"         
                    OnRowUpdating="gvDataList_RowUpdating" 
                    OnRowDeleting="gvDataList_RowDeleting">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                        
                            <asp:BoundField DataField="AdID" ReadOnly="true" HeaderText="编号">
                                 <HeaderStyle Width="70px" />
                                 <ItemStyle Width="70px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="AdGroupID" ReadOnly="true" HeaderText="广告组">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:TemplateField  HeaderText="广告标题">
                                <HeaderStyle Width="250px" />
                                <ItemStyle Width="250px" />
                                <ItemTemplate>
                                    <%# Eval("AdTitle")%>
                                </ItemTemplate>                             
                             </asp:TemplateField>
                              
                             <asp:TemplateField  HeaderText="页面名称">
                                <HeaderStyle Width="350px" />
                                <ItemStyle Width="350px" />
                                <ItemTemplate>
                                    <asp:Literal ID="litPageName" runat="server"></asp:Literal>                                    
                                </ItemTemplate>                             
                             </asp:TemplateField>
                            <asp:TemplateField  HeaderText="备注">                                
                                <ItemTemplate>
                                    <%# Eval("Remark").ToString()%>
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
                       <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
            </td>
        </tr>
         <tr> 
            <td align="left"><webdiyer:aspnetpager id="pager" runat="server" Width="99%" CustomInfoSectionWidth="80%" ShowCustomInfoSection="left" NumericButtonTextFormatString="[{0}]" PageSize="15" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>