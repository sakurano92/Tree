<%@ Control Language="C#" AutoEventWireup="true" CodeFile="grdBind.ascx.cs" Inherits="CommonModule_grdBind" %>
<br />
<table class="tblpaging" style="width: 100%;">
    <tr style="vertical-align: middle; font-weight: bold" align="left">
        <%--1--%>
        <%--<td>
	        <ASP:LINKBUTTON id="lnkShowAllData" Runat="server" Text="Show All Data" OnCommand="NavigationLink_Click"
				CommandName="All" CausesValidation="False" CssClass="pagingbuttoncolor"  ></ASP:LINKBUTTON>
	        </td>--%>
        <td style="display: none">
            <asp:ImageButton ID="removeBtn" OnClick="removebtnHandler_Click" runat="server" Text="Delete"
                CausesValidation="false" ImageUrl="https://venus.wis.ntu.edu.sg/images/button_delete.gif"
                onmouseover="src='https://venus.wis.ntu.edu.sg/images/button_delete_mouseover.gif'"
                onmouseout="src='https://venus.wis.ntu.edu.sg/images/button_delete.gif'" />
        </td>
        <%--2--%>
        <td style="display: none">
            <asp:ImageButton ID="addbtn" OnClick="addbtnHandler_Click" ImageUrl="https://venus.wis.ntu.edu.sg/images/button_add.gif"
                onmouseover="src='https://venus.wis.ntu.edu.sg/images/button_add_mouseover.gif'"
                onmouseout="src='https://venus.wis.ntu.edu.sg/images/button_add.gif'" runat="server"
                Text="Add" CausesValidation="false" />
        </td>
        <%--3--%>
        <td style="display: none">
            <asp:Label ID="_lblStatus" runat="server" Visible="false" Style="font-size: 9pt;"></asp:Label>
        </td>
        <%--4--%>
        <td align="right" class="grdbindlabel">
            <asp:UpdatePanel ID="updTotalRec" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblTotalRecords" runat="server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <%--5--%>
        <td align="right" style="text-align: right;">
            <asp:LinkButton ID="FirstPage" runat="server" Text="|<< " OnCommand="NavigationLink_Click"
                CommandName="First" CausesValidation="False" CssClass="pagingbuttoncolor"></asp:LinkButton>
        </td>
        <%--6--%>
        <td style="text-align: right;">
            <asp:LinkButton ID="_previousPageLink" runat="server" Text="< " OnCommand="NavigationLink_Click"
                CommandName="Prev" CausesValidation="False" CssClass="pagingbuttoncolor"></asp:LinkButton>
        </td>
        <%--7--%>
        <td align="center" class="grdbindlabel" style="text-align: center;">
            <asp:UpdatePanel runat="server" ID="updPageCount">
                <ContentTemplate>
                    <asp:Label ID="lblPagesCount" runat="Server"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <%--8--%>
        <td align="right">
            <asp:LinkButton ID="_nextPageLink" runat="server" Text=" >" OnCommand="NavigationLink_Click"
                CommandName="Next" CausesValidation="False" CssClass="pagingbuttoncolor"></asp:LinkButton>
        </td>
        <%--9--%>
        <td align="left">
            <asp:LinkButton ID="LastPage" runat="server" OnCommand="NavigationLink_Click" CommandName="Last"
                CausesValidation="False" Text=" >>|" CssClass="pagingbuttoncolor"></asp:LinkButton>
        </td>
         <%--10--%>
        <td align="center">
           <asp:DropDownList ID="ddlNavigation" runat="server"  OnCommand="NavigationLink_Click" CommandName="Page" Visible="false"></asp:DropDownList>
        </td>
        <%--11--%>
        <td align="center">
            <asp:LinkButton ID="lnkBtnExport" runat="server" Text="Export" OnCommand="NavigationLink_Click"
                Visible="false" CommandName="Export" CausesValidation="False" CssClass="pagingbuttoncolor"></asp:LinkButton>
        </td>
    </tr>
</table>
<asp:Button ID="linkPostBack" runat="server" Style="display: none;"></asp:Button>
<asp:Button ID="removePostBackObj" runat="server" Style="display: none;"></asp:Button>
<asp:HiddenField ID="hfSort" runat="server" />
<asp:HiddenField ID="hfSortExpression" runat="server" />
