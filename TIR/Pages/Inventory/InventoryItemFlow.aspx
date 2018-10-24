<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="InventoryItemFlow.aspx.cs" Inherits="Pages_Inventory_InventoryItemFlow" %>

<%@ Register Src="~/Modules/Inventory/InventoryItemsFlow.ascx" TagName="InventoryItemsFlow"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:InventoryItemsFlow ID="InventoryItemsFlow1" runat="server" />
</asp:Content>
