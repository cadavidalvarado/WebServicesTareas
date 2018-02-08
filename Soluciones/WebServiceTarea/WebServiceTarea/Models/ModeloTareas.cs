﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebServiceTarea.Models;
using System.Data.SqlClient;

namespace WebServiceTarea.Models
{
    public class ModeloTareas
    {
        BDWebServiceEntities bd = new BDWebServiceEntities();

        public List<PraConsultarTareas_Result> ConsultarTareasBD(string IdAutor, string Estado, string OrdenFecha)
        {
            var tareas = bd.PraConsultarTareas(IdAutor, Convert.ToBoolean(Estado), OrdenFecha).ToList();

            return tareas;
        }

        public void CrearaTarea(TareasPorUsuario tarea)
        {
            var id = bd.PraCrearTarea(tarea.Descripcion, tarea.Estado, tarea.FechaVencimiento, tarea.IdAutor).SingleOrDefault();
            tarea.IdTareaPorUsuario = Convert.ToInt64(id);
        }

        public void ActualizarTarea(TareasPorUsuario tarea)
        {
            bd.PraActualizarTarea(tarea.IdTareaPorUsuario, tarea.Descripcion, tarea.Estado, tarea.FechaVencimiento, tarea.IdAutor);            
        }

        public void EliminarTarea(TareasPorUsuario tarea)
        {
            var id = bd.PraEliminarEventoProceso(tarea.IdTareaPorUsuario);           
        }



    }
}