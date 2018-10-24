<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_SiteFooter.ascx.cs"
    Inherits="UserControl_UC_SiteFooter" %>
<%@ Register Assembly="DevExpress.Web.v8.3, Version=8.3.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v8.3, Version=8.3.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register assembly="DevExpress.Web.v8.3" namespace="DevExpress.Web.ASPxPanel" tagprefix="dxp" %>
<dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" BackColor="#F7F7F7"
    CssFilePath="~/App_Themes/Plastic Blue/{0}/styles.css" CssPostfix="PlasticBlue"
    ShowHeader="False" Height="20px" EnableViewState="False" Font-Names="Trebuchet MS"
    Font-Size="Smaller" ImageFolder="~/App_Themes/Plastic Blue/{0}/">
    <BottomRightCorner Height="2px" Url="~/Images/ASPxRoundPanel/2132298659/BottomRightCorner.png"
        Width="2px" />
    <ContentPaddings PaddingBottom="3px" />
    <NoHeaderTopRightCorner Height="2px" Url="~/Images/ASPxRoundPanel/2132298659/NoHeaderTopRightCorner.png"
        Width="2px" />
    <HeaderRightEdge>
        <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/2132298659/HeaderRightEdge.png"
            Repeat="NoRepeat" VerticalPosition="bottom" HorizontalPosition="right" />
    </HeaderRightEdge>
    <Border BorderStyle="Solid" BorderColor="#CCCCCC" BorderWidth="1px" />
    <HeaderLeftEdge>
        <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/2132298659/HeaderLeftEdge.png"
            Repeat="NoRepeat" VerticalPosition="bottom" HorizontalPosition="left" />
    </HeaderLeftEdge>
    <HeaderStyle BackColor="#E9E9E9">
        <BorderLeft BorderStyle="None" />
        <BorderRight BorderStyle="None" />
        <BorderBottom BorderStyle="None" />
    </HeaderStyle>
    <TopRightCorner Height="2px" Url="~/Images/ASPxRoundPanel/2132298659/TopRightCorner.png"
        Width="2px" />
    <HeaderContent>
        <BackgroundImage ImageUrl="~/Images/ASPxRoundPanel/2132298659/HeaderContent.png"
            Repeat="RepeatX" VerticalPosition="bottom" HorizontalPosition="left" />
    </HeaderContent>
    <NoHeaderTopEdge BackColor="WhiteSmoke">
        <BackgroundImage ImageUrl="~/App_Themes/Plastic Blue/Web/rpNoHeaderTopEdge.gif" />
    </NoHeaderTopEdge>
    <NoHeaderTopLeftCorner Height="2px" Url="~/Images/ASPxRoundPanel/2132298659/NoHeaderTopLeftCorner.png"
        Width="2px" />
    <PanelCollection>
        <dxp:PanelContent ID="pFooter" runat="server" Height="18px" EnableViewState="false">
            <table width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="text-align: left">
                        <b>Copyright
                            <%--<asp:label ID="lblYr" runat="server">--%>
                            2018 The International Restaurant. All rights resererved</b>
                    </td>
                    <td style="text-align: right">
                        <i>Powered by: Tree International Inc.</i>
                    </td>
                </tr>
            </table>
        </dxp:PanelContent>
    </PanelCollection>
    <TopLeftCorner Height="2px" Url="~/Images/ASPxRoundPanel/2132298659/TopLeftCorner.png"
        Width="2px" />
    <BottomLeftCorner Height="2px" Url="~/Images/ASPxRoundPanel/2132298659/BottomLeftCorner.png"
        Width="2px" />
</dxrp:ASPxRoundPanel>
