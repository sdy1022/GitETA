<%@ Page Language="C#"   MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="PartListEdit.aspx.cs" Inherits="_Default" %>
<%@ Import namespace="System.Data" %>
<%@ Register src="ETAGridView.ascx" tagname="ETAGridView" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script language=jscript>
    function EciLogdataCheck(ecilogname) {
        var itemSelected = false
        var elm = document.getElementsByTagName('input');
        
        for (var i = 0; i < elm.length; i++) {
           
            if (elm.item(i).type == "checkbox" && elm.item(i).checked == true && elm.item(i).id==ecilogname ) {
                // if (elm.item(i).type == "checkbox" && elm.item(i).checked == true && elm.item(i).value == "chkecilog") {
                //    alert(elm.item(i).value)
               // alert(elm.item(i).id);
               // alert(elm.item(i).value);
                    
                itemSelected = true;
                //   alert(i);
                break;
            }

        }

        return itemSelected;

    }
function InvokePop(fname,ecilogname ,packagename, ecinumber) 
	{

	   // alert(ecilogname+ ";" + packagename + ";" + ecinumber);
	    if (document.getElementById(fname).value == "" || document.getElementById(fname).value == "undefined")
	     {

	        //   alert(" popup")

	         // if checkbox
	         if (EciLogdataCheck(ecilogname)) 
	         {
	             retVal = window.showModalDialog('http://colweb01/eta/Eci/SelectEciPLItem_new.asp?Package=' + packagename + '&Eci=' + ecinumber, null, 'width=1000mheight=600,scrollbars=yes,resizable=yes')
	      //       alert(retVal);
	             document.getElementById(fname).value = retVal;
	         }
	    }

	    else {
	      //    alert(" not popup");
	    }

        


	}
    


</script>

<script type="text/javascript">
    function SetCursorToTextEnd(textControlID) {
        var text = document.getElementById(textControlID);
        if (text != null && text.value.length > 0) {
            if (text.createTextRange) {
                var FieldRange = text.createTextRange();
                FieldRange.moveStart('character', text.value.length);
                FieldRange.collapse();
                FieldRange.select();
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <div align="center" >
        <asp:Label ID="lblecimode" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
        </div>
      <asp:GridView ID="myGridView" Runat="server" BorderWidth="1px" 
           GridLines="Horizontal" CellPadding="2"
           PageSize="3" 
            Width="100%" RowHeaderColumn=" " 
            AutoGenerateColumns="False" 
            HorizontalAlign="left">
            <EmptyDataTemplate>
               No Member Record<br />
            </EmptyDataTemplate>           
            
             <Columns>
                 <asp:TemplateField >
                     <ItemStyle HorizontalAlign="Center" />
                     <ItemTemplate>                        
                         <uc1:ETAGridView ID="ETAGridView1" runat="server" CurrentRev='<%# currentrev %>'  HeaderID='<%# Bind("Headerid") %>'   PackageName='<%# package %>'  EciAcid='<%# eciacid%>' EciNumber='<%# ecinumber%>'  KeyA='<%# keya%>'   EciMode='<%# ecimode%>' Module='<%# module%>' />
                     </ItemTemplate>
                 </asp:TemplateField>
            
            </Columns>
        <RowStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>
    
  
    
    </form>
</body>
</html>
