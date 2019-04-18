using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_Citas
    {
        public int cita_id { get; set; }
        public string cita_representante { get; set; }
        public DateTime cita_fecha { get; set; }
        public int estado_cita_id { get; set; }
        public string cita_comentario { get; set; }
        public int usu_generado { get; set; }
        public int usu_citado { get; set; }
        public string cita_empresa { get; set; }
        public DateTime cita_fecha_atendido { get; set; }
        public string cita_correo { get; set; }
        public string cita_telefono { get; set; }
    }
}
