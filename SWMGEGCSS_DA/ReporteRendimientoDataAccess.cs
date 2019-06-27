using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWMGEGCSS_EN;
using SWMGEGCSS_DA.Base;
using System.Data;
using System.Data.Common;
namespace SWMGEGCSS_DA

{
    public class ReporteRendimientoDataAccess : BaseConexion
    {
        public List<T_reporte_rendimientoAUX> sp_Consultar_Lista_Reporte_Rendimiento()
        {
            var l_reporte = new List<T_reporte_rendimientoAUX>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Reporte_Rendimiento"))
                {

                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var reporte = new T_reporte_rendimientoAUX();
                            reporte.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            reporte.det_usu_nombre = DataUtil.DbValueToDefault<String>(reader["det_usu_nombre"]);
                            reporte.rep_ren_act_completadas = DataUtil.DbValueToDefault<int>(reader["rep_ren_act_completadas"]);
                            reporte.rep_ren_act_obs_x_error = DataUtil.DbValueToDefault<int>(reader["rep_ren_act_obs_x_error"]);
                            reporte.rep_ren_atraso = DataUtil.DbValueToDefault<int>(reader["rep_ren_atraso"]);
                            reporte.rep_ren_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["rep_ren_fecha_fin"]);
                            reporte.rep_ren_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["rep_ren_fecha_inicio"]);
                            reporte.rep_ren_id = DataUtil.DbValueToDefault<int>(reader["rep_ren_id"]);
                            l_reporte.Add(reporte);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return new List<T_reporte_rendimientoAUX>();
            }
            return l_reporte;
        }

    }
}
