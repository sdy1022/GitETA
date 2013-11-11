<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BrowseBottom.aspx.cs" Inherits="StdPartsBrowser_BrowseBottom" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <title>Untitled Page</title>
    <link type="text/css" rel="Stylesheet" href="Assets/css/dialog.css" />
    <link type="text/css" rel="Stylesheet" href="Assets/css/pager.css" />
    <link type="text/css" rel="Stylesheet" href="Assets/css/grid.css" />
</head>
</head>
<body>
     <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager2" runat="server">
     </asp:ScriptManager>
        
        <div id="dlg" class="dialog" style="width: 100%">
            <div class="header" style="cursor: default">
                <div class="outer">
                    <div class="inner">
                        <div class="content">
                            <h2>
                               Standardard PartList Items</h2>
                        </div>
                    </div>
                </div>
            </div>
            <div class="body">
                <div class="outer">
                    <div class="inner">
                        <div class="content">
                            
                            <asp:Panel CssClass="grid" ID="pnlCust" runat="server">
                                <asp:UpdatePanel ID="pnlUpdate" runat="server"  >
                                    <ContentTemplate>
                                    
                                         <asp:GridView Width="100%" AllowPaging="false" ID="gvouter" AutoGenerateColumns="False"
                                            runat="server" ShowHeader="False" OnRowCreated="gvouter_RowCreated">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Panel CssClass="group" ID="pnlouter" runat="server" >
                                                            <asp:Image ID="imgCollapsible" CssClass="first" ImageUrl="~/StdPartsBrowser/Assets/img/plus.png" 
                                                                Style="margin-right: 5px;" runat="server" /><span class="header">
                                                                    <%#Eval("PartNo")%>
                                                                    :
                                                                    <%#Eval("Level_")%>
                                                                    (<%#Eval("PartName")%>
                                                                    ) </span>
                                                        </asp:Panel>
                                                         
                                                        <asp:Panel Style="margin-left: 20px; margin-right: 20px" ID="pnlinner" runat="server">
                                                             <asp:GridView AutoGenerateColumns="false" CssClass="grid" ID="gvinner" 
                                                                runat="server" ShowHeader="true" EnableViewState="false">
                                                                <RowStyle CssClass="row" />
                                                                <AlternatingRowStyle CssClass="altrow" />
                                                                <Columns>
                                                                    <asp:TemplateField ItemStyle-CssClass="rownum">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Level" DataField="Level_" ItemStyle-Width="80px" />
                                                                    <asp:BoundField HeaderText="PartNumber" DataField="PartNo" 
                                                                        ItemStyle-Width="100px" />
                                                                   <%-- <asp:BoundField HeaderText="Minor" DataField="Minor" 
                                                                        ItemStyle-Width="110px" />
                                                                    <asp:BoundField HeaderText="Qty" DataField="Qty" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                    <asp:BoundField HeaderText="Drawing" DataField="Dwg" 
                                                                        ItemStyle-Width="100px" />
                                                                          <asp:BoundField HeaderText="Material" DataField="Material" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                           <asp:BoundField HeaderText="Mat'l Size" DataField="PartNo" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                          <asp:BoundField HeaderText="Mat'l Description 1" DataField="PartNo" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                          <asp:BoundField HeaderText="Mat'l Description 2" DataField="PartNo" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                          <asp:BoundField HeaderText="Line No." DataField="PartNo" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                          <asp:BoundField HeaderText="Model Code" DataField="PartNo" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                                                                          <asp:BoundField HeaderText="ECI#" DataField="PartNo" 
                                                                        ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />--%>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                        <cc1:CollapsiblePanelExtender ID="cpe2" runat="Server" TargetControlID="pnlinner"
                                                            CollapsedSize="0" Collapsed="True" ExpandControlID="pnlouter" CollapseControlID="pnlouter"
                                                            AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgCollapsible"
                                                            ExpandedImage="~/StdPartsBrowser/Assets/img/minus.png" CollapsedImage="~/StdPartsBrowser/Assets/img/plus.png"
                                                            ExpandDirection="Vertical" />
                                                            
                                                          
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="footer">
                <div class="outer">
                    <div class="inner">
                        <div class="content">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
