<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartList.ascx.cs" Inherits="EngineeringTools_PartRegistry_PartList" EnableViewState="true" %>
&nbsp;<asp:GridView ID="Gvwpartlist" runat="server" BorderWidth="1px" BackColor="LightGoldenrodYellow"
    GridLines="Horizontal" CellPadding="2" BorderColor="Tan" PageSize="3" ForeColor="Black"
    DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False" EnableViewState="True"
   OnRowDataBound="Gvwpartlist_RowDataBound">
    <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
    <EmptyDataTemplate>
       
    </EmptyDataTemplate>
    <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
    <Columns>
        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
            <HeaderTemplate>
                Level
            </HeaderTemplate>
            <ItemTemplate>
             <%--   <asp:Label ID="Label2" runat="server" Text='<%# Bind("INDEX_PARENTPART") %>'></asp:Label>--%>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="CHILDPART" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("PARENT_CHILDPART") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="FROM_ECI" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Labelpartno" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
     <%--    <asp:TemplateField HeaderText="PartsName" ItemStyle-HorizontalAlign="Center">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%# Bind("PARTSNAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
    
    </Columns>
    <SelectedRowStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue" HorizontalAlign="Center">
    </SelectedRowStyle>
    <RowStyle HorizontalAlign="left" />
</asp:GridView>
