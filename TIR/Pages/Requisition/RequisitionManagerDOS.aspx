<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionManagerDOS.aspx.cs" Inherits="Pages_Requisition_RequisitionManagerDOS" MasterPageFile="~/Templates/AppMaster.master"%>


<%@ Register Src="~/Modules/Requisition/RequisitionManagerDOS.ascx" TagName="RequisitionPound"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" Runat="Server">
   <uc1:RequisitionPound ID="RequisitionPound" runat="server" />
</asp:Content>


