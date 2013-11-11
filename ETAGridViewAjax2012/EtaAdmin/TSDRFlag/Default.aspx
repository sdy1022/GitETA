

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="EtaAdmin_TSDRFlag_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invalid TSD</title>
     

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
    
        <asp:GridView ID="Gvwpartlist" runat="server" BorderWidth="1px" BackColor="LightGoldenrodYellow"
            GridLines="Horizontal" CellPadding="2" BorderColor="Tan" PageSize="3" ForeColor="Black"
            DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False"
            AutoGenerateEditButton="True" BorderStyle="Solid" 
          
            onrowcancelingedit="Gvwpartlist_RowCancelingEdit" 
            onrowediting="Gvwpartlist_RowEditing" 
            onrowupdating="Gvwpartlist_RowUpdating">
            <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
            <Columns>
                 <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("TID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TSD Number">
                    <ItemTemplate>
                        <asp:Label ID="lblpair" runat="server" Text='<%# Bind("TSDNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Number">
                    <ItemTemplate>
                        <asp:Label ID="lblname" runat="server" Text='<%# Bind("OrderNumber") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Processed">
                    <ItemTemplate>
                        <asp:Label ID="lblkey" runat="server" Text='<%#Bind("Processed") %>'>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
                </asp:TemplateField>
              
            </Columns>
            <EmptyDataTemplate>
                No Member Record<br />
            </EmptyDataTemplate>
            <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
            <SelectedRowStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue" HorizontalAlign="Center">
            </SelectedRowStyle>
            <RowStyle HorizontalAlign="left" />
        </asp:GridView>
    
    
    
    </div>
    </form>
</body>
</html>
