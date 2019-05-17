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
    public class TipoServicioDataAccess : BaseConexion
    {
        public List<T_tipo_servicio> sp_Consultar_Lista_Tipo_Servicio()
        {
            var lista_Tipo_Servicio = new List<T_tipo_servicio>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Tipo_Servicio"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var tipo_servicio = new T_tipo_servicio();
                            tipo_servicio.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            tipo_servicio.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            lista_Tipo_Servicio.Add(tipo_servicio);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_tipo_servicio>();
            }
            return lista_Tipo_Servicio;
        }
        public List<T_tipo_servicio> sp_Consultar_Tipo_Servicio()
        {
            try
            {
                List<T_tipo_servicio> list_tipo_servicio = new List<T_tipo_servicio>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Tipo_Servicio"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var tipo_servicio = new T_tipo_servicio();
                            tipo_servicio.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            tipo_servicio.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            tipo_servicio.tipo_servicio_descripcion = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_descripcion"]);
                            tipo_servicio.tipo_servicio_estado = DataUtil.DbValueToDefault<bool>(reader["tipo_servicio_estado"]);
                            list_tipo_servicio.Add(tipo_servicio);
                        }
                    }
                }
                return list_tipo_servicio;
            }
            catch (Exception)
            {

                return new List<T_tipo_servicio>();
            }
        }
        public List<T_tipo_servicio> sp_Consultar_Lista_Servicios_Nombre(string tipo_servicio_nombre)
        {
            try
            {
                List<T_tipo_servicio> list_tipo_servicio = new List<T_tipo_servicio>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Servicios_Nombre"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_nombre", DbType.String, tipo_servicio_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var tipo_servicio = new T_tipo_servicio();
                            tipo_servicio.tipo_servicio_id = DataUtil.DbValueToDefault<int>(reader["tipo_servicio_id"]);
                            tipo_servicio.tipo_servicio_nombre = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_nombre"]);
                            tipo_servicio.tipo_servicio_descripcion = DataUtil.DbValueToDefault<string>(reader["tipo_servicio_descripcion"]);
                            tipo_servicio.tipo_servicio_estado = DataUtil.DbValueToDefault<bool>(reader["tipo_servicio_estado"]);
                            list_tipo_servicio.Add(tipo_servicio);
                        }
                    }
                }
                return list_tipo_servicio;
            }
            catch (Exception)
            {

                return new List<T_tipo_servicio>();
            }
        }
        public OperationResult sp_Insertar_Tipo_Servicio(T_tipo_servicio tipo_servicio)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Tipo_Servicio"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_nombre", DbType.String, tipo_servicio.tipo_servicio_nombre);
                    Database.AddInParameter(command, "@tipo_servicio_descripcion", DbType.String, tipo_servicio.tipo_servicio_descripcion);
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
        public OperationResult sp_Insertar_Tipo_Servicio_Actividades(T_tipo_servicio_actividades tipo_servicio_actividades)
        {

            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Tipo_Servicios_Actividades"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, tipo_servicio_actividades.tipo_servicio_id);
                    Database.AddInParameter(command, "@act_id", DbType.Int32, tipo_servicio_actividades.act_id);
                    Database.AddInParameter(command, "@tipo_servicio_obligatorio", DbType.Boolean, tipo_servicio_actividades.act_obligatorio);
                    Database.AddInParameter(command, "@tipo_servicio_costo", DbType.Double, tipo_servicio_actividades.costo);
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
        public List<T_tipo_servicio_actividades_aux> sp_Consultar_tipos_servicios_actividades()
        {
            try
            {
                List<T_tipo_servicio_actividades_aux> list_tipo_act = new List<T_tipo_servicio_actividades_aux>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_tipos_servicios_actividades"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_tipo_servicio_actividades_aux tipo_serv_act = new T_tipo_servicio_actividades_aux();
                            tipo_serv_act.tipo_servicio_id = DataUtil.DbValueToDefault<Int32>(reader["tipo_servicio_id"]);
                            tipo_serv_act.tipo_servicio_nombre = DataUtil.DbValueToDefault<String>(reader["tipo_servicio_nombre"]);
                            tipo_serv_act.act_id = DataUtil.DbValueToDefault<Int32>(reader["act_id"]);
                            tipo_serv_act.act_nombre = DataUtil.DbValueToDefault<String>(reader["act_nombre"]);
                            tipo_serv_act.tipo_servicio_costo = DataUtil.DbValueToDefault<double>(reader["tipo_servicio_costo"]);
                            tipo_serv_act.tipo_servicio_obligatorio = DataUtil.DbValueToDefault<bool>(reader["tipo_servicio_obligatorio"]);
                            list_tipo_act.Add(tipo_serv_act);
                        }
                    }
                }
                return list_tipo_act;
            }
            catch (Exception)
            {

                return new List<T_tipo_servicio_actividades_aux>();
            }
        }
        public OperationResult sp_Cambiar_Estado_Tipo_Servicio(int tipo_servicio_id)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Cambiar_Estado_Tipo_Servicio"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, tipo_servicio_id);
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
        public OperationResult sp_Actualizar_Datos_del_Servicio(T_tipo_servicio tipo_servicio)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command= Database.GetStoredProcCommand("sp_Actualizar_Datos_del_Servicio"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_nombre", DbType.String, tipo_servicio.tipo_servicio_nombre);
                    Database.AddInParameter(command, "@tipo_servicio_descripcion", DbType.String, tipo_servicio.tipo_servicio_descripcion);
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.String, tipo_servicio.tipo_servicio_id);
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
        public OperationResult sp_Activar_Servicio(int tipo_servicio_id)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Activar_Servicio"))
                {
                    Database.AddInParameter(command, "@tipo_servicio_id", DbType.Int32, tipo_servicio_id);
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

    }
}
