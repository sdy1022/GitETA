<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ETAReleaseAIOServer.aspx.cs"
    Inherits="EngineeringTools_PartRegistry_ETAReleaseAIOServer" %>

<%@ Register Src="PartList.ascx" TagName="PartList" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../../Images/minus.gif";
            } else {
                div.style.display = "none";
                img.src = "../../Images/plus.gif";
            }
        }

        function divexpandcollapseChild(divname) {
            var div1 = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div1.style.display == "none") {
                div1.style.display = "inline";
                img.src = "../../Images/minus.gif";
            } else {
                div1.style.display = "none";
                img.src = "../../Images/plus.gif"; ;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtpackage" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" Text="Search By Package"
            Width="187px" />
    </div>
    <br />
    <div>
        <asp:GridView ID="myGridView" runat="server" BorderWidth="1px" GridLines="Horizontal"
            CellPadding="2" PageSize="3" Width="100%" RowHeaderColumn=" " AutoGenerateColumns="False" 
            HorizontalAlign="left" onrowdatabound="myGridView_RowDataBound">
            <EmptyDataTemplate>
                No Member Record<br />
            </EmptyDataTemplate>
            <Columns>
              <asp:TemplateField ItemStyle-Width="20px">
                    <ItemTemplate>
                        <a href="JavaScript:divexpandcollapse('div<%# Eval("ID") %>');">
                            <img id="imgdiv<%#Eval("ID") %>" width="9px" border="0" src="../../Images/minus.gif"
                                alt="" /></a>
                    </ItemTemplate>
                    <ItemStyle Width="20px" VerticalAlign="Middle"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Aitemid">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblitemid" runat="server" Text='<%# Bind("AItemId")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CitemId">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblitemcid" runat="server" Text='<%# Bind("ID")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ModuleNumber">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblmodule" runat="server" Text='<%# Bind("ModuleNumber")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="KEYCODE">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblkeycode" runat="server" Text='<%# Bind("KEYCODE")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ADD_TFC">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbladdtfc" runat="server" Text='<%# Bind("ADD_TFC")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ADD_INDEX">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lbladdIndex" runat="server" Text='<%# Bind("ADD_INDEX")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PartList">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <div id="div<%# Eval("ID") %>" style="overflow: auto; display: inline; position: relative;left: 15px; overflow: auto">
                            <%--    <uc1:ETAGridView ID="ETAGridView1" runat="server" CurrentRev='<%# currentrev %>'  HeaderID='<%# Bind("Headerid") %>'   PackageName='<%# package %>'  EciAcid='<%# eciacid%>' EciNumber='<%# ecinumber%>'  KeyA='<%# keya%>'   EciMode='<%# ecimode%>' Module='<%# module%>' />
--%>
                            <uc1:PartList ID="PartList1" runat="server" PackageName='<%#PackageName%>' PARENTPART='<%# Bind("ADD_INDEX")%>' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
