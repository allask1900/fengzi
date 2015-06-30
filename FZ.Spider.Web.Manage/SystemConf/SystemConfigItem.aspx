<%@ Page Title="系统管理-设置配置项" Language="C#" MasterPageFile="/Site.Master" AutoEventWireup="true"   ValidateRequest="false" CodeBehind="SystemConfigItem.aspx.cs" Inherits="FZ.Spider.Web.Manage.SystemConf.SystemConfigItem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="NavTitle">系统管理/设置配置项</div>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">
        <tr>
            <td bgcolor="#eeeeee" height="22"><span class="STYLE13"><strong>1.选择系统</strong></span> </td>
        </tr>
        <tr id="trAddSubSystem">
            <td height="22" align="left">
               <table  border="0" cellpadding="0" cellspacing="2" width="100%">
                    <tr>
                        <td style="vertical-align:middle" class="style3"  width="90" >所属系统*</td>
                        <td  style="vertical-align:middle" class="style3">
                            <asp:DropDownList ID="ddlSystem" runat="server" CssClass="ddl" style="vertical-align:middle;WIDTH: 215px" onselectedindexchanged="ddlSystem_SelectedIndexChanged" AutoPostBack="true">

                            </asp:DropDownList>                             
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table> 

    <table align="center" border="0" cellpadding="0" cellspacing="0" width="99%">         
        <tr>
            <td bgcolor="#eeeeee" height="22">
                <span class="STYLE13"><strong>2.选择配置单</strong></span></td>
        </tr>
        <tr id="tr1">
            <td height="22" align="left">
                <table  border="0" cellpadding="0" cellspacing="2" width="100%">
                    <tr>
                        <td width="90" style="vertical-align:middle" class="style3">所属配置单*</td>
                        <td   style="vertical-align:middle" class="style3">
                            <asp:DropDownList ID="ddlSysConf" runat="server"  CssClass="ddl" 
                                style="vertical-align:middle;WIDTH: 215px"   AutoPostBack="true"
                                onselectedindexchanged="ddlSysConf_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table> 

    <table  align="center" border="0" cellpadding="0" cellspacing="0" width="99%">   
        <tr>
                        <td>
                            <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" 
                                width="100%">
                                <tr>
                                    <td height="22" style="width: 100%">
                                        <span class="STYLE13"><strong>3.配置单项配置</strong></span></td>
                                </tr>
                                
                            </table>
                        </td>
                    </tr>

        <tr>
                         <td>
                                    <table  border="0" cellpadding="0" cellspacing="2" width="100%">
                                        <tr>
                                           <td width="90" style="vertical-align:middle; height:25px">配置项key*</td>
                                           <td style="vertical-align:middle">
                                                <asp:TextBox ID="txtConfKey" CssClass="Txt" runat="server" style="WIDTH: 210px" ></asp:TextBox>
                                                <asp:Literal ID="litItemID" runat="server" Visible="False"></asp:Literal>
                                           (名称格式:"系统.应用.Name",如:"search.spider.NoAnalysisIDS")</td>
                                        </tr>
                                        <tr>
                                           <td width="90" style="vertical-align:middle; height:25px">配置项value*</td>
                                           <td style="vertical-align:middle">
                                               <asp:TextBox ID="txtConfValue" CssClass="Txt" style="WIDTH: 700px; HEIGHT: 85px" 
                                                   runat="server" TextMode="MultiLine"></asp:TextBox>
                                           </td>
                                        </tr>
                                        <tr>
                                           <td width="90" style="vertical-align:middle; height:25px">数据类型</td>
                                           <td style="vertical-align:middle">
                                                
                                            <asp:DropDownList ID="ddlDataType" runat="server" CssClass="ddl" 
                                                   style="vertical-align:middle;WIDTH: 215px">
                                                <asp:ListItem Text="请选择类型" Value=""/>
                                                <asp:ListItem Text="字符" Value="String"/>
                                                <asp:ListItem Text="数值" Value="Number"/>
                                                <asp:ListItem Text="时间" Value="DateTime"/>
                                            </asp:DropDownList>
                                        </td>
                                        </tr>
                                        <tr>
                                           <td width="90" style="vertical-align:middle; height:25px">备注</td>
                                           <td style="vertical-align:middle">
                                               <asp:TextBox ID="txtDescription" CssClass="Txt" style="WIDTH: 700px; HEIGHT: 25px" 
                                                   runat="server" TextMode="MultiLine"></asp:TextBox>
                                           </td>
                                        </tr>
                                    </table>
                          </td>
                 </tr>

        <tr>
                        <td> 
                              <table  border="0" cellpadding="0" cellspacing="2" width="100%">
                              <tr>
                                <td  width="90" >
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnSave" CssClass="btn" runat="server" style="WIDTH: 48px"  
                                        Text="保存" onclick="btnSave_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnCancel"  CssClass="btn" runat="server" Text="取消"                        onclick="btnCancel_Click" /></td>
                              </tr>
                              </table> 
                        </td>
                    </tr>
                </table>              
         
            <table bgcolor="#eeeeee" border="0" cellpadding="0" cellspacing="0" width="99%" style="margin-left:5px;">
                    <tr>
                        <td height="22"><span class="STYLE13"><strong>4.配置单项列表</strong></span></td>
                        <td height="22"  style="width: 180px; text-align: right;">
                            &nbsp;</td>
                    </tr>
                </table>
                      
    <table border="0" cellspacing="0" style="WIDTH: 100%;">
                    <tr>
                        <td>
                         <asp:GridView ID="gvDataList" CssClass="TB_Grid" runat="server" Width="99%"                       AutoGenerateColumns="False" EmptyDataText="没有相关数据！"  EmptyDataRowStyle-ForeColor="red" OnRowDataBound="gvDataList_RowDataBound"  OnRowUpdating="gvDataList_RowUpdating" OnRowDeleting="gvDataList_RowDeleteing">
                        <HeaderStyle CssClass="STYLE13" />
                        <AlternatingRowStyle BackColor="#EEEEEE" />
                        <Columns>
                            <asp:BoundField DataField="ItemID" HeaderText="序号">
                                 <HeaderStyle Width="40px" />
                                 <ItemStyle Width="40px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="ConfID" HeaderText="配置单">
                                 <HeaderStyle Width="50px" />
                                 <ItemStyle Width="50px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="KeyName" HeaderText="配置项Key">
                            <HeaderStyle Width="250px" />
                                 <ItemStyle Width="250px"  CssClass="WordsBreak" />
                             </asp:BoundField>


                            <asp:TemplateField HeaderText="配置项Value">
                                 <HeaderStyle/>                                
                                 <ItemStyle CssClass="WordsBreak"/>
                                 <ItemTemplate>
                                 <%#Eval("Value").ToString()%>
                                </ItemTemplate>                             
                             </asp:TemplateField>




                            <asp:BoundField DataField="DataType" HeaderText="数据类型">
                            <HeaderStyle Width="65px" />
                                 <ItemStyle Width="65px" HorizontalAlign="Center" />
                             </asp:BoundField>                              
                             <asp:BoundField DataField="LastChangeTime" HeaderText="更新时间">
                                <HeaderStyle Width="120px" />
                                 <ItemStyle  HorizontalAlign="Center" Width="120px" />
                             </asp:BoundField>
                            <asp:BoundField DataField="Description" HeaderText="备注" ItemStyle-CssClass="WordsBreak"/>

                            <asp:TemplateField HeaderText="操作">
                             <HeaderStyle Width="80px" />
                                 <ItemStyle Width="80px" HorizontalAlign="Center"  CssClass="WordsBreak"/>
                                 <ItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" ForeColor="blue" CommandName="Update"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false">修改</asp:LinkButton>
                                &nbsp;&nbsp;
                               <asp:LinkButton ID="btnDelete" runat="server" ForeColor="blue" CommandName="Delete"    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CausesValidation="false"  OnClientClick="return confirm('您确认删除该记录吗?');">删除</asp:LinkButton>
                              </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle BorderStyle="None" ForeColor="Red" HorizontalAlign="Left" />
                    </asp:GridView>
                        </td>
                    </tr>
                    </table>   
</asp:Content>