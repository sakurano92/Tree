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
/// Summary description for Budget Heading
/// </summary>
public class SupplierMgr : DatabaseHelper
{
    private SqlCommand sqlCmd;

    public SupplierMgr()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Get Budget heading detail. For edit form
    /// </summary>
    /// <param name="supplierId"></param>
    /// <returns></returns>
    public DataRow GetSupplierDetail(string supplierId)
    {
        supplierId = supplierId.ToLower().Trim().Replace("'", "''");

        string whereCond = " where 1=1 ";
        if (supplierId != "")
            whereCond += " and supplier_id='" + supplierId + "'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select * from bs_suppliers " + whereCond;
        DataTable dt = base.GetDataResult(sqlCmd);
        if (dt.Rows.Count > 0)
            return dt.Rows[0];
        else
            return null;
    }


    /// <summary>
    /// delete Budget heading. By stored procedure. Saves in log table
    /// </summary>
    /// <param name="supplierId"></param>
    /// <returns></returns>
    public string DeleteSupplier(string supplierId)
    {
       // supplierId = supplierId.ToLower().Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_DELETE_SUPPLIER";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("SUPPLIER_ID", supplierId);
        sqlCmd.Parameters.Add("DELETED_BY", SessionValues.UserIdSession);
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
    /// Get Budget Headings. For search
    /// </summary>
    /// <param name="category"></param>
    /// <param name="heading"></param>
    /// <returns></returns>
    public DataTable GetSuppliers(string name, string address)
    {
        name = name.ToLower().Trim().Replace("'", "''");
        address = address.ToLower().Trim().Replace("'", "''");


        string whereCond = " where 1=1 ";
        if (address != "")
            whereCond += " and lower(supplier_address) like '%" + address + "%'";
        if (name != "")
            whereCond += " and lower(supplier_name) like '%" + name + "%'";
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select * from  bs_suppliers " + whereCond;
        return base.GetDataResult(sqlCmd);
    }




    /// <summary>
    /// Insert/Update budget heading by stored procedure.  Saves in log table
    /// </summary>
    /// <param name="supplierId"></param>
    /// <param name="code"></param>
    /// <param name="heading"></param>
    /// <param name="category"></param>
    /// <param name="detail"></param>
    /// <returns></returns>
    public string SaveSupplier(string supplierId, string code, string name, string address, string contact, string detail)
    {
        //supplierId = supplierId.ToLower().Trim().Replace("'", "''");
        //code = code.ToUpper().Trim().Replace("'", "''");
        //heading = heading.Trim().Replace("'", "''");
        //category = category.ToLower().Trim().Replace("'", "''");
        //detail = detail.Trim().Replace("'", "''");

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandText = "SP_SAVE_SUPPLIER";
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add("SUPPLIER_ID", supplierId);
        sqlCmd.Parameters.Add("supplier_code", code);
        sqlCmd.Parameters.Add("SUPPLIER_NAME", name);
        sqlCmd.Parameters.Add("SUPPLIER_DETAIL", detail);
        sqlCmd.Parameters.Add("SUPPLIER_ADDRESS", address);
        sqlCmd.Parameters.Add("SUPPLIER_CONTACT", contact);
        sqlCmd.Parameters.Add("USER_ID", SessionValues.UserIdSession);
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
    /// To Check weather same budget code already exist or not
    /// </summary>
    /// <param name="code"></param>
    /// <param name="supplierId"></param>
    /// <returns></returns>
    public bool SupplierCodeExists(string code, string supplierId)
    {
        supplierId = supplierId.ToLower().Trim().Replace("'", "''");

        code = code.ToLower().Trim().Replace("'", "''");
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select count(supplier_code) from bs_suppliers where lower(supplier_code)='" + code + "'";
        if (supplierId != "")
        {
            sqlCmd.CommandText += " and supplier_id!='" + supplierId + "'";
        }
        string count = base.ExecuteScaler(sqlCmd);
        if (count == "0")
            return false;
        else
            return true;
    }

    /// <summary>
    ///TO Check Weather Same Budget heading exist for same category or not. 
    /// </summary>
    /// <param name="heading"></param>
    /// <param name="bgt_cat_id"></param>
    /// <param name="supplierId"></param>
    /// <returns></returns>
    public bool SupplierExists(string supplierName, string supplierId)
    {
        supplierName = supplierName.ToLower().Trim().Replace("'", "''");
        supplierId = supplierId.Trim().Replace("'", "''");

        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @"Select count(supplier_code) from bs_suppliers where lower(supplier_name)='" + supplierName + "'";
        if (supplierId != "")
        {
            sqlCmd.CommandText += " and supplier_id!='" + supplierId.Trim() + "'";
        }

        string count = base.ExecuteScaler(sqlCmd);
        if (count == "0")
            return false;
        else
            return true;
    }
    public bool isSupplierUsed(string supplier_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = string.Format(@"select 1 from bs_purchase_order where supplier_id='{0}'", supplier_id);
        try
        {
            object ret = sqlCmd.ExecuteScalar();

            if (ret == null || ret.ToString() == "")
                return false;
            else
                return true;
        }
        catch (Exception ex)
        {
            return true;
        }
    }
}
