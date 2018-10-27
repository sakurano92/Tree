using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for UserManagement
/// </summary>
public class UserManagement : DatabaseHelper
{
    private SqlCommand objCmd;

    public UserManagement()
    {
        //
        // TODO: Add constructor logic here
        //
    }
   
    public string InsertUser(string userName, string fullName, string email, string role, string status, string password, string phone)
    {
        fullName = fullName.Trim();
        userName = userName.Trim();
        email = email.Trim();
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add(new SqlParameter("@FULL_NAME", fullName));
        objCmd.Parameters.Add(new SqlParameter("@USER_NAME", userName));
        objCmd.Parameters.Add(new SqlParameter("@EMAIL", email));
        objCmd.Parameters.Add(new SqlParameter("@ROLE_ID", role));
        objCmd.Parameters.Add(new SqlParameter("@STATUS_ID", status));
        objCmd.Parameters.Add(new SqlParameter("@PASSWORD", password));
        objCmd.Parameters.Add(new SqlParameter("@PHONE", phone));
        objCmd.Parameters.Add(new SqlParameter("@CREATED_BY", SessionValues.UserIdSession));

        objCmd.CommandText = "SP_INSERT_USER";
        try
        {
            objCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public string SelfInsertUser(string userName, string fullName, string email, string role, string status)
    {
        fullName = fullName.Trim();
        userName = userName.Trim();
        email = email.Trim();
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add(new SqlParameter("@FULL_NAME", fullName));
        objCmd.Parameters.Add(new SqlParameter("@USER_NAME", userName));
        objCmd.Parameters.Add(new SqlParameter("@EMAIL", email));
        objCmd.Parameters.Add(new SqlParameter("@ROLE_ID", role));
        objCmd.Parameters.Add(new SqlParameter("@STATUS_ID", status));

        objCmd.CommandText = "SP_INSERT_SELF";
        try
        {
            objCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    
    public string UpdateUser(string userId, string fullName, string email, string role, string status, string password, string phone)
    {
        fullName = fullName.Trim();
        email = email.Trim();
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add(new SqlParameter("@USER_ID", userId));
        objCmd.Parameters.Add(new SqlParameter("@FULL_NAME", fullName));
        objCmd.Parameters.Add(new SqlParameter("@EMAIL", email));
        objCmd.Parameters.Add(new SqlParameter("@ROLE_ID", role));
        objCmd.Parameters.Add(new SqlParameter("@STATUS_ID", status));
        objCmd.Parameters.Add(new SqlParameter("@PASSWORD", password));
        objCmd.Parameters.Add(new SqlParameter("@PHONE", phone));
        objCmd.Parameters.Add(new SqlParameter("@UPDATEDBY", Convert.ToInt64(SessionValues.UserIdSession)));
        objCmd.CommandText = "SP_UPDATE_USER";
        try
        {
            objCmd.ExecuteNonQuery();
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    
    public bool DeactivateUser(string userId)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add(new SqlParameter("@USER_ID", userId));
        objCmd.Parameters.Add(new SqlParameter("@DEACTIVATED_BY", Convert.ToInt64(SessionValues.UserIdSession)));
        objCmd.CommandText = "SP_DEACTIVATE_USER";
        objCmd.ExecuteNonQuery();
        return true;

    }
    
    public bool ActivateUser(string userId)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add(new SqlParameter("@USER_ID", userId));
        objCmd.Parameters.Add(new SqlParameter("@ACTIVATED_BY", Convert.ToInt64(SessionValues.UserIdSession)));
        objCmd.CommandText = "SP_ACTIVATE_USER";
        objCmd.ExecuteNonQuery();
        return true;

    }


    public DataTable GetAllUsers(string searchKey)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = string.Format(@"SELECT BS_USERS.USER_ID,BS_USERS.USER_NAME,BS_USERS.FULL_NAME,BS_USERS.EMAIL,
BS_USER_STATUSES.STATUS_NAME,BS_USER_ROLES.ROLE_NAME,BS_USER_STATUSES.STATUS_ID,BS_USER_ROLES.ROLE_ID 
FROM BS_USERS INNER JOIN BS_USER_STATUSES ON BS_USERS.STATUS_ID = BS_USER_STATUSES.STATUS_ID 
JOIN BS_USER_ROLES ON BS_USERS.ROLE_ID = BS_USER_ROLES.ROLE_ID
 WHERE 
 (BS_USERS.USER_NAME  LIKE '%' + '{0}' + '%'
 OR BS_USERS.full_name  LIKE '%' + '{0}' + '%'
 OR BS_USERS.email  LIKE '%' + '{0}' + '%')", searchKey.Trim().Replace("'", "''"));
        return base.GetDataResult(objCmd);
    }

    public DataTable GetAllUsers(string searchKey, string role_id, string status_id)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = string.Format(@"SELECT BS_USERS.USER_ID,BS_USERS.USER_NAME,BS_USERS.FULL_NAME,BS_USERS.EMAIL,
BS_USER_STATUSES.STATUS_NAME,BS_USER_ROLES.ROLE_NAME,BS_USER_STATUSES.STATUS_ID,BS_USER_ROLES.ROLE_ID 
FROM BS_USERS INNER JOIN BS_USER_STATUSES ON BS_USERS.STATUS_ID = BS_USER_STATUSES.STATUS_ID 
JOIN BS_USER_ROLES ON BS_USERS.ROLE_ID = BS_USER_ROLES.ROLE_ID
 WHERE 
 (BS_USERS.USER_NAME  LIKE '%' + '{0}' + '%'
 OR BS_USERS.full_name  LIKE '%' + '{0}' + '%'
 OR BS_USERS.email  LIKE '%' + '{0}' + '%')", searchKey);
        if (role_id != "")
        {
            objCmd.CommandText += " AND (bs_users.role_id='" + role_id + "') ";
        }
        if (status_id != "")
        {
            objCmd.CommandText += " AND (bs_users.status_id='" + status_id + "') ";
        }
        objCmd.CommandText += "ORDER BY BS_USERS.STATUS_ID ASC";
        return base.GetDataResult(objCmd);
    }


    public bool DeleteUsers(string userId)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.StoredProcedure;
        objCmd.Parameters.Add(new SqlParameter("@USER_ID", userId));
        objCmd.Parameters.Add(new SqlParameter("@DELETED_BY", SessionValues.UserIdSession));
        objCmd.CommandText = "SP_DELETE_USER";
        string deleted = objCmd.ExecuteScalar().ToString();
        if (deleted == "1")
            return true;
        else
            return false;

    }


    public DataTable GetUsersById(string userId)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT * FROM BS_USERS WHERE USER_ID = '" + userId.Trim().Replace("'", "''") + "'";
        return base.GetDataResult(objCmd);
    }

    public bool IsUserInSystem(string username)
    {
        username = username.Trim().Replace("'", "''");
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT COUNT(USER_ID) FROM BS_USERS WHERE lower(USER_NAME) = lower('" + username + "')";
        return objCmd.ExecuteScalar().ToString() != "0";

    }

    public DataTable GetUserbyName(string UserName)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT * FROM BS_USERS WHERE USER_NAME = '" + UserName + "'";
        return base.GetDataResult(objCmd);

    }

    public DataTable GetCodeByUname(string UserName)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT * FROM BS_USERS WHERE USER_NAME = '" + UserName + "'";
        return base.GetDataResult(objCmd);

    }

    public DataTable RetrieveRole(string userId)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT ROLE_ID FROM BS_USERS WHERE USER_ID = '" + userId + "'";
        return base.GetDataResult(objCmd);

    }

    public DataTable GetUserRoles()
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = "SELECT (ROLE_ID , ROLE_NAME) FROM BS_USER_ROLES";
        return base.GetDataResult(objCmd);

    }

    public string GetCurrentUser(string userId)
    {
        objCmd = base.GetSqlCommand();
        objCmd.CommandText = "SELECT U.USER_NAME + ', ' + R.ROLE_NAME FROM BS_USERS U JOIN BS_USER_ROLES R ON U.ROLE_ID = R.ROLE_ID WHERE U.USER_ID = '" + userId + "'";
        objCmd.CommandType = CommandType.Text;
        return objCmd.ExecuteScalar().ToString();
    }

    public bool LoginUser(string UserName, string password)
    //public bool LoginUser(string UserName, string password)
    {
        UserName = UserName.Trim().Replace("'", "''");
        SessionValues.RemoveAllSessions();

        objCmd = base.GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = @"SELECT user_id,user_name,u.role_id,role_name,status_id,full_name FROM BS_USERS u
Left join bs_user_roles ur on u.role_id=ur.role_id
where user_name='" + UserName + "' and password = '" + password + "'" ;
        DataTable dt = base.GetDataResult(objCmd);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["status_id"].ToString() == "1")
            {
                SessionValues.UserIdSession = dt.Rows[0]["user_id"].ToString();
                SessionValues.RoleIdSession = dt.Rows[0]["role_id"].ToString();
                SessionValues.UserNameSession = dt.Rows[0]["user_name"].ToString();
                SessionValues.RoleNameSession = dt.Rows[0]["role_name"].ToString();
                SessionValues.UserFullNameSession = dt.Rows[0]["full_name"].ToString();
                int role_id = Convert.ToInt16(dt.Rows[0]["role_id"]);
                SessionValues.LoginTimeSession = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
                return true;
            }
        }
        return false;
    }

}
