using MBSoftware.SistemaGestionDocumentaria.Entities;
using MBSoftware.SistemaGestionDocumentaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.BussinesLogic
{
    public class ParametroValorBl<TEntity> where TEntity : class, new()
    {
        private readonly IParametroValorRepository<TEntity> _repository;

        public ParametroValorBl()
        {     
            _repository = new ParametroValorRepository<TEntity>();
            
        }

        public IEnumerable<TEntity> Get(int codigoParametro, string estadoRegistro)
        {
            return _repository.Get(codigoParametro, estadoRegistro);
        }

        
    }
}
