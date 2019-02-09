using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class Visita_medicoData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[Visita_medicoSelectAll]";
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
            string selectProcedure = "[Visita_medicoSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Cod Visita") {
                selectCommand.Parameters.AddWithValue("@Cod_visita", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cod_visita", DBNull.Value); }
            if (sField == "Fecha") {
                selectCommand.Parameters.AddWithValue("@Fecha", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Fecha", DBNull.Value); }
            if (sField == "Hora") {
                selectCommand.Parameters.AddWithValue("@Hora", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Hora", DBNull.Value); }
            if (sField == "Diagnostico") {
                selectCommand.Parameters.AddWithValue("@Diagnostico", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Diagnostico", DBNull.Value); }
            if (sField == "Tratamiento") {
                selectCommand.Parameters.AddWithValue("@Tratamiento", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Tratamiento", DBNull.Value); }
            if (sField == "COD Hospital Servicio") {
                selectCommand.Parameters.AddWithValue("@CodigoRefer37", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CodigoRefer37", DBNull.Value); }
            if (sField == "Historia") {
                selectCommand.Parameters.AddWithValue("@Cedula38", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cedula38", DBNull.Value); }
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

        public static Visita_medico Select_Record(Visita_medico Visita_medicoPara)
        {
            Visita_medico Visita_medico = new Visita_medico();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[Visita_medicoSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Cod_visita", Visita_medicoPara.Cod_visita);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Visita_medico.Cod_visita = System.Convert.ToInt32(reader["Cod_visita"]);
                    Visita_medico.Fecha = reader["Fecha"] is DBNull ? null : (DateTime?)reader["Fecha"];
                    Visita_medico.Hora = reader["Hora"] is DBNull ? null : reader["Hora"].ToString();
                    Visita_medico.Diagnostico = reader["Diagnostico"] is DBNull ? null : reader["Diagnostico"].ToString();
                    Visita_medico.Tratamiento = reader["Tratamiento"] is DBNull ? null : reader["Tratamiento"].ToString();
                    Visita_medico.ID_hospitales_servicios = reader["ID_hospitales_servicios"] is DBNull ? null : (Int32?)reader["ID_hospitales_servicios"];
                    Visita_medico.id_historia = reader["id_historia"] is DBNull ? null : (Int32?)reader["id_historia"];
                }
                else
                {
                    Visita_medico = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Visita_medico;
            }
            finally
            {
                connection.Close();
            }
            return Visita_medico;
        }

        public static bool Add(Visita_medico Visita_medico)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[Visita_medicoInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (Visita_medico.Fecha.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Fecha", Visita_medico.Fecha);
            } else {
                insertCommand.Parameters.AddWithValue("@Fecha", DBNull.Value); }
            if (Visita_medico.Hora != null) {
                insertCommand.Parameters.AddWithValue("@Hora", Visita_medico.Hora);
            } else {
                insertCommand.Parameters.AddWithValue("@Hora", DBNull.Value); }
            if (Visita_medico.Diagnostico != null) {
                insertCommand.Parameters.AddWithValue("@Diagnostico", Visita_medico.Diagnostico);
            } else {
                insertCommand.Parameters.AddWithValue("@Diagnostico", DBNull.Value); }
            if (Visita_medico.Tratamiento != null) {
                insertCommand.Parameters.AddWithValue("@Tratamiento", Visita_medico.Tratamiento);
            } else {
                insertCommand.Parameters.AddWithValue("@Tratamiento", DBNull.Value); }
            if (Visita_medico.ID_hospitales_servicios.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ID_hospitales_servicios", Visita_medico.ID_hospitales_servicios);
            } else {
                insertCommand.Parameters.AddWithValue("@ID_hospitales_servicios", DBNull.Value); }
            if (Visita_medico.id_historia.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@id_historia", Visita_medico.id_historia);
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

        public static bool Update(Visita_medico oldVisita_medico, 
               Visita_medico newVisita_medico)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[Visita_medicoUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newVisita_medico.Fecha.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewFecha", newVisita_medico.Fecha);
            } else {
                updateCommand.Parameters.AddWithValue("@NewFecha", DBNull.Value); }
            if (newVisita_medico.Hora != null) {
                updateCommand.Parameters.AddWithValue("@NewHora", newVisita_medico.Hora);
            } else {
                updateCommand.Parameters.AddWithValue("@NewHora", DBNull.Value); }
            if (newVisita_medico.Diagnostico != null) {
                updateCommand.Parameters.AddWithValue("@NewDiagnostico", newVisita_medico.Diagnostico);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDiagnostico", DBNull.Value); }
            if (newVisita_medico.Tratamiento != null) {
                updateCommand.Parameters.AddWithValue("@NewTratamiento", newVisita_medico.Tratamiento);
            } else {
                updateCommand.Parameters.AddWithValue("@NewTratamiento", DBNull.Value); }
            if (newVisita_medico.ID_hospitales_servicios.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewID_hospitales_servicios", newVisita_medico.ID_hospitales_servicios);
            } else {
                updateCommand.Parameters.AddWithValue("@NewID_hospitales_servicios", DBNull.Value); }
            if (newVisita_medico.id_historia.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@Newid_historia", newVisita_medico.id_historia);
            } else {
                updateCommand.Parameters.AddWithValue("@Newid_historia", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCod_visita", oldVisita_medico.Cod_visita);
            if (oldVisita_medico.Fecha.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldFecha", oldVisita_medico.Fecha);
            } else {
                updateCommand.Parameters.AddWithValue("@OldFecha", DBNull.Value); }
            if (oldVisita_medico.Hora != null) {
                updateCommand.Parameters.AddWithValue("@OldHora", oldVisita_medico.Hora);
            } else {
                updateCommand.Parameters.AddWithValue("@OldHora", DBNull.Value); }
            if (oldVisita_medico.Diagnostico != null) {
                updateCommand.Parameters.AddWithValue("@OldDiagnostico", oldVisita_medico.Diagnostico);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDiagnostico", DBNull.Value); }
            if (oldVisita_medico.Tratamiento != null) {
                updateCommand.Parameters.AddWithValue("@OldTratamiento", oldVisita_medico.Tratamiento);
            } else {
                updateCommand.Parameters.AddWithValue("@OldTratamiento", DBNull.Value); }
            if (oldVisita_medico.ID_hospitales_servicios.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", oldVisita_medico.ID_hospitales_servicios);
            } else {
                updateCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", DBNull.Value); }
            if (oldVisita_medico.id_historia.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@Oldid_historia", oldVisita_medico.id_historia);
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

        public static bool Delete(Visita_medico Visita_medico)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[Visita_medicoDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldCod_visita", Visita_medico.Cod_visita);
            if (Visita_medico.Fecha.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldFecha", Visita_medico.Fecha);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldFecha", DBNull.Value); }
            if (Visita_medico.Hora != null) {
                deleteCommand.Parameters.AddWithValue("@OldHora", Visita_medico.Hora);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldHora", DBNull.Value); }
            if (Visita_medico.Diagnostico != null) {
                deleteCommand.Parameters.AddWithValue("@OldDiagnostico", Visita_medico.Diagnostico);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDiagnostico", DBNull.Value); }
            if (Visita_medico.Tratamiento != null) {
                deleteCommand.Parameters.AddWithValue("@OldTratamiento", Visita_medico.Tratamiento);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldTratamiento", DBNull.Value); }
            if (Visita_medico.ID_hospitales_servicios.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", Visita_medico.ID_hospitales_servicios);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", DBNull.Value); }
            if (Visita_medico.id_historia.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@Oldid_historia", Visita_medico.id_historia);
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
 
