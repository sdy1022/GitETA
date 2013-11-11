<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartListNewTSD.aspx.cs" Inherits="EngineeringTools_PartsList_PartListNewTSD" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Src="ETAGridViewInsert.ascx" TagName="ETAGridViewInsert" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="jscript">

    var previousRowID = 'row';
    // var gridviewselectedrowid = "";
    function ChangeRowColor(rowID) {


        if (previousRowID == rowID)
            return; //do nothing
        else if (previousRowID != 'row')
        //change the color of the previous row back to white
            document.getElementById(previousRowID).style.backgroundColor = "";

        document.getElementById(rowID).style.backgroundColor = "32cd32";
        document.getElementById('<%=hiddenfieldgvselectrowid.ClientID%>').value = rowID;
        previousRowID = rowID

        //        var color = document.getElementById(rowID).style.backgroundColor;

        //        if (color != 'yellow')

        //            oldColor = color;

        //        if (color == 'yellow')

        //            document.getElementById(rowID).style.backgroundColor = oldColor;

        //        else document.getElementById(rowID).style.backgroundColor = 'yellow';

        //        document.getElementById('row3').style.backgroundColor = 'red';

    }

    function ChangeRowColorNew(rowname, rowID) {


        if (previousRowID == rowname + rowID)
            return; //do nothing
        else if (previousRowID != 'row')
        //change the color of the previous row back to white
            document.getElementById(previousRowID).style.backgroundColor = "";

        document.getElementById(rowname + rowID).style.backgroundColor = "32cd32";
        document.getElementById('<%=hiddenfieldgvselectrowid.ClientID%>').value = rowID;
        previousRowID = rowname + rowID

        //        var color = document.getElementById(rowID).style.backgroundColor;

        //        if (color != 'yellow')

        //            oldColor = color;

        //        if (color == 'yellow')

        //            document.getElementById(rowID).style.backgroundColor = oldColor;

        //        else document.getElementById(rowID).style.backgroundColor = 'yellow';

        //        document.getElementById('row3').style.backgroundColor = 'red';

    }




    function uncheckOthers(id) {
        var elm = document.getElementsByTagName('input');
        for (var i = 0; i < elm.length; i++) {
            if (elm.item(i).type == "checkbox" && elm.item(i) != id)
                elm.item(i).checked = false;
        }


    }

    function uncheckOthersradio(id) {

        var elm = document.getElementsByTagName('input');
        for (var i = 0; i < elm.length; i++) {
            if (elm.item(i).type == "radio" && elm.item(i) != id && elm.item(i).value != "Before" && elm.item(i).value != "After") {
                // alert(elm.item(i).value);
                elm.item(i).checked = false;
            }
        }
    }




    function InvokePopold(fname, packagename, ecinumber) {


        if (dataCheck2()) {


            if (document.getElementById(fname).value == "" || document.getElementById(fname).value == "undefined") {


                var dialogResults = null;

                while (dialogResults == null) {
                    //   alert(dialogResults);
                    dialogResults = window.showModalDialog('http://colweb01/eta/Eci/SelectEciPLItem_new.asp?Package=' + packagename + '&Eci=' + ecinumber, null, 'width=800mheight=600,scrollbars=yes,resizable=yes')
                }

                document.getElementById(fname).value = dialogResults;


            }

        }

        else {

            // alert("no ecilog");
        }

    }


    function InvokePop(fname, packagename, ecinumber) {

        if (document.getElementById(fname).value == "" || document.getElementById(fname).value == "undefined") {

            //   alert(" popup")

            // if checkbox
            if (dataCheck2()) {
                // alert(ecinumber);
                //                var url2 = "http://colweb01/eta/Eci/SelectEciPLItem_new.asp?Package=" + packagename + "&Eci=" + ecinumber;
                //               // alert(url2);
                //                //retVal = window.showModalDialog('http://colweb01/eta/Eci/SelectEciPLItem_new.asp?Package=' + packagename + '&Eci=' + ecinumber, null, 'width=800mheight=600,scrollbars=yes,resizable=yes')
                //                retVal = window.showModalDialog(url2, null, 'width=800mheight=600,scrollbars=yes,resizable=yes')
                //
                //                //       alert(retVal);

                var url2 = "http://colweb01/eta/Eci/SelectEciPLItem_new.asp?Package=" + packagename + "&Eci=" + ecinumber;
                //alert(url2);
                //retVal = window.showModalDialog('http://colweb01/eta/Eci/SelectEciPLItem_new.asp?Package=' + packagename + '&Eci=' + ecinumber, null, 'width=800mheight=600,scrollbars=yes,resizable=yes')
                var retVal = window.showModalDialog(url2, window, "dialogWidth=1200px;dialogHeight=1000px"); //, 'width=1800mheight=1600,scrollbars=yes,resizable=yes');
                // retVal = window.showModalDialog(url2, null);//, 'width=1800mheight=1600,scrollbars=yes,resizable=yes')


                document.getElementById(fname).value = retVal;
            }
        }

    }



    function logInfo() {

        if (dataCheck()) {
            //**Look for checked log boxes
            var oRad = document.getElementsByName('radio');
            var checkCount = 0;
            for (j = 0; j < oRad.length; j++) {
                var vItem = 'rad' + j;
                var rad1 = document.getElementById(vItem);
                if (rad1.checked) {
                    checkCount = checkCount + 1
                }
            }

            if (checkCount > 0) {
                //**Open window to select item to attach to

                alert('../../Eci/SelectEciPLItem.asp?Package=' + hidPackage.value + '&Eci=' + frm1.hideci.value, null, 'height=600,scrollbars=yes,resizable=yes');

                window.open('../../Eci/SelectEciPLItem.asp?Package=' + hidPackage.value + '&Eci=' + frm1.hideci.value, null, 'height=600,scrollbars=yes,resizable=yes');
            }
            else {
                //**Continue as normal, no logging
                frm1.submit()
            }
        }
        else {
            alert('Please select where to insert item');
        }
    }

    function dataCheck2() {
        var itemSelected = false;
        var elm = document.getElementsByTagName('input');
        for (var i = 0; i < elm.length; i++) {
            //	        var s1 = elm.item(i).type;
            //	        var s2 = elm.item(i).checked;
            //	        var s3 = elm.item(i).value;
            //	        var s4 = elm.item(i).name;

            if (elm.item(i).type == "radio" && elm.item(i).checked == true && elm.item(i).value == "rdoselect") {
                itemSelected = true;
                //  alert(i);
                break;
            }

        }

        var ecilogflag = false;

        for (var j = 1; j < 6; j++) {

            var chklogitem = document.getElementById("chkecilog" + j);
            if (chklogitem.checked == true) {
                ecilogflag = true;
                break;
            }
        }

        return itemSelected & ecilogflag;

    }
    function dataCheck() {
        var oRad = document.getElementsByName('radio');
        var itemSelected = false
        for (var i = 0; i < oRad.length; i++) {
            var target = "tr" + i
            if (oRad[i].checked) {
                itemSelected = true
            }
        }
        if (itemSelected) {
            return true;
        }
        else {
            return false;
            alert('Please select where to insert item');
        }
    }

    function green(target) {
        var td = document.getElementById(target);
        if (td.style.backgroundColor == 'green') {
            td.style.backgroundColor = ''
        }
        else {
            td.style.backgroundColor = 'green'
        }
    }

    function lengthCheck(target, maxChar) {
        if (target.innerText.length >= maxChar) {
            alert('Maximum characters has been reached!')
        }
    }
	
	
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
        li.nav
        {
            list-style-type: none;
            float: right;
            padding: 0px 10px 0px 10px;
        }
    </style>
    </style>
</head>
<body alink="loginfo()">
    <form id="form1" runat="server" autocomplete="off">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600"
        ScriptMode="Release">
    </asp:ScriptManager>--%>
    <div>
        <table cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr>
                <th align="left" width="300">
                    <font size="4">Special Design Parts List</font>
                </th>
                <td>
                    <font color="green"><strong>INSERT MODE</strong></font>
                </td>
            </tr>
        </table>
        <hr color="navy">
        <table cellspacing="0" cellpadding="1" width="100%" border="0">
            <tr>
                <th width="150">
                    PartList Number
                </th>
                <td>
                    <asp:Label ID="lblmodule" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblecimode" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <%-- <li class="nav">
                    <asp:Button ID="btnfiler" runat="server" Text="Filter" Width="81px" 
                        Visible="False" />
                </li>
                <li class="nav">ECI
                    <asp:DropDownList ID="ddleci" runat="server" 
                        Enabled="False">
                        <asp:ListItem runat="server" Text="No Filter" Value="No filter" Enabled="False"></asp:ListItem>
                    </asp:DropDownList>
                </li>
                <li class="nav">Part List
                    <asp:DropDownList ID="ddlpartlist" runat="server" AutoPostBack="True" >
                    </asp:DropDownList>
                </li>--%>
    </div>
    <table cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td>
                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                    <tr id="trHead0" bordercolor="Blue">
                        <td style="border-right: none; border-top-width: 5px">
                            <asp:GridView ID="HeaderGridview" runat="server" Width="100%" AutoGenerateColumns="False"
                                OnRowDataBound="HeaderGridview_RowDataBound" CellPadding="0" CellSpacing="0"
                                BorderColor="Blue" BorderStyle="Solid" BorderWidth="3px">
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
                                        <font size="2">Module:&nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
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
                                    <th align="left" class="style1">
                                        <font size="2">&nbsp;</font>
                                    </th>
                                    <th align="left" class="style1">
                                        <asp:TextBox ID="txtNAME4" runat="server" Width="380px"></asp:TextBox>
                                    </th>
                                    <th align="left" class="style1">
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
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;</td>
        </tr>
    </table>
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblpartlisterror" runat="server" ForeColor="Red"></asp:Label>
                <table cellspacing="0" cellpadding="1" width="100%" border="1">
                    <tr>
                        <th bordercolor="blue" class="style5">
                            &nbsp;
                        </th>
                        <%--<th bordercolor="blue" class="style1">
                                <font size="1">ECI LOG </font>
                            </th>
                            <th bordercolor="blue" class="style1">
                                <font size="1">REV.</font>
                            </th>--%>
                      
                        <th bordercolor="blue" class="style1">
                            <font size="1">LEVEL</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">PART NO.</font>
                        </th>
                          <th bordercolor="blue" class="style1">
                            <font size="1">MINOR</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">PART NAME</font>
                        </th>
                        <th bordercolor="blue" class="style2">
                            <font size="1">QTY</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">MATERIAL</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">SIZE</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">DWG</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">COMMENTS</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">FROM(PN)</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">FROM(LVL)</font>
                        </th>
                        <th bordercolor="blue" class="style1">
                            <font size="1">To(PN)</font>
                        </th>
                        <%-- <th bordercolor="blue" class="style1">
                                <font size="1">To(LVL)</font>
                            </th>--%>
                    </tr>
                    <tr>
                        <td bordercolor="blue" align="center" class="style4">
                            <font size="1">
                                <asp:CheckBox ID="chk1" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged" />
                            </font>
                        </td>
                        <%--  <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:CheckBox ID="chkecilog1" runat="server" />
                                </font>
                            </td>--%>
                        <%--  <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:Image ID="imgrev" runat="server" ImageUrl="http://colweb01/eta/images/RevA.gif" />
                                </font>
                            </td>--%>
                       
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddllevel1" runat="server" Width="50px" Enabled="False">
                                <%--  <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartno1" runat="server" Enabled="False" OnTextChanged="txtpartno1_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblpartlisterror1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </font>
                        </td>
                         <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtminor1" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartname1" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center" class="style3">
                            <font size="1">
                                <asp:TextBox ID="txtqty1" runat="server" Width="61px" Enabled="False" Height="22px"></asp:TextBox>
                                  <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtqty1"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
  
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtmaterial1" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtsize1" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddldwg1" runat="server" Width="50px" Enabled="False">
                                <asp:ListItem Value="0">A</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                                <asp:ListItem Value="2">S</asp:ListItem>
                                <asp:ListItem Value="3">K</asp:ListItem>
                                <asp:ListItem Value="4">C</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtcomment1" runat="server" EnableTheming="False" TextMode="MultiLine"
                                    Enabled="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfrompn1" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfromv1" runat="server" EnableTheming="False" MaxLength="9"></asp:TextBox></font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfromv1"
                                ErrorMessage="Error" ValidationExpression="^[a-zA-Z0-9\s]{9}$">*</asp:RegularExpressionValidator></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtopn1" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <%--  <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                    </tr>
                    <tr>
                        <td bordercolor="blue" align="center" class="style4">
                            <font size="1">
                                <asp:CheckBox ID="chk2" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged" />
                            </font>
                        </td>
                        <%--<td bordercolor="blue" align="center">
                                <font size="1">
                                    <asp:TextBox ID="txtov1" runat="server" EnableTheming="False" 
                                        MaxLength="99"></asp:TextBox></font>
                            </td>--%>
                       
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddllevel2" runat="server" Width="50px" Enabled="False">
                                <%--    <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartno2" runat="server" Enabled="False" OnTextChanged="txtpartno1_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblpartlisterror2" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </font>
                        </td>
                         <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtminor2" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartname2" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center" class="style3">
                            <font size="1">
                                <asp:TextBox ID="txtqty2" runat="server" Width="61px" Enabled="False" Height="22px"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtqty2"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
  
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtmaterial2" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtsize2" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddldwg2" runat="server" Width="50px" Enabled="False">
                                <asp:ListItem Value="0">A</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                                <asp:ListItem Value="2">S</asp:ListItem>
                                <asp:ListItem Value="3">K</asp:ListItem>
                                <asp:ListItem Value="4">C</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtcomment2" runat="server" EnableTheming="False" TextMode="MultiLine"
                                    Enabled="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfrompn2" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfromv2" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtfromv2"
                                ErrorMessage="Error" ValidationExpression="^[a-zA-Z0-9\s]{9}$">*</asp:RegularExpressionValidator></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtopn2" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <%--<td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:CheckBox ID="chkecilog2" runat="server" />
                                </font>
                            </td>
                            <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:Image ID="imgrev0" runat="server" ImageUrl="http://colweb01/eta/images/RevA.gif" />
                                </font>
                            </td>--%>
                    </tr>
                    <tr>
                        <td bordercolor="blue" align="center" class="style4">
                            <font size="1">
                                <asp:CheckBox ID="chk3" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged" />
                            </font>
                        </td>
                        <%--    <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                       
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddllevel3" runat="server" Width="50px" Enabled="False">
                                <%-- <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartno3" runat="server" Enabled="False" OnTextChanged="txtpartno1_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblpartlisterror3" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </font>
                        </td>
                        
                         <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtminor3" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartname3" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center" class="style3">
                            <font size="1">
                                <asp:TextBox ID="txtqty3" runat="server" Width="61px" Enabled="False" Height="22px"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtqty3"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
  
                            </font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtmaterial3" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtsize3" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddldwg3" runat="server" Width="50px" Enabled="False">
                                <asp:ListItem Value="0">A</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                                <asp:ListItem Value="2">S</asp:ListItem>
                                <asp:ListItem Value="3">K</asp:ListItem>
                                <asp:ListItem Value="4">C</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtcomment3" runat="server" EnableTheming="False" TextMode="MultiLine"
                                    Enabled="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfrompn3" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfromv3" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtfromv3"
                                ErrorMessage="Error" ValidationExpression="^[a-zA-Z0-9\s]{9}$">*</asp:RegularExpressionValidator></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtopn3" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <%-- <td bordercolor="blue" align="center">
                                <font size="1">
                                    <asp:TextBox ID="txtov2" runat="server" EnableTheming="False" 
                                        MaxLength="99"></asp:TextBox></font>
                            </td>--%>
                    </tr>
                    <tr>
                        <td bordercolor="blue" align="center" class="style4">
                            <font size="1">
                                <asp:CheckBox ID="chk4" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged" />
                            </font>
                        </td>
                        <%-- <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:CheckBox ID="chkecilog3" runat="server" />
                                </font>
                            </td>
                            <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:Image ID="imgrev1" runat="server" ImageUrl="http://colweb01/eta/images/RevA.gif" />
                                </font>
                            </td>--%>
                       
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddllevel4" runat="server" Width="50px" Enabled="False">
                                <%-- <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartno4" runat="server" Enabled="False" OnTextChanged="txtpartno1_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblpartlisterror4" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </font>
                        </td>
                         <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtminor4" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartname4" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center" class="style3">
                            <font size="1">
                                <asp:TextBox ID="txtqty4" runat="server" Width="61px" Enabled="False" Height="22px"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator4" runat="server" ControlToValidate="txtqty4"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
  </font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtmaterial4" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtsize4" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddldwg4" runat="server" Width="50px" Enabled="False">
                                <asp:ListItem Value="0">A</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                                <asp:ListItem Value="2">S</asp:ListItem>
                                <asp:ListItem Value="3">K</asp:ListItem>
                                <asp:ListItem Value="4">C</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtcomment4" runat="server" EnableTheming="False" TextMode="MultiLine"
                                    Enabled="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfrompn4" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfromv4" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox>
                            </font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtfromv4"
                                ErrorMessage="Error" ValidationExpression="^[a-zA-Z0-9\s]{9}$">*</asp:RegularExpressionValidator>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtopn4" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                            </font>
                        </td>
                        <%-- <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                    </tr>
                    <tr>
                        <td bordercolor="blue" align="center" class="style4">
                            <font size="1">
                                <asp:CheckBox ID="chk5" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged" />
                            </font>
                        </td>
                        <%-- <td bordercolor="blue" align="center">
                                <font size="1">
                                    <asp:TextBox ID="txtov3" runat="server" EnableTheming="False" 
                                        MaxLength="99"></asp:TextBox></font>
                            </td>--%>
                       
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddllevel5" runat="server" Width="50px" Enabled="False">
                                <%-- <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartno5" runat="server" Enabled="False" OnTextChanged="txtpartno1_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblpartlisterror5" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </font>
                        </td>
                         <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtminor5" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartname5" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center" class="style3">
                            <font size="1">
                                <asp:TextBox ID="txtqty5" runat="server" Width="61px" Enabled="False" Height="22px"></asp:TextBox>
                                  <asp:CustomValidator ID="CustomValidator5" runat="server" ControlToValidate="txtqty5"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
  
                               </font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtmaterial5" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtsize5" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddldwg5" runat="server" Width="50px" Enabled="False">
                                <asp:ListItem Value="0">A</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                                <asp:ListItem Value="2">S</asp:ListItem>
                                <asp:ListItem Value="3">K</asp:ListItem>
                                <asp:ListItem Value="4">C</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtcomment5" runat="server" EnableTheming="False" TextMode="MultiLine"
                                    Enabled="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfrompn5" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfromv5" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtfromv5"
                                ErrorMessage="Error" ValidationExpression="^[a-zA-Z0-9\s]{9}$">*</asp:RegularExpressionValidator>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtopn5" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <%-- <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:CheckBox ID="chkecilog4" runat="server" />
                                </font>
                            </td>
                            <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:Image ID="imgrev2" runat="server" ImageUrl="http://colweb01/eta/images/RevA.gif" />
                                </font>
                            </td>--%>
                    </tr>
                    <tr>
                        <td bordercolor="blue" align="center" class="style4">
                            <font size="1">
                                <asp:CheckBox ID="chk6" runat="server" AutoPostBack="True" OnCheckedChanged="chk1_CheckedChanged" />
                            </font>
                        </td>
                        <%-- <asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                       
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddllevel6" runat="server" Width="50px" Enabled="False">
                                <%--<asp:ListItem Value="0">1</asp:ListItem>
                                    <asp:ListItem Value="1">2</asp:ListItem>
                                    <asp:ListItem Value="2">3</asp:ListItem>
                                    <asp:ListItem Value="3">4</asp:ListItem>
                                    <asp:ListItem Value="4">5</asp:ListItem>
                                    <asp:ListItem Value="5">6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartno6" runat="server" Enabled="False" OnTextChanged="txtpartno1_TextChanged"></asp:TextBox>
                                <asp:Label ID="lblpartlisterror6" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </font>
                        </td>
                         <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtminor6" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtpartname6" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center" class="style3">
                            <font size="1">
                                <asp:TextBox ID="txtqty6" runat="server" Width="61px" Enabled="False" Height="22px"></asp:TextBox>
                                   <asp:CustomValidator ID="CustomValidator6" runat="server" ControlToValidate="txtqty6"
                                    ErrorMessage="Invalid Quantity" OnServerValidate="CustomizedQuantityValidationHandlerForQuantity">*</asp:CustomValidator>
  
                                
                                </font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtmaterial6" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtsize6" runat="server" Enabled="False"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <asp:DropDownList ID="ddldwg6" runat="server" Width="50px" Enabled="False">
                                <asp:ListItem Value="0">A</asp:ListItem>
                                <asp:ListItem Value="1">N</asp:ListItem>
                                <asp:ListItem Value="2">S</asp:ListItem>
                                <asp:ListItem Value="3">K</asp:ListItem>
                                <asp:ListItem Value="4">C</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtcomment6" runat="server" EnableTheming="False" TextMode="MultiLine"
                                    Enabled="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfrompn6" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtfromv6" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtfromv6"
                                ErrorMessage="Error" ValidationExpression="^[a-zA-Z0-9\s]{9}$">*</asp:RegularExpressionValidator>
                        </td>
                        <td bordercolor="blue" align="center">
                            <font size="1">
                                <asp:TextBox ID="txtopn6" runat="server" EnableTheming="False" MaxLength="99"></asp:TextBox></font>
                        </td>
                        <%--  <td bordercolor="blue" align="center">
                                <font size="1">
                                    <asp:TextBox ID="txtov4" runat="server" EnableTheming="False" 
                                        MaxLength="99"></asp:TextBox></font>
                            </td>--%>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:Label ID="lblresult" runat="server" Visible="False" Width="781px"></asp:Label>
    <br />
    <br />
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="155px" OnClick="btnsubmit_Click"
        Height="26px" />
    <%--<td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:CheckBox ID="chkecilog5" runat="server" />
                                </font>
                            </td>
                            <td bordercolor="blue" align="center" class="style4">
                                <font size="1">
                                    <asp:Image ID="imgrev3" runat="server" ImageUrl="http://colweb01/eta/images/RevA.gif" />
                                </font>
                            </td>--%>
    <asp:HiddenField ID="hiddenfield" runat="server" />
    <asp:HiddenField ID="hiddenfieldgvselectrowid" runat="server" />
    </form>
</body>
</html>

