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
    
    public partial class UsuarioRol
    {
        public string IdUsuario { get; set; }
        public string IdRol { get; set; }
        public string IdentityUser_Id { get; set; }
    
        public virtual Roles Roles { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}