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
    public class EstadoExpedienteDataAccess:BaseConexion
    {
        public List<T_estado_expediente> sp_Consultar_Lista_Estado_Expediente()
        {
            List<T_estado_expediente> list_est_exp = new List<T_estado_expediente>();
            try
            {
                using (DbCommand command=Database.GetStoredProcCommand("sp_Consultar_Lista_Estado_Expediente"))
                {
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_estado_expediente est_exp = new T_estado_expediente();
                            est_exp.est_exp_id = DataUtil.DbValueToDefault<int>(reader["est_exp_id"]);
                            est_exp.est_exp_nombre = DataUtil.DbValueToDefault<string>(reader["est_exp_nombre"]);
                            est_exp.est_exp_descripcion = DataUtil.DbValueToDefault<string>(reader["est_exp_descripcion"]);
                            list_est_exp.Add(est_exp);
                        }
                    }
                }
                return list_est_exp;
            }
            catch (Exception)
            {

                return new List<T_estado_expediente>();
            }
        }
    }
}
