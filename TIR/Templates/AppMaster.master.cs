using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class AppMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        
        if (SessionValues.RoleIdSession == string.Empty)
        {
            SessionManager.SetSession("ROLE_ID", "0");
            SessionManager.SetSession("USER_ID", "test");
        }

        if (SessionValues.UserIdSession == string.Empty)
            Response.Redirect("~/Default.aspx");
    }
}
