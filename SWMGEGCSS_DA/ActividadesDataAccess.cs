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
    public class ActividadesDataAccess : BaseConexion
    {
        public List<T_actividades> sp_Consultar_Actividades_Diferentes_Plan(int tipo_servicio_id)
        {
            try
            {
                List<T_actividades> list_actividades = new List<T_actividades>();
                using (DbCommand command=Database.GetStoredProcCommand("sp_Consultar_Actividades_Diferentes_Plan"))
                {
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        T_actividades t_actividad = new T_actividades();
                        t_actividad.act_id = DataUtil.DbValueToDefault<int>(reader["act_id"]);
                        t_actividad.act_nombre = DataUtil.DbValueToDefault<string>(reader["act_nombre"]);
                        list_actividades.Add(t_actividad);
                    }
                }
                return list_actividades;
            }
            catch (Exception)
            {

                return new List<T_actividades>();
            }
        }
    }
}
