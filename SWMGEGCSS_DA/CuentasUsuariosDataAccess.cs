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
    public class CuentasUsuariosDataAccess : BaseConexion
    {
        public List<T_usuario_cuentas_aux> sp_Consultar_Lista_Cuentas()
        {
            var l_cuentas = new List<T_usuario_cuentas_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Cuentas"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var cuenta = new T_usuario_cuentas_aux();
                            cuenta.usu_codigo = DataUtil.DbValueToDefault<Int32>(reader["usu_codigo"]);
                            cuenta.usu_usuario = DataUtil.DbValueToDefault<string>(reader["usu_usuario"]);
                            cuenta.usu_contraseña = DataUtil.DbValueToDefault<string>(reader["usu_contraseña"]);
                            cuenta.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            cuenta.rol_nombre = DataUtil.DbValueToDefault<string>(reader["rol_nombre"]);
                            l_cuentas.Add(cuenta);
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.Write(e + " ");
                return new List<T_usuario_cuentas_aux>();
            }
            return l_cuentas;
        }
    }
}
