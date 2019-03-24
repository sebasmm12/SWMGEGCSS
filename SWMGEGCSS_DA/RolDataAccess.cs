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
    public class RolDataAccess:BaseConexion
    {
        public string sp_Obtener_Rol_Nombre_Usuario(int codigo)
        {
            string rol_nombre = "";
            try
            {
                using (DbCommand command=Database.GetStoredProcCommand("sp_Obtener_Rol_Nombre_Usuario"))
                {
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, codigo);
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            rol_nombre = DataUtil.DbValueToDefault<string>(reader["rol_nombre"]);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return rol_nombre;
            }
            return rol_nombre;
        }
    }
}
