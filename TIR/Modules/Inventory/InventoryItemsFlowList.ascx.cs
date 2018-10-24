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

select * from 
(
select '' SN,'BALANCE' Type,sum(isnull((Qty),0)+(opening_stock*cost_price)) price,'In Stock ' receiver,0 stock_inout_id,'{1}' logtimestamp,'0' po_rate from(
 select ISNULL(SUM((qty*is_in)*po_rate),0) Qty ,item_id from bs_stock_inout 
 WHERE 1 = 1  and logtimestamp< '{1}' group by item_id) num right outer join (select item_id,opening_stock,cost_price from bs_inventory_items) i on i.item_id=num.item_id 

UNION

SELECT cast(ROW_NUMBER() OVER(ORDER BY s.logtimestamp ) as varchar) as SN,
case when (is_in = 1) then 'IN' else 'OUT' end Type,isnull((po_rate*qty),0) as price,
case when is_in = 1 then upd.full_name else rd.received_by end receiver,s.stock_inout_id,s.logtimestamp,isnull((s.po_rate),0)
from bs_stock_inout s
left join bs_requisition_details rd on rd.req_dtl_id = s.req_dtl_id
left join bs_purchasereceive_details pd on pd.receive_detail_id = s.receive_detail_id
left join bs_users upd on upd.user_id = pd.received_by
inner join bs_inventory_items iv on iv.item_id=s.item_id 
 WHERE 1 = 1  and s.logtimestamp>='{1}' and s.logtimestamp<'{2}'

UNION 
 select '' SN,'BALANCE' Type,sum(isnull((Qty),0)+(opening_stock*cost_price)) price,'Stock Balance' receiver,0 stock_inout_id,'{2}' logtimestamp,'0' po_rate from(
 select ISNULL(SUM((qty*is_in)*po_rate),0) Qty ,item_id from bs_stock_inout 
 WHERE 1 = 1  and logtimestamp< '{2}' group by item_id) num right outer join (select item_id,opening_stock,cost_price from bs_inventory_items) i on i.item_id=num.item_id 
) data ORDER BY LOGTIMESTAMP
 ", ddlItem.SelectedValue, dtpFromDate.Text, Convert.ToDateTime(dtpToDate.Text).AddDays(1).ToString("yyyy-MM-dd"));

            DataTable dt = dm.GetData(query.ToString());
            gv.RowHeaderColumn = gv.RowHeaderColumn + string.Format("~!!~ Stock In Out from {1} to {2}", ddlItem.SelectedItem.Text, dtpFromDate.Text, dtpToDate.Text);
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