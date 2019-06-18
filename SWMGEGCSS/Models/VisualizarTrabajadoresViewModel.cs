using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace SWMGEGCSS.Models
{
    public class VisualizarTrabajadoresViewModel
    {
        public T_actividades_desarrollar_usuarios actividades_usuario { get; set; }
        public List<T_actividades_desarrollar_usuarios> list_actividades_usuario { get; set; }
        public IPagedList<T_actividades_desarrollar_usuarios> IPlist_actividades_usuario { get; set; }

    }
}