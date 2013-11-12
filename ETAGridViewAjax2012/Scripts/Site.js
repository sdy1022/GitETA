/*
*
* Date: 06/06/2013
* Js file for General Datagrid Template
*/
// Customized Code
var defaultpagesize = 25;
var defaultpage = 1;
var currentpackage = "";
var gridtitle = "";
var userinfo = "";
var roleinfo = "";
var tablecolumninputs = "";
var tcoarray = new Array();
var handlername = "";
var idfieldname = "";
var sortfieldname = "";
var isadmin = false;
var activeeci = "";
//// Init Values
//$.validator.setDefaults({
//    ignore: ':hidden, [readonly=readonly]'
//});


function GetRowFlag(aeci, feci, toeci) {
    //flag 1: full control
    //flag 2: partial control. can not delete
    // flag 3: readonly
    if (isadmin) {
        return 1;
    };
    if (aeci == feci) {
        return 1;
    } else {
        if (toeci.length == 0) {
            return 2;
        } else {
            return 3;
        }
    };

}

///
function InitValues() {
    defaultpagesize = 25;
    defaultpage = 1;
    var temppack = getURLParam("Package");
    if (temppack.length < 1) {
        currentpackage = "N121";

    } else {
        currentpackage = temppack; //"XXX0";
    }

    if (!isadmin) {
        //   $("#searchfield").addClass("hide");
        $("#searchfield").first().hide();
    } else {
        $("#searchfield").first().show();
    }

    gridtitle = "Part Registry Table";
    handlername = "CRUDPageRegistryHanlder.ashx";
    idfieldname = "TID";
    sortfieldname = "PartNo";
    tcoarray.push({ name: "TID", width: 60, editable: 0 });
    tcoarray.push({ name: "PartNo", width: 280, editable: 0 });
    tcoarray.push({ name: "Minor", width: 100, editable: 1 });
    tcoarray.push({ name: "Description", width: 360, editable: 0 });
    tcoarray.push({ name: "TMHU_View", width: 150, editable: 1 });
    tcoarray.push({ name: "From_ECI", width: 200, editable: 1 });
    tcoarray.push({ name: "To_ECI", width: 200, editable: 1 });
    tcoarray.push({ name: "MATERIAL1", width: 150, editable: 1 });
    tcoarray.push({ name: "MATERIAL2", width: 150, editable: 1 });
    tcoarray.push({ name: "DRW", width: 150, editable: 1 });
    tcoarray.push({ name: "COMMENT1", width: 360, editable: 1 });
    tcoarray.push({ name: "From_Date", width: 200, editable: 1 });
    tcoarray.push({ name: "To_Date", width: 200, editable: 1 });
    tcoarray.push({ name: "Mod_From", width: 150, editable: 0 });

}
//// Customized Code Done
// Get value from array
function GetArrayItemByName(inputname, collectionarray) {
    if (typeof collectionarray === "undefined") {
        return "";
    }
    for (var i = 0; i < collectionarray.length; i++) {

        var item = collectionarray[i];
        if (item.name == inputname) {
            return item;
        }
    }

};

///Set Columns input value for datagrid.columns
function SetColumnsInput() {

    var result = new Array();
    for (var i = 0; i < tcoarray.length; i++) {

        var item = tcoarray[i];
        result.push({ field: item.name, title: item.name, width: item.width, sortable: true });
    }
    var res1 = new Array();
    res1[0] = result;
    tablecolumninputs = res1;
    return result;

}
// Cleanup all input when click adding diaglog second time
function ClearTextBoxSecond() {
    // might need to add more to clean up
    // all item start with mod

    var addselectionlist = $("[id*=add]");
    addselectionlist.each(function(index, value) {
        // alert(index + ': ' + value);
        $("#" + this.id).val("");

    });
}
//Show selected row info
function DisplaySelectInfo(flag) {

    // cleanup all errormsage
    //validatebox-text validatebox-invalid

    var selectionitem = $("#test").datagrid('getSelections')[0]; // var BindID = selectionitem.TID;
    var modselectionlist = $("[id*=mod]");
    // cleanup all errormsage
    //validatebox-text validatebox-invalid
    modselectionlist.each(function(index, value) {
        $("#" + this.id).removeClass('validatebox-invalid');
    });

    if (flag == 1) {
        $("#modFlag").text("Full Editable");
        $("#modTMHU_View").combobox('enable');
        $("#btnUpdateActionInfo").first().removeAttr('disabled');
        // enable all mod field
        modselectionlist.each(function(index, value) {
            $("#" + this.id).prop("readonly", false);
            // $("#" + this.id).removeClass('validatebox-invalid');
        });
    } else {
        $("#modTMHU_View").combobox('disable');
        $("#modddldrw").combobox('disable');
        if (flag == 2) {
            // only to-eci is editable
            $("#modFlag").text("Only To_Eci Is Editable");
            modselectionlist.each(function(index, value) {
                if (this.id.substring(3) == "To_ECI") {
                    $("#" + this.id).prop("readonly", false); //.removeAttr('disabled');
                } else {
                    $("#" + this.id).prop("readonly", true);
                    //.attr("disabled", "disabled");
                }
            });

        }
        else {// flag ==3
            $("#modFlag").text("Read Only");
            $("#btnUpdateActionInfo").first().attr("disabled", "disabled");
            modselectionlist.each(function(index, value) {
                $("#" + this.id).prop("readonly", true); //attr("disabled", "disabled");
            });
        }
        ;
    }

    //    modselectionlist.each(function(index, value) {
    //        // alert(index + ': ' + value);
    //        $("#" + this.id).val(selectionitem[this.id.substring(3)]);
    //        // $("#" + this.id).attr('readonly', true);

    //    });
    // Customized Code

    $("#modpackage").val(currentpackage);
    $("#modTID").text(selectionitem.TID);
    $("#modpartno1").val(selectionitem.PartNo.substring(0, 5));
    $("#modpartno2").val(selectionitem.PartNo.substring(6, 7));
    $("#modpartno3").val(selectionitem.PartNo.substring(12, 14));
    $("#modTMHU_View").combobox('setValue', selectionitem.TMHU_View);
    $("#modMaterial1").val(selectionitem.MATERIAL1);
    $("#modMaterial2").val(selectionitem.MATERIAL2);
    $("#modcomment").val(selectionitem.COMMENT1);
    $("#modmodfrom").val(selectionitem.Mod_From);
    $("#modddldrw").combobox('setValue', selectionitem.DRW);
    $("#modMinor").val(selectionitem.Minor);
    $("#modDescription").val(selectionitem.Description);
    $("#modFrom_ECI").val(selectionitem.From_ECI);
    $("#modTo_ECI").val(selectionitem.From_ECI);
    //    $("#toecimod").val(selectionitem.To_ECI);
    // Customized Code Done



}
// Check input change or not 
function IsInputsChange() {
    var result = 0;
    var selectionitem = $("#test").datagrid('getSelections')[0];
    var modselectionlist = $("[id*=mod]");

    modselectionlist.each(function(index, value) {
        var item = GetArrayItemByName(this.id.substring(3), tcoarray);
        if (typeof (item) != "undefined") {
            if (item.editable != 0) {
                // if column is not editable , skip 
                if (selectionitem[this.id.substring(3)] != $("#" + this.id).val()) {
                    result = 1;
                    return;

                }
            }
        }
    });


    if (result == 1) {
        return result;
    } else {
        // Customized Code 
        if (selectionitem.TMHU_View != $("#modTMHU_View").combobox('getValue')) {
            return 1;
        }
        if (selectionitem.DRW != $("#modddldrw").combobox('getValue')) {
            return 1;
        }
        if (selectionitem.MATERIAL1 != $("#modMaterial1").val() ) {
            return 1;
        }
        if (selectionitem.MATERIAL2 != $("#modMaterial2").val()) {
            return 1;
        }
        if (selectionitem.COMMENT1 != $("#modcomment").val()) {
            return 1;
        }
        if (selectionitem.Mod_From != $("#modmodfrom").val()) {
            return 1;
        }
        if (selectionitem.Description != $("#modcomment").val()) {
            return 1;
        }
        if (selectionitem.Minor != $("#modMinor").val()) {
            return 1;
        }

        if (selectionitem.From_ECI != $("#modFrom_ECI").val()) {
            return 1;
        }
        if (selectionitem.To_ECI != $("#modTo_ECI").val()) {
            return 1;
        }
        
//        $("#modpackage").val(currentpackage);
//        $("#modTID").text(selectionitem.TID);
//        $("#modpartno1").val(selectionitem.PartNo.substring(0, 5));
//        $("#modpartno2").val(selectionitem.PartNo.substring(6, 7));
//        $("#modpartno3").val(selectionitem.PartNo.substring(12, 14));
//        $("#modTMHU_View").combobox('setValue', selectionitem.TMHU_View);
//        $("#modMaterial1").val(selectionitem.MATERIAL1);
//        $("#modMaterial2").val(selectionitem.MATERIAL2);
//        $("#modcomment").val(selectionitem.COMMENT1);
//        $("#modmodfrom").val(selectionitem.Mod_From);
//        $("#modddldrw").combobox('setValue', selectionitem.DRW);
//        $("#modMinor").val(selectionitem.Minor);
//        $("#modDescription").val(selectionitem.Description);
//        $("#modFrom_ECI").val(selectionitem.From_ECI);
//        $("#modTo_ECI").val(selectionitem.From_ECI);
      

        if (selectionitem.PartNo != $("#modpartno1").val() + '-' + $("#modpartno2").val() + currentpackage + '-' + $("#modpartno3").val()) {
            return 1;
        }
        // Customized Code Done
        return 0;
    }

    /*
    if (selectionitem.PartNo != $("#PartNo1").val()) {
    return 1;
    } 

    if (selectionitem.TMHU_View != $("#TMHU_View1").combobox('getValue')) {
    return 1;
    }
   
    return 0;
    */
}
// Get Parameter Info
function getURLParam(name) {
    // get query string part of url into its own variable
    //activeeci=12345&Admin=1
    var url = window.location.href;
    var queryString = url.split("?");

    if (url.split("?").length > 1) {
        // make array of all name/value pairs in query string
        var params = queryString[1].split("&");

        // loop through the parameters
        var i = 0;
        while (i < params.length) {
            // compare param name against arg passed in
            var paramItem = params[i].split("=");
            if (paramItem[0] == name) {
                // if they match, return the value
                return paramItem[1];
            }
            i++;
        }
    }
    return "";
}
//Show Add Dialog
function ShowCreateActionInfoDialog() {
    $("#AddActionInfoDialog").dialog('open').dialog('setTitle', "Add Item");
    ClearTextBoxSecond();
    // Customized Code
    $("#addpackage").val(currentpackage);
    // Customized Code done
}
//Show Dialago When Modify is clicked
function ShowUpdateActionInfoDialog() {
    //get selected rows
    var checkId = $("#test").datagrid('getSelections');
    if (checkId.length == 1) {
        $("#UpdateActionInfoDialog").dialog('open').dialog('setTitle', 'Update Info');
        //show input
        // can decide if item is editabale/peditable/not
        // need to get current rowflag
        var selectionitem = $("#test").datagrid('getSelections')[0];
        // alert(selectionitem.From_ECI);
        // get row flag
        DisplaySelectInfo(GetRowFlag(activeeci, selectionitem.From_ECI, selectionitem.To_ECI));
    } else {
        $.messager.alert("Alert", "You Have Selected <font color='red' size='6'>" + checkId.length + "</font> Row(s)");
    }
}

// Load Datagrid based on parameter
function LoadDatagrid(queryParame) {

    // Customized Code 
    if (typeof queryParame === "undefined") {
        // use default

        inputdata = { mode: 'Qry', PackageName: currentpackage, pageSize: defaultpagesize, pageNumber: defaultpage };
    } else {
        inputdata = { mode: 'Qry', PackageName: queryParame.PackageName, pageSize: queryParame.pageSize, pageNumber: queryParame.pageNumber };
    }
    // Customized Code   done
    $.ajax({
        url: handlername, //'CRUDPageRegistryHanlder.ashx',
        type: 'post',
        dataType: 'json',
        data: inputdata, // { size: '10', ItemName: name },
        beforeSend: function() {
            $('#test').datagrid("loading"); //遮罩效果
        },
        success: function(json) {
            $("#test").datagrid("loadData", json); //加载数据
            $("#test").datagrid('reload'); //without this method , refresh will have error
        },
        error: function(data) {
            alert("Error Happed: " + data.responseText);
            $("#test").datagrid('reload');
        }
    });
}

//Delete
function DeleteMutipleActionInfoByClick() {
    // Get all selected rows 
    var checkId = $("#test").datagrid('getSelections');
    if (checkId.length >= 1) {

        var ids = "";
        for (var i = 0; i < checkId.length; i++) {
            ids += checkId[i].TID + ",";
        }
        ids = ids.substring(0, ids.length - 1);

        $.messager.confirm("Alert", "Are You Sure To Delete？", function(deleteData) {
            if (deleteData) {
                //Post Asynchronous request
                $.post(handlername, { mode: 'Del', ID: ids }, function(data) {
                    if (data == "OK") {
                        $.messager.alert("Alert", "Delete Successful");
                        LoadDatagrid();
                        //LoadDatagridWOPa();
                        $("#test").datagrid('reload');
                        $("#test").datagrid('clearSelections');
                    } else {
                        $.messager.alert("Alert", "Delete Failed" + data);
                    }
                });
            }
        });
    } else {
        $.messager.alert("Alert", "Please Select Rows Your want to Delete.");
    }
}
function DeleteActionInfoByClick() {
    // Get all selected rows 
    var checkId = $("#test").datagrid('getSelections');
    if (checkId.length == 1) {
        var selectionitem = $("#test").datagrid('getSelections')[0];
        if (GetRowFlag(activeeci, selectionitem.From_ECI, selectionitem.To_ECI) != 1) {

            $.messager.alert("Alert", "No Permission To Delete.");
            return;
        }


        var ids = "";
        for (var i = 0; i < checkId.length; i++) {
            ids += checkId[i].TID + ",";
        }
        ids = ids.substring(0, ids.length - 1);

        $.messager.confirm("Alert", "Are You Sure To Delete？", function(deleteData) {
            if (deleteData) {
                //Post Asynchronous request
                $.post(handlername, { mode: 'Del', ID: ids }, function(data) {
                    if (data == "OK") {
                        $.messager.alert("Alert", "Delete Successful");
                        LoadDatagrid();
                        //LoadDatagridWOPa();
                        $("#test").datagrid('reload');
                        $("#test").datagrid('clearSelections');
                    } else {
                        $.messager.alert("Alert", "Delete Failed" + data);
                    }
                });
            }
        });
    } else {
        $.messager.alert("Alert", "Please Select Only One Row Your want to Delete.");
    }
}

////Init datagrid table (not loading)

function InitDatagrid(queryParame) {
    // Customized Code 
    if (typeof queryParame === "undefined") {
        // use default
        paramdata = { mode: 'Qry', PackageName: currentpackage, pageSize: defaultpagesize, pageNumber: defaultpage };
    } else {
        paramdata = { mode: 'Qry', PackageName: queryParame.PackageName, pageSize: queryParame.pageSize, pageNumber: queryParame.pageNumber };
    }
    ////                            page1: pageNumber,
    //                            size: pageSize

    // for some reason, if put url value here , it will be fired twice
    // better not to put url here , create another loadgrid function
    $('#test').datagrid({
        title: gridtitle,
        iconCls: 'icon-save',
        height: 800,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        fitColumns: true,  //自動適應欄寬
        frozenColumns: [[{ field: 'ck', checkbox: true}]], //顯示核取方塊
        //url: "CRUDPageRegistryHanlder.ashx",
        sortName: sortfieldname, //'PartNo',
        sortOrder: 'asc',
        //striped:true,
        border: true,
        remoteSort: false,
        idField: idfieldname, //'TID',
        pagination: true,
        rownumbers: true,
        queryParams: paramdata,
        columns: tablecolumninputs,
        //[[{ field: 'TID', title: 'TID', width: 60, sortable: true }, { field: 'PartNo', title: 'PartNo', width: 150, sortable: true }, { field: 'Minor', title: 'Minor', width: 150, sortable: true }, { field: 'TMHU_View', title: 'TMHU_View', width: 360, sortable: true }, { field: 'From_ECI', title: 'From_ECI', width: 360, sortable: true }, { field: 'To_ECI', title: 'To_ECI', width: 360, sortable: true}]],
        // GetColumnsInput(),
        toolbar: [{
            id: 'btnadd',
            text: 'Add',
            iconCls: 'icon-add',
            handler: function() {
                //实现弹出添加用户信息的层
                ShowCreateActionInfoDialog();
            }
        }, '-', {
            id: 'btncut',
            text: 'Modify',
            iconCls: 'icon-cut',
            handler: function() {
                //实现弹出修改用户信息的层
                ShowUpdateActionInfoDialog();
            }
        }, '-', {
            id: 'btnsave',
            text: 'Delete',
            iconCls: 'icon-remove',
            handler: function() {
                //确认只删除一条用户信息
                DeleteActionInfoByClick();
            }
}]
        });

        $('#test').datagrid("getPager").pagination(
            {
                pageSize: defaultpagesize, //每页显示的记录条数，默认为10 
                pageList: [10, 15, 25], //可以设置每页记录条数的列表 
                beforePageText: 'Page ', //页数文本框前显示的汉字 
                afterPageText: ' Of Total {pages} Pages',
                displayMsg: 'Current Showing {from} - {to} Record Total {total} Records',
                onSelectPage: function(pageNumber, pageSize) {
                    defaultpage = pageNumber;
                    defaultpagesize = pageSize;
                    queryParame = {
                        mode: 'Qry',
                        PackageName: currentpackage, // need customized
                        pageNumber: pageNumber,
                        pageSize: pageSize
                    };
                    $(this).pagination('loading');
                    // $("#test").datagrid("load", queryParame);
                    LoadDatagrid(queryParame);
                    $(this).pagination('loaded');
                },
                onBeforeRefresh: function() {
                    // alert("call reload grid from db  ?");
                    return true;
                }
            });

        // Customized Code Done 
    }
    ////设置用户搜索权限的方法
    function InitSearch(queryParame) {
        // Customized Code Done 
        if (typeof queryParame === "undefined") {
            // use default
            $("#txtSearchName").val(currentpackage);
        } else {

            $("#txtSearchName").val(queryParame.PackageName);
        }
        $("#btnSerach").click(function() {

            currentpackage = $("#txtSearchName").val();
            queryParame = {
                mode: 'Qry',
                PackageName: $("#txtSearchName").val(),
                pageSize: defaultpagesize,
                pageNumber: defaultpage
            };
            //Refresh
            LoadDatagrid(queryParame);
            return false;
        });
        //        $("#searchbox").searchbox({
        //            searcher: function(value, name) {
        //                currentpackage = value;
        //                queryParame = {
        //                    mode: 'Qry',
        //                    PackageName: value,
        //                    pageSize: defaultpagesize,
        //                    pageNumber: defaultpage
        //                };
        //                //Refresh
        //                LoadDatagrid(queryParame);
        //                return false;
        //            },
        //            prompt: 'Please Input Package Number'
        //        });
        //        // Customized Code Done 

        // Customized Code Done 
    }

    ////Init Add Dialog

    function InitAddDiaglog() {
        // setup validation rule
        // Customized Code
        $("#addpartno1").validatebox({
            required: true,
            validType: 'length[5,5]'
        });

        $("#addpartno2").validatebox({
            required: true,
            validType: 'length[1,1]'
        });

        $("#addpartno3").validatebox({
            required: true,
            validType: 'length[2,2]'
        });

        $("#addMinor").validatebox({
            //required: true,
            validType: 'length[1,1]'
        });

        $("#addDescription").validatebox({
            required: true
        });

        $("#addTMHU_View").validatebox({
            required: true
        });
        $("#addFrom_ECI").validatebox({
            required: true,
            validType: 'length[9,9]'
        });
        $("#addTo_ECI").validatebox({
            // required: true,
            validType: 'length[9,9]'
        });


        $("#btnAddActionInfo").click(function() {
            //首先验证表单是否通过
            //ActionInfoAdd is add diaglog form's id
            //alert(userinfo);
            var validate = $("#ActionInfoAdd").form('validate');
            //ActionInfoAdd is 
            if (validate == false) {
                return false;
            }
            var postData = {
                mode: 'Add',
                PartNo: $("#addpartno1").val() + '-' + $("#addpartno2").val() + currentpackage + '-' + $("#addpartno3").val(),
                Minor: $("#addMinor").val(),
                Description: $("#addDescription").val(),
                Material1: $("#addMaterial1").val(),
                Material2: $("#addMaterial2").val(),
                Comment: $("#addcomment").val(),
                ModFrom: $("#addmodfrom").val(),
                TMHU_View: $("#addTMHU_View").combobox('getValue'),
                DRW: $("#adddrw").combobox('getValue'),
                From_ECI: $("#addFrom_ECI").val(),
                To_ECI: $("#addTo_ECI").val()
            };


            // another way to send ajax post
            $.post(handlername, postData, function(data) {
                if (data == "OK") {
                    $("#AddActionInfoDialog").dialog('close');
                    LoadDatagrid();
                } else {
                    $.messager.alert("Alert", "Add Failed，Please Check:" + data);
                }
            });
            // Customized Code Done
        });
    }

    // Init Updating Dialog

    function InitUpdateDiaglog() {
        // Customized Code 
        // setup validation rule
        $("#modpartno1").validatebox({
            required: true,
            validType: 'length[5,5]'
        });

        $("#modpartno2").validatebox({
            required: true,
            validType: 'length[1,1]'
        });

        $("#modpartno3").validatebox({
            required: true,
            validType: 'length[2,2]'
        });

        $("#modMinor").validatebox({
            // required: true,
            validType: 'length[1,1]'
        });

        $("#modDescription").validatebox({
            required: true
        });

        $("#modMaterial1").validatebox({
            novalidate: true
        });

        $("#modMaterial2").validatebox({
            novalidate: true
        });

        $("#modcomment").validatebox({
            novalidate: true
        });

        $("#modmodfrom").validatebox({
            novalidate: true
        });
        $("#modddldrw").validatebox({

            novalidate: true

        });




        $("#modTMHU_View").validatebox({
            required: true
        });
        $("#modFrom_ECI").validatebox({
            required: true,
            validType: 'length[9,9]'
        });
        $("#modTo_ECI").validatebox({
            // required: true,
            validType: 'length[9,9]'
        });

        $("#btnUpdateActionInfo").click(function() {
            // alert("error when text field readonly");

            //  var validate = $("#ActionInfoUpdate").form('validate');
            var flag = true;
            //  $('#ActionInfoUpdate input."[id^=mod]"').each(function() {
            // validate all input text field
            $('#ActionInfoUpdate input."[id^=mod]"').each(function() {
                if ($(this).attr('readonly') == "readonly") {
                    return;
                }
                if ($(this).attr('disabled') == "disabled") {
                    return;
                }

                if (!$(this).validatebox('isValid')) {
                    flag = false;
                    return;
                }




            });
            // validate select field
            $('#ActionInfoUpdate select."[id^=mod]"').each(function() {
                //                alert(this.id);



                // if any validation



            });
            // validate all dropdownlist
            if (flag == false) {
                return false;
            }

            // At this point, can check if any value changed or not. if not, return false
            if (IsInputsChange() == 0) {

                return false;
            }

           
            var postData = {
                mode: 'Update',
                Tid: $("#modTID").text(),
                PartNo: $("#modpartno1").val() + '-' + $("#modpartno2").val() + currentpackage + '-' + $("#modpartno3").val(),
                Minor: $("#modMinor").val(),
                Description: $("#modDescription").val(),
                Material1: $("#modMaterial1").val(),
                Material2: $("#modMaterial2").val(),
                Comment: $("#modcomment").val(),
                ModFrom: $("#modmodfrom").val(),
                TMHU_View: $("#modTMHU_View").combobox('getValue'),
                DRW: $("#modddldrw").combobox('getValue'),
                From_ECI: $("#modFrom_ECI").val(),
                To_ECI: $("#modTo_ECI").val()
            };
            $.post(handlername, postData, function(data) {
                if (data == "OK") {
                    $("#UpdateActionInfoDialog").dialog('close');
                    LoadDatagrid();
                } else {
                    $.messager.alert("Alert", "Modify Failed，Please check:" + data);
                }
            });
        });
        // Customized Code Done
    }
    //Get Current user role
    function PageInit(uname) {

        //GetCurrentUserRole
        var postData = {
            mode: 'Authorize',
            Uid: uname
        };

        //发送异步请求进行修改信息
        $.post(handlername, postData, function(data) {
            if (data == "Fail") {
                $.messager.alert("Alert", "User Can Not Be Found ! The Current User Role Will Be Guest");
            } else {
                // data is role value
                roleinfo = data;
                //alert(userinfo + " is :" + roleinfo);
                PageProcess();
            }
        });
    }

    // only user role is fetched successfully and continue to display result

    function PageProcess() {

        var temp1 = getURLParam('package');
        if (temp1 != "") {
            currentpackage = temp1;
        }

        var queryParame = {
            PackageName: currentpackage,
            pageSize: defaultpagesize,
            pageNumber: defaultpage
        };


        InitDatagrid(queryParame);
        InitSearch(queryParame);
        InitAddDiaglog();
        InitUpdateDiaglog(queryParame);
        LoadDatagrid(queryParame);


    }


   


 