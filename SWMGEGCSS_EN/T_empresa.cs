using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_EN
{
    public class T_empresa
    {
        public int emp_id { get; set; }
        public string emp_ruc { get; set; }
        public string emp_razon_social { get; set; }
        public string emp_sigla { get; set; }
        public string emp_representante { get; set; }
        public string emp_direccion { get; set; }
        public string emp_telefono { get; set; }
        public string emp_fax { get; set; }
        public string emp_email { get; set; }
        public bool emp_estado { get; set; }
        public int usu_codigo { get; set; }
    }
}
