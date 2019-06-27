using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using PagedList;
using System.Collections.Generic;
namespace SWMGEGCSS.Models
{
    public class Visualizar_Servicios
    {
        public T_tipo_servicio Tipo_Servicio { get; set; }
        public List<T_tipo_servicio> list_tipo_servicio { get; set; }
    }
}