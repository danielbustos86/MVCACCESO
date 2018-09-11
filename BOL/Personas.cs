using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public class Personas
    {
        public int Id { get; set; }
        public int Rut { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Boolean Inactivo { get; set; }
    }
}
