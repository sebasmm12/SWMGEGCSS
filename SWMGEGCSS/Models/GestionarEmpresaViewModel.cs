using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SWMGEGCSS_EN;
using System.Collections.Generic;

namespace SWMGEGCSS.Models
{
    public class GestionarEmpresaViewModel 
    {
        public T_empresa empresas { get; set; }
        public List<T_empresa> listempresas { get; set; }

        

        public IPagedList<T_empresa> listEmpresas
        { get; set; }
    }
}
