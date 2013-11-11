<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebScheduler.aspx.cs" Inherits="WebScheduler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style2
        {
            width: 177px;
        }
        .style3
        {
            width: 123px;
        }
        .style4
        {
            width: 155px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            Note :All Created Tasks Will Be Executed On <font color="Blue">Daily Basis</font></h2>
    </div>
    <div>
        <div>
            <p>
                &nbsp;</p>
        </div>
        <div>
            Select How Many Hours&nbsp; To Executed From Now&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Selected="True">1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
                <asp:ListItem>3</asp:ListItem>
                <asp:ListItem>4</asp:ListItem>
                <asp:ListItem>5</asp:ListItem>
                <asp:ListItem>6</asp:ListItem>
                <asp:ListItem>7</asp:ListItem>
                <asp:ListItem>8</asp:ListItem>
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
                <asp:ListItem>24</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
        </div>
    </div>
    <table style="width: 90%; ">
        <tr align="center" style="color:blueviolet;font-size: 16pt" >
            <td class="style4" >
                Job Name
            </td>
           
            <td class="style2" >
                Schedule
                Job</td>
            <td class="style3">
                Delete
                Job</td>
             <td>
                Status
            </td>
        </tr>
        <tr>
            <td class="style4">
                AutoPair Scheduler
            </td>
         
            <td class="style2">
                <asp:Button ID="btnapschedule" runat="server" OnClick="btnap_Click" Text="Schedule/Reschedule"
                    Width="171px" />
            </td>
            <td class="style3">
                <asp:Button ID="btnapdelete" runat="server" Text="Delete" Width="94px" 
                    OnClick="btndelete_Click" />
            </td>
               <td>
                <asp:Label ID="lblapjobstatus" runat="server" Font- BackColor="#66FF33"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4">
                Configurator Scheduler
            </td>
            
            <td class="style2">
                <asp:Button ID="Button2" runat="server" OnClick="btncf_Click" Text="Schedule/Reschedule"
                    Width="171px" height="26px" />
            </td>
            <td class="style3">
                <asp:Button ID="btncondelete" runat="server" Text="Delete" Width="94px" 
                    OnClick="btncfdelete_Click" />
            </td>
            <td>
                <asp:Label ID="lblconjobstatus" runat="server" Font- BackColor="#66FF33"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
            <asp:Button ID="btnprocess" runat="server" Text="Check Current All Job Status" OnClick="btnprocess_Click"
                Width="173px" />
        </p>
    <p>
            &nbsp;</p>
    </form>
</body>
</html>
