using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class medicoData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[medicoSelectAll]";
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
            string selectProcedure = "[medicoSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Cod Medico") {
                selectCommand.Parameters.AddWithValue("@Cod_medico", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cod_medico", DBNull.Value); }
            if (sField == "Cedula") {
                selectCommand.Parameters.AddWithValue("@Cedula", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cedula", DBNull.Value); }
            if (sField == "Nombre") {
                selectCommand.Parameters.AddWithValue("@Apellido_medico", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Apellido_medico", DBNull.Value); }
            if (sField == "Fecha Nacimien") {
                selectCommand.Parameters.AddWithValue("@Fecha_nacimien", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha_nacimien", DBNull.Value); }
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

        public static medico Select_Record(medico medicoPara)
        {
            medico medico = new medico();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[medicoSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Cod_medico", medicoPara.Cod_medico);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    medico.Cod_medico = System.Convert.ToInt32(reader["Cod_medico"]);
                    medico.Cedula = reader["Cedula"] is DBNull ? null : reader["Cedula"].ToString();
                    medico.Apellido_medico = reader["Apellido_medico"] is DBNull ? null : reader["Apellido_medico"].ToString();
                    medico.Fecha_nacimien = reader["Fecha_nacimien"] is DBNull ? null : (DateTime?)reader["Fecha_nacimien"];
                }
                else
                {
                    medico = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return medico;
            }
            finally
            {
                connection.Close();
            }
            return medico;
        }

        public static bool Add(medico medico)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[medicoInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (medico.Cedula != null) {
                insertCommand.Parameters.AddWithValue("@Cedula", medico.Cedula);
            } else {
                insertCommand.Parameters.AddWithValue("@Cedula", DBNull.Value); }
            if (medico.Apellido_medico != null) {
                insertCommand.Parameters.AddWithValue("@Apellido_medico", medico.Apellido_medico);
            } else {
                insertCommand.Parameters.AddWithValue("@Apellido_medico", DBNull.Value); }
            if (medico.Fecha_nacimien.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Fecha_nacimien", medico.Fecha_nacimien);
            } else {
                insertCommand.Parameters.AddWithValue("@Fecha_nacimien", DBNull.Value); }
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

        public static bool Update(medico oldmedico, 
               medico newmedico)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[medicoUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newmedico.Cedula != null) {
                updateCommand.Parameters.AddWithValue("@NewCedula", newmedico.Cedula);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCedula", DBNull.Value); }
            if (newmedico.Apellido_medico != null) {
                updateCommand.Parameters.AddWithValue("@NewApellido_medico", newmedico.Apellido_medico);
            } else {
                updateCommand.Parameters.AddWithValue("@NewApellido_medico", DBNull.Value); }
            if (newmedico.Fecha_nacimien.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewFecha_nacimien", newmedico.Fecha_nacimien);
            } else {
                updateCommand.Parameters.AddWithValue("@NewFecha_nacimien", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCod_medico", oldmedico.Cod_medico);
            if (oldmedico.Cedula != null) {
                updateCommand.Parameters.AddWithValue("@OldCedula", oldmedico.Cedula);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCedula", DBNull.Value); }
            if (oldmedico.Apellido_medico != null) {
                updateCommand.Parameters.AddWithValue("@OldApellido_medico", oldmedico.Apellido_medico);
            } else {
                updateCommand.Parameters.AddWithValue("@OldApellido_medico", DBNull.Value); }
            if (oldmedico.Fecha_nacimien.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldFecha_nacimien", oldmedico.Fecha_nacimien);
            } else {
                updateCommand.Parameters.AddWithValue("@OldFecha_nacimien", DBNull.Value); }
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

        public static bool Delete(medico medico)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[medicoDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldCod_medico", medico.Cod_medico);
            if (medico.Cedula != null) {
                deleteCommand.Parameters.AddWithValue("@OldCedula", medico.Cedula);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCedula", DBNull.Value); }
            if (medico.Apellido_medico != null) {
                deleteCommand.Parameters.AddWithValue("@OldApellido_medico", medico.Apellido_medico);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldApellido_medico", DBNull.Value); }
            if (medico.Fecha_nacimien.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldFecha_nacimien", medico.Fecha_nacimien);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldFecha_nacimien", DBNull.Value); }
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
 
