<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="RoleAccess.aspx.cs" Inherits="Pages_RoleAccess" Title="User" %>

<%@ Register Src="~/Modules/UserManagement/RoleAccess.ascx" TagName="RoleAccess"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:RoleAccess ID="RoleAccess1" runat="server" />
</asp:Content>
