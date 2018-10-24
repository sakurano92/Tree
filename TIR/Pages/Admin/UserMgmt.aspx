<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="UserMgmt.aspx.cs" Inherits="Admin_UserMgmt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxCtl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td class="FormSectionHeader">
                    User Info
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel2">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td colspan="4" class="FormSectionHeader">
                    <asp:Image runat="server" ImageUrl="~/icons/information.png" ID="imgFormInfo" />
                    &nbsp;<asp:Label runat="server" ID="lblFormInfo"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    User ID :
                </td>
                <td class="FieldValue">
                    <asp:TextBox ID="txtUserID" runat="server" Width="170px"></asp:TextBox>
                </td>
                <td class="FieldLabel">
                    User Name :
                </td>
                <td class="FieldValue">
                    <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    Branch :
                </td>
                <td class="FieldValue">
                    <asp:DropDownList runat="server" ID="ddlBranch" DataSourceID="SqlDsBranch" DataTextField="branchname"
                        DataValueField="branchcode">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDsBranch" ConnectionString="<%$ConnectionStrings:dbConString %>"
                        SelectCommandType="Text" SelectCommand="SELECT  branchcode,branchname FROM branchtable ORDER BY branchname">
                    </asp:SqlDataSource>
                </td>
                <td class="FieldLabel">
                    Department :
                </td>
                <td class="FieldValue">
                    <asp:DropDownList runat="server" ID="ddlDepartment" DataSourceID="SqlDsDepartment"
                        DataTextField="DeptName" DataValueField="DeptId">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDsDepartment" ConnectionString="<%$ConnectionStrings:dbConString %>"
                        SelectCommandType="Text" SelectCommand="SELECT  DeptId,DeptName FROM Departments ORDER BY DeptName">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    User Type :
                </td>
                <td class="FieldValue">
                    <asp:DropDownList runat="server" ID="ddlUserType" DataSourceID="SqlDsUserType" DataTextField="OptionValue"
                        DataValueField="OptionValue">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDsUserType" ConnectionString="<%$ConnectionStrings:dbConString %>"
                        SelectCommandType="Text" SelectCommand="SELECT  OptionCaption,OptionValue FROM OptionList WHERE OptionNo=1 ORDER BY OptionNo">
                    </asp:SqlDataSource>
                </td>
                <td class="FieldLabel">
                    Status :
                </td>
                <td class="FieldValue">
                    <asp:DropDownList runat="server" ID="ddlStatus" DataSourceID="SqlDsStatus" DataTextField="OptionValue"
                        DataValueField="OptionValue">
                    </asp:DropDownList>
                    <asp:SqlDataSource runat="server" ID="SqlDsStatus" ConnectionString="<%$ConnectionStrings:dbConString %>"
                        SelectCommandType="Text" SelectCommand="SELECT  OptionCaption,OptionValue FROM OptionList WHERE OptionNo=2 ORDER BY OptionNo">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    Email :
                </td>
                <td class="FieldValue">
                    <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="MessageBar" colspan="4">
                    <asp:Label runat="server" ID="lblCmdMsgError" CssClass="MsgError"></asp:Label>
                    <asp:Label runat="server" ID="lblCmdMsgGeneral" CssClass="MsgGeneral"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlAction" runat="server">
        <div style="float: right">
            <asp:HiddenField ID="hdnOldUserId" runat="server" />
            <asp:Button ID="btnAddUser" runat="server" Text="Add User" CommandName="Add" OnClick="btnAction_Click" />
            <asp:Button ID="btnSaveUser" runat="server" Text="Save User" CommandName="Save" OnClick="btnAction_Click"
                Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" OnClick="btnAction_Click"
                Visible="false" />
        </div>
        <div style="clear: both">
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel3">
        <div style="width: 100%">
            <asp:SqlDataSource runat="server" ID="SqlDsUserInfo" ConnectionString="<%$ConnectionStrings:dbConString %>"
                SelectCommandType="Text" SelectCommand="SELECT  * FROM UserInfo Order By UserName ASC">
            </asp:SqlDataSource>
            <asp:GridView ID="gv" runat="server" DataSourceID="SqlDsUserInfo" CssClass="igoogle igoogle-classic"
                AutoGenerateColumns="False" Width="100%" ShowFooter="false" AllowPaging="True"
                AllowSorting="True" PageSize="15" PagerSettings-Mode="NumericFirstLast" EnableViewState="true">
                <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First"
                    LastPageText="Last" Position="Bottom" />
                <PagerStyle CssClass="Pager-Row" HorizontalAlign="Right" />
                <RowStyle CssClass="data-row" />
                <FooterStyle CssClass="Grid-Footer" />
                <HeaderStyle CssClass="header-row" />
                <AlternatingRowStyle BackColor="#F4F4FF" />
                <EmptyDataTemplate>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="CaseRecList_NoRecord">
                                No Record Found
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="UserID" DataField="UserID" ItemStyle-Width="120px" />
                    <asp:BoundField HeaderText="UserName" DataField="UserName" SortExpression="UserName" />
                    <asp:BoundField HeaderText="UserType" DataField="RoleId" SortExpression="RoleId"
                        ItemStyle-Width="80px" />
                    <asp:BoundField HeaderText="Email" DataField="Email" ItemStyle-Width="170px" />
                    <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-Width="80px" />
                    <asp:TemplateField ItemStyle-Width="40px" ItemStyle-BackColor="WhiteSmoke" ItemStyle-ForeColor="Gray">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditRecord" runat="server" ImageUrl="~/icons/edit.png" CommandName="EditRecord"
                                OnClick="imgbtnAction_Click" CommandArgument='<%#bind("UserID")%>' CausesValidation="false" />
                            <asp:ImageButton ID="btnDeleteRecord" runat="server" ImageUrl="~/icons/delete.png"
                                CommandName="DeleteRecord" OnClick="imgbtnAction_Click" CommandArgument='<%#bind("UserID")%>'
                                CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
