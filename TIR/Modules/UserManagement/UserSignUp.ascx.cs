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
using System.Security.Cryptography;

public partial class Modules_UserManagement_UserSignUp : System.Web.UI.UserControl
{
    UserManagement objUsers = new UserManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        ClearAllFields();
        // DisableControls();
    }

    public void DisableControls()
    {
        btnSave.Enabled = false;

        txtEmail.Enabled = false;

        txtUserName.Enabled = false;
        txtFullName.Enabled = false;
        
    }
    public void EnableControls(bool isAdd)
    {
        btnSave.Enabled = true;
        txtEmail.Enabled = true;
        txtUserName.Enabled = isAdd;
        txtFullName.Enabled = true;
       
    }

    public void ClearAllFields()
    {
        txtUserId.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtFullName.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        lblUser.Text = string.Empty;
        
        
    }

 
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        EnableControls(true);
        ClearAllFields();
        hidFormmode.Value = "add";
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
            

        }
    }

    
    protected void btnSaveData_Click(object sender, EventArgs e)
    {
        if (SaveUserInfo())
            Response.Redirect("~/Default.aspx");
    }

    protected void btn_Back(object sender, EventArgs e)
    {
            Response.Redirect("~/Default.aspx");
    }

    public bool SaveUserInfo()
    {
        string uname = txtUserName.Text.Trim();
        string role = 4.ToString();
        string password = encryptPassword(txtPass1.Text);
        string status = 1.ToString();
        if (!objUsers.IsUserInSystem(uname))
        {
            objUsers.InsertUser(txtUserName.Text, txtFullName.Text, txtEmail.Text, role, status, password, txtPhone.Text);
            return true;
        }
        else
        {
            lblUser.Text = string.Empty;
            return false;
        }
    }

    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0) return;
        string status = ((HiddenField)e.Row.FindControl("hdnStatus")).Value;
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


    protected void checkUser(object sender, EventArgs e)
    {
        if (objUsers.IsUserInSystem(txtUserName.Text))
        {
            txtUserName.Focus();
            lblUser.Text = "User already exists.";
            lblUser.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblUser.Text = string.Empty;
        }
    }

    protected string encryptPassword(string pass)
    {
        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
        byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
        return Convert.ToBase64String(inArray);
    }
}

