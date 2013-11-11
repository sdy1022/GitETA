<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartRegistryInsert.aspx.cs"
    Inherits="EngineeringTools_PartRegistry_PartRegistryInsert" %>

<%@ Register Src="PartRegistryNumber.ascx" TagName="PartRegistryNumber" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PartRegistry AIO</title>
    <%--<link href="../../Scripts/JqueryUI/default.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Scripts/Site.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/JqueryUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/JqueryUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqueryUI/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JqueryUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ValidateInputs() {

            alert("start ");
            var userControlID = '<%=PartRegistryNumber1.FullValue %>';
            alert(userControlID);
            alert($("#" + "Text1").val());


        }

        // page load function
        $(document).ready(function() {
            // Initiate layout
            // to avoid proble
            // $.ajaxSetup({ cache: false });
            alert("init");
            //  debugger;
            //var bt = $("#" + '<%=Button1.ClientID%>').val();
            // alert(bt);
            // $("#" + '<%=Button1.ClientID%>').click(function() {
            ValidateInputs();
        });
        //          
        //            $("#submitbtn").click(function() {
        //                ValidateInputs();
        //            });

        //            $("#submitbtn").onclick = function() {

        //                ValidateInputs();
        //            };

        //            $("#submitbtn").onclick = function() {
        //                alert("fgfg");
        //            };

        //        });
        
    </script>

    <style type="text/css">
        body
        {
            font-size: 75%;
            font-family: Verdana, Tahoma, Arial, "Helvetica Neue" , Helvetica, Sans-Serif;
            color: #232323;
            background-color: #fff;
        }
        /* Styles for basic forms
-----------------------------------------------------------*/fieldset
        {
            border: 1px solid #ddd;
            padding: 0 1.4em 1.4em 1.4em;
            margin: 0 0 1.5em 0;
        }
        legend
        {
            font-size: 1.2em;
            font-weight: bold;
        }
        textarea
        {
            min-height: 75px;
        }
        .editor-label
        {
            margin: 1em 0 0 0;
            clear: left;
            float: left;
            min-width: 100px;
            vertical-align: middle;
        }
        .editor-field
        {
            margin: 0.5em 0 0 0;
            width: 150px;
            float: left;
            color: red;
        }
        /* Styles for validation helpers
-----------------------------------------------------------*/.field-validation-error
        {
            color: #ff0000;
        }
        .field-validation-valid
        {
            display: none;
        }
        .input-validation-error
        {
            border: 1px solid #ff0000;
            background-color: #ffeeee;
        }
        .validation-summary-errors
        {
            font-weight: bold;
            color: #ff0000;
        }
        .validation-summary-valid
        {
            display: none;
        }
        /*Additional Styles
********************************************************/.label
        {
            vertical-align: middle;
        }
        .check-box
        {
            vertical-align: bottom;
            margin: .5em 0 0 0;
        }
        div.column
        {
            float: left;
            width: auto;
        }
        .single-line
        {
            width: 100px;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <div style="float: left; width: 80px; text-align: right">
            Part No.</div>
        <div style="float: left">
            <uc1:PartRegistryNumber ID="PartRegistryNumberEdit" runat="server" class="style5"
                PackageName="<%#PackageName%>" />
            <br />
        </div>
        <span style="float: left; width: 80px; text-align: right">Minor</span><span style="float: left"><uc1:PartRegistryNumber
            ID="PartRegistryNumber2" runat="server" class="style5" PackageName="<%#PackageName%>" />
        </span>
    </div>
    <div>
        <asp:GridView ID="NewGvwpartlist" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
            BorderColor="Tan" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" DetailSummaryText="View Details"
            ForeColor="Black" GridLines="Horizontal" OnRowCancelingEdit="NewGvwpartlist_RowCancelingEdit"
            OnRowDataBound="NewGvwpartlist_RowDataBound" OnRowEditing="NewGvwpartlist_RowEditing"
            OnRowUpdating="NewGvwpartlist_RowUpdating" PageSize="3" RowHeaderColumn=" " ShowFooter="True"
            Width="100%">
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        </asp:linkbutton>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="Update">
                        </asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text="Cancel">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="btnsubmit0" runat="server" CausesValidation="False" OnClick="btnsubmit_Click"
                            Text="Submit" Width="125px" />
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="xcxc" runat="server" CausesValidation="False" CommandName="Edit"
                            Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                    <FooterStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Part No.">
                    <EditItemTemplate>
                        <uc1:PartRegistryNumber ID="PartRegistryNumberEdit" runat="server" class="style5"
                            PackageName="<%#PackageName%>" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%-- <uc1:PartRegistryNumber ID="PartRegistryNumber" runat="server" class="style5" PackageName="<%#PackageName%>" />--%>
                        <asp:Label ID="lblpartno" runat="server" Text='<%# string.Format("-U{0}-",PackageName) %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Minor">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtminor" runat="server" Text='<%#Bind("Minor") %>' Width="45px"
                            Wrap="False"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtminor"
                            ErrorMessage="Minor Can Not Be Empty">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblminor" runat="server" Text='<%# Bind("Minor") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdesc" runat="server" Text='<%# Bind("Description") %>' Width="45px"
                            Wrap="False"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblkey0" runat="server" Text='<%#Bind("Description") %>'>&#39;&gt;</asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TMHU_View">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtview" runat="server" Text='<%#Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblview" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From_ECI">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfromeci" runat="server" Text='<%#Bind("Description") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfromeci" runat="server" Text='<%#Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To_ECI">
                    <EditItemTemplate>
                        <asp:TextBox ID="txttoeci" runat="server" Width="45px" Wrap="False" Text='<%#Bind("Description") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtpartcode"
                            ErrorMessage="Part Code Can Not Be Empty">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltoeci" runat="server"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                No Member Record<br />
            </EmptyDataTemplate>
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="left" />
        </asp:GridView>
    </div>
    <p>
        &nbsp;</p>
    <div>
        <uc1:PartRegistryNumber ID="PartRegistryNumber1" runat="server" PackageName="<%#PackageName%>" />
        <input id="submitbtn" type="button" value="button" />
    </div>
    <p>
    </p>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <p>
        <div class="editor-label">
            Part No.
        </div>
        <div class="editor-field">
            aasdfasdfd
        </div>
    </p>
    <p>
        <table id="tblAdd" style="display: inline">
            <tr>
                <th colspan="2">
                    Part Registry
                </th>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    <label for="PartNo">
                        Part No.:</label>
                </td>
                <td>
                    <input id="firstname" name="firstname" />
                </td>
                <td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    <label for="Pwd">
                        Minor：</label>
                </td>
                <td>
                    <input id="email" name="email" class="required email" />
                </td>
            </tr>
        </table>
    </p>
    </form>
</body>
</html>
