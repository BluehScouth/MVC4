using MBSoftware.SistemaGestionDocumentaria.BussinesLogic;
using MBSoftware.SistemaGestionDocumentaria.Common;
using MBSoftware.SistemaGestionDocumentaria.Common.Helper;
using MBSoftware.SistemaGestionDocumentaria.Entities;
using MBSoftware.SistemaGestionDocumentaria.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MBSoftware.SistemaGestionDocumentaria.WebApplication.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteBl _clienteBl = new ClienteBl();
        private readonly ParametroValorBl<ParametroValorBe> _parametroValorBl;
        //
        // GET: /Cliente/

        public ClienteController()
        {
            _parametroValorBl = new ParametroValorBl<ParametroValorBe>();
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Cliente(string id)
        {
            ViewBag.clienteId = id;
            return View();
        }

        public JsonResult GetOneCliente(string id)
        {
            long clienteId = 0;
            long.TryParse(id, out clienteId);


            if (clienteId != 0)
            {
                var cliente = _clienteBl.Single(clienteId);
                return Json(cliente, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult GetMaestros([System.Web.Http.FromBody] int[] parametros)
        {
            List<object> results = new List<object>();

            foreach (var idParametro in parametros)
            {
                ParametroEnum parametroE = (ParametroEnum)idParametro;
                string stringValue = parametroE.ToString();
                results.Add(new { key = stringValue, data = _parametroValorBl.Get(idParametro, Constantes.Activo) });
            }

            return Json(results, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCliente()
        {         


            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

  
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
 
            var customerData = _clienteBl.Get();
  
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);
            }
            //Search    
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerData = customerData.Where(m => m.Nombre == searchValue);
            }

            //total number of rows count     
            recordsTotal = customerData.Count();
            //Paging     
            var data = customerData.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });  
               
        }

        [HttpPost]
        public JsonResult MantenerCliente([System.Web.Http.FromBody]ClienteModel clienteModel)
        {
            //TODO : Usar AutoMapper
            var cliente = new ClienteModel().ToCliente(clienteModel);


            if (cliente.ClienteId == 0)
                cliente = _clienteBl.Add(cliente);
            else
                cliente = _clienteBl.Update(cliente);
            return Json(cliente);
        }

    }
}
