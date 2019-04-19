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
    public class UsuarioDataAccess:BaseConexion
    {
        public int sp_Encontrar_Usuario(T_usuario usuario,T_detalle_usuario t_d_usuario)
        {
            int count = 0;
            try
            {
                using (DbCommand command=Database.GetStoredProcCommand("sp_Encontrar_Usuario"))
                {
                    Database.AddInParameter(command, "@usu_usuario", DbType.String, usuario.usu_usuario);
                    Database.AddInParameter(command, "@usu_contraseña", DbType.String,usuario.usu_contraseña);
                    using (IDataReader reader=Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            count = DataUtil.DbValueToDefault<int>(reader["CONTADOR"]);
                            usuario.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            t_d_usuario.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return count;
            }
            return count;
        }
        public int sp_Consultar_Sesion_Empresa(T_usuario usuario)
        {
            int count = 0;
            try
            {
                using (DbCommand command=Database.GetStoredProcCommand("sp_Consultar_Sesion_Empresa"))
                {
                    Database.AddInParameter(command, "@usu_usuario", DbType.String, usuario.usu_usuario);
                    Database.AddInParameter(command, "@usu_contraseña", DbType.String, usuario.usu_contraseña);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            count = DataUtil.DbValueToDefault<int>(reader["COUNT"]);
                            usuario.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return count;
            }
            return count;
        }
    }
}
