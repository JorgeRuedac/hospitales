using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class ingresosData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[ingresosSelectAll]";
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
            string selectProcedure = "[ingresosSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Num Habitacion") {
                selectCommand.Parameters.AddWithValue("@Num_habitacion", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Num_habitacion", DBNull.Value); }
            if (sField == "Comentario") {
                selectCommand.Parameters.AddWithValue("@Comentario", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Comentario", DBNull.Value); }
            if (sField == "Fecha Ingreso") {
                selectCommand.Parameters.AddWithValue("@Fecha_ingreso", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha_ingreso", DBNull.Value); }
            if (sField == "Fecha Salida") {
                selectCommand.Parameters.AddWithValue("@Fecha_salida", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha_salida", DBNull.Value); }
            if (sField == "Num Cama") {
                selectCommand.Parameters.AddWithValue("@Num_cama24", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Num_cama24", DBNull.Value); }
            if (sField == "Historia") {
                selectCommand.Parameters.AddWithValue("@Cedula25", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cedula25", DBNull.Value); }
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

        public static ingresos Select_Record(ingresos ingresosPara)
        {
            ingresos ingresos = new ingresos();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[ingresosSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Num_habitacion", ingresosPara.Num_habitacion);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ingresos.Num_habitacion = System.Convert.ToInt32(reader["Num_habitacion"]);
                    ingresos.Comentario = reader["Comentario"] is DBNull ? null : reader["Comentario"].ToString();
                    ingresos.Fecha_ingreso = reader["Fecha_ingreso"] is DBNull ? null : (DateTime?)reader["Fecha_ingreso"];
                    ingresos.Fecha_salida = reader["Fecha_salida"] is DBNull ? null : (DateTime?)reader["Fecha_salida"];
                    ingresos.id_cama = reader["id_cama"] is DBNull ? null : (Int32?)reader["id_cama"];
                    ingresos.id_historia = reader["id_historia"] is DBNull ? null : (Int32?)reader["id_historia"];
                }
                else
                {
                    ingresos = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ingresos;
            }
            finally
            {
                connection.Close();
            }
            return ingresos;
        }

        public static bool Add(ingresos ingresos)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[ingresosInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (ingresos.Comentario != null) {
                insertCommand.Parameters.AddWithValue("@Comentario", ingresos.Comentario);
            } else {
                insertCommand.Parameters.AddWithValue("@Comentario", DBNull.Value); }
            if (ingresos.Fecha_ingreso.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Fecha_ingreso", ingresos.Fecha_ingreso);
            } else {
                insertCommand.Parameters.AddWithValue("@Fecha_ingreso", DBNull.Value); }
            if (ingresos.Fecha_salida.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Fecha_salida", ingresos.Fecha_salida);
            } else {
                insertCommand.Parameters.AddWithValue("@Fecha_salida", DBNull.Value); }
            if (ingresos.id_cama.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@id_cama", ingresos.id_cama);
            } else {
                insertCommand.Parameters.AddWithValue("@id_cama", DBNull.Value); }
            if (ingresos.id_historia.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@id_historia", ingresos.id_historia);
            } else {
                insertCommand.Parameters.AddWithValue("@id_historia", DBNull.Value); }
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

        public static bool Update(ingresos oldingresos, 
               ingresos newingresos)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[ingresosUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newingresos.Comentario != null) {
                updateCommand.Parameters.AddWithValue("@NewComentario", newingresos.Comentario);
            } else {
                updateCommand.Parameters.AddWithValue("@NewComentario", DBNull.Value); }
            if (newingresos.Fecha_ingreso.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewFecha_ingreso", newingresos.Fecha_ingreso);
            } else {
                updateCommand.Parameters.AddWithValue("@NewFecha_ingreso", DBNull.Value); }
            if (newingresos.Fecha_salida.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewFecha_salida", newingresos.Fecha_salida);
            } else {
                updateCommand.Parameters.AddWithValue("@NewFecha_salida", DBNull.Value); }
            if (newingresos.id_cama.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@Newid_cama", newingresos.id_cama);
            } else {
                updateCommand.Parameters.AddWithValue("@Newid_cama", DBNull.Value); }
            if (newingresos.id_historia.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@Newid_historia", newingresos.id_historia);
            } else {
                updateCommand.Parameters.AddWithValue("@Newid_historia", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldNum_habitacion", oldingresos.Num_habitacion);
            if (oldingresos.Comentario != null) {
                updateCommand.Parameters.AddWithValue("@OldComentario", oldingresos.Comentario);
            } else {
                updateCommand.Parameters.AddWithValue("@OldComentario", DBNull.Value); }
            if (oldingresos.Fecha_ingreso.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldFecha_ingreso", oldingresos.Fecha_ingreso);
            } else {
                updateCommand.Parameters.AddWithValue("@OldFecha_ingreso", DBNull.Value); }
            if (oldingresos.Fecha_salida.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldFecha_salida", oldingresos.Fecha_salida);
            } else {
                updateCommand.Parameters.AddWithValue("@OldFecha_salida", DBNull.Value); }
            if (oldingresos.id_cama.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@Oldid_cama", oldingresos.id_cama);
            } else {
                updateCommand.Parameters.AddWithValue("@Oldid_cama", DBNull.Value); }
            if (oldingresos.id_historia.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@Oldid_historia", oldingresos.id_historia);
            } else {
                updateCommand.Parameters.AddWithValue("@Oldid_historia", DBNull.Value); }
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

        public static bool Delete(ingresos ingresos)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[ingresosDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldNum_habitacion", ingresos.Num_habitacion);
            if (ingresos.Comentario != null) {
                deleteCommand.Parameters.AddWithValue("@OldComentario", ingresos.Comentario);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldComentario", DBNull.Value); }
            if (ingresos.Fecha_ingreso.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldFecha_ingreso", ingresos.Fecha_ingreso);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldFecha_ingreso", DBNull.Value); }
            if (ingresos.Fecha_salida.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldFecha_salida", ingresos.Fecha_salida);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldFecha_salida", DBNull.Value); }
            if (ingresos.id_cama.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@Oldid_cama", ingresos.id_cama);
            } else {
                deleteCommand.Parameters.AddWithValue("@Oldid_cama", DBNull.Value); }
            if (ingresos.id_historia.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@Oldid_historia", ingresos.id_historia);
            } else {
                deleteCommand.Parameters.AddWithValue("@Oldid_historia", DBNull.Value); }
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
 
