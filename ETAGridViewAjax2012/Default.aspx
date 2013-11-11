<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register src="EngineeringTools/PartRegistry/PartRegistryNumber.ascx" tagname="PartRegistryNumber" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
        <script src="scripts/jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>
      
    
     <script type="text/javascript">




         $(document).ready(function() {

             $("#Text1").change(function() {

                 // alert("text change");
                 var ss =  $("#Text1")[0];
                 if (ss) {
                     alert(ss.text);
                     ss.focus();
                 }
             });
              });
              
              </script>

    <style type="text/css">
        #Text2
        {
            width: 930px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        
          <input id="Text1" type="text" /></p>
            <input id="Text2" type="text" /><asp:TextBox ID="TextBox1" runat="server" Width="820px">EngineeringTools/Configurator/History.aspx</asp:TextBox>
           <asp:Button ID="Button1" runat="server" 
              onclick="Button1_Click1" Text="Edit Test" Visible="False" />
           <asp:TextBox ID="TextBox2" runat="server" Width="820px">http://colweb01/etatest/EngineeringTools/FormC/FormCDataADD.aspx?Package=XXX0 </asp:TextBox>
       
          <asp:Button ID="Button2" runat="server" 
              onclick="Button1_Click" Text="Add Test" Visible="False" />
                 
                 <br />
          <br />
          <br />
          <br />
          <br />
          <br />
                 
                 Please input a valid Package value here 
          <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
          <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="AutoPair" 
              Width="209px" />
                 
          <br />
          <br />
          <br />
          <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="Schedule" 
              Width="111px" />
          <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
&nbsp;Input mintes to run from now<asp:Button ID="Button5" runat="server" 
              onclick="Button5_Click" Text="Shutdown Schedule" Width="213px" />
                           
          &nbsp;</p>
         
    </div>

<div>   <uc1:PartRegistryNumber ID="PartRegistryNumber1" runat="server" /> </div>

  <div>   <uc1:PartRegistryNumber ID="PartRegistryNumber2" runat="server" PackageName="XXXX" /> </div>


    </form>

  </body>
