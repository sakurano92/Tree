<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserSignUp.ascx.cs" Inherits="Modules_UserManagement_UserSignUp" %>
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
                        USER SIGNUP
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel runat="server" ID="Panel3" CssClass="pnlManage" DefaultButton="btnSave">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>User Name:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtUserId" runat="server" Width="128px" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtUserName" runat="server" Width="180px" AutoPostBack="true" OnTextChanged="checkUser"></asp:TextBox>
                            <asp:Label ID="lblUser" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="rqfUserId" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Please enter username" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="rqfUserId"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        <tr>
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
                            <font color="red">*</font>Phone:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtPhone" runat="server" Width="180px" MaxLength="10" />
                            <asp:RangeValidator ID="rngPhone" runat="server" ControlToValidate="txtPhone" SetFocusOnError="true" MinimumValue="10" MaximumValue="10" ErrorMessage="Phone number must be 10 digits"></asp:RangeValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderrngPhone" TargetControlID="rngPhone"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RequiredFieldValidator ID="rqfPhone" runat="server" ControlToValidate="txtPhone"
                                ErrorMessage="Please enter Phone" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="rqfPhone"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regPhone" runat="server" ControlToValidate="txtPhone"
                                ErrorMessage="InValid Phone" ValidationExpression="^[1-9]+[0-9]+$"
                                ValidationGroup="ValidateUsers" SetFocusOnError="true" Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender54" TargetControlID="regPhone"
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
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rqfEmail"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                            <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="InValid Email format" ValidationExpression="^[a-zA-Z0-9]+([\._]?[a-zA-Z0-9]+)*@[a-zA-Z0-9]+([\.-]?[a-zA-Z]+)?(\.[a-zA-Z]{2,3})+$"
                                ValidationGroup="ValidateUsers" SetFocusOnError="true" Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="regEmail"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    
                        <td class="FieldLabel">
                            <font color="red">*</font>Password:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtPass1" runat="server" TextMode="Password" Width="180px" />
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
    </ContentTemplate>
</asp:UpdatePanel>
