using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Admin_DeptMgmt : System.Web.UI.Page
{
    DataManagement dm = new DataManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }
    protected void ClearValues()
    {
        hdnDeptId.Value = string.Empty;
        txtCostCenterCode.Text = string.Empty;
        txtDeptName.Text = string.Empty;
        txtDeptName.Focus();
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        Button cmdBtn = (Button)sender;
        switch (cmdBtn.CommandName)
        {
            case "Add":
                ClearValues();
                btnAddDept.Visible = false;
                btnSaveDept.Visible = true;
                btnCancel.Visible = true;
                break;

            case "Save":
                string tableName = "Departments";
                if (hdnDeptId.Value == string.Empty)
                {
                    string[] fields = { "DeptName", "CostCenterCode", "CreatedBy", "CreatedDate" };
                    string[] data = { txtDeptName.Text, txtCostCenterCode.Text, SessionValues.UserIdSession, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    dm.InsertData(tableName, fields, data);
                }
                else
                {
                    string[] fields = { "DeptName", "CostCenterCode", "CreatedBy", "CreatedDate" };
                    string[] data = { txtDeptName.Text, txtCostCenterCode.Text, SessionValues.UserIdSession, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    dm.UpdateData(tableName, fields, data, "DeptId = '" + hdnDeptId.Value + "'");
                }
                SqlDsUserInfo.DataBind();
                gv.DataBind();
                ClearValues();
                btnSaveDept.Visible = false;
                btnAddDept.Visible = true;
                btnCancel.Visible = false;
                break;

            case "Cancel":
                btnSaveDept.Visible = false;
                btnCancel.Visible = false;
                btnAddDept.Visible = true;
                ClearValues();
                break;

        }
    }
    protected void imgbtnAction_Click(object sender, EventArgs e)
    {
        ImageButton cmdBtn = (ImageButton)sender;
        switch (cmdBtn.CommandName)
        {

            case "EditRecord":
                DataTable dt = dm.GetData("SELECT * FROM DEPARTMENTS WHERE DEPTID = '" + cmdBtn.CommandArgument + "'");
                DataRow dr = dt.Rows[0];
                txtDeptName.Text = dr["DeptName"].ToString();
                txtCostCenterCode.Text = dr["CostCenterCode"].ToString();
                hdnDeptId.Value = dr["DeptId"].ToString();
                btnSaveDept.Visible = true;
                btnAddDept.Visible = false;
                break;

            case "DeleteRecord":
                dm.ExecuteNonQuery("DELETE FROM DEPARTMENTS WHERE DEPTID = '" + cmdBtn.CommandArgument + "'");
                SqlDsUserInfo.DataBind();
                gv.DataBind();
                ClearValues();
                break;
        }
    }
}
