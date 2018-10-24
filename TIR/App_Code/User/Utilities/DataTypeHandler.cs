using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

public enum DataType
{
    SMALLINT,
    INT,
    LONG,
    DOUBLE,
    DATETIME,
    BOOLEAN,
    FLOAT
}


/// <summary>
/// Summary description for DataTypeHandler
/// </summary>
public class DataTypeHandler
{
	public DataTypeHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    // Function to set null values to database. 
    public static object HandleDataType(string data,DataType dtype)
    {
        object o = new object();
        if (data == string.Empty)
        {
            switch (dtype)
            {
                case DataType.DATETIME:
                    o = DBNull.Value;
                    break;
                case DataType.BOOLEAN:
                    o = false;
                    break;

                default:
                    o = DBNull.Value;
                    break;
            }
            return o;
        }
        else
        {
            switch (dtype)
            {
                case DataType.SMALLINT:
                    o = Convert.ToInt16(data);
                    break;
                case DataType.INT:
                    o = Convert.ToInt32(data);
                    break;
                case DataType.LONG:
                    o = Convert.ToInt64(data);
                    break;
                case DataType.DOUBLE:
                    o = Convert.ToDouble(data);
                    break;
                case DataType.DATETIME:
                    o = Convert.ToDateTime(data);
                    break;
                case DataType.BOOLEAN:
                    o = Convert.ToBoolean(Convert.ToInt16(data));
                    break;
                
            }
            return o;
        }
   
    }
   
}
