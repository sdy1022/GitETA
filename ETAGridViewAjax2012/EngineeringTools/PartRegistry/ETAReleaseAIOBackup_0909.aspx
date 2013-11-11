<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ETAReleaseAIO.aspx.cs" Inherits="EngineeringTools_PartRegistry_ETAReleaseAIO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ETA Release All In One</title>
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/themes/icon.css">
    <link rel="stylesheet" type="text/css" href="http://www.jeasyui.com/easyui/demo/demo.css">

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>

    <script type="text/javascript" src="http://www.jeasyui.com/easyui/jquery.easyui.min.js"></script>

   <%-- <script type="text/javascript" src="http://www.jeasyui.com/easyui/datagrid-detailview.js"></script>--%>

    <script src="../../Scripts/datagrid-detailview.js" type="text/javascript"></script>
</head>
<body>
    <h2>
        Expand row in DataGrid to show subgrid</h2>
    <div class="demo-info" style="margin-bottom: 10px">
        <div class="demo-tip icon-tip">
            &nbsp;</div>
        <div>
            Click the expand button to expand row and view subgrid.</div>
    </div>
    <%--   <table id="dg" style="width: 650px; height: 250px" title="DataGrid - SubGrid" singleselect="true"
        fitcolumns="true">
        <thead>
            <tr>
                <th field="itemid" width="80">
                    Item ID
                </th>
                <th field="productid" width="100">
                    Product ID
                </th>
                <th field="listprice" align="right" width="80">
                    List Price
                </th>
                <th field="unitcost" align="right" width="80">
                    Unit Cost
                </th>
                <th field="attr1" width="220">
                    Attribute
                </th>
                <th field="status" width="60" align="center">
                    Status
                </th>
            </tr>
        </thead>
    </table>--%>
    <table id="dg">
    </table>

    <script type="text/javascript">
        function InitValues() {
        }
        function InitFormAGrid() {
            $('#dg').datagrid({
                title: "Title",
                iconCls: 'icon-save',
                height: 800,
                nowrap: true,
                autoRowHeight: false,
                striped: true,
                collapsible: true,
                fitColumns: true,  //自動適應欄寬
                frozenColumns: [[{ field: 'ck', checkbox: true}]], //顯示核取方塊
                //url: "CRUDPageRegistryHanlder.ashx",
                sortName: 'AItemId', //'PartNo',
                sortOrder: 'asc',
                //striped:true,
                border: true,
                remoteSort: false,
                idField: 'AItemId', //'TID',
                pagination: true,
                rownumbers: true,
                // queryParams: paramdata,
                columns:
                    [[
                        { field: 'AItemId', title: 'AItemId', width: 800 },
                        { field: 'DesignNumber', title: 'DesignNumber', width: 200 },
                         { field: 'Key', title: 'Key', width: 200 },
                        { field: 'ModuleNumber', title: 'ModuleNumber', width: 200 }
                    ]],

                view: detailview,
                detailFormatter: function(index, row) {
                    // debugger;
                    return '<div style="padding:2px"><table id="ddv-' + index + '"></table></div>';
                },
                onExpandRow: function(index, row) {
                    //debugger;
                    //  inputdata = { mode: 'QryFormC', AitemId: row.AItemId, pageSize: 15, pageNumber: 1 };
                    $('#ddv-' + index).datagrid({
                        url: 'ETAReleaseHanlder.ashx?mode=QryFormC&AItemId=' + row.AItemId,
                        //   data: inputdata,
                        fitColumns: true,
                        singleSelect: true,
                        rownumbers: true,
                        loadMsg: '',
                        height: 'auto',
                        columns: [[
            { field: 'AItemId', title: 'AItemId', width: 200 },
            { field: 'KEYCODE', title: 'KEYCODE', width: 100, align: 'right' }
            ]],
                        onResize: function() {
                            $('#dg').datagrid('fixDetailRowHeight', index);
                        },
                        onLoad: function() {
                            alert("onload");
                            debugger;
                        },
                        onLoadSuccess: function() {

                           
                            setTimeout(function() {
                                $('#dg').datagrid('fixDetailRowHeight', index);
                            }, 0);
                        }
                    });
                    $('#dg').datagrid('fixDetailRowHeight', index);
                }
            });
        }


        function LoadFormAGrid() {

            inputdata = { mode: 'QryFormA', PackageName: 'N121', pageSize: 15, pageNumber: 0 };
            $.ajax({
                url: 'ETAReleaseHanlder.ashx', //'CRUDPageRegistryHanlder.ashx',
                type: 'post',
                dataType: 'json',
                data: inputdata, // { size: '10', ItemName: name },
                beforeSend: function() {
                    $('#dg').datagrid("loading"); //遮罩效果
                },
                success: function(json) {
                    $("#dg").datagrid("loadData", json); //加载数据

                    $("#dg").datagrid('reload'); //without this method , refresh will have error
                },
                error: function(data) {
                    alert("Error Happed: " + data.responseText);
                    $("#dg").datagrid('reload');
                }
            });
        }

        $(function() {

            // alert("Init dagrid first");
            //url="ETAReleaseHanlder.ashx"

            InitFormAGrid();
            LoadFormAGrid();

            /*
            $('#dg').datagrid({
            view: detailview,
            detailFormatter: function (index, row) {
            return '<div style="padding:2px"><table id="ddv-' + index + '"></table></div>';
            },
            onExpandRow: function (index, row) {
            $('#ddv-' + index).datagrid({
            url: 'datagrid22_getdetail.php?itemid=' + row.itemid,
            fitColumns: true,
            singleSelect: true,
            rownumbers: true,
            loadMsg: '',
            height: 'auto',
            columns: [[
            { field: 'orderid', title: 'Order ID', width: 200 },
            { field: 'quantity', title: 'Quantity', width: 100, align: 'right' },
            { field: 'unitprice', title: 'Unit Price', width: 100, align: 'right' }
            ]],
            onResize: function () {
            $('#dg').datagrid('fixDetailRowHeight', index);
            },
            onLoadSuccess: function () {
            setTimeout(function () {
            $('#dg').datagrid('fixDetailRowHeight', index);
            }, 0);
            }
            });
            $('#dg').datagrid('fixDetailRowHeight', index);
            }
            });
            */
        });
    </script>

</body>
</html>
