<%@ Page Title="搜索引擎系统\产品管理\产品推荐编辑" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductEditorType.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.ProductEditorType" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width:70px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">搜索引擎系统\产品管理\产品推荐分类管理</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">

    <tr>
        <td bgcolor="#eeeeee" height="22">
            <table  width="100%">
                <tr>
                    <td>
                        <span class="STYLE13"><strong>1.添加分类</strong></span></td>
                </tr>
            </table>
        </td>
    </tr> 
    <tr> 
            <td >
                <table width="100%">
                    <tr>
                        <td class="auto-style1">推荐分类:</td>                          
                        <td>
                            <asp:DropDownList ID="ddlEditorType" runat="server" CssClass="ddl"  Width="400px" OnSelectedIndexChanged="ddlEditorType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                           
                        </td>                        
                    </tr>
                    <tr>
                        <td class="auto-style1">分类名称:</td>                          
                        <td>
                            <asp:TextBox ID="txtEditorTypeName" runat="server" CssClass="Txt" Width="550px"></asp:TextBox>                            
                        </td>                        
                    </tr>
                    <tr>
                        <td class="auto-style1">备&nbsp;&nbsp;&nbsp;&nbsp;注:</td>                          
                        <td>
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="Txt" Width="550px"></asp:TextBox>                            
                        </td>                        
                    </tr>
                     <tr>
                        <td class="auto-style1">状&nbsp;&nbsp;&nbsp;&nbsp;态:</td>                          
                        <td>
                                                
                            <asp:DropDownList ID="ddlIsValid" runat="server" CssClass="ddl" Width="120px">
                                <asp:ListItem Value="1">有效</asp:ListItem>
                                <asp:ListItem Value="0">无效</asp:ListItem>
                            </asp:DropDownList>
                                                
                        </td>                        
                    </tr>
                    <tr>
                        <td >&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn" Text=" 添加 " onclick="btnSave_Click" />
                        &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="取消" onclick="btnCancel_Click" />
                        </td>                       
                    </tr>
                </table>
            </td>             
        </tr>
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13" ><strong>2.分类列表</strong></span></td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="900px" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！" 
                      EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound"                 
                    OnRowDeleting="gvDataList_RowDeleting">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                        
                            <asp:BoundField DataField="EditorTypeID" ReadOnly="true" HeaderText="分类编号">
                                 <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="EditorTypeName" ReadOnly="true" HeaderText="分类名称">
                                 <HeaderStyle Width="420px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="Remark" ReadOnly="true" HeaderText="备注">
                                 <HeaderStyle Width="320px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField> 
                            <asp:BoundField DataField="IsValid" ReadOnly="true" HeaderText="状态">
                                 <HeaderStyle Width="55px" />
                                 <ItemStyle Width="55px" HorizontalAlign="Center" />
                             </asp:BoundField>                            
                           <asp:TemplateField HeaderText="操作">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px"  HorizontalAlign="Center"/>
                                <ItemTemplate>
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
            <td align="left"><webdiyer:aspnetpager id="pager" runat="server" Width="900px" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="50" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>