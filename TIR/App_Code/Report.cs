using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

/// <summary>
/// Summary description for Report
/// </summary>
public class Report:DatabaseHelper
{
   private SqlCommand sqlCmd;
	public Report()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    

   public DataTable getbgt(string budgethead)
   {
      sqlCmd=base.GetSqlCommand();
      sqlCmd.CommandType=CommandType.Text;
      sqlCmd.CommandText = @"select bgt_amount,req_frozen_amount,expended_amount,free_amount from vw_budget_status inner join bs_budget_headings on bs_budget_headings.bgt_heading_id=vw_budget_status.bgt_heading_id  where bs_budget_headings.bgt_code='" + budgethead + "' and fsc_yr_id=1";
      return base.GetDataResult(sqlCmd);
   }

   public DataTable getosversion(string userid)
   {
       sqlCmd = base.GetSqlCommand();
       sqlCmd.CommandType = CommandType.Text;
       sqlCmd.CommandText = "select os_version from bs_users where user_id='" + userid + "'";
       return base.GetDataResult(sqlCmd);
   }

   public DataTable GetBudgetHeading(string bgt_cat,string fscl_yr)
   {
       string whereCond = @" FROM vw_budget_status inner join bs_budget_headings on vw_budget_status.bgt_heading_id=bs_budget_headings.bgt_heading_id inner join bs_budget_categories on bs_budget_headings.bgt_cat_id=bs_budget_categories.bgt_cat_id 
       where vw_budget_status.bgt_heading_id>0 ";
       if (bgt_cat != "")
       {
           whereCond += " and bs_budget_headings.bgt_cat_id='" + bgt_cat + "'";
       }
       if (fscl_yr != "")
       {
           whereCond += " and vw_budget_status.fsc_yr_id='" + fscl_yr + "' ";
       }
      sqlCmd = base.GetSqlCommand();
      sqlCmd.CommandType = CommandType.Text;
//      sqlCmd.CommandText = @"
//select bgt_heading,bs_budgets.bgt_amount as bgt_amt,vw_budget_status.bgt_amount as allocated_amt,vw_budget_status.expended_amount as expnde_amt,vw_budget_status.req_frozen_amount as froz_amt,vw_budget_status.free_amount as free_amt,bs_budget_headings.bgt_cat_id,vw_budget_status.fsc_yr_id from bs_budget_headings inner join bs_budgets  on bs_budget_headings.bgt_heading_id=bs_budgets.bgt_heading_id inner join vw_budget_status on vw_budget_status.bgt_heading_id=bs_budgets.bgt_heading_id where 1=1"; 
      sqlCmd.CommandText = "Select * from ";

      sqlCmd.CommandText += @"(SELECT 
      (bs_budget_headings.bgt_code+':'+bs_budget_headings.bgt_heading) as bgt_head,
      bgt_amount,
      req_frozen_amount
      ,claim_frozen_amount
      ,total_frozen_amount
      ,delivered_amount
      ,ordered_amount
      ,paid_claim_amount
      ,total_budget
      ,expended_amount
      ,free_amount
  ";
      sqlCmd.CommandText += whereCond;
      sqlCmd.CommandText += @") first
Union all
 (   
    SELECT 
      'Total' as bgt_head,
      sum(bgt_amount),
      sum(req_frozen_amount)
      ,sum(claim_frozen_amount)
      ,sum(total_frozen_amount)
      ,sum(delivered_amount)
      ,sum(ordered_amount)
      ,sum(paid_claim_amount)
      ,sum(total_budget)
      ,sum(expended_amount)
      ,sum(free_amount)
    "+whereCond +" )";
     // bgt_cat_id='"+bgt_cat+"'and bs_budgets.fsc_yr_id='"+fscl_yr+"'";
      return base.GetDataResult(sqlCmd);
   }
   public DataTable GetBudgetExpense(string bgt_heading_id, string fsc_yr)
   {

      sqlCmd = base.GetSqlCommand();
      sqlCmd.CommandType = CommandType.Text;
      sqlCmd.CommandText = @" SELECT proposed_by,cast(cast(proposed_on as date) as varchar(15)) proposed_on, cast(cast(provided_on as date) as varchar(15)) provided_on,detail,qty,amount,bh.bgt_heading_id from
        ( select requisitioner proposed_by,req_date proposed_on,received_on provided_on,req_detail detail,req_qty qty,req_qty*req_rate amount,bgt_heading_id from
        (
        (select requisitioner,req_id,req_date from bs_requisition where";
      sqlCmd.CommandText += " fsc_yr_id='" + fsc_yr + "' ";
      sqlCmd.CommandText += @") r
        inner join 
        (select req_id,req_detail,req_qty,ISNULL(actual_rate,req_rate) req_rate,received_on,received_by,bgt_heading_id from bs_requisition_details
        where ";
      if (bgt_heading_id != "")
         sqlCmd.CommandText += " bgt_heading_id = '" + bgt_heading_id + @"' and ";
      sqlCmd.CommandText += @" req_dtl_status='13') rd
        on r.req_id=rd.req_id
        )
        UNION ALL";
      sqlCmd.CommandText += @" select claimer proposed_by,claim_date proposed_on,paid_date provided_on,claim_detail detail,claim_qty qty,claim_amount amount,bgt_heading_id from
        (
        (select claimer ,claim_id,claim_date,paid_date  from bs_claim where";
      sqlCmd.CommandText += " fsc_yr_id='" + fsc_yr + "' and paid_by is not null ";
      sqlCmd.CommandText += @") c
        inner join 
        (select claim_id,claim_detail,claim_qty,claim_amount,bgt_heading_id from bs_claim_details
        where 1=1";
      if (bgt_heading_id != "")
         sqlCmd.CommandText += " and bgt_heading_id = '" + bgt_heading_id + "' ";
      sqlCmd.CommandText += @") cd
        on c.claim_id=cd.claim_id
        ))  d
        left join (select bgt_heading_id,bgt_code+':'+bgt_heading heading from bs_budget_headings) bh
        on d.bgt_heading_id=bh.bgt_heading_id";
      return base.GetDataResult(sqlCmd);
   }
   public DataTable GetBudgetExpensetopdf(string bgt_heading_id, string fsc_yr,string colm)
   {

      sqlCmd = base.GetSqlCommand();
      sqlCmd.CommandType = CommandType.Text;
      sqlCmd.CommandText = @" SELECT "+colm+" from ( select requisitioner proposed_by,req_date proposed_on,received_on provided_on,req_detail detail,req_qty qty,req_qty*req_rate amount,bgt_heading_id from (  (select requisitioner,req_id,req_date from bs_requisition where";
      sqlCmd.CommandText += " fsc_yr_id='" + fsc_yr + "' ";
      sqlCmd.CommandText += @") r
        inner join 
        (select req_id,req_detail,req_qty,ISNULL(actual_rate,req_rate) req_rate,received_on,received_by,bgt_heading_id from bs_requisition_details
        where ";
      if (bgt_heading_id != "")
         sqlCmd.CommandText += " bgt_heading_id = '" + bgt_heading_id + @"' and ";
      sqlCmd.CommandText += @" req_dtl_status='13') rd
        on r.req_id=rd.req_id
        )
        UNION ALL";
      sqlCmd.CommandText += @" select claimer proposed_by,claim_date proposed_on,paid_date provided_on,claim_detail detail,claim_qty qty,claim_amount amount,bgt_heading_id from
        (
        (select claimer ,claim_id,claim_date,paid_date  from bs_claim where";
      sqlCmd.CommandText += " fsc_yr_id='" + fsc_yr + "' and paid_by is not null ";
      sqlCmd.CommandText += @") c
        inner join 
        (select claim_id,claim_detail,claim_qty,claim_amount,bgt_heading_id from bs_claim_details
        where 1=1";
      if (bgt_heading_id != "")
         sqlCmd.CommandText += " and bgt_heading_id = '" + bgt_heading_id + "' ";
      sqlCmd.CommandText += @") cd
        on c.claim_id=cd.claim_id
        ))  d
        left join (select bgt_heading_id,bgt_code+':'+bgt_heading heading from bs_budget_headings) bh
        on d.bgt_heading_id=bh.bgt_heading_id";
      return base.GetDataResult(sqlCmd);
   }


   public DataTable GetAllUser(string requsitioner, string from, string to)
   {
      sqlCmd = base.GetSqlCommand();
      sqlCmd.CommandType = CommandType.Text;
      sqlCmd.CommandText = string.Format(@"select bs_requisition.requisitioner as Req,bs_users.user_name as username,bs_users.user_id,bs_requisition_details.bgt_heading_id,(bs_budget_headings.bgt_code + ': '+bs_budget_headings.bgt_heading) as heading,req_time,bs_budget_headings.bgt_code,req_rate,req_detail,bs_requisition_statuses.req_status as status,req_qty,req_dtl_status,bs_requisition_details.req_id from bs_requisition_details right join bs_requisition on  bs_requisition_details.req_id=bs_requisition.req_id inner join bs_requisition_statuses on bs_requisition_details.req_dtl_status=bs_requisition_statuses.req_status_id inner join bs_budget_headings on bs_requisition_details.bgt_heading_id=bs_budget_headings.bgt_heading_id inner join bs_users on bs_users.user_id=bs_requisition.req_created_by where 1=1");
      if (requsitioner != "")
      {
         sqlCmd.CommandText += "and (bs_users.user_id='" + requsitioner + "')";
      }
      if (from!= "" && to!="")
      {
         sqlCmd.CommandText += "and (req_time between '" + from + "' and '" + to + "')";
      }
      sqlCmd.CommandText += "ORDER BY req_time ASC";
      return base.GetDataResult(sqlCmd);

   }

   public DataTable pdfGetAllUser(string usermane, string from, string to,string colm)
   {
      sqlCmd = base.GetSqlCommand();
      sqlCmd.CommandType = CommandType.Text;
      sqlCmd.CommandText = string.Format(@"select "+colm+" from bs_requisition_details right join bs_requisition on  bs_requisition_details.req_id=bs_requisition.req_id inner join bs_requisition_statuses on bs_requisition_details.req_dtl_status=bs_requisition_statuses.req_status_id inner join bs_budget_headings on bs_requisition_details.bgt_heading_id=bs_budget_headings.bgt_heading_id inner join bs_users on bs_users.user_id=bs_requisition.req_created_by where 1=1");
      if (usermane != "")
      {
         sqlCmd.CommandText += "and (bs_users.user_id='" + usermane + "')";
      }
      if (from != "" && to != "")
      {
         sqlCmd.CommandText += "and (req_time between '" + from + "' and '" + to + "')";
      }
      sqlCmd.CommandText += "ORDER BY req_time ASC";
      return base.GetDataResult(sqlCmd);

   }
   //public DataTable GetExpenseByBgtHeading(string bgt_heading_id, string fsc_yr)
   //{
   //   sqlCmd = base.GetSqlCommand();
   //   sqlCmd.CommandType = CommandType.Text;

   //   sqlCmd.CommandText = @"select * from vw_budget_status  where ";
   //   if (bgt_heading_id != "")
   //      sqlCmd.CommandText += " bgt_heading_id = '" + bgt_heading_id + "' and ";
   //   sqlCmd.CommandText += " fsc_yr_id='" + fsc_yr + "'";
   //   return base.GetDataResult(sqlCmd);
   //}

   public DataTable pdfGetBudgetHeading(string bgt_cat, string fscl_yr,string colm)
   {
       string whereCond = @" FROM vw_budget_status inner join bs_budget_headings on vw_budget_status.bgt_heading_id=bs_budget_headings.bgt_heading_id inner join bs_budget_categories on bs_budget_headings.bgt_cat_id=bs_budget_categories.bgt_cat_id 
       where vw_budget_status.bgt_heading_id>0 ";
       if (bgt_cat != "")
       {
           whereCond += " and bs_budget_headings.bgt_cat_id='" + bgt_cat + "'";
       }
       if (fscl_yr != "")
       {
           whereCond += " and vw_budget_status.fsc_yr_id='" + fscl_yr + "' ";
       }
       sqlCmd = base.GetSqlCommand();
       sqlCmd.CommandType = CommandType.Text;
       //      sqlCmd.CommandText = @"
       //select bgt_heading,bs_budgets.bgt_amount as bgt_amt,vw_budget_status.bgt_amount as allocated_amt,vw_budget_status.expended_amount as expnde_amt,vw_budget_status.req_frozen_amount as froz_amt,vw_budget_status.free_amount as free_amt,bs_budget_headings.bgt_cat_id,vw_budget_status.fsc_yr_id from bs_budget_headings inner join bs_budgets  on bs_budget_headings.bgt_heading_id=bs_budgets.bgt_heading_id inner join vw_budget_status on vw_budget_status.bgt_heading_id=bs_budgets.bgt_heading_id where 1=1"; 
       sqlCmd.CommandText = @"Select bgt_heading 'Budget Heading',
      bgt_amount 'Allocated Amount'
      
     
     
     
      
    
   
      ,expended_amount 'Expended Amount'
   ,total_frozen_amount 'Frozen Amount (Total)'
      ,free_amount 'Free Amount' from ";

       sqlCmd.CommandText += @"(SELECT 
      (bs_budget_headings.bgt_code+':'+bs_budget_headings.bgt_heading) as 'bgt_heading',
      bgt_amount 
     
     
     
      
      
      
     
      ,expended_amount 
 ,total_frozen_amount
      ,free_amount 
  ";
       sqlCmd.CommandText += whereCond;
       sqlCmd.CommandText += @") first
Union all
 (   
    SELECT 
      'Total' as bgt_head,
      sum(bgt_amount)
     
     
      
     
 
      
      
      ,sum(expended_amount)
,sum(total_frozen_amount)
      ,sum(free_amount)
    " + whereCond + " )";
       // bgt_cat_id='"+bgt_cat+"'and bs_budgets.fsc_yr_id='"+fscl_yr+"'";
       return base.GetDataResult(sqlCmd);
       //return GetBudgetHeading(bgt_cat, fscl_yr);
//      sqlCmd = base.GetSqlCommand();
//      sqlCmd.CommandType = CommandType.Text;
//      sqlCmd.CommandText = @"
//select " + colm + " from vw_budget_status inner join bs_budget_headings on vw_budget_status.bgt_heading_id=bs_budget_headings.bgt_heading_id inner join bs_budget_categories on bs_budget_headings.bgt_cat_id=bs_budget_categories.bgt_cat_id where bs_budget_headings.bgt_heading_id>0";
//      if (bgt_cat != "")
//      {
//         sqlCmd.CommandText += "and bs_budget_headings.bgt_cat_id='" + bgt_cat + "'";
//      }
//      if (fscl_yr != "")
//      {
//         sqlCmd.CommandText += "and vw_budget_status.fsc_yr_id='" + fscl_yr + "' ";
//      }
//      // bgt_cat_id='"+bgt_cat+"'and bs_budgets.fsc_yr_id='"+fscl_yr+"'";
//      return base.GetDataResult(sqlCmd);
   }

   public DataTable stocktake(string stock,string from,string to)
   {
      sqlCmd = base.GetSqlCommand();
      sqlCmd.CommandType = CommandType.Text;
      sqlCmd.CommandText = @"select * from bs_inventory_items  WHERE 
 ( item_code  LIKE '%' + '"+stock+"' + '%' OR  item_name  LIKE '%' + '"+stock+"' + '%')";
      if (from != "" && to!="")
      {
         sqlCmd.CommandText+="and (created_date between '"+from+"' and '"+to+"') ";
      }
      // bgt_cat_id='"+bgt_cat+"'and bs_budgets.fsc_yr_id='"+fscl_yr+"'";
      return base.GetDataResult(sqlCmd);
   }

   public void xls(GridView GridView_Result,DataTable table,string role,string budghead,string from,string to,string username,string status,string result)
   {
         
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            //HttpContext.Current.Response.ContentType = "application/ms-word";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
           HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
           // HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.doc");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:12.0pt; font-family:Calibri;'>");
            if (role != "")
            { 
            HttpContext.Current.Response.Write("Role:"+role+"<br>");
            }
            if (budghead != "")
            {
               HttpContext.Current.Response.Write("Budget Heading:"+budghead + "<br>");
               
            }
            if (from != "" && to != "")
            {
               HttpContext.Current.Response.Write("From:"+from +"To:"+to+ "<br>");
            }
            if (username != "")
            {
               HttpContext.Current.Response.Write("Username:"+username + "<br>");
            }
            if (status != "")
            {
               HttpContext.Current.Response.Write("Status:" +status + "<br>");
            }
            if (budghead != "")
            {
               HttpContext.Current.Response.Write("" + result + "<br>");            
            }
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
   public void ExportToWord(DataTable dt, string role, string budghead, string from, string to, string username,string status,string result)
   {
      

      //Create a dummy GridView
      GridView GridView1 = new GridView();
      GridView1.AllowPaging = false;
      GridView1.DataSource = dt;
      GridView1.DataBind();

     HttpContext.Current.Response.Clear();
     HttpContext.Current.Response.Buffer = true;
     HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.doc");
     HttpContext.Current.Response.Charset = "";
     HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
     if (role != "")
     { 
     HttpContext.Current.Response.Write("Role:"+role + " ");
     }
     if (budghead != "")
     {
        HttpContext.Current.Response.Write(" "+"Budget Heading:" + budghead + " ");

        
        

     }
     if (from != "" && to != "")
     {
        HttpContext.Current.Response.Write(" "+"From:" + from + "To:" + to + " ");
     }
     if (username != "")
     {
        HttpContext.Current.Response.Write(" "+"Username:" + username + " ");
     }
     if (status != "")
     {
        HttpContext.Current.Response.Write(" " + "Status:" + status + " ");
     }
     if (budghead != "")
     {
        HttpContext.Current.Response.Write(" <br> ");
        HttpContext.Current.Response.Write(" " + result + " ");
     }
      StringWriter sw = new StringWriter();
      HtmlTextWriter hw = new HtmlTextWriter(sw);
      GridView1.RenderControl(hw);
      HttpContext.Current.Response.Output.Write(sw.ToString());
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }

   public void xlsexpensedetails(GridView GridView_Result, DataTable table, string bgt_cat, string fslc_yr)
   {

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.ClearContent();
      HttpContext.Current.Response.ClearHeaders();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.ContentType = "application/ms-excel";
      //HttpContext.Current.Response.ContentType = "application/ms-word";
      HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
      HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
      // HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.doc");
      HttpContext.Current.Response.Charset = "utf-8";
      HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
      HttpContext.Current.Response.Write("<font style='font-size:12.0pt; font-family:Calibri;'>");
      if (bgt_cat != "")
      {
         HttpContext.Current.Response.Write("Budget Category:" + bgt_cat + "<br>");
      }
      if (fslc_yr != "")
      {
         HttpContext.Current.Response.Write("Fiscal Year:" + fslc_yr + "<br>");
      }
     
      HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
      int columnscount = table.Columns.Count;

      for (int j = 0; j < columnscount; j++)
      {
         HttpContext.Current.Response.Write("<Td>");
         HttpContext.Current.Response.Write("<B>");
         HttpContext.Current.Response.Write(table.Columns[j].ToString());
         HttpContext.Current.Response.Write("</B>");
         HttpContext.Current.Response.Write("</Td>");
      }
      HttpContext.Current.Response.Write("</TR>");
      foreach (DataRow row in table.Rows)
      {
         HttpContext.Current.Response.Write("<TR>");
         for (int i = 0; i < table.Columns.Count; i++)
         {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write(row[i].ToString());
            HttpContext.Current.Response.Write("</Td>");
         }

         HttpContext.Current.Response.Write("</TR>");
      }
      HttpContext.Current.Response.Write("</Table>");
      HttpContext.Current.Response.Write("</font>");
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }
   public void Wsexpensedetails(DataTable dt, string bgt_cat, string fslc_yr)
   {


      //Create a dummy GridView
      GridView GridView1 = new GridView();
      GridView1.AllowPaging = false;
      GridView1.DataSource = dt;
      GridView1.DataBind();

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.doc");
      HttpContext.Current.Response.Charset = "";
      HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
      if (bgt_cat != "")
      {
         HttpContext.Current.Response.Write("Budget Category:" + bgt_cat + " ");
      }
      if (fslc_yr != "")
      {
         HttpContext.Current.Response.Write(" " + "Fiscal Year:" + fslc_yr+ " ");
      }
      
      StringWriter sw = new StringWriter();
      HtmlTextWriter hw = new HtmlTextWriter(sw);
      GridView1.RenderControl(hw);
      HttpContext.Current.Response.Output.Write(sw.ToString());
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }
   public void xlsrponbudgetheading(GridView GridView_Result, DataTable table, string bgt_cat, string fslc_yr)
   {

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.ClearContent();
      HttpContext.Current.Response.ClearHeaders();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.ContentType = "application/ms-excel";
      //HttpContext.Current.Response.ContentType = "application/ms-word";
      HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
      HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
      // HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.doc");
      HttpContext.Current.Response.Charset = "utf-8";
      HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
      HttpContext.Current.Response.Write("<font style='font-size:12.0pt; font-family:Calibri;'>");
      if (bgt_cat != "")
      {
         HttpContext.Current.Response.Write("Budget Category:" + bgt_cat + "<br>");
      }
      if (fslc_yr != "")
      {
         HttpContext.Current.Response.Write("Fiscal Year:" + fslc_yr + "<br>");
      }

      HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
      int columnscount = table.Columns.Count;

      for (int j = 0; j < columnscount; j++)
      {
         HttpContext.Current.Response.Write("<Td>");
         HttpContext.Current.Response.Write("<B>");
         HttpContext.Current.Response.Write(table.Columns[j].ToString());
         HttpContext.Current.Response.Write("</B>");
         HttpContext.Current.Response.Write("</Td>");
      }
      HttpContext.Current.Response.Write("</TR>");
      foreach (DataRow row in table.Rows)
      {
         HttpContext.Current.Response.Write("<TR>");
         for (int i = 0; i < table.Columns.Count; i++)
         {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write(row[i].ToString());
            HttpContext.Current.Response.Write("</Td>");
         }

         HttpContext.Current.Response.Write("</TR>");
      }
      HttpContext.Current.Response.Write("</Table>");
      HttpContext.Current.Response.Write("</font>");
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }
   public void xlstoreqtodo(GridView GridView_Result, DataTable table, string role, string bgthead, string from, string to, string username)
   {

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.ClearContent();
      HttpContext.Current.Response.ClearHeaders();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.ContentType = "application/ms-excel";
      //HttpContext.Current.Response.ContentType = "application/ms-word";
      HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
      HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
      // HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.doc");
      HttpContext.Current.Response.Charset = "utf-8";
      HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
      HttpContext.Current.Response.Write("<font style='font-size:12.0pt; font-family:Calibri;'>");
      if (role != "")
      {
         HttpContext.Current.Response.Write("Role:" + role+ "<br>");
      }
      if (bgthead != "")
      {
         HttpContext.Current.Response.Write("Budget Heading:" + bgthead + "<br>");
      }
      if (from != ""&&to!="")
      {
         HttpContext.Current.Response.Write("From:" + from+"To:"+to+ "<br>");
      }
      if (username!= "")
      {
         HttpContext.Current.Response.Write("UserName:" + username + "<br>");
      }

      HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
      int columnscount = table.Columns.Count;

      for (int j = 0; j < columnscount; j++)
      {
         HttpContext.Current.Response.Write("<Td>");
         HttpContext.Current.Response.Write("<B>");
         HttpContext.Current.Response.Write(table.Columns[j].ToString());
         HttpContext.Current.Response.Write("</B>");
         HttpContext.Current.Response.Write("</Td>");
      }
      HttpContext.Current.Response.Write("</TR>");
      foreach (DataRow row in table.Rows)
      {
         HttpContext.Current.Response.Write("<TR>");
         for (int i = 0; i < table.Columns.Count; i++)
         {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write(row[i].ToString());
            HttpContext.Current.Response.Write("</Td>");
         }

         HttpContext.Current.Response.Write("</TR>");
      }
      HttpContext.Current.Response.Write("</Table>");
      HttpContext.Current.Response.Write("</font>");
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }
   public void Wsreqtodo(DataTable dt, string role, string bgthead, string from, string to, string username)
   {


      //Create a dummy GridView
      GridView GridView1 = new GridView();
      GridView1.AllowPaging = false;
      GridView1.DataSource = dt;
      GridView1.DataBind();

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.doc");
      HttpContext.Current.Response.Charset = "";
      HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
      if (role != "")
      {
         HttpContext.Current.Response.Write("Role:" + role + " ");
      }
      if (bgthead != "")
      {
         HttpContext.Current.Response.Write(" " + "Budget Heading:" +bgthead + " ");
      }
      if (from != ""&&to!="")
      {
         HttpContext.Current.Response.Write(" " + "From:" + from + "To:"+to+" ");
      }
      if (username != "")
      {
         HttpContext.Current.Response.Write(" " + "Username:" + username+ " ");
      }

      StringWriter sw = new StringWriter();
      HtmlTextWriter hw = new HtmlTextWriter(sw);
      GridView1.RenderControl(hw);
      HttpContext.Current.Response.Output.Write(sw.ToString());
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }

   public void xlssearchbyuser(GridView GridView_Result, DataTable table, string username, string from,string to)
   {

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.ClearContent();
      HttpContext.Current.Response.ClearHeaders();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.ContentType = "application/ms-excel";
      //HttpContext.Current.Response.ContentType = "application/ms-word";
      HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
      HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
      // HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.doc");
      HttpContext.Current.Response.Charset = "utf-8";
      HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
      HttpContext.Current.Response.Write("<font style='font-size:12.0pt; font-family:Calibri;'>");
      if (username != "")
      {
         HttpContext.Current.Response.Write("User Name:" + username + "<br>");
      }
     
      if (from != "" && to != "")
      {
         HttpContext.Current.Response.Write("From:" + from + "To:" + to + "<br>");
      }
     

      HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
      int columnscount = table.Columns.Count;

      for (int j = 0; j < columnscount; j++)
      {
         HttpContext.Current.Response.Write("<Td>");
         HttpContext.Current.Response.Write("<B>");
         HttpContext.Current.Response.Write(table.Columns[j].ToString());
         HttpContext.Current.Response.Write("</B>");
         HttpContext.Current.Response.Write("</Td>");
      }
      HttpContext.Current.Response.Write("</TR>");
      foreach (DataRow row in table.Rows)
      {
         HttpContext.Current.Response.Write("<TR>");
         for (int i = 0; i < table.Columns.Count; i++)
         {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write(row[i].ToString());
            HttpContext.Current.Response.Write("</Td>");
         }

         HttpContext.Current.Response.Write("</TR>");
      }
      HttpContext.Current.Response.Write("</Table>");
      HttpContext.Current.Response.Write("</font>");
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }
   public void Wssearchbyuser(DataTable dt, string username, string from, string to)
   {


      //Create a dummy GridView
      GridView GridView1 = new GridView();
      GridView1.AllowPaging = false;
      GridView1.DataSource = dt;
      GridView1.DataBind();

      HttpContext.Current.Response.Clear();
      HttpContext.Current.Response.Buffer = true;
      HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.doc");
      HttpContext.Current.Response.Charset = "";
      HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
      if (username != "")
      {
         HttpContext.Current.Response.Write("Role:" + username + " ");
      }
      
      if (from != "" && to != "")
      {
         HttpContext.Current.Response.Write(" " + "From:" + from + "To:" + to + " ");
      }
     

      StringWriter sw = new StringWriter();
      HtmlTextWriter hw = new HtmlTextWriter(sw);
      GridView1.RenderControl(hw);
      HttpContext.Current.Response.Output.Write(sw.ToString());
      HttpContext.Current.Response.Flush();
      HttpContext.Current.Response.End();
   }

   //DataTable stock(string first, string second)
   //{ 
   //}
   }
   //public void ExportTableData(DataTable dtdata)
   //{
   //   string attach = "attachment;filename=Report.xls";
   //   HttpContext.Current.Response.ClearContent();
   //   HttpContext.Current.Response.AddHeader("content-disposition", attach);
   //   HttpContext.Current.Response.ContentType = "application/ms-excel";
   //   if (dtdata != null)
   //   {
   //      foreach (DataColumn dc in dtdata.Columns)
   //      {
   //         HttpContext.Current.Response.Write(dc.ColumnName + "\t");
   //         //sep = ";";
   //      }
   //      HttpContext.Current.Response.Write(System.Environment.NewLine);
   //      foreach (DataRow dr in dtdata.Rows)
   //      {
   //         for (int i = 0; i < dtdata.Columns.Count; i++)
   //         {
   //            HttpContext.Current.Response.Write(dr[i].ToString() + "\t");
   //         }
   //         HttpContext.Current.Response.Write("\n");
   //      }
      

   //      HttpContext.Current.Response.End();
   //   }
   

 


   
  
 
