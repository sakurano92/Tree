<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserUtilityBar.ascx.cs"
    Inherits="Modules_UserManagement_UserUtilityBar" %>
<div class='section-sep'>
<table class="tbl" width="100%" border="0" style="border: hidden; clear: both">
    <tr>
        <td width="25%">
            <div style="float: left; padding: 2px 5px 0 0;">
                Search by:&nbsp;
                <asp:TextBox ID="txtSearchKeyword" runat="server" Width="200px">User Id/User Name/Email</asp:TextBox>
                </div>
        </td>
        <td>
            <div class="btn">
                <span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" ToolTip="Click here to search users by User Id/User Name/Email"
                        OnClick="btnSearch_Click" /></span>
            </div>
        </td>
    </tr>
</table>
</div>