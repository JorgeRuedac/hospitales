using System;
using System.Data;
using System.Data.SqlClient;

namespace hospital.Data
{
    public class hospitalData
    {
        public static string connectionString
                = "Data Source=DESKTOP-8797AMO\\SQLEXPRESS;Initial Catalog=hospital;Integrated Security=SSPI;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}



 
