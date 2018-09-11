using BOL;
using BOL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CaboFrowardMVC.Models
{
    public class Accesos_Menu
    {
        public int Id { get; set; }
        public string Nombre { get; set; }



        public  string Obtener_menus(int usuario)
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
                List<Accesos_Menu> menus = new List<Accesos_Menu>();
                StringBuilder menu = new StringBuilder();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        menu.Append(item["MENU"].ToString() + "/");                     

                    }

                }
                return menu.ToString() ;


            }
            catch (Exception )
            {
                throw;
            }





        }


    }
}
