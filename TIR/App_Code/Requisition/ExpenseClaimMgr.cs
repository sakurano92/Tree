using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for CLS_InventoryMGMT
/// </summary>
public class ExpenseClaimMgr : DatabaseHelper
{
    private SqlCommand sqlCmd;

    public ExpenseClaimMgr()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// To Check If the User is Creator of Claim or not. 
    /// </summary>
    /// <param name="claim_id"></param>
    /// <returns></returns>
    public bool IsClaimCreator(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select created_by from bs_claim where claim_id='" + claim_id + "'";
        string creator = base.ExecuteScaler(sqlCmd);
        if (creator == SessionValues.UserIdSession)
            return true;
        return false;
    }

    /// <summary>
    /// To get Claimer name. 
    /// </summary>
    /// <param name="claim_id"></param>
    /// <returns></returns>
    public string GetClaimCreator(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"select user_name from ( Select created_by from bs_claim where claim_id='" + claim_id + @"') c
left join bs_users u on u.user_id=c.created_by";
        string creator = base.ExecuteScaler(sqlCmd);
        return creator;
    }


    public string PayClaim_Email(int claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_CLAIM_EMAIL_NOTIFY";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@claimid ", claim_id);

        try
        {
            string ret = sqlCmd.ExecuteScalar().ToString();
            return ret;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    /// <summary>
    /// To Check if requisition proceeded By RM or not
    /// </summary>
    /// <param name="claim_id"></param>
    /// <returns></returns>
    public bool IsOpenClaim(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select claim_status_id from bs_claim where claim_id='" + claim_id + "'";
        string status = base.ExecuteScaler(sqlCmd);
        if (status == "1")
            return true;
        return false;
    }

    /// <summary>
    /// Get List of requisition. 
    /// If Staff, Only Created by themself
    /// If RM All
    /// If Approvers, Proceeded By RM
    /// </summary>
    /// <returns></returns>
    public DataTable FetchClaims()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (SessionValues.IsClaimApprover)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,claim_date desc) as sn,claim_id,claimer,claim_date,cs.claim_status,claim_status_remarks,claim_year,fsc_yr
  FROM bs_claim r 
  left join bs_claim_statuses cs on cs.claim_status_id=r.claim_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id where order by claim_date desc";
        }
        else
        {
            sqlCmd.CommandText = @"  SELECT row_number () over (order by fsc_yr,claim_date desc) as sn,claim_id,claimer,claim_date,cs.claim_status,claim_status_remarks,claim_year,fsc_yr
  FROM bs_claim r 
  left join bs_claim_statuses cs on cs.claim_status_id=r.claim_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id where r.created_by = '" + SessionValues.UserIdSession + "'  order by fsc_yr, claim_date desc";
        }
        return base.GetDataResult(sqlCmd);
    }

    public bool FinallyApproved(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" select accountant_approval from bs_claim 
where claim_id='" + claim_id + "'";

        if (base.ExecuteScaler(sqlCmd).ToString() == "1")
            return true;
        return false;
    }
    public bool IsPaid(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" select claim_status_id from bs_claim 
where claim_id='" + claim_id + "'";

        if (base.ExecuteScaler(sqlCmd).ToString() == "9") // paid == 9 from table claim_status
            return true;
        return false;
    }

   
   

    /// <summary>
    /// Get List of requisition. 
    /// If Staff, Only Created by themself
    /// If RM All
    /// If Approvers, Proceeded By RM
    /// </summary>
    /// <returns></returns>
    public DataTable FetchClaims(string fsc_yr_id, string claim_status_id, string claimer, string fromClaimDate, string toClaimDate, bool ownOnly)
    {
        claimer = claimer.Trim().Replace("'", "''");
        if (fromClaimDate.Trim() == "")
        {
            fromClaimDate = "1900-01-01";
        }
        if (toClaimDate.Trim() == "")
        {
            toClaimDate = "9999-12-31";
        }
        string sql = "Select * from bs_claim ";
        sql += " where fsc_yr_id = '" + fsc_yr_id + "'";
        if (claim_status_id != "")
            sql += " and claim_status_id = '" + claim_status_id + "'";
        if (claimer != "")
            sql += " and claimer like '%" + claimer + "%'";
        if (ownOnly)
        {
            sql += " and claim_status_id < 9";//Closed/paid not in TODO
        }

        sql += " and claim_date between '" + fromClaimDate + "' and '" + toClaimDate + "'";

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (SessionValues.IsClaimApprover)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,claim_date desc) as sn,r.claim_id,claimer,claim_date,cs.ordering,r.claim_status_id,cs.claim_status,claim_status_remarks,claim_year,fsc_yr
  FROM  (" + sql + @") r
  left join bs_claim_statuses cs on cs.claim_status_id=r.claim_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
            if (ownOnly)
            {
                if (SessionValues.RoleIdSession == "6")
                    sqlCmd.CommandText += @" where principal_approval = 0 or (principal_approval=1 and accountant_approval=0) ";
                if (SessionValues.RoleIdSession == "8")
                    sqlCmd.CommandText += @" where principal_approval=1 and accountant_approval!=-1 ";
            }
        }
        else
        {
            sqlCmd.CommandText = @"  SELECT row_number () over (order by fsc_yr,claim_date desc) as sn,claim_id,claimer,claim_date,r.claim_status_id,cs.ordering,cs.claim_status,claim_status_remarks,claim_year,fsc_yr
  FROM (" + sql + " and created_by = '" + SessionValues.UserIdSession + "' " + @" ) r 
  left join bs_claim_statuses cs on cs.claim_status_id=r.claim_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
        }

        sqlCmd.CommandText += " order by ordering,claim_id desc";
        return base.GetDataResult(sqlCmd);
    }

    /// <summary>
    /// Get Claim Info
    /// </summary>
    /// <param name="claim_id"></param>
    /// <returns></returns>
    public DataTable GetClaimData(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT claim_id,claimer,claim_date,cs.claim_status,claim_status_remarks,claim_year,c.claim_status_id,
c.fsc_yr_id,c.created_by,u.full_name,no_invoice_reason,rm_comment,principal_cmt,accountant_cmt,rm_approval,principal_approval,accountant_approval
  FROM bs_claim c left join bs_claim_statuses cs 
  on cs.claim_status_id=c.claim_status_id
left join bs_users u 
  on c.created_by= u.user_id where claim_id='" + claim_id + "'";
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetClaimDetails(string claim_id)
    {
        string whereCond = " where 1=1 ";
        if (claim_id != null && claim_id != "")
            whereCond += " and claim_id='" + claim_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = string.Format(@"  SELECT claim_dtl_id,cd.claim_id,bh.bgt_code,(bh.bgt_code+': '+bh.bgt_heading) heading,cd.bgt_heading_id,
claim_detail,claim_qty,claim_amount,cd.rm_comment
,claim_time,total_budget,total_frozen_amount
        FROM (select * from bs_claim_details {0} ) cd
        left join bs_claim c on c.claim_id=cd.claim_id
        left join bs_budget_headings bh on bh.bgt_heading_id=cd.bgt_heading_id
        left join vw_budget_status bs on bs.bgt_heading_id=cd.bgt_heading_id and bs.fsc_yr_id=c.fsc_yr_id ", whereCond);
        return base.GetDataResult(sqlCmd);
    }
    public string GetClaimTotal(string claim_id)
    {
        string whereCond = " where 1=1 ";
        if (claim_id != null && claim_id != "")
            whereCond += " and claim_id='" + claim_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = string.Format(@"  SELECT sum(claim_amount) from bs_claim_details {0}", whereCond);
        return base.ExecuteScaler(sqlCmd);
    }

    public DataTable GetClaimDetailsTemp()
    {
        string whereCond = " where added_by='" + SessionValues.UserIdSession + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = string.Format(@"  SELECT claim_dtl_id,bh.bgt_code,(bh.bgt_code+': '+bh.bgt_heading) heading,claim_detail,claim_qty,claim_amount
        FROM (select * from bs_claim_details_temp {0} ) cd
        left join bs_budget_headings bh on bh.bgt_heading_id=cd.bgt_heading_id", whereCond);
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetClaimDetailTemp(string claim_dtl_id)
    {
        string whereCond = " where 1=1 ";
        if (claim_dtl_id != null && claim_dtl_id != "")
            whereCond += " and claim_dtl_id='" + claim_dtl_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select * from bs_claim_details_temp " + whereCond;
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetClaimDetail(string claim_dtl_id)
    {
        string whereCond = " where 1=1 ";
        if (claim_dtl_id != null && claim_dtl_id != "")
            whereCond += " and claim_dtl_id='" + claim_dtl_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select * from bs_claim_details " + whereCond;
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetBudgetCodes()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select bgt_heading_id,bgt_heading,bgt_code,(bgt_code + ': '+ bgt_heading) heading from bs_budget_headings where bgt_heading_id!=0";
        return base.GetDataResult(sqlCmd);
    }

    public void LoadBgtCodes(DropDownList ddl, bool addEmpty)
    {
        ddl.DataTextField = "heading";
        ddl.DataValueField = "bgt_heading_id";
        DataTable tblData = GetBudgetCodes();
        tblData.DefaultView.Sort = "bgt_heading_id";

        if (addEmpty)
        {
            DataRow row = tblData.NewRow();
            row[1] = "";
            tblData.Rows.InsertAt(row, 0);
            ddl.DataSource = tblData;
        }
        else
            ddl.DataSource = tblData;

        ddl.DataBind();
    }

    /// <summary>
    /// Add/Edit Claimusiiton By Creator
    /// </summary>
    /// <param name="claim_id"></param>
    /// <param name="claimer"></param>
    /// <param name="claim_date"></param>
    /// <param name="claim_year"></param>
    /// <param name="fsc_yr_id"></param>
    /// <returns></returns>

    public string SaveClaim(string claim_id, string claimer, string claim_date, string claim_year,
        string fsc_yr_id,string inv_attached, string no_invoice_reason)
    {
        if (claim_id == "") claim_id = "0";
        //claimer = claimer.Trim().Replace("'", "''");
        //claim_date = claim_date.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_CLAIM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_DATE", Convert.ToDateTime(claim_date).Date);
        sqlCmd.Parameters.Add("@CLAIMER", claimer);
        sqlCmd.Parameters.Add("@CLAIM_YEAR", claim_year);
        sqlCmd.Parameters.Add("@CLAIM_ID", claim_id);
        sqlCmd.Parameters.Add("@FSC_YR_ID", fsc_yr_id);
        sqlCmd.Parameters.Add("@CLAIM_STATUS", 1);
        sqlCmd.Parameters.Add("@INV_ATTACHED", inv_attached);
        sqlCmd.Parameters.Add("@NO_INVOICE_REASON", no_invoice_reason);
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        try
        {
            string Updated = sqlCmd.ExecuteScalar().ToString();
            return Updated;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string SaveClaimRM(string claim_id, string fsc_yr_id, string inv_attached,string no_invoice_reason, string rm_comment, string status)
    {
        if (claim_id == "") return "No Claim ID";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_CLAIM_RM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_ID", claim_id);
        sqlCmd.Parameters.Add("@FSC_YR_ID", fsc_yr_id);
        sqlCmd.Parameters.Add("@CLAIM_STATUS", status);
        sqlCmd.Parameters.Add("@RM_COMMENT", rm_comment);
        sqlCmd.Parameters.Add("@NO_INVOICE_REASON", no_invoice_reason);
        sqlCmd.Parameters.Add("@INV_ATTACHED", inv_attached);
        sqlCmd.Parameters.Add("@RM_ID", SessionValues.UserIdSession);
        try
        {
            sqlCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }



    public string SaveClaimDtlTemp(string claim_dtl_id, string bgt_heading_id, string claim_detail, string claim_qty, string claim_amount)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_CLAIM_DETAIL_TEMP";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_DTL_ID ", claim_dtl_id);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@CLAIM_DETAIL ", claim_detail);
        sqlCmd.Parameters.Add("@CLAIM_QTY ", claim_qty);
        sqlCmd.Parameters.Add("@CLAIM_AMOUNT ", claim_amount);

        sqlCmd.Parameters.Add("@ADDED_BY", SessionValues.UserIdSession);
        try
        {
            sqlCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
   

    public string SaveClaimDetail(string claim_dtl_id, string claim_id, string bgt_heading_id, string claim_detail, string claim_qty, string claim_amount)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_CLAIM_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_DTL_ID ", claim_dtl_id);
        sqlCmd.Parameters.Add("@CLAIM_ID ", claim_id);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@CLAIM_DETAIL ", claim_detail);
        sqlCmd.Parameters.Add("@CLAIM_QTY ", claim_qty);
        sqlCmd.Parameters.Add("@CLAIM_AMOUNT", claim_amount);
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        try
        {
            sqlCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string UpdateClaimDetailRM(string claim_dtl_id, string rm_comment,  string bgt_heading_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_CLAIM_DETAIL_RM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_DTL_ID ", claim_dtl_id);
        sqlCmd.Parameters.Add("@RM_COMMENT ", rm_comment);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@RM", SessionValues.UserIdSession);
        try
        {
            sqlCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string DeleteClaim(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
   
        sqlCmd.CommandText = "SP_DELETE_CLAIM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_ID ", claim_id);
        sqlCmd.Parameters.Add("@DELETED_BY", SessionValues.UserIdSession);
        try
        {
            string val = sqlCmd.ExecuteScalar().ToString();

            return val;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string DeleteClaimDetail(string claim_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELETE_CLAIM_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_DTL_ID ", claim_dtl_id);
        sqlCmd.Parameters.Add("@DELETED_BY", SessionValues.UserIdSession);
        try
        {
            sqlCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string DeleteClaimDetailTemp(string claim_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "Delete from bs_claim_details_temp where claim_dtl_id='" + claim_dtl_id + "' and added_by ='" + SessionValues.UserIdSession + "'";
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            sqlCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string LastClaimCode()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"select max(claim_id) claim_id from bs_claim where created_by='" + SessionValues.UserIdSession + "'";
        string claim_id = base.ExecuteScaler(sqlCmd);
        return claim_id;
    }

    public string ManageClaimApprovers(string claim_id, string role_id, string order, string status)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_MANAGE_CLAIM_APPROVERS";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_ID ", claim_id);
        sqlCmd.Parameters.Add("@ROLE_ID ", role_id);
        sqlCmd.Parameters.Add("@ORDER ", order);
        sqlCmd.Parameters.Add("@STATUS ", status);
        sqlCmd.Parameters.Add("@UPDATED_BY", SessionValues.UserIdSession);
        try
        {
            string ret = sqlCmd.ExecuteScalar().ToString();
            return ret;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public DataTable GetClaimApproversLog(string claim_id)
    {
        string whereCond = " where claim_id='" + claim_id + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT claim_chk_comment,role_name,full_name
                          ,case when claim_chk_proceed=1 then 'Approved'
                           when claim_chk_proceed=-1 then 'Rejected'
                           when claim_chk_proceed=0 then 'On Hold'
                          else '' end as  claim_chk_status,logtimestamp 
                      FROM (Select * from bs_claim_check_log " + whereCond + @") rc
                      left join bs_user_roles ur on rc.role_id=ur.role_id
                      left join bs_users u on rc.modified_by=u.user_id order by logtimestamp desc";
        return base.GetDataResult(sqlCmd);
    }



    public string GetApproverComment(string claim_id, string role_id)
    {
        if (claim_id == "" || role_id == "")
            return "";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (role_id == "6")
            sqlCmd.CommandText = @" SELECT principal_cmt FROM bs_claim "
                + " where claim_id='" + claim_id + "'";
        else if (role_id == "8")
            sqlCmd.CommandText = @" SELECT accountant_cmt FROM bs_claim "
                + " where claim_id='" + claim_id + "'";
        return base.ExecuteScaler(sqlCmd);
    }

    public string UpdateClaimApproval(string claim_id, string role_id, string approve, string comment)
    {
        comment = comment.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_CLAIM_APPROVAL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_ID ", claim_id);
        sqlCmd.Parameters.Add("@ROLE_ID ", role_id);
        sqlCmd.Parameters.Add("@APPROVE ", approve);
        sqlCmd.Parameters.Add("@COMMENT ", comment);
        sqlCmd.Parameters.Add("@UPDATED_BY", SessionValues.UserIdSession);
        try
        {
            string ret = sqlCmd.ExecuteScalar().ToString();
            return ret;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    
    public string PayClaim(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_PAY_CLAIM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@CLAIM_ID ", claim_id);
        sqlCmd.Parameters.Add("@PAID_BY", SessionValues.UserIdSession);
        try
        {
            string ret = sqlCmd.ExecuteScalar().ToString();
            return ret;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string SaveClaimDocs(string claim_id,CheckBoxList chkDocs)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        foreach (ListItem doc in chkDocs.Items)
        {
            if (doc.Selected)
                sqlCmd.CommandText = "SP_SAVE_CLAIM_DOC";
            else
                sqlCmd.CommandText = "SP_DELETE_CLAIM_DOC";

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add("@CLAIM_ID ", claim_id);
            sqlCmd.Parameters.Add("@FILE_NAME ", doc.Text);
            sqlCmd.Parameters.Add("@FILE_PATH ", doc.Value);
            sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
            try
            {
                 sqlCmd.ExecuteScalar();
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        return "";
    }
    public void LoadExpenseClaimDocs(string claim_id,CheckBoxList chkDocs)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select * from bs_claim_docs where claim_id='" + claim_id + "'";
        DataTable dt = GetDataResult(sqlCmd);
        chkDocs.Items.Clear();
        foreach (DataRow dr in dt.Rows)
        {
            chkDocs.Items.Add(new ListItem(dr["file_name"].ToString(), dr["file_path"].ToString()));
            chkDocs.Items[chkDocs.Items.Count - 1].Selected = true;
        }
    }
    public DataTable GetExpenseClaimDocs(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select * from bs_claim_docs where claim_id='" + claim_id + "'";
        return GetDataResult(sqlCmd);
    }
    public string GetClaimStatus(string claim_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select claim_status from bs_claim_statuses where claim_status_id=(Select claim_status_id from bs_claim where claim_id='" + claim_id + "')";
        return sqlCmd.ExecuteScalar().ToString();
    }

}
