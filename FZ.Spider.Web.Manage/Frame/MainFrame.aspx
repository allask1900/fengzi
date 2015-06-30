<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainFrame.aspx.cs" Inherits="FZ.Spider.Web.Manage.MainFrame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>搜索引擎管理平台</title>
</head>
    <frameset rows="98,*,8" cols="*" frameborder="no" border="0" framespacing="0">
   <frame src="Top.aspx" name="topFrame" frameborder="no" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
   <frame src="middel.html" name="middelFrame" scrolling="No" noresize="noresize" id="middelFrame" />
   <frame src="bottom.html" name="bottomFrame" scrolling="No" noresize="noresize" id="bottomFrame" />
</frameset>
<noframes><body>
</body>
</noframes>
</html>
