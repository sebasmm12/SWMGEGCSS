using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SWMGEGCSS_EN;
using System.Collections.Generic;

namespace SWMGEGCSS.Models
{
    public class Gestionar_I_EViewModel
    {

        public T_ingresos_egresos ingresos_egresos { get; set; }
        public List<T_ingresos_egresos> lista_ingresos_egresos { get; set; }
        public IPagedList<T_ingresos_egresos> list_ingresos_egresos
        { get; set; }
        public List<T_ingresos_egresos_aux> lista_ingresos_egresos_aux { get; set; }
        public string tipo_estado { get; set; }
        public T_reporte_fechas reporteFechas { get; set; }

    }
}