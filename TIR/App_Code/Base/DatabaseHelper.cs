using System;
using System.Data;
using System.Configuration;
//using System.Linq;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DatabaseHelper
/// </summary>
public class DatabaseHelper : RIAS.DBAccess.ConnectionManager
{
    protected SqlConnection sqlCon = RIAS.DBAccess.ConnectionManager.GetConnection();
    SqlDataAdapter objSqlDA;
    SqlCommand objSqlCommand;
    Exception objLastException;

	public DatabaseHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected Exception GetLastError
    {
        get { return objLastException; }
    }

    protected SqlCommand GetSqlCommand()
    {

        objSqlCommand = new SqlCommand();
        objSqlCommand.Connection = sqlCon;
        objSqlCommand.CommandType = CommandType.StoredProcedure;

        return objSqlCommand;
    }
    protected SqlCommand GetSqlCommand(string query)
    {

        objSqlCommand = new SqlCommand(query);
        objSqlCommand.Connection = sqlCon;
        return objSqlCommand;
    }

    protected CommandType CommandType
    {
        set { objSqlCommand.CommandType = value; }
    }
    protected SqlDataAdapter GetSqlDataAdapter()
    {
        objSqlDA = new SqlDataAdapter();
        return objSqlDA;
    }


    protected DataTable GetDataResult(SqlCommand objCmd)
    {
            DataTable dt = new DataTable();
            objSqlDA = new SqlDataAdapter(objCmd);
            objSqlDA.Fill(dt);
            return dt;

    }
    protected DataTable GetDataResult(string sql)
    {
        SqlCommand objCmd = GetSqlCommand();
        objCmd.CommandType = CommandType.Text;
        objCmd.CommandText = sql;
        DataTable dt = new DataTable();
        objSqlDA = new SqlDataAdapter(objCmd);
        objSqlDA.Fill(dt);
        return dt;

    }
    protected string ExecuteScaler(SqlCommand objCmd)
    {
        DataTable dt = new DataTable();
        try
        {
            objSqlDA = new SqlDataAdapter(objCmd);
            objSqlDA.Fill(dt);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            else
                return "";
        }
        catch (SqlException es)
        {
            return "";
        }
    }

}

