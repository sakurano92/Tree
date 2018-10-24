<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exporter.aspx.cs" Inherits="Pages_Print_Exporter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblHeading" runat="server" Font-Bold="true" ForeColor="Blue" Font-Size="Larger"></asp:Label>
        <asp:GridView ID="grdExport" runat="server" Width="100%" ShowFooter="false" AllowPaging="False"
            HeaderStyle-BackColor="#50A7F6" AlternatingRowStyle-BackColor="#F4F4FF">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
