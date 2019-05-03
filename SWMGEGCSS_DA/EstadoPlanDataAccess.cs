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
    public class EstadoPlanDataAccess : BaseConexion
    {
        public List<T_plan_estado> sp_Consultar_Lista_Estado_Plan()
        {
            List<T_plan_estado> list_est_plan = new List<T_plan_estado>();
            try
            {//sp_Consultar_Lista_Estado_Plan
                using (DbCommand command = Database.GetStoredProcCommand("sp_lista_Plan_Estado"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_estado est_exp = new T_plan_estado();
                            est_exp.plan_estado_id = DataUtil.DbValueToDefault<int>(reader["plan_estado_id"]);
                            est_exp.plan_estado_nombre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            est_exp.plan_estado_descripcion = DataUtil.DbValueToDefault<string>(reader["plan_estado_descripcion"]);
                            list_est_plan.Add(est_exp);
                        }
                    }
                }
                return list_est_plan;
            }
            catch (Exception)
            {
                return new List<T_plan_estado>();
            }
        }
    }
}
