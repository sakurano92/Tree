<%@ Page Title="Inventory Items" Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="InventoryItem.aspx.cs" Inherits="Pages_Inventory_InventoryItem" %>

<%@ Register Src="~/Modules/Inventory/InventoryItemList.ascx" TagName="InventoryItemsList"
    TagPrefix="uc1" %>
    
<%--<%@ Register Src="~/Modules/Inventory/InventoryList.ascx" TagName="InventoryItemsList"
    TagPrefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:InventoryItemsList ID="InventoryItemsList1" runat="server" />
</asp:Content>
