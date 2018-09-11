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
	public class TipopagoDAL
	{
		public static List<TipoPago> GetTipopago(int idMotivo)
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
				cmd.CommandText = "SP_Tipo_Pago_x_Motivo";
				cmd.Parameters.AddWithValue("@idmotivo", idMotivo);
				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();
				List<TipoPago> ls_tipoPago = new List<TipoPago>();



				if (dt.Rows.Count > 0)
				{
					foreach (DataRow item in dt.Rows)
					{
						ls_tipoPago.Add(new TipoPago
						{
							id = Int32.Parse(item["idTipo"].ToString()),
							descripcion = item["descripcion"].ToString(),

						});

					}

				}
				return ls_tipoPago;


			}
			catch (Exception)
			{
				throw;
			}


		}
	}
}
