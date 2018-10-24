using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for pdfconverter
/// </summary>
public class pdfconverter
{
	public pdfconverter()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void ExportDataTableToPdf(DataTable dt, string role, string bgthead, string from, string to, string username, string status, string result)
    {
        //DataTable dt = (DataTable)HttpContext.Current.Session["datatable"];

        GridView grds = new GridView();
        grds.DataSource = dt;
        grds.DataBind();

        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
        StringWriter sWriter = new StringWriter();
        HtmlTextWriter hTWriter = new HtmlTextWriter(sWriter);
        grds.RenderControl(hTWriter);
        StringReader sReader = new StringReader(sWriter.ToString());
        Document pdf = new Document(iTextSharp.text.PageSize.A2);
        HTMLWorker worker = new HTMLWorker(pdf);
        PdfWriter.GetInstance(pdf, HttpContext.Current.Response.OutputStream);
        //Font lightblue = new Font(Font.COURIER, 9f, Font.NORMAL, new System.Drawing.Color(43, 145, 175));
        pdf.Open();

        Chunk ck1 = new Chunk("  " + "  " + "   " + "    " + "   " + "    " + "   " + "   " + "     " + "   " + "   " + "   " + "   " + "  " + "  " + "   " + "  " + "THE BRITISH SCHOOL", FontFactory.GetFont("georgia", 28f));
        //ck1.SetUnderline(1f, -2.5f);

        Phrase p2 = new Phrase();
        p2.Add(ck1);


        Paragraph p = new Paragraph();
        p.Add(p2);
        pdf.Add(p);

        Paragraph space = new Paragraph("  ");
        pdf.Add(space);
        Paragraph sp = new Paragraph("  ");
        pdf.Add(sp);
        Paragraph sp1 = new Paragraph("  ");
        pdf.Add(sp1);
        if (role != "")
        {
            Chunk ck3 = new Chunk("Requisitioner Role:" + role + "    ");
            ck3.SetUnderline(1f, -2f);
            Phrase p3 = new Phrase();
            p3.Add(ck3);
            Paragraph pr = new Paragraph();
            pr.Add(p3);
            pdf.Add(p3);


        }
        if (bgthead != "")
        {
            Chunk ck4 = new Chunk("Budget Heading:" + bgthead + "  ");
            ck4.SetUnderline(1f, -2f);
            Phrase p4 = new Phrase();
            p4.Add(ck4);
            Paragraph pb = new Paragraph();
            pb.Add(p4);
            pdf.Add(p4);

        }



        //Paragraph ge = new Paragraph(""+result+"'");

        //pdf.Add(ge);
        //Paragraph gee = new Paragraph("  ");
        //pdf.Add(gee);



        if (from != "" && to != "")
        {
            Chunk ck5 = new Chunk("Date From:" + from + "To:" + to + "  ");
            ck5.SetUnderline(1f, -2f);
            Phrase p5 = new Phrase();
            p5.Add(ck5);
            Paragraph pd = new Paragraph();
            pd.Add(p5);
            pdf.Add(p5);

            //Paragraph date=new Paragraph("Date From:"+from+  "To:"+to);
            //pdf.Add(date);
        }
        if (username != "")
        {
            Chunk ck5 = new Chunk("Username:" + username + "   ");
            ck5.SetUnderline(1f, -2f);
            Phrase p5 = new Phrase();
            p5.Add(ck5);
            Paragraph pb = new Paragraph();
            pb.Add(p5);
            pdf.Add(p5);
            // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
            //pdf.Add(ge);
        }
        if (status != "")
        {
            Chunk ck6 = new Chunk("Status:" + status + "   ");
            ck6.SetUnderline(1f, -2f);
            Phrase p6 = new Phrase();
            p6.Add(ck6);
            Paragraph pb = new Paragraph();
            pb.Add(p6);
            pdf.Add(p6);
        }
        if (bgthead != "")
        {
            pdf.Add(Chunk.NEWLINE);
            Chunk ck9 = new Chunk("" + result + "", FontFactory.GetFont("ITALIC", 14));
            ck9.SetUnderline(1f, -2f);
            Phrase p9 = new Phrase();
            p9.Add(ck9);
            Paragraph pbb = new Paragraph();
            pbb.Add(p9);
            pdf.Add(p9);
        }




        //Paragraph r = new Paragraph("");
        // pdf.Add(r);

        worker.Parse(sReader);
        pdf.Close();
        HttpContext.Current.Response.Write(pdf);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();




    }
  //public void ExportDataTableToPdf(DataTable dt,string role,string bgthead,string from,string to,string username,string status)
  // {
  //    //DataTable dt = (DataTable)HttpContext.Current.Session["datatable"];

  //    GridView grds = new GridView();
  //    grds.DataSource = dt;
  //    grds.DataBind();

  //    HttpContext.Current.Response.Clear();

  //    HttpContext.Current.Response.Buffer = true;
  //    HttpContext.Current.Response.Charset = "";
  //    HttpContext.Current.Response.ContentType = "application/pdf";
  //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
  //    StringWriter sWriter = new StringWriter();
  //    HtmlTextWriter hTWriter = new HtmlTextWriter(sWriter);
  //    grds.RenderControl(hTWriter);
  //    StringReader sReader = new StringReader(sWriter.ToString());
  //    Document pdf = new Document(iTextSharp.text.PageSize.A2);
  //    HTMLWorker worker = new HTMLWorker(pdf);
  //    PdfWriter.GetInstance(pdf, HttpContext.Current.Response.OutputStream);
  //    //Font lightblue = new Font(Font.COURIER, 9f, Font.NORMAL, new System.Drawing.Color(43, 145, 175));
  //    pdf.Open();

  //    Chunk ck1 = new Chunk("  "+"  "+"   "+"    "+"   "+"    "+"   "+"   "+"     "+"   "+"   "+"   "+"   "+"  "+"  "+"   "+"  "+"THE BRITISH SCHOOL", FontFactory.GetFont("georgia", 28f));
  //   //ck1.SetUnderline(1f, -2.5f);

  //    Phrase p2 = new Phrase();
  //    p2.Add(ck1);
    
   
  //    Paragraph p = new Paragraph();
  //    p.Add(p2);
  //    pdf.Add(p);

  //    Paragraph space = new Paragraph("  ");
  //    pdf.Add(space);
  //    Paragraph sp = new Paragraph("  ");
  //    pdf.Add(sp);
  //    Paragraph sp1 = new Paragraph("  ");
  //    pdf.Add(sp1);
  //    if (role != "")
  //    {
  //       Chunk ck3 = new Chunk("Requisitioner Role:" + role+"    ");
  //       ck3.SetUnderline(1f, -2f);
  //       Phrase p3 = new Phrase();
  //       p3.Add(ck3);
  //       Paragraph pr = new Paragraph();
  //       pr.Add(p3);
  //    pdf.Add(p3);
     
      
  //    }
  //    if (bgthead != "")
  //    {
  //       Chunk ck4 = new Chunk("Budget Heading:" + bgthead+"   ");
  //       ck4.SetUnderline(1f, -2f);
  //       Phrase p4 = new Phrase();
  //       p4.Add(ck4);
  //       Paragraph pb = new Paragraph();
  //       pb.Add(p4);
  //       pdf.Add(p4);
  //      // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
  //    //pdf.Add(ge);
  //    }

  //    if (from != "" && to != "")
  //    {
  //       Chunk ck5 = new Chunk("Date From:" + from + "To:" + to+"  ");
  //       ck5.SetUnderline(1f, -2f);
  //       Phrase p5 = new Phrase();
  //       p5.Add(ck5);
  //       Paragraph pd = new Paragraph();
  //       pd.Add(p5);
  //       pdf.Add(p5);

  //       //Paragraph date=new Paragraph("Date From:"+from+  "To:"+to);
  //       //pdf.Add(date);
  //    }
  //    if (username != "")
  //    {
  //       Chunk ck5 = new Chunk("Username:" +username + "   ");
  //       ck5.SetUnderline(1f, -2f);
  //       Phrase p5 = new Phrase();
  //       p5.Add(ck5);
  //       Paragraph pb = new Paragraph();
  //       pb.Add(p5);
  //       pdf.Add(p5);
  //       // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
  //       //pdf.Add(ge);
  //    }
  //    if (status != "")
  //    {
  //       Chunk ck6 = new Chunk("Status:" + status + "   ");
  //       ck6.SetUnderline(1f, -2f);
  //       Phrase p6 = new Phrase();
  //       p6.Add(ck6);
  //       Paragraph pb = new Paragraph();
  //       pb.Add(p6);
  //       pdf.Add(p6);
  //    }
        
  //    //Paragraph r = new Paragraph("");
  //   // pdf.Add(r);
    
  //    worker.Parse(sReader);
  //    pdf.Close();
  //    HttpContext.Current.Response.Write(pdf);
  //    HttpContext.Current.Response.Flush();
  //    HttpContext.Current.Response.End();
      
     
     

  // }
  public void ExportDataTableToPdfREQTODO(DataTable dt, string role, string bgthead, string from, string to, string username)
  {
     //DataTable dt = (DataTable)HttpContext.Current.Session["datatable"];

     GridView grds = new GridView();
     grds.DataSource = dt;
     grds.DataBind();

     HttpContext.Current.Response.Clear();

     HttpContext.Current.Response.Buffer = true;
     HttpContext.Current.Response.Charset = "";
     HttpContext.Current.Response.ContentType = "application/pdf";
     HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
     StringWriter sWriter = new StringWriter();
     HtmlTextWriter hTWriter = new HtmlTextWriter(sWriter);
     grds.RenderControl(hTWriter);
     StringReader sReader = new StringReader(sWriter.ToString());
     Document pdf = new Document(iTextSharp.text.PageSize.A2);
     HTMLWorker worker = new HTMLWorker(pdf);
     PdfWriter.GetInstance(pdf, HttpContext.Current.Response.OutputStream);
     //Font lightblue = new Font(Font.COURIER, 9f, Font.NORMAL, new System.Drawing.Color(43, 145, 175));
     pdf.Open();

     Chunk ck1 = new Chunk("  " + "  " + "   " + "    " + "   " + "    " + "   " + "   " + "     " + "   " + "   " + "   " + "   " + "  " + "  " + "   " + "  " + "THE BRITISH SCHOOL", FontFactory.GetFont("georgia", 28f));
     //ck1.SetUnderline(1f, -2.5f);

     Phrase p2 = new Phrase();
     p2.Add(ck1);


     Paragraph p = new Paragraph();
     p.Add(p2);
     pdf.Add(p);

     Paragraph space = new Paragraph("  ");
     pdf.Add(space);
     Paragraph sp = new Paragraph("  ");
     pdf.Add(sp);
     Paragraph sp1 = new Paragraph("  ");
     pdf.Add(sp1);
     if (role != "")
     {
        Chunk ck3 = new Chunk("Fiscal Year:" + role + "    ");
        ck3.SetUnderline(1f, -2f);
        Phrase p3 = new Phrase();
        p3.Add(ck3);
        Paragraph pr = new Paragraph();
        pr.Add(p3);
        pdf.Add(p3);


     }
     if (bgthead != "")
     {
        Chunk ck4 = new Chunk("Requisition Status:" + bgthead + "   ");
        ck4.SetUnderline(1f, -2f);
        Phrase p4 = new Phrase();
        p4.Add(ck4);
        Paragraph pb = new Paragraph();
        pb.Add(p4);
        pdf.Add(p4);
        // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
        //pdf.Add(ge);
     }

     if (from != "" && to != "")
     {
        Chunk ck5 = new Chunk("From:" + from + "To:" + to + "  ");
        ck5.SetUnderline(1f, -2f);
        Phrase p5 = new Phrase();
        p5.Add(ck5);
        Paragraph pd = new Paragraph();
        pd.Add(p5);
        pdf.Add(p5);

        //Paragraph date=new Paragraph("Date From:"+from+  "To:"+to);
        //pdf.Add(date);
     }
     if (username != "")
     {
        Chunk ck5 = new Chunk("Requisitioner:" + username + "   ");
        ck5.SetUnderline(1f, -2f);
        Phrase p5 = new Phrase();
        p5.Add(ck5);
        Paragraph pb = new Paragraph();
        pb.Add(p5);
        pdf.Add(p5);
        // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
        //pdf.Add(ge);
     }

     //Paragraph r = new Paragraph("");
     // pdf.Add(r);

     worker.Parse(sReader);
     pdf.Close();
     HttpContext.Current.Response.Write(pdf);
     HttpContext.Current.Response.Flush();
     HttpContext.Current.Response.End();




  }
  public void pdfbugtheading(DataTable dt, string bugt_cat, string fscl_yr)
  {
     GridView grds = new GridView();


   

     grds.DataSource = dt;
     grds.DataBind();
     
     HttpContext.Current.Response.Clear();

     HttpContext.Current.Response.Buffer = true;
     HttpContext.Current.Response.Charset = "";
     HttpContext.Current.Response.ContentType = "application/pdf";
     HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
     StringWriter sWriter = new StringWriter();
     HtmlTextWriter hTWriter = new HtmlTextWriter(sWriter);
     grds.RenderControl(hTWriter);
     StringReader sReader = new StringReader(sWriter.ToString());
     Document pdf = new Document(iTextSharp.text.PageSize.A2);
     HTMLWorker worker = new HTMLWorker(pdf);
     PdfWriter.GetInstance(pdf, HttpContext.Current.Response.OutputStream);
     //Font lightblue = new Font(Font.COURIER, 9f, Font.NORMAL, new System.Drawing.Color(43, 145, 175));
     pdf.Open();

     Chunk ck1 = new Chunk("  " + "  " + "   " + "    " + "   " + "    " + "   " + "   " + "     " + "   " + "   " + "   " + "   " + "  " + "  " + "   " + "  " + "THE BRITISH SCHOOL", FontFactory.GetFont("georgia", 28f));
     //ck1.SetUnderline(1f, -2.5f);

     Phrase p2 = new Phrase();
     p2.Add(ck1);


     Paragraph p = new Paragraph();
     p.Add(p2);
     pdf.Add(p);

     Paragraph space = new Paragraph("  ");
     pdf.Add(space);
     Paragraph sp = new Paragraph("  ");
     pdf.Add(sp);
     Paragraph sp1 = new Paragraph("  ");
     pdf.Add(sp1);
     if (fscl_yr != "")
     {
        Chunk ck3 = new Chunk("Fiscal Year:" +fscl_yr + "    ");
        ck3.SetUnderline(1f, -2f);
        Phrase p3 = new Phrase();
        p3.Add(ck3);
        Paragraph pr = new Paragraph();
        pr.Add(p3);
        pdf.Add(p3);


     }
     if (bugt_cat != "")
     {
        Chunk ck4 = new Chunk("Budget Category:" + bugt_cat + "   ");
        ck4.SetUnderline(1f, -2f);
        Phrase p4 = new Phrase();
        p4.Add(ck4);
        Paragraph pb = new Paragraph();
        pb.Add(p4);
        pdf.Add(p4);
        // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
        //pdf.Add(ge);
     }

     
     

     //Paragraph r = new Paragraph("");
     // pdf.Add(r);

     worker.Parse(sReader);
     pdf.Close();
     HttpContext.Current.Response.Write(pdf);
     HttpContext.Current.Response.Flush();
     HttpContext.Current.Response.End();


     

        

  }

  public void pdfexpensedetails(DataTable dt, string bugt_head, string fscl_yr)
  {
     GridView grds = new GridView();




    // dt.Columns["buget_heading"].ColumnName = "Budget Heading";
     grds.DataSource = dt;
     grds.DataBind();

     HttpContext.Current.Response.Clear();

     HttpContext.Current.Response.Buffer = true;
     HttpContext.Current.Response.Charset = "";
     HttpContext.Current.Response.ContentType = "application/pdf";
     HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
     StringWriter sWriter = new StringWriter();
     HtmlTextWriter hTWriter = new HtmlTextWriter(sWriter);
     grds.RenderControl(hTWriter);
     StringReader sReader = new StringReader(sWriter.ToString());
     Document pdf = new Document(iTextSharp.text.PageSize.A2);
     HTMLWorker worker = new HTMLWorker(pdf);
     PdfWriter.GetInstance(pdf, HttpContext.Current.Response.OutputStream);
     //Font lightblue = new Font(Font.COURIER, 9f, Font.NORMAL, new System.Drawing.Color(43, 145, 175));
     pdf.Open();

     Chunk ck1 = new Chunk("  " + "  " + "   " + "    " + "   " + "    " + "   " + "   " + "     " + "   " + "   " + "   " + "   " + "  " + "  " + "   " + "  " + "THE BRITISH SCHOOL", FontFactory.GetFont("georgia", 28f));
     //ck1.SetUnderline(1f, -2.5f);

     Phrase p2 = new Phrase();
     p2.Add(ck1);


     Paragraph p = new Paragraph();
     p.Add(p2);
     pdf.Add(p);

     Paragraph space = new Paragraph("  ");
     pdf.Add(space);
     Paragraph sp = new Paragraph("  ");
     pdf.Add(sp);
     Paragraph sp1 = new Paragraph("  ");
     pdf.Add(sp1);
     if (fscl_yr != "")
     {
        Chunk ck3 = new Chunk("Fiscal Year:" + fscl_yr + "    ");
        ck3.SetUnderline(1f, -2f);
        Phrase p3 = new Phrase();
        p3.Add(ck3);
        Paragraph pr = new Paragraph();
        pr.Add(p3);
        pdf.Add(p3);


     }
     if (bugt_head != "")
     {
        Chunk ck4 = new Chunk("Budget Category:" + bugt_head + "   ");
        ck4.SetUnderline(1f, -2f);
        Phrase p4 = new Phrase();
        p4.Add(ck4);
        Paragraph pb = new Paragraph();
        pb.Add(p4);
        pdf.Add(p4);
        // Paragraph ge = new Paragraph("Budget Heading:"+bgthead);
        //pdf.Add(ge);
     }




     //Paragraph r = new Paragraph("");
     // pdf.Add(r);

     worker.Parse(sReader);
     pdf.Close();
     HttpContext.Current.Response.Write(pdf);
     HttpContext.Current.Response.Flush();
     HttpContext.Current.Response.End();






  }

  public void pdfsearchbyuser(DataTable dt, string username, string from,string to)
  {


     GridView grds = new GridView();
     grds.DataSource = dt;
     grds.DataBind();

     HttpContext.Current.Response.Clear();

     HttpContext.Current.Response.Buffer = true;
     HttpContext.Current.Response.Charset = "";
     HttpContext.Current.Response.ContentType = "application/pdf";
     HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Report.pdf");
     StringWriter sWriter = new StringWriter();
     HtmlTextWriter hTWriter = new HtmlTextWriter(sWriter);
     grds.RenderControl(hTWriter);
     StringReader sReader = new StringReader(sWriter.ToString());
     Document pdf = new Document(iTextSharp.text.PageSize.A2);
     HTMLWorker worker = new HTMLWorker(pdf);
     PdfWriter.GetInstance(pdf, HttpContext.Current.Response.OutputStream);
     //Font lightblue = new Font(Font.COURIER, 9f, Font.NORMAL, new System.Drawing.Color(43, 145, 175));
     pdf.Open();

     Chunk ck1 = new Chunk("  " + "  " + "   " + "    " + "   " + "    " + "   " + "   " + "     " + "   " + "   " + "   " + "   " + "  " + "  " + "   " + "  " + "THE BRITISH SCHOOL", FontFactory.GetFont("georgia", 28f));
     //ck1.SetUnderline(1f, -2.5f);

     Phrase p2 = new Phrase();
     p2.Add(ck1);


     Paragraph p = new Paragraph();
     p.Add(p2);
     pdf.Add(p);

     Paragraph space = new Paragraph("  ");
     pdf.Add(space);
     Paragraph sp = new Paragraph("  ");
     pdf.Add(sp);
     Paragraph sp1 = new Paragraph("  ");
     pdf.Add(sp1);
     if (username != "")
     {
        Chunk ck3 = new Chunk("User Name:" + username + "    ");
        ck3.SetUnderline(1f, -2f);
        Phrase p3 = new Phrase();
        p3.Add(ck3);
        Paragraph pr = new Paragraph();
        pr.Add(p3);
        pdf.Add(p3);


     }
   

     if (from != "" && to != "")
     {
        Chunk ck4 = new Chunk("From:" + from + "To:" + to + "  ");
        ck4.SetUnderline(1f, -2f);
        Phrase p4 = new Phrase();
        p4.Add(ck4);
        Paragraph pd = new Paragraph();
        pd.Add(p4);
        pdf.Add(p4);

        //Paragraph date=new Paragraph("Date From:"+from+  "To:"+to);
        //pdf.Add(date);
     }
    

     //Paragraph r = new Paragraph("");
     // pdf.Add(r);

     worker.Parse(sReader);
     pdf.Close();
     HttpContext.Current.Response.Write(pdf);
     HttpContext.Current.Response.Flush();
     HttpContext.Current.Response.End();




  }
   
}