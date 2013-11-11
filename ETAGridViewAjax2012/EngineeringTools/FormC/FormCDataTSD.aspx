<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormCDataTSD.aspx.cs" Inherits="EngineeringTools_FormC_FormCDataTSD" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>FormcData</title>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <th align="left" width="200">
                <font size="4">Table of Contents</font>
            </th>
            <td>
                <font size="3"><strong>VIEW MODE</strong></font>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <hr color="navy">
    <table cellspacing="0" cellpadding="1" width="50%" border="0" align="right">
        <tr>
            <td align="left" width="150">
                Package:&nbsp;<strong><asp:Label ID="lblpackage" runat="server"></asp:Label>
                </strong>&nbsp;
            </td>
            <td align="right" width="150">
                Model:&nbsp;
            </td>
            <td>
                <strong>
                    <asp:Label ID="lblmodel" runat="server"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                Mast:&nbsp;
            </td>
            <td>
                <strong>
                    <asp:Label ID="lblmast" runat="server"></asp:Label></strong>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td align="right">
                Attachment:&nbsp;
            </td>
            <td>
                <strong>
                    <asp:Label ID="lblatt" runat="server"></asp:Label></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:GridView ID="HeaderGridview" runat="server" Width="100%" AutoGenerateColumns="False"
        OnRowDataBound="HeaderGridview_RowDataBound" CellPadding="0" BorderColor="Blue"
        BorderStyle="Solid" BorderWidth="3px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
        <Columns>
            <asp:TemplateField HeaderText="Edit" Visible="False">
                <ItemTemplate>
                    <asp:HyperLink ID="hpledit" runat="server" NavigateUrl="" Text=""></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ECI Mark" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Image ID="imgrev" runat="server" ImageUrl="" />
                </ItemTemplate>
                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Revision" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="lblrevision" runat="server" Text='<%# ((DataRowView)Container.DataItem)["RevComment"]  %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ECI #" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="hpleci" runat="server" NavigateUrl="" Text=""></asp:HyperLink>
                </ItemTemplate>
                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# (((DataRowView)Container.DataItem)["CommentDate"]).ToString() %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Revised By" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# ((DataRowView)Container.DataItem)["RevInitials"]  %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <table border="1" bordercolor="blue" cellpadding="0" cellspacing="0" width="100%"
                frame="border">
                <tr height="20px">
                    <th>
                        <font size="2">Edit</font>
                    </th>
                    <th>
                        <font size="2">ECI Mark</font>
                    </th>
                    <th>
                        <font size="2">Revision</font>
                    </th>
                    <th>
                        <font size="2">ECI #</font>
                    </th>
                    <th>
                        <font size="2">Date</font>
                    </th>
                    <th>
                        <font size="2">Revised By</font>
                    </th>
                </tr>
            </table>
        </EmptyDataTemplate>
        <HeaderStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    <br />
    <div>
        <table cellspacing="0" cellpadding="1" width="30%" border="0" align="right">
            <tr>
                <td align="right">
                    <asp:Button ID="btnfilter" runat="server" Text="Filter" Width="81px" OnClick="btnfilter_Click" />
                </td>
                <td align="right">
                    <asp:DropDownList ID="ddlrevision" runat="server" OnSelectedIndexChanged="ddlrevision_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:GridView ID="Gvwpartlist" runat="server" BorderWidth="1px" BackColor="LightGoldenrodYellow"
        GridLines="Horizontal" CellPadding="2" BorderColor="Tan" PageSize="3" ForeColor="Black"
        DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False"
        BorderStyle="Solid" OnRowDataBound="Gvwpartlist_RowDataBound">
        <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
        <Columns>
            <asp:TemplateField HeaderText="Key #">
                <ItemTemplate>
                    <asp:Label ID="lblkey" runat="server" Text='<%# Bind("KeyCode") %>'>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ass'y #/Name">
                <ItemTemplate>
                    <asp:Label ID="lblname" runat="server" Text='<%# Eval("ADD_INDEX").ToString()%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Add TFC">
                <ItemTemplate>
                    <asp:Label ID="lbladdtfc" runat="server" Text='<%# Bind("ADD_TFC") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Add Index">
                <ItemTemplate>
                    <asp:Label ID="lbladdindexsub" runat="server" Text='<%# Eval("ADD_INDEXSUB") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Add Index" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lbladdindex" runat="server" Text='<%# Eval("ADD_INDEX") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del TFC">
                <ItemTemplate>
                    <asp:Label ID="lbldeltfc" runat="server" Text='<%# Bind("DEL_TFC") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del Index" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lbldelindex" runat="server" Text='<%# Bind("DEL_INDEX") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Del Index">
                <ItemTemplate>
                    <asp:Label ID="lbldelindexsub" runat="server" Text='<%# Bind("DEL_INDEXSUB") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Config">
                <ItemTemplate>
                    <asp:Label ID="lblconfig" runat="server" Text=''></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From ECI">
                <ItemTemplate>
                    <asp:Label ID="lblfromeci" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To ECI">
                <ItemTemplate>
                    <asp:Label ID="lbltoeci" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From Date" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblfromdate" runat="server" Text='<%# Bind("FROM_DATE") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" BorderStyle="Solid" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parts List Location">
                <ItemTemplate>
                    <asp:Label ID="lblpartlistlocation" runat="server" Text='<%# Bind("ModuleNumber") %>'></asp:Label>
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
    <div>
    </div>
    </form>
</body>
</html>
