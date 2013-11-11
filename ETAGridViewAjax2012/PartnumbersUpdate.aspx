<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartnumbersUpdate.aspx.cs" Inherits="PartnumbersUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Literal ID="Literal1" runat="server">Part Number Length </asp:Literal>
       <br />
        <asp:DropDownList ID="ddllengthcount" runat="server" 
            Height="21px" Width="83px" Visible="False">
            <asp:ListItem>&lt;9</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
            <asp:ListItem>16</asp:ListItem>
            <asp:ListItem>17</asp:ListItem>
            <asp:ListItem>18</asp:ListItem>
            <asp:ListItem>19</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>21</asp:ListItem>
            <asp:ListItem>22</asp:ListItem>
            <asp:ListItem>23</asp:ListItem>
            <asp:ListItem>&gt;23</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" Text="Get Records(First 100)" 
            onclick="Button1_Click" Visible="False" />
    
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Get Records By Query" Visible="False" Width="181px" />
        <asp:Label ID="lblpath" runat="server" ForeColor="#FF3300"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
            Text="Generate Update Query" Width="252px" />
        <asp:TextBox ID="TextBox2" runat="server" Width="218px"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server" Height="150px" TextMode="MultiLine" 
            Width="713px" ontextchanged="TextBox1_TextChanged">select partnumber, designnumber, itemid  ,dbo.GetModelByPartNo(substring(designnumber,1,4)) as idenmodel  </asp:TextBox>
    
        <br />
    
    </div>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Width="441px" PageSize="200" 
        onrowdatabound="GridView1_RowDataBound">
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <Columns>
            <asp:TemplateField HeaderText="Added Value">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("finalresult") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblfinalresult" runat="server" Text=''></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("idenmodel") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblidenmodel" runat="server" Text='<%# Bind("idenmodel") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("partnumber") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblpartno" runat="server" Text='<%# Bind("partnumber") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
