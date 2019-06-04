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
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32,tipo_servicio_id);
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades t_actividad = new T_actividades();
                            t_actividad.act_id = DataUtil.DbValueToDefault<int>(reader["act_id"]);
                            t_actividad.act_nombre = DataUtil.DbValueToDefault<string>(reader["act_nombre"]);
                            //t_actividad.act_plazo= DataUtil.DbValueToDefault<int>(reader["act_plazo"]);
                            t_actividad.act_cantidad_maxima = DataUtil.DbValueToDefault<int>(reader["act_cantidad_maxima"]);
                            t_actividad.act_descripcion = DataUtil.DbValueToDefault<string>(reader["act_descripcion"]);                        
                            list_actividades.Add(t_actividad);
                            
                        }

                    }
                }
                return list_actividades;
            }
            catch (Exception)
            {

                return new List<T_actividades>();
            }
        }
        public OperationResult sp_Insertar_Actividades_Desarrollar(T_actividades_desarrollar act_desarrollar)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command=Database.GetStoredProcCommand("sp_Insertar_Actividades_Desarrollar"))
                {
                    Database.AddInParameter(command, "@exp_id", DbType.Int32, act_desarrollar.exp_id);
                    Database.AddInParameter(command, "@usu_creador", DbType.Int32, act_desarrollar.usu_creador);
                    Database.AddInParameter(command, "@est_act_id", DbType.Int32, act_desarrollar.est_act_id);
                    Database.AddInParameter(command, "@act_desa_nombre", DbType.String, act_desarrollar.act_desa_nombre);
                    Database.AddInParameter(command, "@act_desa_descripcion", DbType.String, act_desarrollar.act_desa_descripcion);
                    Database.ExecuteScalar(command);
                }
                return operationResult;
            }
            catch (Exception)
            {

                return new OperationResult();
            }
        }
        public List<T_actividades_planeadas> sp_Consultar_Listar_Actividades_Planeadas()
        {
            try
            {
                List<T_actividades_planeadas> list_act_planeadas = new List<T_actividades_planeadas>();
                using (DbCommand command= Database.GetStoredProcCommand("sp_Consultar_Listar_Actividades_Planeadas"))
                {
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_planeadas act_planeadas = new T_actividades_planeadas();
                            act_planeadas.act_plan_id = DataUtil.DbValueToDefault<int>(reader["act_plan_id"]);
                            act_planeadas.act_id = DataUtil.DbValueToDefault<int>(reader["act_id"]);
                            act_planeadas.plan_id = DataUtil.DbValueToDefault<int>(reader["plan_id"]);
                            act_planeadas.act_plan_nombre = DataUtil.DbValueToDefault<string>(reader["act_plan_nombre"]);
                            act_planeadas.act_plan_descripcion = DataUtil.DbValueToDefault<string>(reader["act_plan_descripcion"]);
                            act_planeadas.act_plan_costo = DataUtil.DbValueToDefault<double>(reader["act_plan_costo"]);
                            act_planeadas.act_plan_tiempo = DataUtil.DbValueToDefault<int>(reader["act_plan_tiempo"]);
                            list_act_planeadas.Add(act_planeadas);
                        }
                    }
                }
                return list_act_planeadas;
            }
            catch (Exception)
            {

                return new List<T_actividades_planeadas>();
            }
        }
        public OperationResult sp_actualizar_actividades_planeadas(T_actividades_planeadas actividades_planeadas)
        {
            var operation = new OperationResult();
            using (DbCommand command = Database.GetStoredProcCommand("sp_actualizar_actividades_planeadas"))
            {
                Database.AddInParameter(command, "@act_plan_id", DbType.Int32, actividades_planeadas.act_plan_id);
                Database.AddInParameter(command, "@act_plan_nombre", DbType.String, actividades_planeadas.act_plan_nombre);
                Database.AddInParameter(command, "@act_plan_descripcion", DbType.String, actividades_planeadas.act_plan_descripcion);
                Database.AddInParameter(command, "@act_plan_costo", DbType.Double, actividades_planeadas.act_plan_costo);
                Database.AddInParameter(command, "@act_plan_tiempo", DbType.Int32, actividades_planeadas.act_plan_tiempo);
                Database.ExecuteScalar(command);
                operation.NewId = 1;
            }
            return operation;
        }
        public List<T_actividades_planeadas_aux> sp_Consultar_Lista_Actividades_Planeadas_aux()
        {
            try
            {
                List<T_actividades_planeadas_aux> list_act_planeadas_aux = new List<T_actividades_planeadas_aux>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Actividades_Planeadas_aux"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_planeadas_aux act_planeadas_aux = new T_actividades_planeadas_aux();
                            act_planeadas_aux.act_plan_id = DataUtil.DbValueToDefault<int>(reader["act_plan_id"]);
                            act_planeadas_aux.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            act_planeadas_aux.act_nombre = DataUtil.DbValueToDefault<string>(reader["act_nombre"]);
                            act_planeadas_aux.act_plan_nombre = DataUtil.DbValueToDefault<string>(reader["act_plan_nombre"]);
                            act_planeadas_aux.act_plan_descripcion = DataUtil.DbValueToDefault<string>(reader["act_plan_descripcion"]);
                            act_planeadas_aux.act_plan_costo = DataUtil.DbValueToDefault<double>(reader["act_plan_costo"]);
                            act_planeadas_aux.act_plan_tiempo = DataUtil.DbValueToDefault<int>(reader["act_plan_tiempo"]);
                            list_act_planeadas_aux.Add(act_planeadas_aux);
                        }
                    }
                }
                return list_act_planeadas_aux;
            }
            catch (Exception)
            {

                return new List<T_actividades_planeadas_aux>();
            }
        }
        public T_actividades sp_Consultar_Actividades_planeadas_por_plan_act(int plan_id, int act_id)
        {
            try
            {
                T_actividades list_actividades = new T_actividades();
                using (DbCommand command = Database.GetStoredProcCommand("sp_actividades_planeadas_act_plan"))
                {
                    Database.AddInParameter(command, "@plan_id", DbType.Int32, plan_id);
                    Database.AddInParameter(command, "@act_id", DbType.Int32, act_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {                   
                            T_actividades t_actividad = new T_actividades();
                            t_actividad.act_id = DataUtil.DbValueToDefault<int>(reader["act_id"]);
                            t_actividad.act_nombre = DataUtil.DbValueToDefault<string>(reader["act_nombre"]);
                            t_actividad.act_descripcion = DataUtil.DbValueToDefault<string>(reader["act_descripcion"]);
                            
                    }
                }
                return list_actividades;
            }
            catch (Exception)
            {

                return new T_actividades();
            }
        }
        public OperationResult sp_registrar_actividades_planeadas(T_actividades_planeadas actividades_planeadas)
        {
            var operation = new OperationResult();
            using (DbCommand command = Database.GetStoredProcCommand("sp_registrar_actividades_planeadas"))
            {
                Database.AddInParameter(command, "@plan_id", DbType.Int32, actividades_planeadas.plan_id);
                Database.AddInParameter(command, "@act_id", DbType.Int32, actividades_planeadas.act_id);
                Database.AddInParameter(command, "@act_plan_nombre", DbType.String, actividades_planeadas.act_plan_nombre);
                Database.AddInParameter(command, "@act_plan_descripcion", DbType.String, actividades_planeadas.act_plan_descripcion);
                Database.AddInParameter(command, "@act_plan_costo", DbType.Double, actividades_planeadas.act_plan_costo);
                Database.AddInParameter(command, "@act_plan_tiempo", DbType.Int32, actividades_planeadas.act_plan_tiempo);
                Database.ExecuteScalar(command);
                operation.NewId = 1;
            }
            return operation;
        }
        public List<T_actividades_desarrollar> sp_Consultar_Actividades_Desarrollar_Expediente()
        {
            try
            {
                List<T_actividades_desarrollar> list_actividades_desarrollar = new List<T_actividades_desarrollar>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Activiades_Desarrollar_Expediente"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar actividades_desarrollar = new T_actividades_desarrollar();
                            actividades_desarrollar.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            actividades_desarrollar.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
                            actividades_desarrollar.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            actividades_desarrollar.est_act_id = DataUtil.DbValueToDefault<int>(reader["est_act_id"]);
                            actividades_desarrollar.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            actividades_desarrollar.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            actividades_desarrollar.est_act_nombre = DataUtil.DbValueToDefault<string>(reader["est_act_nombre"]);
                            list_actividades_desarrollar.Add(actividades_desarrollar);
                        }
                    }
                }
                return list_actividades_desarrollar;
            }
            catch (Exception)
            {

                return new List<T_actividades_desarrollar>();
            }
        }
        public List<T_actividades> sp_Consultar_Actividades()
        {
            try
            {
                List<T_actividades> list_actividades = new List<T_actividades>();
                using (DbCommand command= Database.GetStoredProcCommand("sp_Consultar_Actividades"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades t_Actividades = new T_actividades();
                            t_Actividades.act_id = DataUtil.DbValueToDefault<int>(reader["act_id"]);
                            t_Actividades.act_nombre = DataUtil.DbValueToDefault<string>(reader["act_nombre"]);
                            t_Actividades.act_descripcion = DataUtil.DbValueToDefault<string>(reader["act_descripcion"]);
                           // t_Actividades.act_plazo = DataUtil.DbValueToDefault<int>(reader["act_plazo"]);
                            t_Actividades.act_cantidad_maxima = DataUtil.DbValueToDefault<int>(reader["act_cantidad_maxima"]);
                            list_actividades.Add(t_Actividades);
                        }
                    }
                }
                return list_actividades;
            }
            catch (Exception)
            {

                return new List<T_actividades>();
            }
        }
        public OperationResult sp_eliminar_actividades_planeadas(int plan_id)
        {
            var operation = new OperationResult();
            using (DbCommand command = Database.GetStoredProcCommand("sp_eliminar_actividades_planeadas"))
            {
                Database.AddInParameter(command, "@plan_id", DbType.Int32, plan_id);
                Database.ExecuteScalar(command);
                operation.NewId = 1;
            }
            return operation;
        }

        public List<T_tipo_servicio_actividades> sp_consultar_lista_tipo_servicio_actividades()
        {
            try
            {
                List<T_tipo_servicio_actividades> listTipoServicioAct = new List<T_tipo_servicio_actividades>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_consultar_lista_tipo_servicio_actividades"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_tipo_servicio_actividades tsa = new T_tipo_servicio_actividades();
                            tsa.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            tsa.act_id = DataUtil.DbValueToDefault<int>(reader["act_id"]);
                            tsa.act_obligatorio = DataUtil.DbValueToDefault<bool>(reader["tipo_servicio_obligatorio"]);
                            tsa.costo = DataUtil.DbValueToDefault<double>(reader["tipo_servicio_costo"]);
                            listTipoServicioAct.Add(tsa);
                        }
                    }
                }
                return listTipoServicioAct;
            }
            catch
            {
                return new List<T_tipo_servicio_actividades>();
            }
        }
        public List<T_actividades_desarrollar> sp_Consultar_Actividades_Desarrollar_Expediente_Completa()
        {
            try
            {
                List<T_actividades_desarrollar> list_actividades_desarrollar = new List<T_actividades_desarrollar>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Actividades_Desarrollar_Expediente_Completa"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar actividades_desarrollar = new T_actividades_desarrollar();
                            actividades_desarrollar.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            actividades_desarrollar.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
                            actividades_desarrollar.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            actividades_desarrollar.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            actividades_desarrollar.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            actividades_desarrollar.act_desa_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_finalizada"]);
                            actividades_desarrollar.est_act_id = DataUtil.DbValueToDefault<int>(reader["est_act_id"]);
                            actividades_desarrollar.usu_revisor = DataUtil.DbValueToDefault<int>(reader["usu_revisor"]);
                            actividades_desarrollar.usu_asignado = DataUtil.DbValueToDefault<int>(reader["usu_asignado"]);
                            actividades_desarrollar.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            actividades_desarrollar.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            actividades_desarrollar.act_desa_archivo_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_archivo_nombre"]);
                            actividades_desarrollar.act_desa_fecha_revision = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_revision"]);
                            actividades_desarrollar.usu_revisor_nombre = DataUtil.DbValueToDefault<string>(reader["nombre_revisor"]);
                            actividades_desarrollar.usu_asignado_nombre = DataUtil.DbValueToDefault<string>(reader["nombre_asignado"]);
                            actividades_desarrollar.est_act_nombre = DataUtil.DbValueToDefault<string>(reader["est_act_nombre"]);
                            list_actividades_desarrollar.Add(actividades_desarrollar);
                        }
                    }
                }
                return list_actividades_desarrollar;
            }
            catch (Exception)
            {

                return new List<T_actividades_desarrollar>();
            }
        }
    }
}
