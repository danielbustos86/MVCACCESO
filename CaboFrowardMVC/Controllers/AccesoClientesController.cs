using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BOL;
using BOL.Helpers;
using DAL;
using ExcelDataReader;

namespace CaboFrowardMVC.Controllers
{
	public class AccesoClientesController : Controller
	{
		// GET: AccesoClientes

		public ActionResult CargaExcel()
		{

			TempData["fecha"] = DateTime.Now;

			return View();
		}
		[HttpPost]
		public JsonResult VerIngreso(int rut, string pasaporte)
		{

			var respuesta = new { mensaje = "", solicitud = new IngresoCliente(), existe = 0, patente = "", total_patente = 0, aprobadores = "" };
			List<Vehiculo> vehiculos = new List<Vehiculo>();
			IngresoCliente resultado = new IngresoCliente();
			StringBuilder vehiculo_html = new StringBuilder();
			string aprobadores_html = "";
			try
			{
				resultado = AccesoClienteDAL.BuscarIngreso(rut, pasaporte);
				vehiculos = IngresoDAL.ListarPatente(resultado.Idsolicitud);



				if (vehiculos.Count() > 1)
				{
					//vehiculo_html.AppendLine("<option value='" + "0" + "' selected>" + "Sin Vehículo" + "</option>");
					foreach (Vehiculo item in vehiculos)

					{
						vehiculo_html.AppendLine("<option value='" + item.Id + "'>" + item.Patente + "</option>");
					}

				}
				else if (vehiculos.Count() == 1)
				{
					vehiculo_html.AppendLine("<option value='" + "0" + "'>" + "Sin Vehículo" + "</option>");
					foreach (Vehiculo item in vehiculos)
					{
						vehiculo_html.AppendLine("<option value='" + item.Id + "' selected>" + item.Patente + "</option>");
					}
				}
				else
				{
					vehiculo_html.AppendLine("<option value='" + "0" + "' selected>" + "Sin Vehículo" + "</option>");
				}

				if (resultado.Idsolicitud != 0)
				{

					aprobadores_html = IngresoDAL.DevuelveAprobadores(resultado.Idsolicitud, rut);


					respuesta = new { mensaje = "", solicitud = resultado, existe = 1, patente = vehiculo_html.ToString(), total_patente = vehiculos.Count(), aprobadores = aprobadores_html };
					return Json(respuesta);
				}
				else
				{
					respuesta = new { mensaje = "", solicitud = new IngresoCliente(), existe = 0, patente = "", total_patente = 0, aprobadores = aprobadores_html };
					return Json(respuesta);
				}


			}
			catch (Exception ex)
			{
				respuesta = new { mensaje = ex.Message.ToString(), solicitud = new IngresoCliente(), existe = 0, patente = "", total_patente = 0, aprobadores = "" };
				return Json(respuesta);
			}

		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CargaExcel(HttpPostedFileBase file)
		{
			string fileName = "";
			List<CaboFrowardMVC.Models.Masivo> per = new List<CaboFrowardMVC.Models.Masivo>();
			if (file != null)
			{
				if (!file.FileName.EndsWith(".xls") && !file.FileName.EndsWith(".xlsx"))
					return View();

				fileName = DateTime.Now.ToString("yyyyMMddHHmm.") + file.FileName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Last();
				SaveFile(file, fileName);

				per = UploadRecordsToDataBase(fileName);

			}

			// Tu podras decidir que hacer aqui
			// si el archivo es nulo

			if (per != null)
			{




				DeleteFile(fileName);
				if (per.Count > 0)
				{
					per.RemoveAt(0);

				}

				foreach (CaboFrowardMVC.Models.Masivo item in per)
				{
					item.RegistraMasivoCliente();

				}
				ViewData["personas"] = per;


			}
			else
			{
				ViewData["personas"] = null;
				TempData["alertMessage"] = "FORMATO INVALIDO";
			}


			return View();

		}

		private void SaveFile(HttpPostedFileBase file, string fileName)
		{
			var path = System.IO.Path.Combine(Server.MapPath("~/Content/Files/"), fileName);
			var data = new byte[file.ContentLength];
			file.InputStream.Read(data, 0, file.ContentLength);

			using (var sw = new System.IO.FileStream(path, System.IO.FileMode.Create))
			{
				sw.Write(data, 0, data.Length);
			}
		}
		private void DeleteFile(string fileName)
		{
			string fullPath = Request.MapPath("~/Content/Files/" + fileName);
			if (System.IO.File.Exists(fullPath))
			{
				System.IO.File.Delete(fullPath);
			}
		}

		private List<CaboFrowardMVC.Models.Masivo> UploadRecordsToDataBase(string fileName)
		{


			try
			{
				string pasaporte = "";
				string rut = "";
				string dv = "";
				string patente = "";
				string tipoVehiculo = "";
				string lancha = "";
				string nave = "";
				string chofer = "";
				string motivo = "";
				string pc = "";

				string dvEmpresa = "";
				string NombreEmpresa = "";
				var records = new List<CaboFrowardMVC.Models.Masivo>();
				using (var stream = System.IO.File.Open(Path.Combine(Server.MapPath("~/Content/Files/"), fileName), FileMode.Open, FileAccess.Read))
				{
					using (var reader = ExcelReaderFactory.CreateReader(stream))
					{
						while (reader.Read())

						{
							if (reader.GetValue(6) != null)
							{
								pasaporte = reader.GetValue(6).ToString();

							}
							else
							{
								pasaporte = "";
							}

							if (reader.GetValue(1) != null)
							{
								rut = reader.GetValue(1).ToString();

							}
							else
							{
								rut = "";
							}

							if (reader.GetValue(2) != null)
							{
								dv = reader.GetValue(2).ToString();

							}
							else
							{
								dv = "";
							}




							if (reader.GetValue(11) != null)
							{
								dvEmpresa = reader.GetValue(11).ToString();

							}
							else
							{
								dvEmpresa = "";
							}



							if (reader.GetValue(12) != null)
							{
								NombreEmpresa = reader.GetValue(12).ToString();

							}
							else
							{
								NombreEmpresa = "";
							}
							if (reader.GetValue(13) != null)
							{
								patente = reader.GetValue(13).ToString();

							}
							else
							{
								patente = "";
							}


							if (reader.GetValue(14) != null)
							{
								tipoVehiculo = reader.GetValue(14).ToString();

							}
							else
							{
								tipoVehiculo = "";
							}


							if (reader.GetValue(15) != null)
							{
								lancha = reader.GetValue(15).ToString();

							}
							else
							{
								lancha = "";
							}

							if (reader.GetValue(16) != null)
							{
								nave = reader.GetValue(16).ToString();

							}
							else
							{
								nave = "";
							}

							if (reader.GetValue(17) != null)
							{
								chofer = reader.GetValue(17).ToString();

							}
							else
							{
								chofer = "N";
							}

							if (reader.GetValue(18) != null)
							{
								motivo = reader.GetValue(18).ToString();

							}
							else
							{
								motivo = "0";
							}

							if (reader.GetValue(19) != null)
							{
								pc = reader.GetValue(19).ToString();

							}
							else
							{
								pc = "";
							}




							if (pasaporte != "" || rut != "") { 

							records.Add(new CaboFrowardMVC.Models.Masivo
							{
								idNacionalidad = reader.GetValue(0).ToString(),
								Rut = rut,
								Dv = dv,
								Nombre = reader.GetValue(3).ToString(),
								ApellidoPat = reader.GetValue(4).ToString(),
								ApellidoMat = reader.GetValue(5).ToString(),
								pasaporte = pasaporte,
								idLocacion = reader.GetValue(7).ToString(),
								FechaDesde = reader.GetValue(8).ToString(),
								FechaHasta = reader.GetValue(9).ToString(),
								RutEmpresa = reader.GetValue(10).ToString(),
								Patente = patente,
								TipVehiculo = tipoVehiculo,
								Lancha = lancha,
								Nave = nave,
								Motivo = motivo,
								PC = pc,
								DvEmpresa = dvEmpresa,
								NombreEmpresa = NombreEmpresa,
								Chofer = chofer

								});
							}
						}
					}
				}


				return records;
			}
			catch (Exception exp)
			{
				String R = exp.Message.ToString();
				return null;
			}
			//if (records.Any())
			//{
			//    db.Users.AddRange(records);
			//    db.SaveChanges();
			//}


		}


		public ActionResult Reporte()
		{

			TempData["fecha"] = DateTime.Now;

			return View();
		}


		public ActionResult Salida()
		{

			TempData["fecha"] = DateTime.Now;

			return View();
		}
		public ActionResult Index()
		{

			//List<CaboFrowardMVC.Models.ClientesTTP> list = new List<CaboFrowardMVC.Models.ClientesTTP>();
			//List<BOL.Empresas> emp = new List<BOL.Empresas>();
			//emp = DAL.AccesoClienteDAL.GetEmpresas();


			List<CaboFrowardMVC.Models.ClientesTTP> clientesTTPs = new List<CaboFrowardMVC.Models.ClientesTTP>();
			try
			{
				SqlCommand cmd = new SqlCommand();
				DataTable dt = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				Coneccion param = Parameter.Leer_parametros();
				cmd.Connection = new SqlConnection(param.ConString);
				cmd.Connection.Open();
				cmd.Parameters.Clear();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "select ID_EMPRESA,NOMBRES,RUT = cast(RUT as varchar) + '-' + DV from empresas";


				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();

				if (dt.Rows.Count > 0)
				{
					foreach (DataRow item in dt.Rows)
					{
						clientesTTPs.Add(new CaboFrowardMVC.Models.ClientesTTP
						{
							Id = Int32.Parse(item["ID_EMPRESA"].ToString()),
							Rut = item["RUT"].ToString(),
							Nombre = item["NOMBRES"].ToString()
						});
					}
				}
				ViewData["empresas"] = clientesTTPs;
			}
			catch (SqlException ex)
			{
				ViewData["empresas"] = null;
				TempData["alertMessage"] = ex.Message.ToString();
			}




			TempData["fecha"] = DateTime.Now;
			Login Login = new Login();
			Login = (Login)Session["UsuarioAutentificado"];



			TempData["usuario"] = Login.Nombre;
				
			ViewData["naves"] = RetornaNavesTTTP();
			ViewData["lanchas"] = RetornaLANCHATTP();

			
			return View();
		}



		public List<CaboFrowardMVC.Models.NavesTTP> RetornaNavesTTTP()
		{
			List<CaboFrowardMVC.Models.NavesTTP> navesTTPs = new List<CaboFrowardMVC.Models.NavesTTP>();

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
				cmd.CommandText = "RecuperaNaveTTP";


				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();

				if (dt.Rows.Count > 0)
				{
					foreach (DataRow item in dt.Rows)
					{
						navesTTPs.Add(new CaboFrowardMVC.Models.NavesTTP
						{
							Id = item["id"].ToString(),
							Nombre = item["nombre"].ToString()
						});
					}
				}

			}
			catch (SqlException ex)
			{
				return null;
			}






			return navesTTPs;
		}

		public List<CaboFrowardMVC.Models.NavesTTP> RetornaLANCHATTP()
		{
			List<CaboFrowardMVC.Models.NavesTTP> navesTTPs = new List<CaboFrowardMVC.Models.NavesTTP>();

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
				cmd.CommandText = "RecuperaLanchaTTP";


				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();

				if (dt.Rows.Count > 0)
				{
					foreach (DataRow item in dt.Rows)
					{
						navesTTPs.Add(new CaboFrowardMVC.Models.NavesTTP
						{
							Id = item["ID"].ToString(),
							Nombre = item["nombre"].ToString()
						});
					}
				}

			}
			catch (SqlException ex)
			{
				return null;
			}






			return navesTTPs;
		}


		[HttpPost]
		public JsonResult RegistrarIngreso(int TipoVehiculo, string Patente, int TipoDoc, int IdSol, int IdSolCli, int IdPersonaAproba, int TipoMov, int CodigoCliente, string Motivo, int TipoPago, int Detalle, string Nave, string NGuia, double Toneladas, string PC, string nboleta,string lancha,string usuario,string tipoContado,string cantidadhoras,double tara)
		{
			String codigo1 = "";
			var respuesta = new { mensaje = "", codigo = "" };

			try
			{
				string resp = AccesoClienteDAL.RegistraAcceso(TipoMov, "", IdPersonaAproba, Patente, TipoPago, Detalle, NGuia, Toneladas, PC, nboleta,Motivo,Nave,lancha,CodigoCliente,IdSol,TipoVehiculo,usuario,tipoContado, cantidadhoras, tara);

				respuesta = new { mensaje = resp, codigo = codigo1 };
				return Json(respuesta);
			}
			catch (Exception ex)
			{
				respuesta = new { mensaje = ex.Message, codigo = "" };
				return Json(respuesta);
			}

		}


		[HttpGet]
			public FileResult DescargarArchivo()
		{
			var ruta = Server.MapPath("../Cargamasiva.xlsx");
			return File(ruta, "application/xlsx", "CargaMasiva1.xlsx");

		}


		[HttpPost]
		public JsonResult RetornaIngreso(string Patente)
		{
			var respuesta = new { mensaje = "", datos = "",Fecha="" };
			string html = "";
			string msj = "";
			string codigo1 = "";
			string FechaIng = "";
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
				cmd.CommandText = "SPRecuperaIngresoCli";
				cmd.Parameters.AddWithValue("@Patente", Patente);

				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();



				if (dt.Rows.Count > 0)
				{
					codigo1 = dt.Rows[0]["CODIGO"].ToString();
					FechaIng = dt.Rows[0]["FechaHora"].ToString();

				}
				else {
					msj = "La patente " + Patente+" no se encuentra registrada dentro del puerto";

				}




			}
			catch (SqlException ex)
			{
				msj = ex.Message.ToString();
			}



			respuesta = new { mensaje = msj, datos = codigo1, Fecha = FechaIng };
			return Json(respuesta);




		}

		[HttpPost]
		public JsonResult RetornaConvenio(string Patente)
		{
			var respuesta = new { mensaje = "", datos = "", cantidad = "" };
			string html = "";
			string msj = "";
			string codigo1 = "";
			string FechaIng = "";
			string can = "";
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
				cmd.CommandText = "BuscaConvenio";
				cmd.Parameters.AddWithValue("@Patente", Patente);

				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();



				if (dt.Rows.Count > 0)
				{
					codigo1 = dt.Rows[0]["TipoExclusion"].ToString();
		

				}
				else
				{
					can = "0";

				}




			}
			catch (SqlException ex)
			{
				msj = ex.Message.ToString();
			}



			respuesta = new { mensaje = msj, datos = codigo1, cantidad = can };
			return Json(respuesta);




		}





		[HttpPost]
		public JsonResult RegistraSalida(string ano,string id, int DetalleSalida,string guiaSalida,decimal ToneladasSalida,string PCSalida)
		{

			var respuesta = new { mensaje = "", datos = "", Fecha = "" };
			string html = "";
			string msj = "";
			string codigo1 = "";
			string FechaIng = "";
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
				cmd.CommandText = "RegistraSalida";
				cmd.Parameters.AddWithValue("@ano", ano);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@DetalleSalida", DetalleSalida);
				cmd.Parameters.AddWithValue("@guiaSalida", guiaSalida);
				cmd.Parameters.AddWithValue("@ToneladasSalida", ToneladasSalida);
				cmd.Parameters.AddWithValue("@PCSalida", PCSalida);
				cmd.ExecuteNonQuery();

				msj = "OK";




			}
			catch (SqlException ex)
			{
				msj = ex.Message.ToString();
			}



			respuesta = new { mensaje = msj, datos = codigo1, Fecha = FechaIng };
			return Json(respuesta);




		}



		[HttpPost]
		public JsonResult Reporte(string FechaInicio,string FechaFin)
		{
			var respuesta = new { mensaje = "", datos = "", Fecha = "" };
			string html = "";
			string msj = "";
			string codigo1 = "";
			string FechaIng = "";
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
				cmd.CommandText = "ReporteCLiente1";
				cmd.Parameters.AddWithValue("@fechainicio", FechaInicio);
				cmd.Parameters.AddWithValue("@fechafin", FechaFin);
				da.Fill(dt);
				cmd.Connection.Close();
				cmd.Dispose();



				if (dt.Rows.Count > 0)
				{
					codigo1 = Util.DevuelveBodyHtmlNormal(dt);
				}
				else
				{
					msj = "NO HAY REGISTROS PARA LA CONSULTA INGRESADA";

				}




			}
			catch (SqlException ex)
			{
				msj = ex.Message.ToString();
			}



			respuesta = new { mensaje = msj, datos = codigo1, Fecha = FechaIng };
			return Json(respuesta);




		}

		public JsonResult ModificaVehiculo(string patente, int tipo, string descripcion, int id)

		{

			StringBuilder vehiculo_html = new StringBuilder();
			List<Vehiculo> vehiculos = new List<Vehiculo>();
			var respuesta = new { mensaje = "", patente = "" };
			try
			{

				VehiculoDAL.ModificaPatenteIngreso(patente, tipo, descripcion, id);

				//Obtenemos nuevo listado de patente
				//vehiculo_html.AppendLine("<option value='" + "0" + "'>" + "Sin Vehículo" + "</option>");
				vehiculos = IngresoDAL.ListarPatente(id);
				foreach (Vehiculo item in vehiculos)
				{

					if (patente == item.Patente)
					{
						vehiculo_html.AppendLine("<option value='" + item.Id + "'" + " selected>" + item.Patente + "</option>");
					}
					else
					{
						vehiculo_html.AppendLine("<option value='" + item.Id + "'" + ">" + item.Patente + "</option>");
					}
				}

				respuesta = new { mensaje = "", patente = vehiculo_html.ToString() };
				return Json(respuesta);
			}
			catch (Exception ex)
			{
				respuesta = new { mensaje = "ERROR: " + ex.Message.ToString(), patente = "" };
				return Json(respuesta);
			}
		}


		[HttpPost]
		public JsonResult CargaMotivo()
		{

			var respuesta = new { mensaje = "", html="" };
			List<Motivo> lm = DAL.MotivoDAL.GetMotivos();
			StringBuilder motivo_html = new StringBuilder();

			if (lm.Count > 0)
			{
				foreach (Motivo item in lm)
				{
					motivo_html.AppendLine("<option value='" + item.idMotivo + "'>" + item.descripcionMotivo + "</option>");
				}

			}


			 respuesta = new { mensaje = "", html = motivo_html.ToString() };

            return Json(respuesta);


		}

		[HttpPost]
		public JsonResult CargaTipoPago(int idMotivo)
		{

			var respuesta = new { mensaje = "", html = "" };
			List<TipoPago> lt = DAL.TipopagoDAL.GetTipopago(idMotivo);
			StringBuilder tipopago_html = new StringBuilder();

			if (lt.Count > 0)
			{
				foreach (TipoPago item in lt)
				{
					tipopago_html.AppendLine("<option value='" + item.id + "'>" + item.descripcion + "</option>");
				}

			}
			else {

				tipopago_html.AppendLine("");
			}


			respuesta = new { mensaje = "", html = tipopago_html.ToString() };

			return Json(respuesta);


		}

	}

	}
