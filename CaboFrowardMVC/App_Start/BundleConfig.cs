using System.Web;
using System.Web.Optimization;

namespace CaboFrowardMVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {



            bundles.Add(new ScriptBundle("~/bundles/js").Include(                           
                            "~/scripts/bootstrap.min.js",                       
                            "~/content/vendor/metisMenu/metisMenu.min.js",
                            "~/content/vendor/raphael/raphael.min.js",
                            "~/scripts/custom/LogOut.js",
                            "~/scripts/custom/Msg.js",
                            "~/content/dist/js/sb-admin-2.js"
                            ));



            bundles.Add(new StyleBundle("~/bundles/css").Include("~/content/vendor/bootstrap/css/bootstrap.min.css",
                                                             "~/content/vendor/metisMenu/metisMenu.min.css",
                                                             "~/content/dist/css/sb-admin-2.css",
                                                             "~/content/selectize.default.css",
                                                             "~/content/vendor/font-awesome/css/font-awesome.min.css",
                                                             "~/content/jquery.datetimepicker.css",
                                                                       "~/scripts/chosen/chosen.min.css",
																	  "~/scripts/Select2/select2.min.css",
																	  "~/scripts/Select2/select2-bootstrap.css"));
     

            bundles.Add(new ScriptBundle("~/bundles/js_solicitud").Include(
                          "~/scripts/custom/Personas.js",
                           "~/scripts/custom/Vehiculo.js",
                           "~/scripts/custom/Nave.js",
                           "~/scripts/custom/Locaciones.js",
                           "~/scripts/custom/Solicitud.js",
                           "~/scripts/custom/listados.js",
							"~/scripts/Select2/Select2.js",
                           "~/scripts/jquery.datetimepicker.full.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js_lista_solicitud").Include(
                         "~/scripts/custom/Solicitud.js",
                           "~/scripts/jquery.datetimepicker.full.min.js"));
            
    
            bundles.Add(new ScriptBundle("~/bundles/js_lista_aprobar").Include(
                         "~/scripts/custom/Aprobacion.js"));

            bundles.Add(new ScriptBundle("~/bundles/js_lista_mis_sol").Include(
                 "~/scripts/custom/ListadoMisSol.js",
				 "~/scripts/Select2/Select2.js"));


            bundles.Add(new ScriptBundle("~/bundles/js_ingreso").Include(
                "~/scripts/custom/Ingreso.js"));

            bundles.Add(new ScriptBundle("~/bundles/js_Nombrada").Include(
        "~/scripts/custom/Nombrada.js",
            "~/scripts/jquery.datetimepicker.full.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js_cargaexcel").Include(
            "~/scripts/custom/CargaMasivaExcel.js"));


            bundles.Add(new ScriptBundle("~/bundles/dash").Include(
                "~/scripts/custom/Dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/reportes").Include(
                "~/scripts/custom/Reportes.js"));

            bundles.Add(new ScriptBundle("~/bundles/AccesoClientes").Include(
      "~/scripts/chosen/chosen.jquery.min.js",
        "~/scripts/custom/AccesoClientes.js",
		"~/scripts/Select2/Select2.js"));



			bundles.Add(new ScriptBundle("~/bundles/ReporteAcceso").Include(
	  "~/scripts/chosen/chosen.jquery.min.js",
		"~/scripts/custom/ReporteClientes.js",
		"~/scripts/Select2/Select2.js",
		"~/scripts/jquery.datetimepicker.full.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/Induccion").Include(
  "~/scripts/Custom/Induccion.js",
   "~/scripts/jquery.datetimepicker.full.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/PersonaEF").Include(
"~/scripts/Custom/PersonaEF.js"));

			bundles.Add(new ScriptBundle("~/bundles/usuarioPerfil").Include(
                "~/scripts/custom/UsuariosPerfiles.js",
                "~/scripts/custom/gsdk-checkbox.js",
                  "~/scripts/custom/gsdk.css"
                ));


            
        }
    }
}
