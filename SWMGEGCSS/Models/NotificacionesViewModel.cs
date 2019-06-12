using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
namespace SWMGEGCSS.Models
{
    public class NotificacionesViewModel
    {
        public T_notificaciones notificacion {get;set;}
        public List<T_notificaciones> list_notificaciones { get; set; }
        public IPagedList<T_notificaciones> Plist_notificaciones { get; set; }
    }
}