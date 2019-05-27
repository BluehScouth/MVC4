using MBSoftware.SistemaGestionDocumentaria.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MBSoftware.SistemaGestionDocumentaria.WebApplication.Models
{
    public class ClienteModel
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

        public ClienteBe ToCliente(ClienteModel clienteModel) 
        {
            ClienteBe cliente = new ClienteBe();
            cliente.ClienteId = clienteModel.ClienteId;
            cliente.Nombre = clienteModel.Nombre;
            cliente.Apellidos = clienteModel.Apellidos;
            cliente.Email = clienteModel.Email;
            cliente.Telefono = clienteModel.Telefono;
            cliente.Direccion = clienteModel.Direccion;
            cliente.CodigoPais = clienteModel.CodigoPais;
            cliente.CodigoDocumento = clienteModel.CodigoDocumento;
            cliente.NumeroDocumento = clienteModel.NumeroDocumento;

            return cliente;
        }
    }
}