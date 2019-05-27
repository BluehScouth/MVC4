using MBSoftware.SistemaGestionDocumentaria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.Repository
{
    public interface IClienteRepository
    {
         IEnumerable<ClienteBe> Get();
         ClienteBe Insert(ClienteBe cliente);
         ClienteBe Update(ClienteBe cliente);
         ClienteBe Single(long idCliente);
    }
}
