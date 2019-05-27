using EDOSwit.AbstraccionSP;
using MBSoftware.SistemaGestionDocumentaria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.Repository
{
    public class ClienteRepository : BaseRepository<ClienteBe>, IClienteRepository
    {
        public IEnumerable<ClienteBe> Get()
        {
            return Get("usp_select_cliente");
        }

        public ClienteBe Insert(ClienteBe cliente)
        {
          return  Add("usp_insert_cliente",cliente);
        }

        public ClienteBe  Update(ClienteBe cliente)
        {
            return Update("usp_update_cliente", cliente);
        }

        public ClienteBe Single(long idCliente)
        {
            return Single("usp_single_cliente",idCliente);
        }
    }
}
