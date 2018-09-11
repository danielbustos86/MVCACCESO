using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CaboFrowardMVC.Models
{
	public class ClientesTTP
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Rut { get; set; }

	}
}
