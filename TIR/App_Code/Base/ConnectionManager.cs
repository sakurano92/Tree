using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RIAS.DBAccess
{
    public class ConnectionManager
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static SqlConnection objSqlConnection;

        public static SqlConnection GetConnection()
        {
            // Get the connection string from the configuration file
            if (objSqlConnection == null)
                objSqlConnection = new SqlConnection(connectionString);

            if (objSqlConnection.State == ConnectionState.Closed)
                objSqlConnection.Open();

            return objSqlConnection;
        }
      

        public ConnectionManager()
        {
            //
            // TODO: Add constructor logic here
            //

        }

    }
}
