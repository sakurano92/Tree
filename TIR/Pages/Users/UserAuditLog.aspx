<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="UserAuditLog.aspx.cs" Inherits="Pages_UserAuditLog" Title="User Audit Log" %>

<%@ Register Src="~/Modules/AuditLog/UserAuditLog.ascx" TagName="UserAuditLog" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:UserAuditLog ID="UserAuditLog1" runat="server" />
</asp:Content>
