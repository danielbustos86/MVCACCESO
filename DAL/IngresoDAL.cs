using BOL;
using BOL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class IngresoDAL
    {


        public static Ingreso BuscarIngreso(int rut,string pasaporte)

        {

            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DATOS_MOVIMIENTO";
                if (rut==0)
                {
                    cmd.Parameters.AddWithValue("@RUT",System.DBNull.Value);
                }else
                {
                    cmd.Parameters.AddWithValue("@RUT", rut);
                
                }
                if (pasaporte == "")
                {
                    cmd.Parameters.AddWithValue("@PASAPORTE", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PASAPORTE", pasaporte);

                }

                SqlDataReader rdr = cmd.ExecuteReader();
                Ingreso aux_ingreso = new Ingreso();

                while (rdr.Read())
                {
                    aux_ingreso.Nombre = rdr["NOMBRE"].ToString();
                    aux_ingreso.Idsolicitud = Int32.Parse(rdr["ID_SOLICITUD"].ToString());
                    aux_ingreso.Estado = rdr["ESTADO"].ToString();
                    aux_ingreso.Autorizacion = rdr["AUTORIZACION"].ToString();
                    aux_ingreso.Puerto = rdr["PUERTO"].ToString();
                    aux_ingreso.Usuario = rdr["USUARIO"].ToString();
                    aux_ingreso.Locacion = rdr["LOCACION"].ToString();
                    aux_ingreso.Nave = rdr["NAVE"].ToString();
                    aux_ingreso.Inicio = rdr["FECHA_DESDE"].ToString();
                    aux_ingreso.Fin = rdr["FECHA_HASTA"].ToString();
                    aux_ingreso.Empresa = rdr["EMPRESA"].ToString();
                    aux_ingreso.Idaprobado = Int32.Parse(rdr["ID_PERSONA_APROBADA"].ToString());
                    aux_ingreso.Pasaporte= rdr["PASAPORTE"].ToString();
                }

                cmd.Connection.Close();
                cmd.Dispose();
                return aux_ingreso;
            }
            catch (Exception)
            {
                throw;
            }

        }



        public static List<Vehiculo> ListarPatente(int id)
        {            
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LISTA_VEHICULOS_SOL";
                cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);                
                SqlDataReader rdr = cmd.ExecuteReader();
                List<Vehiculo> ls_patente = new List<Vehiculo>();
                while (rdr.Read())
                {
                    ls_patente.Add(new Vehiculo
                    {
                        Id = Int32.Parse(rdr["ID_VEHICULO"].ToString()),
                        Patente = rdr["PATENTE"].ToString(),
                    });                }

                cmd.Connection.Close();
                cmd.Dispose();
                return ls_patente;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Vehiculo ValidaVehiculo(int idpersonaaprpnada) {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_ULTIMO_VEHICULOS";
                cmd.Parameters.AddWithValue("@ID_PERSONA_APROBADA", idpersonaaprpnada);
                SqlDataReader rdr = cmd.ExecuteReader();
               Vehiculo ls_patente = new Vehiculo();
                while (rdr.Read())
                {


                    ls_patente.Id = Int32.Parse(rdr["ID_VEHICULO"].ToString());
                    ls_patente.Patente = rdr["PATENTE"].ToString();
                   
                }

                cmd.Connection.Close();
                cmd.Dispose();
                return ls_patente;
            }
            catch (Exception)
            {
                throw;
            }


        }

        public static string DevuelveAprobadores(int id, int rut)
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
                cmd.CommandText = "SP_APROBADORES_INGRESO";
                string html = "";                
                cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);
                cmd.Parameters.AddWithValue("@RUT", rut);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();                
                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyHtmlNormalEditable(dt);
                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        

        public static void RegistraAcceso(int tipo, string ip , int persona_persona_aprobada, string patente )
        {
            try
            {

				IPHostEntry host;
				string localIP = "";
				host = Dns.GetHostEntry(Dns.GetHostName());
				foreach (IPAddress i in host.AddressList)
				{
					if (i.AddressFamily.ToString() == "InterNetwork")
					{
						localIP = i.ToString();
					}
				}

				SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "REGISTRA_ACCESO";
                cmd.Parameters.AddWithValue("@MOV_ID_MOVIMIENTO", tipo);
                cmd.Parameters.AddWithValue("@IP", localIP);
                cmd.Parameters.AddWithValue("@ID_VEHICULO", patente);
                cmd.Parameters.AddWithValue("@ID_PERSONA_APROBADA", persona_persona_aprobada);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string UltimasSolicitudes(int rut , ref string nombre,string pasaporte)
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
                cmd.CommandText = "INFO_ULTIMAS_SOL";
                string html = "";
                if (rut == 0)
                {
                    cmd.Parameters.AddWithValue("@RUT", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RUT", rut);

                }
                if (pasaporte == "")
                {
                    cmd.Parameters.AddWithValue("@PASAPORTE", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PASAPORTE", pasaporte);

                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {
                    nombre = dt.Rows[0]["NOMBRE"].ToString();                        
                    html = Util.DevuelveBodyHtmlNormalHidden(dt,1);
                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
