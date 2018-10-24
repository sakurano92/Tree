using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Print_PrintWeb : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblToPrint.Text = SessionValues.PrintContent;
        PrintWebForm.PrintWebControl(PlaceHolder1);
    }

}