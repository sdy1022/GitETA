<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TiemHistoryUpload.aspx.cs" Inherits="EtaAdmin_upload_TiemHistoryUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

     <div>
    
        <p style="margin-left: 130px">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" 
                Text="To import TOTIEM.MDB to SQL Server. Copy the totiem.mdb to \\colweb01\UploadAccessDB\ .  Then execute this page."></asp:Label>
        </p>
    
    </div>
    <p>
        <asp:TextBox ID="txtstatus" runat="server" Height="90px" Width="724px"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    
    <p>
        <asp:Button ID="Button1" runat="server" Text="Execute Package" Width="201px" 
            onclick="Button1_Click" />
        </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="GoTo History Record Search" Width="204px" />
        </p>
   
    </form>
</body>
</html>
