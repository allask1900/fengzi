<%@ Page Title="SEM工具\广告系列管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdCategory.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.AdCategory" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">SEM工具\广告系列管理</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
    <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.广告系列添加</strong></span> </td>
        </tr>
        <tr> 
            <td >
                <table  width="100%">
                    <tr>
                        <td  width="90px">广告系列名称:</td>                          
                        <td>
                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="Txt" Width="160px"></asp:TextBox>
                        </td>
                        
                    </tr>
                     <tr>
                         <td  width="90px">所属分类:</td>
                         <td><asp:DropDownList ID="ddlSysCategory_1" runat="server"  CssClass="ddl"  Width="160px"></asp:DropDownList></td> 
                    </tr>
                    <tr>
                        <td  width="60px">备&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 注:</td>
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="Txt" TextMode="MultiLine" 
                                Width="454px" Height="30px"></asp:TextBox>
                        </td>
                       
                    </tr>
                    <tr>
                        <td  width="60px">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text=" 添加 " onclick="btnSave_Click" />
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
                <span class="STYLE13"><strong>2.广告系列列表</strong></span></td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！" 
                      EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound"         
                    OnRowUpdating="gvDataList_RowUpdating" 
                    OnRowDeleting="gvDataList_RowDeleting"
                    OnRowCancelingEdit="gvDataList_RowCancelingEdit"
                    OnRowEditing="gvDataList_RowEditing"
                     EnableModelValidation="True">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                        
                            <asp:BoundField DataField="AdCategoryID" ReadOnly="true" HeaderText="类别编号">
                                 <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            
                            <asp:TemplateField  HeaderText="所属分类">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />
                                <ItemTemplate>
                                    <%# Eval("CategoryID")%>
                                </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:TextBox ID="txtEditCategoryID" Text='<%# Eval("CategoryID") %>' runat="server" Width="80px" />
                              </EditItemTemplate>
                             </asp:TemplateField>

                             <asp:TemplateField  HeaderText="类别名称">
                                <HeaderStyle Width="300px" />
                                <ItemStyle Width="300px" />
                                <ItemTemplate>
                                    <%# Eval("AdCategoryName")%>
                                </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:TextBox ID="txtEditAdCategoryName" Text='<%# Eval("AdCategoryName") %>' runat="server" Width="300px" />
                              </EditItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField  HeaderText="备注">                               
                                <ItemTemplate>
                                    <%# Eval("remark").ToString()%>
                                </ItemTemplate>
                             <EditItemTemplate>
                                 <asp:TextBox ID="txtEditRemark" Text='<%# Eval("Remark") %>' runat="server" Width="600px" />
                              </EditItemTemplate>
                             </asp:TemplateField>
                               
                            <asp:CommandField ItemStyle-HorizontalAlign="Center" 
                                ShowEditButton="True" EditText="编辑" HeaderText="更新" 
                                CancelText="取消"  UpdateText="更新" DeleteText=""> 
                                <HeaderStyle Width="90px" />                           
                                <ItemStyle Width="90px" HorizontalAlign="Center"/>
                                
                            </asp:CommandField>  
                            <asp:TemplateField HeaderText="删除">
                              <HeaderStyle Width="50px" />                           
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
            <td align=right><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="99%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="15" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>