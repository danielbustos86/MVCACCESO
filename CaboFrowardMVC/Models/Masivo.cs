using BOL.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using BOL; 

namespace CaboFrowardMVC.Models
{
    public class Masivo
    {
        public string Rut { get; set; }
        public string Dv { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string idNacionalidad { get; set; }
        public string pasaporte { get; set; }
        public string idLocacion { get; set; }
        public string DescripcionLocacion { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta{ get; set; }
        public string Observacion { get; set; }
        public string RutEmpresa { get; set; }
        public string Patente { get; set; }
        public string TipVehiculo { get; set; }
		public string Lancha { get; set; }
		public string Nave { get; set; }
		public string Chofer { get; set; }
		public string Motivo { get; set; }
		public string PC { get; set; }
		public string DvEmpresa { get; set; }
		public string NombreEmpresa { get; set; }

		public Masivo RegistraMasivo()
		{
			DateTime fecha1;
			DateTime fecha2;
			Boolean validafecha1 = false;
			Boolean validafecha2 = false;



			try
			{




				SqlCommand cmd = new SqlCommand();
				DataTable dt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				Coneccion param = BOL.Parameter.Leer_parametros();
				cmd.Connection = new SqlConnection(param.ConString);
				cmd.Connection.Open();
				cmd.Parameters.Clear();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "SpRegistraMasivo";

				cmd.Parameters.AddWithValue("@rut", Rut);
				cmd.Parameters.AddWithValue("@dv ", Dv);
				cmd.Parameters.AddWithValue("@nombre ", Nombre);
				cmd.Parameters.AddWithValue("@apellidoPaterno ", ApellidoPat);
				cmd.Parameters.AddWithValue("@apellidoMaterno ", ApellidoMat);
				cmd.Parameters.AddWithValue("@nacionalidad ", idNacionalidad);
				cmd.Parameters.AddWithValue("@idlocacion ", idLocacion);
				cmd.Parameters.AddWithValue("@fechaDesde ", FechaDesde);
				cmd.Parameters.AddWithValue("@fechaHasta ", FechaHasta);
				cmd.Parameters.AddWithValue("@rutEmpresa ", RutEmpresa);
				cmd.Parameters.AddWithValue("@pasaporte ", pasaporte);
				cmd.Parameters.AddWithValue("@vehiculo ", Patente);
				cmd.Parameters.AddWithValue("@tipoVehiculo ", TipVehiculo);




				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();
				if (dt.Rows.Count > 0)
				{

					DescripcionLocacion = dt.Rows[0]["LOCACION"].ToString();
					Observacion = dt.Rows[0]["RESPUESTA"].ToString();

				}

			}
			catch (SqlException exp)


			{

				if (exp.Message.ToString().Contains("Error converting data type nvarchar to datetime"))
				{
					Observacion = "Revise formato de fechas (AAAA-MM-DD HH:MM:SS)";
				}
				else
				{
					Observacion = exp.Message.ToString();
				}

			}
			return this;
		}
		public Masivo RegistraMasivoCliente()
        {
            DateTime fecha1;
            DateTime fecha2;
            Boolean validafecha1 = false;
            Boolean validafecha2 = false;

       




            
            try
            {

                
                

                SqlCommand cmd = new SqlCommand();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                Coneccion param = BOL.Parameter.Leer_parametros();
                cmd.Connection = new SqlConnection(param.ConString);
                cmd.Connection.Open();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SpRegistraMasivoCliente";
               
                cmd.Parameters.AddWithValue("@rut",Rut);
                cmd.Parameters.AddWithValue("@dv", Dv);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", ApellidoPat);
                cmd.Parameters.AddWithValue("@apellidoMaterno", ApellidoMat);
                cmd.Parameters.AddWithValue("@nacionalidad", idNacionalidad);
                cmd.Parameters.AddWithValue("@idlocacion",idLocacion);
                cmd.Parameters.AddWithValue("@fechaDesde",FechaDesde);
                cmd.Parameters.AddWithValue("@fechaHasta", FechaHasta);
                cmd.Parameters.AddWithValue("@rutEmpresa", RutEmpresa);
                cmd.Parameters.AddWithValue("@pasaporte", pasaporte);
                cmd.Parameters.AddWithValue("@vehiculo", Patente);
                cmd.Parameters.AddWithValue("@tipoVehiculo ", TipVehiculo);


				cmd.Parameters.AddWithValue("@Lancha", Lancha);
				cmd.Parameters.AddWithValue("@Nave", Nave);
				cmd.Parameters.AddWithValue("@Chofer",Chofer);
				cmd.Parameters.AddWithValue("@Motivo", Motivo);
				cmd.Parameters.AddWithValue("@PC", PC);
				cmd.Parameters.AddWithValue("@dvEmpresa ", DvEmpresa);
				cmd.Parameters.AddWithValue("@NombreEmpresa", NombreEmpresa);



				da.Fill(dt);
                cmd.Connection.Close();
                cmd.Dispose();
                if (dt.Rows.Count > 0)
                {

                    DescripcionLocacion = dt.Rows[0]["LOCACION"].ToString(); 
                    Observacion = dt.Rows[0]["RESPUESTA"].ToString();
               
                }
                
            }
            catch (SqlException exp)


            {

                if (exp.Message.ToString().Contains("Error converting data type nvarchar to datetime")){
                    Observacion = "Revise formato de fechas (AAAA-MM-DD HH:MM:SS)";
                }
                else {
                    Observacion = exp.Message.ToString();
                }

            }
            return this;
        }
    }
}
