<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionList.aspx.cs"
    Inherits="Pages_Requisition_RequisitionList" MasterPageFile="~/Templates/AppMaster.master"
    Title="Requisition List" %>

<%@ Register Src="~/Modules/Requisition/RequisitionList.ascx" TagName="RequisitionList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:RequisitionList ID="ReqList1" runat="server" />
</asp:Content>
