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

                using (DbCommand command = Database.GetStoredProcCommand("sp_Ultimo_Servicio_Empresa"))
                {
                    Database.AddInParameter(command, "@emp_id", DbType.Int32, emp_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
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


        public List<T_tipo_servicio> sp_Consultar_Servicios()
        {
            var l_servicios = new List<T_tipo_servicio>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Servicios"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_tipo_servicio model = new T_tipo_servicio();
                            model.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            model.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            model.tipo_servicio_descripcion = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_descripcion"]);
                            model.tipo_servicio_estado = DataUtil.DbValueToDefault<bool>(reader["tipo_servicio_estado"]);
                            l_servicios.Add(model);
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e + "");
                return new List<T_tipo_servicio>();
            }
            return l_servicios;
        }
    }
}

