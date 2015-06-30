<%@ Page Title="SEM工具\监测点管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StatPoint.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.StatPoint" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">SEM工具\监测点管理</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.监测点添加</strong></span> </td>
        </tr>
        <tr> 
            <td >
                <table  width="100%">                   
                    <tr>
                        <td  width="120px">监测点名称:</td>                          
                        <td>
                            <asp:TextBox ID="txtPointName" runat="server" CssClass="Txt" Width="210px"></asp:TextBox>
                             <asp:Literal ID="litPointID" runat="server" Visible="false">0</asp:Literal>
                        </td>
                        
                    </tr>
                    <tr>
                        <td  width="60px">投放代码:</td>
                        <td>
                            <asp:TextBox ID="txtPointCode" runat="server" CssClass="Txt" TextMode="MultiLine" 
                                Width="500px" Height="50px"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td  width="60px">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 注:</td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="Txt" TextMode="MultiLine" 
                                Width="500px" Height="30px"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td  width="60px">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text=" 添加 "                      onclick="btnSave_Click" />
                        &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="取消" 
                onclick="btnCancel_Click" />
                        </td>
                       
                    </tr>
                </table>
            </td>             
        </tr>
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>2.监测点列表</strong></span></td>
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
                            <asp:BoundField DataField="PointID" ReadOnly="true" HeaderText="编号">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:TemplateField  HeaderText="监测点名称">
                                <HeaderStyle Width="200px" />
                                <ItemStyle Width="200px" />
                                <ItemTemplate>
                                    <%# Eval("PointName") %>
                                </ItemTemplate>                            
                             </asp:TemplateField>
                            <asp:TemplateField  HeaderText="投放代码">
                                <HeaderStyle Width="600px" />
                                <ItemStyle Width="600px" HorizontalAlign="Center"/>
                                <ItemTemplate>
                                     <asp:TextBox runat="server" CssClass="Txt" TextMode="MultiLine" Width="580px" Height="40px" ReadOnly="true" Text='<%# Eval("PointCode") %>'></asp:TextBox>
                                </ItemTemplate>                            
                             </asp:TemplateField>

                             <asp:TemplateField  HeaderText="备注">                            
                                <ItemTemplate>
                                    <%# Eval("Remark").ToString()%>
                                </ItemTemplate>                            
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="操作">
                                <HeaderStyle Width="120px" />
                                <ItemStyle Width="120px"  HorizontalAlign="Center"/>
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
            <td><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="15" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>