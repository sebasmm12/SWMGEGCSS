using PagedList;
using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWMGEGCSS.Models
{
    public class TareasAsignadasViewModel
    {
        public T_actividades_desarrollar Actividad_a_Desarrollar { get; set; }

        public List<T_actividades_desarrollar> Lista_Actividades_a_Desarrollar { get; set; }

        public IPagedList<T_actividades_desarrollar> PLista_Actividades_a_Desarrollar { get; set; }

        public HttpPostedFile archivo { get; set; }

        public T_actividades_desarrollar_aux3 Actividad_aux3 { get; set; }
    }
}