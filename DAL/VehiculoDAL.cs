using BOL;
using BOL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VehiculoDAL
    {        

        public static void GuardaPatente(string patente , int tipo , string descripcion)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_REGISTRA_AUTO";              
                cmd.Parameters.AddWithValue("@patente", patente);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@des", descripcion);             
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }





        }


        public static void GuardaPatenteIngreso(string patente, int tipo, string descripcion, int id)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_REGISTRA_AUTO_INGRESO";
                cmd.Parameters.AddWithValue("@patente", patente);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@des", descripcion);
                cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);                
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }





        }
		public static void ModificaPatenteIngreso(string patente, int tipo, string descripcion, int id)
		{

			try
			{
				SqlCommand cmd = new SqlCommand();
				DataTable dt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				Coneccion param = Parameter.Leer_parametros();
				cmd.Connection = new SqlConnection(param.ConString);
				cmd.Connection.Open();
				cmd.Parameters.Clear();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SP_MODIFICA_AUTO_INGRESO";
				cmd.Parameters.AddWithValue("@patente", patente);
				cmd.Parameters.AddWithValue("@tipo", tipo);

				cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);
				cmd.ExecuteNonQuery();
				cmd.Connection.Close();
				cmd.Dispose();
			}
			catch (Exception)
			{
				throw;
			}





		}


		public static Vehiculo GetVehiculo(String patente)
        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BUSCA_PATENTE";
                cmd.Parameters.AddWithValue("@PATENTE", patente);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                Vehiculo vehiculo = null;
                if (dt.Rows.Count > 0)
                {
                    vehiculo = new Vehiculo();
                    vehiculo.Id = Int32.Parse(dt.Rows[0]["ID_VEHICULO"].ToString());
                    vehiculo.Patente = dt.Rows[0]["PATENTE"].ToString();
                    vehiculo.Tipo = dt.Rows[0]["TIPONOMBRE"].ToString();
                }

                return vehiculo;
            }
            catch (Exception)
            {
                throw;
            }





        }


        public static string GetValidaVehiculo(String patente)
        {
            string resp="NOK";
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_VALIDA_PATENTE";
                cmd.Parameters.AddWithValue("@PATENTE", patente);
                da.Fill(dt);
                cmd.Connection.Close();
               
                    if (dt.Rows.Count > 0)
                    {
                    resp = dt.Rows[0]["MENSAJE"].ToString();
       

                    }

            }
            catch (Exception)
            {
                throw;
            }


            return resp;


        }


    }
}
