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
        public OperationResult sp_Agregar_Plan(T_plan Plan)/*sp_Agregar_Plan*/
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Agregar_Plan"))
                {
                    Database.AddInParameter(command, "@plan_nombre", DbType.String, Plan.plan_nombre);
                    Database.AddInParameter(command, "@plan_fecha", DbType.Date, Plan.plan_fecha);
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, Plan.usu_codigo);
                    Database.AddInParameter(command, "@emp_ruc", DbType.String, Plan.emp_ruc);
                    //Database.AddInParameter(command, "@plan_costo", DbType.Decimal, Plan.plan_costo);
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, Plan.tipo_servicio_id);
                    //Database.AddInParameter(command, "@plan_tiempo", DbType.Int32, Plan.plan_tiempo);
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
        public List<T_plan_aux> sp_Consultar_Lista_Plan()
        {
            List<T_plan_aux> lista_planes_aux = new List<T_plan_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_plan"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_aux t_plan_aux = new T_plan_aux();
                            t_plan_aux.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            t_plan_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_plan_aux.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            t_plan_aux.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            t_plan_aux.emp_razon_social = DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);
                            t_plan_aux.plan_estado_nobre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            t_plan_aux.plan_costo = DataUtil.DbValueToDefault<float>(reader["plan_costo"]);
                            t_plan_aux.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            t_plan_aux.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            lista_planes_aux.Add(t_plan_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan_aux>();
            }
            return lista_planes_aux;
        }
        public List<T_plan_aux> sp_Consultar_Lista_Tipo_Nombre_Planes(string plan_nombre)
        {
            List<T_plan_aux> T_Plan = new List<T_plan_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Tipo_Nombre_Planes"))
                {
                    Database.AddInParameter(command, "@plan_nombre", DbType.String, plan_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_aux t_plan_aux = new T_plan_aux();
                            t_plan_aux.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            t_plan_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);                            
                            t_plan_aux.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            t_plan_aux.plan_estado_nobre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            t_plan_aux.plan_costo = DataUtil.DbValueToDefault<double>(reader["plan_costo"]);
                            t_plan_aux.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            t_plan_aux.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            T_Plan.Add(t_plan_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan_aux>();
            }
            return T_Plan;
        }
        public OperationResult sp_Actualizar_Plan(T_plan Plan)
        {
            /*try
            {*/
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Plan"))
                {
                        Database.AddInParameter(command, "@plan_id", DbType.Int32, Plan.plan_id);
                        Database.AddInParameter(command, "@plan_nombre", DbType.String, Plan.plan_nombre);
                        Database.AddInParameter(command, "@plan_fecha", DbType.Date, Plan.plan_fecha);
                        Database.AddInParameter(command, "@emp_ruc", DbType.String, Plan.emp_ruc);
                        Database.AddInParameter(command, "@plan_estado", DbType.Int32, Plan.plan_estado);
                        Database.AddInParameter(command, "@plan_costo", DbType.Double, Plan.plan_costo);
                        Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, Plan.tipo_servicio_id);
                        Database.AddInParameter(command, "@plan_tiempo", DbType.Int32, Plan.plan_tiempo);
                        Database.ExecuteScalar(command);
                        operation.NewId = 1;
                }
                return operation;
            /*}
            catch (Exception)
            {
                return new OperationResult();
            }*/
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
        /*para autocompletado*/
        public List<T_tipo_servicio> sp_Consultar_Lista_Nombre_Tipo_Servicio(string tipo_servicio_nombre)
        {
            List<T_tipo_servicio> T_Tipo_Servicio = new List<T_tipo_servicio>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Nombre_Tipo_Servicio"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_nombre", DbType.String, tipo_servicio_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_tipo_servicio t_servicio = new T_tipo_servicio();
                            t_servicio.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            t_servicio.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);                     
                            T_Tipo_Servicio.Add(t_servicio);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_tipo_servicio>();
            }
            return T_Tipo_Servicio;
        }
        /*para autocompletado*/
        public List<T_empresa> sp_Consultar_Lista_Nombre_Empresa(string emp_razon_social)
        {
            List<T_empresa> T_Empresa = new List<T_empresa>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Nombre_Empresa"))
                {
                    Database.AddInParameter(command, "@emp_nombre", DbType.String, emp_razon_social);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_empresa t_empresa = new T_empresa();
                            //t_empresa.emp_id = DataUtil.DbValueToDefault<int>(reader["emp_id"]);
                            t_empresa.emp_razon_social = DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);
                            T_Empresa.Add(t_empresa);
                        }
                    }
                }
            }
            catch(Exception)
            {
                return new List<T_empresa>();
            }
            return T_Empresa;
        }
        public OperationResult sp_Cancelar_Plan(int plan_id, string plan_comentario)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Cancelar_Plan"))
                {
                    Database.AddInParameter(command, "@plan_id", DbType.String, plan_id);
                    Database.AddInParameter(command, "@plan_comentario", DbType.String, plan_comentario);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                    //execute scalar permite ejecutar el comando
                }
                    return operation;
            }
            catch (Exception)
            {
                   return new OperationResult();
            }
        }
        public T_plan sp_Consultar_Plan_Por_Nombre(string plan_nombre)
        {
            T_plan T_Plan = new T_plan();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Obtener_Plan"))
                {
                    Database.AddInParameter(command, "@plan_nombre", DbType.String, plan_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        T_Plan.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                        T_Plan.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                        T_Plan.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                        T_Plan.emp_ruc = DataUtil.DbValueToDefault<string>(reader["emp_ruc"]);
                        T_Plan.plan_estado= DataUtil.DbValueToDefault<int>(reader["plan_estado"]);
                        //T_Plan.plan_costo = DataUtil.DbValueToDefault<double>(reader["plan_costo"]);
                        T_Plan.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                        //T_Plan.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                    }
                }
            }
            catch (Exception)
            {
                return new T_plan();
            }
            return T_Plan;
        }
        /*para autocompletado*/
        public List<T_plan_estado> Sp_Consultar_Lista_Estado_Plan(string plan_estado_nombre)
        {
            List<T_plan_estado> T_Plan_Estado = new List<T_plan_estado>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Plan_Estado"))
                {
                    Database.AddInParameter(command, "@plan_estado_nombre", DbType.String, plan_estado_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_estado t_plan_estado = new T_plan_estado();
                            t_plan_estado.plan_estado_id = DataUtil.DbValueToDefault<int>(reader["plan_estado_id"]);
                            t_plan_estado.plan_estado_nombre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            T_Plan_Estado.Add(t_plan_estado);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan_estado>();
            }
            return T_Plan_Estado;
        }
        public List<T_tipo_servicio> sp_Consultar_Lista_Tipo_Servicio()
        {
            List<T_tipo_servicio> lista_tipo_servicio = new List<T_tipo_servicio>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Tipo_Servicio"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_tipo_servicio t_servicio = new T_tipo_servicio();
                            t_servicio.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            t_servicio.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            lista_tipo_servicio.Add(t_servicio);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_tipo_servicio>();
            }
            return lista_tipo_servicio;
        }

        public List<T_empresa> sp_Consultar_Lista_Empresa()
        {
            List<T_empresa> lista_empresa = new List<T_empresa>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_empresa"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_empresa t_empresa = new T_empresa();
                            //t_empresa.emp_id = DataUtil.DbValueToDefault<int>(reader["emp_id"]);
                            t_empresa.emp_ruc = DataUtil.DbValueToDefault<string>(reader["emp_ruc"]);
                            t_empresa.emp_razon_social = DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);
                            t_empresa.emp_sigla = DataUtil.DbValueToDefault<string>(reader["emp_sigla"]);
                            t_empresa.emp_representante = DataUtil.DbValueToDefault<string>(reader["emp_representante"]);
                            t_empresa.emp_direccion = DataUtil.DbValueToDefault<string>(reader["emp_direccion"]);
                            t_empresa.emp_telefono = DataUtil.DbValueToDefault<string>(reader["emp_telefono"]);
                            t_empresa.emp_fax= DataUtil.DbValueToDefault<string>(reader["emp_fax"]);
                            t_empresa.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            lista_empresa.Add(t_empresa);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_empresa>();
            }
            return lista_empresa;
        }
        public List<T_plan_estado> sp_Consultar_Lista_Plan_Estado()
        {
            List<T_plan_estado> lista_plan_estado = new List<T_plan_estado>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_plan_estado"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_estado t_plan_estado = new T_plan_estado();
                            t_plan_estado.plan_estado_id = DataUtil.DbValueToDefault<int>(reader["plan_estado_id"]);
                            t_plan_estado.plan_estado_nombre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            t_plan_estado.plan_estado_descripcion = DataUtil.DbValueToDefault<string>(reader["plan_estado_descripcion"]);
                            lista_plan_estado.Add(t_plan_estado);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan_estado>();
            }
            return lista_plan_estado;
        }
        public List<T_plan_aux> sp_Consultar_Lista_Plan_Aux()
        {
            List<T_plan_aux> lista_planes_aux = new List<T_plan_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_plan_aux"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_aux t_plan_aux = new T_plan_aux();
                            t_plan_aux.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            t_plan_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_plan_aux.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            //t_plan_aux.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            t_plan_aux.emp_razon_social = DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);
                            t_plan_aux.plan_estado_nobre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            t_plan_aux.plan_costo = DataUtil.DbValueToDefault<float>(reader["plan_costo"]);
                            t_plan_aux.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            t_plan_aux.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            lista_planes_aux.Add(t_plan_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan_aux>();
            }
            return lista_planes_aux;
        }
        public List<T_actividades> sp_Consultar_Actividades_Plan(int tipo_servicio_id)
        {
            List<T_actividades> lista_actividades = new List<T_actividades>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Actividades_Plan"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, tipo_servicio_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades t_act = new T_actividades();
                            t_act.act_nombre = DataUtil.DbValueToDefault<string>(reader["act_nombre"]);
                            lista_actividades.Add(t_act);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_actividades>();
            }
            return lista_actividades;
        }
        public OperationResult sp_Actualizar_Costo_Tiempo_Actividades(double costo,int tiempo,int planid)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Plan_Costo_Tiempo"))
                {
                    Database.AddInParameter(command, "@plan_id", DbType.Int32, planid);
                    Database.AddInParameter(command, "@plan_costo", DbType.Double, costo);
                    Database.AddInParameter(command, "@plan_tiempo", DbType.Int32, tiempo);
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
        public List<T_plan_aux> sp_Consultar_Lista_Tipo_Nombre_Planes_Expediente(string plan_nombre)
        {
            List<T_plan_aux> T_Plan = new List<T_plan_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Tipo_Nombre_Planes_Expediente"))
                {
                    Database.AddInParameter(command, "@plan_nombre", DbType.String, plan_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_plan_aux t_plan_aux = new T_plan_aux();
                            t_plan_aux.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            t_plan_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            t_plan_aux.plan_fecha = DataUtil.DbValueToDefault<DateTime>(reader["plan_fecha"]);
                            t_plan_aux.plan_estado_nobre = DataUtil.DbValueToDefault<string>(reader["plan_estado_nobre"]);
                            t_plan_aux.plan_costo = DataUtil.DbValueToDefault<double>(reader["plan_costo"]);
                            t_plan_aux.plan_tiempo = DataUtil.DbValueToDefault<int>(reader["plan_tiempo"]);
                            t_plan_aux.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            T_Plan.Add(t_plan_aux);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_plan_aux>();
            }
            return T_Plan;
        }
    }
}
