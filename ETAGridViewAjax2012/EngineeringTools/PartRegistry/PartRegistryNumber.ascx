<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartRegistryNumber.ascx.cs"
    Inherits="EngineeringTools_PartRegistry_PartRegistryNumber" %>
<div style="width: 220px; padding: 0px">
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtfront"
        ErrorMessage="Error" ValidationExpression="\w{5}">*</asp:RegularExpressionValidator>
    <asp:TextBox ID="txtfront" runat="server" MaxLength="5" Width="50px" Style="height: 22px"
        OnTextChanged="txtfront_TextChanged"></asp:TextBox>
    <asp:TextBox ID="TextBox3" runat="server" Width="10px" Style="height: 22px; border: none"
        ReadOnly="True">-
    </asp:TextBox>
    <asp:TextBox ID="txtmiddle" runat="server" MaxLength="5" Width="50px" Style="height: 22px"
        ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="TextBox2" runat="server" Width="10px" Style="height: 22px; border: none"
        ReadOnly="True">-
    </asp:TextBox>
    <asp:TextBox ID="Txtend" runat="server" MaxLength="2" Width="20px" Style="height: 22px"
        OnTextChanged="Txtend_TextChanged"></asp:TextBox>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Txtend"
        ErrorMessage="Error" ValidationExpression="\w{2}">*</asp:RegularExpressionValidator>
</div>
