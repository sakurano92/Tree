<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequisitionToDo.ascx.cs" Inherits="Modules_Report_RequisitionToDo" %>

<%@ Register Src="~/UserControl/grdBind.ascx" TagName="grdPagerTemplate" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


        <asp:HiddenField ID="hidFormmode" runat="server" Value="add" />
        <asp:HiddenField ID="hidSearchKey" runat="server" />
        <asp:Panel runat="server" ID="Panel2" class="PanelSectionHeader">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td class="FormSectionHeader">
                        Report On Requestion To Do</td>
                </tr>
            </table>
        </asp:Panel>
        <div class='section-sep'>
            <asp:Panel runat="server" ID="Panel5" Visible="true" CssClass="pnlManage" DefaultButton="btnSearch">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="6" class="FormSectionHeader">
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Requisitioner:
                        </td>
                        <td class="FieldValue">
                            <asp:TextBox ID="txtRequisitioner" runat="server" Width="300px"></asp:TextBox>
                        </td>
                        <td class="FieldLabel">
                            Fiscal Year:
                        </td>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlFscYr" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="FieldLabel">
                            Requisition Status:
                        </td>
                        <td class="FieldValue">
                            <asp:DropDownList ID="ddlReqStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldLabel">
                            Req Date:<td class="FieldValue">
                            <asp:TextBox runat="server" ID="txtReqdateFrm" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtReqdateFrm"
                                Animated="true" PopupPosition="BottomRight" PopupButtonID="imgbtnReqdatefrm"
                                Format="yyyy-MM-dd"></cc1:CalendarExtender>
                            <asp:ImageButton runat="server" ID="imgbtnReqdatefrm" ImageUrl="~/icons/Calendar.png"
                                ToolTip="Select Req Date From" />
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="ValidateDetail"
                                Style="display: none" ControlToValidate="txtReqdatefrm" ErrorMessage="Date is in incorrect format"
                                OnServerValidate="CustomValidator1_ServerValidate" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CustomValidator1"
                                runat="server"></cc1:ValidatorCalloutExtender>
                            To
                            <asp:TextBox runat="server" ID="txtReqdateTo" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtReqdateTo"
                                Animated="true" PopupPosition="BottomRight" PopupButtonID="imgbtnReqdateTo" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                            <asp:ImageButton runat="server" ID="imgbtnReqdateTo" ImageUrl="~/icons/Calendar.png"
                                ToolTip="Select Req Date" />
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ValidationGroup="ValidateDetail"
                                Style="display: none" ControlToValidate="txtReqdateTo" ErrorMessage="Date is in incorrect format"
                               OnServerValidate="CustomValidator3_ServerValidate" />
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="CustomValidator3"
                                runat="server"></cc1:ValidatorCalloutExtender>
                        </td>
                        </td>
                        
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            <asp:Panel ID="Panel1" runat="server" CssClass="pnlAction">
                                
                                <asp:Button ID="btnexport" runat="server" Text="Export" Style="float: right;" OnClientClick="return false" />
                                <asp:Button ID="btnSearch" runat="server" Text="Search Requisitions" OnClick="btnSearch_Click"
                                    ValidationGroup="ValidateDetail" ToolTip="Search Requisitions" 
                                    Style="float: right;"  />
                                <div style="clear: both">
                                </div>
                            </asp:Panel>
                            <cc1:PopupControlExtender runat="server" TargetControlID="btnexport" PopupControlID="panel3" Position="Center" OffsetX="-207"></cc1:PopupControlExtender>
                            <asp:Panel ID="panel3" runat="server" BackColor="#3a8acf">
                 <asp:CheckBoxList ID="chklist" runat="server">
                 
                     <asp:ListItem Value="bs_requisition.req_id as 'REQUISITION ID'">Req No</asp:ListItem>
                     <asp:ListItem Value="bs_requisition.requisitioner as REQUISITION">Requisitioner</asp:ListItem>
                     <asp:ListItem Value="cast(cast(bs_requisition.req_date as date)as varchar(15)) as 'CREATED DATE'">Created Date</asp:ListItem>
                     <asp:ListItem Value="(bh.bgt_code + ': '+bh.bgt_heading) as 'BUDGET HEADING'">Budgt Heading</asp:ListItem>
                     <asp:ListItem Value="d.req_detail as DETAILS">Details</asp:ListItem>
                     <asp:ListItem Value="d.req_qty as QUANTITY">Quantity</asp:ListItem>
                     <asp:ListItem Value="d.req_rate as 'ESTIMATED COST'">Estimated Cost</asp:ListItem>
                     <asp:ListItem Value="bs_requisition_statuses.req_status as 'REQUISITON STATUS'">Req Status</asp:ListItem>
                     
                     
                     
                     
                          
                     
                 
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
                      <asp:BoundField DataField="req_id" HeaderText="Req No" />
                        <asp:BoundField DataField="requisitioner" HeaderText="Requisitioner" />
                        <asp:BoundField DataField="req_date" HeaderText="Created Date" />
                      <asp:BoundField DataField="heading" HeaderText="Budget Heading" />
                      <asp:BoundField DataField="detail" HeaderText="Details" />
                      <asp:BoundField DataField="quantity" HeaderText="Quantity" />
                      <asp:BoundField DataField="rate" HeaderText="Estimate Cost" />
                        <asp:BoundField DataField="req_status" HeaderText="Req Status" />
                       
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
          
       

