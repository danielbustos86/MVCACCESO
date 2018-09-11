using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BOL;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.IO;
using ExcelDataReader;

namespace CaboFrowardMVC.Controllers
{
    public class CargasMasivasController : Controller
    {
        // GET: CargasMasivas
        public ActionResult CargaNombrada()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult CargarExcel()
        {
            if (Session["UsuarioAutentificado"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public JsonResult RecuperaNombrada(string fecha,string turno)
        {
            var respuesta = new { mensaje = "",nombrada="" };
            var resul = new JavaScriptSerializer();
            try
            {

                respuesta = new { mensaje = "" , nombrada=""};

                CredencialesWS cr = BOL.Parameter.LeerCredencialesws();
                Nombrada nm = new Nombrada();

                nm.fechaInicioNombrada = fecha;
                string respuesta1 = wsNombrada.Execute(cr, nm);

                XDocument xmlDoc = XDocument.Parse(respuesta1);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(respuesta1);

                List<Nombrada> nombradas = new List<Nombrada>();

                XmlElement root = doc.DocumentElement;
                XmlNodeList elemList = root.GetElementsByTagName("NombradaOutConcesionario");
				string idnave1 = "";
				string Navedes1 = "";
				string aux = "";



				for (int i = 0; i < elemList.Count; i++)
				{
					List<PerNombrada> pers = new List<PerNombrada>();
					foreach (XmlNode per in elemList[i].LastChild)
					{
						pers.Add(new PerNombrada
						{
							Rut = per.ChildNodes[1].InnerXml,
							DV = per.ChildNodes[2].InnerXml,
							Nombre = per.ChildNodes[3].InnerXml,
							ApellidoPat = per.ChildNodes[4].InnerXml,
							ApellidoMat = per.ChildNodes[4].InnerXml
						});
					}

					XmlNamespaceManager nsManager;

					nsManager = new XmlNamespaceManager(doc.NameTable);

					nsManager.AddNamespace("fi", "https://sccnlp.com/");


					if (elemList[i].SelectSingleNode("fi:idNave", nsManager).InnerXml != "")
					{
						idnave1 = elemList[i].SelectSingleNode("fi:idNave", nsManager).InnerXml;
						Navedes1 = elemList[i].SelectSingleNode("fi:nombreNave", nsManager).InnerXml;

					}
					

					
			

					aux= elemList.Item(i).SelectSingleNode("fi:fechaFinNombrada", nsManager).InnerXml;


					nombradas.Add(new Nombrada
                    {
					
						fechaInicioNombrada = elemList[i].SelectSingleNode("fi:fechaInicioNombrada", nsManager).InnerXml,
					    fechaFinNombrada = elemList[i].SelectSingleNode("fi:fechaFinNombrada", nsManager).InnerXml,
						idNave = idnave1,
					    NaveDescrip = Navedes1,
						idLocacion = elemList[i].SelectSingleNode("fi:idLocacion", nsManager).InnerXml,
						idPuerto = elemList[i].SelectSingleNode("fi:idPuerto", nsManager).InnerXml,
						idTurno = elemList[i].SelectSingleNode("fi:idTurno", nsManager).InnerXml,
						rutEmpresa = elemList[i].SelectSingleNode("fi:rutMuellaje", nsManager).InnerXml,
						PersonasNom = pers
                    });
                }

                List<Nombrada> list = nombradas.Where(x => x.idTurno == turno && x.fechaInicioNombrada.Contains(fecha)).ToList();

                DAL.NombradaDal.CargarNombrada(list);

                string resp = DAL.NombradaDal.GetNombradas(fecha, turno);
                string msj = "";

                if (resp == "") {
                    msj = "Sin resultados";
                }

                respuesta = new { mensaje = msj, nombrada = resp };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message , nombrada = "" };
                return Json(respuesta);

           }

        }


        [HttpPost]
        public JsonResult CargarSelect(int puerto)
        {
            var respuesta = new { mensaje = "", locaciones = ""};
            StringBuilder locacion_html = new StringBuilder();
  
            List<Locacion> ls_locacion = new List<Locacion>();
            ls_locacion = Mantenedores.GetLocaciones(puerto);

            locacion_html.AppendLine("<option value='" + "0" + "' selected>" + "Seleccione" + "</option>");


            if (ls_locacion.Count > 0)
            {
                foreach (Locacion item in ls_locacion)
                {
                    locacion_html.AppendLine("<option value='" + item.Id + "'>" + item.Nombre + "</option>");
                }

            }


            respuesta = new { mensaje = "", locaciones = locacion_html.ToString() };
            return Json(respuesta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CargarExcel(HttpPostedFileBase file)
        {
            string fileName="";
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
                    item.RegistraMasivo();

                }
                ViewData["personas"] = per;


            }
            else {
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
        private void DeleteFile(string fileName) {
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
                                patente = reader.GetValue(11).ToString();

                            }
                            else
                            {
                                patente = "";
                            }

                            if (reader.GetValue(12) != null)
                            {
                                tipoVehiculo = reader.GetValue(12).ToString();

                            }
                            else
                            {
                                tipoVehiculo = "";
                            }


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
                                TipVehiculo = tipoVehiculo
                                //    Email = reader.GetString(0),
                                //    Credits = int.Parse(reader.GetValue(1).ToString()),
                                //    RecordDate = DateTime.Now,
                                //    IsActive = true,

                            });
                        }
                    }
                }


                return records;
            }
            catch(Exception exp) {
                return null;
            }
            //if (records.Any())
            //{
            //    db.Users.AddRange(records);
            //    db.SaveChanges();
            //}


        }

        //[HttpPost]
        //public ActionResult CargarExcel(List<CaboFrowardMVC.Models.Masivo> personas)
        //{


        //    ViewData["personas"] = personas;

        //    return View();
        //}


        public string GuardaPersonaSol(CaboFrowardMVC.Models.Masivo ma)
        {
            string resp = "";





            return resp; 
        }


        [HttpPost]
        public JsonResult GetNombradas(string fecha, string turno)
        {
        
            var respuesta = new { mensaje = "", html = "" };
            string resp_html = "";
            string msj = "";
            try
            {
                resp_html = DAL.NombradaDal.GetNombradas(fecha, turno);

                if (resp_html == "") {
                    msj = "sin resultados";

                }
                respuesta = new { mensaje = msj, html = resp_html };
                return Json(respuesta);
            }
            catch (Exception ex)
            {
                respuesta = new { mensaje = ex.Message.ToString(), html = "" };
                return Json(respuesta);
            }
        }


    }



}
