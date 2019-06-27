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
    public class ObservacionesDataAccess : BaseConexion
    {
        public List<T_observacion_actividades> sp_Consultar_Observaciones_No_Resueltas(int id)
        {
            List<T_observacion_actividades> list_obs = new List<T_observacion_actividades>();

            try
            {

                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Observaciones_No_Resueltas"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_observacion_actividades t_obs = new T_observacion_actividades();
                            t_obs.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            t_obs.obs_act_creacion = DataUtil.DbValueToDefault<DateTime>(reader["obs_act_creacion"]);
                            t_obs.obs_act_est_id = DataUtil.DbValueToDefault<int>(reader["obs_act_est_id"]);
                            t_obs.obs_act_fecha_revisor = DataUtil.DbValueToDefault<DateTime>(reader["obs_act_fecha_revisor"]);
                            t_obs.obs_act_fecha_usuario = DataUtil.DbValueToDefault<DateTime>(reader["obs_act_fecha_usuario"]);
                            t_obs.obs_act_id = DataUtil.DbValueToDefault<int>(reader["obs_act_id"]);
                            t_obs.obs_act_nombre = DataUtil.DbValueToDefault<String>(reader["obs_act_nombre"]);
                            t_obs.obs_act_revisor = DataUtil.DbValueToDefault<String>(reader["obs_act_revisor"]);
                            t_obs.obs_act_usuario = DataUtil.DbValueToDefault<String>(reader["obs_act_usuario"]);
                            list_obs.Add(t_obs);
                        }
                    }
                }
                return list_obs;
            }
            catch (Exception)
            {

                return new List<T_observacion_actividades>();
            }
        }

        public T_observacion_actividades sp_Consultar_Observacion(int id)
        {
            T_observacion_actividades obs = new T_observacion_actividades();
            try
            {
                T_observacion_actividades t_obs = new T_observacion_actividades();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Observacion"))
                {

                    Database.AddInParameter(command, "@obs_act_id", DbType.String, id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {

                        while (reader.Read())
                        {

                            t_obs.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            t_obs.obs_act_creacion = DataUtil.DbValueToDefault<DateTime>(reader["obs_act_creacion"]);
                            t_obs.obs_act_est_id = DataUtil.DbValueToDefault<int>(reader["obs_act_est_id"]);
                            t_obs.obs_act_fecha_revisor = DataUtil.DbValueToDefault<DateTime>(reader["obs_act_fecha_revisor"]);
                            t_obs.obs_act_fecha_usuario = DataUtil.DbValueToDefault<DateTime>(reader["obs_act_fecha_usuario"]);
                            t_obs.obs_act_id = DataUtil.DbValueToDefault<int>(reader["obs_act_id"]);
                            t_obs.obs_act_nombre = DataUtil.DbValueToDefault<String>(reader["obs_act_nombre"]);
                            t_obs.obs_act_revisor = DataUtil.DbValueToDefault<String>(reader["obs_act_revisor"]);
                            t_obs.obs_act_usuario = DataUtil.DbValueToDefault<String>(reader["obs_act_usuario"]);

                        }
                    }
                }
                return t_obs;
            }
            catch (Exception)
            {

                return new T_observacion_actividades();
            }
        }

        public List<T_observacion_actividades_estado> sp_Consultar_Observacion_actividades_estado()
        {
            List<T_observacion_actividades_estado> list_obs_est = new List<T_observacion_actividades_estado>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Observacion_actividades_estado"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_observacion_actividades_estado t_obs_est = new T_observacion_actividades_estado();
                            t_obs_est.obs_act_est_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            t_obs_est.obs_act_est_nombre = DataUtil.DbValueToDefault<String>(reader["obs_act_nombre"]);
                            t_obs_est.obs_act_est_descripcion = DataUtil.DbValueToDefault<String>(reader["obs_act_est_descripcion"]);
                            list_obs_est.Add(t_obs_est);
                        }
                    }
                }
                return list_obs_est;
            }
            catch (Exception)
            {

                return new List<T_observacion_actividades_estado>();
            }
        }

        public OperationResult sp_registrar_observacion(T_observacion_actividades observaciones_actividades)
        {
            var operation = new OperationResult();
            using (DbCommand command = Database.GetStoredProcCommand("sp_registrar_observacion"))
            {
                Database.AddInParameter(command, "@act_desa_id", DbType.Int32, observaciones_actividades.act_desa_id);
                Database.AddInParameter(command, "@obs_act_nombre", DbType.String, observaciones_actividades.obs_act_nombre);
                Database.AddInParameter(command, "@obs_act_revisor", DbType.String, observaciones_actividades.obs_act_revisor);
                Database.AddInParameter(command, "@obs_act_est_id", DbType.Int32, 1);
                Database.ExecuteScalar(command);

                operation.NewId = 1;
            }
            return operation;
        }

        public OperationResult sp_actualizar_observacion(T_observacion_actividades observaciones_actividades)
        {
            var operation = new OperationResult();
            using (DbCommand command = Database.GetStoredProcCommand("sp_actualizar_observacion"))
            {
                Database.AddInParameter(command, "@obs_act_id", DbType.Int32, observaciones_actividades.obs_act_id);
                Database.AddInParameter(command, "@obs_act_nombre", DbType.String, observaciones_actividades.obs_act_nombre);
                Database.AddInParameter(command, "@obs_act_revisor", DbType.String, observaciones_actividades.obs_act_revisor);
                Database.AddInParameter(command, "@obs_act_est_id", DbType.Int32, observaciones_actividades.obs_act_est_id);
                Database.ExecuteScalar(command);
                operation.NewId = 1;
            }
            return operation;
        }

    }
}
