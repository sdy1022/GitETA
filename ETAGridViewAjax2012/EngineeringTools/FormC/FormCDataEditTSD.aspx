<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormCDataEditTSD.aspx.cs"
    Inherits="EngineeringTools_FormC_FormCDataEditTSD" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="jscript">

    function green(target) {
        var td = document.getElementById(target);
        if (td.style.backgroundColor == 'green') {
            td.style.backgroundColor = ''
        }
        else {
            td.style.backgroundColor = 'green'
        }
    }

    function EciLogdataCheck() {
        var itemSelected = false
        var elm = document.getElementsByTagName('input');
        for (var i = 0; i < elm.length; i++) {
            if (elm.item(i).type == "checkbox" && elm.item(i).checked == true) {
                // if (elm.item(i).type == "checkbox" && elm.item(i).checked == true && elm.item(i).value == "chkecilog") {
                //    alert(elm.item(i).value)
                itemSelected = true;
                //   alert(i);
                break;
            }

        }

        return itemSelected;

    }

    function InvokePop(fname, packagename, ecinumber) {



        if (document.getElementById(fname).value == "" || document.getElementById(fname).value == "undefined") {

            //   alert(" popup")

            // if checkbox
            if (EciLogdataCheck()) {
                retVal = window.showModalDialog('http://colweb01/eta/Eci/SelectEciItem_new.asp?Package=' + packagename + '&Eci=' + ecinumber, null, 'dialogWidth=800px;dialogHeight=600px,scrollbars=yes,resizable=yes')
                //       alert(retVal);
                document.getElementById(fname).value = retVal;
            }
        }

        else {
            //    alert(" not popup");
        }




    }
    

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr>
                <th align="left" width="200">
                    <font size="4">Table of Contents</font>
                </th>
                <td>
                    <strong>
                        <asp:Label ID="lbleci" runat="server" Font-Bold="True" ForeColor="Red" Text="ECI "
                            Visible="False"></asp:Label>
                    </strong><font size="3" color="red"><strong>EDIT MODE </strong></font>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Image
                        ID="Image1" runat="server" ImageUrl="http://colweb01/eta/images/docbag1.gif" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="http://colweb01/eta/help/FormCHelp.htm" target="_blank"><font size="3">Form
                        C Help</font></a>&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
        <hr color="navy">
        <table cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr>
                <td align="left" width="150">
                    Package:<strong><asp:Label ID="lblpackage" runat="server"></asp:Label>
                    </strong>
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
                    &nbsp;
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
                <asp:TemplateField HeaderText="Edit" Visible="true">
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
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Part Code is not valid "
            OnServerValidate="PageCodePairValidatingHandler" Display="None">*</asp:CustomValidator>
        <br />
        <asp:GridView ID="Gvwpartlist" runat="server" BorderWidth="1px" BackColor="LightGoldenrodYellow"
            GridLines="Horizontal" CellPadding="2" BorderColor="Tan" PageSize="3" ForeColor="Black"
            DetailSummaryText="View Details" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False"
            BorderStyle="Solid" OnRowDataBound="Gvwpartlist_RowDataBound" AutoGenerateEditButton="True"
            OnRowEditing="Gvwpartlist_RowEditing" OnRowCancelingEdit="Gvwpartlist_RowCancelingEdit"
            OnRowUpdating="Gvwpartlist_RowUpdating" AutoGenerateDeleteButton="True" 
            onrowdeleting="Gvwpartlist_RowDeleting">
            <HeaderStyle Font-Bold="True" BackColor="Tan"></HeaderStyle>
            <Columns>
                <asp:TemplateField HeaderText="ECI Log">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkecilog" runat="server" />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ass'y Name">
                    <EditItemTemplate>
                        <asp:Label ID="lblassyname" runat="server" Text='<%# Bind("ADD_INDEX") %>' Visible="false"></asp:Label>
                        <asp:DropDownList ID="ddlassyname" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlassyname"
                            ErrorMessage="Name Can Not Be Empty">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblassyname" runat="server" ></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Key #">
                    <EditItemTemplate>
                        <asp:Label ID="lblkey" runat="server" Text='<%# Bind("KeyCode") %>' Visible="False"></asp:Label>
                        <asp:Label ID="lblaitemid" runat="server" Text='<%# Bind("AItemid") %>' Visible="false"></asp:Label>
                      <asp:DropDownList ID="ddlkey" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem>&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorkey" runat="server" ControlToValidate="ddlkey"
                            ErrorMessage="Key Can Not Be Empty">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblkey" runat="server" Text='<%#Bind("KeyCode") %>'>&#39;&gt;</asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Add TFC">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtaddtfc" runat="server" Text='<%# Bind("ADD_TFC") %>'></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddtfc" runat="server" ControlToValidate="txtaddtfc"
                            ErrorMessage="Add TFC Can Not Be Empty">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp1" runat="server" ErrorMessage="ADD TFC Length Must Be 4 "
                            ControlToValidate="txtaddtfc" ValidationExpression="^[a-zA-Z0-9\s]{4,4}$">*</asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbladdtfc" runat="server" Text='<%# Bind("ADD_TFC") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Add Index">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtaddindex" runat="server"  Text='<%# Bind("ADD_INDEXSUB") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidatoraddindex" runat="server" ControlToValidate="txtaddindex"
                            ErrorMessage="Add Index Can Not Be Empty">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp3" runat="server" ErrorMessage="ADD INDEX Length Must Be 4 Or 6"
                            ControlToValidate="txtaddindex" ValidationExpression="^[a-zA-Z0-9\s]{4}$|^[a-zA-Z0-9\s]{6}$">*</asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbladdindex" runat="server"  Text='<%# Bind("ADD_INDEX") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Del TFC">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdeltfc" runat="server" Text='<%# Bind("DEL_TFC") %>'></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidatordeltfc" runat="server" ControlToValidate="txtdeltfc"
                            ErrorMessage="Del TFC Can Not Be Empty">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp2" runat="server" ErrorMessage="DEL TFC Length Must Be 4 "
                            ControlToValidate="txtdeltfc" ValidationExpression="^[a-zA-Z0-9\s]{4,4}$">*</asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbldeltfc" runat="server" Text='<%# Bind("DEL_TFC") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Del Index">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdelindex" runat="server"  Text='<%# Bind("DEL_INDEXSUB") %>'></asp:TextBox>
                        <asp:Label ID="lbldelindex" runat="server" Text='<%# Bind("DEL_INDEX") %>' Visible="False"></asp:Label>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidatordelindex" runat="server" ControlToValidate="txtdelindex"
                            ErrorMessage="Del Index Can Not Be Empty">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegExp4" runat="server" ErrorMessage="DEL INDEX Length Must Be 4 Or 6"
                            ControlToValidate="txtdelindex" ValidationExpression="^[a-zA-Z0-9\s]{4}$|^[a-zA-Z0-9\s]{6}$">*</asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                         <asp:Label ID="lbldelindexshow" runat="server"  Text='<%# Bind("DEL_INDEXSUB") %>' ></asp:Label>
          
                        <asp:Label ID="lbldelindex" runat="server" Text='<%# Bind("DEL_INDEX") %>'  Visible="False"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtdesc" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtdesc"
                            ErrorMessage="Description Can Not Be Empty">*</asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="From ECI">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtfromeci" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblfromeci" runat="server" Text='<%# Bind("FROM_ECI") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="To ECI">
                    <EditItemTemplate>
                        <asp:TextBox ID="txttoeci" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbltoeci" runat="server" Text='<%# Bind("TO_ECI") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle BorderStyle="Solid" HorizontalAlign="Center" />
                </asp:TemplateField>
                  <asp:TemplateField Visible="False">
                      <EditItemTemplate>
                        <asp:Label ID="lblitemid" runat="server" Text='<%# Bind("ID") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblitemid" runat="server" Text='<%# Bind("ID") %>' />
                    </ItemTemplate>
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
        <asp:HiddenField ID="hiddenfield" runat="server" />
    </div>
    </form>
</body>
</html>
