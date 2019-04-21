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
    public class ExpedienteDataAccess : BaseConexion
    {
        public List<T_expediente_aux> sp_Consultar_Lista_Proyectos()
        {
            List<T_expediente_aux> T_expediente = new List<T_expediente_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Proyectos"))
                {
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_expediente_aux t_aux = new T_expediente_aux();
                            t_aux.est_exp_nombre = DataUtil.DbValueToDefault<string>(reader["est_exp_nombre"]);
                            t_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_aux.exp_inicio= DataUtil.DbValueToDefault<DateTime>(reader["exp_inicio"]);
                            t_aux.exp_fin = DataUtil.DbValueToDefault<DateTime>(reader["exp_fin"]);
                            t_aux.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            t_aux.exp_ganancia= DataUtil.DbValueToDefault<double>(reader["exp_ganancia"]);
                            t_aux.exp_nombre = DataUtil.DbValueToDefault<string>(reader["exp_nombre"]);
                            T_expediente.Add(t_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return new List<T_expediente_aux>();
            }
            return T_expediente;
        }
        public List<T_expediente_aux> sp_Consultar_Lista_Tipo_Proyectos(int est_exp_id)
        {
            List<T_expediente_aux> T_expediente = new List<T_expediente_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Tipo_Proyectos"))
                {
                    Database.AddInParameter(command, "@est_exp_id", DbType.Int32, est_exp_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_expediente_aux t_aux = new T_expediente_aux();
                            t_aux.est_exp_nombre = DataUtil.DbValueToDefault<string>(reader["est_exp_nombre"]);
                            t_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_aux.exp_inicio = DataUtil.DbValueToDefault<DateTime>(reader["exp_inicio"]);
                            t_aux.exp_fin = DataUtil.DbValueToDefault<DateTime>(reader["exp_fin"]);
                            t_aux.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            t_aux.exp_ganancia = DataUtil.DbValueToDefault<double>(reader["exp_ganancia"]);
                            T_expediente.Add(t_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return new List<T_expediente_aux>();
            }
            return T_expediente;
        }
        public List<T_expediente_aux> sp_Consultar_Lista_Tipo_Proyectos_Nombre(string exp_nombre)
        {
            List<T_expediente_aux> T_expediente = new List<T_expediente_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Proyectos_Nombre"))
                {
                    Database.AddInParameter(command, "@exp_nombre", DbType.String, exp_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_expediente_aux t_aux = new T_expediente_aux();
                            t_aux.est_exp_nombre = DataUtil.DbValueToDefault<string>(reader["est_exp_nombre"]);
                            t_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_aux.exp_inicio = DataUtil.DbValueToDefault<DateTime>(reader["exp_inicio"]);
                            t_aux.exp_fin = DataUtil.DbValueToDefault<DateTime>(reader["exp_fin"]);
                            t_aux.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            t_aux.exp_ganancia = DataUtil.DbValueToDefault<double>(reader["exp_ganancia"]);
                            t_aux.exp_nombre = DataUtil.DbValueToDefault<string>(reader["exp_nombre"]);
                            T_expediente.Add(t_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return new List<T_expediente_aux>();
            }
            return T_expediente;
        }
        public List<T_plan> sp_Obtener_Planes()
        {
            List<T_plan> T_plan = new List<T_plan>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Obtener_Planes"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan plan = new T_plan();
                            plan.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            plan.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            plan.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            plan.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            plan.emp_id = DataUtil.DbValueToDefault<int>(reader["emp_id"]);
                            plan.plan_estado = DataUtil.DbValueToDefault<int>(reader["plan_estado"]);
                            plan.plan_costo = DataUtil.DbValueToDefault<double>(reader["plan_costo"]);
                            plan.plan_tipo = DataUtil.DbValueToDefault<int>(reader["plan_tipo"]);
                            plan.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            plan.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            T_plan.Add(plan);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return new List<T_plan>();
            }
            return T_plan;
        }
        public OperationResult sp_Insertar_Proyecto(T_expedientes Expediente)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Proyecto"))
                {
                    Database.AddInParameter(command, "@plan_id", DbType.Int32, Expediente.plan_id);
                    Database.AddInParameter(command, "@usu_creador", DbType.Int32, Expediente.usu_creador);
                    Database.AddInParameter(command, "@exp_inicio", DbType.Date, Expediente.exp_inicio);
                    Database.AddInParameter(command, "@exp_fin", DbType.Date, Expediente.exp_fin);
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, Expediente.tipo_servicio_id);
                    Database.AddInParameter(command, "@exp_ganancia", DbType.Double, Expediente.exp_ganancia);
                    Database.AddInParameter(command, "@exp_nombre", DbType.String, Expediente.exp_nombre);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception)
            {

                return new OperationResult();
            }
        }
    }
}
