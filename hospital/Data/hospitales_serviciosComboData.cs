using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using hospital.Models;

namespace hospital.Data
{

    public class hospitales_servicios_hospitalesData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitales_servicios_hospitales17Select]";
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

        public static List<hospitales> List()
        {
            List<hospitales> hospitalesList = new List<hospitales>();
            SqlConnection connection = hospitalData.GetConnection();
            String selectProcedure = "[hospitales_servicios_hospitales17Select]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                hospitales hospitales = new hospitales();
                while (reader.Read())
                {
                    hospitales = new hospitales();
                    hospitales.Cod_hospital = System.Convert.ToInt32(reader["Cod_hospital"]);
                    hospitales.Nombre = Convert.ToString(reader["Nombre"]);
                    hospitalesList.Add(hospitales);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return hospitalesList;
            }
            finally
            {
                connection.Close();
            }
            return hospitalesList;
        }

    }

    public class hospitales_servicios_serviciosData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitales_servicios_servicios18Select]";
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

        public static List<servicios> List()
        {
            List<servicios> serviciosList = new List<servicios>();
            SqlConnection connection = hospitalData.GetConnection();
            String selectProcedure = "[hospitales_servicios_servicios18Select]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                servicios servicios = new servicios();
                while (reader.Read())
                {
                    servicios = new servicios();
                    servicios.Id_servicio = System.Convert.ToInt32(reader["Id_servicio"]);
                    servicios.Nombre_servicio = Convert.ToString(reader["Nombre_servicio"]);
                    serviciosList.Add(servicios);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return serviciosList;
            }
            finally
            {
                connection.Close();
            }
            return serviciosList;
        }

    }

}

 
