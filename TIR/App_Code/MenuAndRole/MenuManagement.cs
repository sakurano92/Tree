using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for MenuManagement
/// </summary>
public class MenuManagement : DatabaseHelper
{
    private SqlCommand objCmd;

	public MenuManagement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetAllParentMenu()
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = @"SELECT * FROM BS_MENUS where parent_menu_id is null and disabled!=1 order by menu_order";
        return base.GetDataResult(objCmd);
    }
    public DataTable GetAllSubMenu(string parent_menu)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = @"SELECT * FROM BS_MENUS where parent_menu_id='" + parent_menu
            + "' and disabled!=1  order by menu_order";
        return base.GetDataResult(objCmd);
    }
    public DataTable GetParentMenu()
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = @"SELECT * FROM BS_MENUS where parent_menu_id is null
and disabled!=1 and menu_id in (select menu_id from bs_role_menu_map where role_id ='" + SessionValues.RoleIdSession 
+
        "') order by menu_order";
        return base.GetDataResult(objCmd);
    }
    public DataTable GetSubMenu(string parent_menu)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = @"SELECT * FROM BS_MENUS where parent_menu_id='" + parent_menu
            + "' and disabled!=1  and menu_id in (select menu_id from bs_role_menu_map where role_id ='" + SessionValues.RoleIdSession
            +"') order by menu_order";
        return base.GetDataResult(objCmd);
    }
    public DataTable GetRoleMenus(string role)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT DISTINCT (MENU_ID) FROM BS_ROLE_MENU_MAP WHERE ROLE_ID = " + role;
        return base.GetDataResult(objCmd);
    }
    public int SetRoleMenus(string role, CheckBoxList chkMenus)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        int count = 0;
        objCmd.CommandText = "DELETE FROM BS_ROLE_MENU_MAP WHERE ROLE_ID = " + role;
        objCmd.ExecuteNonQuery();
        for (int i = 0; i < chkMenus.Items.Count; i++)
        {
            if (chkMenus.Items[i].Selected == true)
            {
                objCmd.CommandText = "INSERT INTO BS_ROLE_MENU_MAP VALUES(" + role + " , " + chkMenus.Items[i].Value + ");";
                try
                {
                    objCmd.ExecuteNonQuery();
                }
                catch
                {
                    return -1;
                }
                count++;
            }
        }
        return count;
    }
}
