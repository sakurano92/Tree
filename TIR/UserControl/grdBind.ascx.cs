using System;
using System.Collections;
using System.Configuration;
using System.Data;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class CommonModule_grdBind : System.Web.UI.UserControl
{
    private int TotalRecords;
    private int TotalPages;
    private int CurrentPage;
    private int PageSize;
    private string strSortOrder = String.Empty;

    public event CommandEventHandler NavigationLink;
    public event EventHandler addbtnHandler;
    public event EventHandler removebtnHandler;



    public ImageButton AddBtn
    {
        get { return addbtn; }
        set { addbtn = value; }
    }

    public bool addvisible
    {
        get { return addbtn.Visible; }
        set { addbtn.Visible = value; }
    }

    public bool delvisible
    {
        get { return removeBtn.Visible; }
        set { removeBtn.Visible = value; }
    }


    public ImageButton RemoveBtn
    {
        get { return removeBtn; }
        set { removeBtn = value; }
    }



    public string LinkClientID
    {
        get { return linkPostBack.ClientID; }
    }


    public string RemovePostBackID
    {
        get { return removePostBackObj.ClientID; }

    }
    public bool ExportVisible
    {
        get { return lnkBtnExport.Visible; }
        set { lnkBtnExport.Visible = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page Load
    }

    protected void addbtnHandler_Click(object sender, EventArgs e)
    {
        if (addbtnHandler != null) addbtnHandler(this, EventArgs.Empty);
    }


    protected void removebtnHandler_Click(object sender, EventArgs e)
    {
        if (removebtnHandler != null) removebtnHandler(this, EventArgs.Empty);
    }


    protected void NavigationLink_Click(object sender, CommandEventArgs e)
    {
        if (NavigationLink != null) NavigationLink(this, e);
    }




    public void GridViewPager(GridView grd, DataTable dt, CommandEventArgs e)
    {
        int RecordCount;

        try
        {
            PageSize = grd.PageSize;
            double i1;
            int i;

            switch (e.CommandName)
            {
                case "First":
                    ViewState["CurrentPage"] = 0;
                    grd.PageIndex = 0;
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    _previousPageLink.Enabled = false;
                    _nextPageLink.Enabled = true;
                    FirstPage.Enabled = false;
                    LastPage.Enabled = true;

                    TotalRecords = (int)dt.Rows.Count;

                    i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                    i = Convert.ToInt32(Math.Ceiling(i1));

                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    break;

                case "Last":
                    TotalPages = Convert.ToInt32(ViewState["GridPageCount"]);
                    TotalPages = (int)grd.PageCount; ;
                    ViewState["CurrentPage"] = TotalPages - 1;
                    grd.PageIndex = TotalPages - 1; //since page number starts from 0
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    _nextPageLink.Enabled = false;
                    _previousPageLink.Enabled = true;
                    LastPage.Enabled = false;
                    FirstPage.Enabled = true;

                    RecordCount = (TotalPages - 1) * PageSize;
                    TotalRecords = (int)dt.Rows.Count;

                    i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                    i = Convert.ToInt32(Math.Ceiling(i1));

                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    break;
                case "Page":
                    TotalPages = Convert.ToInt32(ViewState["GridPageCount"]);
                    TotalPages = (int)grd.PageCount; ;
                    ViewState["CurrentPage"] = ddlNavigation.SelectedValue;
                    grd.PageIndex = ddlNavigation.SelectedIndex; //since page number starts from 0
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    if (ddlNavigation.SelectedIndex == TotalPages)
                    {
                        _nextPageLink.Enabled = false;
                        LastPage.Enabled = false;
                    }
                    if (ddlNavigation.SelectedIndex == 0)
                    {
                        _previousPageLink.Enabled = false;

                        FirstPage.Enabled = true;
                    }

                    RecordCount = (TotalPages - 1) * PageSize;
                    TotalRecords = (int)dt.Rows.Count;

                    i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                    i = Convert.ToInt32(Math.Ceiling(i1));

                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    break;

                case "Next":
                    CurrentPage = (int)ViewState["CurrentPage"];
                    CurrentPage = CurrentPage + 1;
                    ViewState["CurrentPage"] = CurrentPage;
                    grd.PageIndex = CurrentPage;
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    RecordCount = CurrentPage * PageSize;
                    TotalRecords = (int)dt.Rows.Count;

                    if (CurrentPage == grd.PageCount - 1)
                    {
                        _nextPageLink.Enabled = false;
                        _previousPageLink.Enabled = true;
                        LastPage.Enabled = false;
                        FirstPage.Enabled = true;

                        i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                        i = Convert.ToInt32(Math.Ceiling(i1));
                        lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                        lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    }
                    else
                    {
                        _nextPageLink.Enabled = true;
                        _previousPageLink.Enabled = true;
                        LastPage.Enabled = true;
                        FirstPage.Enabled = true;


                        i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                        i = Convert.ToInt32(Math.Ceiling(i1));
                        lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                        lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    }
                    break;

                case "Prev":
                    CurrentPage = (int)ViewState["CurrentPage"];
                    CurrentPage = CurrentPage - 1;
                    ViewState["CurrentPage"] = CurrentPage;
                    grd.PageIndex = CurrentPage;
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    RecordCount = CurrentPage * PageSize;
                    TotalRecords = (int)dt.Rows.Count;

                    if (CurrentPage == 0)
                    {
                        _previousPageLink.Enabled = false;
                        _nextPageLink.Enabled = true;
                        FirstPage.Enabled = false;
                        LastPage.Enabled = true;


                        i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                        i = Convert.ToInt32(Math.Ceiling(i1));

                        lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                        lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";

                    }
                    else
                    {
                        _nextPageLink.Enabled = true;
                        _previousPageLink.Enabled = true;
                        LastPage.Enabled = true;
                        FirstPage.Enabled = true;


                        i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                        i = Convert.ToInt32(Math.Ceiling(i1));
                        lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                        lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    }
                    break;

                case "All":
                    _nextPageLink.Enabled = false;
                    _previousPageLink.Enabled = false;
                    LastPage.Enabled = false;
                    FirstPage.Enabled = false;
                    grd.AllowPaging = false;
                    //lnkShowAllData.Text = "Show Paged Data";
                    //lnkShowAllData.CommandName = "None";
                    lblPagesCount.Text = "Page " + "1" + " of " + "1" + "";
                    grd.DataSource = dt;
                    grd.DataBind();
                    break;
                case "None":
                    ViewState["CurrentPage"] = 0;
                    grd.AllowPaging = true;
                    //lnkShowAllData.Text = "Show All Data";
                    //lnkShowAllData.CommandName = "All";
                    grd.PageIndex = 0;
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    _previousPageLink.Enabled = false;
                    _nextPageLink.Enabled = true;
                    FirstPage.Enabled = false;
                    LastPage.Enabled = true;

                    TotalRecords = (int)dt.Rows.Count;

                    i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                    i = Convert.ToInt32(Math.Ceiling(i1));

                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    break;

                case "Export":

                    try
                    {
                        // Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                        // ExcelHelper exHelp = new ExcelHelper();
                        grd.AllowPaging = false;
                        grd.DataSource = dt;
                        grd.DataBind();
                        DataTable properFormat = new DataTable();
                        for (int k = 0; k < grd.Columns.Count; k++)
                        {
                            if (grd.Columns[k].Visible)
                            {
                                properFormat.Columns.Add(grd.Columns[k].HeaderText);
                            }
                        }
                        int num = 0;
                        object[] vals = null;
                        int j = 0;
                        for (j = 0; j < grd.Rows.Count; j++)
                        {
                            vals = new object[properFormat.Columns.Count];
                            num = 0;
                            for (int k = 0; k < grd.Columns.Count; k++)
                            {
                                if (grd.Columns[k].Visible)
                                {
                                    vals[num] = grd.Rows[j].Cells[k].Text;
                                    if (vals[num] == null || vals[num].ToString() == "")
                                    {
                                        int ctrlCnt = grd.Rows[j].Cells[k].Controls.Count;
                                        foreach (Control ctrl in grd.Rows[j].Cells[k].Controls)
                                        {
                                            string type = ctrl.ToString();
                                            if (type != "System.Web.UI.LiteralControl")
                                            {
                                                if (type == "System.Web.UI.WebControls.Label")
                                                {
                                                    vals[num] = ((Label)ctrl).Text;
                                                }
                                                if (type == "System.Web.UI.DataBoundLiteralControl")
                                                {
                                                    vals[num] = ((DataBoundLiteralControl)ctrl).Text.Trim();
                                                }
                                               
                                            }
                                        }
                                    }
                                    num++;
                                }
                            }
                            DataRow properRow = properFormat.NewRow();
                            properRow.ItemArray = vals;
                            properFormat.Rows.Add(properRow);
                           
                        }
                        grd.AllowPaging = true;
                        grd.DataSource = dt;

                        grd.DataBind();
                        if (properFormat.Columns.Contains("Actions"))
                            properFormat.Columns.Remove("Actions");
                        if (properFormat.Columns.Contains("Action"))
                            properFormat.Columns.Remove("Action");
                        SessionValues.ExportData = properFormat;
                        SessionValues.ExportFileName = grd.RowHeaderColumn;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "<script language=javascript> window.open('../Print/Exporter.aspx','', 'left=300,top=200,screenX=200,screenY=100,width=600,height=300,scrollbars=yes,toolbar=no,location=no,directories=no,status=yes,menubar=no,resizable=no,copyhistory=no');</script>", false);


                    }
                    catch (Exception es)
                    {
                        CustomMessage.DisplayMessage(this.Page, "Data could not be exported. Please try again later.", CustomMessage.MessageType.INFO);
                    }
                    break;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }

    }

    public void GridViewPagerSession(GridView grd, DataTable dt,int pageno)
    {
        int RecordCount;

        try
        {
            PageSize = grd.PageSize;
            double i1;
            int i;

            CurrentPage = pageno;
            //CurrentPage = CurrentPage + 1;
            ViewState["CurrentPage"] = pageno;
            grd.PageIndex = pageno;
                    grd.DataSource = SetPageSortOrder(dt);
                    grd.DataBind();
                    grd.Visible = true;

                    RecordCount = CurrentPage * PageSize;
                    TotalRecords = (int)dt.Rows.Count;

                    if (CurrentPage == grd.PageCount - 1)
                    {
                        _nextPageLink.Enabled = false;
                        _previousPageLink.Enabled = true;
                        LastPage.Enabled = false;
                        FirstPage.Enabled = true;

                        i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                        i = Convert.ToInt32(Math.Ceiling(i1));
                        lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                        lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    }
                    else
                    {
                        _nextPageLink.Enabled = true;
                        _previousPageLink.Enabled = true;
                        LastPage.Enabled = true;
                        FirstPage.Enabled = true;


                        i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                        i = Convert.ToInt32(Math.Ceiling(i1));
                        lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                        lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    }
                   

        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }

        finally
        {
            if (dt != null)
            {
                dt.Dispose();
            }
        }

    }


    public void BindDetails(GridView grd, DataTable dt)
    {
        try
        {
            string strSortExpression = hfSortExpression.Value;
            strSortOrder = SetSortOrder("Asc");
            if (dt != null)
            {
                grd.PageIndex = 0;
                DataView dv;
                dv = new DataView(dt);
                if (strSortExpression != "")
                {
                    dv.Sort = strSortExpression + " " + strSortOrder;
                }
                grd.DataSource = dv;
                grd.DataBind();



                TotalRecords = (int)dt.Rows.Count;
                TotalPages = (int)grd.PageCount;
                ViewState["GridPageCount"] = grd.PageCount;
                ViewState["CurrentPage"] = 0;
                _previousPageLink.Enabled = false;
                _nextPageLink.Enabled = true;
                FirstPage.Enabled = false;
                LastPage.Enabled = true;
                //lnkShowAllData.Enabled = dt.Rows.Count > grd.PageSize;

                double i1 = (double)dt.Rows.Count / (double)grd.PageSize;
                int i = Convert.ToInt32(Math.Ceiling(i1));

                if (TotalRecords == 0)
                {
                    _lblStatus.Visible = true;
                    _lblStatus.CssClass = "recordstatus";
                }
                else
                    _lblStatus.Visible = false;

                if (TotalRecords == 0)
                {
                    // grd.Visible = false;
                    _nextPageLink.Enabled = false;
                    _previousPageLink.Enabled = false;
                    FirstPage.Enabled = false;
                    LastPage.Enabled = false;
                    lblPagesCount.Text = "  Page " + "0" + " of " + i + "";
                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    _lblStatus.Text = "No Records Found";



                }
                else if (TotalRecords <= grd.PageSize)
                {
                    grd.Visible = true;
                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    lblPagesCount.Text = "Page " + (grd.PageIndex + 1) + " of " + i + "";
                    _nextPageLink.Enabled = false;
                    LastPage.Enabled = false;
                }
                else
                {

                    grd.Visible = true;
                    lblTotalRecords.Text = "Total Records: " + TotalRecords.ToString() + "  ";
                    lblPagesCount.Text = "  Page " + (grd.PageIndex + 1) + " of " + i + "";
                    lblPagesCount.Visible = true;
                }
            }
            else
            {
                grd.DataSource = dt;
                grd.DataBind();
                //grd.Visible = false;
                _nextPageLink.Enabled = false;
                _previousPageLink.Enabled = false;
                FirstPage.Enabled = false;
                LastPage.Enabled = false;
                lblPagesCount.Text = "  Page " + "0" + " of " + "0" + "";
                lblTotalRecords.Text = "Total Records: 0  ";
                _lblStatus.Text = "No Records Found";
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message); }

        finally
        {
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }


    public void GridViewSorting(GridView grd, DataTable dt, GridViewSortEventArgs e)
    {
        try
        {
            hfSortExpression.Value = e.SortExpression;
            BindDetails(grd, dt);
            addImage(grd);
        }
        catch
        {
        }
    }

    //To get the index of sorted column
    public Int32 getSortIndex(GridView grd)
    {
        if (hfSortExpression.Value == string.Empty) return 1;

        foreach (DataControlField field in grd.Columns)
        {
            if (field.SortExpression.ToString() == hfSortExpression.Value)
                return grd.Columns.IndexOf(field);
        }
        return -1;
    }


    private void addImage(GridView grd)
    {
        try
        {
            //int intSortIndex = getSortIndex(grd);
            //foreach (GridViewRow gvr in grd.Rows)
            //{
            //    for (int i = 0; i <= gvr.Cells.Count - 1; i++)
            //    {

            //        if (i == intSortIndex)
            //        {
            //            Image sortImage = new Image();
            //            if (String.IsNullOrEmpty(hfSort.Value))
            //                sortImage.ImageUrl = "https://venus.wis.ntu.edu.sg/images/arrow_down.gif";
            //            else if (hfSort.Value == "Asc")
            //                sortImage.ImageUrl = "https://venus.wis.ntu.edu.sg/images/arrow_up.gif";
            //            else if (hfSort.Value == "Desc")
            //                sortImage.ImageUrl = "https://venus.wis.ntu.edu.sg/images/arrow_down.gif";
            //            grd.HeaderRow.Cells[i].Controls.Add(sortImage);

            //            grd.HeaderRow.Cells[i].Font.Bold = true;
            //            grd.HeaderRow.Cells[i].VerticalAlign = VerticalAlign.Middle;
            //            grd.HeaderRow.Cells[i].HorizontalAlign = HorizontalAlign.Left;
            //            break;
            //        }
            //    }
            //    break;
            //}
        }
        catch { }
    }


    private DataView SetPageSortOrder(DataTable dt)
    {
        DataView dv = null;
        try
        {
            dv = new DataView(dt);
            dv.Sort = hfSortExpression.Value + " " + hfSort.Value;
        }
        catch
        {
        }
        return dv;
    }


    private String SetSortOrder(String strSortOrder)
    {
        if (String.IsNullOrEmpty(hfSort.Value))
        {
            hfSort.Value = strSortOrder.ToString();
        }
        else if (hfSort.Value == "Asc")
        {
            hfSort.Value = "Desc";
        }
        else if (hfSort.Value == "Desc")
        {
            hfSort.Value = "Asc";
        }

        return hfSort.Value;
    }

}
