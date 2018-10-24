<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="Signup.aspx.cs" Inherits="Pages_Users" Title="User" %>

<%@ Register Src="~/Modules/UserManagement/UserSignUp.ascx" TagName="userSignUp" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:userSignUp ID="uuserSignUp" runat="server" />
</asp:Content>
