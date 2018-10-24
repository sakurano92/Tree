using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Print_ExportToWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblToPrint.Text = SessionValues.PrintContent;
        string filename = SessionValues.ExportFileName.Split(new string[] { "~!!~" }, StringSplitOptions.None)[0].Replace(' ', '_');
        if (filename == "") filename = "Export";
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".doc");
        Response.ContentType = "application/vnd.word";
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        lblToPrint.RenderControl(htmlWriter);

        Response.Output.Write(stringWriter.ToString().Replace("&amp;nbsp;", ""));
        Response.End();
    }

}