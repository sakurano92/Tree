using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public partial class Modules_Report_StockTake : System.Web.UI.UserControl
{
   Report rp = new Report();
   DataManagement dm = new DataManagement();
   ResourceMgr rscMgr = new ResourceMgr();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
       {
          LoadUsers();
       }
    }
    protected void chkbtn_Click(object sender, EventArgs e)
    {
       
    }
    protected void btn_xls_Click(object sender, EventArgs e)
    {

    }
    protected void btn_word_Click(object sender, EventArgs e)
    {

    }
    protected void btnexport_Click(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
       LoadUsers();
    }
    private void LoadUsers()
    {

       DataTable dt = rp.stocktake(src_stock_take.Text,txtReqdateFrm.Text,txtReqdateTo.Text);
       grdUsers.DataSource = dt;
       grdUsers.DataBind();




    }

    
    protected void Button1_Click(object sender, EventArgs e)
    {


       //int rowIndex = 0;

       //for (int i = 0; i < grdUsers.Rows.Count; i++)
       //{

       //   TextBox box = (TextBox)grdUsers.Rows[i].Cells[4].FindControl("TextBox1");

       //   decimal b = Convert.ToDecimal(box.Text.ToString());
       //   rowIndex++;
       //   // con.Open();
       //   string sqlStatement = "update  bs_inventory_items set last_bal =" + b + " where item_id=" + rowIndex + "";
       //   dm.ExecuteNonQuery(sqlStatement);

       //}
       

       foreach (GridViewRow gr in grdUsers.Rows)
       {
          string id = ""; string val = "";
          id = ((HiddenField)gr.FindControl("grdId")).Value;
          val = ((TextBox)gr.FindControl("TextBox1")).Text;
          //decimal b = Convert.ToDecimal(val.ToString());

          rscMgr.upadatakestock(id, val);
          //string sqlStatement = "update  bs_inventory_items set last_bal =" + val + " where item_id=" + id + "";
          //dm.ExecuteNonQuery(sqlStatement);
       }
          

    }
    protected void grdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
       
    }
    protected void grdUsers_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void grdUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
       grdUsers.EditIndex = e.NewEditIndex;
       grdUsers.DataBind();
       
    }
    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowIndex < 0)
          return;
       GridViewRow grdRow = e.Row;
    }
}