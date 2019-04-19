using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_detalle_usuario
    {
        public int usu_codigo { get; set; }
        public string det_usu_nombre { get; set; }
        public string det_usu_correo { get; set; }
        public string det_usu_direccion { get; set; }
        public string det_usu_telefono { get; set; }
        public string det_usu_sexo { get; set; }
        public int det_usu_tip_doc { get; set; }
        public string det_usu_tip_doc_numero { get; set; }
        public byte[] det_usu_imagem { get; set; }
        public int tipo_det_usu_tipo { get; set; }
        public string det_usu_codigoColegio { get; set; }
        public string det_usu_especialidad { get; set; }
    }
    public class Imagen
    {
        public byte[] Imagenes { get; set; }
    }
}
