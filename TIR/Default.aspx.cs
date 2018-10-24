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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblFormInfo.Text = "Invalid User or Password";

        UserManagement usrMgmt = new UserManagement();
        if (usrMgmt.LoginUser(txtUserName.Text))
        {
            Response.Redirect("~/Pages/Users/Users.aspx");
        }
        else
        {
            lblFormInfo.Text = "Invalid User or Password";
        }
    }
}