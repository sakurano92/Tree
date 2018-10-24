using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class Modules_Report_RequisitionToDo : System.Web.UI.UserControl
{
   pdfconverter pdf = new pdfconverter();
   RequisitionMgr reqMgr = new RequisitionMgr();
   UserManagement objUsers = new UserManagement();
   Report rp = new Report();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (IsPostBack) return;
      loadddl();
      lbAll_Click();
   
      
      
    }
    protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
    {
       DateTime d;
       e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
    }

    protected void CustomValidator3_ServerValidate(object sender, ServerValidateEventArgs e)
    {
       DateTime d;
       e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       LoadUsers();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {

    }
    protected void NavigationLink_Click(object sender, CommandEventArgs e)
    {
       DataTable dt = reqMgr.FetchReqReport(ddlFscYr.SelectedValue, ddlReqStatus.SelectedValue, txtRequisitioner.Text, txtReqdateFrm.Text, txtReqdateTo.Text);
       grdPagerTemplate.GridViewPager(grdUsers, dt, e);
    }
    private void ClearFields()
    {
       txtReqdateFrm.Text = "";
       txtReqdateTo.Text = "";
       txtRequisitioner.Text = "";
       ddlFscYr.SelectedIndex = -1;
       ddlReqStatus.SelectedIndex = -1;
    }
    //private void LoadData()
    //{
       
    //   ClearFields();
    //   DataManagement dm = new DataManagement();
    //   dm.LoadDDL(ddlFscYr, "bs_fiscal_years", "fsc_yr_id", "fsc_yr", " fsc_yr_active=1 ", " fsc_yr desc ", false, "");
    //   dm.LoadDDL(ddlReqStatus, "bs_requisition_statuses", "req_status_id", "req_status", " is_req_dtl=0 ", "req_status_id", true, "");
    //   // dm.LoadDDL(ddlReqStatus, "bs_requisition_statuses", "req_status_id", "req_status", " 1=1 ", "req_status_id", true, "");
    //   txtReqdateTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
    //   txtReqdateFrm.Text = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd");
       
    //}
    //public DataTable FetchData()
    //{
    //   DataTable dt = reqMgr.FetchReqReport(ddlFscYr.SelectedValue, ddlReqStatus.SelectedValue, txtRequisitioner.Text, txtReqdateFrm.Text, txtReqdateTo.Text);
    //   if (dt.Rows.Count > 0)
    //      return dt;
    //   return null;
    //}
    private void LoadUsers()
    {
      
       DataTable dt = reqMgr.FetchReqReport(ddlFscYr.SelectedValue, ddlReqStatus.SelectedValue, txtRequisitioner.Text, txtReqdateFrm.Text, txtReqdateTo.Text);
       grdPagerTemplate.BindDetails(grdUsers, dt);

     
    }
   protected void loadddl()
   {
      ClearFields();
      DataManagement dm = new DataManagement();
      dm.LoadDDL(ddlFscYr, "bs_fiscal_years", "fsc_yr_id", "fsc_yr", " fsc_yr_active=1 ", " fsc_yr desc ", false, "");
      dm.LoadDDL(ddlReqStatus, "bs_requisition_statuses", "req_status_id", "req_status", " is_req_dtl=0 ", "req_status_id", true, "");
      // dm.LoadDDL(ddlReqStatus, "bs_requisition_statuses", "req_status_id", "req_status", " 1=1 ", "req_status_id", true, "");
      txtReqdateTo.Text = DateTime.Today.ToString("yyyy-MM-dd");
      txtReqdateFrm.Text = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd");
      LoadUsers();
   }


   public string GenerateSelectColumns()
   {

      string selectStatement = string.Empty;
      foreach (System.Web.UI.WebControls.ListItem li in chklist.Items)
      {
         if (li.Selected)
         {
            selectStatement = selectStatement + li.Value + ",";
         }
      }
      if (selectStatement != "")
      {

         return selectStatement.Substring(0, selectStatement.Length - 1);
      }
      else
      {
         string all = "";
        // all = "bs_requisition.req_id as 'REQUISITION ID',bs_requisition.requisitioner as REQUISITIONER,bs_requisition.req_date as 'CREATED DATE',bs_requisition_statuses.req_status as STATUS";
         all = "bs_requisition.req_id as 'REQUISITION ID',bs_requisition.requisitioner as REQUISITION,cast(cast(bs_requisition.req_date as date)as varchar(15)) as 'CREATED DATE',(bh.bgt_code + ': '+bh.bgt_heading) as 'BUDGET HEADING',d.req_detail as DETAILS,d.req_qty as QUANTITY,d.req_rate as 'ESTIMATED COST',bs_requisition_statuses.req_status as 'REQUISITON STATUS'";
         return all;
      }



   }
   protected void chkbtn_Click(object sender, EventArgs e)
   {
      string colm = "";
      colm = GenerateSelectColumns().ToString();
      DataTable dt = objUsers.topdfreqtodo(ddlFscYr.SelectedValue, ddlReqStatus.SelectedValue, txtRequisitioner.Text, txtReqdateFrm.Text, txtReqdateTo.Text, colm);
      string fiscalyear =ddlFscYr.SelectedItem.ToString();
      string status = ddlReqStatus.SelectedItem.ToString();
      string from = txtReqdateFrm.Text.ToString();
      string to = txtReqdateTo.Text.ToString();
      string requisitioner = txtRequisitioner.Text.ToString();

      //DataTable dt = (DataTable)Session["datatable"];


      // DataRow drs=dt.Select("")
      pdf.ExportDataTableToPdfREQTODO(dt, fiscalyear, status, from, to, requisitioner);

   }
   protected void btn_xls_Click(object sender, EventArgs e)
   {
      string colm = "";
      colm = GenerateSelectColumns().ToString();
      DataTable dt = objUsers.topdfreqtodo(ddlFscYr.SelectedValue, ddlReqStatus.SelectedValue, txtRequisitioner.Text, txtReqdateFrm.Text, txtReqdateTo.Text, colm);
      string fiscalyear = ddlFscYr.SelectedItem.ToString();
      string status = ddlReqStatus.SelectedItem.ToString();
      string from = txtReqdateFrm.Text.ToString();
      string to = txtReqdateTo.Text.ToString();
      string requisitioner = txtRequisitioner.Text.ToString();
      rp.xlstoreqtodo(grdUsers, dt, fiscalyear, status, from, to, requisitioner);
         
   }
   protected void btn_word_Click(object sender, EventArgs e)
   {
      string colm = "";
      colm = GenerateSelectColumns().ToString();
      DataTable dt = objUsers.topdfreqtodo(ddlFscYr.SelectedValue, ddlReqStatus.SelectedValue, txtRequisitioner.Text, txtReqdateFrm.Text, txtReqdateTo.Text, colm);
      string fiscalyear = ddlFscYr.SelectedItem.ToString();
      string status = ddlReqStatus.SelectedItem.ToString();
      string from = txtReqdateFrm.Text.ToString();
      string to = txtReqdateTo.Text.ToString();
      string requisitioner = txtRequisitioner.Text.ToString();
      rp.Wsreqtodo( dt, fiscalyear, status, from, to, requisitioner);
   }
   public void lbAll_Click()
   {
      foreach (System.Web.UI.WebControls.ListItem li in chklist.Items)
      {
         li.Selected = true;
      }
   }
}