<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true" CodeFile="ExpenseDetalis.aspx.cs" Inherits="Pages_Report_ExpenseDetalis" %>

<%@ Register Src="~/Modules/Report/ExpenseDetails.ascx" TagName="ExpenseDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" Runat="Server">
   <uc1:ExpenseDetails ID="ExpenseDetails" runat="server" />
</asp:Content>
