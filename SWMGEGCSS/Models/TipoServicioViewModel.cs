using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace SWMGEGCSS.Models
{
    public class TipoServicioViewModel
    {
        public T_tipo_servicio tipoServicio { get; set; }
        public List<T_tipo_servicio> ListtipoServicio { get; set; }

        public List<T_tipo_servicio_actividades> list_tipo_servicio_actividades { get; set; }
        public IPagedList<T_tipo_servicio> PList_tipo_servicio { get; set; }

        public string tipo_servicio_estado { get; set; }
    }
}