using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWMGEGCSS.Models
{
    public class TipoServicioViewModel
    {
        public T_tipo_servicio tipoServicio { get; set; }
        public List<T_tipo_servicio> ListtipoServicio { get; set; }
    }
}