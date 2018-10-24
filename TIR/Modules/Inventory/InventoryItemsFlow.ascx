<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryItemsFlow.ascx.cs"
    Inherits="Modules_Inventory_InventoryItemsFlow" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="updItemFlow" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdnItemId" runat="server" />
        <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        INVENTORY ITEM FLOW
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
                        <th class="FieldLabel" style="width: 200px">
                            <font color="red">*</font> Search By Item Category:
                        </th>
                        <td class="FieldValue" style="width: 300px">
                            <asp:DropDownList ID="ddlItemCat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ItemCatChanged">
                            </asp:DropDownList>
                        </td>
                        <th class="FieldLabel" style="width: 150px">
                            <font color="red">*</font> Item:
                        </th>
                        <td class="FieldValue" style="width: 300px">
                            <asp:DropDownList ID="ddlItem" runat="server" Width="300">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th class="FieldLabel">
                            <font color="red">*</font> Start Date(A.D)
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox runat="server" ID="dtpFromDate" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dtpFromDate"
                                ErrorMessage="*" ValidationGroup="ValidateReq" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpFromDate"
                                Animated="true" PopupPosition="BottomRight" PopupButtonID="imgbtnFromDate" Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                            <asp:ImageButton runat="server" ID="imgbtnFromDate" ImageUrl="~/icons/Calendar.png"
                                ToolTip="Select Requisition Date" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="ValidateData"
                                Display="None" ControlToValidate="dtpFromDate" ErrorMessage="Date is in incorrect format"
                                OnServerValidate="CustomValidator1_ServerValidate" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CustomValidator1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <th class="FieldLabel">
                            <font color="red">*</font> End Date(A.D)
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox runat="server" ID="dtpToDate" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dtpToDate"
                                ErrorMessage="*" ValidationGroup="ValidateReq" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="dtpToDate"
                                Animated="true" PopupPosition="BottomRight" PopupButtonID="imgbtnToDate" Format="yyyy-MM-dd">
                            </cc1:CalendarExtender>
                            <asp:ImageButton runat="server" ID="imgbtnToDate" ImageUrl="~/icons/Calendar.png"
                                ToolTip="Select Requisition Date" />
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ValidationGroup="ValidateData"
                                Display="None" ControlToValidate="dtpToDate" ErrorMessage="Date is in incorrect format"
                                OnServerValidate="CustomValidator1_ServerValidate" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="CustomValidator2"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                CssClass="igoogle igoogle-classic" EnableViewState="true" PagerSettings-Mode="NumericFirstLast"
                PageSize="15" ShowFooter="false" Width="100%" RowHeaderColumn="Inventory Items Flow">
                <PagerSettings FirstPageText="First" LastPageText="Last" NextPageText="Next" Position="Bottom"
                    PreviousPageText="Previous" />
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
                    <asp:BoundField DataField="SN" HeaderText="SN" ItemStyle-Width="80px" />
                    <asp:BoundField DataField="type" HeaderText="Type" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="receiver" HeaderText="Receiver" ItemStyle-Width="300px" />
                    <asp:BoundField DataField="qty" HeaderText="Quantity" ItemStyle-Width="100px" />
                    <asp:BoundField DataField="logtimestamp" HeaderText="Date" ItemStyle-Width="300px"
                        DataFormatString="{0:yyyy-MM-dd hh:mm:ss tt}" />
                    <%--<asp:BoundField DataField="logtimestamp" HeaderText="Date" ItemStyle-Width="300px"/>--%>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
