using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Modules_Report_SearchByUser : System.Web.UI.UserControl
{
   Report rp = new Report();
   pdfconverter pdf = new pdfconverter();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (IsPostBack) return;
       loadddl();
       lbAll_Click();
    }
    protected void btnexport_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       loaduser();
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
          all = "bs_users.user_name as USERNAME,req_time 'REQUISITION TIME',(bs_budget_headings.bgt_code + ': '+bs_budget_headings.bgt_heading) as 'BUDGET HEADING',req_detail AS 'REQUISITION DETAILS',req_qty QUANTITY,req_rate 'ESTIMATED COST',bs_requisition_statuses.req_status as STATUS";
          return all;
       }



    }
    protected void chkbtn_Click(object sender, EventArgs e)
    {
       string colm = "";
       colm = GenerateSelectColumns().ToString();
       DataTable dt = rp.pdfGetAllUser(ddl_user.SelectedValue, txtReqdateFrm.Text,txtReqdateTo.Text, colm);
       string username = ddl_user.SelectedItem.ToString();
       string from = txtReqdateFrm.Text.ToString();
       string to = txtReqdateTo.Text.ToString();
       pdf.pdfsearchbyuser(dt, username,from,to);
    }
    public void loadddl()
   {
      DataManagement dm = new DataManagement();
      dm.LoadDDL(ddl_user, "BS_USERS", "USER_ID", "USER_NAME", true);
      //dm.LoadDDL(ddl_srch_role, "BS_USER_ROLES", "ROLE_ID", "ROLE_NAME",true);
      loaduser();
     
   }
    protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
    {
       DateTime d;
       e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
    }
    protected void CustomValidator2_ServerValidate(object sender, ServerValidateEventArgs e)
    {
       DateTime d;
       e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
    }
    public void loaduser()
    {
       DataTable dt = rp.GetAllUser(ddl_user.SelectedValue,txtReqdateFrm.Text, txtReqdateTo.Text);
       grdPagerTemplate.BindDetails(grdUsers, dt);
    }
    protected void NavigationLink_Click(object sender, CommandEventArgs e)
    {
       DataTable dt = rp.GetAllUser(ddl_user.SelectedValue, txtReqdateFrm.Text, txtReqdateTo.Text);
       grdPagerTemplate.GridViewPager(grdUsers, dt, e);
    }


    protected void btn_xls_Click(object sender, EventArgs e)
    {
       string colm = "";
       colm = GenerateSelectColumns().ToString();
       DataTable dt = rp.pdfGetAllUser(ddl_user.SelectedValue, txtReqdateFrm.Text, txtReqdateTo.Text, colm);
       string username = ddl_user.SelectedItem.ToString();
       string from = txtReqdateFrm.Text.ToString();
       string to = txtReqdateTo.Text.ToString();
       rp.xlssearchbyuser(grdUsers, dt, username, from,to);

    }
    protected void btn_word_Click(object sender, EventArgs e)
    {
       string colm = "";
       colm = GenerateSelectColumns().ToString();
       DataTable dt = rp.pdfGetAllUser(ddl_user.SelectedValue, txtReqdateFrm.Text, txtReqdateTo.Text, colm);
       string username = ddl_user.SelectedItem.ToString();
       string from = txtReqdateFrm.Text.ToString();
       string to = txtReqdateTo.Text.ToString();
       rp.Wssearchbyuser(dt, username, from, to);
    }
    public void lbAll_Click()
    {
       foreach (System.Web.UI.WebControls.ListItem li in chklist.Items)
       {
          li.Selected = true;
       }
    }
}