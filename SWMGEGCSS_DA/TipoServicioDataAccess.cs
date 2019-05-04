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
    }
}
