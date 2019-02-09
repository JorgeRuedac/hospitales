using System;
using System.Data;
using System.Data.SqlClient;
using hospital.Models;

namespace hospital.Data
{
    public class hospitalesData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitalesSelectAll]";
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
            string selectProcedure = "[hospitalesSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Cod Hospital") {
                selectCommand.Parameters.AddWithValue("@Cod_hospital", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cod_hospital", DBNull.Value); }
            if (sField == "Nombre") {
                selectCommand.Parameters.AddWithValue("@Nombre", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Nombre", DBNull.Value); }
            if (sField == "Ciudad") {
                selectCommand.Parameters.AddWithValue("@Ciudad", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Ciudad", DBNull.Value); }
            if (sField == "Tlefono") {
                selectCommand.Parameters.AddWithValue("@Tlefono", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Tlefono", DBNull.Value); }
            if (sField == "Cod Medico") {
                selectCommand.Parameters.AddWithValue("@Cedula", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Cedula", DBNull.Value); }
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

        public static hospitales Select_Record(hospitales hospitalesPara)
        {
            hospitales hospitales = new hospitales();
            SqlConnection connection = hospitalData.GetConnection();
            string selectProcedure = "[hospitalesSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Cod_hospital", hospitalesPara.Cod_hospital);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    hospitales.Cod_hospital = System.Convert.ToInt32(reader["Cod_hospital"]);
                    hospitales.Nombre = reader["Nombre"] is DBNull ? null : reader["Nombre"].ToString();
                    hospitales.Ciudad = reader["Ciudad"] is DBNull ? null : reader["Ciudad"].ToString();
                    hospitales.Tlefono = reader["Tlefono"] is DBNull ? null : reader["Tlefono"].ToString();
                    hospitales.Cod_medico = reader["Cod_medico"] is DBNull ? null : (Int32?)reader["Cod_medico"];
                }
                else
                {
                    hospitales = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return hospitales;
            }
            finally
            {
                connection.Close();
            }
            return hospitales;
        }

        public static bool Add(hospitales hospitales)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string insertProcedure = "[hospitalesInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (hospitales.Nombre != null) {
                insertCommand.Parameters.AddWithValue("@Nombre", hospitales.Nombre);
            } else {
                insertCommand.Parameters.AddWithValue("@Nombre", DBNull.Value); }
            if (hospitales.Ciudad != null) {
                insertCommand.Parameters.AddWithValue("@Ciudad", hospitales.Ciudad);
            } else {
                insertCommand.Parameters.AddWithValue("@Ciudad", DBNull.Value); }
            if (hospitales.Tlefono != null) {
                insertCommand.Parameters.AddWithValue("@Tlefono", hospitales.Tlefono);
            } else {
                insertCommand.Parameters.AddWithValue("@Tlefono", DBNull.Value); }
            if (hospitales.Cod_medico.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@Cod_medico", hospitales.Cod_medico);
            } else {
                insertCommand.Parameters.AddWithValue("@Cod_medico", DBNull.Value); }
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

        public static bool Update(hospitales oldhospitales, 
               hospitales newhospitales)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string updateProcedure = "[hospitalesUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            if (newhospitales.Nombre != null) {
                updateCommand.Parameters.AddWithValue("@NewNombre", newhospitales.Nombre);
            } else {
                updateCommand.Parameters.AddWithValue("@NewNombre", DBNull.Value); }
            if (newhospitales.Ciudad != null) {
                updateCommand.Parameters.AddWithValue("@NewCiudad", newhospitales.Ciudad);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCiudad", DBNull.Value); }
            if (newhospitales.Tlefono != null) {
                updateCommand.Parameters.AddWithValue("@NewTlefono", newhospitales.Tlefono);
            } else {
                updateCommand.Parameters.AddWithValue("@NewTlefono", DBNull.Value); }
            if (newhospitales.Cod_medico.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewCod_medico", newhospitales.Cod_medico);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCod_medico", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCod_hospital", oldhospitales.Cod_hospital);
            if (oldhospitales.Nombre != null) {
                updateCommand.Parameters.AddWithValue("@OldNombre", oldhospitales.Nombre);
            } else {
                updateCommand.Parameters.AddWithValue("@OldNombre", DBNull.Value); }
            if (oldhospitales.Ciudad != null) {
                updateCommand.Parameters.AddWithValue("@OldCiudad", oldhospitales.Ciudad);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCiudad", DBNull.Value); }
            if (oldhospitales.Tlefono != null) {
                updateCommand.Parameters.AddWithValue("@OldTlefono", oldhospitales.Tlefono);
            } else {
                updateCommand.Parameters.AddWithValue("@OldTlefono", DBNull.Value); }
            if (oldhospitales.Cod_medico.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldCod_medico", oldhospitales.Cod_medico);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCod_medico", DBNull.Value); }
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

        public static bool Delete(hospitales hospitales)
        {
            SqlConnection connection = hospitalData.GetConnection();
            string deleteProcedure = "[hospitalesDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldCod_hospital", hospitales.Cod_hospital);
            if (hospitales.Nombre != null) {
                deleteCommand.Parameters.AddWithValue("@OldNombre", hospitales.Nombre);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldNombre", DBNull.Value); }
            if (hospitales.Ciudad != null) {
                deleteCommand.Parameters.AddWithValue("@OldCiudad", hospitales.Ciudad);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCiudad", DBNull.Value); }
            if (hospitales.Tlefono != null) {
                deleteCommand.Parameters.AddWithValue("@OldTlefono", hospitales.Tlefono);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldTlefono", DBNull.Value); }
            if (hospitales.Cod_medico.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldCod_medico", hospitales.Cod_medico);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCod_medico", DBNull.Value); }
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
 
