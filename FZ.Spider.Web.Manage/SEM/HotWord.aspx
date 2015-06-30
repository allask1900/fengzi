<%@ Page Title="搜索引擎系统\热门关键词" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HotWord.aspx.cs" Inherits="FZ.Spider.Web.Manage.Search.HotWord" %>
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
    <div class="NavTitle"  width="80%">搜索引擎系统\热门关键词</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
    <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>1.热门关键词添加</strong></span> </td>
        </tr>
    <tr> 
            <td >
                <table  width="100%">
                    <tr>
                        <td    class="auto-style1">热词类别:</td>                          
                        <td>
                            <asp:DropDownList ID="dropHotType" runat="server" CssClass="ddl"  Width="200px">
                                <asp:ListItem Value="0">选择关键词类型</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        
                    </tr>
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
                        <td class="auto-style1">热门关键词:</td>                          
                        <td>
                            <asp:TextBox ID="txtWords" runat="server" CssClass="Txt" Width="550px" Height="60px" TextMode="MultiLine"></asp:TextBox>                            
                            多个以换行分隔</td>
                        
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
            <td bgcolor="#eeeeee">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td  style="width:160px; height:35px"><span class="STYLE13" ><strong>2.关键词列表</strong></span></td>
                        <td style="width:160px"><asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="ddl"  Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                    <td style="width:160px"><asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" CssClass="ddl" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                    <td style="width:160px"><asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" CssClass="ddl" Width="150px" AutoPostBack="True"></asp:DropDownList></td>
                    <td><asp:DropDownList ID="DropDownList4" runat="server" CssClass="ddl" Width="150px"></asp:DropDownList></td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="2" width="100%">
                <tr>
                    <td style="width:100px;height: 30px;"><input type="submit"  value="全选" onclick="Check(true); return false;" id="AllCheck" class="btn" />&nbsp;<input type="submit"  value="反选" onclick="    Check(false); return false;" id="UnCheck" class="btn" /></td> 
                    <td style="width:100px;height: 30px;"><input id="chkCheckedRange" type="checkbox"  onchange="CheckedRange();return false;" />范围内全选定</td>        
                    <td style="width:150px;"><asp:Button ID="btnSysCategory" runat="server" Text="词对应选定分类" onclick="btnSysCategory_Click" CssClass="btn"/></td>
                    <td style="width:60px"></td>
                    <td style="width:150px;"><asp:Button ID="btnDeleteCategory" runat="server" Text="删除选定词" onclick="btnDeleteCategory_Click" CssClass="btn"  OnClientClick="return confirm('您确认删除所选定词吗?');" /></td>
                    <td style="width:65px;">关键词like:</td>
                    <td style="width:220px;"><asp:TextBox ID="txtLikeWord" runat="server" CssClass="Txt" Width="200px"></asp:TextBox></td>
                    <td style="width:65px;">原分类like:</td>
                    <td style="width:220px;"><asp:TextBox ID="txtLikeRemark" runat="server" CssClass="Txt" Width="200px"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearch" runat="server" CssClass="btn" Text=" 查询 " onclick="btnSearch_Click" /></td>
                    
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
                             <asp:BoundField DataField="CategoryID" ReadOnly="true" HeaderText="分类ID">
                                 <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField>
                                                        
                            <asp:BoundField DataField="HotType" ReadOnly="true" HeaderText="热词分类">
                                 <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center" />
                             </asp:BoundField>

                             <asp:BoundField DataField="Status" ReadOnly="true" HeaderText="状态">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>

                            <asp:BoundField DataField="Rank" ReadOnly="true" HeaderText="排序">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" HorizontalAlign="Center" />
                             </asp:BoundField>

                             <asp:BoundField DataField="Word" ReadOnly="true" HeaderText="关键词">
                                 <HeaderStyle Width="250px" />
                                 <ItemStyle Width="250px" HorizontalAlign="Left" />
                             </asp:BoundField>

                            <asp:BoundField DataField="Remark" ReadOnly="true" HeaderText="Remark">
                                 <HeaderStyle />
                                 <ItemStyle HorizontalAlign="Left" />
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
            <td align="left"><webdiyer:aspnetpager id="pager" runat="server" Width="99%" CustomInfoSectionWidth="80%" ShowCustomInfoSection="Left" NumericButtonTextFormatString="[{0}]" PageSize="100" OnPageChanged="pager_PageChanged" FirstPageText="首页" LastPageText="尾页" MoreButtonType="Image" NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never"></webdiyer:aspnetpager></td>
          </tr>
    </table>
</asp:Content>