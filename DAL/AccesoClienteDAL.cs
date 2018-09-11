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
	public class AccesoClienteDAL
	{
		public static IngresoCliente BuscarIngreso(int rut, string pasaporte)

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
				cmd.CommandText = "DATOS_MOVIMIENTO_CLIENTE";
				if (rut == 0)
				{
					cmd.Parameters.AddWithValue("@RUT", System.DBNull.Value);
				}
				else
				{
					cmd.Parameters.AddWithValue("@RUT", rut);

				}
				if (pasaporte == "")
				{
					cmd.Parameters.AddWithValue("@PASAPORTE", System.DBNull.Value);
				}
				else
				{
					cmd.Parameters.AddWithValue("@PASAPORTE", pasaporte);

				}

				SqlDataReader rdr = cmd.ExecuteReader();
				IngresoCliente aux_ingreso = new IngresoCliente();

				while (rdr.Read())
				{
					aux_ingreso.Nombre = rdr["NOMBRE"].ToString();
					aux_ingreso.Idsolicitud = Int32.Parse(rdr["ID_SOLICITUD"].ToString());
					aux_ingreso.Estado = rdr["ESTADO"].ToString();
					aux_ingreso.Autorizacion = rdr["AUTORIZACION"].ToString();
					aux_ingreso.Puerto = rdr["PUERTO"].ToString();
					aux_ingreso.Usuario = rdr["USUARIO"].ToString();
					aux_ingreso.Locacion = rdr["LOCACION"].ToString();
					aux_ingreso.Nave = rdr["NAVE"].ToString();
					aux_ingreso.Inicio = rdr["FECHA_DESDE"].ToString();
					aux_ingreso.Fin = rdr["FECHA_HASTA"].ToString();
				
					aux_ingreso.Idaprobado = Int32.Parse(rdr["ID_PERSONA_APROBADA"].ToString());
					aux_ingreso.Pasaporte = rdr["PASAPORTE"].ToString();

					aux_ingreso.Empresa = rdr["ID_EMPRESA"].ToString();
					aux_ingreso.NaveTTP = rdr["NaveTTP"].ToString();
					aux_ingreso.Lancha  = rdr["LANCHA"].ToString();
					aux_ingreso.Motivo = rdr["MOTIVO"].ToString();

					aux_ingreso.IdSolicitudCliente = Int32.Parse(rdr["ID_SOLICITUD_CLIENTE"].ToString());
					aux_ingreso.PC = rdr["PC"].ToString();
					aux_ingreso.TipoVehiculo = rdr["TIPO_VEHICULO"].ToString();
					aux_ingreso.TipoPago = Int32.Parse(rdr["TIPOPAGO"].ToString());
					aux_ingreso.TipoContado = rdr["tipoContado"].ToString();
					aux_ingreso.CantidadHora  = rdr["horas"].ToString();
					aux_ingreso.IdEntrada = rdr["ID_ENTRADA"].ToString();
					aux_ingreso.Induccion = rdr["INDUCCION"].ToString();
					

				}

				cmd.Connection.Close();
				cmd.Dispose();
				return aux_ingreso;
			}
			catch (SqlException  exp)
			{
				string msj = exp.Message.ToString();
				throw;
			}

		}


		public static string RegistraAcceso(int tipo, string ip, int persona_persona_aprobada, string patente,int tipoPago,int detalle,string guia,double toneledas,string pc,string boleta,string motivo,string nave,string lancha,int idempresa,int idsolicitud,int tipoVehiculo,string usuario,string tipoContado,string cantidadHoras,double tara)
		{

			string idresp="0";
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
				cmd.CommandText = "REGISTRA_ACCESO_CLIENTE";
				cmd.Parameters.AddWithValue("@MOV_ID_MOVIMIENTO", tipo);
				cmd.Parameters.AddWithValue("@IP", "");
				cmd.Parameters.AddWithValue("@ID_VEHICULO", patente);
				cmd.Parameters.AddWithValue("@ID_PERSONA_APROBADA", persona_persona_aprobada);

				cmd.Parameters.AddWithValue("@TIPO_PAGO", tipoPago);
				cmd.Parameters.AddWithValue("@DETALLE", detalle);
				cmd.Parameters.AddWithValue("@NGUIA", guia);
				cmd.Parameters.AddWithValue("@TONELADAS", toneledas);
				cmd.Parameters.AddWithValue("@TARA", tara);
				cmd.Parameters.AddWithValue("@PC", pc);
				cmd.Parameters.AddWithValue("@BOLETA", boleta);

				cmd.Parameters.AddWithValue("@MOTIVO", motivo);
				cmd.Parameters.AddWithValue("@NAVE", nave);
				cmd.Parameters.AddWithValue("@LANCHA", lancha);
				cmd.Parameters.AddWithValue("@IDEMPRESA", idempresa);
				cmd.Parameters.AddWithValue("@ID_SOLICITUD", idsolicitud);

				cmd.Parameters.AddWithValue("@TIPOVEHICULO", tipoVehiculo);

				cmd.Parameters.AddWithValue("@USUARIO", usuario);
				cmd.Parameters.AddWithValue("@TIPOCONTADO", tipoContado);
				cmd.Parameters.AddWithValue("@CANTIDADHORAS", cantidadHoras);
				cmd.Parameters.AddWithValue("@IDENTRADA", 0);
				cmd.Parameters["@IDENTRADA"].Direction = ParameterDirection.Output;
				cmd.ExecuteNonQuery();
				idresp = cmd.Parameters["@IDENTRADA"].Value.ToString();

				cmd.Connection.Close();
				cmd.Dispose();

				return idresp;
			}
			catch (SqlException ex)
			{
				return "Error Numero 57-AccesoClienteDAL";
			}
		}




	}
}
