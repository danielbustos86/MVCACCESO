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
public	class MotivoDAL
	{
		public static List<Motivo> GetMotivos()
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
				cmd.CommandText = "SP_LISTAR_MOTIVOS";
				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();
				List<Motivo> ls_motivo = new List<Motivo>();



				if (dt.Rows.Count > 0)
				{
					foreach (DataRow item in dt.Rows)
					{
						ls_motivo.Add(new Motivo
						{
							idMotivo = Int32.Parse(item["idMotivo"].ToString()),
							descripcionMotivo = item["dm"].ToString(),
				
						});

					}

				}
				return ls_motivo;


			}
			catch (Exception)
			{
				throw;
			}


		}
	}
}
