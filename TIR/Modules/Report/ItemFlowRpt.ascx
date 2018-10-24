<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemFlowRpt.ascx.cs"
    Inherits="Modules_Inventory_InventoryItemsFlow_Rpt" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 200px;
        height: 42px;
    }
    .style2
    {
        width: 300px;
        height: 42px;
    }
    .style3
    {
        width: 150px;
        height: 42px;
    }
</style>
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
                    
                    </tr>
                    <tr>
                        <th class="FieldLabel">
                            <font color="red">*</font> Start Date(A.D)
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="dtpFromDate" runat="server" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ControlToValidate="dtpFromDate" ErrorMessage="*" 
                                ValidationGroup="ValidateReq" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" 
                                Format="yyyy-MM-dd" PopupButtonID="imgbtnFromDate" PopupPosition="BottomRight" 
                                TargetControlID="dtpFromDate">
                            </cc1:CalendarExtender>
                            <asp:ImageButton ID="imgbtnFromDate" runat="server" 
                                ImageUrl="~/icons/Calendar.png" ToolTip="Select Requisition Date" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" 
                                ControlToValidate="dtpFromDate" Display="None" 
                                ErrorMessage="Date is in incorrect format" 
                                OnServerValidate="CustomValidator1_ServerValidate" 
                                ValidationGroup="ValidateData" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                                TargetControlID="CustomValidator1">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <th class="FieldLabel">
                            <font color="red">*</font> End Date(A.D)
                        </th>
                        <td class="FieldValue">
                            <asp:TextBox ID="dtpToDate" runat="server" Width="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="dtpToDate" ErrorMessage="*" ValidationGroup="ValidateReq" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" 
                                Format="yyyy-MM-dd" PopupButtonID="imgbtnToDate" PopupPosition="BottomRight" 
                                TargetControlID="dtpToDate">
                            </cc1:CalendarExtender>
                            <asp:ImageButton ID="imgbtnToDate" runat="server" 
                                ImageUrl="~/icons/Calendar.png" ToolTip="Select Requisition Date" />
                            <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                ControlToValidate="dtpToDate" Display="None" 
                                ErrorMessage="Date is in incorrect format" 
                                OnServerValidate="CustomValidator1_ServerValidate" 
                                ValidationGroup="ValidateData" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                                TargetControlID="CustomValidator2">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td>
                            <asp:Panel ID="pnlSrch" runat="server" CssClass="pnlAction">
                                <asp:Button ID="btnSearch" runat="server" CommandName="search" 
                                    OnClick="btn_OnClick" Text="Search" />
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
                PageSize="200" ShowFooter="false" Width="100%" RowHeaderColumn="Inventory Items Flow">
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
                    <asp:BoundField DataField="item_name" HeaderText="Items" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="opening_stock" HeaderText="Opening Stock" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="qty_in" HeaderText="Quantity IN" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="amountIn" HeaderText="Amount IN" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="qty_out" HeaderText="Quantity OUT" ItemStyle-Width="300px" />
                    <asp:BoundField DataField="amount_out" HeaderText="Amount OUT" ItemStyle-Width="100px" />
                   
                    <%--<asp:BoundField DataField="logtimestamp" HeaderText="Date" ItemStyle-Width="300px"/>--%>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>
