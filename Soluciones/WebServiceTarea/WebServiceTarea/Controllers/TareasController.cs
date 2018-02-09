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
                    var resultado = new ConsultarOutBindingModel();

                    if (argumentos.IdUsuario != null && argumentos.EstadoTarea != null && argumentos.OrdenFecha != null)
                    {
                        var resultadoBd = modelo.ConsultarTareasBD(argumentos.IdUsuario, Convert.ToBoolean(argumentos.EstadoTarea), argumentos.OrdenFecha).ToList();
                        resultado.ListaTareas = resultadoBd.Select(item => new TareasBindingModel()
                        {
                            Descripcion = item.Descripcion,
                            EmailUsuario = item.EmailUsuario,
                            Estado = item.Estado,
                            FechaVencimiento = item.FechaVencimiento,
                            IdTareaPorUsuario = item.IdTareaPorUsuario,
                        }).ToList();
                    }

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

                    if (argumentos.IdAutor != null && Convert.ToString(argumentos.Estado) != null && argumentos.FechaVencimiento != null)
                    {
                        var usuarioActual = User.Identity.Name;
                        var idUsuario = BDContexto.Usuarios.Where(item => item.EmailUsuario == usuarioActual).Select(item => item.IdUsuario).SingleOrDefault();

                        if (idUsuario != null)
                        {
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

                        else return null;
                    }

                    else return null;
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

                    if (Convert.ToString(argumentos.Estado) != null && argumentos.FechaVencimiento != null && argumentos.Descripcion != null)
                    {
                        var usuarioActual = User.Identity.Name;
                        var idUsuario = BDContexto.Usuarios.Where(item => item.EmailUsuario == usuarioActual).Select(item => item.IdUsuario).SingleOrDefault();

                        if (idUsuario != null)
                        {
                            var usuarioValido = BDContexto.TareasPorUsuario.Where(item => item.IdAutor == idUsuario && item.IdTareaPorUsuario == argumentos.IdTareaPorUsuario).SingleOrDefault();

                            if (usuarioValido != null)
                            {
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

                            else return null;
                        }

                        else return null;
                    }

                    else return null;
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
