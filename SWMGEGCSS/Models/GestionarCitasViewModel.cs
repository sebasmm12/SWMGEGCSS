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
        
        public T_Citas_aux Citas { get; set; }

        public List<T_Citas_aux> listcitas { get; set; }

        public IPagedList<T_Citas_aux> listCitas { get; set; }

        public UsuarioViewModel UsuarioModel { get; set; }
       


    }
}