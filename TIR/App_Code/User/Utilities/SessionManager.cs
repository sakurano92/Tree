using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Data;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public static class SessionManager
{
    /// <summary>
    /// Generic Function to create session 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetSession(string key, string value) {
        HttpContext.Current.Session[key] = value;
    }

    /// <summary>
    /// Generc function to retrieve session
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetSession(string key) {
        string strValue = "";
        if (HttpContext.Current.Session[key] != null)
            strValue = HttpContext.Current.Session[key].ToString();
        else if (key == "USER_ID")
            HttpContext.Current.Response.Redirect("~/Default.aspx");
        
        return strValue;
    }
    public static void SetSessionObject(string key, object value)
    {
        HttpContext.Current.Session[key] = value;
    }
    public static object GetSessionObject(string key)
    {
        object dt = null;
        if (HttpContext.Current.Session[key] != null)
            dt = HttpContext.Current.Session[key];
      
        return dt;
    }

    public static string EligibilityChecksum() {
        string _strCheckSum = GetSession("EligibilityChecksum") == null ? Guid.NewGuid().ToString() : GetSession("EligibilityChecksum");

        SetSession("EligibilityChecksum", _strCheckSum);
        return _strCheckSum;
    }

    public static void ResetEligibilityChecksum() {
        HttpContext.Current.Session.Remove("EligibilityChecksum");
    }

    public static void RemoveKey(string key) {
        if (HttpContext.Current.Session[key] != null)
            HttpContext.Current.Session.Remove(key);
    }  
}
