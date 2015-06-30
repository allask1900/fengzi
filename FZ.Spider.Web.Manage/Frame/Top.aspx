<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="FZ.Spider.Web.Manage.Top" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    <!--
    body {
	    margin-left: 0px;
	    margin-top: 0px;
	    margin-right: 0px;
	    margin-bottom: 0px;
    }
    .STYLE1 {
	    font-size: 12px;
	    color: #FFFFFF;
    }
    .STYLE2 {font-size: 9px}
    .STYLE3 {
	    color: #033d61;
	    font-size: 12px;
    }
    -->
    </style>
 <script>
 function jsDate()
{
	<!--
	todayDate = new Date();
	date = todayDate.getDate();
	month= todayDate.getMonth() +1;
	year= todayDate.getYear();
    
	if(navigator.appName == "Netscape")
	{
		document.write(1900+year);
		document.write("年");
		document.write(month);
		document.write("月");
		document.write(date);
		document.write("日&nbsp;&nbsp;&nbsp;"); 
	}
	if(navigator.appVersion.indexOf("MSIE") != -1)
	{
	    document.write(1900+year);
	    document.write("年");
	    document.write(month);
	    document.write("月");
	    document.write(date);
	    document.write("日&nbsp;&nbsp;&nbsp;");

	}
	if (todayDate.getDay() == 5) document.write("星期五")
	if (todayDate.getDay() == 6) document.write("星期六")
	if (todayDate.getDay() == 0) document.write("星期日")
	if (todayDate.getDay() == 1) document.write("星期一")
	if (todayDate.getDay() == 2) document.write("星期二")
	if (todayDate.getDay() == 3) document.write("星期三")
	if (todayDate.getDay() == 4) document.write("星期四")
	
	//--> 
}
</script>
</head>
<body>
    <form id="form1" runat="server">
     <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="70" background="../images/main_05.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td height="24"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="270" height="24" background="../images/main_03.gif">&nbsp;</td>
                    <td width="505" background="../images/main_04.gif">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td width="21"><img src="../images/main_07.gif" width="21" height="24"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td height="38"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="435" height="38" background="../images/main_09.gif">&nbsp;</td>
                    <td><table width="750" border="0" align="right" cellpadding="0" cellspacing="0">
                        <tr>
                          <td  height="25"><div align="right"><span class="STYLE1"><span class="STYLE2">■</span> 现在时间：
                            <script>jsDate();</script> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;服务器时间： <%=System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %> 
                          </span></div></td>
                          <td style="padding-left:10px;" width="80px"><asp:ImageButton ID="btnExit2" runat="server" ImageUrl="../images/main_20.gif" OnClick="btnExit_Click" /></td>
                        </tr>
                    </table></td>
                    <td width="21"><img src="../images/main_11.gif" width="21" height="38"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td height="8" style=" line-height:8px;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="270" background="../images/main_29.gif" style=" line-height:8px;">&nbsp;</td>
                    <td width="505" background="../images/main_30.gif" style=" line-height:8px;">&nbsp;</td>
                    <td style=" line-height:8px;">&nbsp;</td>
                    <td width="21" style=" line-height:8px;"><img src="../images/main_31.gif" width="21" height="8" /></td>
                  </tr>
              </table></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td height="28" background="../images/main_36.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="177" height="28" background="../images/main_32.gif"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="20%"  height="22">&nbsp;</td>
                    <td width="59%" valign="bottom"><div align="center" class="STYLE1"></div></td>
                    <td width="21%">&nbsp;</td>
                  </tr>
              </table></td>
              <td style="padding-left:20px"><table width="500" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <% for (int i = 0; i < systemList.Count; i++)
                     { %>
                  <td height="23" style="cursor:hand" width="80px"><a href="Left.aspx?SysID=<%=systemList[i].SysID %>" target="menu" >
                    <div align="center" class="STYLE3"><b><%=systemList[i].SysName%></b></div>
                  </a></td>
                  <% if (i != systemList.Count - 1)
                     { %>
                  <td width="3"><img src="../images/main_34.gif" width="3" height="28" /></td>
                  <% }} %>
                  <td>&nbsp;</td>
                </tr>
              </table></td>
              <td width="21"><img src="../images/main_37.gif" width="21" height="28"></td>
            </tr>
        </table></td>
      </tr>
    </table>
      </form>
</body>
</html>
