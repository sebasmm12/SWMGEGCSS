using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWMGEGCSS_EN;
using SWMGEGCSS_DA.Base;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SWMGEGCSS_DA
{
    public class ActividadesUsuarioDataAccess : BaseConexion
    {
        public List<T_actividades_desarrollar_usuarios> sp_Consultar_Lista_Actividades_Usuario()
        {
            var l_actividades = new List<T_actividades_desarrollar_usuarios>();
            try
            {
                using(DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Actividades_Usuario"))
                {
                    using(IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var actividades = new T_actividades_desarrollar_usuarios();
                            actividades.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            actividades.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            actividades.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            actividades.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            actividades.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            actividades.plan_nombre = DataUtil.DbValueToDefault<string>(reader["plan_nombre"]);
                            l_actividades.Add(actividades);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " ");
                return new List<T_actividades_desarrollar_usuarios>();
            }
            return l_actividades;
        }

        public List<T_actividades_desarrollar_usuarios> sp_Consultar_Lista_Actividades_Usuario_Paged()
        {
            var l_actividades = new List<T_actividades_desarrollar_usuarios>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Actividades_Usuario_Paged"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var actividades = new T_actividades_desarrollar_usuarios();
                            actividades.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            actividades.cant_actividades = DataUtil.DbValueToDefault<int>(reader["COUNT"]);
                            actividades.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            l_actividades.Add(actividades);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e + " ");
                return new List<T_actividades_desarrollar_usuarios>();
            }
            return l_actividades;
        }
    }
}
