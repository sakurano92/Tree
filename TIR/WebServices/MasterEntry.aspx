<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterEntry.aspx.cs" Inherits="Pages_MasterEntry"
    MasterPageFile="~/Templates/AppMaster.master" Title="The British School" %>

<%@ Register Src="../Modules/MasterData/MasterDataList.ascx" TagName="MasterData"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainBody" runat="Server">
    <uc1:MasterData ID="MasterData1" runat="server" />
</asp:Content>
