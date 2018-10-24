<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true" CodeFile="SearchByUser.aspx.cs" Inherits="Pages_Report_searchbyuser" %>

<%@ Register Src="~/Modules/Report/SearchByUser.ascx" TagName="SearchByUser"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" Runat="Server">
   <uc1:SearchByUser ID="SearchByUser" runat="server" />
</asp:Content>
