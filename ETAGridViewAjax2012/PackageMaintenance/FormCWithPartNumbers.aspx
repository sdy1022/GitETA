﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormCWithPartNumbers.aspx.cs"
    Inherits="EngineeringTools_FormC_FormCWithPartNumbers" %>

<%@ Register Assembly="GroupingView" Namespace="UNLV.IAP.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="FormCWithPartNumbers" method="post" runat="server">
    <div>
        <asp:Button ID="btnApply" runat="server" Text="Apply change" OnClick="btnApply_Click" />
        <asp:Label ID="lblSelectedCitemids" runat="server" Text=""></asp:Label>
        <br />
        
        <br />
        <asp:Label ID="Label4" runat="server" Text="Please Select Group Not Inserted :" ForeColor="#FF5050"></asp:Label>
        <br />
        <br />
    </div>
    <div>
        <cc1:GroupingView ID="GRVPartNumber" runat="server" OnItemDataBound="GRVPartNumber_ItemDataBound">
            <GroupTemplate>
                <%-- <ul >

            <asp:PlaceHolder id="itemPlaceholder" runat="server" />
          </ul >--%>
                <%-- table header --%>
                <b>
                <%#Eval("CategoryAddress")%>
                <%#Eval("AssyCode")%>
                <%#Eval("PartCode")%>
                <asp:CheckBox ID="CheckBox1" runat="server" />
                <asp:Label Visible="false" ID="Label2" runat="server" Text='<%#Eval("Citemid")%>'></asp:Label>
                </b>
                <table border="1">
                    <%-- data items --%>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    <%-- computed totals and averages for the group --%>
                </table>
            </GroupTemplate>
            <ItemTemplate>
                <%--<li>           
            <asp:Label ID="Label1" runat="server" Text='<%#Eval("PartNumber")%>'></asp:Label>
          </li>--%>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("PartNumber")%>'></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </cc1:GroupingView>
    </div>
    </form>
</body>
</html>
