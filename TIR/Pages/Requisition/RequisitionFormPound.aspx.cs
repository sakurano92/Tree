using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Requisition_RequisitionFormPound : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        #region Till Login not made
        //string user = Request.QueryString["user_type"];
        //if (user == "2")
        //{
        //    SessionValues.IsRM = false; SessionValues.IsApprover = false;
        //}
        //else if (user == "3") SessionValues.IsRM = true;
        //else if (user == "1") SessionValues.IsRM = true;
        //else
        //{
        //    SessionValues.IsRM = false; 
        //    SessionValues.RoleIdSession = user;
        //    SessionValues.IsApprover = true;
        //}
        #endregion

        selectview();
    }
    public void selectview()
    {
        string req_id = Request.QueryString["req_id"];

        RequisitionMgr reqMgr = new RequisitionMgr();
        if (reqMgr.IsOpenRequisition(req_id))
        {
            
            mvRequisition.ActiveViewIndex = 0;//editable Requisition
        }
        else
        {
            mvRequisition.ActiveViewIndex = 1;//Only Viewed by users
           
        }
    }
   

    
   
}
