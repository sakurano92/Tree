using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Web.SessionState;

/// <summary>
/// Summary description for PrintWebForm
/// </summary>
public class PrintWebForm
{
	public PrintWebForm()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void PrintWebControl(Control ctrl)
    {
        PrintWebControl(string.Empty,ctrl, string.Empty);
    }
    public static void PrintWebControl(Control ctrl, string Script)
    {
        PrintWebControl(string.Empty, ctrl, Script);
    }
    public static void PrintWebControl(string heading,Control ctrl, string Script)
    {
        StringWriter stringWrite = new StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        if (ctrl is WebControl)
        {
            Unit w = new Unit(100, UnitType.Percentage); ((WebControl)ctrl).Width = w;
        }
        Page pg = new Page();
        pg.EnableEventValidation = false;
        pg.EnableTheming = true;
        pg.ApplyStyleSheetSkin(ctrl.Page);
        if (Script == string.Empty)
        {
           Script= @"<style>
                    .tblgrid,.tblgrid table
                    {
	                    width:100%;
	                    background-color:blue;
                    }
                    </style>";
        }
        if (Script != string.Empty)
        {
            pg.ClientScript.RegisterStartupScript(pg.GetType(), "PrintJavaScript", Script);
        }
        HtmlForm frm = new HtmlForm();
      
        pg.Controls.Add(frm);
        frm.Attributes.Add("runat", "server");
        frm.Controls.Add(ctrl);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = heading + stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }
}
