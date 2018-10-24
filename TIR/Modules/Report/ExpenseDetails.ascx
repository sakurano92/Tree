<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpenseDetails.ascx.cs" Inherits="Modules_Report_ExpenseDetails" %>
<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Panel runat="server" ID="Panel2" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        Report On Expense Details</td>
                </tr>
            </table>
        </asp:Panel>

<div class='section-sep'>
            <asp:Panel runat="server" ID="Panel5" Visible="true" CssClass="pnlManage" DefaultButton="btnSearch">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="8" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Budget Categories:</td>
                        <td class="FieldLabel">
                            <asp:DropDownList ID="ddl_bugt_cat" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddl_bugt_cat_SelectedIndexChanged" 
                                AppendDataBoundItems="true" Width="200px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="FieldLabel">
                            Budget Heading:</td>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddl_bugt_head" runat="server" Width="200px">
                        
                          
                            </asp:DropDownList>
                        </td>
                        <td class="FieldLabel">
                            Fiscal Year:
                        </td>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlFscYr" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="FieldLabel">
                          <asp:Button ID="btnexport0" runat="server" onclick="btnexport_Click" 
                                OnClientClick="return false" Style="float: right;" Text="Export" />
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" 
                                Style="float: right;" Text="Search " ToolTip="Search Requisitions" 
                                ValidationGroup="ValidateDetail" />
                          
                            <cc1:PopupControlExtender ID="btnexport0_PopupControlExtender" runat="server" 
                                OffsetX="-207" PopupControlID="panel3" Position="Center" 
                                TargetControlID="btnexport0"></cc1:PopupControlExtender>
                        </td>
                        <td class="FieldValue">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="FieldLabel" colspan="3">
                            &nbsp;<td class="FieldValue">
                           
                        </td>
                        </td>
                        
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            <asp:Panel ID="Panel1" runat="server" CssClass="pnlAction">
                                
                                <div style="clear: both">
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="panel3" runat="server" BackColor="#3a8acf">
                 <asp:CheckBoxList ID="chklist" runat="server">
                 
                     <asp:ListItem Value="proposed_by as 'PROPOSED BY'">Proposed By</asp:ListItem>
                     
                     <asp:ListItem Value=" detail AS DETAILS">Details</asp:ListItem>
                     <asp:ListItem Value=" qty AS QUANTITY">Quantity</asp:ListItem>
                     <asp:ListItem Value=" amount AS AMOUNT">Total Amount</asp:ListItem>
                     <asp:ListItem Value=" cast(cast(proposed_on as date) as varchar(15)) AS 'PROPOSED ON'">Proposed On</asp:ListItem>
                     <asp:ListItem Value=" cast(cast(provided_on as date) as varchar(15)) AS 'PROVIDED ON'">Provided On</asp:ListItem>
                          
                     
                 
                 </asp:CheckBoxList>
                      <asp:Button ID="chkbtn" runat="server" Text="To PDF" onclick="chkbtn_Click" 
                         Width="55px" />
                         <asp:Button ID="btn_xls" runat="server" Text="To XLS"  
                         Width="55px" onclick="btn_xls_Click" />
                     <asp:Button ID="btn_word" runat="server" Text="To WORD" Width="75px" 
                         onclick="btn_word_Click" />
                 </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div class='section-sep'>
                <uc1:grdPagerTemplate ID="grdPagerTemplate" runat="server" OnNavigationLink="NavigationLink_Click" ExportVisible="False"/>
                <asp:GridView ID="grdUsers" runat="server" CssClass="igoogle igoogle-classic" AutoGenerateColumns="False"
                    Width="100%" ShowFooter="false" AllowPaging="True" AllowSorting="True" PageSize="15"
                    PagerSettings-Mode="NumericFirstLast" EnableViewState="true">
                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" FirstPageText="First"
                        LastPageText="Last" Position="Bottom" Visible="false" />
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
                     <asp:TemplateField HeaderText="SN" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                      <asp:BoundField DataField="proposed_by" HeaderText="Proposed By" />
                       
                      
                        <asp:BoundField DataField="detail" HeaderText="Details" />
                       
                            <asp:BoundField DataField="qty" HeaderText="quantity" />   
                             <asp:BoundField DataField="amount" HeaderText="Amount" />
                              <asp:BoundField DataField="proposed_on" HeaderText="Proposed On" />
                        <asp:BoundField DataField="provided_on" HeaderText="Provided On" />

                     <%--  
                       <asp:TemplateField>
                            <ItemTemplate>
                           <asp:ImageButton ID="btnEditUserInfo" runat="server" ImageUrl="~/icons/edit.png"
                                    OnClick="btnEditUserInfo_Click" CommandArgument='<%#bind("USER_ID")%>' ToolTip="Edit User" />
                                <asp:ImageButton ID="btnDeactivate" runat="server" ImageUrl="~/icons/agent_delete.png"
                                    CommandArgument='<%#bind("USER_ID") %>' OnClick="btnDeactivate_Click" CommandName="Deactivate"
                                    ToolTip="Deactivate User" OnClientClick="return confirm('Are you sure want to deactivate the user?')" />
                                <asp:ImageButton ID="btnActivate" runat="server" ImageUrl="~/icons/agent_add.png"
                                    CommandArgument='<%#bind("USER_ID") %>' OnClick="btnActivate_Click" CommandName="Activate"
                                    ToolTip="Activate User" OnClientClick="return confirm('Are you sure want to activate the user?')" />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/icons/delete.png" CommandArgument='<%#bind("USER_ID") %>'
                                    OnClick="btnDelete_Click" CommandName="Delete" ToolTip="Delete User" OnClientClick="return confirm('Are you sure want to delete the user?')" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </div>