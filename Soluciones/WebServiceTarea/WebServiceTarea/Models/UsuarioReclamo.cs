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
    
    public partial class UsuarioReclamo
    {
        public int IdUsuarioReclamo { get; set; }
        public string IdUsuario { get; set; }
        public string TipoReclamo { get; set; }
        public string Reclamo { get; set; }
        public string IdentityUser_Id { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
