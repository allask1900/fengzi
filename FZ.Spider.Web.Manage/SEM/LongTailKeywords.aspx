<%@ Page Title="SEM工具\长尾词分析" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LongTailKeywords.aspx.cs" Inherits="FZ.Spider.Web.Manage.SEM.LongTailKeywords" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width:120px;
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
    <div class="NavTitle"  width="80%">SEM工具\长尾词分析</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
    <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.长尾词添加</strong></span> </td>
        </tr>
    <tr> 
            <td >
                <table  width="100%">                    
                    <tr>
                        <td   class="auto-style1">所属分类:</td>                          
                        <td align="left">
                            <table>
                                <tr>
                                     <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_1" runat="server" OnSelectedIndexChanged="ddlSysCategory_1_SelectedIndexChanged" CssClass="ddl"  Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_2" runat="server" OnSelectedIndexChanged="ddlSysCategory_2_SelectedIndexChanged" CssClass="ddl" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_3" runat="server" OnSelectedIndexChanged="ddlSysCategory_3_SelectedIndexChanged" CssClass="ddl" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                        <td style="width:160px"><asp:DropDownList ID="ddlSysCategory_4" runat="server" CssClass="ddl" Width="150px"></asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style1">核心词(词源):</td>                          
                        <td> <asp:TextBox ID="txtSourceKeywords" runat="server" CssClass="Txt" Width="550px"></asp:TextBox> </td>                        
                    </tr>                     
                    <tr>
                        <td class="auto-style1">长尾词:</td>                          
                        <td> <asp:TextBox ID="txtLongTailKeywords" runat="server" CssClass="Txt" Width="550px" Height="60px" TextMode="MultiLine"></asp:TextBox>多个以换行分隔</td>                   
                    </tr>                     
                    <tr>
                        <td >&nbsp;</td>
                        <td><asp:Button ID="btnSave" runat="server" CssClass="btn" Text=" 添加 " onclick="btnSave_Click" />   &nbsp; <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="取消" onclick="btnCancel_Click" />
                        </td>
                       
                    </tr>
                </table>
            </td>             
        </tr>
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width:150px; height:35px"><span class="STYLE13" ><strong>2.长尾词列表</strong></span></td>
                        <td style="width:80px;">广告系列:</td>
                        <td style="width:220px;">
                            <asp:DropDownList ID="ddlAdCategory" runat="server" CssClass="ddl" Width="200px" onselectedindexchanged="ddlAdCategory_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                        <td style="width:80px;">广告组:</td>
                        <td style="width:220px;"><asp:DropDownList ID="ddlAdGroup" runat="server" CssClass="ddl" WIDTH="200px" selectedindexchanged="ddlAdCategory_SelectedIndexChanged" AutoPostBack="true" OnSelectedIndexChanged="ddlAdGroup_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width:80px;">广告:</td>
                        <td><asp:DropDownList ID="ddlAds" runat="server" CssClass="ddl" style="WIDTH:200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                <tr>
                    <td style="width:120px;height: 30px;"><input type="submit"  value="全选" onclick="Check(true); return false;" id="AllCheck" class="btn" />&nbsp;<input type="submit"  value="反选" onclick="    Check(false); return false;" id="UnCheck" class="btn" /></td> 
                    <td style="width:120px;height: 30px;"><input id="chkCheckedRange" type="checkbox"  onchange="CheckedRange();return false;" />范围内全选</td>
                    <td style="width:150px;height: 30px;"><asp:Button ID="btnAddToAds" runat="server" Text="添加到选定广告" onclick="btnAddToAds_Click" CssClass="btn" /></td>
                   
                    <td style="width:100px;"></td>
                    <td  style="width:150px;"><asp:Button ID="btnDeleteCategory" runat="server" Text="删除选定词" onclick="btnDeleteCategory_Click" CssClass="btn"  OnClientClick="return confirm('您确认删除所选定词吗?');" /></td>
                    <td style="width:65px;">长尾词like:</td>
                    <td style="width:220px;"><asp:TextBox ID="txtLikeLongTailKeywords" runat="server" CssClass="Txt" Width="200px"></asp:TextBox></td>
                    <td style="width:55px;">词源like:</td>
                    <td style="width:220px;"><asp:TextBox ID="txtLikeSourceKeywords" runat="server" CssClass="Txt" Width="200px"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearch" runat="server" CssClass="btn" Text=" 查询 " onclick="btnSearch_Click" /></td>
                </tr>    
            </table>
             
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="80%" 
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
                             <asp:BoundField DataField="CategoryID" ReadOnly="true" HeaderText="分类ID">
                                 <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField> 
                             <asp:BoundField DataField="Status" ReadOnly="true" HeaderText="状态">
                                 <HeaderStyle Width="60px" />
                                 <ItemStyle Width="60px" HorizontalAlign="Center" />
                             </asp:BoundField>
                             <asp:BoundField DataField="LongTailKeywords" ReadOnly="true" HeaderText="长尾词">
                                 <HeaderStyle/>
                                 <ItemStyle HorizontalAlign="Left" />
                             </asp:BoundField>

                            <asp:BoundField DataField="SourceKeywords" ReadOnly="true" HeaderText="核心词(词源)">
                                 <HeaderStyle Width="250px"/>
                                 <ItemStyle Width="250px" HorizontalAlign="Left" />
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
            <td align="left"><webdiyer:aspnetpager id="pager" runat="server" Width="80%" CustomInfoSectionWidth="100%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="100" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>