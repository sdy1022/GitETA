<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Import namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
 <div>

<h2>

    &nbsp;</h2>
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="EditStatus"
        Width="394px" />



</div>
    <asp:GridView ID="myGridView" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
        BorderColor="Tan" BorderWidth="1px" CellPadding="2" DetailSummaryText="View Details"
        ForeColor="Black" GridLines="Horizontal" HorizontalAlign="left" OnRowCancelingEdit="myGridView_RowCancelingEdit"
        OnRowDeleting="myGridView_RowDeleting" 
        OnRowEditing="myGridView_RowEditing" OnRowUpdating="myGridView_RowUpdating"
        PageSize="3" RowHeaderColumn=" " Width="524px" 
         onrowdatabound="myGridView_RowDataBound" 
        onselectedindexchanged="myGridView_SelectedIndexChanged">
        <RowStyle HorizontalAlign="Center" />
        <Columns>
            <asp:TemplateField HeaderText="TID" SortExpression="TID">
                
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# ((DataRowView)Container.DataItem)["TID"] %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Package" SortExpression="Package_Number">
                <EditItemTemplate>
                    <asp:TextBox ID="txtpackage" runat="server" Text='<%# ((DataRowView)Container.DataItem)["Package_Number"] %>' Width="50px"></asp:TextBox>
                   
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# ((DataRowView)Container.DataItem)["Package_Number"] %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" SortExpression="statusvalue">
                <EditItemTemplate>
                    
                   <asp:Label ID="lblstatusid" runat="server" Visible="false" Text='<%# ((DataRowView)Container.DataItem)["statusvalue"] %>'></asp:Label>
                           <asp:DropDownList ID="ddlstatus" runat="server" Width="100px"
                             AppendDataBoundItems="true"  > 
                            <asp:ListItem></asp:ListItem>                           
                            </asp:DropDownList>  
                  
                  
                   
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# ((DataRowView)Container.DataItem)["statusvalue"] %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:Label ID="lblPID" runat="server" Text='<%# ((DataRowView)Container.DataItem)["TID"] %>'
                        Visible="false"></asp:Label>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Update" Text="Update"
                        ValidationGroup="EditStatus"></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPID" runat="server" Text='<%# ((DataRowView)Container.DataItem)["TID"] %>'
                        Visible="false"></asp:Label>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit">Edit</asp:LinkButton>
                    <asp:LinkButton ID="delete" runat="server" CausesValidation="False" CommandName="Delete"
                        OnClientClick="return confirm('Are you sure you want to delete this record?');"
                        Text="Delete" Visible="False"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Wrap="False" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            No Member Record<br />
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" HorizontalAlign="Center" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
    </asp:GridView>



    </form>
</body>
</html>
