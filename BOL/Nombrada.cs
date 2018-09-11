using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
  public class Nombrada
    {
        public string fechaInicioNombrada { get; set; }
        public string fechaFinNombrada { get; set; }
        public string idPuerto { get; set; }
        public string idNave { get; set; }
        public string NaveDescrip { get; set; }
        public string idLocacion { get; set; }
        public string idTurno { get; set; }
        public string rutEmpresa { get; set; }

        public List<PerNombrada> PersonasNom { get; set; }

    }
}
