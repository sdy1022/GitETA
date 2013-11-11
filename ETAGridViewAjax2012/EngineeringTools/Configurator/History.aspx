<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="EngineeringTools_Configurator_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../Scripts/CSS/site2.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="row">
            <div id="left">
                <p>
                    Package:</p>
            </div>
            <div id="middle">
                <p>
                    <asp:Label ID="lblpackage" runat="server"></asp:Label></p>
            </div>
           
        </div>
        <div id="row" style="visibility:hidden">
            <div id="left">
                <p>
                    History:</p>
            </div>
            <div id="middle">
                    <asp:DropDownList ID="ddlhistory" runat="server" OnSelectedIndexChanged="ddlhistory_SelectedIndexChanged">
                    </asp:DropDownList>
            </div>
          </div>
        <div>
            <div id="left">
                
                    <asp:Button ID="btnQuery" runat="server" Text="Query" Width="140px" OnClick="btnQuery_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnautopair" runat="server" Text="Schedule Configurator Run" 
                        Width="191px" OnClick="btnautopair_Click" />
             </div>  
             <div id="middle">
                
                    <asp:Label ID="lblstatus" runat="server" EnableViewState="False"></asp:Label>
               
            </div>
        </div>
        <div id="row">
            <div id="left">
                <p>
                    &nbsp;</p>
                <p>
                <asp:Label ID="lblgridscstatus" runat="server" EnableViewState="True"></asp:Label>
                </p>
               
            </div>
        </div>
     
    </div>
       <div>
            
         
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
                        BorderColor="Tan" BorderStyle="Solid" BorderWidth="1px" CellPadding="2" DetailSummaryText="View Details"
                        ForeColor="Black" GridLines="Horizontal" PageSize="50" RowHeaderColumn=" " Width="95%">
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <EmptyDataTemplate>
                            Please Select Historical Record From DropDown List<br />
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" HorizontalAlign="Center" />
                        <RowStyle HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbllistlevel" runat="server" Text='<%# Eval("List_Level")%>' Visible="false" />
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MODELCODE" HeaderText="MODELCODE" SortExpression="MODELCODE" />
                            <asp:BoundField DataField="ATT" HeaderText="ATT" SortExpression="ATT" />
                            <asp:BoundField DataField="KEYCODE" HeaderText="KEYCODE" SortExpression="KEYCODE" />
                            <asp:BoundField DataField="ITEMCODE" HeaderText="ITEMCODE" SortExpression="ITEMCODE" />
                            <asp:BoundField DataField="ADD_TFC" HeaderText="ADD_TFC" SortExpression="ADD_TFC" />
                            <asp:BoundField DataField="ADD_INDEX" HeaderText="ADD_INDEX" SortExpression="ADD_INDEX" />
                            <asp:BoundField DataField="PageCode3" HeaderText="PageCode3" SortExpression="PageCode3" />
                            <asp:BoundField DataField="DEL_TFC" HeaderText="DEL_TFC" SortExpression="DEL_TFC" />
                            <asp:BoundField DataField="DEL_INDEX" HeaderText="DEL_INDEX" SortExpression="DEL_INDEX" />
                            <asp:BoundField DataField="FROM_ECI" HeaderText="FROM_ECI" SortExpression="FROM_ECI" />
                            <asp:BoundField DataField="FROM_DATE" HeaderText="FROM_DATE" SortExpression="FROM_DATE" />
                            <asp:BoundField DataField="TO_ECI" HeaderText="TO_ECI" SortExpression="TO_ECI" />
                            <asp:BoundField DataField="TO_DATE" HeaderText="TO_DATE" SortExpression="TO_DATE" />
                            <asp:BoundField DataField="List_Level" HeaderText="List_Level" SortExpression="List_Level" />
                        </Columns>
                    </asp:GridView>
               
            
        </div>
    </form>
</body>
</html>
