using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using hospital.Models;

namespace hospital.Data
{

    public class camas_hospitales_serviciosData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[camas_hospitales_serviciosSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static List<hospitales_servicios> List()
        {
            List<hospitales_servicios> hospitales_serviciosList = new List<hospitales_servicios>();
            SqlConnection connection = hospitalData.GetConnection();
            String selectProcedure = "[camas_hospitales_serviciosSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                hospitales_servicios hospitales_servicios = new hospitales_servicios();
                while (reader.Read())
                {
                    hospitales_servicios = new hospitales_servicios();
                    hospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(reader["ID_hospitales_servicios"]);
                    hospitales_servicios.CodigoRefer = Convert.ToString(reader["CodigoRefer"]);
                    hospitales_serviciosList.Add(hospitales_servicios);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return hospitales_serviciosList;
            }
            finally
            {
                connection.Close();
            }
            return hospitales_serviciosList;
        }

    }

}

 
