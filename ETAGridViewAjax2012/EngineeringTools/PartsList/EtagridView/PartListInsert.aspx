<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartListInsert.aspx.cs" Inherits="EngineeringTools_PartsList_EtagridView_PartListInsert" %>
<%@ Import namespace="System.Data" %>
<%@ Register src="ETAGridViewInsert.ascx" tagname="ETAGridViewInsert" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language=jscript>

    var previousRowID = 'row';
    function ChangeRowColor(rowID) {

        
        if (previousRowID == rowID)
            return; //do nothing
        else if (previousRowID != 'row')
        //change the color of the previous row back to white
            document.getElementById(previousRowID).style.backgroundColor = "";

        document.getElementById(rowID).style.backgroundColor = "32cd32";

        previousRowID = rowID
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
            if (elm.item(i).type == "radio" && elm.item(i) != id && elm.item(i).value !="Before" && elm.item(i).value !="After")
                {
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
	
	if (dataCheck())
		{
		//**Look for checked log boxes
		var oRad=document.getElementsByName('radio');	
		var checkCount=0;
		for (j=0;j<oRad.length;j++)
			{
			var vItem='rad'+j;
			var rad1=document.getElementById(vItem);
			if (rad1.checked)
				{
				checkCount=checkCount+1
				}
			}
			
		if (checkCount>0)
			{
			    //**Open window to select item to attach to

			    alert('../../Eci/SelectEciPLItem.asp?Package=' + hidPackage.value + '&Eci=' + frm1.hideci.value, null, 'height=600,scrollbars=yes,resizable=yes');

			    window.open('../../Eci/SelectEciPLItem.asp?Package=' + hidPackage.value + '&Eci=' + frm1.hideci.value, null, 'height=600,scrollbars=yes,resizable=yes');
			}
		else			
			{
			//**Continue as normal, no logging
			frm1.submit()
			}
		}
	else
		{
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
function dataCheck()
	{
	var oRad=document.getElementsByName('radio');	
	var itemSelected=false
	for (var i=0; i<oRad.length; i++)
		{
		var target="tr"+i
		if (oRad[i].checked)
			{
			itemSelected=true
			}
		}
	if (itemSelected)
		{
		return true;
		}
	else
		{
		return false;
		alert('Please select where to insert item');
		}
	}

function green(target)
	{
	var td=document.getElementById(target);
	if(td.style.backgroundColor=='green')
		{
		td.style.backgroundColor=''
		}
	else
		{
		td.style.backgroundColor='green'
		}		
	}

function lengthCheck(target,maxChar)
	{
	if(target.innerText.length>=maxChar)
		{
		alert('Maximum characters has been reached!')
		}
	}
	
	
</script>	
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body alink="loginfo()">
<form id="form1" runat="server"  autocomplete="off">
<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="3600"  ScriptMode="Release">
</asp:ScriptManager>

    <div>
    <TABLE cellSpacing=0 cellPadding=1 width="100%" border=0>
  <TR>
	<TH align=left width=300><FONT size=4>Special Design Parts 
      List</FONT>   </TH>
    <TD><FONT color=green><STRONG>INSERT MODE</STRONG></FONT></TD>
  </TR>
</TABLE>
<HR color=navy>    
<TABLE cellSpacing=0 cellPadding=1 width="100%" border=0>
  <TR>
    <TH width=150>Module Number</TH>
    <TD><asp:Label ID="lblmodule" runat="server"></asp:Label></TD></TR>
  <TR>
  <TD>
    <asp:Label ID="lblecimode" runat="server"></asp:Label>
      </TD></TR>
</TABLE>
<STRONG>Insert:</STRONG>

        <asp:RadioButtonList ID="rdolist" runat="server" Font-Bold="True">
            <asp:ListItem Selected="True">Before</asp:ListItem>
            <asp:ListItem>After</asp:ListItem>
        </asp:RadioButtonList>


       


    </div>
<table cellSpacing=0 cellPadding=1 width="100%" border=0>
<tr>
<td>
<asp:GridView ID="myGridView" Runat="server" BorderWidth="1px" 
           GridLines="Horizontal" CellPadding="2"
           PageSize="3" 
            Width="100%" RowHeaderColumn=" " 
            AutoGenerateColumns="False" 
            HorizontalAlign="Left" onrowdatabound="myGridView_RowDataBound" 
        onselectedindexchanged="myGridView_SelectedIndexChanged" 
        ShowHeader="False">
            <EmptyDataTemplate>
               No Member Record<br />
            </EmptyDataTemplate>           
            
             <Columns>
                 <asp:TemplateField Visible="False" >
                     <ItemTemplate>
                         <asp:CheckBox ID="chkselect" runat="server"  AutoPostBack=true
                             oncheckedchanged="chkselect_CheckedChanged"   />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField >
                     <ItemStyle HorizontalAlign="Center" />
                     <ItemTemplate>                        
                         <uc1:ETAGridViewInsert ID="ETAGridView1"      EnableGridView=true     runat="server" HeaderID='<%# Bind("Headerid") %>'   PackageName='<%# PackageName %>'  EciAcid='<%# EciAcid%>' EciNumber='<%# EciNumber%>'  KeyA='<%# keya%>'   EciMode='<%# EciMode%>' Module='<%# Module%>' />
                     </ItemTemplate>
                 </asp:TemplateField>
            
            </Columns>
        <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
 
</td>

</tr>
     <tr>
     <td>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
     
            <TABLE cellSpacing=0 cellPadding=1 width="100%" border=1>
 	<TR>
			<TH bordercolor=blue class="style5">&nbsp;</TH>
			<TH bordercolor=blue class="style1"><FONT size=1>ECI LOG </FONT></TH>
			<TH bordercolor=blue class="style1"><FONT size=1>REV.</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>LEVEL</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>PART NO.</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>PART NAME</FONT></TH>
		<TH bordercolor=blue class="style2"><FONT size=1>QTY</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>MATERIAL</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>SIZE</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>DWG</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>COMMENTS</FONT></TH>
		
		</TR>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk1" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
                </FONT></TD>
            
            <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chkecilog1" runat="server" 
                    />
                </FONT></TD>
               <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
               <asp:Image ID="imgrev" runat="server"  
                       ImageUrl="http://colweb01/eta/images/RevA.gif"/>
                </FONT></TD>   
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel1" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno1" runat="server" Enabled="False" 
                ontextchanged="txtpartno1_TextChanged"  
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror1" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname1" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty1" 
                runat="server" Width="61px" Enabled="False" height="22px"   
                    ></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtqty1"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1><asp:TextBox ID="txtmaterial1" 
                runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize1" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg1" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment1" 
                runat="server" EnableTheming="False" TextMode="MultiLine" Enabled="False" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk2" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
                </FONT></TD>
            
            <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chkecilog2" runat="server" 
                    />
                </FONT></TD>
               <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
               <asp:Image ID="imgrev0" runat="server"  
                       ImageUrl="http://colweb01/eta/images/RevA.gif"/>
                </FONT></TD>   
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel2" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno2" runat="server" Enabled="False" 
                ontextchanged="txtpartno1_TextChanged"  
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror2" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname2" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty2" 
                runat="server" Width="61px" Enabled="False" height="22px"   
                    ></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtqty2"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial2" 
                runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize2" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg2" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment2" 
                runat="server" EnableTheming="False" TextMode="MultiLine" Enabled="False" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk3" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
                </FONT></TD>
            
            <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chkecilog3" runat="server" 
                    />
                </FONT></TD>
               <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
               <asp:Image ID="imgrev1" runat="server"  
                       ImageUrl="http://colweb01/eta/images/RevA.gif"/>
                </FONT></TD>   
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel3" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno3" runat="server" Enabled="False" 
                ontextchanged="txtpartno1_TextChanged"  
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror3" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname3" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty3" 
                runat="server" Width="61px" Enabled="False" height="22px"   
                    ></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtqty3"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial3" 
                runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize3" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg3" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment3" 
                runat="server" EnableTheming="False" TextMode="MultiLine" Enabled="False" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk4" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
                </FONT></TD>
            
            <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chkecilog4" runat="server" 
                    />
                </FONT></TD>
               <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
               <asp:Image ID="imgrev2" runat="server"  
                       ImageUrl="http://colweb01/eta/images/RevA.gif"/>
                </FONT></TD>   
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel4" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno4" runat="server" Enabled="False" 
                ontextchanged="txtpartno1_TextChanged"  
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror4" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname4" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty4" 
                runat="server" Width="61px" Enabled="False" height="22px"   
                    ></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtqty4"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial4" 
                runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize4" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg4" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment4" 
                runat="server" EnableTheming="False" TextMode="MultiLine" Enabled="False" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk5" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
                </FONT></TD>
            
            <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chkecilog5" runat="server" 
                    />
                </FONT></TD>
               <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
               <asp:Image ID="imgrev3" runat="server"  
                       ImageUrl="http://colweb01/eta/images/RevA.gif"/>
                </FONT></TD>   
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel5" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno5" runat="server" Enabled="False" 
                ontextchanged="txtpartno1_TextChanged"  
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror5" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname5" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty5" 
                runat="server" Width="61px" Enabled="False" height="22px"   
                    ></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtqty5"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial5" 
                runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize5" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg5" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment5" 
                runat="server" EnableTheming="False" TextMode="MultiLine" Enabled="False" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk6" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
                </FONT></TD>
            
            <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chkecilog6" runat="server" 
                    />
                </FONT></TD>
               <TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
               <asp:Image ID="imgrev4" runat="server"  
                       ImageUrl="http://colweb01/eta/images/RevA.gif"/>
                </FONT></TD>   
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel6" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno6" runat="server" Enabled="False" 
                ontextchanged="txtpartno1_TextChanged"  
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror6" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname6" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty6" 
                runat="server" Width="61px" Enabled="False" height="22px"   
                    ></asp:TextBox>
            <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtqty6"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial6" 
                runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize6" runat="server" Enabled="False" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg6" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment6" 
                runat="server" EnableTheming="False" TextMode="MultiLine" Enabled="False" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
 </TABLE>
     
</ContentTemplate>
     
     <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnsubmit" />
            </Triggers>
     </asp:UpdatePanel>
     </td>
     </tr>  
          
          
</table>
           
    
           
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:Label ID="lblresult" runat="server" Visible="False" 
        Width="781px"></asp:Label>
    <br/>
       <br />
    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="155px" 
        onclick="btnsubmit_Click" Height="26px"   />
           
       <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
        Text="Button" Visible="False" />
        
        <asp:HiddenField ID="hiddenfield" runat="server" /> 
              
       </form>
</body>
</html>
