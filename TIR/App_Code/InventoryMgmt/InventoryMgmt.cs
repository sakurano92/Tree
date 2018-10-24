using System;
using System.Collections.Generic;
////using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for InventoryMgr
/// </summary>
public class InventoryMgr: DatabaseHelper
{
    SqlCommand sqlCmd;
	public InventoryMgr()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void SaveInventoryDetails(string itemCode, string itemName, string itemDesc, string itemCategory, string unitID, string vendorID, string costPrice, string minStock, string openingStock, string userID)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        sqlCmd.CommandText = "SP_INSERT_INVENTORY_DETAIL";
        sqlCmd.Parameters.AddWithValue("@ITEM_CODE", itemCode);
        sqlCmd.Parameters.AddWithValue("@ITEM_NAME", itemName);
        sqlCmd.Parameters.AddWithValue("@ITEM_DESC", itemDesc);
        sqlCmd.Parameters.AddWithValue("@ITEM_CATEGORY", itemCategory);
        sqlCmd.Parameters.AddWithValue("@UNIT_ID", unitID);
        sqlCmd.Parameters.AddWithValue("@VENDOR_ID", vendorID);
        sqlCmd.Parameters.AddWithValue("@COST_PRICE", costPrice);
        sqlCmd.Parameters.AddWithValue("@MIN_STOCK", minStock);
        sqlCmd.Parameters.AddWithValue("@CREATED_BY", userID);
        sqlCmd.Parameters.AddWithValue("@OPENING_STOCK", openingStock);

        sqlCmd.ExecuteNonQuery();

    }

    public void UpdateInventoryDetails(string itemID, string itemCode, string itemName, string itemDesc, string itemCategory, string unitID, string vendorID, string costPrice, string minStock, string openingStock, string userID)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        sqlCmd.CommandText = "SP_UPDATE_INVENTORY_DETAIL";
        sqlCmd.Parameters.AddWithValue("@ITEM_ID", itemID);
        sqlCmd.Parameters.AddWithValue("@ITEM_CODE", itemCode);
        sqlCmd.Parameters.AddWithValue("@ITEM_NAME", itemName);
        sqlCmd.Parameters.AddWithValue("@ITEM_DESC", itemDesc);
        sqlCmd.Parameters.AddWithValue("@ITEM_CATEGORY", itemCategory);
        sqlCmd.Parameters.AddWithValue("@UNIT_ID", unitID);
        sqlCmd.Parameters.AddWithValue("@VENDOR_ID", vendorID);
        sqlCmd.Parameters.AddWithValue("@COST_PRICE", costPrice);
        sqlCmd.Parameters.AddWithValue("@MIN_STOCK", minStock);
        sqlCmd.Parameters.AddWithValue("@MODIFIED_BY", userID);
        sqlCmd.Parameters.AddWithValue("@OPENING_STOCK", openingStock);

        sqlCmd.ExecuteNonQuery();

    }

    public void DeleteInventoryDetails(string itemID)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
        sqlCmd.CommandText = "SP_DELETE_INVENTORY_DETAIL";
        sqlCmd.Parameters.AddWithValue("@ITEM_ID", itemID);
        sqlCmd.ExecuteNonQuery();
    }

    public DataRow GetItemDetail(string item_id)
    {
        sqlCmd = base.GetSqlCommand();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = @" SELECT * from bs_inventory_items where item_id = '" + item_id + "'";
        DataTable dt = base.GetDataResult(sqlCmd);
        if (dt.Rows.Count > 0)
            return dt.Rows[0];
        else return null;
    }
}