using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class historia_clinicaData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[historia_clinicaSelectAll]";
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
            string selectProcedure = "[historia_clinicaSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id Historia") {
                selectCommand.Parameters.AddWithValue("@id_historia", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@id_historia", DBNull.Value); }
            if (sField == "Cedula") {
                selectCommand.Parameters.AddWithValue("@Cedula", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cedula", DBNull.Value); }
            if (sField == "Apellido") {
                selectCommand.Parameters.AddWithValue("@Apellido", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Apellido", DBNull.Value); }
            if (sField == "Nombre") {
                selectCommand.Parameters.AddWithValue("@Nombre", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre", DBNull.Value); }
            if (sField == "Fecha Nacim") {
                selectCommand.Parameters.AddWithValue("@Fecha_nacim", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha_nacim", DBNull.Value); }
            if (sField == "Num Seguridad Social") {
                selectCommand.Parameters.AddWithValue("@Num_seguridad_social", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Num_seguridad_social", DBNull.Value); }
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

        public static historia_clinica Select_Record(historia_clinica historia_clinicaPara)
        {
            historia_clinica historia_clinica = new historia_clinica();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[historia_clinicaSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id_historia", historia_clinicaPara.id_historia);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    historia_clinica.id_historia = System.Convert.ToInt32(reader["id_historia"]);
                    historia_clinica.Cedula = reader["Cedula"] is DBNull ? null : reader["Cedula"].ToString();
                    historia_clinica.Apellido = reader["Apellido"] is DBNull ? null : reader["Apellido"].ToString();
                    historia_clinica.Nombre = reader["Nombre"] is DBNull ? null : reader["Nombre"].ToString();
                    historia_clinica.Fecha_nacim = reader["Fecha_nacim"] is DBNull ? null : (DateTime?)reader["Fecha_nacim"];
                    historia_clinica.Num_seguridad_social = reader["Num_seguridad_social"] is DBNull ? null : reader["Num_seguridad_social"].ToString();
                }
                else
                {
                    historia_clinica = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return historia_clinica;
            }
            finally
            {
                connection.Close();
            }
            return historia_clinica;
        }

        public static bool Add(historia_clinica historia_clinica)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[historia_clinicaInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (historia_clinica.Cedula != null) {
                insertCommand.Parameters.AddWithValue("@Cedula", historia_clinica.Cedula);
            } else {
                insertCommand.Parameters.AddWithValue("@Cedula", DBNull.Value); }
            if (historia_clinica.Apellido != null) {
                insertCommand.Parameters.AddWithValue("@Apellido", historia_clinica.Apellido);
            } else {
                insertCommand.Parameters.AddWithValue("@Apellido", DBNull.Value); }
            if (historia_clinica.Nombre != null) {
                insertCommand.Parameters.AddWithValue("@Nombre", historia_clinica.Nombre);
            } else {
                insertCommand.Parameters.AddWithValue("@Nombre", DBNull.Value); }
            if (historia_clinica.Fecha_nacim.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Fecha_nacim", historia_clinica.Fecha_nacim);
            } else {
                insertCommand.Parameters.AddWithValue("@Fecha_nacim", DBNull.Value); }
            if (historia_clinica.Num_seguridad_social != null) {
                insertCommand.Parameters.AddWithValue("@Num_seguridad_social", historia_clinica.Num_seguridad_social);
            } else {
                insertCommand.Parameters.AddWithValue("@Num_seguridad_social", DBNull.Value); }
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

        public static bool Update(historia_clinica oldhistoria_clinica, 
               historia_clinica newhistoria_clinica)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[historia_clinicaUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newhistoria_clinica.Cedula != null) {
                updateCommand.Parameters.AddWithValue("@NewCedula", newhistoria_clinica.Cedula);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCedula", DBNull.Value); }
            if (newhistoria_clinica.Apellido != null) {
                updateCommand.Parameters.AddWithValue("@NewApellido", newhistoria_clinica.Apellido);
            } else {
                updateCommand.Parameters.AddWithValue("@NewApellido", DBNull.Value); }
            if (newhistoria_clinica.Nombre != null) {
                updateCommand.Parameters.AddWithValue("@NewNombre", newhistoria_clinica.Nombre);
            } else {
                updateCommand.Parameters.AddWithValue("@NewNombre", DBNull.Value); }
            if (newhistoria_clinica.Fecha_nacim.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewFecha_nacim", newhistoria_clinica.Fecha_nacim);
            } else {
                updateCommand.Parameters.AddWithValue("@NewFecha_nacim", DBNull.Value); }
            if (newhistoria_clinica.Num_seguridad_social != null) {
                updateCommand.Parameters.AddWithValue("@NewNum_seguridad_social", newhistoria_clinica.Num_seguridad_social);
            } else {
                updateCommand.Parameters.AddWithValue("@NewNum_seguridad_social", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@Oldid_historia", oldhistoria_clinica.id_historia);
            if (oldhistoria_clinica.Cedula != null) {
                updateCommand.Parameters.AddWithValue("@OldCedula", oldhistoria_clinica.Cedula);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCedula", DBNull.Value); }
            if (oldhistoria_clinica.Apellido != null) {
                updateCommand.Parameters.AddWithValue("@OldApellido", oldhistoria_clinica.Apellido);
            } else {
                updateCommand.Parameters.AddWithValue("@OldApellido", DBNull.Value); }
            if (oldhistoria_clinica.Nombre != null) {
                updateCommand.Parameters.AddWithValue("@OldNombre", oldhistoria_clinica.Nombre);
            } else {
                updateCommand.Parameters.AddWithValue("@OldNombre", DBNull.Value); }
            if (oldhistoria_clinica.Fecha_nacim.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldFecha_nacim", oldhistoria_clinica.Fecha_nacim);
            } else {
                updateCommand.Parameters.AddWithValue("@OldFecha_nacim", DBNull.Value); }
            if (oldhistoria_clinica.Num_seguridad_social != null) {
                updateCommand.Parameters.AddWithValue("@OldNum_seguridad_social", oldhistoria_clinica.Num_seguridad_social);
            } else {
                updateCommand.Parameters.AddWithValue("@OldNum_seguridad_social", DBNull.Value); }
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

        public static bool Delete(historia_clinica historia_clinica)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[historia_clinicaDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@Oldid_historia", historia_clinica.id_historia);
            if (historia_clinica.Cedula != null) {
                deleteCommand.Parameters.AddWithValue("@OldCedula", historia_clinica.Cedula);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCedula", DBNull.Value); }
            if (historia_clinica.Apellido != null) {
                deleteCommand.Parameters.AddWithValue("@OldApellido", historia_clinica.Apellido);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldApellido", DBNull.Value); }
            if (historia_clinica.Nombre != null) {
                deleteCommand.Parameters.AddWithValue("@OldNombre", historia_clinica.Nombre);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldNombre", DBNull.Value); }
            if (historia_clinica.Fecha_nacim.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldFecha_nacim", historia_clinica.Fecha_nacim);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldFecha_nacim", DBNull.Value); }
            if (historia_clinica.Num_seguridad_social != null) {
                deleteCommand.Parameters.AddWithValue("@OldNum_seguridad_social", historia_clinica.Num_seguridad_social);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldNum_seguridad_social", DBNull.Value); }
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
 
