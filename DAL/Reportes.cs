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
    public class Reportes
    {
        public static string GetReporte1(int idpuerto, DateTime fecha_inicio, DateTime fecha_fin,int tipo_ingreso,int empresa)
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
                cmd.CommandText = "REPORTE_1_SOLICITUDES";
                string html = "";


                if (idpuerto == 0)
                {
                    cmd.Parameters.AddWithValue("@idpuerto", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@idpuerto", idpuerto);
                }


            
                    cmd.Parameters.AddWithValue("@fecha_inicio", fecha_inicio);
                

                    cmd.Parameters.AddWithValue("@fecha_fin", fecha_fin);

                if (tipo_ingreso == 0)
                {
                    cmd.Parameters.AddWithValue("@tipo_ingreso", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@tipo_ingreso", tipo_ingreso);
                }

             

                if (empresa == 0)
                {
                    cmd.Parameters.AddWithValue("@empresa", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@empresa", empresa);
                }


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

        public static string Getmovimientos(int idpuerto, DateTime fecha_inicio, DateTime fecha_fin)
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
                cmd.CommandText = "REPORTE_MOVIMIENTOS";
                string html = "";


                if (idpuerto == 0)
                {
                    cmd.Parameters.AddWithValue("@puerto", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@puerto", idpuerto);
                }



                cmd.Parameters.AddWithValue("@fecha1", fecha_inicio);


                cmd.Parameters.AddWithValue("@fecha2", fecha_fin);
                
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

        public static string Getsituacionactual(int idpuerto)
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
                cmd.CommandText = "SITUACION_ACTUAL";
                string html = "";


                
                    cmd.Parameters.AddWithValue("@puerto", idpuerto);
                

                
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
    }
}
