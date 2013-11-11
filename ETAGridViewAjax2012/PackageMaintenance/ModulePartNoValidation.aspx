<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModulePartNoValidation.aspx.cs" Inherits="PackageMaintenance_ModulePartNoValidation" %>

<%@ Register src="../EngineeringTools/PartsList/EtagridView/ETAGridView.ascx" tagname="etagridview" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
      <asp:GridView ID="myGridView" Runat="server" BorderWidth="1px" 
            BackColor="LightGoldenrodYellow" GridLines="Horizontal" CellPadding="2"
            BorderColor="Tan" PageSize="3" ForeColor="Black" 
            DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " 
            AutoGenerateColumns="False" 
        AutoGenerateEditButton="True" onrowediting="myGridView_RowEditing" 
            onrowcancelingedit="myGridView_RowCancelingEdit" 
            onrowdatabound="myGridView_RowDataBound" 
            onrowupdating="myGridView_RowUpdating" >
            <Columns>
                <asp:TemplateField Visible="true">
                    <EditItemTemplate>
                          <asp:Label ID="lblitemid" runat="server" Text='<%# Bind("itemid") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblitemid" runat="server" Text='<%# Bind("itemid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Module">
                    
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("designnumber") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Part Number">
                    <EditItemTemplate>
                       <asp:TextBox ID="txtpartno" runat="server" Text='<%# Bind("partnumber") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblpartnumber" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Validation Status">
                   
                    <ItemTemplate>
                        <asp:Label ID="lblvaldationstatus" runat="server" ForeColor="#FF3300"  Text="" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lblPartNoValidationStatus" runat="server" ForeColor="#FF3300"  Visible="false" Text='<%# Bind("PartNoValidationStatus") %>'>' Font-Bold="True"></asp:Label>                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
               No Member Record<br />
            </EmptyDataTemplate>           
            
      <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
     <SelectedRowStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue" HorizontalAlign="Center"></SelectedRowStyle>
        <RowStyle HorizontalAlign="left" />      
     
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
