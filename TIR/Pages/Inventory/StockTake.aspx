<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true" CodeFile="StockTake.aspx.cs" Inherits="Pages_Report_StockTake" %>

<%@ Register Src="~/Modules/Inventory/StockTake.ascx" TagName="StockTake"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" Runat="Server">
   <uc1:StockTake ID="StockTake" runat="server" />
</asp:Content>
