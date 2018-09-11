using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
	public class PERSONAINDUCCION
	{
		public int Id { get; set; }
		public string Rut { get; set; }
		public string Dv { get; set; }
		public string Pasaporte { get; set; }
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public Boolean Inactivo { get; set; }
		public string estadoInduccion { get; set; }
		public string fechaInduccion { get; set; }
	}
}
