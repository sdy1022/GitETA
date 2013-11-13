<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PartRegistryAIO.aspx.cs"
    Inherits="EngineeringTools_PartRegistry_PartRegistryAIO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>PartRegistry AIO</title>
    <link href="../../Scripts/JqueryUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/JqueryUI/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqueryUI/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JqueryUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JqueryUI/jquery.pagination.js" type="text/javascript"></script>
    <script src="../../Scripts/Site.js" type="text/javascript"></script>
    <script type="text/javascript">
        // page load function
        $(function() {
            //$('[id=mod*]');
            // alert(getURLParam("Package"));
            var role = getURLParam("zzzz");
            activeeci = getURLParam("activeeci");
            if (role != "undefined") {
                if (role == "1") {
                    isadmin = true;
                }
            }
            InitValues();
            SetColumnsInput();
            // Get user info
            userinfo = $("#userinfo").val();
            if (userinfo.length >= 0) {
                PageInit(userinfo);
            }
        });
        function Select1_onclick() {
        }
    </script>

</head>
<body>
    <form runat="server">
    <asp:HiddenField ID="userinfo" runat="server" />
    </form>
    <%-- --- Search Data--%>
    <fieldset id="searchfield">
        <legend>Search</legend>
        <div>
            <label for="txtActionName">
                Package Number：</label>
            <input type="text" id="txtSearchName" name="txtSearchName" />
            <span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span> <a href="#" class="easyui-linkbutton"
                iconcls="icon-search" id="btnSerach" name="btnSerach">Seach</a>
        </div>
    </fieldset>
    <%-- -- using searchbox to search--%>
    <%--<fieldset>
        <legend>Search</legend>
        <div>
            <input id="searchbox" class="easyui-searchbox" style="width: 300px"></input>
        </div>
    </fieldset>--%>
    <%-- -- Display Data--%>
    <div>
        <table id="test">
        </table>
    </div>
    <%-- ----------------------------Add data----------------------------------%>
    <div id="AddActionInfoDialog" class="easyui-dialog" style="width: 360px; height: 500px;
        padding: 10px 20px" closed="true" resizable="true" modal="true" buttons="#dlg-buttons"
        align="center">
        <form id="ActionInfoAdd" method="post" novalidate="novalidate">
        <table id="tblAdd">
            <tr>
                <th colspan="2">
                    Part Registry
                </th>
            </tr>
            <td class="rightalign">
                <label for="PartNo">
                    PartNo：</label>
            </td>
            <td class="leftalign">
                <input style="width: 40px" type="text" id="addpartno1" name="partno" />-
                <input style="width: 10px" type="text" id="addpartno2" name="partno" />
                <input style="width: 30px;" type="text" id="addpackage" name="packageadd" disabled />-
                <input style="width: 20px;" type="text" id="addpartno3" name="partno" />
            </td>
            <tr>
                <td class="rightalign">
                    <label for="Minor">
                        Minor：</label>
                </td>
                <td>
                    <input type="text" id="addMinor" name="minor" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Description">
                        Description：</label>
                </td>
                <td>
                    <input type="text" id="addDescription" name="descrition" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Material1">
                        Material1：</label>
                </td>
                <td>
                    <input type="text" id="addMaterial1" name="addMaterial1" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Material2">
                        Material2：</label>
                </td>
                <td>
                    <input type="text" id="addMaterial2" name="addMaterial2" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Comment">
                        Comment：</label>
                </td>
                <td>
                    <input type="text" id="addcomment" name="addcomment" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="ModFrom">
                        Mod_From：</label>
                </td>
                <td>
                    <input type="text" id="addmodfrom" name="addmodefrom" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="TMHU_View">
                        TMHU_View：</label>
                </td>
                <td class="leftalign">
                    <select id="addTMHU_View" name="TMHU_View" class="easyui-combobox">
                        <option value="1">True</option>
                        <option value="0">False</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="TMHU_View">
                        DRW：</label>
                </td>
                <td class="leftalign">
                    <select id="adddrw" name="addddldrw" class="easyui-combobox">
                        <option value=""></option>
                        <option value="A">A</option>
                        <option value="N">N</option>
                        <option value="S">S</option>
                        <option value="K">K</option>
                        <option value="C">C</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="PartCode">
                        From_ECI：</label>
                </td>
                <td>
                    <input type="text" id="addFrom_ECI" name="from_eci" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Toeci">
                        To_ECI：</label>
                </td>
                <td>
                    <input type="text" id="addTo_ECI" name="To_ECI" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; padding-top: 10px">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="btnAddActionInfo" iconcls="icon-ok">
                        Confirm</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                            onclick="javascript:$('#AddActionInfoDialog').dialog('close')">Close</a>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <%-- ----------------------------Modify data----------------------------------%>
    <div id="UpdateActionInfoDialog" class="easyui-dialog" style="width: 360px; height: 500px;
        padding: 10px 20px" closed="true" resizable="true" modal="true" buttons="#dlg-buttons"
        align="center">
        <form id="ActionInfoUpdate" method="post" novalidate="novalidate">
        <table id="tblUpdate">
            <tr>
                <th colspan="2">
                    <label for="title" id="modFlag">
                    </label>
                </th>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="TID">
                        TID：</label>
                </td>
                <td class="leftalign">
                    <label for="TID" id="modTID">
                    </label>
                </td>
                <td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="PartNo">
                        PartNo：</label>
                </td>
                <td class="leftalign">
                    <input style="width: 40px" type="text" id="modpartno1" name="partno" />-
                    <input style="width: 10px" type="text" id="modpartno2" name="partno" />
                    <input style="width: 30px;" type="text" id="modpackage" name="packagemod" disabled />-
                    <input style="width: 20px;" type="text" id="modpartno3" name="partno" />
                </td>
                <td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Minor">
                        Minor：</label>
                </td>
                <td>
                    <input type="text" id="modMinor" name="minor" />
                </td>
                <td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Description">
                        Description：</label>
                </td>
                <td>
                    <input type="text" id="modDescription" name="minor" />
                </td>
                <td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Material1">
                        Material1：</label>
                </td>
                <td>
                    <input type="text" id="modMaterial1" name="modMaterial1" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Material2">
                        Material2：</label>
                </td>
                <td>
                    <input type="text" id="modMaterial2" name="modMaterial2" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="Comment">
                        Comment：</label>
                </td>
                <td>
                    <input type="text" id="modcomment" name="modcomment" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="ModFrom">
                        Mod_From：</label>
                </td>
                <td>
                    <input type="text" id="modmodfrom" name="modmodfrom" />
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="TMHU_View">
                        TMHU_View：</label>
                </td>
                <td class="leftalign">
                    <select id="modTMHU_View" name="TMHU_View1" class="easyui-combobox">
                        <option value="1">True</option>
                        <option value="0">False</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="TMHU_View">
                        DRW：</label>
                </td>
                <td class="leftalign">
                    <select id="modddldrw" name="modddldrw" class="easyui-combobox" onclick="return Select1_onclick()">
                          <option value=""></option>
                        <option value="A">A</option>
                        <option value="N">N</option>
                        <option value="S">S</option>
                        <option value="K">K</option>
                        <option value="C">C</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="From_ECI">
                        From_ECI：</label>
                </td>
                <td>
                    <input type="text" id="modFrom_ECI" name="minor" />
                </td>
                <td>
            </tr>
            <tr>
                <td class="rightalign">
                    <label for="To_ECI">
                        To_ECI：</label>
                </td>
                <td>
                    <input type="text" id="modTo_ECI" name="minor" />
                </td>
                <td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; padding-top: 10px">
                    <a href="javascript:void(0)" class="easyui-linkbutton" id="btnUpdateActionInfo" iconcls="icon-ok">
                        Confirm</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                            onclick="javascript:$('#UpdateActionInfoDialog').dialog('close')">Close</a>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
