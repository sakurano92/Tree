<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_MenuBar.ascx.cs" Inherits="UserControl_UC_MenuBar" %>
<%@ Register Assembly="DevExpress.Web.v8.3, Version=8.3.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<dx:ASPxMenu ID="Menu1" runat="server" AutoSeparators="RootOnly" Width="100%" SelectParentItem="true"
    ItemAutoWidth="false" BackColor="#006699" ForeColor="White" SubMenuItemStyle-BackColor="#006699"
    SubMenuStyle-ForeColor="White" ItemStyle-SelectedStyle-BackColor="#006699" ItemStyle-SelectedStyle-ForeColor="LightGreen"
    ItemStyle-SelectedStyle-Border-BorderColor="#006699" ItemStyle-HoverStyle-BackColor="#0099cc"
    SubMenuItemStyle-HoverStyle-BackColor="#0099cc" SubMenuStyle-Paddings-Padding="0px">
    <Items>
    </Items>
    <LoadingPanelImage Url="~/App_Themes/PlasticBlue/Web/Loading.gif"></LoadingPanelImage>
    <ItemSubMenuOffset FirstItemY="-1" LastItemY="-1" Y="-1" />
    <RootItemSubMenuOffset FirstItemX="1" LastItemX="1" X="1" />
    <SubMenuStyle GutterWidth="0px" />
</dx:ASPxMenu>
<table cellpadding="0" cellspacing="0" width="100%" class="LoginDetail">
    <tr>
        <td style="text-align: right; color: White;">
            <b><asp:Label runat="server" ID="lblLogin"></asp:Label></b>
            <asp:Label runat="server" ID="lblLoginId" Text=""></asp:Label>&nbsp;|&nbsp;
              <asp:Label runat="server" ID="lblLoginTime" Text=""></asp:Label>&nbsp;|&nbsp;
            <asp:Label ID="lblSignout" runat="server" Visible="false"><a href="Signout.aspx">Sign out</a></asp:Label>
        </td>
    </tr>
</table>
