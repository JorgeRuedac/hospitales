using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using hospital.Models;

namespace hospital.Data
{

    public class hospitales_medicoData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitales_medicoSelect]";
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

        public static List<medico> List()
        {
            List<medico> medicoList = new List<medico>();
            SqlConnection connection = hospitalData.GetConnection();
            String selectProcedure = "[hospitales_medicoSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                medico medico = new medico();
                while (reader.Read())
                {
                    medico = new medico();
                    medico.Cod_medico = System.Convert.ToInt32(reader["Cod_medico"]);
                    medico.Cedula = Convert.ToString(reader["Cedula"]);
                    medicoList.Add(medico);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return medicoList;
            }
            finally
            {
                connection.Close();
            }
            return medicoList;
        }

    }

}

 
