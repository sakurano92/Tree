<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryItemList.ascx.cs"
    Inherits="Modules_Inventory_InventoryItemList" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="updDetail" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdnItemId" runat="server" />
        <asp:Panel runat="server" ID="Panel2" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        INVENTORY ITEM DETAILS
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel runat="server" ID="pnlItemDetails" CssClass="pnlManage" DefaultButton="btnSave">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="4" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <th class="FieldLabel" style="width: 200px">
                            <font color="red">*</font>Item Code:
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtItemCode" runat="server" Width="150px" />
                            
                            <asp:RequiredFieldValidator ID="rqfItemCode" runat="server" ControlToValidate="txtItemCode"
                                ErrorMessage="Please enter valid Item Code" ValidationGroup="ValidateItem" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rqfItemCode"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <th class="FieldLabel">
                            <font color="red">*</font>Item Name:
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtItemName" runat="server" Width="300px" />
                            <asp:RequiredFieldValidator ID="rqfItemName" runat="server" ControlToValidate="txtItemName"
                                ErrorMessage="Please enter Item Name" ValidationGroup="ValidateItem" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="rqfItemName"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <th class="FieldLabel">
                            <font color="red">*</font>Item Category:
                        </th>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlItemCategory" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th class="FieldLabel">
                            Unit:
                        </th>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlUnit" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th class="FieldLabel">
                            <font color="red">*</font>Cost Price:
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtCostPrice" runat="server" Width="80px" Text="0.00" />
                            <asp:RequiredFieldValidator ID="rqfCostPrice" runat="server" ControlToValidate="txtCostPrice"
                                ErrorMessage="Please enter Cost Price" ValidationGroup="ValidateItem" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rqfCostPrice"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RangeValidator ID="rvCost" runat="server" ControlToValidate="txtCostPrice" ValidationGroup="ValidateItem"
                                MinimumValue="0.00" MaximumValue="99999999" Type="Double" SetFocusOnError="true" Display="None" ErrorMessage="Please enter valid Price"></asp:RangeValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="rvCost"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <th class="FieldLabel">
                            <font color="red">*</font>Minimum Stock:
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtMinStock" runat="server" Width="80px" Text="0.00" />
                            <asp:RequiredFieldValidator ID="rqfMinStock" runat="server" ControlToValidate="txtMinStock"
                                ErrorMessage="Please enter Minimum Stock" ValidationGroup="ValidateItem" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="rqfMinStock"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RangeValidator ID="rvMin" runat="server" ControlToValidate="txtMinStock" ValidationGroup="ValidateItem"
                                MinimumValue="0.00" MaximumValue="99999999" Type="Double" SetFocusOnError="true" Display="None" ErrorMessage="Please enter valid Number"></asp:RangeValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="rvMin"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                     <th class="FieldLabel">
                            <font color="red">*</font>Opening Balance:
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtOpeningBalance" runat="server" Width="80px" Text="0.00" />
                            <asp:RequiredFieldValidator ID="reqOpenBal" runat="server" ControlToValidate="txtOpeningBalance"
                                ErrorMessage="Please enter Cost Price" ValidationGroup="ValidateItem" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="reqOpenBal"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RangeValidator ID="rngBal" runat="server" ControlToValidate="txtOpeningBalance" ValidationGroup="ValidateItem"
                                MinimumValue="0.00" MaximumValue="99999999" Type="Double" SetFocusOnError="true" Display="None" ErrorMessage="Please enter valid Price"></asp:RangeValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="rngBal"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <th class="FieldLabel">
                            Description:
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine" />
                        </td>
                        </tr>
                        <tr>
                        <td colspan="2"></td>
                        <td colspan="2">
                            <asp:Panel ID="pnlAction" runat="server" CssClass="pnlAction">
                                <asp:Button ID="btnSave" runat="server" Text="Save"
                                    OnClick="btnSave_Click"  ValidationGroup="ValidateItem"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Clear" CommandName="cancel" OnClick="btn_OnClick" 
                                    />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        INVENTORY ITEM LIST
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <th class="FieldLabel" style="width: 150px">
                            <font color="red">*</font>Search By Item Category:
                        </th>
                        <td class="FieldValue" style="width: 100px">
                            <asp:DropDownList ID="ddlItemCat" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th class="FieldLabel">
                            Item Code:
                        </th>
                        <td class="FieldValue" style="width: 200px">
                            <asp:TextBox ID="txtCode" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <th class="FieldLabel">
                            Item Name:
                        </th>
                        <td class="FieldValue" style="width: 200px">
                            <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <th class="FieldLabel">
                            Stock:
                        </th>
                        <td class="FieldValue" style="width: 100px">
                            <asp:DropDownList ID="ddlStockBal" runat="server">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="1" Text="Enough Stock"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Low Stock"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Panel ID="pnlSrch" CssClass="pnlAction" runat="server">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btn_OnClick" CommandName="search" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class='section-sep'>
    <uc1:grdPagerTemplate ID="grdPagerTemplate" runat="server" OnNavigationLink="NavigationLink_Click"
        ExportVisible="true" />
    <asp:UpdatePanel ID="updGrid" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="igoogle igoogle-classic" EnableViewState="true" AllowPaging="True"
                PageSize="50" ShowFooter="false" Width="100%" RowHeaderColumn="Inventory Items List">
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="Bottom"
                    PreviousPageText="Previous" Visible="false" />
                <PagerStyle CssClass="Pager-Row" HorizontalAlign="Right" />
                <RowStyle CssClass="data-row" />
                <FooterStyle CssClass="Grid-Footer" />
                <SelectedRowStyle BackColor="#3399FF" />
                <HeaderStyle CssClass="header-row" />
                <AlternatingRowStyle BackColor="#F4F4FF" />
                <EmptyDataTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="CaseRecList_NoRecord">
                                No Record Found
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="SN" HeaderText="SN" ItemStyle-Width="80px">
                        <ItemStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" ItemStyle-Width="150px">
                    </asp:BoundField>
                    <asp:BoundField DataField="item_name" HeaderText="ITEM NAME" ItemStyle-Width="300px">
                    </asp:BoundField>
                    <asp:BoundField DataField="itm_cat_name" HeaderText="CATEGORY" ItemStyle-Width="200px">
                    </asp:BoundField>
                    <asp:BoundField DataField="cost_price" HeaderText="COST PRICE" ItemStyle-Width="100px"
                        ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                        <asp:BoundField DataField="frozen_num" HeaderText="FROZEN QTY" ItemStyle-Width="100px"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="min_stock" HeaderText="MIN STOCK" ItemStyle-Width="100px"
                        ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="last_bal" HeaderText="STOCK BALANCE" ItemStyle-Width="120px"
                        ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                         <asp:BoundField DataField="tot_bal" HeaderText="TOTAL BALANCE" ItemStyle-Width="120px"
                        ItemStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderText="Actions">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#bind("item_id")%>'
                                ImageUrl="~/icons/edit.png" ToolTip="Click here to View Item Details" OnClick="btnEdit_Click" />
                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%#bind("item_id")%>'
                                ImageUrl="~/icons/delete.png" ToolTip="Click here to Delete this Item" OnClick="btnDelete_Click"
                                OnClientClick="return confirm('Are you sure to delete this item?')" />
                        </ItemTemplate>
                        <ItemStyle Width="80px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
