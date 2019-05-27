﻿using EDOSwit.AbstraccionSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBSoftware.SistemaGestionDocumentaria.Repository
{
    public interface IParametroValorRepository<TEntity> 
    {
         IEnumerable<TEntity> Get(int codigoParametro, string estadoRegistro);
       
    }
}