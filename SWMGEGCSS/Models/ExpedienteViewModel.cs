using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
namespace SWMGEGCSS.Models
{
    public class ExpedienteViewModel
    {
        public T_expediente_aux Expediente { get; set; }
        public List<T_expediente_aux> List_Expediente { get; set; }
    }
}