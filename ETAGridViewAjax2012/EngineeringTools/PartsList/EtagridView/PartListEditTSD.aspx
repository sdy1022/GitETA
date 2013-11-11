<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartListEditTSD.aspx.cs"
    Inherits="EngineeringTools_PartsList_EtagridView_PartListEditTSD" %>

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
        li.headerview
        {
            list-style-type: none;
            float: left;
            padding: 0px 10px 0px 10px;
        }
        div.clear
        {
            clear: both;
        }
        .txtVertical
        {
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3); /* IE6,IE7 */
            ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=3)"; /* IE8 */
            -moz-transform: rotate(-90deg); /* FF3.5+ */
            -o-transform: rotate(-90deg); /* Opera 10.5 */
            -webkit-transform: rotate(-90deg); /* Safari 3.1+, Chrome */
            position: absolute;
        }
        .txtVertical1
        {
            writing-mode: tb-lr;
        }
        .line-separator
        {
            height: 10px;
            background: #717171;
            border-bottom: 1px solid #313030;
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
                    <asp:Button ID="btnfiler" runat="server" Text="Filter" Width="81px" OnClick="btnfiler_Click"
                        Visible="False" />
                </li>
                <li class="nav">ECI
                    <asp:DropDownList ID="ddleci" runat="server" OnSelectedIndexChanged="ddleci_SelectedIndexChanged"
                        Enabled="False">
                        <asp:ListItem runat="server" Text="No Filter" Value="No filter" Enabled="False"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="nav">Part List
                    <asp:DropDownList ID="ddlpartlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlpartlist_SelectedIndexChanged">
                    </asp:DropDownList>
                </li>
            </div>
            <div class="clear">
                <asp:Label ID="lblerror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </div>
            <div>
                <li class="headerview">
                    <%--  <font size="1" >EDIT HEADER</font>--%>
                    <asp:CheckBox ID="chkeditheader" runat="server" OnCheckedChanged="chkeditheader_CheckedChanged"
                        AutoPostBack="True" Text="Edit Header" CssClass="txtVertical1" />
                </li>
                <li class="headerview">
                    <asp:GridView ID="HeaderGridview" runat="server" AutoGenerateColumns="False" OnRowDataBound="HeaderGridview_RowDataBound"
                        CellPadding="0" CellSpacing="0" BorderColor="Blue" BorderStyle="Solid" BorderWidth="3px">
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hpledit" runat="server" NavigateUrl="" Text=""></asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="Small" BorderColor="Blue" />
                                <ItemStyle BorderColor="Blue" BorderStyle="Solid" />
                            </asp:TemplateField>
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
                            <table border="1" bordercolor="blue" cellpadding="0" cellspacing="0">
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
                </li>
                <li class="headerview">
                    <table border="1" cellpadding="1" cellspacing="0">
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
                                <asp:TextBox ID="txtNAME1" runat="server" Width="95%"></asp:TextBox>
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
                                <asp:TextBox ID="txtNAME2" runat="server" Width="95%"></asp:TextBox>
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
                                <asp:TextBox ID="txtNAME3" runat="server" Width="95%"></asp:TextBox>
                            </th>
                            <th align="left">
                                <asp:TextBox ID="txtCODE3" runat="server"></asp:TextBox>
                            </th>
                        </tr>
                        <tr bordercolor="Blue">
                            <th align="left">
                                <font size="2">&nbsp;</font><asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click"
                                    Text="Submit" Width="109px" BackColor="Yellow" Visible="False" />
                            </th>
                            <th align="left">
                                <asp:TextBox ID="txtNAME4" runat="server" Width="95%"></asp:TextBox>
                            </th>
                            <th align="left">
                                <asp:TextBox ID="txtCODE4" runat="server"></asp:TextBox>
                            </th>
                        </tr>
                    </table>
                </li>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="clear">
                <asp:Label ID="lblpartlisterror" runat="server" ForeColor="Red" Visible="False"></asp:Label>
              <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            </div>
            <div class="line-separator">
            </div>
            <div>
                <asp:GridView ID="Gvwpartlist" runat="server" BorderWidth="1px" BackColor="LightGoldenrodYellow"
                    GridLines="Horizontal" CellPadding="2" BorderColor="Tan" PageSize="3" ForeColor="Black"
                    DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False"
                    AutoGenerateEditButton="True" AutoGenerateDeleteButton="True" OnRowDataBound="Gvwpartlist_RowDataBound"
                    OnRowCancelingEdit="Gvwpartlist_RowCancelingEdit" OnRowDeleting="Gvwpartlist_RowDeleting"
                    OnRowEditing="Gvwpartlist_RowEditing" OnRowUpdating="Gvwpartlist_RowUpdating">
                    <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
                    <EmptyDataTemplate>
                        No Member Record<br />
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="PaleGoldenrod"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="LEVEL">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtlevel" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtlevel"
                                    ErrorMessage="Invalid Level" OnServerValidate="CustomizedQuantityValidationHandlerForLevel">*</asp:CustomValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("TreeLevel") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PART NO.">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtpartno" runat="server" Text='<%# Bind("PARENT_CHILDPART") %>'></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtpartno"
                                    ErrorMessage="Invalid Partno" OnServerValidate="CustomizedQuantityValidationHandlerForPartno">*</asp:CustomValidator>
                            </EditItemTemplate>
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
                            <EditItemTemplate>
                                <asp:TextBox ID="txtqty" runat="server" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtqty"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
                            </EditItemTemplate>
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
                            <EditItemTemplate>
                                <asp:Label ID="lblfromeci1" runat="server" Text='<%# Bind("FROM_ECI") %>' Visible="False"></asp:Label>
                                <asp:TextBox ID="txtfromeci1" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:TextBox>
                            </EditItemTemplate>
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
                            <EditItemTemplate>
                                <asp:TextBox ID="txttoeci" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbltoeci1" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ID" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblid" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                <asp:Label ID="lblordervalue" runat="server" Text='<%# Bind("ordervalue") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle ForeColor="GhostWhite" BackColor="DarkSlateBlue" HorizontalAlign="Center">
                    </SelectedRowStyle>
                    <RowStyle HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
