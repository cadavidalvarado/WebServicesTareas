﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServiceTarea.Models;

namespace WebServiceTarea.Controllers
{ 
    [Authorize]
    [RoutePrefix("api/tareas")]
    public class TareasController : ApiController
    {
        [Route("Consultar")]
        public ConsultarOutBindingModel GetConsultar(ConsultarOutBindingModel argumentos)
        {
            return null;                
        }

        [Route("crear")]
        public CrearOutBindingModel PostCrear(CrearInBindingModel argumentos)
        {
            return null;
        }

        public ActualizarOutBindingModel PostActualizar(ActualizarInBindingModel argumentos)
        {
            return null;
        }

        public void PostBorrar(BorrarInBindingModel argumentos)
        {
            
        }
    }
}
