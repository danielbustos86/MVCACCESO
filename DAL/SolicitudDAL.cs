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
    public class SolicitudDAL
    {
                        
        public static DataSet VerDetalle(int id)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet Retorno = new DataSet();
            Coneccion param = Parameter.Leer_parametros();
            cmd.Connection = new SqlConnection(param.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_VER_SOLICITUD";
            cmd.Parameters.AddWithValue("@solicitud", id);         
            da.SelectCommand = cmd;
            da.Fill(Retorno);
            return Retorno;
        }

        public static DataSet VerDetallePermiso(int id, int usuario)
        {

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet Retorno = new DataSet();
            Coneccion param = Parameter.Leer_parametros();
            cmd.Connection = new SqlConnection(param.ConString);
            cmd.Connection.Open();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_VER_SOLICITUD_PERMISO";
            cmd.Parameters.AddWithValue("@solicitud", id);
            cmd.Parameters.AddWithValue("@id_usuario", usuario);
            da.SelectCommand = cmd;
            da.Fill(Retorno);
            return Retorno;
        }

        public static List<Solicitud> SolicitudListado(string id,string op)

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
                cmd.CommandText = "SP_LISTA_SOLICITUDES";

                if (id != null)
                {
                    cmd.Parameters.AddWithValue("@ID_USUARIO", id);
                }

                if (op != null) {
                    cmd.Parameters.AddWithValue("@OPCION", op);

                }

                SqlDataReader rdr = cmd.ExecuteReader();
                List<Solicitud> ls_solicitud = new List<Solicitud>();
                while (rdr.Read())
                {
                    ls_solicitud.Add(new Solicitud
                    {
                        Id = Int32.Parse(rdr["ID_SOLICITUD"].ToString()),
                        FechaCreacion = rdr["FECHA_CREACION"].ToString(),
                        Empresa = rdr["NOMBRES"].ToString(),
                        Desde = rdr["FECHA_DESDE"].ToString(),
                        Hasta = rdr["FECHA_HASTA"].ToString(),
                        Solicitante = rdr["NOMBRE"].ToString(),
                        Canttotalpersona = Int32.Parse(rdr["CANT_PERSONAS"].ToString()),
                        Cantaprobados = Int32.Parse(rdr["CANT_APROBADAS"].ToString())

                    });
                }

                cmd.Connection.Close();
                cmd.Dispose();            
                return ls_solicitud;
            }
            catch (Exception e)
            {

                string msj = "";
                msj = e.Message.ToString();
                List<Solicitud> ls_solicitud1 = new List<Solicitud>();
                return ls_solicitud1;
            }                        
        }
        
        public static string ValidaIngresoSolicitud(string puerto, DateTime fechainicio, DateTime fechafin, string tingreso, string empresa, string perxml, string vehiculoxml)
        {
            
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_VALIDAR_SOLICITUD";
                cmd.Parameters.AddWithValue("@puerto", puerto);
                cmd.Parameters.AddWithValue("@fechainicio", fechainicio);
                cmd.Parameters.AddWithValue("@fechafin", fechafin);
                cmd.Parameters.AddWithValue("@tingreso", tingreso);
                cmd.Parameters.AddWithValue("@empresa", empresa);
                cmd.Parameters.AddWithValue("@perxml", perxml);
                cmd.Parameters.AddWithValue("@vehiculoxml", vehiculoxml);
                string html = "";                                
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveTablaMod(dt , "Quitar");
                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }                        
        }


        public static void GuardaSolicitud(string puerto, DateTime fechainicio, DateTime fechafin, string tingreso, string empresa, string perxml, string vehiculoxml , int usuario)
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
                cmd.CommandText = "SP_GUARDA_SOLICITUD";
                cmd.Parameters.AddWithValue("@puerto", puerto);
                cmd.Parameters.AddWithValue("@fechainicio", fechainicio);
                cmd.Parameters.AddWithValue("@fechafin", fechafin);
                cmd.Parameters.AddWithValue("@tingreso", tingreso);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@empresa", empresa);
                cmd.Parameters.AddWithValue("@perxml", perxml);
                cmd.Parameters.AddWithValue("@vehiculoxml", vehiculoxml);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void Eliminar(int id)
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
                cmd.CommandText = "SP_ELIMINA_SOLICITUD";
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

        public static void EliminarPersonaMiSol(int id , int idpersona)
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
                cmd.CommandText = "SP_MI_SOLICITUD_PERSONA_BORRAR";
                cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);
                cmd.Parameters.AddWithValue("@IDPERSONA", idpersona);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void LocacionGuarda(int puerto, string descripcion, int locacion, string informada)
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
                cmd.CommandText = "SP_LOCACION_PUERTO_INS";
                cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion);
                cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
                cmd.Parameters.AddWithValue("@ID_LOC", locacion);
                cmd.Parameters.AddWithValue("@INFORMADA", informada);            
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }




        public static void EliminarPatenteMiSol(int id, string patente)
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
                cmd.CommandText = "SP_MI_SOLICITUD_PATENTE_BORRAR";
                cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);
                cmd.Parameters.AddWithValue("@PATENTE", patente);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
