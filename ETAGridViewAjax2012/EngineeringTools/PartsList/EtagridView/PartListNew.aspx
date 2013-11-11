<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartListNew.aspx.cs" Inherits="EngineeringTools_PartsList_PartListNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<head runat="server">
    <title></title>
    <script src="http://colweb01/eta/scripts/jquery.min.js" type="text/javascript"></script>

     
    <% if (false){%>
    <script src="../../../scripts/jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>
    <% }%>
     
       <script type="text/javascript">
           $(function() {

//           $("#txtcomment14").keyup(function() {
//                   alert("test ");
//               });
//               
               
               
               //  alert($('textarea[name^="txtcomment"]').size());
               $('textarea[name^="txtcomment"]').each(function() {
                 //  alert(this.id);
                   //alerr(this.value.length);
                   $("#" + this.id).keyup(function() {
                      
                                              if ($("#" + this.id).val().length > 100) {
                                                  this.value = $("#" + this.id).val().substring(0, 100);
                                                  alert("Input Can Not Exceed 100 ");

                                              }
                 
                  

                   });


                   $("#" + this.id).change(function() {

                       if ($("#" + this.id).val().length > 100) {
                           this.value = $("#" + this.id).val().substring(0, 100);
                           alert("Input Can Not Exceed 100 ");

                       }



                   });


               })

           });
         </script>
     
     

    <style type="text/css">
        .style1
        {
            height: 16px;
        }
        .style2
        {
            height: 16px;
            width: 76px;
        }
        .style3
        {
            width: 76px;
        }
        .style4
        {
            width: 182px;
        }
        .style5
        {
            height: 16px;
            width: 182px;
        }
    </style>
    

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" >
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
            
        <Scripts>
                <asp:ScriptReference Path="http://colweb01/eta/scripts/jquery.min.js" />
            </Scripts>
    
    </asp:ScriptManager>
    
    <div>
    <TABLE cellSpacing=0 cellPadding=1 width="100%" border=0>
  <TR>
	<TH align=left width=300><FONT size=4>Special Design Parts 
      List</FONT>   </TH>
    <TD align=left><FONT color=green><STRONG>ADD MODE </STRONG></FONT></TD>
  </TR>
</TABLE>
<HR color=navy>    
<TABLE cellSpacing=0 cellPadding=1 width="100%" border=0>
  <TR>
    <TH width=150>Package Number</TH>
    <TD>
        <asp:Label ID="lblpackage" runat="server"></asp:Label>
      </TD></TR>
  <TR>
  <TD>&nbsp;</TD></TR>
</TABLE>

<FONT size=2>Ass'y#/Name | Key | Ass'y Code | Treatment | Part Code | Page Code</FONT>
    
    
    </div>
    <asp:DropDownList ID="ddlcitem" runat="server" Width="400px" 
        AutoPostBack="True" AppendDataBoundItems="True" DataTextField="TextValue" 
        DataValueField="CitemId" 
        onselectedindexchanged="ddlcitem_SelectedIndexChanged">
      <asp:ListItem  Value="0" >-Select Form C Item to attach to-</asp:ListItem>
    </asp:DropDownList>
    <BR>
    <asp:Label ID="lblerror" runat="server" ForeColor="Red" 
        Text="No Module Location in FormCitems table for this record" Visible="False" 
        Width="781px"></asp:Label>
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    
    <BR>
    <TABLE cellSpacing=0 cellPadding=1 width="100%" border=1>

	<TR>
	<TD bordercolor=blue colspan=9 >
	
     
         <asp:UpdatePanel ID="UpdatePanel1" runat="server"   UpdateMode="Conditional">
		   <ContentTemplate>
		<TABLE cellSpacing=0 cellPadding=1 width="100%" border=1>
		
			<TR>
			<TH bordercolor=blue align=left class="style4">
				<FONT size=2>Assembly Name/Code: <BR>
				
				</TH>
			<TH bordercolor=blue align=left width=200>
				<asp:TextBox ID="txtMajor" runat="server" 
                    style="WIDTH: 200px; font-size:xx-small;" Enabled="False" height="19px"  ></asp:TextBox>
                </TH>
			<TH bordercolor=blue align=left>
				<FONT size=1><asp:TextBox ID="txtAssyCode" runat="server" 
                    style="WIDTH: 200px; font-size:xx-small;" Enabled="False" height="19px" 
                   ></asp:TextBox>
				</FONT></TH>
			</TR>
			<TR>
			<TH bordercolor=blue align=left class="style4">
				<FONT size=2>Page Code 1 
				
				</FONT>&nbsp;</TH>
			<TH bordercolor=blue align=left>
				<FONT size=1><asp:TextBox ID="txtMinor" runat="server" 
                    style="WIDTH: 200px; font-size:xx-small;" height="19px"></asp:TextBox>
				</FONT></TH>
			<TH bordercolor=blue align=left>
				<FONT size=1>
                    <asp:TextBox ID="txtPageCode1" runat="server" 
                    style="WIDTH: 200px; font-size:xx-small;" height="19px" Enabled="False" ></asp:TextBox>
				</FONT></TH>
			</TR>
			<TR>
			<TH bordercolor=blue align=left class="style4">
				<FONT size=2>Page Code 2</FONT></TH>
			<TH bordercolor=blue align=left>
				<FONT size=1><asp:TextBox ID="txtName3" runat="server" 
                    style="WIDTH: 200px; font-size:xx-small;" height="19px"></asp:TextBox>
			   </FONT></TH>
			<TH bordercolor=blue align=left>
				<FONT size=1><asp:TextBox ID="txtPageCode2" runat="server" 
                    style="WIDTH: 200px; font-size:xx-small;" Enabled="False" height="19px" 
                    ></asp:TextBox>
				</FONT></TH>
			</TR>
			<TR>
			<TH bordercolor=blue align=left class="style4">
				<FONT size=2>Page:&nbsp;E1</FONT></TH>
			<TH bordercolor=blue align=left>
				<FONT size=1>
				   &nbsp;</FONT></TH>
			<TH bordercolor=blue align=left>
				<FONT size=1>
				
				 &nbsp;</FONT></TH>
			</TR>
	</TABLE>
		</ContentTemplate>
			 <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlcitem" EventName="SelectedIndexChanged" />
            </Triggers>
		  </asp:UpdatePanel>
		  
		     <asp:UpdatePanel ID="UpdatePanel2" runat="server" 
             UpdateMode="Conditional"  >
		   <ContentTemplate>
			<TABLE cellSpacing=0 cellPadding=1 width="100%" border=1>
			
			
			
			<TR>
			<TH bordercolor=blue class="style5"><FONT size=1></FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>LEVEL</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>PART NO.</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>PART NAME</FONT></TH>
		<TH bordercolor=blue class="style2"><FONT size=1>QTY</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>MATERIAL</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>SIZE</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>DWG</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>COMMENTS</FONT></TH>
		<TH bordercolor=blue class="style1"><FONT size=1>ECI</FONT></TH>
		</TR>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk1" runat="server" AutoPostBack="True" oncheckedchanged="chk1_CheckedChanged" 
                    />
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
                runat="server" EnableTheming="False" TextMode="MultiLine"  
               
                    ></asp:TextBox>
                     </FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk2" runat="server" AutoPostBack="True" oncheckedchanged="chk2_CheckedChanged1" 
                    />
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
            <asp:TextBox ID="txtpartno2" runat="server" ontextchanged="txtpartno2_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror2" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname2" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty2" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtqty2"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial2" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize2" runat="server" 
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
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk3" runat="server" AutoPostBack="True" oncheckedchanged="chk3_CheckedChanged1" 
                    />
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
            <asp:TextBox ID="txtpartno3" runat="server" ontextchanged="txtpartno3_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror3" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname3" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty3" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtqty3"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial3" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize3" runat="server" 
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
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk4" runat="server" oncheckedchanged="chk4_CheckedChanged" 
                    />
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
            <asp:TextBox ID="txtpartno4" runat="server" ontextchanged="txtpartno4_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror4" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname4" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty4" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtqty4"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial4" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize4" runat="server" 
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
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk5" runat="server" oncheckedchanged="chk5_CheckedChanged" 
                    />
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
            <asp:TextBox ID="txtpartno5" runat="server" ontextchanged="txtpartno5_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror5" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname5" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty5" 
                runat="server" Width="61px" AutoPostBack="True" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtqty5"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial5" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize5" runat="server" 
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
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk6" runat="server" oncheckedchanged="chk6_CheckedChanged" 
                    />
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
            <asp:TextBox ID="txtpartno6" runat="server" ontextchanged="txtpartno6_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror6" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname6" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty6" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtqty6"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial6" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize6" runat="server" 
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
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk7" runat="server" AutoPostBack="True" oncheckedchanged="chk7_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel7" runat="server" Width="50px">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno7" runat="server" ontextchanged="txtpartno7_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror7" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname7" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1 id="txtqty7">
            <asp:TextBox ID="txtqty7" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtqty7"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial7" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize7" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg7" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment7" 
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk8" runat="server" oncheckedchanged="chk8_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel8" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno8" runat="server" ontextchanged="txtpartno8_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror8" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname8" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty8" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtqty8"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial8" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize8" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg8" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment8" 
                runat="server" EnableTheming="False" TextMode="MultiLine" MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk9" runat="server" oncheckedchanged="chk9_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel9" runat="server" Width="50px">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno9" runat="server" ontextchanged="txtpartno9_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror9" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname9" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty9" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtqty9"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial9" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize9" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg9" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment9" 
                runat="server" EnableTheming="False" TextMode="MultiLine"  MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>

			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk10" runat="server" oncheckedchanged="chk10_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel10" runat="server" Width="50px">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno10" runat="server" ontextchanged="txtpartno10_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror10" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname10" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty10" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtqty10"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial10" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize10" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg10" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment10" 
                runat="server" EnableTheming="False" TextMode="MultiLine"  MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
	
			<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk11" runat="server" oncheckedchanged="chk11_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel11" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno11" runat="server" ontextchanged="txtpartno11_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror11" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname11" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty11" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtqty11"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial11" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize11" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg11" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment11" 
                runat="server" EnableTheming="False" TextMode="MultiLine"  MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk12" runat="server" oncheckedchanged="chk12_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel12" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno12" runat="server" ontextchanged="txtpartno12_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror12" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname12" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty12" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtqty12"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial12" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize12" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg12" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment12" 
                runat="server" EnableTheming="False" TextMode="MultiLine"  MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		<tr>
		<TD bordercolor=blue align=center class="style4">
				
				<FONT size=1>
                <asp:CheckBox ID="chk13" runat="server" oncheckedchanged="chk13_CheckedChanged" 
                    />
                </FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddllevel13" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">1</asp:ListItem>
                    <asp:ListItem Value="1">2</asp:ListItem>
                <asp:ListItem Value="2">3</asp:ListItem>
                  <asp:ListItem Value="3">4</asp:ListItem>
                  <asp:ListItem Value="4">5</asp:ListItem>
                   <asp:ListItem Value="5">6</asp:ListItem>
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartno13" runat="server" style="height: 22px" ontextchanged="txtpartno13_TextChanged" 
                    ></asp:TextBox>

<asp:Label ID="lblpartlisterror13" runat="server" ForeColor="Red" Visible="False" ></asp:Label>

            </FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtpartname13" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center class="style3"><FONT size=1>
            <asp:TextBox ID="txtqty13" 
                runat="server" Width="61px" height="22px" 
                    ></asp:TextBox><asp:RegularExpressionValidator
                    ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtqty13"
                    ErrorMessage="Quantity Error" ValidationExpression="^\d*$|[Xx][Xx][Xx]$">*</asp:RegularExpressionValidator></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtmaterial13" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtsize13" runat="server" 
                    ></asp:TextBox></FONT></TD>
		<TD bordercolor=blue align=center>
            <asp:DropDownList ID="ddldwg13" runat="server" Width="50px" Enabled="False">
                <asp:ListItem Value="0">A</asp:ListItem>
                    <asp:ListItem Value="1">N</asp:ListItem>
                <asp:ListItem Value="2">S</asp:ListItem>
                  <asp:ListItem Value="3">K</asp:ListItem>
                  <asp:ListItem Value="4">C</asp:ListItem>
                  
            </asp:DropDownList>
            </TD>
		<TD bordercolor=blue align=center><FONT size=1>
            <asp:TextBox ID="txtcomment13" 
                runat="server" EnableTheming="False" TextMode="MultiLine"  MaxLength="99" 
                    ></asp:TextBox></FONT></TD>
		</tr>
		</TABLE>
		</ContentTemplate>
		</asp:UpdatePanel>
		
	
	</TD>
	</TR>
	
	
	</TABLE>
	
	
	<br />
	
	
	
	<br />
	<asp:Button ID="btnsumbit" runat="server" Text="Submit" 
        onclick="btnsumbit_Click" Width="165px"  />
		
    </form>
</body>
</html>
