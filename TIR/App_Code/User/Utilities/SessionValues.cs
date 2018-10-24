using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for SessionValues
/// </summary>
public class SessionValues
{
   
    public static string UserIdSession
    {
        get { return SessionManager.GetSession("USER_ID"); }
        set { SessionManager.SetSession("USER_ID", value); }
    }
    public static string UserNameSession
    {
        get { return SessionManager.GetSession("USER_NAME"); }
        set { SessionManager.SetSession("USER_NAME", value); }
    }
    public static string UserFullNameSession
    {
        get { return SessionManager.GetSession("FULL_NAME"); }
        set { SessionManager.SetSession("FULL_NAME", value); }
    }
    public static string DeptSession
    {
        get { return SessionManager.GetSession("DEPT_ID"); }
        set { SessionManager.SetSession("DEPT_ID", value); }
    }
    public static string RoleIdSession
    {
        get { return SessionManager.GetSession("ROLE_ID"); }
        set { SessionManager.SetSession("ROLE_ID", value); }
    }
    public static string RoleNameSession
    {
        get { return SessionManager.GetSession("ROLE_NAME"); }
        set { SessionManager.SetSession("ROLE_NAME", value); }
    }
    public static string LoginTimeSession
    {
        get { return SessionManager.GetSession("LOGIN_TIME"); }
        set { SessionManager.SetSession("LOGIN_TIME", value); }
    }
    public static string PrintContent
    {
        get { return SessionManager.GetSession("PRINT_CONTENT"); }
        set { SessionManager.SetSession("PRINT_CONTENT", value); }
    }
    public static string ExportFileName
    {
        get { return SessionManager.GetSession("EXPORT_FILENAME"); }
        set { SessionManager.SetSession("EXPORT_FILENAME", value); }
    }
    public static DataTable ExportData
    {
        get { return (DataTable)SessionManager.GetSessionObject("EXPORT_DATA"); }
        set { SessionManager.SetSessionObject("EXPORT_DATA", value); }
    }
    public static bool IsRM
    {
        get { if (SessionManager.GetSession("IS_RM")=="") return false;
            return Convert.ToBoolean(SessionManager.GetSession("IS_RM")); }
        set { SessionManager.SetSession("IS_RM", value.ToString()); }
    }
    public static bool IsHOD
    {
        get
        {
            if (SessionManager.GetSession("IS_HOD") == "") return false;
            return Convert.ToBoolean(SessionManager.GetSession("IS_HOD"));
        }
        set { SessionManager.SetSession("IS_HOD", value.ToString()); }
    }
    public static bool IsApprover
    {
        get {
            if (SessionManager.GetSession("IS_APPROVER") == "") return false; 
            return Convert.ToBoolean(SessionManager.GetSession("IS_APPROVER"));
        }
        set { SessionManager.SetSession("IS_APPROVER", value.ToString()); }
    }
    public static bool IsPOApprover
    {
        get {
            if (SessionManager.GetSession("IS_POAPPROVER") == "") return false; 
            return Convert.ToBoolean(SessionManager.GetSession("IS_POAPPROVER")); }
        set { SessionManager.SetSession("IS_POAPPROVER", value.ToString()); }
    }
    public static bool IsClaimApprover
    {
        get {
            if (SessionManager.GetSession("IS_CLAIMAPPROVER") == "") return false;
            return Convert.ToBoolean(SessionManager.GetSession("IS_CLAIMAPPROVER"));
        }
        set { SessionManager.SetSession("IS_CLAIMAPPROVER", value.ToString()); }
    }
    public static string RMName
    {
        get { return SessionManager.GetSession("RM_NAME"); }
        set { SessionManager.SetSession("RM_NAME", value.ToString()); }
    }
    public static string PrincipalName
    {
        get { return SessionManager.GetSession("PRINCIPAL_NAME"); }
        set { SessionManager.SetSession("PRINCIPAL_NAME", value.ToString()); }
    }
    public static string AccountantName
    {
        get { return SessionManager.GetSession("ACCOUNTANT_NAME"); }
        set { SessionManager.SetSession("ACCOUNTANT_NAME", value.ToString()); }
    }
    public static void RemoveAllSessions()
    {
        SessionManager.RemoveKey("USER_ID");
        SessionManager.RemoveKey("USER_NAME");
        SessionManager.RemoveKey("ROLE_ID");
        SessionManager.RemoveKey("ROLE_NAME");
        SessionManager.RemoveKey("LOGIN_TIME");
        SessionManager.RemoveKey("PRINT_CONTENT");
        SessionManager.RemoveKey("FULL_NAME");
        SessionManager.RemoveKey("ROLE_ID");
        IsApprover = false;
        IsRM = false;
        IsClaimApprover = false;
        IsPOApprover = false;
        IsHOD = false;
    }
    public void SetSessionValue(string key, string value)
    {
        SessionManager.SetSession(key, value);
    }
    public string GetSessionValue(string key)
    {
        return SessionManager.GetSession(key);
    }
}
