<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartListAddTSD.aspx.cs" Inherits="EngineeringTools_PartsList_EtagridView_PartListAddTSD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">


        function insertCheck() {
            if (selModuleInsert.value == '') {
                window.alert('Must select a module to insert items into.')
            }
            else {
                goInsert()
            }
        }

        function goInsert() {
            window.location = 'http://colweb01/eta/EngineeringTools/PartsList/EtagridView/PartListInsert.aspx?Module=' + selModuleInsert.value
        }

        function goNew() {
            if (selModuleNew.value == '') {
                window.alert('Must select a module to attach to.')
            }
            else {
                window.location = 'http://colweb01/eta/EngineeringTools/PartsList/EtagridView/PartListNew.aspx?Module=' + selModuleNew.value
            }
        }

        function goSub() {
            if (selModuleSub.value == '') {
                window.alert('Must select a module to select from.')
            }
            else {
                window.location = 'http://colweb01/eta/EngineeringTools/PartsList/PartsListSubSelect.asp?Module=' + selModuleSub.value
            }
        }

    </script>

    <style type="text/css">
        .style1
        {
            height: 79px;
            width: 668px;
        }
        .style2
        {
            width: 165px;
        }
        .style3
        {
            height: 22px;
            width: 668px;
        }
        .style4
        {
            width: 668px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellspacing="1" cellpadding="1">
        <tr>
            <th class="style4">
                <font size="6">Parts List Add Menu</font>
            </th>
        </tr>
        <tr>
            <td class="style4">
                <hr color="navy">
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                Part List to Insert into:
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                <asp:DropDownList ID="ddlinsertnew" runat="server">
                    <asp:ListItem Value="-Select Part List-" Text="-Select Part List-" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                <asp:Button ID="btninsertnewitems" runat="server" Text="Insert New Items" 
                    Width="150px" onclick="btninsertnewitems_Click" />
            </td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                <hr color="navy">
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                Part List to attach to:
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                
                <asp:DropDownList ID="ddlattach" runat="server">
                    <asp:ListItem Value="-Select Part List-" Text="-Select Part List-"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    
        <tr>
            <td align="middle" class="style4">
                <asp:Button ID="btn" runat="server" Text="New Page" Width="150px" onclick="btn_Click" 
                     />
            </td>
        </tr>
        
        <tr>
            <td align="middle" class="style4">
                <hr color="navy">
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                Part List to select from:
            </td>
        </tr>
        <tr>
            <td align="middle" class="style4">
               
                <asp:DropDownList ID="ddlsublist" runat="server">
                    <asp:ListItem Value="-Select Part List-" Text="-Select Part List-" Selected="True"></asp:ListItem>
                </asp:DropDownList>
               
            </td>
        </tr>
         <tr>
            <td align="middle" class="style4">
               
                <asp:Button ID="btnsublist" runat="server" Text="New Sub List" Width="150px" onclick="btnsublist_Click" 
                     /></td>
        </tr>
        <tr>
            <td align="middle" class="style4">
                <hr color="navy">
            </td>
        </tr>
     
        <tr>
            <td align="middle" class="style1">
                  <br />
                <br />
                <table  style="width:100%;  ">
        <tr>
            <td>
                PKG #
            <td class="style2">
               
                <asp:TextBox ID="txtcpkg" runat="server"></asp:TextBox>
                </td>
            <td>
                Assy Code</td>
                 <td>
                <asp:TextBox ID="txtassy" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Pg1</td>
            <td class="style2">
                <asp:TextBox ID="txtpg1" runat="server"></asp:TextBox>
                </td>
            <td>
                Pg2</td>
                 <td>
                <asp:TextBox ID="txtpg2" runat="server"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td>
                Pg3</td>
            <td class="style2">
                <asp:TextBox ID="txtpg3" runat="server"></asp:TextBox>
                 </td>
            <td>
                PartPart List to Copy&nbsp; </td>
                 <td>
                               
                <asp:DropDownList ID="ddlcattach" runat="server">
                    <asp:ListItem Value="-Select Part List-" Text="-Select Part List-"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
                      
            </td>
        </tr>
        
           <tr>
            <td align="middle" class="style3">
               
                <asp:Button ID="btncopylist" runat="server" Text="New Copy List" Width="150px" onclick="btncopylist_Click" 
                     />
                <br/>
              
              
            </td>
        </tr>
      
    </table>
   <%-- <input type="hidden" id="hidPackage" value="<%=Package%>">
    <input type="hidden" id="hidEci" value="<%=EciNumber%>">--%>
        
    
   
    
    
    <p>
                
    </p>
    </form>
    
    
    </body>
</html>
