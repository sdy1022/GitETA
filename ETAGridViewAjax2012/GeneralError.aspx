<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeneralError.aspx.cs" Inherits="GeneralError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <STRONG>
<FONT size=6 color=red>Transaction Failed!</FONT></STRONG>

<hr color=navy>
<BR>

Print out this page and contact the DataBase Administrator.
<BR>
ID=<asp:Label ID="lblid" runat="server"></asp:Label>
<BR>
<DIV style="background-Color:f5deb3">
<BR>
Page Error Occurred: <Br>
<SCRIPT language=jscript>
document.writeln(document.referrer)
</Script>
<BR>
<br>
Error Description: <BR>
 <asp:Label ID="lblerror" runat="server"></asp:Label>
<BR>
<BR>
</DIV>

    </form>
</body>
</html>
