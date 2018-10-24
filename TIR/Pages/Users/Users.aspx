<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Pages_Users" Title="User" %>

<%@ Register Src="~/Modules/UserManagement/UserLists.ascx" TagName="UserLists" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:UserLists ID="UserLists1" runat="server" />
</asp:Content>
