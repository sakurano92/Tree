using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

public partial class Modules_UserManagement_fogetPass : System.Web.UI.UserControl
{
    UserManagement objUsers = new UserManagement();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
    }

    public void ClearAllFields()
    {
        txtUserId.Text = string.Empty;
        txtUserName.Text = string.Empty;
        txtPass1.Text = string.Empty;
        txtPass2.Text = string.Empty;
        lblUser.Text = string.Empty;
    }

    protected void btnChangePass_Click(object sender, EventArgs e)
    {
        string uname = txtUserName.Text.Trim();
        string password = encryptPassword(txtPass1.Text);
        string code = txtCode.Text;
        if(objUsers.IsUserInSystem(txtUserName.Text))
        {

        }
        
    }

    protected void checkCode(object sender, EventArgs e)
    {
        if (!objUsers.IsUserInSystem(txtUserName.Text))
        {
            txtUserName.Focus();
            lblCodeMsg.Text = "Code not matched.";
            lblCodeMsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblCodeMsg.Text = string.Empty;
          
        }
    }

    protected void checkUser(object sender, EventArgs e)
    {
        if (!objUsers.IsUserInSystem(txtUserName.Text))
        {
            txtUserName.Focus();
            lblUser.Text = "User not exists.";
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearAllFields();
    }

    protected void btn_Back(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}