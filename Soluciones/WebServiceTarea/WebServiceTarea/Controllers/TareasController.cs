using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServiceTarea.Models;
using AutoMapper;

namespace WebServiceTarea.Controllers
{
    [Authorize]
    [RoutePrefix("api/tareas")]
    public class TareasController : ApiController
    {
        //Para crear el modelo: http://www.c-sharpcorner.com/article/using-stored-procedure-in-entity-framework-mvc/

        [Route("Consultar")]
        //public ConsultarOutBindingModel GetConsultar(ConsultarInBindingModel argumentos)        
        public ConsultarOutBindingModel GetConsultar(ConsultarInBindingModel argumentos)
        {
            using (var BDContexto = new BDWebServiceEntities())
            {
                var resultadoBd = BDContexto.PraConsultarTareas(argumentos.IdUsuario, argumentos.EstadoTarea, argumentos.OrdenFecha).ToList();
                var resultado = new ConsultarOutBindingModel();

                resultado.ListaTareas = resultadoBd.Select(item => new TareasBindingModel()
                {
                    Descripcion = item.Descripcion,
                    EmailUsuario = item.EmailUsuario,
                    Estado = item.Estado,
                    FechaVencimiento = item.FechaVencimiento,
                    IdTareaPorUsuario = item.IdTareaPorUsuario,
                }).ToList();

                return resultado;
            }

        }

        [Route("Crear")]
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
