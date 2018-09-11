using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
	public class IngresoCliente
	{
		public string Pasaporte { get; set; }
		public string Nombre { get; set; }
		public int Idsolicitud { get; set; }
		public string Estado { get; set; }
		public string Puerto { get; set; }
		public string Usuario { get; set; }
		public string Locacion { get; set; }
		public string Inicio { get; set; }
		public string Fin { get; set; }

		public string Empresa { get; set; }
		public string Nave { get; set; }
		public string NaveTTP { get; set; }
		public string Lancha { get; set; }
		public int Idaprobado { get; set; }
		public int IdSolicitudCliente { get; set; }
		public string Autorizacion { get; set; }
		public string Motivo { get; set; }
		public string TipoVehiculo { get; set; }
		public string PC { get; set; }
		public int TipoPago { get; set; }
		public string TipoContado { get; set; }
		public string CantidadHora { get; set; }
		public string IdEntrada { get; set; }
		public string Induccion { get; set; }
	}
}
