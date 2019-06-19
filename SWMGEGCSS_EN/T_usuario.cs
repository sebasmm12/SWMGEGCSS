using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_usuario
    {
        public int usu_codigo { get; set; }
        public string usu_usuario { get; set; }
        public string usu_contraseña { get; set; }
        public bool estado { get; set; }
        public bool usu_trabajador { get; set; }
    }

    public class T_usuario_cuentas_aux
    {
        public int usu_codigo { get; set; }
        public string usu_usuario { get; set; }
        public string usu_contraseña { get; set; }
        public int usu_estado { get; set; }
        public int usu_trabajador { get; set; }
        public string det_usu_nombre { get; set; }
        public string det_usu_correo { get; set; }
        public string det_usu_direccion { get; set; }
        public string det_usu_telefono { get; set; }
        public string det_usu_sexo { set; get; }
        public int det_usu_tip_doc { get; set; }
        public string det_usu_tip_doc_numero { get; set; }
        public int tipo_det_usu_tipo { get; set; }
        public string det_usu_codigoColegio { get; set; }
        public string det_usu_especialidad {get;set;}
        public byte[] det_usu_imagem { get; set; }
        public string rol_nombre { get; set; }
        public int rol_codigo { get; set; }
    }

    

}
