//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebServiceTarea.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TareasPorUsuario
    {
        public long IdTareaPorUsuario { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public System.DateTime FechaVencimiento { get; set; }
        public string IdAutor { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}