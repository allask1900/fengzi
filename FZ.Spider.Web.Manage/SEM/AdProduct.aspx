<%@ Page Title="SEM工具\编辑广告产品" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdProduct.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.AdProduct" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <div class="NavTitle"  width="80%">SEM工具\编辑广告产品</div>
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
                        <td style="width:85px">编辑类型广告:</td>
                        <td>
                            <asp:DropDownList ID="ddlAds" runat="server" CssClass="ddl" Width="255px" AutoPostBack="true" OnSelectedIndexChanged="ddlAds_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                   
                     <tr>
                         <td style="width:85px">添加产品:</td>
                         <td colspan="5"><asp:TextBox ID="txtProductIDs" runat="server" CssClass="Txt"  Width="960px" Height="30px" TextMode="MultiLine"></asp:TextBox></td>
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
                <span class="STYLE13"><strong>2.广告产品列表</strong></span></td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                <tr>
                    <td style="width:120px;height: 30px;"><input type="submit"  value="全选" onclick="Check(true); return false;" id="AllCheck" class="btn" />&nbsp;<input type="submit"  value="反选" onclick="    Check(false); return false;" id="UnCheck" class="btn" /></td> 
                    <td style="width:120px;height: 30px;"><input id="chkCheckedRange" type="checkbox"  onchange="CheckedRange();return false;" />范围内全选</td>                    
                    <td><asp:Button ID="btnDeleteAdProduct" runat="server" Text="删除选定产品" onclick="btnDeleteAdProduct_Click" CssClass="btn"  OnClientClick="return confirm('您确认删除选定产品吗?');" /></td>
              </tr>    
            </table>
             
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%" 
                      AutoGenerateColumns="False" EmptyDataText="没有相关数据！" 
                      EmptyDataRowStyle-ForeColor="red" 
                    OnRowDataBound="gvDataList_RowDataBound"       
                    OnRowDeleting="gvDataList_RowDeleting">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>                              
                            <asp:TemplateField HeaderText="编号">
                                <ItemStyle HorizontalAlign="Center" Width="80px"/>
                                <HeaderStyle HorizontalAlign="Center" />                                            
                                <ItemTemplate><asp:CheckBox runat="server" ID="OrdID" Text='<%# Bind("OrdID")%>'></asp:CheckBox></ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ProductID" ReadOnly="true" HeaderText="产品ID">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:BoundField DataField="AdID" ReadOnly="true" HeaderText="广告ID">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:TemplateField  HeaderText="产品名称">                                
                                <ItemTemplate>
                                    <asp:Literal ID="litFullName" runat="server"></asp:Literal>
                                </ItemTemplate>                             
                             </asp:TemplateField>
                             <asp:BoundField DataField="CategoryID"  HeaderText="产品分类">
                                <HeaderStyle Width="60px" />
                                <ItemStyle Width="60px" />                                                         
                             </asp:BoundField> 
                            <asp:BoundField DataField="UPCOrISBN"  HeaderText="UPCorISBN">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" />                                                         
                             </asp:BoundField>
                            <asp:BoundField DataField="OrgPrice" ReadOnly="true" HeaderText="OrgPrice">
                                 <HeaderStyle Width="65px" />
                                 <ItemStyle Width="65px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:BoundField DataField="Price" ReadOnly="true" HeaderText="Price">
                                 <HeaderStyle Width="65px" />
                                 <ItemStyle Width="65px" HorizontalAlign="Center" />
                             </asp:BoundField>
                            <asp:TemplateField  HeaderText="Discount">
                                 <HeaderStyle Width="65px" />
                                 <ItemStyle Width="65px" HorizontalAlign="Center" />
                                 <ItemTemplate>
                                     <asp:Literal ID="litDiscount" runat="server"></asp:Literal>
                                 </ItemTemplate>
                             </asp:TemplateField>
                            <asp:BoundField DataField="UsedPrice" ReadOnly="true" HeaderText="UsedPrice">
                                 <HeaderStyle Width="65px" />
                                 <ItemStyle Width="65px" HorizontalAlign="Center" />
                             </asp:BoundField>
                           
                            <asp:BoundField DataField="RentPrice" ReadOnly="true" HeaderText="RentPrice">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>  
                           <asp:TemplateField HeaderText="操作">
                                <HeaderStyle Width="50px" />
                                <ItemStyle Width="50px"  HorizontalAlign="Center"/>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" ForeColor="blue" CommandName="Delete" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false"  OnClientClick="return confirm('您确认删除该记录吗?');">删除</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
                       <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
            </td>
        </tr>
         <tr> 
            <td align="left"><webdiyer:aspnetpager id="pager" runat="server" Width="99%" CustomInfoSectionWidth="80%" ShowCustomInfoSection="left" NumericButtonTextFormatString="[{0}]" PageSize="50" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>