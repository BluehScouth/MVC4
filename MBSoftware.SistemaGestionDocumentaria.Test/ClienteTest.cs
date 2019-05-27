using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBSoftware.SistemaGestionDocumentaria.BussinesLogic;
using MBSoftware.SistemaGestionDocumentaria.Entities;

namespace MBSoftware.SistemaGestionDocumentaria.Test
{
    [TestClass]
    public class ClienteTest
    {
        [TestMethod]
        public void Get()
        {
          ClienteBl _clienteBl = new ClienteBl();
          var clientes =   _clienteBl.Get();
        }

        [TestMethod]
        public void Add()
        {
            ClienteBl _clienteBl = new ClienteBl();
            ClienteBe cliente = new ClienteBe();
            cliente.Nombre = "Rene";
            cliente.Apellidos = "Cardenas";
            cliente.Direccion = "La Victoria";
            cliente.Email = "rene@gmail.com";
            cliente.Telefono = "999235144";
            _clienteBl.Add(cliente);
        }
    }
}
