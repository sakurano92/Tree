using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;

/// <summary>
/// Summary description for AutoFillUp
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class AutoFillUp : System.Web.Services.WebService {

    public AutoFillUp () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string[] GetSupplierDetails(string prefixText, string contextKey)
    {
        DataManagement dm = new DataManagement();
        string query = @"SELECT supplier_id,supplier_code+': '+supplier_name+'
'+supplier_address supplier_detail FROM bs_suppliers
where supplier_code+' '+supplier_name+' '+supplier_address like '%" + prefixText + "' + '%'";
        if (contextKey == "name")
            query = string.Format(query, "supplier_detail");
        else if (contextKey == "id")
            query = string.Format(query, "supplier_id");
        DataTable dt = dm.GetData(query);
        List<string> txtItems = new List<string>();
        String dbValues;

        foreach (DataRow row in dt.Rows)
        {
            //String From DataBase(dbValues)
            dbValues = row["supplier_detail"].ToString();
            txtItems.Add(dbValues);
        }

        return txtItems.ToArray();

    }
}
