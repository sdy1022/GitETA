<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LinkSelection2.aspx.cs" Inherits="PackageMaintenance_LinkSelection2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="http://colweb01/eta/scripts/jquery.min.js" type="text/javascript"></script>

    <% if (false){%>

    <script src="../scripts/jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>

    <% }%>
</head>
<body>
   <form  id="formlinkselectioin"  method="post"  >
     <div ID="modulelocked"  runat="server">
    
    <CENTER>
	<H1 >You cannot Link to the Module.<br>  
	It is either locked or has been Released.</H1>

	</CENTER>
        
    
    </div>    
     <div ID="moduleregular"  runat="server">
    
   
		<CENTER>
            
            
        	<FONT size=5><STRONG>Module to link</STRONG></FONT>
	   
		<BR>
	<INPUT name="txtLinkAs" style="TEXT-TRANSFORM: uppercase" id="txtModule"><BR>
	<BR>
	 <input id="submit1" value="Submit"  type="button" onclick="return submit1_onclick()" /> 
	 <input id="Hidden1" type="hidden" name="sdy"   runat="server" >

			<br />
			<img alt="In Process" src="post-2-1222892578.gif" width="200" height="200" id="imageinprocess"  /><BR>
	Link will be created in Package

            
		
		</CENTER>
        
    
    </div>
     <script type="text/javascript">
		jQuery.stringFormat = function(format, arguments) {
			var str = format;
			for (i = 0; i < arguments.length; i++) {
				
				str = str.replace('{' + i + '}', arguments[i]);
			}
			return str;
		};

         /*==========用户自定义方法==========*/
         function RequestQuerytring(strName) {


             var strHref = window.document.location.href;
             var intPos = strHref.indexOf("?");
             var strRight = strHref.substr(intPos + 1);

             var arrTmp = strRight.split("&");
             for (var i = 0; i < arrTmp.length; i++) {
                 var arrTemp = arrTmp[i].split("=");

                 if (arrTemp[0].toUpperCase() == strName.toUpperCase()) return arrTemp[1];
             }
             return "";
         }


         function AjaxSucceeded(result) {


             if (result.toString() == "Having Invalid Quantity") {


                 alert("Can Not Implement Because This Module Has Invalid Quantity Value");
                 return;

             }

             if (result.toString() != "SUCCESS") {

                 //$("#submit1").removeAttr("disabled");

                 // alert (result.toString().substring(0, 3));
                 if (result.toString().substring(0, 3) == "INV") {



				//	alert($.stringFormat("{0} {1}!", ["Hello", "world"]));
                    

					   				
						 mystr=result.toString().split(";"); 
						
						 var sdy2="";
						 for(i=0;i<mystr.length-1;i++)   
						 {
								if(i==0)
								{
									mystr[i]=mystr[i].split(":")[1];
								}
								mystr[i]=mystr[i].split("@")[0];
								sdy2=sdy2+mystr[i]+" | "
								if((i-2)%3==0)
								{
									sdy2=sdy2+"\n";
									
								}
						 }
						
						var answer = confirm("This Moudle Has The Following Invalid Part Number(s) !\nPress OK to continue, or Cancel to stay on the current page\n\n" + sdy2);
						if (answer){
							//alert("Continue!")
							
							 window.location.href = "LinkModuleEcrCheckNoParts.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();

						}
						else{
							//alert("Cancel!")
							$("#submit1").removeAttr("disabled");
						}

					

                 }
                 else {

                     alert("This Moudle Has The Following Invalid Part Number(s) !!!\n" + result.toString());
                     window.location.href = "LinkModuleEcrCheckNoParts.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();


                 }


             }
             else {

                 // alert("SaModuleEcrCheckByquerystring.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val());

                 window.location.href = "LinkModuleEcrCheckNoParts.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();
             }

         }
         function AjaxFailed(result) {
             alert(result.status + " " + result.statusText);
         }

         /*==========事件绑定==========*/
		
         $(function() {

         });

         /*==========加载时执行的语句==========*/
         $(function() {

             $(document).ready(function() {

                 $("#imageinprocess").hide();

             });


             $("#submit1").click(function(event) {





                 if ($("#txtModule").val().length < 1 || $("#txtModule").val().substr(4, 1) != "-") {


                     alert("Please Set Correct Module To Copy.");
                 }
                 else {
                     $("#submit1").attr("disabled", "true");
                     $("#imageinprocess").show();


                     $.ajax({
                         type: "POST",
                         url: "../EtaAdmin/WebService/ETAServices.asmx/GetInvalidPartNosFromModule",
                         data: "{'module': '" + $("#txtModule").val() + "'}",
                         contentType: "application/json; charset=utf-8",
                         dataType: "json",
                         success: function(msg) {
                             AjaxSucceeded(msg.d);
                         },
                         error: AjaxFailed
                     });

                 }
             });
         }); 
function submit1_onclick() {

}

    </script>

    
    
    
    
    
    </form>
    
    
</body>
</html>
