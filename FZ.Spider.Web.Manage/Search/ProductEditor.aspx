<%@ Page Title="搜索引擎系统\产品管理\产品推荐编辑" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductEditor.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.ProductEditor" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width:120px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">搜索引擎系统\产品管理\产品推荐编辑</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
    <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.产品添加</strong></span> </td>
        </tr>
    <tr> 
            <td >
                <table  width="100%">
                    <tr>
                        <td    class="auto-style1">推荐分类:</td>                          
                        <td>
                            <asp:DropDownList ID="ddlEditorType" runat="server" CssClass="ddl"  Width="200px"></asp:DropDownList>                           
                             (IndexRecommended | IndexBestSellers不用选分类，CategoryRecommended|CategoryBestSellers必选分类)</td>
                        
                    </tr>
                    <tr>
                        <td   class="auto-style1">所属分类:</td>                          
                        <td>
                            <asp:DropDownList ID="dropFirstCategory" runat="server" CssClass="ddl"  Width="200px"></asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style1">编辑产品ID:</td>                          
                        <td>
                            <asp:TextBox ID="txtProductIDS" runat="server" CssClass="Txt" Width="550px" TextMode="MultiLine"></asp:TextBox>                            
                            多个以&quot;,&quot;分隔</td>
                        
                    </tr>
                     
                    <tr>
                        <td >&nbsp;</td>
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
                <span class="STYLE13" ><strong>2.编辑产品列表</strong></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="dropCategory" runat="server" CssClass="ddl"  Width="200px" AutoPostBack="true"  OnSelectedIndexChanged="dropCategory_SelectedIndexChanged"></asp:DropDownList> <asp:DropDownList ID="dropEditorType" runat="server" AutoPostBack="true" CssClass="ddl"  Width="200px" OnSelectedIndexChanged="dropEditorType_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="100%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！" 
                      EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound"                 
                    OnRowDeleting="gvDataList_RowDeleting">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                        
                            <asp:BoundField DataField="OrdID" ReadOnly="true" HeaderText="编号">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="FullName" ReadOnly="true" HeaderText="产品名称">                                  
                                 <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                             <asp:BoundField DataField="CategoryID" ReadOnly="true" HeaderText="分类ID">
                                 <HeaderStyle Width="55px" />
                                 <ItemStyle Width="55px" HorizontalAlign="Center" />
                             </asp:BoundField>                            
                            <asp:BoundField DataField="EditorTypeName" ReadOnly="true" HeaderText="推荐分类">
                                 <HeaderStyle Width="150px" />
                                 <ItemStyle Width="150px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:BoundField DataField="CheckInTime" ReadOnly="true" HeaderText="更新时间">
                                 <HeaderStyle Width="150px" />
                                 <ItemStyle Width="150px" HorizontalAlign="Center" />
                             </asp:BoundField>
                           <asp:TemplateField HeaderText="操作">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px"  HorizontalAlign="Center"/>
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
            <td align="left"><webdiyer:aspnetpager id="pager" runat="server" Width="100%" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="50" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>