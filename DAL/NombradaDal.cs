using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using BOL.Helpers;
namespace DAL
{
   public class NombradaDal
    {
        public static string CargarNombrada(List<Nombrada> list)
        {
            string resp = "";

            foreach(Nombrada nom in list)
            {
                    foreach(PerNombrada per in nom.PersonasNom)
                    {
                        try
                        {

                            string fechaaux = "";
                            fechaaux = nom.fechaInicioNombrada.Substring(0, 10);
                            fechaaux = fechaaux.Replace("-", "");

                            SqlCommand cmd = new SqlCommand();
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            Coneccion param = Parameter.Leer_parametros();
                            cmd.Connection = new SqlConnection(param.ConString);
                            cmd.Connection.Open();
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "SP_CARGAR_NOMBRADA";
                            cmd.Parameters.AddWithValue("@fecha", fechaaux);
                            cmd.Parameters.AddWithValue("@turno", nom.idTurno);
                            cmd.Parameters.AddWithValue("@rut", per.Rut);
                            cmd.Parameters.AddWithValue("@dv", per.DV);
                            cmd.Parameters.AddWithValue("@idLocacion", nom.idLocacion);
                        if (nom.idNave == "")
                        {

                            cmd.Parameters.AddWithValue("@IdNave", System.DBNull.Value);
                            cmd.Parameters.AddWithValue("@Navedesc", System.DBNull.Value);

                        }
                        else
                        {

                            cmd.Parameters.AddWithValue("@IdNave", nom.idNave);
                            cmd.Parameters.AddWithValue("@Navedesc", nom.NaveDescrip );
                        }
                  
                        cmd.Parameters.AddWithValue("@nombre", per.Nombre);
                        cmd.Parameters.AddWithValue("@apellidoPaterno", per.ApellidoPat);
                        cmd.Parameters.AddWithValue("@apellidoMaterno", per.ApellidoPat);
                        cmd.Parameters.AddWithValue("@idPuerto", nom.idPuerto);
                        cmd.Parameters.AddWithValue("@rutempresa", nom.rutEmpresa);
                     //   cmd.ExecuteNonQuery();


                        da.Fill(dt);

                    

                        cmd.Connection.Close();
                            cmd.Dispose();

                        DataRow row = dt.Rows[0];

                        resp = row["respuesta"].ToString();





                
                    }
                    catch (SqlException exp)
                        {
                        resp = exp.Message.ToString();
                        }



                
                }


                }

            return resp;

        }
        public static string GetNombradas(string fecha, string turno)
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
                cmd.CommandText = "SP_LISTA_NOMBRADA";
                string html = "";
                cmd.Parameters.AddWithValue("@fecha", fecha);
                cmd.Parameters.AddWithValue("@turno", turno);
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
                return "";
            }



        }

    }
    
}
