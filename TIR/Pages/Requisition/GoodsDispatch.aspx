<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsDispatch.aspx.cs" Inherits="Pages_Requisition_GoodsDispatch"
    MasterPageFile="~/Templates/AppMaster.master" Title="Goods Dispatch" %>

<%@ Register Src="~/Modules/Requisition/GoodsDispatch.ascx" TagName="GoodsDispatch"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:GoodsDispatch ID="GoodsDispatch1" runat="server" />
</asp:Content>
