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
    }
}
