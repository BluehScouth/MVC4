using EDOSwit.AbstraccionSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.Repository
{
    public class ParametroValorRepository<TEntity> : BaseRepository<TEntity>, IParametroValorRepository<TEntity>
           where TEntity : class, new()
    {
        public IEnumerable<TEntity> Get(int codigoParametro,string estadoRegistro)
        {
            return Get("Usp_select_ParametroValor",codigoParametro,estadoRegistro);
        }

       
    }
}
