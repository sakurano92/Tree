using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;

public partial class Modules_Report_ExpenseDetails : System.Web.UI.UserControl
{
   Report rp = new Report();
   //UserManagement objUsers = new UserManagement();
   pdfconverter pdf = new pdfconverter();
   
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack) 
       { 
          loadddl();
          lbAll_Click();

          
       }
      
       
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       LoadUsers();
    }
    protected void btnexport_Click(object sender, EventArgs e)
    {

    }
    protected void chkbtn_Click(object sender, EventArgs e)
    {
       string colm = "";
       colm = GenerateSelectColumns().ToString();
       DataTable dt = rp.GetBudgetExpensetopdf(ddl_bugt_head.SelectedValue,ddlFscYr.SelectedValue,colm);
     
          string bgt_head = ddl_bugt_cat.SelectedItem.ToString();
       
       string fslc_yr = ddlFscYr.SelectedItem.ToString();

       //DataTable dt = (DataTable)Session["datatable"];


       // DataRow drs=dt.Select("")
       pdf.pdfexpensedetails(dt,bgt_head,fslc_yr);
    }
    private void ClearFields()
    {

       ddlFscYr.SelectedIndex = -1;
       ddl_bugt_cat.SelectedIndex = -1;
    }
    protected void loadddl()
    {
      // ClearFields();
       DataManagement dm = new DataManagement();
       dm.LoadDDL(ddlFscYr, "bs_fiscal_years", "fsc_yr_id", "fsc_yr", " fsc_yr_active=1 ", " fsc_yr desc ", false, "");
       dm.LoadDDL(ddl_bugt_cat, "bs_budget_categories", "bgt_cat_id", "bgt_cat_name");
       LoadUsers();
       //ddl_bugt_head.DataBind();
      
       //LoadUsers();
       
    }
    protected void ddl_bugt_cat_SelectedIndexChanged(object sender, EventArgs e)
    {

       if (ddl_bugt_cat.SelectedValue != "")
       {
      
          int cat_id = 0;
          cat_id = int.Parse(ddl_bugt_cat.SelectedValue.ToString());
          DataTable dt = bugt_head(cat_id);
          ddl_bugt_head.DataSource = dt;
          
          ddl_bugt_head.DataTextField = "bgt_heading";
          ddl_bugt_head.DataValueField = "bgt_heading_id";
          ddl_bugt_head.DataBind();
       }
    }
    public DataTable bugt_head(int cat_id)
    {
       SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
       string sql = "select * from bs_budget_headings where bgt_cat_id=" + cat_id + " and bgt_heading!='STOCK ITEMS'";
       SqlCommand cmd = new SqlCommand(sql, con);
       SqlDataAdapter da = new SqlDataAdapter(cmd);
       DataSet ds = new DataSet();
       da.Fill(ds, "bugt_head");
       return ds.Tables["bugt_head"];

     
    }
    private void LoadUsers()
    {

       DataTable dt = rp.GetBudgetExpense(ddl_bugt_head.SelectedValue, ddlFscYr.SelectedValue);
       grdPagerTemplate.BindDetails(grdUsers, dt);




    }
    protected void NavigationLink_Click(object sender, CommandEventArgs e)
    {
       DataTable dt = rp.GetBudgetHeading(ddl_bugt_head.SelectedValue, ddlFscYr.SelectedValue);
       grdPagerTemplate.GridViewPager(grdUsers, dt, e);
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
          all = " proposed_by AS 'PROPOSED BY', detail AS DETAILS,qty AS QUANTITY,amount AS AMOUNT,cast(cast(proposed_on as date) as varchar(15)) AS 'PROPOSED ON',cast(cast(provided_on as date) as varchar(15)) AS 'PROVIDED ON'";
          return all;
       }



    }
    protected void btn_xls_Click(object sender, EventArgs e)
    {
       string colm = "";
       colm = GenerateSelectColumns().ToString();
       DataTable dt = rp.GetBudgetExpensetopdf(ddl_bugt_head.SelectedValue, ddlFscYr.SelectedValue, colm);

       string bgt_head = ddl_bugt_cat.SelectedItem.ToString();

       string fslc_yr = ddlFscYr.SelectedItem.ToString();

       //DataTable dt = (DataTable)Session["datatable"];


       // DataRow drs=dt.Select("")
       rp.xlsexpensedetails(grdUsers,dt, bgt_head, fslc_yr);
    }
    protected void btn_word_Click(object sender, EventArgs e)
    {
       string colm = "";
       colm = GenerateSelectColumns().ToString();
       DataTable dt = rp.GetBudgetExpensetopdf(ddl_bugt_head.SelectedValue, ddlFscYr.SelectedValue, colm);

       string bgt_head = ddl_bugt_cat.SelectedItem.ToString();

       string fslc_yr = ddlFscYr.SelectedItem.ToString();

       //DataTable dt = (DataTable)Session["datatable"];


       // DataRow drs=dt.Select("")
       rp.Wsexpensedetails(dt, bgt_head, fslc_yr);

    }
  
    public void lbAll_Click()
    {
       foreach (ListItem li in chklist.Items)
       {
          li.Selected = true;
       }
    }


   
}