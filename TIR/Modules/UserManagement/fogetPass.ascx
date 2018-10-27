<%@ Control Language="C#" AutoEventWireup="true" CodeFile="fogetPass.ascx.cs" Inherits="Modules_UserManagement_fogetPass" %>
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
                        CHANGE PASSWORD
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
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>Code:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtCode" runat="server" Width="180px" AutoPostBack="true" OnTextChanged="checkCode"></asp:TextBox>
                            <asp:Label ID="lblCodeMsg" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfCode" runat="server" ControlToValidate="txtCode"
                                ErrorMessage="Please enter code yo got in your email" ValidationGroup="ValidateUsers" SetFocusOnError="true"
                                Display="None" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="rfCode"
                                runat="server">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                        
                    
                        
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            <font color="red">*</font>New Password:
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
                <asp:Button ID="btnSave" runat="server" Text="Change Password" OnClick="btnChangePass_Click"
                    ValidationGroup="ValidateUsers" ToolTip="Click here to save user details" />
                <asp:Button ID="btnClear" runat="server" Text="Clear Fields" OnClick="btnClear_Click"
                    ToolTip="Click here to add a new user" />
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btn_Back"
                    ToolTip="Click here to go back" />
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
