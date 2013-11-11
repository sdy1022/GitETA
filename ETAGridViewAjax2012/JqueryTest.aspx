<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JqueryTest.aspx.cs" Inherits="JqueryTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- <script src="scripts/jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>
   --%>    
       <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.jqcascade.min.js" type="text/javascript"></script>
  
    
     <script type="text/javascript">

         $('#Select2').cascading({
            
             dataUrl: '../EtaAdmin/WebService/ETAServices.asmx/GetJasonResultTest',
             parentDropDownId: 'Select1',
             noSelectionValue: '11',
             noSelectionText: '[sdy]'
         });




//         $(document).ready(function() {

//             $("#name").change(function() {

//                 // alert("text change");
//                 var ss = $("#name")[0];
//                 if (ss) {
//                     alert(ss.text);
//                     ss.focus();
//                 }
//             });


//             $("#sayHelloButton").click(function(event) {


//                 $.ajax({
//                     type: "POST",
//                     url: "EtaAdmin/WebService/ETAServices.asmx/ValidateModule",
//                     data: "{'module': '" + $("#name").val() + "'}",
//                     contentType: "application/json; charset=utf-8",
//                     dataType: "json",
//                     success: function(msg) {
//                         AjaxSucceeded(msg);
//                     },
//                     error: AjaxFailed
//                 });
//             });
//         });
//         function AjaxSucceeded(result) {
//             if (result.toString() == "false") {
//                 alert("ASdfasdF");
//             }
//            
//              
//           }  
//           function AjaxFailed(result) {  
//               alert(result.status + " " + result.statusText);
//           }

//           function sayHelloButton_onclick() {

//           }

     </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
      <%--  <input id="name" />
    
        <input id="sddse0" /><br />

   <input id="sayHelloButton" value="Validate"  type="button" onclick="return sayHelloButton_onclick()" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" 
            style="height: 26px" />--%>
        
      
    
<select name="drop1" id="Select1" size="4" multiple="multiple">

    <option value="1">item 1</option>

    <option value="2">item 2</option>

    <option value="3">item 3</option>

    <option value="4">item 4</option>

  

</select>

<select name="drop2" id="Select2" size="4" multiple="multiple">

    <option value="11">item 11</option>

    <option value="12">item 12</option>

    <option value="13">item 13</option>

    <option value="14">item 14</option>

  
</select>

<select name="drop3" id="Select3" size="4" multiple="multiple">

    <option value="111">item 111</option>

    <option value="112">item 112</option>

    <option value="113">item 113</option>

    <option value="114">item 114</option>



</select>


        <br />
        <br />
        <br />
        <br />
        <br />
        
      
    


    </div>
    </form>
</body>
</html>
