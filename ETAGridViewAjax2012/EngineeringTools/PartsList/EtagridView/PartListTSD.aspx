<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartListTSD.aspx.cs" Inherits="EngineeringTools_PartsList_EtagridView_PartListTSD" %>

<%@ Import Namespace="System.Data" %>
<%@ Register Src="ETAGridViewTSD.ascx" TagName="ETAGridViewTSD" TagPrefix="uc1" %>
<%@ Register TagPrefix="uc1" TagName="ETAGridView" Src="~/EngineeringTools/PartsList/EtagridView/ETAGridView.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        li.nav
        {
            list-style-type: none;
            float: right;
            padding: 0px 10px 0px 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div>
                <li class="nav">
                    <asp:Button ID="btnfiler" runat="server" Text="Filter" Width="81px" OnClick="btnfiler_Click" />
                </li>
                <li class="nav">ECI
                    <asp:DropDownList ID="ddleci" runat="server" OnSelectedIndexChanged="ddleci_SelectedIndexChanged">
                        <asp:ListItem runat="server" Text="No Filter" Value="No filter"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="nav">Part List
                    <asp:DropDownList ID="ddlpartlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartlist_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
            </div>
            <%--<div>
        <div align="center">
            <asp:Label ID="lblecimode" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        </div>
        <asp:GridView ID="myGridView" runat="server" BorderWidth="1px" GridLines="Horizontal"
            CellPadding="2" PageSize="3" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False"
            HorizontalAlign="left" OnRowDataBound="myGridView_RowDataBound">
            <EmptyDataTemplate>
                No Member Record<br />
            </EmptyDataTemplate>
            <Columns>
                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblheaderid" runat="server" Visible="False" Text='<%# ((DataRowView)Container.DataItem)["HeaderID"]  %>'></asp:Label>
                        <uc1:ETAGridViewTSD ID="ETAGridView1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>--%>
            <div>
                <asp:Label ID="lblerror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                <tr id="trHead0" bordercolor="Blue">
                                    <td style="border-right: none; border-top-width: 5px">
                                        <asp:GridView ID="HeaderGridview" runat="server" Width="100%" AutoGenerateColumns="False"
                                            OnRowDataBound="HeaderGridview_RowDataBound" CellPadding="0" CellSpacing="0"
                                            BorderColor="Blue" BorderStyle="Solid" BorderWidth="3px">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpledit" runat="server" NavigateUrl="" Text=""></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                                            <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ECI Mark">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgrev" runat="server" ImageUrl="" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                                                    <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Revision">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrevision" runat="server" Text='<%# ((DataRowView)Container.DataItem)["RevComment"]  %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                                                    <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ECI #">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hpleci" runat="server" NavigateUrl="" Text=""></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                                                    <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# (((DataRowView)Container.DataItem)["CommentDate"]).ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                                                    <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Revised By">
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
                                    </td>
                                    <td style="border-top-width: 5px">
                                        <table border="1" cellpadding="1" cellspacing="0" frame="void" width="100%">
                                            <tr bordercolor="Blue">
                                                <th align="left" width="150">
                                                    <font size="2">Module:&nbsp;<asp:Label ID="lblmodule" runat="server"></asp:Label>
                                                    </font>
                                                </th>
                                                <th align="left" width="400">
                                                    <input name="txtCode50" type="hidden" value="1" />
                                                    <input name="txtOriginal0" type="hidden" value="" />
                                                    <input name="txtID0" type="hidden" value="45996" />
                                                    <input name="txtDesignnumber0" type="hidden" value="N9JU-2" />
                                                    <asp:TextBox ID="txtNAME1" runat="server" Width="380px"></asp:TextBox>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtCODE1" runat="server"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr bordercolor="Blue">
                                                <th align="left">
                                                    <font size="2">Original Package:&nbsp;<asp:Label ID="lbloriginal" runat="server"></asp:Label>
                                                    </font>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtNAME2" runat="server" Width="380px"></asp:TextBox>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtCODE2" runat="server"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr bordercolor="Blue">
                                                <th align="left">
                                                    <font size="2">Page:&nbsp;<asp:Label ID="lblCODE5" runat="server"></asp:Label>
                                                    </font>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtNAME3" runat="server" Width="380px"></asp:TextBox>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtCODE3" runat="server"></asp:TextBox>
                                                </th>
                                            </tr>
                                            <tr bordercolor="Blue">
                                                <th align="left">
                                                    <font size="2">&nbsp;</font>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtNAME4" runat="server" Width="380px"></asp:TextBox>
                                                </th>
                                                <th align="left">
                                                    <asp:TextBox ID="txtCODE4" runat="server"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblpartlisterror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:GridView ID="Gvwpartlist" runat="server" BorderWidth="1px" BackColor="LightGoldenrodYellow"
                                GridLines="Horizontal" CellPadding="2" BorderColor="Tan" PageSize="3" ForeColor="Black"
                                DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False">
                                <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
                                <EmptyDataTemplate>
                                    No Member Record<br />
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="LEVEL">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtleve1" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PART NO.">
                                        <ItemTemplate>
                                            <asp:Label ID="Labelpartno" runat="server" Text='<%# Bind("PARENT_CHILDPART") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MINOR">
                                        <ItemTemplate>
                                            <asp:Label ID="Labelminor" runat="server" Text='<%# Bind("MAINOR") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PART Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpartname" runat="server" Text='<%# Bind("PARTSNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MAT'L">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("MATERIAL1") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MAT'L SIZE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblm2" runat="server" Text='<%# Bind("MATERIAL2") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FROM(Part No.)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromeci" runat="server" Text='<%# Bind("FROMECI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FROM(LVL)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromeci1" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TO(Part No.)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltoeci" runat="server" Text='<%# Bind("TOECI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TO(LVL)">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltoeci1" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue" HorizontalAlign="Center">
                                </SelectedRowStyle>
                                <RowStyle HorizontalAlign="left" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
