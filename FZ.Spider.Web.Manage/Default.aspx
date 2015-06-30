<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FZ.Spider.Web.Manage.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>购物搜索引擎 - 管理系统</title>
<link href="CSS/css.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #016aa9;
}
-->
</style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td><p>&nbsp;</p>
    <table width="962" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td height="235" valign="bottom" background="images/login_03.gif"><table width="50%" height="126" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td valign="bottom"><div align="center"><fon color="#FF0000">
              <span class="SendStyleColor">注：请使用管理员账号/密码登录</font></span></div></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="53"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="394" height="53" background="images/login_05.gif">&nbsp;</td>
            <td width="206" background="images/login_06.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="16%" height="25"><div align="right"><span class="STYLE1">用户</span></div></td>
                <td width="57%" height="25"><div align="center">
                    <asp:TextBox id="txtUser" runat="server" MaxLength="20" style="width:105px; height:17px; background-color:#292929; border:solid 1px #7dbad7; font-size:12px; color:#6cd0ff"></asp:TextBox>
                </div></td>
                <td width="27%" height="25">&nbsp;</td>
              </tr>
              <tr>
                <td height="25"><div align="right"><span class="STYLE1">密码</span></div></td>
                <td height="25"><div align="center">
                    <asp:TextBox id="txtPwd" runat="server" TextMode="Password"
					Height="20px" MaxLength="20" style="width:105px; height:17px; background-color:#292929; border:solid 1px #7dbad7; font-size:12px; color:#6cd0ff"></asp:TextBox>
                </div></td>
                <td height="25"><div align="left">
                  <div align="left">
                      <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="images/dl.gif" OnClick="btnLogin_Click" /><a href="main.html"></a></div>
                </div></td>
              </tr>
              
            </table></td>
            <td width="362" background="images/login_07.gif">&nbsp;</td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="213" valign="top" background="images/login_08.gif">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
</table>
    </form>
</body>
</html>
