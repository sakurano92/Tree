<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true" CodeFile="RequisitionToDo.aspx.cs" Inherits="Pages_Report_RequisitionToDo" %>

<%@ Register Src="~/Modules/Report/RequisitionToDo.ascx" TagName="RequisitionToDo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" Runat="Server">
   <uc1:RequisitionToDo ID="RequisitionToDo" runat="server" />
</asp:Content>


