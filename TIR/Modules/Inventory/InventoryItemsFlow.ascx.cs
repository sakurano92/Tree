using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class Modules_Inventory_InventoryItemsFlow : System.Web.UI.UserControl
{
    DataManagement dm = new DataManagement();
    InventoryMgr invMgr = new InventoryMgr();

    protected DataTable _DataSource
    {

        get
        {
            DateTime d;
            if (!DateTime.TryParseExact(dtpFromDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                // CustomMessage.DisplayMessage("Please ");
                dtpFromDate.Focus();
                return null;
            }
            if (!DateTime.TryParseExact(dtpToDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d))
            {
                dtpToDate.Focus();
                return null;
            }
            StringBuilder query = new StringBuilder();
            query.AppendFormat(@"
SELECT * FROM
(
 select SN,Type,Qty+opening_stock Qty,receiver,stock_inout_id,logtimestamp from(
select '' SN,'BALANCE' Type,ISNULL(SUM(qty*is_in),0) Qty,'In Stock' receiver,0 stock_inout_id,'{1}' logtimestamp from bs_stock_inout 
 WHERE 1 = 1 and item_id = {0} and logtimestamp< '{1}') num inner join (select item_id,opening_stock from bs_inventory_items) i on i.item_id={0}

union
SELECT cast(ROW_NUMBER() OVER(ORDER BY s.logtimestamp ) as varchar) as SN,
case when (is_in = 1) then 'IN' else 'OUT' end Type,qty,
case when is_in = 1 then upd.full_name else rd.received_by end receiver,s.stock_inout_id,s.logtimestamp
from bs_stock_inout s
left join bs_requisition_details rd on rd.req_dtl_id = s.req_dtl_id
left join bs_purchasereceive_details pd on pd.receive_detail_id = s.receive_detail_id
left join bs_users upd on upd.user_id = pd.received_by
 WHERE 1 = 1 and s.item_id = {0} and s.logtimestamp>='{1}' and s.logtimestamp<'{2}'
 Union
 select SN,Type,Qty+opening_stock Qty,receiver,stock_inout_id,logtimestamp from(
 select '' SN,'BALANCE' Type,ISNULL(SUM(qty*is_in),0) Qty,'Stock Balance' receiver,0 stock_inout_id,'{2}' logtimestamp from bs_stock_inout 
 WHERE 1 = 1 and item_id = {0} and logtimestamp< '{2}') num inner join (select item_id,opening_stock from bs_inventory_items) i on i.item_id={0}) data
ORDER BY LOGTIMESTAMP
 ", ddlItem.SelectedValue, dtpFromDate.Text, Convert.ToDateTime(dtpToDate.Text).AddDays(1).ToString("yyyy-MM-dd"));

            DataTable dt = dm.GetData(query.ToString());
            gv.RowHeaderColumn = gv.RowHeaderColumn + string.Format("~!!~{0} Stock In Out from {1} to {2}", ddlItem.SelectedItem.Text, dtpFromDate.Text, dtpToDate.Text);
            return dt;
        }
    }
    protected void CustomValidator1_ServerValidate(object sender, ServerValidateEventArgs e)
    {
        DateTime d;
        e.IsValid = DateTime.TryParseExact(e.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out d);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        dm.LoadDDL(ddlItemCat, "bs_item_categories", "itm_cat_id", "itm_cat_name", false);
        dm.LoadDDL(ddlItem, "bs_inventory_items", "item_id", "item_code", "item_category='" + ddlItemCat.SelectedValue + "'");
        LoadData();
        if (Request.QueryString["itemid"] != null)
            hdnItemId.Value = Request.QueryString["itemid"];

        LoadInitialData();
    }

    public void LoadData()
    {
        grdPagerTemplate.BindDetails(gv, _DataSource);
    }

    public void NavigationLink_Click(object sender, CommandEventArgs e)
    {
        grdPagerTemplate.GridViewPager(gv, _DataSource, e);
    }

    protected void LoadInitialData()
    {
        dtpFromDate.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
        dtpToDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
    }
    protected void ItemCatChanged(object sender, EventArgs e)
    {
        dm.LoadDDL(ddlItem, "bs_inventory_items", "item_id", "item_code", "item_category='" + ddlItemCat.SelectedValue + "'");
    }


    protected void btn_OnClick(object sender, EventArgs e)
    {
        LoadData();
    }



}