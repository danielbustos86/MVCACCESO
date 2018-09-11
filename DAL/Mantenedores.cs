using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BOL;
using BOL.Helpers;
using Newtonsoft.Json;

namespace DAL
{
    public class Mantenedores
    {


        public static List<Puertos> GetPuertos(string patente = null)
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
                cmd.CommandText = "SP_RECUPERA_PUERTO";               
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<Puertos> ls_puerto = new List<Puertos>();



                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ls_puerto.Add(new Puertos
                        {
                            Id = Int32.Parse(item["id_puerto"].ToString()),
                            Nombre = item["nombre"].ToString(),
                            Rut = item["Rut"].ToString()
                        });

                    }

                }
                return ls_puerto;


            }
            catch (Exception )
            {
                throw;
            }


        }
        public static List<Empresas> GetEmpresas(string rut , string nombre)
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
                cmd.CommandText = "SP_RECUPERA_EMPRESAS_ACTIVAS";

                if (rut != "N")
                {
                    cmd.Parameters.AddWithValue("@RUT", rut);
                }
                if (nombre != "N")
                {
                    cmd.Parameters.AddWithValue("@NOMBRES", nombre);
                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<Empresas> ls_empresas = new List<Empresas>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ls_empresas.Add(new Empresas
                        {
                            Id = Int32.Parse(item["id_empresa"].ToString()),
                            Rut = Int32.Parse(item["rut"].ToString()),
                            Nombre = item["nombres"].ToString(),
                            Giro = item["giro"].ToString(),
                            Info  = item["info"].ToString(),
                        });
                    }
                }
                return ls_empresas;
            }
            catch (Exception)
            {
                throw;
            }
            


        }
        public static List<TipoIngreso> GetTipoIngreso()
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
                cmd.CommandText = "SP_RECUPERA_TIPOS_DE_INGRESO";               
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<TipoIngreso> ls_tipos = new List<TipoIngreso>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ls_tipos.Add(new TipoIngreso
                        {
                            Id = Int32.Parse(item["ID_TIPO_INGRESO"].ToString()),                          
                            Nombre = item["nombre"].ToString()                          
                        });
                    }
                }
                return ls_tipos;
            }
            catch (Exception)
            {
                throw;
            }



        }
        public static List<Nacionalidad> GetNacionalidad()
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
                cmd.CommandText = "SP_RECUPERA_NACIONALIDAD";
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<Nacionalidad> ls_nacionalidad = new List<Nacionalidad>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ls_nacionalidad.Add(new Nacionalidad
                        {
                            Id = Int32.Parse(item["ID_NACIONALIDAD"].ToString()),
                            Nombre = item["NACIONALIDAD"].ToString()
                        });
                    }
                }
                return ls_nacionalidad;
            }
            catch (Exception)
            {
                throw;
            }



        }
        public static List<TipoVehiculo> GetTipoVehiculos()
        {

            string ruta = "https://sccnlp-piloto.dirtrab.cl/api/Mantenedor/getTipoVehiculo";
            string respuesta;
            using (WebClient client = new WebClient())
            {
                respuesta= client.DownloadString(ruta);
            }
            List<TipoVehiculo> ls_tipovehiculo = new List<TipoVehiculo>();
            ls_tipovehiculo   = JsonConvert.DeserializeObject<List<TipoVehiculo>>(respuesta);
            return ls_tipovehiculo;

        }
        public static string GetLocaciones_Html(int codigo , string nombre , int puerto)
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
                cmd.CommandText = "SP_BUSCA_LOCACIONES";
                string html = "";
                if (codigo== 0)
                {
                    cmd.Parameters.AddWithValue("@CODIGO", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CODIGO", codigo);
                }

                if (nombre == "")
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                }
                             
                   cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
           
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                              
                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveTablaHtml(dt);                                                         

                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }



        }        
        public static string GetNaves(int codigo, string nombre , int puerto)
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
                cmd.CommandText = "SP_BUSCA_NAVE_ACTIVAS";
                string html = "";


                if (codigo == 0)
                {
                    cmd.Parameters.AddWithValue("@CODIGO", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CODIGO", codigo);
                }


                if (nombre == "")
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveTablaHtml(dt);

                }
                                           

                return html;

            }
            catch (Exception)
            {
                throw;
            }



        }
        public static DashBoard Resumen(int? idpuerto)
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
                cmd.CommandText = "SP_DASHBOARD_SEL";

                if (idpuerto == 0)
                {
                    cmd.Parameters.AddWithValue("@ID_PUERTO", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ID_PUERTO", idpuerto);
                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                DashBoard Resumen = new DashBoard();             
                    foreach (DataRow item in dt.Rows)
                    {
                    Resumen.Ingresos = Int32.Parse(item["INGRESO"].ToString());
                    Resumen.Solicitudes = Int32.Parse(item["SOLICITUDES"].ToString());
                    Resumen.Vehiculos = Int32.Parse(item["VEHICULOS"].ToString());                                        
                    }               
                return Resumen;            }
            catch (Exception)
            {
                throw;
            }

        }                
        public static string GetSolicitudesDia(int idpuerto)
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
                cmd.CommandText = "SOLICITUD_DIA_ACTUAL";

                if (idpuerto == 0)
                {
                    cmd.Parameters.AddWithValue("@ID_PUERTO", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ID_PUERTO", idpuerto);
                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                string html = "";
                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyHtmlNormal(dt);

                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static string GetVehiculosDia()
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
                cmd.CommandText = "SP_INGRESOS_VEHICULO";
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                string html = "";

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyHtmlNormal(dt);

                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public static string GetLocacionesPuertoPrincipal_Html(int puerto)
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
                cmd.CommandText = "SP_BUSCA_PUERTO_LOCACIONES_ADMIN";
                string html = "";
                cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
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
        public static string GetLocacionesPuerto_Html(int puerto)
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
                cmd.CommandText = "SP_BUSCA_PUERTO_LOCACIONES";
                string html = "";             
                cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyHtmlNormal(dt);

                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }



        }

        public static List<Locacion> GetLocaciones(int puerto)
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
                cmd.CommandText = "SP_BUSCA_PUERTO_LOCACIONES";                
                cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<Locacion> ls_Locacion = new List<Locacion>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ls_Locacion.Add(new Locacion
                        {
                            Id = Int32.Parse(item["ID_LOCACION"].ToString()),
                            Nombre = item["NOMBRE"].ToString()
                        });
                    }
                }

                return ls_Locacion;

            }
            catch (Exception)
            {
                return null;
            }



        }

        public static string GetIngresosDia(int idpuerto)
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
                cmd.CommandText = "SP_INGRESOS_DIA_ACTUAL";
                cmd.Parameters.Clear();
                if (idpuerto == 0)
                {
                    cmd.Parameters.AddWithValue("@ID_PUERTO", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ID_PUERTO", idpuerto);
                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                string html = "";

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyHtmlNormal(dt);

                }
                return html;

            }
            catch (Exception)
            {
                throw;
            }

        }        
        public static string GetNaves_Html()
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
                cmd.CommandText = "SP_NAVES_LISTAR";
                string html = "";             
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyHtmlNormal(dt);

                }
                return html;
            }
            catch (Exception)
            {
                throw;
            }



        }
        public static string GetNavesPrincipal_Html()
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
                cmd.CommandText = "SP_NAVES_LISTAR_ESTADO";
                string html = "";
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
        public static void AgregaNave(int id, string nombre)
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
                cmd.CommandText = "SP_GUARDA_NAVE_API";
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@NOMBRE", nombre);   
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void ActualizaLocDescripcion(int puerto, int locacion, string descripcion)
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
                cmd.CommandText = "SP_ACTUALIZA_LOCACION_DESCRIPCION";
                cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
                cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion);
                cmd.Parameters.AddWithValue("@ID_LOC", locacion);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void CambiaEstadoLoc(int puerto, int locacion, int opcion)
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
                cmd.CommandText = "SP_ACTUALIZA_LOCACION_ESTADO";
                cmd.Parameters.AddWithValue("@ID_PUERTO", puerto);
                cmd.Parameters.AddWithValue("@ESTADO", opcion);
                cmd.Parameters.AddWithValue("@ID_LOC", locacion);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void CambiaEstadoNave(int nave, int opcion)
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
                cmd.CommandText = "SP_ACTUALIZA_NAVE_ESTADO";
                cmd.Parameters.AddWithValue("@ID_NAVE", nave);
                cmd.Parameters.AddWithValue("@ESTADO", opcion);          
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void CambiaDescripcionNave(int nave, string descripcion)
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
                cmd.CommandText = "SP_ACTUALIZA_NAVE_DESCRIPCION";
                cmd.Parameters.AddWithValue("@ID_NAVE", nave);
                cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string GetNavesActivas(int codigo, string nombre, int puerto)
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
                cmd.CommandText = "SP_BUSCA_NAVE_ACTIVAS";
                string html = "";


                if (codigo == 0)
                {
                    cmd.Parameters.AddWithValue("@CODIGO", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CODIGO", codigo);
                }


                if (nombre == "")
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                }

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveTablaHtml(dt);

                }


                return html;

            }
            catch (Exception)
            {
                throw;
            }



        }
        public static string GetEmpresasActivas(int rut,string nombre)
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
                cmd.CommandText = "SP_EMPRESAS_ACTIVAS";
                string html = "";


                if (rut == 0)
                {
                    cmd.Parameters.AddWithValue("@rut", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rut", rut);
                }


                if (nombre == "")
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                }
      

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveTablaHtml(dt);

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
