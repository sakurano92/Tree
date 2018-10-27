using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Security.Cryptography;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblFormInfo.Text = "Invalid User or Password";
        lblFormInfo.ForeColor = System.Drawing.Color.Red;
        string password = encryptPassword(txtUserPassword.Text);
        UserManagement usrMgmt = new UserManagement();
        if (usrMgmt.LoginUser(txtUserName.Text, password))
           // if (usrMgmt.LoginUser(txtUserName.Text, password))
        {
            Response.Redirect("~/Home.aspx");
        }
        else
        {
            lblFormInfo.Text = "Invalid User or Password";
            lblFormInfo.ForeColor = System.Drawing.Color.Red;
            txtUserName.Text = string.Empty;
            txtUserPassword.Text = string.Empty;
        }
    }

    protected string encryptPassword ( string pass)
    {
        byte[] bytes = System.Text.Encoding.Unicode.GetBytes(pass);
        byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
        return Convert.ToBase64String(inArray);
    }
}