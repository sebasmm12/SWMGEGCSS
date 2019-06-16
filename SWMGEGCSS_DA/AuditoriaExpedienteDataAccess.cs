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
    public class AuditoriaExpedienteDataAccess : BaseConexion
    {
        public List<T_aux_auditar_expedientes> sp_Listar_Aux_Expediente_por_ExpedienteId(int exp_id)
        {
            List<T_aux_auditar_expedientes> listAuditoriaAux = new List<T_aux_auditar_expedientes>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Aux_Expediente_por_ExpedienteId"))
                {
                    Database.AddInParameter(command, "@exp_id", DbType.Int32, exp_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_aux_auditar_expedientes t_aux_audit_exp = new T_aux_auditar_expedientes();
                            t_aux_audit_exp.aud_exp_id = DataUtil.DbValueToDefault<int>(reader["aud_exp_id"]);
                            t_aux_audit_exp.aud_exp_fecha_auditoria = DataUtil.DbValueToDefault<DateTime>(reader["aud_exp_fecha_auditoria"]);
                            t_aux_audit_exp.aud_exp_inicio = DataUtil.DbValueToDefault<DateTime>(reader["aud_exp_inicio"]);
                            t_aux_audit_exp.aud_exp_fin = DataUtil.DbValueToDefault<DateTime>(reader["aud_exp_fin"]);
                            t_aux_audit_exp.tipo_servicio_nombre = DataUtil.DbValueToDefault<String>(reader["tipo_servicio_nombre"]);
                            t_aux_audit_exp.aud_exp_ganancia = DataUtil.DbValueToDefault<double>(reader["aud_exp_ganancia"]);
                            t_aux_audit_exp.aud_exp_comentario = DataUtil.DbValueToDefault<String>(reader["aud_exp_comentario"]);
                            listAuditoriaAux.Add(t_aux_audit_exp);
                        }
                    }
                }
            }
            catch
            {
                return new List<T_aux_auditar_expedientes>();
            }
            return listAuditoriaAux;
        }
        public List<T_auditoria_actividades_desarrollo> sp_Listar_Auditoria_Actividades_Desarrollar_por_ActividadesId(int act_desa_id)
        {
            List<T_auditoria_actividades_desarrollo> listAuditoriaAux = new List<T_auditoria_actividades_desarrollo>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Auditoria_Actividades_Desarrollar_por_ActividadesId"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, act_desa_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_auditoria_actividades_desarrollo t_aux_audit_act = new T_auditoria_actividades_desarrollo();
                            t_aux_audit_act.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            t_aux_audit_act.audi_act_fecha_auditoria = DataUtil.DbValueToDefault<DateTime>(reader["audi_act_fecha_auditoria"]);
                            t_aux_audit_act.audi_act_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["audi_act_fecha_inicio"]);
                            t_aux_audit_act.audi_act_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["audi_act_fecha_fin"]);
                            t_aux_audit_act.audi_act_fecha_revision = DataUtil.DbValueToDefault<DateTime>(reader["audi_act_fecha_revision"]);
                            t_aux_audit_act.audi_act_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["audi_act_fecha_finalizada"]);
                            t_aux_audit_act.usu_asignado = DataUtil.DbValueToDefault<int>(reader["usu_asignado"]);
                            t_aux_audit_act.audi_act_archivo_url = DataUtil.DbValueToDefault<String>(reader["audi_act_archivo_url"]);
                            t_aux_audit_act.audi_act_archivo_nombre = DataUtil.DbValueToDefault<String>(reader["audi_act_archivo_nombre"]);
                            listAuditoriaAux.Add(t_aux_audit_act);
                        }
                    }
                }
            }
            catch
            {
                return new List<T_auditoria_actividades_desarrollo>();
            }
            return listAuditoriaAux;
        }
    }
}
