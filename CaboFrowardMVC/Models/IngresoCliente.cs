using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaboFrowardMVC.Models
{
	public class IngresoCliente
	{
		public int IdIngresoCliente { get; set; }
		public int IdSolicitud { get; set; }
		public string RutEMpresa { get; set; }
		public string FechaDesde { get; set; }
		public string FechaHasta { get; set; }
		public string Patente { get; set; }
		public string RutChofer { get; set; }
		public string ChoferNombre { get; set; }
		public string Motivo { get; set; }
		public int IdVehiculo { get; set; }
		public int IdPersonaAprobada { get; set; }
		

	}
}
