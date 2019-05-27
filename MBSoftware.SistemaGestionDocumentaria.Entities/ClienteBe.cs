using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.Entities
{
    public class ClienteBe
    {
    

        public long ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
    }
}
