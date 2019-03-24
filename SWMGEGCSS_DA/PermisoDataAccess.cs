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
    public class PermisoDataAccess:BaseConexion
    {
        public List<T_permiso> sp_Listar_Permisos_Usuario(string rol_nombre)
        {
            var l_permiso_usuario = new List<T_permiso>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Permisos_Usuario"))
                {
                    Database.AddInParameter(command, "@rol_nombre", DbType.String, rol_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var permiso = new T_permiso();
                            permiso.per_codigo = DataUtil.DbValueToDefault<int>(reader["per_codigo"]);
                            permiso.per_nombre = DataUtil.DbValueToDefault<string>(reader["per_nombre"]);
                            permiso.per_descripcion = DataUtil.DbValueToDefault<string>(reader["per_descripcion"]);
                            permiso.per_url = DataUtil.DbValueToDefault<string>(reader["per_url"]);
                            permiso.per_img = DataUtil.DbValueToDefault<string>(reader["per_img"]);
                            l_permiso_usuario.Add(permiso);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return new List<T_permiso>();
            }
            return l_permiso_usuario;
        }
    }
}
