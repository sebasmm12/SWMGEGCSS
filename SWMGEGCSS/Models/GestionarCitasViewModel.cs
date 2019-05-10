using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SWMGEGCSS_EN;
using PagedList;
using SWMGEGCSS_EN;
using System.Collections.Generic;

namespace SWMGEGCSS.Models
{
    public class GestionarCitasViewModel
    {

        public T_Citas citas { get; set; }

        public List<T_Citas> listcitas { get; set; }

        public IPagedList<T_Citas> listCitas { get; set; }

       


    }
}