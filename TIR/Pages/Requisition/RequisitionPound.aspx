<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true" CodeFile="RequisitionPound.aspx.cs" Inherits="Pages_Report_RequisitionPound" %>


<%@ Register Src="~/Modules/Requisition/RequisitionPound.ascx" TagName="RequisitionPound"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" Runat="Server">
   <uc1:RequisitionPound ID="RequisitionPound" runat="server" />
</asp:Content>

