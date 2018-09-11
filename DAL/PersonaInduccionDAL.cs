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
	public class PersonaInduccionDAL
	{
		public static string ActualizaFecha(DateTime fecha,string personas)
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
				cmd.CommandText = "SP_ACTUALIZA_INDUCCION";
				cmd.Parameters.AddWithValue("@fecha", fecha);
				cmd.Parameters.AddWithValue("@personas", personas);

				cmd.ExecuteNonQuery();
				cmd.Connection.Close();
				cmd.Dispose();
				return "ok";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		public static string ListarPersonas(string personas) {

			try
			{
				SqlCommand cmd = new SqlCommand();
				DataTable dt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				string html = "";
				Coneccion param = Parameter.Leer_parametros();
				cmd.Connection = new SqlConnection(param.ConString);
				cmd.Connection.Open();
				cmd.Parameters.Clear();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SP_LISTA_INDUCCION_ACTUALIZADA";
				cmd.Parameters.AddWithValue("@personas", personas);
				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();

				if (dt.Rows.Count > 0)
				{
					html = Util.DevuelveBodyHtmlNormal(dt);

				}


				return html;


			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			
		}

	}
}
