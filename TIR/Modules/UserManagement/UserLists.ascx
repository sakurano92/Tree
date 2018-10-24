<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserLists.ascx.cs" Inherits="Modules_UserManagement_UserLists" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>
<%@ Register Src="~/Modules/UserManagement/UserUtilityBar.ascx" TagName="srchUser"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel runat="server" ID="updPnl">
    <ContentTemplate>
        <asp:HiddenField ID="hidFormmode" runat="server" Value="add" />
        <asp:HiddenField ID="hidSearchKey" runat="server" />
        <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        USERS DETAILS
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel runat="server" ID="Panel3" CssClass="pnlManage" DefaultButton="btnSave">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="4" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>User Name:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtUserId" runat="server" Width="128px" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtUserName" runat="server" Width="128px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rqfUserId" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Please enter username" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="rqfUserId"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="FieldLabel">
                            <font color="red">*</font>Full Name:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtFullName" runat="server" Width="180px" />
                            <asp:RequiredFieldValidator ID="rqfFullName" runat="server" ControlToValidate="txtFullName"
                                ErrorMessage="Please enter Full Name" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" TargetControlID="rqfFullName"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>Email:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtEmail" runat="server" Width="180px" />
                            <asp:RequiredFieldValidator ID="rqfEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Please enter Email" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="rqfEmail"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="InValid Email - Must be format of abc@abc.abc" ValidationExpression="^[a-zA-Z0-9]+([\._]?[a-zA-Z0-9]+)*@[a-zA-Z0-9]+([\.-]?[a-zA-Z]+)?(\.[a-zA-Z]{2,3})+$"
                                ValidationGroup="ValidateUsers" SetFocusOnError="true" Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender54" TargetControlID="revEmail"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <td class="FieldLabel">
                            <font color="red">*</font>Role:
                        </td>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlRoles" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>Status:
                        </td>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="1" Text="Active" />
                                <asp:ListItem Value="2" Text="InActive" />
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>Phone:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtPhone" runat="server" Width="180px" MaxLength="10" />
                            <asp:RequiredFieldValidator ID="rqfPhone" runat="server" ControlToValidate="txtPhone"
                                ErrorMessage="Please enter Phone" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rqfPhone"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regPhone" runat="server" ControlToValidate="txtPhone"
                                ErrorMessage="InValid Phon- Must be 10 digits" ValidationExpression="^[1-9]{1}[0-9]{9}$"
                                ValidationGroup="ValidateUsers" SetFocusOnError="true" Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="regPhone"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>

                        </td>
                    </tr>
                    <td class="FieldLabel">
                            <font color="red">*</font>Password:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtPass1" runat="server" Width="180px" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="rfvpass1" runat="server" ControlToValidate="txtPass1"
                                ErrorMessage="Please enter Password" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderpass1" TargetControlID="rfvpass1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>Confirm Password:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtPass2" runat="server" Width="180px" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="rfvpass2" runat="server" ControlToValidate="txtPass2"
                                ErrorMessage="Please enter Confirmation Password" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderpass2" TargetControlID="rfvpass2"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:CompareValidator runat="server" ID="cmp1" ControlToValidate="txtPass2" ControlToCompare="txtPass1" ErrorMessage="Password donot match!"></asp:CompareValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderpass2comp" TargetControlID="cmp1"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel5" runat="server" CssClass="pnlAction">
                <asp:Button ID="btnSave" runat="server" Text="Save User Data" OnClick="btnSaveData_Click"
                    ValidationGroup="ValidateUsers" ToolTip="Click here to save user details" />
                <asp:Button ID="btnNew" runat="server" Text="Clear Feilds" OnClick="btnAdd_Click"
                    ToolTip="Click here to add a new user" />
            </asp:Panel>
        </div>
        <asp:Panel runat="server" ID="Panel2" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        USERS LISTS
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="Panel4">
            <div class='section-sep'>
                <asp:Panel runat="server" ID="Panel6" DefaultButton="btnSearch">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2" class="FormSectionHeader">
                            </td>
                        </tr>
                        <tr>
                            <td class="FieldLabel">
                                Search By User Name/Full Name/Email:
                            </td>
                            <td class="FieldValue">
                                <asp:TextBox ID="srchText" runat="server" Width="200px"></asp:TextBox>
                            </td>
                            <td class="FieldLabel">
                                Role
                            </td>
                            <td class="FieldValue">
                                <asp:DropDownList ID="srchRole" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="FieldLabel">
                                Status
                            </td>
                            <td class="FieldValue">
                                <asp:DropDownList ID="srchStatus" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                    ToolTip="Search" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div class='section-sep'>
                <uc1:grdPagerTemplate ID="grdPagerTemplate" runat="server" OnNavigationLink="NavigationLink_Click" />
                <asp:GridView ID="grdUsers" runat="server" CssClass="igoogle igoogle-classic" AutoGenerateColumns="False"
                    Width="100%" ShowFooter="false" AllowPaging="True" AllowSorting="True" PageSize="15"
                    PagerSettings-Mode="NumericFirstLast" EnableViewState="true" OnRowDataBound="grdUsers_RowDataBound">
                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First"
                        LastPageText="Last" Position="Bottom" Visible="false" />
                    <PagerStyle CssClass="Pager-Row" HorizontalAlign="Right" />
                    <RowStyle CssClass="data-row" />
                    <FooterStyle CssClass="Grid-Footer" />
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
                        <asp:BoundField DataField="USER_NAME" HeaderText="User Name" />
                        <asp:BoundField DataField="FULL_NAME" HeaderText="Full Name" />
                        <asp:BoundField DataField="EMAIL" HeaderText="Email" />
                        <asp:BoundField DataField="ROLE_NAME" HeaderText="Role" />
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField Value='<%#bind("STATUS_ID") %>' runat="server" ID="hdnStatus" />
                                <asp:ImageButton ID="btnEditUserInfo" runat="server" ImageUrl="~/icons/edit.png"
                                    OnClick="btnEditUserInfo_Click" CommandArgument='<%#bind("USER_ID")%>' ToolTip="Edit User" />
                                <asp:ImageButton ID="btnDeactivate" runat="server" ImageUrl="~/icons/agent_delete.png"
                                    CommandArgument='<%#bind("USER_ID") %>' OnClick="btnDeactivate_Click" CommandName="Deactivate"
                                    ToolTip="Deactivate User" OnClientClick="return confirm('Are you sure want to deactivate the user?')" />
                                <asp:ImageButton ID="btnActivate" runat="server" ImageUrl="~/icons/agent_add.png"
                                    CommandArgument='<%#bind("USER_ID") %>' OnClick="btnActivate_Click" CommandName="Activate"
                                    ToolTip="Activate User" OnClientClick="return confirm('Are you sure want to activate the user?')" />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/icons/delete.png" CommandArgument='<%#bind("USER_ID") %>'
                                    OnClick="btnDelete_Click" CommandName="Delete" ToolTip="Delete User" OnClientClick="return confirm('Are you sure want to delete the user?')" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
