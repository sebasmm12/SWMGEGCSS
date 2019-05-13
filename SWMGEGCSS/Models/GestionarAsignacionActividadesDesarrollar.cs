using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using SWMGEGCSS_DA.Base;
using System.Data;
using System.Data.Common;
using PagedList;

namespace SWMGEGCSS.Models
{
    public class GestionarAsignacionActividadesDesarrollar
    {
        public T_actividades_desarrollar actividadesDesarrollar { get; set; }
        public T_actividades_desarrollar_aux actividadesDesarrollarAux { get; set; }
        public List<T_actividades_desarrollar> listActividadesDesarrollar { get; set; }
        public IPagedList<T_actividades_desarrollar> listPagedActividadesDesarrollar { get; set; }
        public List<T_actividades_desarrollar_aux> listActividadesDesarrollarAux { get; set; }
        
        public List<T_usuario> listUsuarios { get; set; }
        public List<T_detalle_usuario> listDetalleUsuario { get; set; }
        public T_detalle_usuario detalle_Usuario { get; set; }
        public T_rol_usuario_Aux rol_usuario_aux { get; set; }
        public T_expedientes expedienteActDesarrollar {get; set;}
    }
}