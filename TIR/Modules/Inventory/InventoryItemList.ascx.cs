using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Modules_Inventory_InventoryItemList : System.Web.UI.UserControl
{
    DataManagement dm = new DataManagement();
    InventoryMgr invMgr = new InventoryMgr();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        DataManagement dm = new DataManagement();
        dm.LoadDDL(ddlItemCat, "bs_item_categories", "itm_cat_id", "itm_cat_name", true);
        LoadData();
        if (Request.QueryString["itemid"] != null)
            _ItemId = Request.QueryString["itemid"];

        LoadInitialData();
        txtItemCode.Focus();
    }

    protected DataTable _DataSource
    {
        get
        {
            StringBuilder query = new StringBuilder(@"SELECT ROW_NUMBER() OVER(ORDER BY item_id) as SN,item_id,item_code,item_name,item_desc,item_category,i.unit_id
                              ,vendor_id,cost_price,min_stock,last_bal,created_by,created_date,itm_cat_name,frozen_num
                              ,modified_by,modified_date,bs_units.unit_name,last_bal*cost_price as tot_bal
                              FROM bs_inventory_items i LEFT JOIN bs_units ON
                              i.unit_id = bs_units.unit_id 
                            LEFT JOIN bs_item_categories ic ON
                              i.item_category = ic.itm_cat_id WHERE 1=1 ");
            if (txtName.Text.Trim() != "")
                query.Append(" and item_name like '%" + txtName.Text.Trim().Replace("'", "''") + "%'");
            if (txtCode.Text.Trim() != "")
                query.Append(" and item_code like '%" + txtCode.Text.Trim().Replace("'", "''") + "%'");
            if (ddlItemCat.SelectedValue != "")
                query.Append(" and i.item_category = '" + ddlItemCat.SelectedValue + "'");
            if (ddlStockBal.SelectedValue == "1")
                query.Append(" and i.last_bal >= i.min_stock ");
            else if (ddlStockBal.SelectedValue == "0")
                query.Append(" and i.last_bal < i.min_stock ");

            DataTable dt = dm.GetData(query.ToString());
            gv.RowHeaderColumn = string.Format("Inventory Items List~!!~{0}  {1} ",
                ddlItemCat.SelectedValue == "" ? "" : "Category: " + ddlItemCat.SelectedItem.Text
                , ddlStockBal.SelectedValue == "" ? "" : "Balance: " + ddlStockBal.SelectedItem.Text);
            return dt;
        }
    }

    public void LoadData()
    {
        grdPagerTemplate.BindDetails(gv, _DataSource);

    }

    public void NavigationLink_Click(object sender, CommandEventArgs e)
    {
        grdPagerTemplate.GridViewPager(gv, _DataSource, e);
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton cmdBtn = (ImageButton)sender;
        _ItemId = cmdBtn.CommandArgument;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton cmdBtn = (ImageButton)sender;
        invMgr.DeleteInventoryDetails(cmdBtn.CommandArgument);
        LoadData();
    }


    public string _UserID
    {
        get { return SessionValues.UserIdSession; }
    }
    public string _CurrDate
    {
        get { return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); }
    }
    public string _ItemId
    {
        get { return hdnItemId.Value; }
        set { hdnItemId.Value = value; if (hdnItemId.Value != "") LoadItemDetailsByID(); }
    }
    public string _ItemCode
    {
        get { return txtItemCode.Text; }
        set { txtItemCode.Text = value; }
    }
    public string _ItemName
    {
        get { return txtItemName.Text; }
        set { txtItemName.Text = value; }
    }
    public string _ItemDesc
    {
        get { return txtDescription.Text; }
        set { txtDescription.Text = value; }
    }
    public string _Quantity
    {
        get { return txtQuantity.Text; }
        set { txtQuantity.Text = value; }
    }
    public string _ItemCategory
    {
        get { return ddlItemCategory.SelectedValue; }
        set
        {
            if (value == string.Empty)
                ddlItemCategory.SelectedIndex = -1;
            else
                ddlItemCategory.SelectedValue = value;
        }
    }
    public string _ItemUnit
    {
        get { return ddlUnit.SelectedValue; }
        set
        {
            if (value == string.Empty || value == "0")
                ddlUnit.SelectedIndex = -1;
            else
                ddlUnit.SelectedValue = value;
        }
    }

    public string _CostPrice
    {
        get { return txtCostPrice.Text; }
        set { txtCostPrice.Text = value; }
    }
    public string _MinStock
    {
        get { return txtMinStock.Text; }
        set { txtMinStock.Text = value; }
    }
    public string _OpeningStock
    {
        get { return txtOpeningBalance.Text; }
        set { txtOpeningBalance.Text = value; }
    }


    protected void LoadInitialData()
    {
        dm.LoadDDL(ddlUnit, "bs_units", "unit_id", "unit_name", true, "");
        dm.LoadDDL(ddlItemCategory, "bs_item_categories", "itm_cat_id", "itm_cat_name", false, "");
    }

    protected void ClearForm()
    {
        _ItemId = string.Empty;
        _ItemCode = string.Empty;
        _ItemName = string.Empty;
        _ItemDesc = string.Empty;
        _ItemCategory = string.Empty;
        _ItemUnit = string.Empty;
        _CostPrice = "0.00";
        _OpeningStock = "0.00";
        _MinStock = "0.00";
        _Quantity = "0";
        txtItemCode.Focus();
    }


    protected bool Save()
    {
        if (!ItemCodeExists())
        {
            if (_ItemId == string.Empty)
            {
                invMgr.SaveInventoryDetails(_ItemCode, _ItemName, _ItemDesc, _ItemCategory, _ItemUnit, string.Empty, _CostPrice, _MinStock, _OpeningStock, SessionValues.UserIdSession, _Quantity);
            }
            else
            {
                invMgr.UpdateInventoryDetails(_ItemId, _ItemCode, _ItemName, _ItemDesc, _ItemCategory, _ItemUnit, string.Empty, _CostPrice, _MinStock, _OpeningStock, SessionValues.UserIdSession, _Quantity);
            }
            return true;
        }
        return false;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Save())
        {
            CustomMessage.DisplayMessage(this.Page, "Item added successfully.", CustomMessage.MessageType.SUCCESS);
            ClearForm();
            LoadData();
        }
    }
    protected void btn_OnClick(object sender, EventArgs e)
    {
        Button cmdBtn = (Button)sender;

        switch (cmdBtn.CommandName)
        {
            case "cancel":
                ClearForm();
                break;
            case "save":
                if (Save())
                {
                    ClearForm();
                    LoadData();
                }

                break;
            case "search":
                LoadData();
                break;
            default:
                break;
        }
    }

    protected void LoadItemDetailsByID()
    {
        string query = @"SELECT item_id,item_code,quantity,item_name,item_desc,item_category,bs_inventory_items.unit_id,opening_stock
                          ,vendor_id,cost_price,min_stock,last_bal,created_by,created_date
                          ,modified_by,modified_date,bs_units.unit_name
                          FROM bs_inventory_items LEFT JOIN bs_units ON
                          bs_inventory_items.unit_id = bs_units.unit_id WHERE item_id = '" + _ItemId + "'";
        DataTable dt = dm.GetData(query);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            _ItemCode = dr["item_code"].ToString();
            _ItemName = dr["item_name"].ToString();
            _ItemDesc = dr["item_desc"].ToString();
            _ItemCategory = dr["item_category"].ToString();
            _ItemUnit = dr["unit_id"].ToString();
            _CostPrice = dr["cost_price"].ToString();
            _OpeningStock = dr["opening_stock"].ToString();
            _MinStock = dr["min_stock"].ToString();
            _Quantity = dr["quantity"].ToString();

        }
    }

    private bool ItemCodeExists()
    {
        string query = @"SELECT item_id from bs_inventory_items WHERE item_code = '" + _ItemCode + "'";
        if (_ItemId != "")
        {
            query += " and item_id!='" + _ItemId + "'";
        }
        DataTable dt = dm.GetData(query);
        if (dt.Rows.Count > 0)
        {
            CustomMessage.DisplayMessage(this.Page, "Item Code Already Exists.", CustomMessage.MessageType.WARNING);
            txtItemCode.Focus();
            return true;
        }
        else
        {
            return false;
        }
    }
}