using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
namespace SWMGEGCSS.Models
{
    public class PermisoViewModel
    {
        public T_permiso Permiso { get; set; }
        public List<T_permiso> List_Permisos { get; set; }
    }
}