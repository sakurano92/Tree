using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Print_Exporter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // if (IsPostBack) return;
        string filename = SessionValues.ExportFileName.Split(new string[] { "~!!~" }, StringSplitOptions.None)[0].Trim().Replace(' ', '_');
        lblHeading.Text = SessionValues.ExportFileName.Split(new string[] { "~!!~" }, StringSplitOptions.None).Length > 1
            ? SessionValues.ExportFileName.Split(new string[] { "~!!~" }, StringSplitOptions.None)[1].Trim() : "";
        if (filename == "") filename = "Export";
        grdExport.DataSource = SessionValues.ExportData;
        grdExport.DataBind();
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
        Response.ContentType = "application/vnd.xls";
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.IO.StringWriter stringWriter = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);
        lblHeading.RenderControl(htmlWriter);
        grdExport.RenderControl(htmlWriter);

        Response.Output.Write(stringWriter.ToString().Replace("&amp;nbsp;", "").Replace("&amp;", "&"));
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

}