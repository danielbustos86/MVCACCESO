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
   public class UsuarioLocacionesDAL
    {
        public static string GetUsuarioLocaciones(int id)
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
                cmd.CommandText = "USUARIO_LOCACIONES";
                string html = "";

                cmd.Parameters.AddWithValue("@ID_USUARIO", id);


                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyLocacionUSHtmlNormal(dt);
                }

                return html;

            }
            catch (Exception)
            {
                throw;
            }


        }


        public static void ModificarLocaciones(int idusuario, int locacion, string accion)
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
                cmd.CommandText = "MODIFICAR_LOCACIONES";
                cmd.Parameters.AddWithValue("@idusuario", idusuario);
                cmd.Parameters.AddWithValue("@locacion", locacion);
                cmd.Parameters.AddWithValue("@accion", accion);
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
