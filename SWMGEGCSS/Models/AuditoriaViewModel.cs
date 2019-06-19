using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using PagedList;
namespace SWMGEGCSS.Models
{
    public class AuditoriaViewModel
    {
        public T_auditar_expedientes auditoria_expediente { get; set; }
        public List<T_auditar_expedientes> list_auditoria_expediente { get; set; }
        public List<T_actividades_desarrollar> list_actividades_desarrollar { get; set; }
        public IPagedList<T_auditoria_actividades_desarrollo> Plist_auditoria_actividades_desarrollar { get; set; }
        public List<T_aux_auditar_expedientes> list_aux_auditoria_expediente { get; set; }
        public IPagedList<T_auditar_expedientes> Plist_auditoria_expediente { get; set; }
        public IPagedList<T_aux_auditar_expedientes> Plist_aux_auditoria_expediente { get; set; }
    }
}