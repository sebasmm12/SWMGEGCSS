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
       
    }
}
