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


public partial class Modules_UserManagement_LANUserInfo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        GetLDAPUser();

    }
    protected void GetLDAPUser()
    {
     
         //ActiveDirectoryMembershipUser usr = new ActiveDirectoryMembershipUser();
         ActiveDirectoryMembershipProvider provide = new ActiveDirectoryMembershipProvider();
         //txtSysInfo.Text += "Active direectory" + usr.UserName;
       // MembershipUserCollection collect= provide.GetAllUsers(0,1,out 2);
         txtSysInfo.Text += "Context.User.Identity.Name = " + Context.User.Identity.Name + "\n";
         txtSysInfo.Text += "Context.User.Identity.AuthenticationType = " + Context.User.Identity.AuthenticationType + "\n";
         txtSysInfo.Text += "Context.User.Identity.IsAuthenticated = " + Context.User.Identity.IsAuthenticated + "\n";
         for (int i = 0; i < Request.ServerVariables.Keys.Count; i++)
         {
             txtSysInfo.Text += Request.ServerVariables.Keys[i].ToString() + " = " + Request.ServerVariables[Request.ServerVariables.Keys[i].ToString()].ToString() + "\n";
         }
    }
    

}
