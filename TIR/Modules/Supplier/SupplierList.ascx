<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierList.ascx.cs"
    Inherits="Modules_Supplier_SupplierList" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:HiddenField ID="hdnSearchKey" runat="server" />
<asp:UpdatePanel ID="updEntry" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="hdnFormMode" runat="server" />
        <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        SUPPLIER DETAIL
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel runat="server" ID="Panel2" CssClass="pnlManage" DefaultButton="btnSave">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="4" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>Supplier Code:
                        </td>
                        <td class="FieldValue">
                            <asp:HiddenField ID="hdnId" runat="server" />
                            <asp:TextBox ID="txtCode" runat="server" Width="180px" />
                            <asp:RequiredFieldValidator ID="rqdCode" runat="server" ControlToValidate="txtCode"
                                ErrorMessage="Please enter valid Code" ValidationGroup="ValidateData" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="rqdCode"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="FieldLabel">
                            <font color="red">*</font>Supplier Name:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtName" runat="server" Width="180px" />
                            <asp:RequiredFieldValidator ID="rqfName" runat="server" ControlToValidate="txtName"
                                ErrorMessage="Please enter Heading" ValidationGroup="ValidateData" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="rqfName"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Address:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtAddress" runat="server" Width="180px" TextMode="MultiLine" />
                        </td>
                        <td class="FieldLabel">
                            Contact Number:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtContact" runat="server" Width="180px" TextMode="Multiline" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Description:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtDetail" runat="server" Width="180px" />
                        </td>
                        <td colspan="2">
                            <asp:Panel ID="pnlAction" runat="server" CssClass="pnlAction">
                                <asp:Button ID="btnSave" runat="server" Text="Save Data" OnClick="btnSaveData_Click"
                                    ValidationGroup="ValidateData" ToolTip="Click here to save details" />
                                <asp:Button ID="btnCLear" runat="server" Text="Clear Form" OnClick="btnAdd_Click"
                                    ToolTip="Click here to add a new data" />
                                <asp:Label ID="lblNotice" runat="server" Text="  *Please Clear Form to Add New Data!!"
                                    Visible="false"></asp:Label>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <br />
        <asp:Panel runat="server" ID="Panel4" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        BUDGET CODE LIST
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel runat="server" ID="Panel5" DefaultButton="btnSearch">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Search By Supplier Name:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="srchName" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td class="FieldLabel">
                            Address:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="srchAddress" runat="server" Width="300px"></asp:TextBox>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                ToolTip="Search" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class='section-sep'>
    <uc1:grdPagerTemplate ID="grdPagerTemplate" runat="server" OnNavigationLink="NavigationLink_Click" ExportVisible="true" />
    <asp:UpdatePanel ID="updGrid" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="Panel3">
                <div style="width: 100%; padding-top: 5px">
                    <asp:GridView ID="grdDatas" runat="server" CssClass="igoogle igoogle-classic" AutoGenerateColumns="False"
                        Width="100%" ShowFooter="false" AllowPaging="True" AllowSorting="True" PageSize="15"
                        PagerSettings-Mode="NumericFirstLast" EnableViewState="true" RowHeaderColumn="Supplier Details">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First"
                            LastPageText="Last" Position="Bottom" Visible="false" />
                        <PagerStyle CssClass="Pager-Row" HorizontalAlign="Right" />
                        <RowStyle CssClass="data-row" />
                        <FooterStyle CssClass="Grid-Footer" />
                        <SelectedRowStyle BackColor="#A0C0E2" ForeColor="#333333" />
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
                            <asp:TemplateField HeaderText="SN" ItemStyle-Width="80px">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="supplier_code" HeaderText="Supplier Code" ItemStyle-Width="80px">
                            </asp:BoundField>
                            <asp:BoundField DataField="supplier_name" HeaderText="Name" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="supplier_address" HeaderText="Address" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="supplier_contact" HeaderText="Contact" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="supplier_detail" HeaderText="Detail" ItemStyle-Width="200px" />
                            <asp:TemplateField ItemStyle-Width="80px" HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/icons/edit.png" OnClick="btnEdit_Click"
                                        CommandArgument='<%#bind("supplier_id")%>' ToolTip="Edit Supplier" />
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/icons/delete.png" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure want to delete the Supplier?')" CommandArgument='<%#bind("supplier_id")%>'
                                        ToolTip="Delete Supplier" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
