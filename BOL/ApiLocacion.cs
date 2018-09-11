using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
   public class ApiLocacion
    {
        public int id { get; set; }
        public string idPuerto { get; set; }
        public string idConcesionario { get; set; }
        public string rutConcesionario { get; set; }
        public string dvConcesionario { get; set; }
        public string nombreConcesionario { get; set; }
        public string idInstalacion { get; set; }
        public string nombreInstalacion { get; set; }
        public string nombrePuerto { get; set; }
        public string lugar { get; set; }
        public string posicion { get; set; }
        public string activo { get; set; }
    }
}
