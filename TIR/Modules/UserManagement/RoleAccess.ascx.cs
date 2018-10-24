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

public partial class Modules_UserManagement_RoleAccess : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetMenuAttribute();
        if (IsPostBack) return;

        
        LoadData();
        ClearAllFields();
    }

    private void SetMenuAttribute()
    {
        MenuManagement mnuMgr = new MenuManagement();
        DataTable dtMenu = mnuMgr.GetAllParentMenu();
        int i = 0;
        int parentid = 0;
        foreach (ListItem listItem in chkMenus.Items)
        {
            if (dtMenu.Select("menu_id=" + listItem.Value).Length == 0)
            {
                listItem.Attributes.CssStyle.Add("padding-left", "20px");
                listItem.Attributes.Add("onClick", "if(document.getElementById('ctl00_ContentMainBody_RoleAccess1_chkMenus_" + i.ToString() + "').checked==true) document.getElementById('ctl00_ContentMainBody_RoleAccess1_chkMenus_" + parentid.ToString() + "').checked=true;");
            }
            else
            {
                parentid = i;
            }
            i++;
        }
    }

    public void ClearAllFields()
    {
        CustomMessage.ClearMessage(this.Page);
    }

    public void LoadData()
    {
        DataManagement dm = new DataManagement();
        dm.LoadrdbList(rdbRoles, "BS_USER_ROLES", "ROLE_ID", "ROLE_NAME"," 1=1");
     //   dm.LoadChkList(chkMenus, "BS_MENUS", "MENU_ID", "MENU_TEXT", " 1=1","menu_order,parent_menu_id");
       // chkMenus.Items[2].Attributes.CssStyle.Add("padding-left", "20px");

        MenuManagement mnuMgr = new MenuManagement();
        DataTable dtMenu = mnuMgr.GetAllParentMenu();
        DataTable dtSubMenu = new DataTable();
        int i = 0;
        int parentid = 0;
        foreach (DataRow dr in dtMenu.Rows)
        {
            chkMenus.Items.Add(new ListItem(dr["menu_text"].ToString(), dr["menu_id"].ToString()));
            dtSubMenu = mnuMgr.GetAllSubMenu(dr["menu_id"].ToString());
            parentid = i;
            i++;
            foreach (DataRow drSub in dtSubMenu.Rows)
            {
                chkMenus.Items.Add(new ListItem(drSub["menu_text"].ToString(), drSub["menu_id"].ToString()));
                chkMenus.Items[i].Attributes.CssStyle.Add("padding-left","20px");
               // CheckBox chkbx = (CheckBox)chkMenus.Items[i];
                ListItem item = (ListItem)chkMenus.Items[i];
                item.Attributes.Add("onClick", "if(document.getElementById('ctl00_ContentMainBody_RoleAccess1_chkMenus_" + i.ToString() + "').checked==true) document.getElementById('ctl00_ContentMainBody_RoleAccess1_chkMenus_" + parentid.ToString() + "').checked=true;");
                i++;
            }
        }
    }

    protected void SelectAll(object sender, EventArgs e)
    {
        foreach (ListItem listItem in chkMenus.Items)
        {
            listItem.Selected = true;
        }
       
        CustomMessage.ClearMessage(this.Page);
    }

    protected void ClearAll(object sender, EventArgs e)
    {
        foreach (ListItem listItem in chkMenus.Items)
        {
            listItem.Selected = false;
        }
        CustomMessage.ClearMessage(this.Page);
    }

    protected void Save(object sender, EventArgs e)
    {
        MenuManagement mnuMgr = new MenuManagement();
        string role = rdbRoles.SelectedItem.Value;
        mnuMgr.SetRoleMenus(role, chkMenus);
        CustomMessage.DisplayMessage(this.Page, "Roles Saved!!", CustomMessage.MessageType.SUCCESS);
    }

    protected void RoleChange(object sender, EventArgs e)
    {
        ClearAll(sender, e);
        MenuManagement mnuMgr = new MenuManagement();
        string role=rdbRoles.SelectedItem.Value;
        DataTable dt = mnuMgr.GetRoleMenus(role);

        foreach (DataRow dr in dt.Rows)
        {
            for (int i = 0; i < chkMenus.Items.Count; i++)
            {
                if (chkMenus.Items[i].Value == dr["MENU_ID"].ToString())
                {
                    chkMenus.Items[i].Selected = true;
                }
            }
        }

    }
}
