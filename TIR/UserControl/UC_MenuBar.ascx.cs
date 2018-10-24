using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class UserControl_UC_MenuBar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        LoadUserInfo();
        LoadMenus();
    }

    private void LoadUserInfo()
    {
        if (SessionValues.UserIdSession != string.Empty)
        {
            lblLogin.Text = "Login name:";
            lblLoginId.Text = SessionValues.UserNameSession;
            lblLoginTime.Text = SessionValues.LoginTimeSession;
            lblSignout.Visible = true;
        }
    }

    private void LoadMenus()
    {
        ///Currently works for two level menu only.
        MenuManagement mnuMgr = new MenuManagement();
        DataTable dtMenu = mnuMgr.GetParentMenu();
        DataTable dtSubMenu = new DataTable();
        int i = 0;
        foreach (DataRow dr in dtMenu.Rows)
        {
            Menu1.Items.Add(dr["menu_text"].ToString(), dr["menu_name"].ToString(), dr["image_url"].ToString(), dr["navigateUrl"].ToString());
            dtSubMenu = mnuMgr.GetSubMenu(dr["menu_id"].ToString());
            foreach (DataRow drSub in dtSubMenu.Rows)
            {
                Menu1.Items[i].Items.Add(drSub["menu_text"].ToString(), drSub["menu_name"].ToString(), drSub["image_url"].ToString(), drSub["navigateUrl"].ToString());
            }
            i++;
        }

    }
}
