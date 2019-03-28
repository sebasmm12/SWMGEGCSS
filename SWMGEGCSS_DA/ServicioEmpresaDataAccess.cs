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
    public class ServicioEmpresaDataAccess : BaseConexion
    {
        public T_servicio_empresa sp_Ultimo_Servicio_Empresa(int emp_id)
        {
            var model = new T_servicio_empresa();
            try
            {
                
                using (DbCommand command=Database.GetStoredProcCommand("sp_Ultimo_Servicio_Empresa"))
                {
                    Database.AddInParameter(command, "@emp_id", DbType.Int32, emp_id);
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            model.ser_emp_id = DataUtil.DbValueToDefault<int>(reader["ser_emp_id"]);
                            model.ser_id = DataUtil.DbValueToDefault<int>(reader["ser_id"]);
                            model.ser_est = DataUtil.DbValueToDefault<int>(reader["ser_est"]);
                            model.emp_id = DataUtil.DbValueToDefault<int>(reader["emp_id"]);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return new T_servicio_empresa();
            }
            return model;
        }
    }
}
