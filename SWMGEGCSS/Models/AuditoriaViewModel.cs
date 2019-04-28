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
        public IPagedList<T_auditar_expedientes> Plist_auditoria_expediente { get; set; }
    }
}