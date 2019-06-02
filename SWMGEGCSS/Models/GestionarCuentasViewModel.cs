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
    public class GestionarCuentasViewModel
    {

        //LISTA PARA TODA LA PAGINACIÓN
        public T_usuario_cuentas_aux cuentas {get;set; }
        public List<T_usuario_cuentas_aux> list_cuentas { get; set; }
        public IPagedList<T_usuario_cuentas_aux> Plist_cuentas { get; set; }

        //LISTA PARA EL REGISTRAR


    }
}