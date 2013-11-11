<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormAData.aspx.cs" Inherits="EngineeringTools_FormA_FormAData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FormA Data</title>
    <style type="text/css">
        @media print
        {
            body
            {
                visibility: hidden;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table name="Header" cellspacing="0" cellpadding="1" width="100%" border="0" style="width: 100%">
        <tr>
            <th style="width: 200px" width="200" align="left">
                <font size="4">Special Design Number</font>
            </th>
            <td>
                <input name="txtDesignNumber" style="border-style: none; font-weight: bold;" value="Package"
                    readonly>
                &nbsp;&nbsp;&nbsp;
            </td>
            <td>
                <font><strong>VIEW MODE</strong></font>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Image ID="Image1" runat="server" />
                <asp:Image ID="Image2" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="http://colweb01/eta/ECR/RelEcr.asp?Package=">
                    Relavent ECRs</a>
            </td>
            <td>
                <b>Configurator: </b>
            </td>
        </tr>
    </table>
    <hr color="navy">
    <table name="Header1" cellspacing="0" cellpadding="1" width="100%" border="0">
        <tr>
            <td colspan="4">
                <strong>Truck Model Information
                    <br>
                  <%--  <marquee direction="right"><FONT color=red>ECR/ECI Pending on this Package</FONT></marquee>--%>
                </strong>
            </td>
        </tr>
        <tr>
    </table>
    <table width="100%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <td align="left" border="0" width="60">
                <strong>MAST</strong>
            </td>
            <td align="center" width="60">
                vMast
            </td>
            <td align="left" width="130">
                <strong>BACK TILT</strong>
            </td>
            <td align="center" width="50">
                vBkTilt
            </td>
            <td align="right" width="80">
                <strong>ATTACH</strong>
            </td>
            <td align="center">
                vAttach
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <td align="left" border="0" width="60">
                <strong>TYPE</strong>
            </td>
            <td align="center" width="60">
                vType
            </td>
            <td align="left" border="0" width="90">
                <strong>VOLTAGE</strong>
            </td>
            <td align="right" width="60">
                vVoltage&nbsp;&nbsp;V
            </td>
            <td align="right" border="0" width="250">
                <strong>BATTERY MAX AH</strong>
            </td>
            <td align="right">
                vMaxAH&nbsp;&nbsp;AH
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <td align="left" rowspan="2" border="0" width="70">
                <strong>FRONT<br>
                    TREAD</strong>
            </td>
            <td align="right" width="100">
                vFntTreadIN&nbsp;&nbsp;in
            </td>
            <td align="right" width="100">
                <b>TIRE&nbsp;&nbsp;&nbsp;&nbsp;FR</b>
            </td>
            <td align="center">
                vFntTireDim
            </td>
        </tr>
        <tr>
            <td align="right">
                vFntTreadMM&nbsp;&nbsp;mm
            </td>
            <td align="right">
                <b>SIZE&nbsp;&nbsp;&nbsp;&nbsp;RR</b>
            </td>
            <td align="center">
                vRearTireDim
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <td align="LEFT" rowspan="2" border="0" width="250">
                <strong>TRUCK WEIGHT<br>
                    W/O BATTERY</strong>
            </td>
            <td align="right" width="90">
                vTruckWeight&nbsp;&nbsp;lb
            </td>
            <td align="center">
                <b>BATTERY WEIGHT</b>
            </td>
            <td align="right" width="100">
                vMinBatWt&nbsp;&nbsp;lb/
            </td>
            <td align="right" width="100">
                vMaxBatWt&nbsp;&nbsp;lb
            </td>
        </tr>
        <tr>
            <td align="right">
                vTruckWeightkg&nbsp;&nbsp;kg
            </td>
            <td align="center">
                <b>MIN./MAX.</b>
            </td>
            <td align="right" width="100">
                vMinBatWtkg&nbsp;&nbsp;kg/
            </td>
            <td align="right" width="100">
                vMaxBatWtkg&nbsp;&nbsp;kg
            </td>
        </tr>
    </table>
    <table width="100%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <td align="middle" rowspan="5" width="200">
                <img src="../../images/LoadDiagram.gif" width="175" height="101">
               
            </td>
            <th style="border-bottom-color: blue;">
                A
            </th>
            <th style="border-bottom-color: blue;">
                B
            </th>
            <th style="border-bottom-color: blue;">
                C
            </th>
            <th style="border-bottom-color: blue;">
                CAPACITY
            </th>
        </tr>
        <tr>
            <td align="right">
                Ld1a&nbsp;&nbsp;in
            </td>
            <td align="right">
                Ld1b&nbsp;&nbsp;in
            </td>
            <td align="right">
                Ld1c&nbsp;&nbsp;in
            </td>
            <td align="right">
              Ld1Cap&nbsp;&nbsp;lb
            </td>
        </tr>
        <tr>
            <td align="right" style="border-bottom-color: blue;">
                Ld1amm&nbsp;&nbsp;mm
            </td>
            <td align="right" style="border-bottom-color: blue;">
                Ld1bmm&nbsp;&nbsp;mm
            </td>
            <td align="right" style="border-bottom-color: blue;">
                Ld1cmm&nbsp;&nbsp;mm
            </td>
            <td align="right" style="border-bottom-color: blue;">
                Ld1Capmm&nbsp;&nbsp;kg
            </td>
        </tr>
        <tr>
            <td align="right">
                Ld2a&nbsp;&nbsp;in
            </td>
            <td align="right">
                Ld2b&nbsp;&nbsp;in
            </td>
            <td align="right">
                Ld2c&nbsp;&nbsp;in
            </td>
            <td align="right">
                Ld2Cap&nbsp;&nbsp;lb
            </td>
        </tr>
        <tr>
            <td align="right">
                Ld2amm&nbsp;&nbsp;mm
            </td>
            <td align="right">
                Ld2bmm&nbsp;&nbsp;mm
            </td>
            <td align="right">
                Ld2cmm&nbsp;&nbsp;mm
            </td>
            <td align="right">
                Ld2Capmm&nbsp;&nbsp;kg
            </td>
        </tr>
    </table>
    <br>
    <table width="50%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <th style="border-bottom-color: blue;">
                <font size="2)">ECI Mark</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">Revision</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">ECI #</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">Date</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">Revised By</font>
            </th>
        </tr>
       
    </table>
    <table width="50%" border="1" cellspacing="0" cellpadding="1">
        <tr>
            <th style="border-bottom-color: blue;">
                <font size="2)">ECI Mark</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">Revision</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">ECI #</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">Date</font>
            </th>
            <th style="border-bottom-color: blue;">
                <font size="2)">Revised By</font>
            </th>
        </tr>
        
    </table>
    <table name="Header2" cellspacing="0" cellpadding="1" width="100%" border="0" style="width: 100%"
        background>
        <tr>
            <td style="width: 200px" width="200">
                &nbsp;
            </td>
            <td>
            </td>
            <td style="width: 150px" width="150">
            </td>
            <td style="width: 200px" width="200">
            </td>
        </tr>
        <tr>
            <td align="right">
                Customer:&nbsp;
            </td>
            <td>
                <input id="text1" name="text1" style="width: 150px; border-style: none" width="150"
                    value="Customer" readonly>
            </td>
            <td align="right">
                Dealer:&nbsp;
            </td>
            <td>
                <input id="text2" name="text2" style="width: 150px; border-style: none" value="Dealer"
                    readonly>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">
                Order Numbers:&nbsp;<br>
                <font size="2" color="blue">(Click to view order info)</font>
            </td>
            <td>
                <select id="select1" size="3" name="selOrderNumbers" style="width: 150px; height: 60px"
                    onclick="getInfo()" readonly>
                    
                </select>
            </td>
            <td align="right">
                Model:&nbsp;<br>
                TS Items:&nbsp;
            </td>
            <td>
                <input type="text" name="txtModel" value="Model" style="border-style: none"
                    readonly><br>
                <input type="text" name="txtTsItems" value="TsItems" style="border-style: none"
                    readonly>
            </td>
        </tr>
        <tr>
            <td align="right">
                Total Number of Units:&nbsp;
            </td>
            <td>
                <input id="text3" name="text3" style="width: 150px; border-style: none" width="150"
                    value="Units" readonly>
            </td>
            <td align="right">
                Line-On Date:&nbsp;
            </td>
            <td>
                <input id="text5" name="text5" style="width: 150px; border-style: none" value="rsDates(0)"
                    readonly>
            </td>
        </tr>
        <tr>
            <td align="right">
                TSDR Number:&nbsp;
            </td>
            <td>
                <input id="text4" name="text4" style="width: 150px; border-style: none" value="Tsdr"
                    readonly>
            </td>
            <td align="right">
                Package Due Date:&nbsp;
            </td>
            <td>
                <input id="text6" name="text6" style="width: 150px; border-style: none" value="rsDates>"
                    readonly>
            </td>
        </tr>
        <tr>
            <td align="right">
                Engineer:&nbsp;<br>
                ECI Date:&nbsp;
            </td>
            <td>
                Engineer<br>
                <input id="text8" name="text8" style="width: 150px; border-style: none" readonly>
            </td>
            <td align="right">
                Mast:&nbsp;<br>
                Attachment:&nbsp;
            </td>
            <td>
                <input name="txtMast" style="width: 150px; border-style: none;" value="Mast"
                    readonly><br>
                <input name="txtAtt" style="width: 150px; border-style: none;" value="Att" readonly>
            </td>
        </tr>
        <tr>
            <td align="right">
                ECI Description:&nbsp;
            </td>
            <td>
                <textarea name="txtaEci" style="width: 150px;" readonly>ECI</textarea>
            </td>
            <td align="right">
                Att. Description:&nbsp;
            </td>
            <td>
                <textarea name="txtaAttDescription" style="width: 150px;" readonly>AttDescrip></textarea>
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
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
