using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebServiceTarea.Models;

namespace WebServiceTarea.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private BDWebServiceEntities db = new BDWebServiceEntities();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create

        public string GenerardorContraseña()
        {
            try
            {
                Random r = new Random(DateTime.Now.Millisecond);
                int L1, L2, l1, l2;
                L1 = r.Next(65, 90);
                L2 = r.Next(65, 90);
                l1 = r.Next(97, 122);
                l2 = r.Next(97, 122);
                string PN = Convert.ToChar(L1).ToString() + Convert.ToChar(L2).ToString();
                string PA = Convert.ToChar(l1).ToString() + Convert.ToChar(l2).ToString();
                string PassAuto = (PN + PA + r.Next(99999) + "$").ToString();
                return PassAuto;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuarios usuario)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Usuarios usuarioT = new Usuarios();
                    usuarioT.Contrasena = GenerardorContraseña();
                    usuarioT.Correo = usuario.Correo;
                    usuarioT.EmailUsuario = usuario.EmailUsuario;
                    usuarioT.Telefono = usuario.Telefono;
                    usuarioT.EmailConfirmacion = usuario.EmailConfirmacion;
                    usuarioT.TelefonoConfirmacion = usuario.TelefonoConfirmacion;
                    usuarioT.Bloqueo = usuario.Bloqueo;
                    var manager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    var usuarioIdentity = new ApplicationUser() { UserName = usuario.EmailUsuario, Email = usuario.Correo, PhoneNumber = usuario.Telefono, EmailConfirmed = usuario.EmailConfirmacion, PhoneNumberConfirmed = usuario.TelefonoConfirmacion, LockoutEnabled = usuario.Bloqueo };
                    var resultadoOperacion = manager.Create(usuarioIdentity, usuarioT.Contrasena);
                    if (resultadoOperacion.Succeeded)
                    {
                        db.Usuarios.Add(usuarioT);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("EmailUsuario", "El nombre de usuario ya existe");
                        ModelState.AddModelError("Correo", "El Correo de usuario ya existe");
                        return View(usuario);
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return View(usuario);

        }
        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                Usuarios usuariosT = db.Usuarios.Find(usuarios.IdUsuario);
                usuariosT.Telefono = usuarios.Telefono;
                usuariosT.EmailUsuario = usuarios.EmailUsuario;
                usuariosT.EmailConfirmacion = usuarios.EmailConfirmacion;
                usuariosT.TelefonoConfirmacion = usuarios.TelefonoConfirmacion;
                usuariosT.Bloqueo = usuarios.Bloqueo;

                db.Entry(usuariosT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Attach(usuarios);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
