using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for PurchaseOrderMgr
/// </summary>
public class PurchaseOrderMgr : DatabaseHelper
{
    private SqlCommand sqlCmd;
	public PurchaseOrderMgr()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable ItemsToCreatePO()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select rd.*,(bh.bgt_code+': '+bh.bgt_heading) heading,
 case when rd.req_dtl_type='1' and an.ast_name is not null then '('+an.ast_name+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then '('+itm.item_name+')'
 else ''
 end req_item  from 
(Select cast(row_number() over (order by req_qty) as varchar(10)) as sn,cast(req_dtl_id as varchar(50)) req_dtl_id,bgt_code,req_detail,req_qty,cast(req_rate as varchar(50)) req_rate,req_qty*req_rate req_cost,item_id from bs_requisition_details where req_dtl_status = 10
Union all
Select '','','','Total',sum(req_qty),'',sum(req_qty*req_rate) req_cost,'','','' from bs_requisition_details where req_dtl_status = 10";
        return base.GetDataResult(sqlCmd);
       
    }

    public DataTable ItemsToCreatePO(string req_dtl_ids)
    {
        if (req_dtl_ids == "")
            return null;
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select rd.*,(bh.bgt_code+': '+bh.bgt_heading) heading,'' po_desc,
 case when rd.req_dtl_type='1' and an.ast_name is not null then '('+an.ast_name+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then '('+itm.item_name+')'
 else ''
 end req_item  from 
(Select cast(row_number() over (order by req_qty) as varchar(10)) as sn,cast(req_dtl_id as varchar(50)) req_dtl_id,bgt_code,req_detail,req_qty,req_dtl_type,cast(req_rate as varchar(50)) req_rate,cast((req_qty*req_rate)as decimal(18,2))req_cost,item_id,req_id from bs_requisition_details where req_dtl_status = 10 and req_dtl_id in ("
            + req_dtl_ids + @")) rd left join bs_budget_headings bh on bh.bgt_code=rd.bgt_code
left join bs_asset_names an on rd.item_id=an.ast_name_id and rd.req_dtl_type='1'
        left join bs_inventory_items itm on rd.item_id=itm.item_id and rd.req_dtl_type='0'
Union all
Select '','','','Total',sum(req_qty),'','',cast(sum(req_qty*req_rate)as decimal(18,2)) req_cost,'','','','','' from bs_requisition_details where req_dtl_status = 10  and req_dtl_id in (" + req_dtl_ids + ")";
        return base.GetDataResult(sqlCmd);

    }

    public DataTable POItems(string req_dtl_ids)
    {
        if (req_dtl_ids == "")
            return null;
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select rd.*,(bh.bgt_code+': '+bh.bgt_heading) heading,'' po_desc,
 case when rd.req_dtl_type='1' and an.ast_name is not null then '('+an.ast_name+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then '('+itm.item_name+')'
 else ''
 end req_item  from 
(Select cast(row_number() over (order by req_qty) as varchar(10)) as sn,cast(req_dtl_id as varchar(50)) req_dtl_id,bgt_code,req_detail,req_qty,req_dtl_type,cast(req_rate as varchar(50)) req_rate,req_qty*req_rate req_cost,item_id,req_id from bs_requisition_details where req_dtl_id in ("
            + req_dtl_ids + @")) rd left join bs_budget_headings bh on bh.bgt_code=rd.bgt_code
left join bs_asset_names an on rd.item_id=an.ast_name_id and rd.req_dtl_type='1'
        left join bs_inventory_items itm on rd.item_id=itm.item_id and rd.req_dtl_type='0'
Union all
Select '','','','Total',sum(req_qty),'','',sum(req_qty*req_rate) req_cost,'','','','','' from bs_requisition_details where req_dtl_id in (" + req_dtl_ids + ")";
        return base.GetDataResult(sqlCmd);

    }
    public DataTable POItemList(string po_id)
    {
        if (po_id == "")
            return null;
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"select cast(row_number() over (order by po_qty) as varchar(10)) as sn,po.req_dtl_id,
bh.bgt_code bgt_code,req_detail,cast(po_qty as varchar(10)) req_qty,po_type req_dtl_type,cast(po_rate as varchar(25)) req_rate,
cast((po_qty*po_rate)as decimal(18,2)) req_cost,(bh.bgt_code+': '+bh.bgt_heading) heading,po_desc,
 case when rd.req_dtl_type='1' and an.ast_name is not null then '('+an.ast_name+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then '('+itm.item_name+')'
 else ''
 end req_item,
 case when rd.req_dtl_type='1' and an.ast_name is not null then an.ast_name+' ('+req_detail+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then itm.item_name+' ('+req_detail+')'
 else req_detail
 end po_item_detail,req_id
from (select * from bs_po_items where po_id = '" + po_id+ @"') po
left join (select req_id,req_dtl_id,req_detail,bgt_heading_id,req_dtl_type,item_id from bs_requisition_details ) rd on po.req_dtl_id=rd.req_dtl_id
left join bs_budget_headings bh on bh.bgt_heading_id=rd.bgt_heading_id
left join bs_asset_names an on rd.item_id=an.ast_name_id and rd.req_dtl_type='1'
        left join bs_inventory_items itm on rd.item_id=itm.item_id and rd.req_dtl_type='0'
Union all
Select '','','','Total','','','',cast(sum(po_qty*po_rate) as decimal(18,2)),'','','','','' from bs_po_items where po_id='" + po_id + "'";
        return base.GetDataResult(sqlCmd);

    }

    public DataTable ItemsToCreatePO(string fsc_yr_id,string bgt_heading_id,string details, string req_id, string quantity, string qtyType, string cost, string costType)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select rd.*,cast((req_qty*req_rate)as decimal(18,2)) req_cost,(bh.bgt_code + ': '+ bh.bgt_heading) heading,r.requisitioner,
 case when rd.req_dtl_type='1' and an.ast_name is not null then '('+an.ast_name+')'
 when rd.req_dtl_type='0' and itm.item_name is not null then '('+itm.item_name+')'
 else ''
 end req_item from bs_requisition_details rd
inner join bs_requisition r on r.req_id=rd.req_id
left join bs_asset_names an on rd.item_id=an.ast_name_id and rd.req_dtl_type='1'
        left join bs_inventory_items itm on rd.item_id=itm.item_id and rd.req_dtl_type='0'
left join bs_budget_headings bh on bh.bgt_heading_id=rd.bgt_heading_id where req_dtl_status = 10 ";
        if (details.Replace("'","''").Trim() != "")
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
            if(Convert.ToInt32(costType)==0)
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

    public string SavePO(string po_id,string po_status_id, string supplier, string deliverTo, string POdate, string deliverByDate, string desc, string fsc_yr_id, string req_dtl_ids)
    {
        if (po_id == "") po_id = "0";
        //supplier = supplier.Trim().Replace("'", "''");
        //deliverTo = deliverTo.Trim().Replace("'", "''");
        //desc = desc.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_PO";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_DATE", Convert.ToDateTime(POdate).Date);
        sqlCmd.Parameters.Add("@DELIVERY_DATE", Convert.ToDateTime(deliverByDate).Date);
        sqlCmd.Parameters.Add("@SUPPLIER", supplier);
        sqlCmd.Parameters.Add("@DELIVERTO", deliverTo);
        sqlCmd.Parameters.Add("@DESCRIPTION", desc);
        sqlCmd.Parameters.Add("@PO_ID", po_id);
        sqlCmd.Parameters.Add("@FSC_YR_ID", fsc_yr_id);
        sqlCmd.Parameters.Add("@PO_STATUS_ID", po_status_id);
        sqlCmd.Parameters.Add("@REQ_DTL_IDS", req_dtl_ids);
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
    public string DispatchPO(string po_id)
    {
        if (po_id == "") { po_id = "0"; return "NO PO ID"; }
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DISPATCH_PO";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_ID", po_id);
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        try
        {
            return sqlCmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string DeletePO(string po_id)
    {
        if (po_id == "" || po_id == "0")
        {
            return "Select Purchase Order";
        }
      
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELETE_PO";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
      
        sqlCmd.Parameters.Add("@PO_ID", po_id);
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        try
        {
            string s= sqlCmd.ExecuteScalar().ToString();
            return s;
            

        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public DataTable getPoStatus(int po_id)
    {
        if (po_id==null)
            return null;
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select * from bs_purchase_order where po_id='" + po_id + "'";
        return base.GetDataResult(sqlCmd);

    }
    public string DeletePO1(string po_id)
    {
        if (po_id == "" || po_id == "0")
        {
            return "Select Purchase Order";
        }

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELETE_PO1";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();

        sqlCmd.Parameters.Add("@POrder_ID", po_id);
        sqlCmd.Parameters.Add("@USER_ID", SessionValues.UserIdSession);
        try
        {
            string s = sqlCmd.ExecuteScalar().ToString();
            return s;


        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public string SavePOItem(string po_id, string req_dtl_id, string poQty, string poRate, string podesc, string poType)
    {
        if (po_id == "" || po_id == "0") return "No PO ID";
       // podesc = podesc.Trim().Replace("'", "''");
       
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_PO_ITEM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_QTY", poQty);
        sqlCmd.Parameters.Add("@PO_RATE", poRate);
        sqlCmd.Parameters.Add("@PO_TYPE", poType);
        sqlCmd.Parameters.Add("@PO_DESC", podesc);
        sqlCmd.Parameters.Add("@PO_ID", po_id);
        sqlCmd.Parameters.Add("@REQ_DTL_ID", req_dtl_id);
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
    public string RemovePOItem(string po_id, string req_dtl_id)
    {
        if (po_id == "" || po_id == "0") return "No PO ID";

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_REMOVE_PO_ITEM";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_ID", po_id);
        sqlCmd.Parameters.Add("@REQ_DTL_ID", req_dtl_id);
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

    public string ApprovePO(string po_id, string req_dtl_ids,int proceed,string comment)
    {
        if (po_id == "" || po_id == "0")
        {
            return "error";
        }
       // comment = comment.Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_APPROVE_PO";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_ID", po_id);
        sqlCmd.Parameters.Add("@REQ_DTL_IDS", req_dtl_ids);
        sqlCmd.Parameters.Add("@ROLE_ID", SessionValues.RoleIdSession);
        sqlCmd.Parameters.Add("@PROCEED", proceed);
        sqlCmd.Parameters.Add("@APPROVAL_CMT", comment);
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

    public DataTable POList()
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select * from bs_purchase_order p left join bs_po_statuses s on p.po_status_id=s.po_status_id order by po_id desc";
        return base.GetDataResult(sqlCmd);
    }
    public DataTable POList(string fsc_yr_id, string supplier, string deliverto, string description,
        string fromPODate, string toPOdate, string fromDeliverUpto, string toDeliverUpto, string POstatus)
    {

        supplier = supplier.Trim().Replace("'", "''");
        deliverto = deliverto.Trim().Replace("'", "''");
        description = description.Trim().Replace("'", "''");
        if (fromPODate.Trim() == "")
        {
            fromPODate = "1900-01-01";
        }
        if (toPOdate.Trim() == "")
        {
            toPOdate = "9999-12-31";
        }
        
        if (fromDeliverUpto.Trim() == "")
        {
            fromDeliverUpto = "1900-01-01";
        } 
        if (toDeliverUpto.Trim() == "")
        {
            toDeliverUpto = "9999-12-31";
        }
        
        

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText
            = @"Select p.*,s.po_status,
                case when ISNULL(po_items_num,0)=0 then 'true' else 'false' end allow_delete 
                from bs_purchase_order p 
                left join bs_po_statuses s on p.po_status_id=s.po_status_id 
                left join (select COUNT(po_id) po_items_num,po_id from bs_po_items group by po_id) num on p.po_id=num.po_id 
                ";
        sqlCmd.CommandText += " where s.po_status_id!=8 and po_fsc_yr_id='" + fsc_yr_id + "'";
        
        if (supplier != "")
            sqlCmd.CommandText += " and po_supplier like '%" + supplier + "%'";
        if (deliverto != "")
            sqlCmd.CommandText += " and po_deliver_to like '%" + deliverto + "%'";
        if (description != "")
            sqlCmd.CommandText += " and po_description like '%" + description + "%'";
        if (POstatus != "")
            sqlCmd.CommandText += " and p.po_status_id = '" + POstatus + "'";
        //if (!SessionValues.IsRM)
          //  sqlCmd.CommandText += " and p.po_status_id != '0'";//created by rm not proceeded

        sqlCmd.CommandText += " and po_date between '" + fromPODate + "' and '" + toPOdate + "'";
        sqlCmd.CommandText += " and po_deliver_upto between '" + fromDeliverUpto + "' and '" + toDeliverUpto + "'";

        sqlCmd.CommandText += " order by po_id desc";
        return base.GetDataResult(sqlCmd);
    }



    public DataTable POList_new(string fsc_yr_id, string supplier, string deliverto, string description,
       string fromPODate, string toPOdate, string fromDeliverUpto, string toDeliverUpto, string POstatus, string search_by_items,string user_role,bool ownonly)
    {

        supplier = supplier.Trim().Replace("'", "''");
        deliverto = deliverto.Trim().Replace("'", "''");
        description = description.Trim().Replace("'", "''");
        if (fromPODate.Trim() == "")
        {
            fromPODate = "1900-01-01";
        }
        if (toPOdate.Trim() == "")
        {
            toPOdate = "9999-12-31";
        }

        if (fromDeliverUpto.Trim() == "")
        {
            fromDeliverUpto = "1900-01-01";
        }
        if (toDeliverUpto.Trim() == "")
        {
            toDeliverUpto = "9999-12-31";
        }



        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText
            = @"Select  distinct p.*,s.po_status,
                case when ISNULL(po_items_num,0)=0 then 'true' else 'false' end allow_delete 
               from bs_purchase_order p 
                left join bs_po_statuses s on p.po_status_id=s.po_status_id 
                left join (select COUNT(po_id) po_items_num,po_id from bs_po_items group by po_id) num on p.po_id=num.po_id 
             "; if (search_by_items != "") { sqlCmd.CommandText += " inner join (select   po_id,req_detail from bs_requisition_details where po_id is not null and req_detail like'%" + search_by_items + "%') rd on rd.po_id=p.po_id"; }

        sqlCmd.CommandText += " where po_fsc_yr_id='" + fsc_yr_id + "'";

        if (supplier != "")
            sqlCmd.CommandText += " and po_supplier like '%" + supplier + "%'";
        if (deliverto != "")
            sqlCmd.CommandText += " and po_deliver_to like '%" + deliverto + "%'";
        if (description != "")
            sqlCmd.CommandText += " and po_description like '%" + description + "%'";
        if (POstatus != "")
            sqlCmd.CommandText += " and p.po_status_id = '" + POstatus + "'";
        //if (search_by_items == "" && POstatus == "" && supplier == "" && deliverto == "" && description == "")
        //    sqlCmd.CommandText += "and s.po_status_id!=8";

        if (ownonly)
        {
            if (user_role == "6")
            {
                sqlCmd.CommandText += "and (p.po_status_id=1 or p.po_status_id=2)";
            }
            else if (user_role == "8")
            {
                sqlCmd.CommandText += "and (p.po_status_id=1 or p.po_status_id=4)";
            }
            else if (user_role == "5")
            {
                sqlCmd.CommandText += "and (p.po_status_id=1 or p.po_status_id=4 or p.po_status_id=2)";
            }
            else if (user_role == "3")
            {
                sqlCmd.CommandText += "and p.po_status_id<6 ";
            }
        }


        //if (!SessionValues.IsRM)
        //  sqlCmd.CommandText += " and p.po_status_id != '0'";//created by rm not proceeded

        sqlCmd.CommandText += " and p.po_status_id !=11 and po_date between '" + fromPODate + "' and '" + toPOdate + "'";
        sqlCmd.CommandText += " and po_deliver_upto between '" + fromDeliverUpto + "' and '" + toDeliverUpto + "'";
       //if( POstatus == "")
       //{
       //    sqlCmd.CommandText += " and p.po_status_id !=11";
       //}
        sqlCmd.CommandText += "order by po_id desc";
        return base.GetDataResult(sqlCmd);
    }
    public DataTable DispatchedPOList_new(string fsc_yr_id, string supplier, string deliverto, string description,
       string fromPODate, string toPOdate, string fromDeliverUpto, string toDeliverUpto, string by_items)
    {

        supplier = supplier.Trim().Replace("'", "''");
        deliverto = deliverto.Trim().Replace("'", "''");
        description = description.Trim().Replace("'", "''");
        if (fromPODate.Trim() == "")
        {
            fromPODate = "1900-01-01";
        }
        if (toPOdate.Trim() == "")
        {
            toPOdate = "9999-12-31";
        }

        if (fromDeliverUpto.Trim() == "")
        {
            fromDeliverUpto = "1900-01-01";
        }
        if (toDeliverUpto.Trim() == "")
        {
            toDeliverUpto = "9999-12-31";
        }


        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText
            = @"Select p.po_id,p.po_code + ' '+cast(convert(date,p.po_date,103) as varchar(10))+' (' +s.po_status+') '+ cast(num.po_items_num as varchar(10))+' items ' po_detail
                
                from bs_purchase_order p 
                left join bs_po_statuses s on p.po_status_id=s.po_status_id 
                left join (select COUNT(po_id) po_items_num,po_id from bs_po_items group by po_id) num on p.po_id=num.po_id 
                    ";
        if (by_items != "")
        {
            sqlCmd.CommandText += "INNER join (select   po_id,req_detail from bs_requisition_details where po_id is not null  AND REQ_DETAIL IS NOT NULL  AND REQ_DETAIL LIKE '%" + by_items + "%'  ) rd on rd.po_id=p.po_id ";
        }
        sqlCmd.CommandText += "  where po_fsc_yr_id='" + fsc_yr_id + "'";

        if (supplier != "")
            sqlCmd.CommandText += " and po_supplier like '%" + supplier + "%'";
        if (deliverto != "")
            sqlCmd.CommandText += " and po_deliver_to like '%" + deliverto + "%'";
        if (description != "")
            sqlCmd.CommandText += " and po_description like '%" + description + "%'";
        if (supplier == "" && deliverto == "" && description == "" && by_items == "")
            sqlCmd.CommandText += "and  s.po_status_id!=8";

        sqlCmd.CommandText += " and p.po_status_id!=11 and p.po_status_id >= 6";

        sqlCmd.CommandText += " and po_date between '" + fromPODate + "' and '" + toPOdate + "'";
        sqlCmd.CommandText += " and po_deliver_upto between '" + fromDeliverUpto + "' and '" + toDeliverUpto + "'";

        sqlCmd.CommandText += " order by p.po_status_id asc,po_id desc";
        return base.GetDataResult(sqlCmd);
    }

    public DataTable DispatchedPOList(string fsc_yr_id, string supplier, string deliverto, string description,
        string fromPODate, string toPOdate, string fromDeliverUpto, string toDeliverUpto)
    {

        supplier = supplier.Trim().Replace("'", "''");
        deliverto = deliverto.Trim().Replace("'", "''");
        description = description.Trim().Replace("'", "''");
        if (fromPODate.Trim() == "")
        {
            fromPODate = "1900-01-01";
        }
        if (toPOdate.Trim() == "")
        {
            toPOdate = "9999-12-31";
        }

        if (fromDeliverUpto.Trim() == "")
        {
            fromDeliverUpto = "1900-01-01";
        }
        if (toDeliverUpto.Trim() == "")
        {
            toDeliverUpto = "9999-12-31";
        }


        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText
            = @"Select p.po_id,p.po_code + ' '+cast(convert(date,p.po_date,103) as varchar(10))+' (' +s.po_status+') '+ cast(num.po_items_num as varchar(10))+' items ' po_detail
                
                from bs_purchase_order p 
                left join bs_po_statuses s on p.po_status_id=s.po_status_id 
                left join (select COUNT(po_id) po_items_num,po_id from bs_po_items group by po_id) num on p.po_id=num.po_id 
                ";
        sqlCmd.CommandText += " where po_fsc_yr_id='" + fsc_yr_id + "'";

        if (supplier != "")
            sqlCmd.CommandText += " and po_supplier like '%" + supplier + "%'";
        if (deliverto != "")
            sqlCmd.CommandText += " and po_deliver_to like '%" + deliverto + "%'";
        if (description != "")
            sqlCmd.CommandText += " and po_description like '%" + description + "%'";

        sqlCmd.CommandText += " and p.po_status_id >= 6";

        sqlCmd.CommandText += " and po_date between '" + fromPODate + "' and '" + toPOdate + "'";
        sqlCmd.CommandText += " and po_deliver_upto between '" + fromDeliverUpto + "' and '" + toDeliverUpto + "'";

        sqlCmd.CommandText += " order by p.po_status_id asc,po_id desc";
        return base.GetDataResult(sqlCmd);
    }

    public DataTable FetchPODetails(string POid)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select p.*,rm.full_name rm_name,acc.full_name accountant_name,prin.full_name principal_name from bs_purchase_order p
left join bs_users rm on rm.user_id=p.po_created_by
left join bs_users acc on acc.user_id=p.po_accountant
left join bs_users prin on prin.user_id=p.po_principal where po_id='" + POid + "'";
        return base.GetDataResult(sqlCmd);
    }

    public string FetchPOItems(string POid)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select req_dtl_id from bs_requisition_details where po_id='" + POid + "'";
        DataTable dt= base.GetDataResult(sqlCmd);
        string items = "";
        foreach (DataRow dr in dt.Rows)
        {
            items += dr["req_dtl_id"].ToString() + ",";
        }
        System.Text.StringBuilder str = new System.Text.StringBuilder(items);
        if (items.Length > 0)
            items = str.Remove(str.Length - 1, 1).ToString();
        return items;
    }
    public DataTable GetPOApprovalLog(string po_id)
    {
        string whereCond = "where  po_id='" + po_id + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select comment,logtimestamp,u.full_name,r.role_name,
case when approved=1 then 'Approved'
 when approved=0 then 'Back To RM'
else 'Rejected' end approved from
(select * from bs_po_approval_log
" + whereCond + @" ) po 
left join bs_users u on u.user_id=po.modified_by 
left join bs_user_roles r on r.role_id=po.role_id 
order by logtimestamp desc";
        return base.GetDataResult(sqlCmd);
    }

    public string PurchaseReceived(string po_item_id, string received_qty, string remarks,string rate)
    {

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_RECEIVE_PO";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_ITEM_ID", po_item_id);
        sqlCmd.Parameters.Add("@RECEIVEDQTY", received_qty);
        sqlCmd.Parameters.Add("@REMARKS", remarks);
        sqlCmd.Parameters.Add("@po_rate", rate);
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

    public string PurchaseReceived_Canceal(string po_item_id, string received_qty, string remarks, string rate)
    {

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_RECEIVE_PO_canceal";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("@PO_ITEM_ID", po_item_id);
        sqlCmd.Parameters.Add("@RECEIVEDQTY", received_qty);
        sqlCmd.Parameters.Add("@REMARKS", remarks);
        sqlCmd.Parameters.Add("@po_rate", rate);
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
}