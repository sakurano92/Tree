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

public partial class Admin_UserMgmt : System.Web.UI.Page
{
    DataManagement dm = new DataManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }
    protected void ClearValues()
    {
        hdnOldUserId.Value = string.Empty;
        txtEmail.Text = string.Empty;
        txtUserID.Text = string.Empty;
        txtUserName.Text = string.Empty;
        lblCmdMsgError.Text = string.Empty;
        lblCmdMsgGeneral.Text = string.Empty;
        txtUserID.Focus();
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        Button cmdBtn = (Button)sender;
        switch (cmdBtn.CommandName)
        {
            case "Add":
                ClearValues();
                btnAddUser.Visible = false;
                btnSaveUser.Visible = true;
                btnCancel.Visible = true;
                break;

            case "Save":
                string tableName = "UserInfo";
                if (hdnOldUserId.Value == string.Empty)
                {
                    string[] fields = { "UserId", "UserName", "Email", "RoleId", "Status", "Branchcode", "DeptId", "CreatedBy", "CreatedDate" };
                    string[] data = { txtUserID.Text, txtUserName.Text, txtEmail.Text, ddlUserType.SelectedValue, 
                                        ddlStatus.SelectedValue,ddlBranch.SelectedValue,  ddlDepartment.SelectedValue, SessionValues.UserIdSession, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    dm.InsertData(tableName, fields, data);

                }
                else
                {
                    string[] fields = { "UserId", "UserName", "Email", "RoleId", "Status", "Branchcode", "DeptId", "ModifiedBy", "ModifiedDate" };
                    string[] data = { txtUserID.Text, txtUserName.Text, txtEmail.Text, ddlUserType.SelectedValue, 
                                        ddlStatus.SelectedValue,ddlBranch.SelectedValue, ddlDepartment.SelectedValue, SessionValues.UserIdSession, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    dm.UpdateData(tableName, fields, data, "UserId = '" + hdnOldUserId.Value + "'");
                }
                SqlDsUserInfo.DataBind();
                gv.DataBind();
                ClearValues();
                lblCmdMsgGeneral.Text = "User Data Saved Successfully";
                btnSaveUser.Visible = false;
                btnCancel.Visible = false;
                break;
            case "Cancel":
                btnSaveUser.Visible = false;
                btnCancel.Visible = false;
                btnAddUser.Visible = true;
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
                DataTable dt = dm.GetData("SELECT * FROM UserInfo WHERE UserId = '" + cmdBtn.CommandArgument + "'");
                DataRow dr = dt.Rows[0];
                txtUserID.Text = dr["UserId"].ToString();
                txtUserName.Text = dr["UserName"].ToString();
                txtEmail.Text = dr["Email"].ToString();
                ddlDepartment.SelectedValue = dr["DeptId"].ToString();
                ddlBranch.SelectedValue = dr["BranchCode"].ToString();
                ddlStatus.SelectedValue = dr["Status"].ToString();
                ddlUserType.SelectedValue = dr["RoleId"].ToString();
                hdnOldUserId.Value = dr["UserId"].ToString();
                lblCmdMsgGeneral.Text = string.Empty;
                btnAddUser.Visible = false;
                btnSaveUser.Visible = true;
                btnCancel.Visible = true;
                break;

            case "DeleteRecord":
                dm.ExecuteNonQuery("DELETE FROM UserInfo WHERE UserId = '" + cmdBtn.CommandArgument + "'");
                SqlDsUserInfo.DataBind();
                gv.DataBind();
                ClearValues();
                lblCmdMsgGeneral.Text = "User Data Deleted Successfully";
                break;

        }
    }
}
