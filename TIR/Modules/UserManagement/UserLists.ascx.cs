using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;

public partial class Modules_UserManagement_UserLists : System.Web.UI.UserControl
{
    UserManagement objUsers = new UserManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;


        LoadData();
        ClearAllFields();
        // DisableControls();
    }

    public void DisableControls()
    {
        btnSave.Enabled = false;

        txtEmail.Enabled = false;

        txtUserName.Enabled = false;
        txtFullName.Enabled = false;
        ddlRoles.Enabled = false;
        ddlStatus.Enabled = false;
    }
    public void EnableControls(bool isAdd)
    {
        btnSave.Enabled = true;
        txtEmail.Enabled = true;
        txtUserName.Enabled = isAdd;
        txtFullName.Enabled = true;
        ddlRoles.Enabled = true;
        ddlStatus.Enabled = true;
    }

    public void ClearAllFields()
    {
        txtUserId.Text = "";
        txtUserName.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPass1.Text = string.Empty;
        txtPass2.Text = string.Empty;
        txtPhone.Text = string.Empty;
        ddlRoles.SelectedIndex = -1;
        ddlStatus.SelectedIndex = -1;
    }

    public void LoadData()
    {
        LoadUsers();
        DataManagement dm = new DataManagement();
        dm.LoadDDL(ddlRoles, "BS_USER_ROLES", "ROLE_ID", "ROLE_NAME");
        dm.LoadDDL(srchRole, "BS_USER_ROLES", "ROLE_ID", "ROLE_NAME",true);
        dm.LoadDDL(srchStatus, "BS_USER_STATUSES", "STATUS_ID", "STATUS_NAME",true);
    }
    private void LoadUsers()
    {
        DataTable dt = objUsers.GetAllUsers(srchText.Text, srchRole.SelectedValue, srchStatus.SelectedValue);
        grdPagerTemplate.BindDetails(grdUsers, dt);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        EnableControls(true);
        ClearAllFields();
        hidFormmode.Value = "add";
    }

    protected void NavigationLink_Click(object sender, CommandEventArgs e)
    {
        DataTable dt = objUsers.GetAllUsers(srchText.Text,srchRole.SelectedValue,srchStatus.SelectedValue);
        grdPagerTemplate.GridViewPager(grdUsers, dt, e);
    }
    protected void btnEditUserInfo_Click(object sender, EventArgs e)
    {
        EnableControls(false);
        ImageButton btn = (ImageButton)sender;
        GetUsersById(btn.CommandArgument);
        hidFormmode.Value = "edit";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        EnableControls(true);
        ClearAllFields();
    }

    protected void GetUsersById(string userId)
    {

        DataTable dt = objUsers.GetUsersById(userId);

        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtUserId.Text = userId;
            txtUserName.Text = dr["USER_NAME"].ToString();
            txtFullName.Text = dr["FULL_NAME"].ToString();
            txtEmail.Text = dr["EMAIL"].ToString();
            txtPass1.Text = dr["PASSWORD"].ToString();
            txtPass2.Text = dr["PASSWORD"].ToString();
            txtPhone.Text = dr["PHONE"].ToString();
            ddlRoles.Text = dr["ROLE_ID"].ToString();
            ddlStatus.Text = dr["STATUS_ID"].ToString();

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {

        ImageButton btn = (ImageButton)sender;
        if (btn.CommandArgument == SessionManager.GetSession("USER_ID"))
        {
            CustomMessage.DisplayMessage(this.Page, "Self deletion not allowed. Contact other admin.", CustomMessage.MessageType.WARNING);
        }
        else
        {
            if (objUsers.DeleteUsers(btn.CommandArgument))
            {
                CustomMessage.DisplayMessage(this.Page, "User Deleted Succesfully", CustomMessage.MessageType.SUCCESS);
            }
            else
            {
                CustomMessage.DisplayMessage(this.Page, "User has performed transactions. Cannot delete user.", CustomMessage.MessageType.WARNING);
            }
        }
        LoadUsers();
    }
    protected void btnDeactivate_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        if (btn.CommandArgument == SessionManager.GetSession("USER_ID"))
        {
            CustomMessage.DisplayMessage(this.Page, "Self deletion not allowed. Contact other admin.", CustomMessage.MessageType.WARNING);
        }
        else
        {
            objUsers.DeactivateUser(btn.CommandArgument);
            LoadUsers();
        }
    }
    protected void btnActivate_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        objUsers.ActivateUser(btn.CommandArgument);
        LoadUsers();
    }

    protected void btnSaveData_Click(object sender, EventArgs e)
    {
        if (SaveUserInfo())
            ClearAllFields();
    }
    public bool SaveUserInfo()
    {
        string ret = "";
        if (hidFormmode.Value == "edit")
        {
            //insert code for editing of data]
            ret = objUsers.UpdateUser(txtUserId.Text, txtFullName.Text, txtEmail.Text, ddlRoles.SelectedValue, ddlStatus.SelectedValue, txtPass1.Text,txtPhone.Text);
            if (ret == "")
            {
                CustomMessage.DisplayMessage(this.Page, "User Updated Successfully", CustomMessage.MessageType.INFO);
                LoadUsers();
            }
            else
            {
                CustomMessage.DisplayMessage(this.Page, ret, CustomMessage.MessageType.INFO);
                return false;
            }
        }

        else
        {
            if (!(objUsers.IsUserInSystem(txtUserName.Text)))
            {

                ret = objUsers.InsertUser(txtUserName.Text, txtFullName.Text, txtEmail.Text, ddlRoles.SelectedValue, ddlStatus.SelectedValue,txtPass1.Text,txtPhone.Text);
                if (ret == "")
                {
                    LoadData();
                    ClearAllFields();
                    CustomMessage.DisplayMessage(this.Page, "User Added Successfully", CustomMessage.MessageType.INFO);
                }
                else
                {
                    CustomMessage.DisplayMessage(this.Page, ret, CustomMessage.MessageType.INFO);
                    return false;
                }
            }

            else
            {
                CustomMessage.DisplayMessage(this.Page, "User Name already Exists.", CustomMessage.MessageType.INFO);
                return false;
            }
        }
        return true;
    }
    protected void grdUsers_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnResetPassword_Click(object sender, EventArgs e)
    {

    }

    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0) return;
        string status  = ((HiddenField)e.Row.FindControl("hdnStatus")).Value;
        if (status == "1")
        {
            ImageButton btnActivate = (ImageButton)e.Row.FindControl("btnActivate");
            btnActivate.Visible = false;
        }
        else
        {
            ImageButton btnDeactivate = (ImageButton)e.Row.FindControl("btnDeactivate");
            btnDeactivate.Visible = false;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadUsers();
    }
}
