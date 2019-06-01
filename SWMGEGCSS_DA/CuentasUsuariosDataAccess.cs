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

        public OperationResult sp_Insertar_Cuenta_Usuario_Detalle(T_usuario_cuentas_aux usuario, string det_usu_tip_doc, string det_usu_sexo, string tipo_det_usu_tipo)
        {
            try
            {
                var operation = new OperationResult();
                using(DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Cuenta_Usuario_Detalle"))
                {
                    Database.AddInParameter(command, "@det_usu_nombre", DbType.String, usuario.det_usu_nombre);
                    Database.AddInParameter(command, "@det_usu_correo", DbType.String, usuario.det_usu_correo);
                    Database.AddInParameter(command, "@det_usu_direccion", DbType.String, usuario.det_usu_direccion);
                    Database.AddInParameter(command, "@det_usu_telefono", DbType.String, usuario.det_usu_telefono);
                    Database.AddInParameter(command, "@det_usu_tip_doc", DbType.Int32, Convert.ToInt32(det_usu_tip_doc));
                    Database.AddInParameter(command, "@det_usu_tip_doc_numero", DbType.String, usuario.det_usu_tip_doc_numero);
                    Database.AddInParameter(command, "@tipo_det_usu_tipo", DbType.Int32, Convert.ToInt32(tipo_det_usu_tipo));
                    Database.AddInParameter(command, "@det_usu_sexo", DbType.String, det_usu_sexo);
                    Database.AddInParameter(command, "@det_usu_codigoColegio", DbType.String, usuario.det_usu_codigoColegio);
                    Database.AddInParameter(command, "@det_usu_especialidad", DbType.String, usuario.det_usu_especialidad);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception e)
            {
                Console.Write(e + "");
                return new OperationResult();
            }
        }

    }
}
