using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace PruebasWebServiceTarea
{
    /// <summary>
    /// Summary description for PruebasWebServiceTarea
    /// </summary>
    [TestClass]
    public class PruebasWebServiceTarea
    {

        public string GenerarToken(string nombreDeUsuario, string contraseña)
        {
            string tokenDeAcceso = "";
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                string tokenComoString = cliente.UploadString("http://localhost:55910/token", "POST", $"grant_type=password&username={nombreDeUsuario}&password={contraseña}");

                var tokenComoDictionary = (new System.Web.Script.Serialization.JavaScriptSerializer()).DeserializeObject(tokenComoString) as Dictionary<string, object>;

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
                var texto = cliente.DownloadString(url);
                Assert.IsFalse(texto.Contains("<html>"));
            }
        }

        [TestMethod]
        public void Test_Crear()
        {
            string nombreDeUsuairo = "cadavid", contraseña = "!Qaz12";
            string tokenDeAcceso = GenerarToken(nombreDeUsuairo, contraseña);
            using (var cliente = new WebClient())
            {
                cliente.Headers.Add("Content-Type", "application/json; charset=utf-8");
                cliente.Headers.Add("Authorization", "Bearer " + tokenDeAcceso);
                string url = "http://localhost:55910/api/tareas/crear";
                var texto = cliente.UploadString(url, "POST", $"c=c&a=a&b=b");
                //var texto = cliente.DownloadString(url);
                //Assert.IsFalse(texto.Contains("<html>"));
            }
        }



        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }
    }
}
