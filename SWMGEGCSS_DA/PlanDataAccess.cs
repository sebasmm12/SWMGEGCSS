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
        public List<T_plan> sp_Consultar_Lista_Tipo_Nombre_Planes(string plan_nombre)
        {
            List<T_plan> T_Plan = new List<T_plan>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Tipo_Nombre_Planes"))
                {
                    Database.AddInParameter(command, "@plan_nombre", DbType.String, plan_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan t_plan = new T_plan();
                            t_plan.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_plan.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            t_plan.plan_estado = DataUtil.DbValueToDefault<int>(reader["plan_estado"]);
                            t_plan.plan_costo = DataUtil.DbValueToDefault<double>(reader["plan_costo"]);
                            t_plan.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            T_Plan.Add(t_plan);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan>();
            }
            return T_Plan;
        }
        public OperationResult sp_Actualizar_Plan(T_plan Plan)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Plan"))
                {
                    Database.AddInParameter(command, "@plan_id", DbType.Int32, Plan.plan_id);
                    Database.AddInParameter(command, "@plan_nombre", DbType.String, Plan.plan_nombre);
                    Database.AddInParameter(command, "@plan_fecha", DbType.Date, Plan.plan_fecha);
                    Database.AddInParameter(command, "@plan_estado", DbType.Int32, Plan.plan_estado);
                    Database.AddInParameter(command, "@plan_costo", DbType.Double, Plan.plan_costo);
                    Database.AddInParameter(command, "@plan_tipo", DbType.Int32, Plan.plan_tipo);
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, Plan.tipo_servicio_id);
                    Database.AddInParameter(command, "@plan_tiempo", DbType.Int32, Plan.plan_tiempo);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception)
            {
                return new OperationResult();
            }
        }
        public List<T_tipo_servicio> sp_Obtener_Lista_Servicios(T_servicio servicio)
        {
            List<T_tipo_servicio> T_Tipo_Servicios = new List<T_tipo_servicio>();

            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Tipo_Servicio"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_tipo_servicio tipo_servicio = new T_tipo_servicio();
                            tipo_servicio.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            tipo_servicio.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servico_nombre"]);
                            T_Tipo_Servicios.Add(tipo_servicio);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_tipo_servicio>();
            }
            return T_Tipo_Servicios;
        }
    }
}
