using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class hospitales_serviciosData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitales_serviciosSelectAll]";
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
            string selectProcedure = "[hospitales_serviciosSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "I D Hospitales Servicios") {
                selectCommand.Parameters.AddWithValue("@ID_hospitales_servicios", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ID_hospitales_servicios", DBNull.Value); }
            if (sField == "Cod Hospital") {
                selectCommand.Parameters.AddWithValue("@Nombre17", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre17", DBNull.Value); }
            if (sField == "Id Servicio") {
                selectCommand.Parameters.AddWithValue("@Nombre_servicio18", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre_servicio18", DBNull.Value); }
            if (sField == "Codigo Refer") {
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

        public static hospitales_servicios Select_Record(hospitales_servicios hospitales_serviciosPara)
        {
            hospitales_servicios hospitales_servicios = new hospitales_servicios();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitales_serviciosSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ID_hospitales_servicios", hospitales_serviciosPara.ID_hospitales_servicios);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    hospitales_servicios.ID_hospitales_servicios = System.Convert.ToInt32(reader["ID_hospitales_servicios"]);
                    hospitales_servicios.Cod_hospital = reader["Cod_hospital"] is DBNull ? null : (Int32?)reader["Cod_hospital"];
                    hospitales_servicios.Id_servicio = reader["Id_servicio"] is DBNull ? null : (Int32?)reader["Id_servicio"];
                    hospitales_servicios.CodigoRefer = reader["CodigoRefer"] is DBNull ? null : reader["CodigoRefer"].ToString();
                }
                else
                {
                    hospitales_servicios = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return hospitales_servicios;
            }
            finally
            {
                connection.Close();
            }
            return hospitales_servicios;
        }

        public static bool Add(hospitales_servicios hospitales_servicios)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[hospitales_serviciosInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (hospitales_servicios.Cod_hospital.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Cod_hospital", hospitales_servicios.Cod_hospital);
            } else {
                insertCommand.Parameters.AddWithValue("@Cod_hospital", DBNull.Value); }
            if (hospitales_servicios.Id_servicio.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Id_servicio", hospitales_servicios.Id_servicio);
            } else {
                insertCommand.Parameters.AddWithValue("@Id_servicio", DBNull.Value); }
            if (hospitales_servicios.CodigoRefer != null) {
                insertCommand.Parameters.AddWithValue("@CodigoRefer", hospitales_servicios.CodigoRefer);
            } else {
                insertCommand.Parameters.AddWithValue("@CodigoRefer", DBNull.Value); }
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

        public static bool Update(hospitales_servicios oldhospitales_servicios, 
               hospitales_servicios newhospitales_servicios)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[hospitales_serviciosUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newhospitales_servicios.Cod_hospital.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewCod_hospital", newhospitales_servicios.Cod_hospital);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCod_hospital", DBNull.Value); }
            if (newhospitales_servicios.Id_servicio.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewId_servicio", newhospitales_servicios.Id_servicio);
            } else {
                updateCommand.Parameters.AddWithValue("@NewId_servicio", DBNull.Value); }
            if (newhospitales_servicios.CodigoRefer != null) {
                updateCommand.Parameters.AddWithValue("@NewCodigoRefer", newhospitales_servicios.CodigoRefer);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCodigoRefer", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", oldhospitales_servicios.ID_hospitales_servicios);
            if (oldhospitales_servicios.Cod_hospital.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldCod_hospital", oldhospitales_servicios.Cod_hospital);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCod_hospital", DBNull.Value); }
            if (oldhospitales_servicios.Id_servicio.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldId_servicio", oldhospitales_servicios.Id_servicio);
            } else {
                updateCommand.Parameters.AddWithValue("@OldId_servicio", DBNull.Value); }
            if (oldhospitales_servicios.CodigoRefer != null) {
                updateCommand.Parameters.AddWithValue("@OldCodigoRefer", oldhospitales_servicios.CodigoRefer);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCodigoRefer", DBNull.Value); }
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

        public static bool Delete(hospitales_servicios hospitales_servicios)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[hospitales_serviciosDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldID_hospitales_servicios", hospitales_servicios.ID_hospitales_servicios);
            if (hospitales_servicios.Cod_hospital.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldCod_hospital", hospitales_servicios.Cod_hospital);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCod_hospital", DBNull.Value); }
            if (hospitales_servicios.Id_servicio.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldId_servicio", hospitales_servicios.Id_servicio);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldId_servicio", DBNull.Value); }
            if (hospitales_servicios.CodigoRefer != null) {
                deleteCommand.Parameters.AddWithValue("@OldCodigoRefer", hospitales_servicios.CodigoRefer);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCodigoRefer", DBNull.Value); }
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
 
