<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ Register Src="~/UserControl/UC_SiteHeader.ascx" TagName="UC_SiteHeader" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/UC_MenuBar.ascx" TagName="UC_SiteMenu" TagPrefix="uc1" %>
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
        <uc1:UC_SiteMenu runat="server" ID="UC_SiteMenu" />
        <div id="loginbody" style="border: 1px solid rgb(51, 102, 255); width: 900px; margin: 15% auto 0;">
            Welcome to International Restaurant
        </div>
        <div class="bottom">
            <uc2:UC_SiteFooter ID="UC_SiteFooter1" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
