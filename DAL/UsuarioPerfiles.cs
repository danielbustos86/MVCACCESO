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
    public class UsuarioPerfiles
    {
        public static string GetUsuarioPerfil(int id)
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
                cmd.CommandText = "PERFIL_USUARIO";
                string html = "";

                    cmd.Parameters.AddWithValue("@ID_USUARIO", id);
             

                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();

                if (dt.Rows.Count > 0)
                {
                    html = Util.DevuelveBodyPerfilesUSHtmlNormal(dt);
                }

                return html;

            }
            catch (Exception)
            {
                throw;
            }


        }

        public static List<PerfilesPersona> GetUsuarioPerfil1(int id)

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
                cmd.CommandText = "PERFIL_USUARIO";
                cmd.Parameters.AddWithValue("@ID_USUARIO", id);

                SqlDataReader rdr = cmd.ExecuteReader();
                List<PerfilesPersona> ls_perfilper = new List<PerfilesPersona>();
                while (rdr.Read())
                {
                    ls_perfilper.Add(new PerfilesPersona
                    {
                        //Id = Int32.Parse(rdr["ID_SOLICITUD"].ToString()),
                        Asignado = rdr["ASIGNADO"].ToString(),
                        idperfil = Int32.Parse(rdr["ID_PERFIL"].ToString()),
                        Nombre = rdr["NOMBRE"].ToString()


                    });
                }

                cmd.Connection.Close();
                cmd.Dispose();
                return ls_perfilper;
            }
            catch (Exception e)
            {

                string msj = "";
                msj = e.Message.ToString();
                List<PerfilesPersona> ls_perfilper = new List<PerfilesPersona>();
                return ls_perfilper;
            }
        }



        public static void ModificarPerfiles(int idusuario, int perfil, string accion)
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
                cmd.CommandText = "MODIFICAR_PERFILES";
                cmd.Parameters.AddWithValue("@idusuario", idusuario);
                cmd.Parameters.AddWithValue("@perfil", perfil);
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
