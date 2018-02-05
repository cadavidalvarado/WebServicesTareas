using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceTarea.Models
{

    public class TareasBindingModel
    {

    }

    public class ConsultarInBindingModel
    {
        public IEnumerable<TareasBindingModel> tareas { get; set; }
    }

    public class ConsultarOutBindingModel
    {
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