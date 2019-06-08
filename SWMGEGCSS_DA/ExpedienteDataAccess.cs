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
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_expediente_aux t_aux = new T_expediente_aux();
                            t_aux.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
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
                            t_aux.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
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
                            plan.emp_ruc = DataUtil.DbValueToDefault<string>(reader["emp_ruc"]);
                            plan.plan_estado = DataUtil.DbValueToDefault<int>(reader["plan_estado"]);
                            plan.plan_costo = DataUtil.DbValueToDefault<double>(reader["plan_costo"]);
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
        public OperationResult sp_Eliminar_Proyecto(int exp_id,string exp_comentario)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command=Database.GetStoredProcCommand("sp_Eliminar_Proyecto"))
                {
                    Database.AddInParameter(command, "@exp_id", DbType.Int32, exp_id);
                    Database.AddInParameter(command, "@exp_comentario", DbType.String, exp_comentario);
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
        public OperationResult sp_Insertar_Auditoria_Expediente(T_auditar_expedientes t_auditoria_exp)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command=Database.GetStoredProcCommand("sp_Insertar_Auditoria_Expediente"))
                {
                    Database.AddInParameter(command, "@exp_id", DbType.Int32, t_auditoria_exp.exp_id);
                    Database.AddInParameter(command, "@aud_exp_inicio", DbType.Date, t_auditoria_exp.aud_exp_inicio);
                    Database.AddInParameter(command, "@aud_exp_fin", DbType.Date, t_auditoria_exp.aud_exp_fin);
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, t_auditoria_exp.tipo_servicio_id);
                    Database.AddInParameter(command, "@aud_exp_ganancia", DbType.Double, t_auditoria_exp.aud_exp_ganancia);
                    Database.AddInParameter(command, "@aud_exp_comentario", DbType.String, t_auditoria_exp.aud_exp_comentario);
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
        public OperationResult sp_Actualizar_Datos_Expediente(T_expedientes t_exp)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Datos_Expediente"))
                {
                    Database.AddInParameter(command, "@exp_fin", DbType.Date, t_exp.exp_fin);
                    Database.AddInParameter(command, "@exp_inicio", DbType.Date, t_exp.exp_inicio);
                    Database.AddInParameter(command, "@exp_ganancia", DbType.Double, t_exp.exp_ganancia);
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
        public List<T_expedientes> sp_Consultar_Lista_Expedientes()
        {
            List<T_expedientes> list_expediente = new List<T_expedientes>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Expedientes"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_expedientes expediente = new T_expedientes();
                            expediente.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
                            expediente.est_exp_id = DataUtil.DbValueToDefault<int>(reader["est_exp_id"]);
                            expediente.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            expediente.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            expediente.exp_inicio = DataUtil.DbValueToDefault<DateTime>(reader["exp_inicio"]);
                            expediente.exp_fin = DataUtil.DbValueToDefault<DateTime>(reader["exp_fin"]);
                            expediente.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            expediente.exp_ganancia = DataUtil.DbValueToDefault<double>(reader["exp_ganancia"]);
                            expediente.exp_nombre = DataUtil.DbValueToDefault<string>(reader["exp_nombre"]);
                            list_expediente.Add(expediente);
                        }
                    }
                }
                return list_expediente;
            }
            catch (Exception)
            {

                return new List<T_expedientes>();
            }
           
            
        }
        public OperationResult sp_Insertar_Auditoria_Actividades_Desarrollar(T_actividades_desarrollar act_desarrollar)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command=Database.GetStoredProcCommand("sp_Insertar_Auditoria_Actividades_Desarrollar"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, act_desarrollar.act_desa_id);
                    Database.AddInParameter(command, "@audi_act_comentario", DbType.String, act_desarrollar.act_desa_comentario);
                    Database.AddInParameter(command, "@audi_act_nombre", DbType.String, act_desarrollar.act_desa_nombre);
                    Database.ExecuteScalar(command);
                    operationResult.NewId = 1;
                }
                return operationResult;
            }
            catch (Exception)
            {

                return new OperationResult();
            }
        }
        public OperationResult sp_Eliminar_Actividades_Desarrollar_Expediente(int act_desa_id)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command= Database.GetStoredProcCommand("sp_Eliminar_Actividades_Desarrollar_Expediente"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, act_desa_id);
                    Database.ExecuteScalar(command);
                    operationResult.NewId = 1;
                }
                return operationResult;
            }
            catch (Exception)
            {

                return new OperationResult();
            }
        }
        public double sp_Calcular_Suma_Cantidad_Actividades_Planeadas(int plan_id)
        {
            try
            {
                double costo = 0;
                using (DbCommand command=Database.GetStoredProcCommand("sp_Calcular_Suma_Cantidad_Actividades_Planeadas"))
                {
                    Database.AddInParameter(command, "@plan_id", DbType.Double, plan_id);
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            costo = DataUtil.DbValueToDefault<double>(reader["SUMA"]);
                        }
                    }
                }
                return costo;
            }
            catch (Exception)
            {

                return new double();
            }
        }

    }
}
