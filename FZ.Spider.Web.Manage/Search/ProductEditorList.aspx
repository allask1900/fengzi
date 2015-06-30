<%@ Page Title="搜索引擎系统\产品管理\产品推荐选择" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductEditorList.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.ProductEditorList" ValidateRequest="false" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width:100px;
            height: 30px;
        }
    </style>
     <script type="text/javascript">
         function Check(ck) {
             var gv = document.getElementById('MainContent_gvDataList');
             var inp = gv.getElementsByTagName("input");
             if (ck) {
                 for (var i = 0; i < inp.length; i++) {
                     if (inp[i].type == 'checkbox')
                         inp[i].checked = true;
                 }
             }
             else {
                 for (var i = 0; i < inp.length; i++) {
                     if (inp[i].type == 'checkbox') {
                         if (inp[i].checked)
                             inp[i].checked = false;
                         else
                             inp[i].checked = true;
                     }
                 }
             }
             return false;
         }
         function CheckedRange() {

             var gv = document.getElementById('MainContent_gvDataList');
             var inp = gv.getElementsByTagName("input");

             var chk = false;
             for (var i = 0; i < inp.length; i++) {
                 if (inp[i].type == 'checkbox') {
                     if (inp[i].checked && chk) {
                         chk = false;
                         return false;
                     }
                     if (chk)
                         inp[i].checked = true;

                     if (inp[i].checked) {
                         chk = true;
                     }

                 }
             }
             return false;
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle"  width="80%">搜索引擎系统\产品管理\产品推荐选择</div>
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
                        <td> <asp:DropDownList ID="ddlEditorType" runat="server" CssClass="ddl"  Width="200px"></asp:DropDownList>(IndexRecommended | IndexBestSellers不用选分类，CategoryRecommended|CategoryBestSellers必选分类)</td>                        
                    </tr>
                    <tr>
                        <td   class="auto-style1">所属分类:</td>                          
                        <td>
                            <asp:DropDownList ID="dropFirstCategory" runat="server" CssClass="ddl"  Width="200px"></asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style1">产品推荐配置：</td>                          
                        <td>
                            <asp:TextBox ID="txtRecommentProductConfigs" runat="server" CssClass="Txt" Width="850px" TextMode="MultiLine" Height="100px"></asp:TextBox>                            
                            </td>
                        
                    </tr>
                     
                    <tr>
                        <td >&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSaveRecommentProductConfig" runat="server" CssClass="btn" Text="保存获取分类爬虫配置" onclick="btnSaveRecommentProductConfig_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnGetCategoryRecommentProduct" runat="server" CssClass="btn" Text="获取分类推荐产品" onclick="btnGetCategoryRecommentProduct_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnGetAllCategoryRecommentProduct" runat="server" CssClass="btn" Text="获取全部推荐产品" 
                onclick="btnbtnGetAllCategoryRecommentProduct_Click" />
                        </td>
                       
                    </tr>
                </table>
            </td>             
        </tr>
        <tr>
            <td bgcolor="#eeeeee" height="22" align="left">
                <table>
                    <tr>
                        <td style="width:80px"><span class="STYLE13" ><strong>2.产品列表</strong></span></td>
                        <td><asp:Button ID="btnSysCategory" runat="server" Text="确认推荐产品" onclick="btnSysCategory_Click" CssClass="btn"/></td>
                    </tr>
                </table>  
            </td>              
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="100%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！" 
                      EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                        
                            <asp:TemplateField HeaderText="产品ID">
                                <ItemStyle HorizontalAlign="Center" Width="100px"/>
                                <HeaderStyle HorizontalAlign="Center" />                                            
                                <ItemTemplate><asp:CheckBox runat="server" ID="ProductID" Text='<%# Bind("ProductID")%>'></asp:CheckBox></ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="FullName" ReadOnly="true" HeaderText="产品名称">                                  
                                 <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>
                             <asp:BoundField DataField="CategoryID" ReadOnly="true" HeaderText="分类ID">
                                 <HeaderStyle Width="55px" />
                                 <ItemStyle Width="55px" HorizontalAlign="Center" />
                             </asp:BoundField>     
                            <asp:BoundField DataField="IsValid" ReadOnly="true" HeaderText="IsValid">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>     
                            <asp:BoundField DataField="ImageType" ReadOnly="true" HeaderText="Image">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>                       
                            <asp:BoundField DataField="ShopCount" ReadOnly="true" HeaderText="商家数">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="CommentCount" ReadOnly="true" HeaderText="评论数">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="ScoreCount" ReadOnly="true" HeaderText="评分数">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:TemplateField HeaderText="分数">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" /> 
                                <ItemTemplate><%#Convert.ToSingle(Eval("Score"))/Convert.ToSingle(Eval("ScoreCount")) %></ItemTemplate>
                             </asp:TemplateField> 
                             <asp:BoundField DataField="MinPrice" ReadOnly="true" HeaderText="MinPrice">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="MaxPrice" ReadOnly="true" HeaderText="MaxPrice">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
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
    </table>
</asp:Content>