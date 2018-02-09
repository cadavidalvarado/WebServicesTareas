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

        public string EstadoTarea { get; set; }

        public string OrdenFecha { get; set; }
    }

    public class ConsultarOutBindingModel
    {
        public List<TareasBindingModel> ListaTareas { get; set; }
    }

    public class CrearInBindingModel
    {
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string FechaVencimiento { get; set; }
        public string IdAutor { get; set; }        
    }

    public class CrearOutBindingModel: CrearInBindingModel
    {
        public long IdTareaPorUsuario { get; set; }
    }

    public class ActualizarOutBindingModel: CrearOutBindingModel
    {

    }

    public class ActualizarInBindingModel: CrearOutBindingModel
    {

    }

    public class BorrarInBindingModel
    {
        public long IdTareaPorUsuario { get; set; }
    }
}