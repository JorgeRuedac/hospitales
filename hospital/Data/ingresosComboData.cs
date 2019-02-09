using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using hospital.Models;

namespace hospital.Data
{

    public class ingresos_camasData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[ingresos_camas24Select]";
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

        public static List<camas> List()
        {
            List<camas> camasList = new List<camas>();
            SqlConnection connection = hospitalData.GetConnection();
            String selectProcedure = "[ingresos_camas24Select]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                camas camas = new camas();
                while (reader.Read())
                {
                    camas = new camas();
                    camas.id_cama = System.Convert.ToInt32(reader["id_cama"]);
                    camas.Num_cama = Convert.ToInt32(reader["Num_cama"]);
                    camasList.Add(camas);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return camasList;
            }
            finally
            {
                connection.Close();
            }
            return camasList;
        }

    }

    public class ingresos_historia_clinicaData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[ingresos_historia_clinica25Select]";
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

        public static List<historia_clinica> List()
        {
            List<historia_clinica> historia_clinicaList = new List<historia_clinica>();
            SqlConnection connection = hospitalData.GetConnection();
            String selectProcedure = "[ingresos_historia_clinica25Select]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                historia_clinica historia_clinica = new historia_clinica();
                while (reader.Read())
                {
                    historia_clinica = new historia_clinica();
                    historia_clinica.id_historia = System.Convert.ToInt32(reader["id_historia"]);
                    historia_clinica.Cedula = Convert.ToString(reader["Cedula"]);
                    historia_clinicaList.Add(historia_clinica);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return historia_clinicaList;
            }
            finally
            {
                connection.Close();
            }
            return historia_clinicaList;
        }

    }

}

 
