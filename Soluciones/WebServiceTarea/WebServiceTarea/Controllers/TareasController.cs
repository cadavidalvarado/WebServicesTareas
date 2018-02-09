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
            argumentos = argumentos ?? new ConsultarInBindingModel();

            using (var BDContexto = new BDWebServiceEntities())
            {
                var modelo = new ModeloTareas(BDContexto);
                var resultadoBd = modelo.ConsultarTareasBD(argumentos.IdUsuario, argumentos.EstadoTarea, argumentos.OrdenFecha).ToList();
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
            using (var BDContexto = new BDWebServiceEntities())
            {
                var modelo = new ModeloTareas(BDContexto);

                var entidadBD = new TareasPorUsuario()
                {
                    Descripcion = argumentos.Descripcion,
                    Estado = argumentos.Estado,
                    FechaVencimiento = argumentos.FechaVencimiento,
                    IdAutor = argumentos.IdAutor,
                };

                modelo.CrearaTarea(entidadBD);

                return new CrearOutBindingModel()
                {
                    IdTareaPorUsuario = entidadBD.IdTareaPorUsuario,
                    Descripcion = entidadBD.Descripcion,
                    Estado = entidadBD.Estado,
                    FechaVencimiento = entidadBD.FechaVencimiento,
                    IdAutor = entidadBD.IdAutor,
                };
            }
        }

        [Route("Actualizar")]
        public ActualizarOutBindingModel PostActualizar(ActualizarInBindingModel argumentos)
        {
            using (var BDContexto = new BDWebServiceEntities())
            {
                var modelo = new ModeloTareas(BDContexto);

                var entidadBD = new TareasPorUsuario()
                {
                    IdTareaPorUsuario = argumentos.IdTareaPorUsuario,
                    Descripcion = argumentos.Descripcion,
                    Estado = argumentos.Estado,
                    FechaVencimiento = argumentos.FechaVencimiento,
                    IdAutor = argumentos.IdAutor,
                };

                modelo.ActualizarTarea(entidadBD);

                return new ActualizarOutBindingModel()
                {
                    IdTareaPorUsuario = entidadBD.IdTareaPorUsuario,
                    Descripcion = entidadBD.Descripcion,
                    Estado = entidadBD.Estado,
                    FechaVencimiento = entidadBD.FechaVencimiento,
                    IdAutor = entidadBD.IdAutor,
                }; 
            }
        }

        public void PostBorrar(BorrarInBindingModel argumentos)
        {
            using (var BDContexto = new BDWebServiceEntities())
            {
                var modelo = new ModeloTareas(BDContexto);
                var entidadBD = new TareasPorUsuario()
                {
                    IdTareaPorUsuario = argumentos.IdTareaPorUsuario,                   
                };

                modelo.EliminarTarea(entidadBD);
            }
        }
    }
}
