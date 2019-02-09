using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class camasData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[camasSelectAll]";
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
            string selectProcedure = "[camasSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Id Cama") {
                selectCommand.Parameters.AddWithValue("@id_cama", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@id_cama", DBNull.Value); }
            if (sField == "Num Cama") {
                selectCommand.Parameters.AddWithValue("@Num_cama", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Num_cama", DBNull.Value); }
            if (sField == "Estado") {
                selectCommand.Parameters.AddWithValue("@Estado", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Estado", DBNull.Value); }
            if (sField == "COD Hospital Servicio") {
                selectCommand.Parameters.AddWithValue("@CodigoRefer", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CodigoRefer", DBNull.Value); }
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

        public static camas Select_Record(camas camasPara)
        {
            camas camas = new camas();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[camasSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@id_cama", camasPara.id_cama);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    camas.id_cama = System.Convert.ToInt32(reader["id_cama"]);
                    camas.Num_cama = reader["Num_cama"] is DBNull ? null : (Int32?)reader["Num_cama"];
                    camas.Estado = reader["Estado"] is DBNull ? null : reader["Estado"].ToString();
                    camas.ID_hospitales_servicios = reader["ID_hospitales_servicios"] is DBNull ? null : (Int32?)reader["ID_hospitales_servicios"];
                }
                else
                {
                    camas = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return camas;
            }
            finally
            {
                connection.Close();
            }
            return camas;
        }

        public static bool Add(camas camas)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[camasInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (camas.Num_cama.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Num_cama", camas.Num_cama);
            } else {
                insertCommand.Parameters.AddWithValue("@Num_cama", DBNull.Value); }
            if (camas.Estado != null) {
                insertCommand.Parameters.AddWithValue("@Estado", camas.Estado);
            } else {
                insertCommand.Parameters.AddWithValue("@Estado", DBNull.Value); }
            if (camas.ID_hospitales_servicios.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ID_hospitales_servicios", camas.ID_hospitales_servicios);
            } else {
                insertCommand.Parameters.AddWithValue("@ID_hospitales_servicios", DBNull.Value); }
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

        public static bool Update(camas oldcamas, 
               camas newcamas)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[camasUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newcamas.Num_cama.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewNum_cama", newcamas.Num_cama);
            } else {
                updateCommand.Parameters.AddWithValue("@NewNum_cama", DBNull.Value); }
            if (newcamas.Estado != null) {
                updateCommand.Parameters.AddWithValue("@NewEstado", newcamas.Estado);
            } else {
                updateCommand.Parameters.AddWithValue("@NewEstado", DBNull.Value); }
            if (newcamas.ID_hospitales_servicios.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewID_hospitales_servicios", newcamas.ID_hospitales_servicios);
            } else {
                updateCommand.Parameters.AddWithValue("@NewID_hospitales_servicios", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@Oldid_cama", oldcamas.id_cama);
            if (oldcamas.Num_cama.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldNum_cama", oldcamas.Num_cama);
            } else {
                updateCommand.Parameters.AddWithValue("@OldNum_cama", DBNull.Value); }
            if (oldcamas.Estado != null) {
                updateCommand.Parameters.AddWithValue("@OldEstado", oldcamas.Estado);
            } else {
                updateCommand.Parameters.AddWithValue("@OldEstado", DBNull.Value); }
            if (oldcamas.ID_hospitales_servicios.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", oldcamas.ID_hospitales_servicios);
            } else {
                updateCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", DBNull.Value); }
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

        public static bool Delete(camas camas)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[camasDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@Oldid_cama", camas.id_cama);
            if (camas.Num_cama.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldNum_cama", camas.Num_cama);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldNum_cama", DBNull.Value); }
            if (camas.Estado != null) {
                deleteCommand.Parameters.AddWithValue("@OldEstado", camas.Estado);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldEstado", DBNull.Value); }
            if (camas.ID_hospitales_servicios.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", camas.ID_hospitales_servicios);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", DBNull.Value); }
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
 
