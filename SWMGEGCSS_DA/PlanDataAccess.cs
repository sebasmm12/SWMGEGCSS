using Microsoft.Practices.EnterpriseLibrary.Data;
using SWMGEGCSS_DA.Base;
using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMGEGCSS_DA
{
    public class PlanDataAccess : BaseConexion
    {
        public List<T_plan> sp_Consultar_Lista_Plan()
        {
            List<T_plan> lista_planes = new List<T_plan>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Plan"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan t_plan = new T_plan();
                            t_plan.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_plan.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            t_plan.plan_estado = DataUtil.DbValueToDefault<int>(reader["plan_estado"]);
                            t_plan.plan_costo = DataUtil.DbValueToDefault<float>(reader["plan_costo"]);
                            t_plan.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            lista_planes.Add(t_plan);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan>();
            }
            return lista_planes;
        }
    }
}
