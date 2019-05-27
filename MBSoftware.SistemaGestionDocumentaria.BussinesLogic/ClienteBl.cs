using MBSoftware.SistemaGestionDocumentaria.Entities;
using MBSoftware.SistemaGestionDocumentaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.BussinesLogic
{
    public class ClienteBl
    {
        private readonly IClienteRepository _repository;

        public ClienteBl() 
        {
            _repository = new ClienteRepository();
        }

        public IEnumerable<ClienteBe> Get() 
        {
            return _repository.Get();
        }

        public ClienteBe Single(long clienteId)
        {
            return _repository.Single(clienteId);
        }

        public ClienteBe Add(ClienteBe cliente) 
        {
         return  _repository.Insert(cliente);
        }

        public ClienteBe Update(ClienteBe cliente)
        {
            return _repository.Update(cliente);
        }
    }
}
