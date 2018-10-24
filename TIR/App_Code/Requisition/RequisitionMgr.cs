using System;
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
using System.Data;

/// <summary>
/// Summary description for CLS_InventoryMGMT
/// </summary>
public class RequisitionMgr : DatabaseHelper
{
    private SqlCommand sqlCmd;

    public RequisitionMgr()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// To Check If the User is Creator of Requisition or not. 
    /// </summary>
    /// <param name="req_id"></param>
    /// <returns></returns>

    public DataTable FetchRequisitionsREPORT(string fsc_yr_id, string req_status_id, string requisitioner, string fromReqDate, string toReqDate, bool ownOnly)
    {
        requisitioner = requisitioner.Trim().Replace("'", "''");
        if (fromReqDate.Trim() == "")
        {
            fromReqDate = "1900-01-01";
        }
        if (toReqDate.Trim() == "")
        {
            toReqDate = "9999-12-31";
        }
        string sql = "Select * from bs_requisition ";
        sql += " where fsc_yr_id = '" + fsc_yr_id + "'";
        if (req_status_id != "")
            sql += " and req_status_id = '" + req_status_id + "'";
        if (requisitioner != "")
            sql += " and requisitioner like '%" + requisitioner + "%'";
        //if (ownOnly)
        //{
        //    sql += " and req_status_id < 15";//Closed not in TODO
        //    if (SessionValues.IsHOD)
        //        sql += " and req_hod = '" + SessionValues.UserIdSession + "' ";

        //}
        //else if (SessionValues.IsHOD)
        //    sql += " and (req_hod = '" + SessionValues.UserIdSession + "' or req_created_by = '" + SessionValues.UserIdSession + "')";

        sql += " and req_date between '" + fromReqDate + "' and '" + toReqDate + "'";

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn, r.req_id,requisitioner,req_date,rs.ordering,r.req_status_id,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM (" + sql + @") r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id 
  left join (select count(req_dtl_id) num,req_id reqId from bs_requisition_details group by req_id ) rd on rd.reqId=r.req_id
";
//            if (ownOnly)
//            {
//                sqlCmd.CommandText += @" left join (SELECT req_chk_proceed,rcrole.req_id FROM 
//(SELECT max(role_id) role_id,req_id FROM bs_req_check GROUP BY req_id) rcrole
//left join bs_req_check rcval ON rcval.req_id=rcrole.req_id AND rcval.role_id=rcrole.role_id) final_approval on r.req_id=final_approval.req_id";
//                sqlCmd.CommandText += " where num>0 and r.is_pound=0  and ((rs.role_id=" + SessionValues.RoleIdSession + @" and rs.req_proceed!=-1)  -- with RM, Open/Approved by RM avoid Rejected by RM
//or (final_approval.req_chk_proceed = 1 ) -- Finally Approved " + @"
//) ";
//            }
//            else
//            {
//                sqlCmd.CommandText += " where num>0 ";
//            }
        
//        else if (SessionValues.IsApprover)
//        {
//            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn,r.req_id,requisitioner,req_date,rs.ordering,r.req_status_id,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
//  FROM  (" + sql + @") r  -- and req_status_id!=1
//  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
//  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
//            if (ownOnly)//TODO
//            {
//                sqlCmd.CommandText += @" left join (SELECT max(role_id) role_id,req_id FROM bs_req_check where user_id is not null GROUP BY req_id) max_proceeded_approver
// on r.req_id=max_proceeded_approver.req_id 
//  left join bs_req_check rc on rc.role_id= " + SessionValues.RoleIdSession + @"  and rc.req_id=r.req_id 
// left join (SELECT req_chk_proceed,rcrole.req_id FROM 
//(SELECT max(role_id) role_id,req_id FROM bs_req_check where role_id<" + SessionValues.RoleIdSession + @"  GROUP BY req_id) rcrole
//left join bs_req_check rcval ON rcval.req_id=rcrole.req_id AND rcval.role_id=rcrole.role_id) just_below_approval on r.req_id=just_below_approval.req_id";
//                sqlCmd.CommandText += " where  (5 =" + SessionValues.RoleIdSession + @" and is_pound=1) or ( rs.req_status_id<10 and ((rs.role_id=" + SessionValues.RoleIdSession + @" and rs.req_proceed!=-1) -- with approver 
//                 or ((just_below_approval.req_chk_proceed=1  --Approved by approver just below
// or just_below_approval.req_chk_proceed is null ) -- No other approver below assigned 
// and isnull(max_proceeded_approver.role_id,3)<=" + SessionValues.RoleIdSession + @" -- No appprover above have worked
// and rc.req_id is not null --Approver role is assigned
// and rs.req_proceed=1 --Requsition appproved by RM
// )))";
//            }
//        }
//        else
//        {
//            sqlCmd.CommandText = @"  SELECT row_number () over (order by fsc_yr,req_date desc) as sn,req_id,requisitioner,req_date,r.req_status_id,rs.ordering,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
//  FROM (" + sql + " and req_created_by = '" + SessionValues.UserIdSession + "' " + @" ) r 
//  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
//  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
//        }

        //sqlCmd.CommandText += " order by ordering,req_id desc";
        sqlCmd.CommandText += " order by req_id desc,req_last_modified_date  desc";
       return base.GetDataResult(sqlCmd);
    }


    public bool IsReqCreator(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select req_created_by from bs_requisition where req_id='" + req_id + "'";
        string creator = base.ExecuteScaler(sqlCmd);
        if (creator == SessionValues.UserIdSession)
            return true;
        return false;
    }

    /// <summary>
    /// To Check if requisition proceeded By RM or not
    /// </summary>
    /// <param name="req_id"></param>
    /// <returns></returns>

    public string SaveRequisitionPound(string req_id, string requisitioner, string requisition_date, string requisition_year, string fsc_yr_id)
    {
        if (req_id == "") req_id = "0";
        //requisitioner = requisitioner.Trim().Replace("'", "''");
        //requisition_date = requisition_date.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_REQUISITION_POUND";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DATE", Convert.ToDateTime(requisition_date).Date);
        sqlCmd.Parameters.Add("@REQUISITIONER", requisitioner);
        sqlCmd.Parameters.Add("@REQ_YEAR", requisition_year);
        sqlCmd.Parameters.Add("@REQ_ID", req_id);
        sqlCmd.Parameters.Add("@FSC_YR_ID", fsc_yr_id);
        sqlCmd.Parameters.Add("@REQ_STATUS", 1);// open when saved
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        sqlCmd.Parameters.Add("@IS_POUND", 1);

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

    public string SaveReqDtlTempPound(string req_dtl_id, string bgt_heading_id, string req_detail, string req_qty, string ex_rate, string pound_amt)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_REQ_DETAIL_TEMP_POUND";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@REQ_DETAIL ", req_detail);
        sqlCmd.Parameters.Add("@REQ_QTY ", req_qty);
        sqlCmd.Parameters.Add("@ADDED_BY", SessionValues.UserIdSession);
        sqlCmd.Parameters.Add("@EX_RATE", ex_rate);
        sqlCmd.Parameters.Add("@POUND_AMT", pound_amt);
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

    public DataTable get_exchange_rate()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"SELECT TOP 1  nrs FROM bs_forex_rates order by forex_id desc";
        return base.GetDataResult(sqlCmd);
    }

    public string DeliverReqItem_email(string req_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELIVER_REQ_DETAIL_EMAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);


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

    public bool IsOpenRequisition(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select req_status_id from bs_requisition where req_id='" + req_id + "'";
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
    public DataTable FetchRequisitions()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (SessionValues.IsRM)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn, req_id,requisitioner,req_date,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM bs_requisition r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id 
  left join (select count(req_dtl_id) num,req_id reqId from bs_requisition_details group by req_id ) rd on rd.reqId=r.req_id
where num>0
  order by fsc_yr,req_date desc";
        }
        else if (SessionValues.IsApprover)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn,req_id,requisitioner,req_date,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM bs_requisition r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id where r.req_status_id!=1  order by req_date desc";
        }
        else
        {
            sqlCmd.CommandText = @"  SELECT row_number () over (order by fsc_yr,req_date desc) as sn,req_id,requisitioner,req_date,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM bs_requisition r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id where r.req_created_by = '" + SessionValues.UserIdSession + "'  order by fsc_yr, req_date desc";
        }
        return base.GetDataResult(sqlCmd);
    }

    /// <summary>
    /// Get List of requisition. 
    /// If Staff, Only Created by themself// closed too
    /// If RM All
    /// If Approvers, Proceeded By RM
    /// </summary>
    /// <returns></returns>
    /// 
    public DataTable FetchReqReport(string fsc_yr_id, string req_status_id, string requisitioner, string fromReqDate, string toReqDate)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        string sql = "Select * from bs_requisition ";
        sql += " where fsc_yr_id = '" + fsc_yr_id + "' and req_status_id < 15 ";
        if (req_status_id != "")
            sql += " and req_status_id = '" + req_status_id + "'";
        if (requisitioner != "")
            sql += " and requisitioner like '%" + requisitioner + "%'";


        if (fromReqDate != "" && toReqDate != "")
        {
            sql += " and req_date between '" + fromReqDate + "' and '" + toReqDate + "'";
        }
        sqlCmd.CommandText = string.Format(@" SELECT row_number () over (order by fsc_yr,req_date desc) as sn, r.req_id,requisitioner,cast(cast(req_date as date)as varchar(15)) as req_date ,r.req_status_id,rs.req_status,req_year,fsc_yr,(bh.bgt_code + ': '+bh.bgt_heading) as heading,d.req_detail as detail,d.req_qty as quantity,d.req_rate as rate
  FROM(" + sql + @")  r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id
  left join bs_requisition_details d on d.req_id=r.req_id
  inner join bs_budget_headings bh on bh.bgt_heading_id=d.bgt_heading_id");


        return base.GetDataResult(sqlCmd);
    }

    public DataTable FetchRequisitions_searchbyreqid(string fsc_yr_id, string req_status_id, string requisitioner, string fromReqDate, string toReqDate, bool ownOnly, string reqid)
    {
        requisitioner = requisitioner.Trim().Replace("'", "''");
        if (fromReqDate.Trim() == "")
        {
            fromReqDate = "1900-01-01";
        }
        if (toReqDate.Trim() == "")
        {
            toReqDate = "9999-12-31";
        }
        string sql = "Select * from bs_requisition ";
        sql += " where fsc_yr_id = '" + fsc_yr_id + "'";
        if (req_status_id != "")
            sql += " and req_status_id = '" + req_status_id + "'";
        if (requisitioner != "")
            sql += " and requisitioner like '%" + requisitioner + "%'";

        if (reqid != "")
        {
            sql += "and req_id= '" + reqid + "'";
        }
        if (ownOnly)
        {
            sql += " and req_status_id < 15";//Closed not in TODO
            if (SessionValues.IsHOD)
                sql += " and req_hod = '" + SessionValues.UserIdSession + "' ";

        }
        else if (SessionValues.IsHOD)
            sql += " and (req_hod = '" + SessionValues.UserIdSession + "' or req_created_by = '" + SessionValues.UserIdSession + "')";


        sql += " and req_date between '" + fromReqDate + "' and '" + toReqDate + "'";

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (SessionValues.IsRM)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn, r.req_id,requisitioner,req_date,rs.ordering,r.req_status_id,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM (" + sql + @") r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id 
  left join (select count(req_dtl_id) num,req_id reqId from bs_requisition_details group by req_id ) rd on rd.reqId=r.req_id
";
            if (ownOnly)
            {
                sqlCmd.CommandText += @" left join (SELECT req_chk_proceed,rcrole.req_id FROM 
(SELECT max(role_id) role_id,req_id FROM bs_req_check GROUP BY req_id) rcrole
left join bs_req_check rcval ON rcval.req_id=rcrole.req_id AND rcval.role_id=rcrole.role_id) final_approval on r.req_id=final_approval.req_id";
                sqlCmd.CommandText += " where num>0 and r.is_pound=0  and ((rs.role_id=" + SessionValues.RoleIdSession + @" and rs.req_proceed!=-1)  -- with RM, Open/Approved by RM avoid Rejected by RM
or (final_approval.req_chk_proceed = 1 ) -- Finally Approved " + @"
) ";
            }
            else
            {
                sqlCmd.CommandText += " where num>0 ";
            }
        }
        else if (SessionValues.IsApprover)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn,r.req_id,requisitioner,req_date,rs.ordering,r.req_status_id,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM  (" + sql + @") r  -- and req_status_id!=1
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
            if (ownOnly)//TODO
            {
                sqlCmd.CommandText += @" left join (SELECT max(role_id) role_id,req_id FROM bs_req_check where user_id is not null GROUP BY req_id) max_proceeded_approver
 on r.req_id=max_proceeded_approver.req_id 
  left join bs_req_check rc on rc.role_id= " + SessionValues.RoleIdSession + @"  and rc.req_id=r.req_id 
 left join (SELECT req_chk_proceed,rcrole.req_id FROM 
(SELECT max(role_id) role_id,req_id FROM bs_req_check where role_id<" + SessionValues.RoleIdSession + @"  GROUP BY req_id) rcrole
left join bs_req_check rcval ON rcval.req_id=rcrole.req_id AND rcval.role_id=rcrole.role_id) just_below_approval on r.req_id=just_below_approval.req_id";
                sqlCmd.CommandText += " where  (5 =" + SessionValues.RoleIdSession + @" and is_pound=1) or ( rs.req_status_id<10 and ((rs.role_id=" + SessionValues.RoleIdSession + @" and rs.req_proceed!=-1) -- with approver 
                 or ((just_below_approval.req_chk_proceed=1  --Approved by approver just below
 or just_below_approval.req_chk_proceed is null ) -- No other approver below assigned 
 and isnull(max_proceeded_approver.role_id,3)<=" + SessionValues.RoleIdSession + @" -- No appprover above have worked
 and rc.req_id is not null --Approver role is assigned
 and rs.req_proceed=1 --Requsition appproved by RM
 )))";
            }
        }
        else
        {
            sqlCmd.CommandText = @"  SELECT row_number () over (order by fsc_yr,req_date desc) as sn,req_id,requisitioner,req_date,r.req_status_id,rs.ordering,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM (" + sql + " and req_created_by = '" + SessionValues.UserIdSession + "' " + @" ) r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
        }

        sqlCmd.CommandText += " order by req_last_modified_date desc,ordering,req_id desc";
        return base.GetDataResult(sqlCmd);
    }


    public DataTable FetchRequisitions(string fsc_yr_id, string req_status_id, string requisitioner, string fromReqDate, string toReqDate, bool ownOnly)
    {
        requisitioner = requisitioner.Trim().Replace("'", "''");
        if (fromReqDate.Trim() == "")
        {
            fromReqDate = "1900-01-01";
        }
        if (toReqDate.Trim() == "")
        {
            toReqDate = "9999-12-31";
        }
        string sql = "Select * from bs_requisition ";
        sql += " where fsc_yr_id = '" + fsc_yr_id + "'";
        if (req_status_id != "")
            sql += " and req_status_id = '" + req_status_id + "'";
        if (requisitioner != "")
            sql += " and requisitioner like '%" + requisitioner + "%'";
        if (ownOnly)
        {
            sql += " and req_status_id < 15";//Closed not in TODO
            if (SessionValues.IsHOD)
                sql += " and req_hod = '" + SessionValues.UserIdSession + "' ";

        }
        else if (SessionValues.IsHOD)
            sql += " and (req_hod = '" + SessionValues.UserIdSession + "' or req_created_by = '" + SessionValues.UserIdSession + "')";


        sql += " and req_date between '" + fromReqDate + "' and '" + toReqDate + "'";

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (SessionValues.IsRM)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn, r.req_id,requisitioner,req_date,rs.ordering,r.req_status_id,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM (" + sql + @") r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id 
  left join (select count(req_dtl_id) num,req_id reqId from bs_requisition_details group by req_id ) rd on rd.reqId=r.req_id
";
            if (ownOnly)
            {
                sqlCmd.CommandText += @" left join (SELECT req_chk_proceed,rcrole.req_id FROM 
(SELECT max(role_id) role_id,req_id FROM bs_req_check GROUP BY req_id) rcrole
left join bs_req_check rcval ON rcval.req_id=rcrole.req_id AND rcval.role_id=rcrole.role_id) final_approval on r.req_id=final_approval.req_id";
                sqlCmd.CommandText += " where num>0 and r.is_pound=0  and ((rs.role_id=" + SessionValues.RoleIdSession + @" and rs.req_proceed!=-1)  -- with RM, Open/Approved by RM avoid Rejected by RM
or (final_approval.req_chk_proceed = 1 ) -- Finally Approved " + @"
) ";
            }
            else
            {
                sqlCmd.CommandText += " where num>0 ";
            }
        }
        else if (SessionValues.IsApprover)
        {
            sqlCmd.CommandText = @" SELECT row_number () over (order by fsc_yr,req_date desc) as sn,r.req_id,requisitioner,req_date,rs.ordering,r.req_status_id,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM  (" + sql + @") r  -- and req_status_id!=1
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
            if (ownOnly)//TODO
            {
                sqlCmd.CommandText += @" left join (SELECT max(role_id) role_id,req_id FROM bs_req_check where user_id is not null GROUP BY req_id) max_proceeded_approver
 on r.req_id=max_proceeded_approver.req_id 
  left join bs_req_check rc on rc.role_id= " + SessionValues.RoleIdSession + @"  and rc.req_id=r.req_id 
 left join (SELECT req_chk_proceed,rcrole.req_id FROM 
(SELECT max(role_id) role_id,req_id FROM bs_req_check where role_id<" + SessionValues.RoleIdSession + @"  GROUP BY req_id) rcrole
left join bs_req_check rcval ON rcval.req_id=rcrole.req_id AND rcval.role_id=rcrole.role_id) just_below_approval on r.req_id=just_below_approval.req_id";
                sqlCmd.CommandText += " where  (5 =" + SessionValues.RoleIdSession + @" and is_pound=1) or ( rs.req_status_id<10 and ((rs.role_id=" + SessionValues.RoleIdSession + @" and rs.req_proceed!=-1) -- with approver 
                 or ((just_below_approval.req_chk_proceed=1  --Approved by approver just below
 or just_below_approval.req_chk_proceed is null ) -- No other approver below assigned 
 and isnull(max_proceeded_approver.role_id,3)<=" + SessionValues.RoleIdSession + @" -- No appprover above have worked
 and rc.req_id is not null --Approver role is assigned
 and rs.req_proceed=1 --Requsition appproved by RM
 )))";
            }
        }
        else
        {
            sqlCmd.CommandText = @"  SELECT row_number () over (order by fsc_yr,req_date desc) as sn,req_id,requisitioner,req_date,r.req_status_id,rs.ordering,rs.req_status,req_status_remarks,req_rm_comment,req_year,fsc_yr
  FROM (" + sql + " and req_created_by = '" + SessionValues.UserIdSession + "' " + @" ) r 
  left join bs_requisition_statuses rs on rs.req_status_id=r.req_status_id  
  left join bs_fiscal_years f on f.fsc_yr_id= r.fsc_yr_id ";
        }

        sqlCmd.CommandText += " order by req_last_modified_date desc,ordering,req_id desc";
        return base.GetDataResult(sqlCmd);
    }

    /// <summary>
    /// Get Requisition Info
    /// </summary>
    /// <param name="requisiton_id"></param>
    /// <returns></returns>
    public DataTable GetRequisionData(string requisiton_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT req_id,requisitioner,req_date,rs.req_status,req_status_remarks,req_rm_comment,req_year,is_pound,r.req_status_id,r.fsc_yr_id,r.req_created_by,u.full_name,req_hod
  FROM bs_requisition r left join bs_requisition_statuses rs 
  on rs.req_status_id=r.req_status_id
left join bs_users u 
  on r.req_created_by= u.user_id where req_id='" + requisiton_id + "'";
        return base.GetDataResult(sqlCmd);
    }

    public DataTable get_pound_for_chk(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" select  req_id,requisitioner,is_pound,u.role_id as role_id from bs_requisition r inner join  bs_users u on r.req_created_by=u.user_id where req_id='" + req_id + "'";
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetRequisitonDetails(string requisiton_id)
    {
        string whereCond = " where 1=1 ";
        if (requisiton_id != null && requisiton_id != "")
            whereCond += " and req_id='" + requisiton_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = string.Format(@"  SELECT req_dtl_id,rd.req_id,bh.bgt_code,(bh.bgt_code+': '+bh.bgt_heading) heading,rd.bgt_heading_id,
 case when rd.req_dtl_type='1' and an.ast_name is not null then '('+an.ast_name+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then '('+itm.item_name+')'
 else ''
 end req_item,req_detail,req_qty,req_rate,cast((req_qty*req_rate) as decimal(18,2)) req_cost,pound_amount
,req_dtl_rm_comment,req_time,req_dtl_rm,req_dtl_in_stock,rd.req_dtl_status,rd.req_dtl_type,case when parent_req_dtl_id is null or rd.req_dtl_status=18 then rs.req_status else 'Partial Delivery' end 'req_status',total_budget,total_frozen_amount,rs.req_proceed,rd.item_id,po_received,
case when (po_received=1 or req_dtl_in_stock=1) and (rd.req_dtl_status<13 and rs.req_proceed>=0 and (select req_chk_proceed from bs_req_check
where req_id=r.req_id and req_chk_order=(select max(req_chk_order) from bs_req_check where req_id=r.req_id))=1 ) then 'Available'
else rs.req_status end final_status
        FROM (select * from bs_requisition_details {0} ) rd
        left join bs_requisition_statuses rs on rd.req_dtl_status=rs.req_status_id
        left join bs_asset_names an on rd.item_id=an.ast_name_id and rd.req_dtl_type='1'
        left join bs_inventory_items itm on rd.item_id=itm.item_id and rd.req_dtl_type='0'
        left join bs_requisition r on r.req_id=rd.req_id
        left join bs_budget_headings bh on bh.bgt_heading_id=rd.bgt_heading_id
        left join vw_budget_status bs on bs.bgt_heading_id=rd.bgt_heading_id and bs.fsc_yr_id=r.fsc_yr_id ", whereCond);
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetRequisitonDetailsTemp()
    {
        string whereCond = " where added_by='" + SessionValues.UserIdSession + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = string.Format(@"  SELECT req_dtl_id,bh.bgt_code,(bh.bgt_code+': '+bh.bgt_heading) heading,req_detail,req_qty,exchange_rate,pound_amount
        FROM (select * from bs_req_details_temp {0} ) rd
        left join bs_budget_headings bh on bh.bgt_heading_id=rd.bgt_heading_id", whereCond);
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetRequisitonDetailTemp(string req_dtl_id)
    {
        string whereCond = " where 1=1 ";
        if (req_dtl_id != null && req_dtl_id != "")
            whereCond += " and req_dtl_id='" + req_dtl_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select * from bs_req_details_temp " + whereCond;
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetRequisitonDetail(string req_dtl_id)
    {
        string whereCond = " where 1=1 ";
        if (req_dtl_id != null && req_dtl_id != "")
            whereCond += " and req_dtl_id='" + req_dtl_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select * from bs_requisition_details " + whereCond;
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetBudgetCodes()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        if (SessionValues.IsRM)
            sqlCmd.CommandText = @"Select bgt_heading_id,bgt_heading,bgt_code,(bgt_code + ': '+ bgt_heading) heading from bs_budget_headings where bgt_heading_id!=0 ";
        else
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
    /// Add/Edit Requsiiton By Creator
    /// </summary>
    /// <param name="req_id"></param>
    /// <param name="requisitioner"></param>
    /// <param name="requisition_date"></param>
    /// <param name="requisition_year"></param>
    /// <param name="fsc_yr_id"></param>
    /// <returns></returns>


    public string SaveRequisitionDetail_Pound(string req_dtl_id, string req_id, string bgt_code, string bgt_heading_id, string req_detail, string req_qty, decimal exchange_rate, decimal pound_amount)
    {
        //bgt_code = bgt_code.Trim().Replace("'", "''");
        //req_detail = req_detail.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_REQ_DETAIL_POUND";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
        sqlCmd.Parameters.Add("@BGT_CODE ", bgt_code);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@REQ_DETAIL ", req_detail);
        sqlCmd.Parameters.Add("@REQ_QTY ", req_qty);
        sqlCmd.Parameters.Add("@REQ_RATE ", "0.000");
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        sqlCmd.Parameters.Add("@EXC_RATE ", exchange_rate);
        sqlCmd.Parameters.Add("@POUND_AMOUNT ", pound_amount);

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

    public string UpdateRequisitioner(string requisitioner, int req_id, string old_requisitioner, int req_update_status, DateTime req_date, DateTime req_last_modified, int  req_last_modified_by,string desc,int req_year,int fixcal_year)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText ="SP_UPDATE_REQUISITIONER";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQUISITIONER", requisitioner);
        sqlCmd.Parameters.Add("@REQ_NO", req_id);
        sqlCmd.Parameters.Add("@OLD_REQUISITIONER", old_requisitioner);
        sqlCmd.Parameters.Add("@REQ_UPDATE_STATUS", req_update_status);
        sqlCmd.Parameters.Add("@REQ_DATETIME", req_date);
        sqlCmd.Parameters.Add("@REQ_LAST_MODIFIED", req_last_modified);
        sqlCmd.Parameters.Add("@REQ_LAST_MODIFIED_BY", req_last_modified_by);
        sqlCmd.Parameters.Add("@DESC", desc);
        sqlCmd.Parameters.Add("@REQ_YEAR", req_year);
        sqlCmd.Parameters.Add("@FISCAL_YEAR", fixcal_year);
        


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



    public string SaveRequisition(string req_id, string requisitioner, string requisition_date, string requisition_year, string fsc_yr_id)
    {
        if (req_id == "") req_id = "0";
        //requisitioner = requisitioner.Trim().Replace("'", "''");
        //requisition_date = requisition_date.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_REQUISITION";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DATE", Convert.ToDateTime(requisition_date).Date);
        sqlCmd.Parameters.Add("@REQUISITIONER", requisitioner);
        sqlCmd.Parameters.Add("@REQ_YEAR", requisition_year);
        sqlCmd.Parameters.Add("@REQ_ID", req_id);
        sqlCmd.Parameters.Add("@FSC_YR_ID", fsc_yr_id);
        sqlCmd.Parameters.Add("@REQ_STATUS", 1);
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

    public string UpdateRequisitionRM(string req_id, string rm_comment, string status_remark, string status)
    {
        //rm_comment = rm_comment.Trim().Replace("'", "''");
        //status_remark = status_remark.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQUISITION_RM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_RM_COMMENT", rm_comment);
        sqlCmd.Parameters.Add("@REQ_STATUS_REMARKS", status_remark);
        sqlCmd.Parameters.Add("@REQ_STATUS", status);
        sqlCmd.Parameters.Add("@REQ_ID", req_id);
        sqlCmd.Parameters.Add("@REQ_RM", SessionValues.UserIdSession);
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

    public string UpdateRequisitionRMPound(string req_id, string rm_comment, string status_remark, string status)
    {
        //rm_comment = rm_comment.Trim().Replace("'", "''");
        //status_remark = status_remark.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQUISITION_RM_Pound";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_RM_COMMENT", rm_comment);
        sqlCmd.Parameters.Add("@REQ_STATUS_REMARKS", status_remark);
        sqlCmd.Parameters.Add("@REQ_STATUS", status);
        sqlCmd.Parameters.Add("@REQ_ID", req_id);
        sqlCmd.Parameters.Add("@REQ_RM", SessionValues.UserIdSession);
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
    public string Update_BGT_CODE(string req_id, string bgt_code, string user_id, string req_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DOS_UPDATE_BGTCODE";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_code);
        sqlCmd.Parameters.Add("@UPDATED_BY", user_id);
        sqlCmd.Parameters.Add("@req_dtl_id", req_dtl_id);
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

    public string UpdateRequisitionDOS(string req_id, string rm_comment, string status_remark, string status)
    {
        //rm_comment = rm_comment.Trim().Replace("'", "''");
        //status_remark = status_remark.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQUISITION_DOS";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DOS_COMMENT", rm_comment);
        sqlCmd.Parameters.Add("@REQ_STATUS_REMARKS", status_remark);
        sqlCmd.Parameters.Add("@REQ_STATUS", status);
        sqlCmd.Parameters.Add("@REQ_ID", req_id);
        sqlCmd.Parameters.Add("@REQ_DOS", SessionValues.UserIdSession);
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

    public string UpdateRequisitionRM(string req_id, string rm_comment, string status_remark, string status, string hod)
    {
        //rm_comment = rm_comment.Trim().Replace("'", "''");
        //status_remark = status_remark.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQUISITION_WITHHOD_RM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_RM_COMMENT", rm_comment);
        sqlCmd.Parameters.Add("@REQ_STATUS_REMARKS", status_remark);
        sqlCmd.Parameters.Add("@REQ_STATUS", status);
        sqlCmd.Parameters.Add("@REQ_ID", req_id);
        sqlCmd.Parameters.Add("@REQ_HOD", hod);
        sqlCmd.Parameters.Add("@REQ_RM", SessionValues.UserIdSession);
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

    public string SaveReqDtlTemp(string req_dtl_id, string bgt_heading_id, string req_detail, string req_qty)
    {
        decimal req_qtyy = Convert.ToDecimal(req_qty);
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_REQ_DETAIL_TEMP";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@REQ_DETAIL ", req_detail);
        sqlCmd.Parameters.Add("@REQ_QTY ", req_qtyy);
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

    public string SaveRequisitionDetail(string req_id, string bgt_code, string bgt_heading_id, string req_detail, string req_qty)
    {
        //bgt_code = bgt_code.Trim().Replace("'", "''");
        //req_detail = req_detail.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_INSERT_REQ_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
        sqlCmd.Parameters.Add("@BGT_CODE ", bgt_code);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@REQ_DETAIL ", req_detail);
        sqlCmd.Parameters.Add("@REQ_QTY ", req_qty);
        sqlCmd.Parameters.Add("@REQ_RATE ", "0.000");
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

    public string SaveRequisitionDetail(string req_dtl_id, string req_id, string bgt_code, string bgt_heading_id, string req_detail, string req_qty)
    {
        //bgt_code = bgt_code.Trim().Replace("'", "''");
        //req_detail = req_detail.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_REQ_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
        sqlCmd.Parameters.Add("@BGT_CODE ", bgt_code);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@REQ_DETAIL ", req_detail);
        sqlCmd.Parameters.Add("@REQ_QTY ", req_qty);
        sqlCmd.Parameters.Add("@REQ_RATE ", "0.000");
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

    public string UpdateRequisitionDetailRM(string req_dtl_id, string in_stock, string type, string rm_comment, string rate, string status, string bgt_heading_id, string bgt_code)
    {
        //rm_comment = rm_comment.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQ_DETAIL_RM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@IN_STOCK ", in_stock);
        sqlCmd.Parameters.Add("@REQ_DTL_RM_COMMENT ", rm_comment);
        sqlCmd.Parameters.Add("@REQ_RATE ", rate);
        sqlCmd.Parameters.Add("@STATUS ", status);
        sqlCmd.Parameters.Add("@TYPE ", type);
        sqlCmd.Parameters.Add("@BGT_HEADING_ID ", bgt_heading_id);
        sqlCmd.Parameters.Add("@BGT_CODE ", bgt_code);
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

    public string UpdateReqDtlItemMapRM(string req_dtl_id, string item_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQ_DTL_ITEM_MAP_RM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@ITEM_ID ", item_id);
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

    /// <summary>
    /// Enlist Order/ASpproved Details in Purchase Order ans set Status of Requisition as Purchase Order Created 
    /// </summary>
    /// <param name="req_id"></param>
    /// <returns></returns>
    public string RequisitionDetailPO(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_REQ_DETAIL_PO";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
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

    public string DeleteRequisition(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select req_dtl_id from bs_requisition_details where req_id='" + req_id + "'";
        DataTable dt = GetDataResult(sqlCmd);
        foreach (DataRow dr in dt.Rows)
        {
            DeleteRequisitionDetail(dr["req_dtl_id"].ToString());
        }

        sqlCmd.CommandText = "SP_DELETE_REQ";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
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

    public string DeleteRequisitionDetail(string req_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELETE_REQ_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
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

    public string DeleteReqDetailTemp(string req_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "Delete from bs_req_details_temp where req_dtl_id='" + req_dtl_id + "' and added_by ='" + SessionValues.UserIdSession + "'";
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

    public string LastReqCode()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"select max(req_id) req_id from bs_requisition where req_created_by='" + SessionValues.UserIdSession + "'";
        string req_id = base.ExecuteScaler(sqlCmd);
        return req_id;
    }

    public string ManageReqApprovers(string req_id, string role_id, string order, string status)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_MANAGE_REQ_APPROVERS";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
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

    public DataTable GetReqApprovers(string req_id)
    {
        string whereCond = " ";
        if (req_id != null && req_id != "")
            whereCond += " where req_id='" + req_id + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT req_chk_comment,role_name,full_name
                          ,case when req_chk_proceed=1 then 'Approved'
                           when req_chk_proceed=-1 then 'Rejected'
                           when req_chk_proceed=0 then 'With RM'
                          else '' end as  req_chk_status 
                      FROM bs_req_check rc
                      left join bs_user_roles ur on rc.role_id=ur.role_id
                      left join bs_users u on rc.user_id=u.user_id" + whereCond + " order by req_chk_order";
        return base.GetDataResult(sqlCmd);
    }

    public DataTable GetReqApproversLog(string req_id)
    {
        string whereCond = " where description !='APPROVER ADDED' and description !='APPROVER REMOVED'  ";
        // if (req_id != null && req_id != "")
        whereCond += " and req_id='" + req_id + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT req_chk_comment,role_name,full_name
                          ,case when req_chk_proceed=1 then 'Approved'
                           when req_chk_proceed=-1 then 'Rejected'
                           when req_chk_proceed=0 then 'With RM'
                          else '' end as  req_chk_status,logtimestamp 
                      FROM (Select * from bs_req_check_log " + whereCond + @") rc
                      left join bs_user_roles ur on rc.role_id=ur.role_id
                      left join bs_users u on rc.modified_by=u.user_id order by logtimestamp desc";
        return base.GetDataResult(sqlCmd);
    }

    public bool IsApproverAssigned(string req_id, string role_id)// role id not taken for session as needed to check others also. checkbox
    {
        if (req_id == "" || role_id == "")
            return false;
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT COUNT(req_id) FROM bs_req_check "
            + " where req_id='" + req_id + "' and role_id='" + role_id + "'";
        if (base.ExecuteScaler(sqlCmd) == "0")
            return false;
        else
            return true;
    }

    public bool IsApproverAllowed(string req_id, string role_id)
    {
        if (req_id == "" || role_id == "")
            return false;
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        //        sqlCmd.CommandText = @"Select role_id from (select req_status_id from bs_requisition where req_id=" + req_id + @") r
        //inner join (select role_id,req_status_id from bs_requisition_statuses 
        //where req_proceed=1) rs on rs.req_status_id=r.req_status_id";

        sqlCmd.CommandText = string.Format(@"
Select r.req_status_id from
(select req_status_id from bs_requisition where req_id={0}) r
inner join  bs_requisition_statuses rs on rs.req_status_id=r.req_status_id
inner join bs_req_check rc on  req_id={0} and rc.role_id= {1}
left join (SELECT max(role_id) role_id,req_id FROM bs_req_check where user_id is not null GROUP BY req_id) max_proceeded_approver on {0}=max_proceeded_approver.req_id 
left join (SELECT req_chk_proceed FROM 
(SELECT max(role_id) role_id FROM bs_req_check where role_id<{1} and req_id={0}) rcrole
inner join bs_req_check rcval ON req_id={0} and rcval.role_id=rcrole.role_id) just_below_approval on 1=1
 where r.req_status_id<10 and ((rs.role_id={1}) -- with approver
 or ((just_below_approval.req_chk_proceed=1  --Approved by approver just below
 or just_below_approval.req_chk_proceed is null ) -- No other approver below assigned 
 and isnull(max_proceeded_approver.role_id,3)<={1} -- No appprover above have worked
 and rc.req_id is not null --Approver role is assigned
 and rs.req_proceed=1 --Requsition appproved by RM
 ))", req_id, role_id); ;
        string approvalrole = base.ExecuteScaler(sqlCmd);

        if (approvalrole == "")
            return false;
        else
            return true;
    }

    public bool FinallyApproved(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT TOP(1) req_chk_proceed
FROM bs_req_check
where req_id='" + req_id + "' order by role_id desc";

        if (base.ExecuteScaler(sqlCmd).ToString() == "1")
            return true;
        return false;
    }

    public bool POCreated(string req_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT count(req_dtl_id)
FROM bs_requisition_details where req_id='" + req_id + "' and req_dtl_status=2 and req_dtl_in_stock=0";

        if (Convert.ToInt32(base.ExecuteScaler(sqlCmd)) > 0)
            return false;
        return true;
    }

    public string GetApproverComment(string req_id, string role_id)
    {
        if (req_id == "" || role_id == "")
            return "";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT req_chk_comment FROM bs_req_check "
            + " where req_id='" + req_id + "' and role_id='" + role_id + "'";
        return base.ExecuteScaler(sqlCmd);
    }



    public string UpdateReqApproval(string req_id, string role_id, string approve, string comment)
    {
        comment = comment.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_UPDATE_REQ_APPROVAL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_ID ", req_id);
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

    #region DISPATCH

    public DataTable ItemsToDispatch(string fsc_yr_id, string bgt_heading_id, string details, string req_id, string quantity, string qtyType, string cost, string costType)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select rd.*,req_qty*req_rate req_cost,(bh.bgt_code + ': '+ bh.bgt_heading) heading,r.* from bs_requisition_details rd
inner join bs_requisition r on r.req_id=rd.req_id
inner join (select * from bs_requisition_statuses where req_proceed!=-1) rs on rs.req_status_id=rd.req_dtl_status
left join bs_budget_headings bh on bh.bgt_heading_id=rd.bgt_heading_id where req_dtl_in_stock=1";
        if (details.Replace("'", "''").Trim() != "")
        {
            sqlCmd.CommandText += " and req_detail like '%" + details.Replace("'", "''").Trim() + "%'";
        }
        if (req_id.Trim() != "")
        {
            sqlCmd.CommandText += " and req_id = '" + req_id + "'";
        }
        if (fsc_yr_id.Trim() != "")
        {
            sqlCmd.CommandText += " and fsc_yr_id = '" + fsc_yr_id + "'";
        }
        if (bgt_heading_id.Trim() != "")
        {
            sqlCmd.CommandText += " and rd.bgt_heading_id = '" + bgt_heading_id + "'";
        }
        if (cost.Trim() != "")
        {
            sqlCmd.CommandText += " and req_qty*req_rate ";
            if (Convert.ToInt32(costType) == 0)
                sqlCmd.CommandText += " = ";
            else if (Convert.ToInt32(costType) < 0)
                sqlCmd.CommandText += " < ";
            else
                sqlCmd.CommandText += " > ";

            sqlCmd.CommandText += cost.Trim();

        }
        if (quantity.Trim() != "")
        {
            sqlCmd.CommandText += " and req_qty ";
            if (Convert.ToInt32(qtyType) == 0)
                sqlCmd.CommandText += " = ";
            else if (Convert.ToInt32(qtyType) < 0)
                sqlCmd.CommandText += " < ";
            else
                sqlCmd.CommandText += " > ";

            sqlCmd.CommandText += quantity.Trim();
        }
        return base.GetDataResult(sqlCmd);

    }

    public DataTable ItemsToDispatch(string fsc_yr_id, string bgt_heading_id, string details, string req_id, string quantity, string qtyType,
        string cost, string costType, string requisitioner, string item_type, string fromReqDate, string toReqDate)
    {

        requisitioner = requisitioner.Trim().Replace("'", "''");
        if (fromReqDate.Trim() == "")
        {
            fromReqDate = "1900-01-01";
        }
        if (toReqDate.Trim() == "")
        {
            toReqDate = "9999-12-31";
        }
        string req_sql = "Select r.* from ";
        req_sql += "(Select * from bs_requisition ";
        req_sql += " where fsc_yr_id = '" + fsc_yr_id + "'";
        if (req_id.Trim() != "")
        {
            req_sql += " and req_id = '" + req_id + "'";
        }
        if (requisitioner != "")
            req_sql += " and requisitioner like '%" + requisitioner + "%'";
        req_sql += @") r left join (SELECT max(role_id) role_id,req_id
FROM bs_req_check 
group by req_id) ha on ha.req_id=r.req_id
inner join (select * from bs_req_check where req_chk_proceed=1 ) rcp on rcp.role_id=ha.role_id and rcp.req_id=r.req_id
";

        req_sql += " and req_date between '" + fromReqDate + "' and '" + toReqDate + "'";


        string req_dtl_sql = @"Select *,case when req_dtl_type=0 then 'Consumable'
        when  req_dtl_type=1 then 'Asset'
else 'One Time' end item_type from bs_requisition_details  where (req_dtl_in_stock=1 or po_received=1) and req_dtl_status<13 ";
        if (details.Replace("'", "''").Trim() != "")
        {
            req_dtl_sql += " and req_detail like '%" + details.Replace("'", "''").Trim() + "%'";
        }
        if (req_id.Trim() != "")
        {
            req_dtl_sql += " and req_id = '" + req_id + "'";
        }
        if (bgt_heading_id.Trim() != "")
        {
            req_dtl_sql += " and rd.bgt_heading_id = '" + bgt_heading_id + "'";
        }
        if (cost.Trim() != "")
        {
            req_dtl_sql += " and req_qty*req_rate ";
            if (Convert.ToInt32(costType) == 0)
                req_dtl_sql += " = ";
            else if (Convert.ToInt32(costType) < 0)
                req_dtl_sql += " < ";
            else
                req_dtl_sql += " > ";

            req_dtl_sql += cost.Trim();

        }
        if (quantity.Trim() != "")
        {
            req_dtl_sql += " and req_qty ";
            if (Convert.ToInt32(qtyType) == 0)
                req_dtl_sql += " = ";
            else if (Convert.ToInt32(qtyType) < 0)
                req_dtl_sql += " < ";
            else
                req_dtl_sql += " > ";

            req_dtl_sql += quantity.Trim();
        }
        if (item_type != "")
        {
            req_dtl_sql += " and req_dtl_type = '" + item_type + "' ";
        }


        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select rd.*,CAST((req_qty*req_rate) AS decimal(18,2)) req_cost,cast((req_qty*actual_rate) as decimal(18,2)) actual_cost,(bh.bgt_code + ': '+ bh.bgt_heading) heading,r.* from (" + req_dtl_sql + @") rd
inner join (" + req_sql + @") r on r.req_id=rd.req_id
inner join (select * from bs_requisition_statuses where req_proceed!=-1) rs on rs.req_status_id=rd.req_dtl_status
left join bs_budget_headings bh on bh.bgt_heading_id=rd.bgt_heading_id";

        return base.GetDataResult(sqlCmd);

    }

    public string DeliverReqItem(string req_dtl_id, string receiver)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELIVER_REQ_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@RECEIVER ", receiver);
        sqlCmd.Parameters.Add("@REMARKS ", "");
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
    public string DeliverReqItem(string req_dtl_id, string receiver, string remarks,string po_rate)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELIVER_REQ_DETAIL";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@RECEIVER ", receiver);
        sqlCmd.Parameters.Add("@REMARKS ", remarks);
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        sqlCmd.Parameters.Add("@po_rate", po_rate);
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
    public string DeliverReqItemPartially(string req_dtl_id, string receiver, string remarks, string delivered_qty)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELIVER_REQ_DETAIL_PARTIALLY";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        sqlCmd.Parameters.Add("@RECEIVER ", receiver);
        sqlCmd.Parameters.Add("@REMARKS ", remarks);
        sqlCmd.Parameters.Add("@DELVERED_QTY ", delivered_qty);
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

    public DataTable GetReqDispatchDtl(string req_dtl_id)
    {
        string whereCond = " where 1=1 ";
        if (req_dtl_id != null && req_dtl_id != "")
            whereCond += " and req_dtl_id='" + req_dtl_id.ToString() + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" Select rd.req_detail,(bh.bgt_code+': '+bh.bgt_heading) heading,r.requisitioner,f.fsc_yr,
case when rd.req_dtl_type='1' and an.ast_name is not null then an.ast_name
 when rd.req_dtl_type='0' and i.item_name is not null then i.item_name
 else rd.req_detail
 end req_item
,case when rd.req_dtl_type='1' then 'Asset'
 when rd.req_dtl_type='0' then 'Consumable'
 else 'One Time'
 end req_type,req_qty,req_qty*isnull(actual_rate,req_rate) req_cost,isnull(actual_rate,req_rate) req_rate,rd.item_id,req_rate from  (Select * from bs_requisition_details " + whereCond;
        sqlCmd.CommandText += @") rd inner join bs_requisition r on rd.req_id=r.req_id";
        sqlCmd.CommandText += @" inner join bs_fiscal_years f on r.fsc_yr_id=f.fsc_yr_id";
        sqlCmd.CommandText += @" inner join bs_budget_headings bh on rd.bgt_heading_id=bh.bgt_heading_id";
        sqlCmd.CommandText += @" left join bs_asset_names an on rd.req_dtl_type=1 and rd.item_id=an.ast_name_id";
        sqlCmd.CommandText += @" left join bs_inventory_items i on rd.req_dtl_type=0 and rd.item_id=i.item_id";
        return base.GetDataResult(sqlCmd);
    }
    public DataTable GetAssetDispatchList(string req_dtl_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SELECT_ASSET_DISPATH_LIST";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@REQ_DTL_ID ", req_dtl_id);
        return base.GetDataResult(sqlCmd);
        
    }
    #endregion
}
