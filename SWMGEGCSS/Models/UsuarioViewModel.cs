using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SWMGEGCSS_EN;
namespace SWMGEGCSS.Models
{
    public class UsuarioViewModel
    {
        public T_usuario Usuario { get; set; }

        public List<T_usuario> list_usuario { get; set; }

        public IPagedList<T_usuario> IPlist_Usuario { get; set; }

    }

    public class DetalleUsuarioViewModel
    {
        public T_detalle_usuario Detalle_Usuario { get; set; }

        public List<T_detalle_usuario> list_usuario { get; set; }

        public IPagedList<T_detalle_usuario> Lista_Usuario { get; set; }

    }
}