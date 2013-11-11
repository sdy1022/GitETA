<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ETAGridViewTSD.ascx.cs"
    Inherits="ETAGridViewTSD" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <span style="background-color: rgb(204, 68, 68);">Validating... </span>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
        <asp:Label ID="lblerror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        <table border="0" width="100%">
            <tr>
                <td>
                    <table border="1" cellpadding="0" cellspacing="0" width="100%">
                        <tr id="trHead0" bordercolor="Blue">
                            <%--  <th style="writing-mode: tb-rl; border-top-width: 5px">
                                <font size="1">EDIT HEADER</font><asp:CheckBox ID="chkeditheader" runat="server"
                                    OnCheckedChanged="chkeditheader_CheckedChanged" AutoPostBack="True" />
                            </th>--%>
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
                                        <th align="left" width="200">
                                            <input name="txtCode50" type="hidden" value="1" />
                                            <input name="txtOriginal0" type="hidden" value="" />
                                            <input name="txtID0" type="hidden" value="45996" />
                                            <input name="txtDesignnumber0" type="hidden" value="N9JU-2" />
                                            <asp:TextBox ID="txtNAME1" runat="server"></asp:TextBox>
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
                                            <asp:TextBox ID="txtNAME2" runat="server"></asp:TextBox>
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
                                            <asp:TextBox ID="txtNAME3" runat="server"></asp:TextBox>
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
                                            <asp:TextBox ID="txtNAME4" runat="server"></asp:TextBox>
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
                        DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False"
                        OnRowEditing="Gvwpartlist_RowEditing" OnRowCancelingEdit="Gvwpartlist_RowCancelingEdit"
                        OnRowUpdating="Gvwpartlist_RowUpdating">
                        <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
                        <EmptyDataTemplate>
                            No Member Record<br />
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
                        <Columns>
                            <%--  <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkecilog" runat="server" />
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    ECI LOG
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkoverwrite" runat="server" />
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    OVERWRITE
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkoverwrite" runat="server" Enabled="False" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="REV">
                                <EditItemTemplate>
                                    <asp:Label ID="lblrev" runat="server" Text='<%# Eval("REV") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("REV") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="LEVEL">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtleve1" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PART NO.">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtpartno" runat="server" Text='<%# Bind("INDEX_PARENTPART") %>'
                                        AutoPostBack="false">
                                    </asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtpartno"
                                        ErrorMessage="Invalid Partno" OnServerValidate="CustomizedQuantityValidationHandlerForPartno">*</asp:CustomValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Labelpartno" runat="server" Text='<%# Bind("INDEX_PARENTPART") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PART Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtpartname" runat="server" TextMode="MultiLine" Text='<%# Bind("PARTSNAME") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblpartname" runat="server" Text='<%# Bind("PARTSNAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtQty"
                                        ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MATERIAL">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMaterial" runat="server" Text='<%# Bind("MATERIAL1") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("MATERIAL1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FROMECI">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOECI">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:Label>
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
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hiddenfield" runat="server" />
