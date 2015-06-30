<%@ Page Title="SEM工具\关键词编辑" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WordEdit.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.WordEdit" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <SCRIPT type="text/javascript"> 
     function openWindow(url)
     {
         window.open(url, 'window', 'height=700,width=1050,top=150,left=150,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no')
         return false;
     } 
</SCRIPT>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td>
    <div class="NavTitle">SEM工具\关键词编辑</div>     
    <table align="center" border="0" bgcolor="#eeeeee" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td height="30" style="width:60px">&nbsp;广告系列 </td>
                        <td style="width:200px"><asp:DropDownList ID="ddlAdCategory" CssClass="ddl" 
                                runat="server" Width="180px" 
                                onselectedindexchanged="ddlAdCategory_SelectedIndexChanged"  AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="width:60px; text-align:right">广告组</td>
                        <td style="width:200px"><asp:DropDownList ID="ddlAdGroup" CssClass="ddl"   AutoPostBack="true"
                                runat="server" Width="180px" 
                                onselectedindexchanged="ddlAdGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="vertical-align:middle; width:80px;" class="style3">选择广告</td>
                         <td>
                            <asp:DropDownList ID="ddlAdList" runat="server" CssClass="ddl" style="vertical-align:middle;WIDTH: 315px" OnSelectedIndexChanged="ddlAdList_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="width:60px; text-align:right">关键词 </td>
                        <td Width="120px"><asp:TextBox ID="txtKeyWord" runat="server" Width="90px" CssClass="Txt"></asp:TextBox></td>
                        <td> 
                            <asp:Button CssClass="btn" ID="btnSearch" runat="server" Text=" 查 询 " 
                                onclick="btnSearch_Click"/>
                        </td>
                    </tr>
                    </table>             
    <table  align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！"  
                      EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound"         OnRowUpdating="gvDataList_RowUpdating" OnRowDeleting="gvDataList_RowDeleting" OnRowCancelingEdit="gvDataList_RowCancelingEdit" OnRowEditing="gvDataList_RowEditing" >
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                         
                            <asp:BoundField DataField="WordID" HeaderText="序号" ReadOnly="true">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField> 
                              <asp:BoundField DataField="AdID" HeaderText="广告" ReadOnly="true">
                                 <HeaderStyle Width="130px" />
                                 <ItemStyle Width="130px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:TemplateField HeaderText="词文本">
                                <HeaderStyle Width="500px" />
                                <ItemStyle Width="500px"/>
                                <ItemTemplate>
                                <%# Eval("WordText")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:TextBox ID="txtEditWordText" Text='<%# Eval("WordText") %>' runat="server" Width="480px" />
                              </EditItemTemplate>
                             </asp:TemplateField> 
                            <asp:TemplateField HeaderText="单价">
                                     <HeaderStyle Width="60px" />
                                     <ItemStyle Width="60px" HorizontalAlign="Center" />                                 
                                    <ItemTemplate>
                                       <%# Eval("ClickPrice")%>
                                     </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="状态">
                                     <HeaderStyle Width="50px" />
                                     <ItemStyle Width="50px" HorizontalAlign="Center" />                                 
                                    <ItemTemplate>
                                       <%# FZ.Spider.Common.CommonFun.GetStatusName(Convert.ToInt32(Eval("status")))%>
                                     </ItemTemplate>
                                </asp:TemplateField>  
                            <asp:TemplateField HeaderText="查看统计">
                                     <HeaderStyle Width="60px"  HorizontalAlign="Center" />
                                     <ItemStyle Width="60px" HorizontalAlign="Center" />                                 
                                    <ItemTemplate>
                                        <a href="#" onclick="return openWindow('<%# Eval("WordID","wordstat.aspx?wordid={0}")%>')">查看统计</a>                                 
                                     </ItemTemplate>
                                </asp:TemplateField>                 
                             <asp:CommandField ItemStyle-HorizontalAlign="Right" 
                                ShowEditButton="True" EditText="编辑" HeaderText="更新" 
                                CancelText="取消"  UpdateText="更新" DeleteText="">   
                                <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                <ItemStyle Width="70px"  HorizontalAlign="Right"/>                                
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" HorizontalAlign="Center"/>
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
            <td><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="20" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</td>
</tr>
</table>
</asp:Content>