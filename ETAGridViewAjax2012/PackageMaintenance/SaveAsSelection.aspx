<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaveAsSelection.aspx.cs"
    Inherits="PackageMaintenance_SaveAsSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script src="http://colweb01/eta/scripts/jquery.min.js" type="text/javascript"></script>

    <% if (false)
       {%>

    <script src="../scripts/jquery-1.3.2-vsdoc2.js" type="text/javascript"></script>

    <% }%>
    <!--<script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js" type="text/javascript"></script> -->
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="modulelocked" runat="server">
        <center>
            <h1>
                You cannot Copy into this Module.<br>
                It is either locked or has been Released.</h1>
        </center>
    </div>
    <div id="moduleregular" runat="server">
        <center>
            <font size="5"><strong>Module to Copy</strong></font> <font color="red" size="2"><strong>
                NOTE: This may take a few seconds for large modules!<br>
                &nbsp;DO NOT click more than once.</strong></font>
            <br>
            <input name="txtSaveAs" style="text-transform: uppercase" id="txtModule"><br>
            <br />
            <img alt="In Process" src="post-2-1222892578.gif" width="200" height="200" id="imageinprocess" /><br />
            <br>
            <input id="submit1" value="Submit" type="button" />
            <br>
            <br>
            New Module will be pasted into Package
        </center>
    </div>

    <script type="text/javascript">


        /*==========用户自定义方法==========*/
        jQuery.stringFormat = function(format, arguments) {
            var str = format;
            for (i = 0; i < arguments.length; i++) {

                str = str.replace('{' + i + '}', arguments[i]);
            }
            return str;
        };
        
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


                alert("Can Not Implement Because This Moudle Has Invalid Quantity Value");
                return;

            }


            if (result.toString() != "SUCCESS") {

                $("#submit1").removeAttr("disabled");

                // alert (result.toString().substring(0, 3));
                if (result.toString().substring(0, 3) == "INV") {



                    // invalid format 
                    // alert("Can Not Implement Because This Moudle Has The Invalid Part Number(s) !!!\n" + result.toString());
                    //window.location.href = "FormCWithPartNumbers.aspx?FromPage=SaveAs&Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();


                    mystr = result.toString().split(";");

                    var sdy2 = "";
                    for (i = 0; i < mystr.length - 1; i++) {
                        if (i == 0) {
                            mystr[i] = mystr[i].split(":")[1];
                        }
                        mystr[i] = mystr[i].split("@")[0];
                        sdy2 = sdy2 + mystr[i] + " | "
                        if ((i - 2) % 3 == 0) {
                            sdy2 = sdy2 + "\n";

                        }
                    }

                    var answer = confirm("This Moudle Has The Following Invalid Part Number(s) !\nPress OK to continue, or Cancel to stay on the current page\n\n" + sdy2);
                    if (answer) {
                        //alert("Continue!")

                        window.location.href = "SaModuleEcrCheckByquerystring.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();

                    }
                    else {
                        //alert("Cancel!")
                        $("#submit1").removeAttr("disabled");
                    }



                }
                else {

                    alert("This Moudle Has The Following Invalid Part Number(s) !!!\n" + result.toString());
                    window.location.href = "SaModuleEcrCheckByquerystring.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();


                }



            }
            else {

                //  alert("SaModuleEcrCheckByquerystring.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val());

                window.location.href = "SaModuleEcrCheckByquerystring.asp?Package=" + RequestQuerytring("Package") + "&Module=" + $("#txtModule").val();
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

                // alert($("#txtModule").val().length);


                if ($("#txtModule").val().length < 1) {


                    // alert("Please indicate Module to Copy.");
                }
                else {

                    $("#imageinprocess").show();
                    $("#submit1").attr("disabled", "true")
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
    </script>

    </form>
</body>
</html>
