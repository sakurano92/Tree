<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionFormPound.aspx.cs"
    Inherits="Pages_Requisition_RequisitionFormPound" MasterPageFile="~/Templates/AppMaster.master"
    Title="Requisition Form" %>

<%@ Register Src="~/Modules/Requisition/RequisitionFormPound.ascx" TagName="RequisitionFormPound"
    TagPrefix="uc1" %>
    <%@ Register Src="~/Modules/Requisition/RequisitionViewPound.ascx" TagName="RequisitionViewPound"
    TagPrefix="uc2" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" runat="Server">
   <%-- <uc1:RequisitionFormPound ID="ReqList1" runat="server" />

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">--%>
    <%--</asp:Content>--%>
    <asp:MultiView ID="mvRequisition" runat="server">
        <asp:View ID="vwReqCreate" runat="server">
            <uc1:RequisitionFormPound ID="ReqList1" runat="server" />
        </asp:View>
       
       
        <asp:View ID="vwReqApprover" runat="server">
            <uc2:RequisitionViewPound ID="ReqViewApprover" runat="server" />
            
        </asp:View>
    </asp:MultiView>

</asp:Content>


