<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControl/UC_SiteHeader.ascx" TagName="UC_SiteHeader" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/UC_SiteFooter.ascx" TagName="UC_SiteFooter" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>International Restaurant</title>
    <link rel="shortcut icon" href="~/icons/IR.ico" />
    <link href="~/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/Grid.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/igoogle-classic.css" rel="stylesheet" type="text/css" />
    <link href="~/Css/igoogle-common.css" rel="stylesheet" type="text/css" />
    <style>
        .Main
        {
            background-color: #Fafaff;
            margin: 0 auto;
            padding: 0;
            width: 100%;
        }
        .bottom
        {
            bottom: 0;
            float: left;
            margin-top: 5px;
            position: fixed;
            width: 100%;
        }
        #pnlAction input
        {
            border: 1px solid #3366FF;
            color: #333333;
            margin: 10px;
        }
        body
        {
            font-family: Trebuchet MS;
            font-size: 8pt;
            margin: 0;
            min-width: 900px;
        }
        .Main .Header table
        {
            margin: 0 auto;
            width: 1200px;
        }
        #loginbody #Panel2
        {
            width: 300px !important;
            background: #fff;
        }
        #loginbody #Panel1
        {
            display: block;
            height: 20px;
        }
        #btnLogin
        {
            background: none repeat scroll 0 0 #006699 !important;
            border: 1px solid #006699;
            color: #FFFFFF !important;
            cursor: pointer;
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%; overflow-x: hidden;">
    <div class="Main">
        <uc3:UC_SiteHeader runat="server" ID="UC_SiteHeader1" />
        <div id="loginbody" style="border: 1px solid rgb(51, 102, 255); width: 300px; margin: 15% auto 0;">
            <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
                <table cellpadding="0" cellspacing="0" width="100%" style="background-color: Menu;">
                    <tr>
                        <td style="width: 10px">
                            &nbsp;
                        </td>
                        <td class="FormSectionHeader">
                        Tree International: LOGIN
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel2">
                <table cellpadding="0" cellspacing="0" width="300px" align="center">
                    <tr>
                        <td colspan="2" class="FormSectionHeader" style="text-align: center;">
                            <%--<asp:Image runat="server" ImageUrl="~/icons/information.png" ID="imgFormInfo" />--%>
                            &nbsp;<asp:Label runat="server" ID="lblFormInfo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            User Name :
                        </td>
                        <td class="FieldValue" style="width: 220px">
                            <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Password :
                        </td>
                        <td class="FieldValue" style="width: 220px">
                            <asp:TextBox ID="txtUserPassword" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:LinkButton ID="lnkForgetPass" runat="server" ForeColor="Black" Font-Underline="true" PostBackUrl="~/Pages/Users/forgotPass.aspx">Forgot Password?</asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlAction" runat="server">
                                <div style="float: right">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                                </div>
                                <div style="clear: both">
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Not a user? <asp:LinkButton ID="lnkSignup" runat ="server" PostBackUrl="~/Pages/Users/Signup.aspx" ForeColor="Black" Font-Underline="true" Font-Bold="true">Sign up</asp:LinkButton>
                        </td>

                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div class="bottom">
            <uc2:UC_SiteFooter ID="UC_SiteFooter1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
