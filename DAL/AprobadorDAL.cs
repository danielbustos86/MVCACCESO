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
   public class AprobadorDAL
    {

        public static void ApruebaSolicitud(int id , string idpersona, int id_usuario)
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
                cmd.CommandText = "SP_PERSONAS_APROBADAS";
                cmd.Parameters.AddWithValue("@ID_SOLICITUD", id);
                cmd.Parameters.AddWithValue("@XML", idpersona);
                cmd.Parameters.AddWithValue("@ID_USUARIO", id_usuario);
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
