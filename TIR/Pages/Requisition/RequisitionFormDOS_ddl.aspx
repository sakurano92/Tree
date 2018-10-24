<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionFormDOS_ddl.aspx.cs"
    Inherits="Pages_Requisition_RequisitionFormDOS_ddl" MasterPageFile="~/Templates/AppMaster.master"
    Title="Requisition Form" %>

<%@ Register Src="~/Modules/Requisition/RequisitionForm.ascx" TagName="RequisitionForm"
    TagPrefix="uc1" %>
<%@ Register Src="~/Modules/Requisition/RequisitionView_DOS_ddl.ascx" TagName="RequisitionView"
    TagPrefix="uc2" %>
<%@ Register Src="~/Modules/Requisition/RequisitionManager.ascx" TagName="RequisitionManager"
    TagPrefix="uc3" %>
<%@ Register Src="~/Modules/Requisition/RequisitionApprover_DOS_ddl.ascx" TagName="RequisitionApprover"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <asp:MultiView ID="mvRequisition" runat="server">
        <asp:View ID="vwReqCreate" runat="server">
            <uc1:RequisitionForm ID="ReqCreate" runat="server" />
        </asp:View>
        <asp:View ID="vwReqView" runat="server">
            <uc2:RequisitionView ID="ReqView" runat="server" />
            <asp:Panel ID="pnlButton" runat="server" CssClass="pnlAction">
                <asp:Button ID="btnClose" runat="server" Text="Back" OnClick="ReturnToList" />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vwRMReq" runat="server">
            <uc3:RequisitionManager ID="ReqManage" runat="server" />
        </asp:View>
        <asp:View ID="vwReqApprover" runat="server">
            <uc2:RequisitionView ID="ReqViewApprover" runat="server" />
            <uc4:RequisitionApprover ID="ReqApprover" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
