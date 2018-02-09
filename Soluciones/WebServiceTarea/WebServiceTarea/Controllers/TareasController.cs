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
        [Route("Consultar")]        
        public ConsultarOutBindingModel GetConsultar(ConsultarInBindingModel argumentos)
        {
            try
            {
                argumentos = argumentos ?? new ConsultarInBindingModel();

                using (var BDContexto = new BDWebServiceEntities())
                {
                    var modelo = new ModeloTareas(BDContexto);
                    var resultadoBd = modelo.ConsultarTareasBD(argumentos.IdUsuario, Convert.ToBoolean(argumentos.EstadoTarea), argumentos.OrdenFecha).ToList();
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
            catch (Exception)
            {

                throw;
            }
           

        }

        [Route("Crear")]
        public CrearOutBindingModel PostCrear([FromBody]CrearInBindingModel argumentos)
        {
            try
            {
                using (var BDContexto = new BDWebServiceEntities())
                {
                    var modelo = new ModeloTareas(BDContexto);

                    var entidadBD = new TareasPorUsuario()
                    {
                        Descripcion = argumentos.Descripcion,
                        Estado = argumentos.Estado,
                        FechaVencimiento = Convert.ToDateTime(argumentos.FechaVencimiento),
                        IdAutor = argumentos.IdAutor,
                    };

                    modelo.CrearaTarea(entidadBD);

                    return new CrearOutBindingModel()
                    {
                        IdTareaPorUsuario = entidadBD.IdTareaPorUsuario,
                        Descripcion = entidadBD.Descripcion,
                        Estado = entidadBD.Estado,
                        FechaVencimiento = Convert.ToString(entidadBD.FechaVencimiento),
                        IdAutor = entidadBD.IdAutor,
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }

            
        }

        [Route("Actualizar")]
        public ActualizarOutBindingModel PostActualizar([FromBody]ActualizarInBindingModel argumentos)
        {
            try
            {
                using (var BDContexto = new BDWebServiceEntities())
                {
                    var modelo = new ModeloTareas(BDContexto);

                    var entidadBD = new TareasPorUsuario()
                    {
                        IdTareaPorUsuario = argumentos.IdTareaPorUsuario,
                        Descripcion = argumentos.Descripcion,
                        Estado = argumentos.Estado,
                        FechaVencimiento = Convert.ToDateTime(argumentos.FechaVencimiento),
                        IdAutor = argumentos.IdAutor,
                    };

                    modelo.ActualizarTarea(entidadBD);

                    return new ActualizarOutBindingModel()
                    {
                        IdTareaPorUsuario = entidadBD.IdTareaPorUsuario,
                        Descripcion = entidadBD.Descripcion,
                        Estado = entidadBD.Estado,
                        FechaVencimiento = Convert.ToString(entidadBD.FechaVencimiento),
                        IdAutor = entidadBD.IdAutor,
                    };
                }

            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public void PostBorrar([FromBody]BorrarInBindingModel argumentos)
        {
            try
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
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
