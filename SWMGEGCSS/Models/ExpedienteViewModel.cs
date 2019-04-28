using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using PagedList;
namespace SWMGEGCSS.Models
{
    public class ExpedienteViewModel
    {
        public T_expediente_aux Expediente { get; set; }
        public List<T_expediente_aux> List_Expediente { get; set; }

        public IPagedList<T_expediente_aux> PList_Expedientes { get; set; }
        public T_expedientes Expedientes { get; set; }
    }
}