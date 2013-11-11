//TextBox Count Functioin

function Count(textboxid, length) {

  //  var sss = $('#<%= textboxid.ClientID %>');
    var maxlength = new Number(length); // Change number to your max length.
    var selectstring = "#" + textboxid.id;
    

    if ($(selectstring).val().length > maxlength) {
        //alert($('#<%= txtcomment1.ClientID %>'))
        textboxid.value = textboxid.value.substring(0, maxlength);
        alert("Input Can Not Exceed " + maxlength);
    }

}



