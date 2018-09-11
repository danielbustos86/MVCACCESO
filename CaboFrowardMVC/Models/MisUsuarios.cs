using BOL;
using BOL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CaboFrowardMVC.Models
{
    public class MisUsuarios
    {

        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Rut { get; set; }
        public string Telefono { get; set; }
        public string Activo { get; set; }
        public string Clave { get; set; }
        public string Aprueba_nave { get; set; }


        public List<MisUsuarios> Listado_usuarios()
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
                cmd.CommandText = "SP_LISTAR_USUARIOS";
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                List<MisUsuarios> usuarios = new List<MisUsuarios>();                
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        usuarios.Add(new MisUsuarios
                        {
                            Id = Int32.Parse(item["Id"].ToString()),
                            Rut = item["Rut"].ToString(),
                            Usuario = item["Nombre"].ToString(),                         
                            Activo= item["Activo"].ToString(),
                            Telefono = item["Telefono"].ToString(),
                            Clave  = item["Clave"].ToString(),
                            Aprueba_nave = item["aprueba_nave"].ToString()
                        });

                    }

                }
                
                return usuarios;
            }
            catch (Exception)
            {
                throw;
            }





        }
        

        public MisUsuarios Info_usuario(int id)
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
                cmd.CommandText = "SP_LISTAR_USUARIOS";              
                cmd.Parameters.AddWithValue("@ID", id);        
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                MisUsuarios usuario = new MisUsuarios();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        usuario.Id = Int32.Parse(item["Id"].ToString());
                        usuario.Rut = item["Rut"].ToString();
                        usuario.Usuario = item["Nombre"].ToString();
                        usuario.Activo = item["Activo"].ToString();
                        usuario.Telefono = item["Telefono"].ToString();
                        usuario.Clave = item["Clave"].ToString();
                        usuario.Aprueba_nave = item["aprueba_nave"].ToString();
                    }

                }

                return usuario;
            }
            catch (Exception)
            {
                throw;
            }





        }

        
        public void Actualiza(int id, string nombre ,string telefono , string clave, bool activo, bool aprueba_nave)
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
                    cmd.CommandText = "SP_USUARIOS_ACTUALIZA";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@NOMBRE", nombre);
                    cmd.Parameters.AddWithValue("@PASSWORD", clave);
                    cmd.Parameters.AddWithValue("@TELEFONO", telefono);
                    cmd.Parameters.AddWithValue("@ACTVO", activo);
                    cmd.Parameters.AddWithValue("@aprueba_nave", aprueba_nave);
                    cmd.ExecuteNonQuery();

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception)
                {
                    throw;
                }




            }

        public DataTable GetPersona(string rut)
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
                cmd.CommandText = "SP_VALIDA_RUT_USUARIO";
                cmd.Parameters.AddWithValue("@RUT", rut);
                da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();                     
                return dt;
            }
            catch (Exception)
            {
                throw;
            }





        }




        public void CreaUsuario(int id, string nombre, string telefono, string clave)
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
                cmd.CommandText = "SP_CREA_USUARIO";
                cmd.Parameters.AddWithValue("@ID_PERSONA", id);
                cmd.Parameters.AddWithValue("@USUARIO", nombre);
                cmd.Parameters.AddWithValue("@CLAVE", clave);
                cmd.Parameters.AddWithValue("@TELEFONO", telefono);          
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