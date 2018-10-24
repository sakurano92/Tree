using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Supplier_SupplierList : System.Web.UI.UserControl
{
    SupplierMgr supplierMgr = new SupplierMgr();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        LoadData();
    }
    private void LoadData()
    {
        grdPagerTemplate.BindDetails(grdDatas, FetchData());
        ClearFields();
    }
    private DataTable FetchData()
    {
        DataTable dt = new DataTable();
        dt = supplierMgr.GetSuppliers(srchName.Text,srchAddress.Text);
        return dt;
    }
    private void ClearFields()
    {
        hdnId.Value = "";
        txtName.Text = "";
        txtDetail.Text = "";
        txtCode.Text = "";
        txtAddress.Text = "";
        txtContact.Text = "";
        btnSave.Text = "Add New Supplier";
        hdnFormMode.Value = "add";
        lblNotice.Visible = false;
        CustomMessage.ClearMessage(this.Page);
        updEntry.Update();
        
    }
    public void NavigationLink_Click(object sender, CommandEventArgs e)
    {
        grdPagerTemplate.GridViewPager(grdDatas, FetchData(), e);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ClearFields();
    }
   
    protected void btnSaveData_Click(object sender, EventArgs e)
    {
        
            if (!supplierMgr.SupplierCodeExists(txtCode.Text, hdnId.Value.Trim()))
            {
                if (!supplierMgr.SupplierExists(txtName.Text,  hdnId.Value.Trim()))
                {
                    string ret = supplierMgr.SaveSupplier(hdnId.Value, txtCode.Text, txtName.Text, txtAddress.Text, txtContact.Text, txtDetail.Text);
                    if (ret == "")
                    {
                        ClearFields();
                        CustomMessage.DisplayMessage(this.Page, "Data Saved Successfully", CustomMessage.MessageType.INFO);

                        grdPagerTemplate.BindDetails(grdDatas, FetchData());
                    }
                    else
                        CustomMessage.DisplayMessage(this.Page, ret, CustomMessage.MessageType.ERROR);
                }
                else
                    CustomMessage.DisplayMessage(this.Page, "Supplier Name Already Exist.", CustomMessage.MessageType.WARNING);
            }
            else
            {
                CustomMessage.DisplayMessage(this.Page, "Supplier Code is already assigned to other Supplier.", CustomMessage.MessageType.WARNING);
                txtCode.Focus();
            }

   
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        string supplierId = btn.CommandArgument;
        DataRow dr = supplierMgr.GetSupplierDetail(supplierId);
        if (dr != null)
        {
            hdnId.Value = supplierId;
            txtCode.Text = dr["supplier_code"].ToString();
            txtDetail.Text = dr["supplier_detail"].ToString();
            txtName.Text = dr["supplier_name"].ToString();
            txtAddress.Text = dr["supplier_address"].ToString();
            txtContact.Text = dr["supplier_contact"].ToString();
            btnSave.Text = "Update Supplier Detail";
            hdnFormMode.Value = "edit";
            lblNotice.Visible = true;
            updEntry.Update();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;
        string supplierId = imgBtn.CommandArgument;
        if (!supplierMgr.isSupplierUsed(supplierId))
        {
            string ret = supplierMgr.DeleteSupplier(supplierId);
            if (ret == "")
            {
                CustomMessage.DisplayMessage(this.Page, "Supplier deleted Successfully", CustomMessage.MessageType.INFO);
                grdPagerTemplate.BindDetails(grdDatas, FetchData());
            }
            else
                CustomMessage.DisplayMessage(this.Page, ret, CustomMessage.MessageType.ERROR);
        }
        else
            CustomMessage.DisplayMessage(this.Page, "Supplier has Transactions. So, cannot Delete.", CustomMessage.MessageType.INFO);

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        grdPagerTemplate.BindDetails(grdDatas, supplierMgr.GetSuppliers(srchName.Text,srchAddress.Text));
    }
}
