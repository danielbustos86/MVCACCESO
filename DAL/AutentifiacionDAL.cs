using BOL.Helpers;
using BOL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;


namespace DAL
{
   public static class AutentifiacionDAL
    {

        public static Login ValidarAcceso(string usuario , string clave )
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
                cmd.CommandText = "SP_VALIDA_ACCESO";
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.Parameters.AddWithValue("@CLAVE", clave);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                               
                Login Datos_Login = new Login();
                Datos_Login.Id = Int32.Parse(dt.Rows[0]["id_usuario"].ToString());
                Datos_Login.Nombre = dt.Rows[0]["Nombre"].ToString();
                Datos_Login.Perfil = Int32.Parse(dt.Rows[0]["id_perfil"].ToString());
                Datos_Login.Inactivo = dt.Rows[0]["CLAVE_INICIAL"].ToString();

                return Datos_Login;


            }
            catch (Exception)
            {
                throw;
            }            

                          

        }

        public static void Activa_Inicio(string usuario, string clave, string nueva_clave)
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
                cmd.CommandText = "SP_ACTIVA_INICIO_USUARIO";
                cmd.Parameters.AddWithValue("@USUARIO", usuario);
                cmd.Parameters.AddWithValue("@CONTRASEÑA_ACTUAL", clave);
                cmd.Parameters.AddWithValue("@NUEVA_CONTRASEÑA", nueva_clave);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
                throw;
            }

            


        }

        
        public static List<Menu> Obtener_menus(int usuario)
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
                cmd.CommandText = "SP_MENUS_POR_PERFIL_USUARIO";
                cmd.Parameters.AddWithValue("@ID", usuario);               
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<Menu> menus = new List<Menu>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        menus.Add(new Menu  
                        {
                            Id = Int32.Parse(item["ID_MENU"].ToString()),
                            Descripcion = item["MENU"].ToString(),
                        });

                    }

                }
                return menus;


            }
            catch (Exception ex)
            {
                throw;
            }





        }
    }
}
