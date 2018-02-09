using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Linq;

namespace PruebasWebServiceTarea
{

    #region Entidades
    public class TareaWS
    {
        public string EmailUsuario { get; set; }
        public int IdTareaPorUsuario { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Descripcion { get; set; }
    }

    public class ResultadoWSCrear
    {
        public int IdTareaPorUsuario { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string FechaVencimiento { get; set; }
        public string IdAutor { get; set; }
    }

    public class ResultadoWSConsultar
    {
        public List<TareaWS> ListaTareas { get; set; }
    }
    #endregion
    /// <summary>
    /// Summary description for PruebasWebServiceTarea
    /// </summary>
    [TestClass]
    public class PruebasWebServiceTarea
    {
        public System.Web.Script.Serialization.JavaScriptSerializer Serializador { get; set; } = new System.Web.Script.Serialization.JavaScriptSerializer();

        public string GenerarToken(string nombreDeUsuario, string contraseña)
        {
            string tokenDeAcceso = "";
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                string tokenComoString = cliente.UploadString("http://localhost:55910/token", "POST", $"grant_type=password&username={nombreDeUsuario}&password={contraseña}");

                var tokenComoDictionary = Serializador.DeserializeObject(tokenComoString) as Dictionary<string, object>;

                if (tokenComoDictionary != null && tokenComoDictionary.ContainsKey("access_token"))
                {
                    tokenDeAcceso = tokenComoDictionary["access_token"] as string;
                }

                return tokenDeAcceso;
            }
        }

        [TestMethod]
        public void Test_Token()
        {
            string nombreDeUsuairo = "carlosalvaradofarfan@gmail.com", contraseña = "Admin1.";
            string tokenDeAcceso = GenerarToken(nombreDeUsuairo, contraseña);
            Assert.AreNotEqual(tokenDeAcceso, null);
        }

        [TestMethod]
        public void Test_Consultar()
        {
            string nombreDeUsuairo = "carlosalvaradofarfan@gmail.com", contraseña = "Admin1.";
            string tokenDeAcceso = GenerarToken(nombreDeUsuairo, contraseña);
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/json; charset=utf-8");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/consultar";

                var respuestaWS = cliente.DownloadString(url);

                var listado = Serializador.Deserialize<ResultadoWSConsultar>(respuestaWS);

                Assert.AreEqual(listado, null);

                Assert.IsTrue(listado.ListaTareas.Count > 0);
            }
        }

        [TestMethod]
        public void Test_Crear()
        {
            string nombreDeUsuairo = "carlosalvaradofarfan@gmail.com", contraseña = "Admin1.";
            string tokenDeAcceso = GenerarToken(nombreDeUsuairo, contraseña);
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/crear";
                var respuestaWS = cliente.UploadString(url, "POST", $"Descripcion=Descripción&Estado=true&FechaVencimiento=02/02/2018&IdAutor=1ba09536-6ab9-4e71-ba96-1f4745477106");
                var tareaCreada = Serializador.Deserialize<ResultadoWSCrear>(respuestaWS);

                Assert.IsTrue(tareaCreada.IdTareaPorUsuario > 0);
                Assert.AreEqual(tareaCreada.IdAutor, "1ba09536-6ab9-4e71-ba96-1f4745477106");
            }
        }

        [TestMethod]
        public void Test_Actualizar()
        {
            string nombreDeUsuairo = "carlosalvaradofarfan@gmail.com", contraseña = "Admin1.";
            string tokenDeAcceso = GenerarToken(nombreDeUsuairo, contraseña);
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/Actualizar";
                Int64 id = 2;
                string descripcion = DateTime.Now.ToString();
                var respuestaWS = cliente.UploadString(url, "POST", $"IdTareaPorUsuario={id}&Descripcion={descripcion}&Estado=true&FechaVencimiento=02/02/2018&IdAutor=1ba09536-6ab9-4e71-ba96-1f4745477106");
                var tareaCreada = Serializador.Deserialize<ResultadoWSCrear>(respuestaWS);

                Assert.AreEqual(tareaCreada.IdTareaPorUsuario, id);
                Assert.AreEqual(tareaCreada.Descripcion, descripcion);
            }
        }

        [TestMethod]
        public void Test_Borrar()
        {
            string nombreDeUsuairo = "carlosalvaradofarfan@gmail.com", contraseña = "Admin1.";
            string tokenDeAcceso = GenerarToken(nombreDeUsuairo, contraseña);

            ResultadoWSCrear tareaCreada = null;
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/crear";
                var respuestaWS = cliente.UploadString(url, "POST", $"Descripcion=Descripción&Estado=true&FechaVencimiento=02/02/2018&IdAutor=1ba09536-6ab9-4e71-ba96-1f4745477106");
                tareaCreada = Serializador.Deserialize<ResultadoWSCrear>(respuestaWS);
            }

            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/Borrar";
                Int64 id = tareaCreada.IdTareaPorUsuario;
                var respuestaWS = cliente.UploadString(url, "POST", $"IdTareaPorUsuario={id}");
                
            }

            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/json; charset=utf-8");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/consultar";

                var respuestaWS = cliente.DownloadString(url);

                var listado = Serializador.Deserialize<ResultadoWSConsultar>(respuestaWS);

                Assert.IsFalse(listado.ListaTareas.Any(item=>item.IdTareaPorUsuario == tareaCreada.IdTareaPorUsuario));
            }
        }
    }
}
