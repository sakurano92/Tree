<%@ Page Language="C#" MasterPageFile="~/Templates/AppMaster.master" AutoEventWireup="true"
    CodeFile="DeptMgmt.aspx.cs" Inherits="Admin_DeptMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" ID="Panel1" class="PanelSectionHeader">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 10px">
                    &nbsp;
                </td>
                <td class="FormSectionHeader">
                    Department Info
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
                    Department Name :
                </td>
                <td class="FieldValue">
                    <asp:TextBox ID="txtDeptName" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="FieldLabel">
                    Cost Center Code :
                </td>
                <td class="FieldValue">
                    <asp:TextBox ID="txtCostCenterCode" runat="server" Width="200px"></asp:TextBox>
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
            <asp:HiddenField ID="hdnDeptId" runat="server" />
            <asp:Button ID="btnAddDept" runat="server" Text="Add New Department" CommandName="Add"
                OnClick="btnAction_Click" />
            <asp:Button ID="btnSaveDept" runat="server" Text="Save Department" CommandName="Save"
                OnClick="btnAction_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" OnClick="btnAction_Click"
                Visible="false" />
        </div>
        <div style="clear: both">
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="Panel3">
        <div style="width: 100%">
            <asp:SqlDataSource runat="server" ID="SqlDsUserInfo" ConnectionString="<%$ConnectionStrings:dbConString %>"
                SelectCommandType="Text" SelectCommand="SELECT  * FROM Departments Order By DeptName ASC">
            </asp:SqlDataSource>
            <asp:GridView runat="server" ID="gv" ShowHeader="true" EnableViewState="true" DataSourceID="SqlDsUserInfo"
                HeaderStyle-CssClass="CaseRecList_Header" RowStyle-CssClass="CaseRecList_Title"
                AutoGenerateColumns="false" GridLines="Horizontal" AllowPaging="true" AllowSorting="true"
                PagerSettings-Mode="NumericFirstLast" PageSize="15" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="Department ID" DataField="DeptId" ItemStyle-Width="120px"
                        ItemStyle-VerticalAlign="Top" />
                    <asp:BoundField HeaderText="Department Name" DataField="DeptName" ItemStyle-VerticalAlign="Top" />
                    <asp:BoundField HeaderText="Cost Center Code" DataField="CostCenterCode" ItemStyle-Width="150px"
                        ItemStyle-VerticalAlign="Top" />
                    <asp:TemplateField ItemStyle-Width="40px" ItemStyle-VerticalAlign="Top">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditRecord" runat="server" ImageUrl="~/icons/edit.png" CommandName="EditRecord"
                                OnClick="imgbtnAction_Click" CommandArgument='<%#bind("DeptId")%>' CausesValidation="false" />
                            <asp:ImageButton ID="btnDeleteRecord" runat="server" ImageUrl="~/icons/delete.png"
                                CommandName="DeleteRecord" OnClick="imgbtnAction_Click" CommandArgument='<%#bind("DeptId")%>'
                                CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
