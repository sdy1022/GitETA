<%@ Language=VBScript %>
<html>
<head>
<meta NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
<style>
@media print { body {visibility: hidden;}}
</style>
</head>
<%
Dim cn
Dim rs
Dim sSql
Dim Package

Package=Request.QueryString("Package")
'for shortcut for PC  CDW 10-15-07
SzForm = Request.Form("txtPackage")
If Len(szForm) > 0 Then
	Package = CStr(szForm)
	SzPrint = Request.Form("Print")
End if

Set cn=server.CreateObject("adodb.connection")
cn.Open Application("cnn")
'DSN=ETA;UID=eta;PASSWORD=ETA;
'Response.Write(Application("cnn")&"   sdy:"&SzForm)

'**Check for released status
sSql="SELECT EditLock,ReleaseDate FROM eta.dbo.PackageLog " & _
		"WHERE DesignNumber='" & Package & "'"
Set rsCheck=cn.Execute(sSql)

If rsCheck(0)=true THEN
	EditLock="on"
Else	
	EditLock=""
End If

If isDate(rsCheck(1)) THEN
	Released="on"
Else	
	Released=""
End If

rsCheck.Close
set rsCheck=nothing

'**Get Header data
sSql="SELECT Mast,Attachment,EciDescription,EmpId,AttDescription " & _
		"FROM eta.dbo.viewHeaderInfo " & _
		"WHERE DesignNumber='" & Package & "'"
Set rsHeader=cn.Execute(sSql)

If rsHeader.eof THEN
	rsHeader.Close
	set rsHeader=nothing
	Response.Redirect("http://colweb01/eta/NoPackage.asp")
End If

Mast=rsHeader("Mast")
Att=rsHeader("Attachment")
ECI=rsHeader("EciDescription")
AttDescrip=rsHeader("AttDescription")

'**Get FormA Items data
sSql="SELECT AItemId,[key],Major,Minor,Comment,ModuleNumber " & _
		"FROM Eta.dbo.FormAItems " & _
		"WHERE DesignNumber='" & Package & _
		"' ORDER BY [key]"		
Set rsItems=cn.Execute(sSql)

'**Get Load capacity information
sSql="SELECT * FROM Eta.dbo.LoadCapacities WHERE DesignNumber='" & Package & "'"
Set rsCap=cn.Execute(sSql)

If rsCap.bof and rsCap.eof THEN
	Ld1a=""
	Ld1b=""
	Ld1c=""
	Ld1Cap=""
	Ld1amm=""
	Ld1bmm=""
	Ld1cmm=""
	Ld1Capmm=""
	Ld2a=""
	Ld2b=""
	Ld2c=""
	Ld2Cap=""
	Ld2amm=""
	Ld2bmm=""
	Ld2cmm=""
	Ld2Capmm=""
	oldCenter=""
	oldCapacity=""
	vMast=""
	vBkTilt=""
	vAttach=""
	vType=""
	vVoltage=""
	vMaxAH=""
	vFntTreadIN=""
	vFntTireDim=""
	vFntTreadMM=""
	vRearTireDim=""
	vTruckWeight=""
	vTruckWeightkg=""
	vMinBatWt=""
	vMaxBatWt=""
	vMinBatWtkg=""
	vMaxBatWtkg=""
Else
	Ld1a=rsCap("LdCtr1SaeA")
	Ld1b=rsCap("LdCtr1SaeB")
	Ld1c=rsCap("LdCtr1SaeC")
	Ld1Cap=rsCap("LdCtr1SaeCap")
	Ld1amm=rsCap("LdCtr1MksA")
	Ld1bmm=rsCap("LdCtr1MksB")
	Ld1cmm=rsCap("LdCtr1MksC")
	Ld1Capmm=rsCap("LdCtr1MksCap")
	Ld2a=rsCap("LdCtr2SaeA")
	Ld2b=rsCap("LdCtr2SaeB")
	Ld2c=rsCap("LdCtr2SaeC")
	Ld2Cap=rsCap("LdCtr2SaeCap")
	Ld2amm=rsCap("LdCtr2MksA")
	Ld2bmm=rsCap("LdCtr2MksB")
	Ld2cmm=rsCap("LdCtr2MksC")
	Ld2Capmm=rsCap("LdCtr2MksCap")
	oldCenter=rsCap("oldCenter")
	oldCapacity=rsCap("oldCapacity")
	vMast=rsCap("Mast")
	vBkTilt=rsCap("BkTilt")
	vAttach=rsCap("Attach")
	vType=rsCap("Type")
	vVoltage=rsCap("Voltage")
	vMaxAH=rsCap("MaxAH")
	vFntTreadIN=rsCap("FntTreadIN")
	vFntTireDim=rsCap("FntTireDim")
	vFntTreadMM=rsCap("FntTreadMM")
	vRearTireDim=rsCap("RearTireDim")
	vTruckWeight=rsCap("TruckWeight")
	vTruckWeightkg=rsCap("TruckWeightkg")
	vMinBatWt=rsCap("MinBatWt")
	vMaxBatWt=rsCap("MaxBatWt")
	vMinBatWtkg=rsCap("MinBatWtkg")
	vMaxBatWtkg=rsCap("MaxBatWtkg")
End If

rsCap.Close
set rsCap=nothing

'**Get Order Numbers
sSql="SELECT OrderNumber FROM Eta.dbo.OrderNumbers WHERE DesignNumber='" & Package & "'"
set rsOrdNum=cn.Execute(sSql)

'**Get Dates from PackageLog
sSql="SELECT LineOnDate,IssueDate,TsdrNumber,Model,TsItem " & _
		"FROM eta.dbo.PackageLog WHERE DesignNumber='" & Package & "'"
set rsDates=cn.Execute (sSql)

Tsdr=rsDates("TsdrNumber")
Model=rsDates("Model")
TsItems=rsDates("TsItem")

'**Get Customer and Dealer info from Tsdr Database
sSql="SELECT [Customer Name], [Dealer Name],[Units] FROM [colsql01].Tsdr_Data.dbo.[Tsdr Header] " & _
		"WHERE  [Tsdr Number]='" & Tsdr & "'"

set rsCustomer=cn.Execute(sSql)

If rsCustomer.bof AND rsCustomer.eof THEN
	Customer=""
	Dealer=""
	Units=""
Else
	Customer=rsCustomer(0)
	Dealer=rsCustomer(1)
	Units=rsCustomer(2)
End If

rsCustomer.Close
set rsCustomer=nothing

'**Get Engineer Name
sSql="SELECT EmpLastName,EmpFirstName " & _
		"FROM EmployeeData.dbo.Employees WHERE EmpId =" & rsHeader("EmpId")
Set rsName=cn.Execute(sSql)

Engineer=rsName(0) & ", " & rsName(1)

rsName.Close
set rsName=nothing

rsHeader.Close
set rsHeader=nothing

'**Check for ECR status
Ecr=""

sSql="SELECT EcrId,Status FROM Eci.dbo.Ecr " & _
		"WHERE Status <>'Rejected' AND DesignNumber='" & Package & "' ORDER BY EcrId DESC" 
set rsEcr=cn.Execute(sSql)

Pending=""
Rejected=""
Accepted=""

if not rsEcr.eof Then
'	Do Until rsEcr.eof
		if rsEcr(1)="Pending" THEN Pending="true"
		if rsEcr(1)="Rejected" THEN Rejected="true"
		if rsEcr(1)="Accepted" THEN Accepted="true"
'		rsEcr.MoveNext
'	Loop

sStatus= rsEcr(1)
End If

rsEcr.Close
set rsEcr=nothing

%>
<body<%
	'**Get info from ECI view to find active status
	sSql="SELECT * FROM eci.dbo.viewEciHistory WHERE DesignNumber='" & _
			Package & "' AND EciFrom IS NULL ORDER BY EcrId DESC"
	set rsEciHistory=cn.Execute (sSql)
	
	If not rsEciHistory.EOF THEN
		If isNull(rsEciHistory("EciFrom")) THEN
			bActive=true
		Else
			bActive=false
		End If
	End If
	
	rsEciHistory.Close
	set rsEciHistory=nothing
	
	If Pending="true" then Background= " background='../../images/EciPending3.png' " & _
		"style='background-repeat:no-repeat;background-position:center center' " & _
		"bgproperties=fixed"
	If Accepted="true" and bActive then Background= " background='../../images/EciActive.gif'" & _
		"style='background-repeat:no-repeat;background-position:center center' " & _
		"bgproperties=fixed"
		
	Response.Write Background
	%>><form>
<table Name="Header" cellSpacing="0" cellPadding="1" width="100%" border="0" style="WIDTH: 100%">
  
  <tr>
    <th style="WIDTH: 200px" width="200" align="left"><font size="4">Special Design Number</font> </th>
	<td>
		<input name="txtDesignNumber" style="BORDER-STYLE: none;FONT-WEIGHT:bold;" 
			value="<%=Package%>" readOnly>  &nbsp;&nbsp;&nbsp;

<%If szPrint = "yes" Then%>

		
		<input type="button" value="Print" onClick="window.print()">
		
		</td>
	
	
<%Else%>
			<FONT><strong>VIEW MODE</strong></FONT>&nbsp;&nbsp;&nbsp;&nbsp;
			<%
			If Released="on" AND EditLock="" THEN Response.Write "<IMG SRC='../../images/docbag1.gif'>"
			If EditLock="on" THEN Response.Write "<IMG SRC='../../images/padlock.gif'>"
			%> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="http://colweb01/eta/ECR/RelEcr.asp?Package=<% =Package %>">Relavent ECRs</a>
	</td>
<%End if%>
<TD><B>Configurator:
<%
'**Get Configurator Data
sSqlCF="SELECT TID FROM eta.dbo.Configurator_Transfer WHERE TSDNumber='" & Package & "'"
Set rsCF=cn.Execute(sSqlCF)
If rsCF.eof Or rsCF.bof Then
	Response.Write("NO")
Else 
	Response.Write("Yes")
End if
%>
</B></TD>
      </tr>
</table>
</form>
<hr color="navy">
<table Name="Header1" cellSpacing="0" cellPadding="1" width="100%" border="0">
  
  <tr>
    <td colspan="4"><strong>Truck Model Information
    <%
    If Ecr="Pending" THEN
		Response.Write "<BR><marquee direction=right><FONT color=red>ECR/ECI Pending on this Package</FONT></marquee>"
	End If%>  </strong></td>
    </tr>
  <tr>
</table>
<table WIDTH="100%" BORDER="1" CELLSPACING="0" CELLPADDING="1">
	<TR>
		<TD align="left" border="0" Width="60"><STRONG>MAST</STRONG></TD>
		<TD align="center" Width="60"><%=vMast%></TD>
		<TD align="left" Width="130"><STRONG>BACK TILT</STRONG></TD>
		<TD align="center" Width="50"><%=vBkTilt%></TD>
		<TD align="right" Width="80"><STRONG>ATTACH</STRONG></TD>
		<TD align="center"><%=vAttach%></TD>
	</TR>
</table>
<table WIDTH="100%" BORDER="1" CELLSPACING="0" CELLPADDING="1">
	<TR>
		<TD align="left" border="0" Width="60"><STRONG>TYPE</STRONG></TD>
		<TD align="center" Width="60"><%=vType%></TD>
		<TD align="left" border="0" Width="90"><STRONG>VOLTAGE</STRONG></TD>
		<TD align="right" Width="60"><%=vVoltage%>&nbsp;&nbsp;V</TD>
		<TD align="right" border="0" Width="250"><STRONG>BATTERY MAX AH</STRONG></TD>
		<TD align="right"><%=vMaxAH%>&nbsp;&nbsp;AH</TD>
	</TR>
</table>
<table WIDTH="100%" BORDER="1" CELLSPACING="0" CELLPADDING="1">
	<tr>
		<td align="left" Rowspan="2" border="0" Width="70"><STRONG>FRONT<BR>TREAD</STRONG></TD>
		<TD align="right" Width="100"><%=vFntTreadIN%>&nbsp;&nbsp;in</TD>
		<TD align="right" Width="100"><B>TIRE&nbsp;&nbsp;&nbsp;&nbsp;FR</B></TD>
		<TD align="center"><%=vFntTireDim%></TD>
	</TR>
	<tr>
		<TD align="right" ><%=vFntTreadMM%>&nbsp;&nbsp;mm</TD>
		<TD align="right"><B>SIZE&nbsp;&nbsp;&nbsp;&nbsp;RR</B></TD>
		<TD align="center"><%=vRearTireDim%></TD>
	</TR>
</table>
<table WIDTH="100%" BORDER="1" CELLSPACING="0" CELLPADDING="1">
	<tr>
		<td align="LEFT" Rowspan="2" border="0" Width="250"><STRONG>TRUCK WEIGHT<BR>W/O BATTERY</STRONG></TD>
		<TD align="right" Width="90"><%=vTruckWeight%>&nbsp;&nbsp;lb</TD>
		<TD align="center"><B>BATTERY WEIGHT</B></TD>
		<TD align="right" Width="100"><%=vMinBatWt%>&nbsp;&nbsp;lb/</TD>
		<TD align="right" Width="100"><%=vMaxBatWt%>&nbsp;&nbsp;lb</TD>
	</TR>
	<tr>
		<TD align="right" ><%=vTruckWeightkg%>&nbsp;&nbsp;kg</TD>
		<TD align="center"><B>MIN./MAX.</B></TD>
		<TD align="right" Width="100"><%=vMinBatWtkg%>&nbsp;&nbsp;kg/</TD>
		<TD align="right" Width="100"><%=vMaxBatWtkg%>&nbsp;&nbsp;kg</TD>
	</TR>
</table>
<table WIDTH="100%" BORDER="1" CELLSPACING="0" CELLPADDING="1">
	<tr>
		<td align="middle" rowspan="5" width="200">
			<img SRC="../../images/LoadDiagram.gif" WIDTH="175" HEIGHT="101">
			<%
			sSql="SELECT Rev FROM Eta.dbo.ChangeLogLoad WHERE DesignNumber='" & _ 
					Package & "' ORDER BY Rev"
			set rsLoadRev=cn.Execute(sSql)
			
			IF rsLoadRev.eof=false THEN
				sLoadRev="<BR>Revisons:&nbsp;" & _
					"<IMG SRC='../../images/Rev" & trim(rsLoadRev(0)) & ".gif'>"
				rsLoadRev.MoveNext
				
				Do Until rsLoadRev.eof
					sLoadRev=sLoadRev & "<IMG SRC='../../images/Rev" & trim(rsLoadRev(0)) & ".gif'>"
					rsLoadRev.MoveNext
				Loop
				
				Response.Write sLoadRev
			End If
			
			rsLoadRev.Close
			set rsLoadRev=nothing
			%></td>
		<th style="border-bottom-color:blue;">A</th>
		<th style="border-bottom-color:blue;">B</th>
		<th style="border-bottom-color:blue;">C</th>
		<th style="border-bottom-color:blue;">CAPACITY</th>
	</tr>
	<tr>
		<td align="right"><%=Ld1a%>&nbsp;&nbsp;in</td>
		<td align="right"><%=Ld1b%>&nbsp;&nbsp;in</td>
		<td align="right"><%=Ld1c%>&nbsp;&nbsp;in</td>
		<td align="right"><%=Ld1Cap%>&nbsp;&nbsp;lb</td>
	</tr>
	<tr>
		<td align="right" style="border-bottom-color:blue;"><%=Ld1amm%>&nbsp;&nbsp;mm</td>
		<td align="right" style="border-bottom-color:blue;"><%=Ld1bmm%>&nbsp;&nbsp;mm</td>
		<td align="right" style="border-bottom-color:blue;"><%=Ld1cmm%>&nbsp;&nbsp;mm</td>
		<td align="right" style="border-bottom-color:blue;"><%=Ld1Capmm%>&nbsp;&nbsp;kg</td>
	</tr>
	<tr>
		<td align="right"><%=Ld2a%>&nbsp;&nbsp;in</td>
		<td align="right"><%=Ld2b%>&nbsp;&nbsp;in</td>
		<td align="right"><%=Ld2c%>&nbsp;&nbsp;in</td>
		<td align="right"><%=Ld2Cap%>&nbsp;&nbsp;lb</td>
	</tr>
	<tr>
		<td align="right"><%=Ld2amm%>&nbsp;&nbsp;mm</td>
		<td align="right"><%=Ld2bmm%>&nbsp;&nbsp;mm</td>
		<td align="right"><%=Ld2cmm%>&nbsp;&nbsp;mm</td>
		<td align="right"><%=Ld2Capmm%>&nbsp;&nbsp;kg</td>
	</tr>
</table>
<BR>

<TABLE width=50% BORDER="1" CELLSPACING="0" CELLPADDING="1">
<TR>
	<th style="border-bottom-color:blue;"><FONT size=2)>ECI Mark</FONT></th>
	<th style="border-bottom-color:blue;"><FONT size=2)>Revision</FONT></th>
	<th style="border-bottom-color:blue;"><FONT size=2)>ECI #</FONT></th>
	<th style="border-bottom-color:blue;"><FONT size=2)>Date</FONT></th>
	<th style="border-bottom-color:blue;"><FONT size=2)>Revised By</FONT></th>
</tr>
<%
'**Get ECI header info
sSql="SELECT Rev,RevComment,a.EciNumber,CommentDate,RevInitials " & _
		"FROM Eta.dbo.EciHeadA as a " & _
		"JOIN Eci.dbo.Eci as e ON a.EciNumber=e.EciNumber " & _
		"WHERE a.DesignNumber='" & Package & "' ORDER BY Rev"
set rsEciHead=cn.Execute(sSql)

Do Until rsEciHead.eof
	'**Write rows of data
		with Response
			.Write("<TR align=middle>" & chr(13))
			.Write("<TD><IMG SRC='../../images/Rev" & trim(rsEciHead("Rev")) & ".gif'></TD>" & chr(13))
			.Write("<TD><FONT size=2>" & rsEciHead("RevComment") & "&nbsp;</FONT></TD>" & chr(13))
			.Write("<TD><FONT size=2><A href='http://colweb01/eta/ECI/Eci.asp?Eci=" & _
					 rsEciHead("EciNumber") & "' target='_blank'>" & rsEciHead("EciNumber") & "</A></FONT></TD>" & chr(13))
			.Write("<TD><FONT size=2>" & rsEciHead("CommentDate") & "&nbsp;</FONT></TD>" & chr(13))
			.Write("<TD><FONT size=2>" & rsEciHead("RevInitials") & "&nbsp;</FONT></TD>" & chr(13))
			.Write("</TR>" & chr(13))
	    end with
	
	rsEciHead.MoveNext
Loop

rsEciHead.Close
set rsEciHead=nothing
%>
</TABLE>    
    
<table name="Header2" cellSpacing="0" cellPadding="1" width="100%" border="0" style="WIDTH: 100%" background>
  <tr>
    <td style="WIDTH: 200px" width="200">&nbsp;</td>
    <td></td>
    <td style="WIDTH: 150px" width="150"></td>
    <td style="WIDTH: 200px" width="200"></td></tr>
  <tr>
    <td align="right">Customer:&nbsp;</td>
    <td><input id="text1" name="text1" style="WIDTH: 150px; border-style:none" width="150" value="<%=Customer%>" readonly></td>
    
    <td align="right">Dealer:&nbsp;</td>
    <td><input id="text2" name="text2" style="WIDTH: 150px; border-style:none" value="<%=Dealer%>" readonly></td></tr>
  <tr>
    <td align="right" valign="top">Order Numbers:&nbsp;<BR>
		<FONT size=2 color=blue>(Click to view order info)</FONT></td>
    <td><select id="select1" size="3" name="selOrderNumbers" 
			style="WIDTH: 150px; HEIGHT: 60px" onclick="getInfo()" readonly> 
        <%
        Do Until rsOrdNum.eof
			Response.Write("<OPTION value='" & rsOrdNum("OrderNumber") & "'>" & rsOrdNum("OrderNumber") & "</OPTION>" & chr(13))
			rsOrdNum.moveNext
		Loop
		rsOrdNum.Close
		set rsOrdNum=nothing
        %>
        </select></td>
    <td align=right>Model:&nbsp;<BR>TS Items:&nbsp;</td>
    <td>
		<INPUT type="text" name=txtModel value="<%=Model%>" style="border-style:none" readonly><BR>
		<INPUT type="text" name=txtTsItems value="<%=TsItems%>" style="border-style:none" readonly></td>
  </tr>
  <tr>
    <td align="right">Total Number of Units:&nbsp;</td>
    <td><input id="text3" name="text3" style="WIDTH: 150px; border-style:none" 
			width="150" value="<%=Units%>" readonly></td>
		
    <td align="right">Line-On Date:&nbsp;</td>
    <td><input id="text5" name="text5" style="WIDTH: 150px; border-style:none" 
			value="<%=rsDates(0)%>" readonly></td></tr>
  <tr>
    <td align="right">TSDR Number:&nbsp;</td>
    <td><input id="text4" name="text4" style="WIDTH: 150px; border-style:none" 
			value="<%=Tsdr%>" readonly></td>
      
    <td align="right">Package Due Date:&nbsp;</td>
    <td><input id="text6" name="text6" style="WIDTH: 150px; border-style:none" 
			value="<%=rsDates(1)%>" readonly></td></tr>
  <tr>
    <td align="right">Engineer:&nbsp;<BR>
			ECI Date:&nbsp;</td>
    <td><%=Engineer%><BR>
			<Input id="text8" name="text8" style="WIDTH: 150px; border-style:none" readonly></td>
    <td align=right>Mast:&nbsp;<BR>
			Attachment:&nbsp;</td>
    <td><input name="txtMast" style="WIDTH: 150px; border-style:none;" 
			value="<%=Mast%>" readonly><BR>
			<input name="txtAtt" style="WIDTH: 150px; border-style:none;" 
			value="<%=Att%>" readonly>
	</td></tr>
  <tr>
    <td align="right">ECI Description:&nbsp;</td>
    <td><Textarea name="txtaEci" style="WIDTH: 150px;" readonly><%=ECI%></Textarea></td>
    <td align=right>Att. Description:&nbsp; </td>
    <td><Textarea name="txtaAttDescription" style="WIDTH: 150px;" readonly><%=AttDescrip%></Textarea></td>
  </tr>    
  <tr>
    <td>&nbsp;</td>
    <td></td>
    <td></td>
    <td></td></tr></table>
<table Name="tblFormAItems" cellSpacing="0" cellPadding="1" width="100%" border="1" style="WIDTH: 100%" background>
  
  <tr>
    <th rowSpan="2" style="WIDTH: 35px" width="35" bordercolor="Blue">Rev.</th>
    <th rowSpan="2" style="WIDTH: 35px" width="35" bordercolor="Blue">Key No.</th>
    <th colspan="2" bordercolor="Blue">Classification</th>
    <th rowSpan="2" bordercolor="Blue">Comments</th>
    <TH rowSpan=2 bordercolor=Blue>Module</TH></tr>
  <tr>
    <th bordercolor="Blue">Major</th>
    <th bordercolor="Blue">Minor</th>
  </tr>
<%
rsDates.Close
set rsDates=nothing

'**************Populate table with data for FormAItems*******************************************

If rsItems.bof=false and rsItems.eof=false then
	'**If recordset is empty, skip this step
	'**Create table for viewing data only - no editing possible
	Do until rsItems.eof
		'**Lookup Revisions for this Item and create string of images
		sSql="SELECT Rev FROM Eta.dbo.ChangeLogA WHERE AitemId=" & rsItems("AitemId")
		set rsRev=cn.Execute(sSql)
		
		If not rsRev.eof THEN
			sRev="<IMG SRC='../../images/Rev" & trim(rsRev(0)) & ".gif'>"
			rsRev.MoveNext
			Do Until rsRev.eof
				sRev=sRev & "<IMG SRC='../../images/Rev" & trim(rsRev(0)) & ".gif'>"
				rsRev.MoveNext
			Loop
		Else
			sRev=""
		End If
		rsRev.Close
		set rsRev=nothing
		
		with Response
			.Write("<TR align=middle>" & chr(13))
			.Write("<TD>" & sRev & "&nbsp;</TD>" & chr(13))
			.Write("<TD>" & rsItems("key") & "&nbsp;</TD>" & chr(13))
			.Write("<TD>" & rsItems("Major") & "&nbsp;</TD>" & chr(13))
			.Write("<TD>" & rsItems("Minor") & "&nbsp;</TD>" & chr(13))
			.Write("<TD>" & rsItems("Comment") & "&nbsp;</TD>" & chr(13))
	    end with
			'**Added to allow user to view originating module when it is not native
			'  to the current package.  KDL 11-15-02
			If left(rsItems("ModuleNumber"),4)=Package THEN
				Response.Write("<TD>" & rsItems("ModuleNumber") & "&nbsp;</TD>" & chr(13))
			Else
				Response.Write("<TD><A href='http://colweb01/eta/EngineeringTools/" & _
					"EngineeringToolbox.asp?Package=" & left(rsItems("ModuleNumber"),4) & _
					"' target='_blank'>" & rsItems("ModuleNumber") & "&nbsp;</A></TD>" & chr(13))
			End If
			Response.Write("</TR>" & chr(13))
	    rsItems.MoveNext
	Loop
End If

rsItems.Close
set rsItems=nothing


cn.Close
SET cn=nothing
%>
</table>
<BR>
<BR>
<script language=jscript> 
//Added to allow users fast access to order info.  kdl 1-7-03
function getInfo()
	{
	target="http://colweb01/eta/OrderInfo/InfoSheet.asp?OrdNum="+select1.value;
	var win = window.open('','OrderInfo','height=500,width=800,resizable=yes,scrollbars=yes');
	win.document.write('Loading from AS400...');
	win.document.location=target;
	}
</script>

</body>
</html>
