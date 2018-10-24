<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RoleAccess.ascx.cs" Inherits="Modules_UserManagement_RoleAccess" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>

<asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="FormSectionHeader">
                ROLE ACCESS
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:UpdatePanel runat="server" ID="Panel3">
    <ContentTemplate>
<div class='section-sep'>
    <asp:HiddenField ID="hidFormmode" runat="server" Value="add" />
<asp:HiddenField ID="hidSearchKey" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="2" class="FormSectionHeader">
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <div style="height: 30px;">
                    </div>
                    <asp:RadioButtonList ID="rdbRoles" runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="RoleChange"
                        AutoPostBack="true">
                    </asp:RadioButtonList>
                </td>
                <td>
                    <asp:Panel ID="Panel5" runat="server" CssClass="pnlAction">
                        <asp:Button ID="btnClearAll" runat="server" Text="Clear All" OnClick="ClearAll" ToolTip="Clear All" />
                        <asp:Button ID="btnSelectAll" runat="server" Text="Select All" OnClick="SelectAll"
                            ToolTip="Select All" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="Save" ToolTip="Save" />
                        <asp:Panel ID="pnlChkList" runat="server">
                            <asp:CheckBoxList ID="chkMenus" runat="server">
                            </asp:CheckBoxList>
                        </asp:Panel>
                    </asp:Panel>
                </td>
            </tr>
        </table>
</div>
    </ContentTemplate>
</asp:UpdatePanel>
