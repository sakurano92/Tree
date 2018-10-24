using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MasterEntry : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"];
        if (type != null)
            MasterData1.LoadInitialInfo(type);
    }
}
