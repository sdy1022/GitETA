<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HistoryRecordSearch.aspx.cs" Inherits="EtaAdmin_dataupload_HistoryRecordSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
     
        <table class="style1">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Part Number
                </td>
                <td>
                    <asp:TextBox ID="txtpartno" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnsearch" runat="server" Text="Search" Width="170px" 
                        onclick="btnsearch_Click" />
                </td>
                <td>
                
                </td>
                    
            </tr>
        </table>
    
     
    </div>
    <asp:GridView ID="Gvwpartlist" runat="server" 
      BorderWidth="1px" 
            BackColor="LightGoldenrodYellow" GridLines="Horizontal" CellPadding="2"
            BorderColor="Tan" PageSize="3" ForeColor="Black" 
            DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " 
            AutoGenerateColumns="true" 
        >
            <HeaderStyle Font-Bold="True" BackColor="Tan" ></HeaderStyle>
            <EmptyDataTemplate>
               No Member Record<br />
            </EmptyDataTemplate>
             <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
      
     <SelectedRowStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue" 
                HorizontalAlign="Center"></SelectedRowStyle>
        <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </form>
</body>
</html>
