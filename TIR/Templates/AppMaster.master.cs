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
        //SessionManager.SetSession("USER_ID", "5");//as user login module not added

        if (SessionValues.UserIdSession == null)
            Response.Redirect("~/Default.aspx");
    }
}
