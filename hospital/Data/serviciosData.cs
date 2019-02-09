using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class serviciosData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[serviciosSelectAll]";
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

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[serviciosSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id Servicio") {
                selectCommand.Parameters.AddWithValue("@Id_servicio", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Id_servicio", DBNull.Value); }
            if (sField == "Nombre Servicio") {
                selectCommand.Parameters.AddWithValue("@Nombre_servicio", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre_servicio", DBNull.Value); }
            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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

        public static servicios Select_Record(servicios serviciosPara)
        {
            servicios servicios = new servicios();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[serviciosSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Id_servicio", serviciosPara.Id_servicio);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    servicios.Id_servicio = System.Convert.ToInt32(reader["Id_servicio"]);
                    servicios.Nombre_servicio = reader["Nombre_servicio"] is DBNull ? null : reader["Nombre_servicio"].ToString();
                }
                else
                {
                    servicios = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return servicios;
            }
            finally
            {
                connection.Close();
            }
            return servicios;
        }

        public static bool Add(servicios servicios)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[serviciosInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (servicios.Nombre_servicio != null) {
                insertCommand.Parameters.AddWithValue("@Nombre_servicio", servicios.Nombre_servicio);
            } else {
                insertCommand.Parameters.AddWithValue("@Nombre_servicio", DBNull.Value); }
            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(servicios oldservicios, 
               servicios newservicios)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[serviciosUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newservicios.Nombre_servicio != null) {
                updateCommand.Parameters.AddWithValue("@NewNombre_servicio", newservicios.Nombre_servicio);
            } else {
                updateCommand.Parameters.AddWithValue("@NewNombre_servicio", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldId_servicio", oldservicios.Id_servicio);
            if (oldservicios.Nombre_servicio != null) {
                updateCommand.Parameters.AddWithValue("@OldNombre_servicio", oldservicios.Nombre_servicio);
            } else {
                updateCommand.Parameters.AddWithValue("@OldNombre_servicio", DBNull.Value); }
            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(servicios servicios)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[serviciosDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldId_servicio", servicios.Id_servicio);
            if (servicios.Nombre_servicio != null) {
                deleteCommand.Parameters.AddWithValue("@OldNombre_servicio", servicios.Nombre_servicio);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldNombre_servicio", DBNull.Value); }
            deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
 
