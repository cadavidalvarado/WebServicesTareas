using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceTarea.Models
{


    public enum OrdenFecha
    {
        Asc,
        Desc,
        SinOrden

    }

    public class TareasBindingModel
    {
        public string EmailUsuario { get; set; }
        public long IdTareaPorUsuario { get; set; }
        public bool Estado { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public string Descripcion { get; set; }
    }



    public class ConsultarInBindingModel
    {
        public string IdUsuario { get; set; }

        public bool EstadoTarea { get; set; }

        public string OrdenFecha { get; set; }
    }

    public class ConsultarOutBindingModel
    {
        public List<TareasBindingModel> ListaTareas { get; set; }
    }

    public class CrearInBindingModel
    {

    }

    public class CrearOutBindingModel
    {

    }

    public class ActualizarOutBindingModel
    {

    }

    public class ActualizarInBindingModel
    {

    }

    public class BorrarInBindingModel
    {

    }
}