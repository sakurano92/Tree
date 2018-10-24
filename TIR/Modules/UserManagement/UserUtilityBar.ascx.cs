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

public partial class Modules_UserManagement_UserUtilityBar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        txtSearchKeyword.Attributes.Add("onFocus", "if(this.value == 'User Id/User Name/Email') {this.value='';}");
        txtSearchKeyword.Attributes.Add("onblur", "if(this.value == '') {this.value='User Id/User Name/Email';}");

    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        object o = Page;
        string searchKeyWord = txtSearchKeyword.Text == "User Id/User Name/Email" ? string.Empty : txtSearchKeyword.Text;
        o.GetType().InvokeMember("LoadUserData", System.Reflection.BindingFlags.InvokeMethod, null, o, new object[] { searchKeyWord });
        

    }

    

}
