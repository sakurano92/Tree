<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="forgotPass.aspx.cs" Inherits="Pages_Users" Title="User" %>

<%@ Register Src="~/Modules/UserManagement/fogetPass.ascx" TagName="forgotPass" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:forgotPass ID="fPass" runat="server" />
</asp:Content>
