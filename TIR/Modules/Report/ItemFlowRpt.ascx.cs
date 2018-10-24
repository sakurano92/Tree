using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Globalization;

public partial class Modules_Inventory_InventoryItemsFlow_Rpt : System.Web.UI.UserControl
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

            query.AppendFormat(@"select item_name,'Status'= case  when is_in=-1 then 'OUT' 
 when is_in=1 then 'IN' end,
opening_stock,AMT_OUT_IN .qty_out,AMT_OUT_IN .amount_out  as amount_out ,AMT_OUT_IN.qty_in,AMT_OUT_IN .amountIn    from bs_inventory_items inv inner join

 (select  item_id,is_in,Amount_out,amountIn,qty_out,qty_in  from(
 (select item_id,-1 is_in,sum(qty)as qty_out,0 as qty_in,0 as amountIn, cast (sum(qty*po_rate)as decimal(18,2)) as amount_out from bs_stock_inout where is_in=-1 and logtimestamp between '{0}' and '{1}' group by item_id )
 union
 ( select item_id,1 is_in,0 as qty_out,sum(qty)as qty_in,CAST( sum(qty*po_rate)as decimal(18,2)) as amount_in,0 as amountOut from bs_stock_inout where is_in=1 and logtimestamp between '{0}' and '{1}' group by item_id ))aoutin) AMT_OUT_IN on AMT_OUT_IN .item_id =inv .item_id 
 UNION ALL
 
 select 'TOTAL' as item_name,'' as Status,
sum(opening_stock),sum(AMT_OUT_IN .qty_out) as amount_out,sum(AMT_OUT_IN .amount_out) ,sum(AMT_OUT_IN.qty_in),sum(AMT_OUT_IN .amountIn)    from bs_inventory_items inv inner join

 (select  item_id,is_in,Amount_out,amountIn,qty_out,qty_in  from(
 (select item_id,-1 is_in,sum(qty)as qty_out,0 as qty_in,0 as amountIn, cast (sum(qty*po_rate)as decimal(18,2)) as amount_out from bs_stock_inout where is_in=-1 and logtimestamp between '{0}' and '{1}' group by item_id )
 union
 ( select item_id,1 is_in,0 as qty_out,sum(qty)as qty_in,CAST( sum(qty*po_rate)as decimal(18,2)) as amount_in,0 as amountOut from bs_stock_inout where is_in=1 and logtimestamp between '{0}' and '{1}' group by item_id ))aoutin) AMT_OUT_IN on AMT_OUT_IN .item_id =inv .item_id 
 

 ", dtpFromDate.Text, dtpToDate.Text  );
            DataTable dt = dm.GetData(query.ToString());
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
        //dm.LoadDDL(ddlItemCat, "bs_item_categories", "itm_cat_id", "itm_cat_name", false);
        //dm.LoadDDL(ddlItem, "bs_inventory_items", "item_id", "item_code", "item_category='" + ddlItemCat.SelectedValue + "'");
      

        LoadInitialData();
        LoadData();
        
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
        dtpToDate.Text = DateTime.Today.AddDays (+1).ToString("yyyy-MM-dd");
    }
  


    protected void btn_OnClick(object sender, EventArgs e)
    {
        LoadData();
    }



}