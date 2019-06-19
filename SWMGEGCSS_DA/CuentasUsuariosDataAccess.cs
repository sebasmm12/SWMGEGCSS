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
                            cuenta.usu_estado = DataUtil.DbValueToDefault<Int32>(reader["usu_estado"]);
                            cuenta.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            cuenta.det_usu_correo = DataUtil.DbValueToDefault<string>(reader["det_usu_correo"]);
                            cuenta.det_usu_direccion = DataUtil.DbValueToDefault<string>(reader["det_usu_direccion"]);
                            cuenta.det_usu_telefono = DataUtil.DbValueToDefault<string>(reader["det_usu_telefono"]);
                            cuenta.det_usu_sexo = DataUtil.DbValueToDefault<string>(reader["det_usu_sexo"]);
                            cuenta.det_usu_tip_doc_numero = DataUtil.DbValueToDefault<string>(reader["det_usu_tip_doc_numero"]);
                            cuenta.det_usu_codigoColegio = DataUtil.DbValueToDefault<string>(reader["det_usu_codigoColegio"]);
                            cuenta.det_usu_especialidad = DataUtil.DbValueToDefault<string>(reader["det_usu_especialidad"]);
                            cuenta.rol_nombre = DataUtil.DbValueToDefault<string>(reader["rol_nombre"]);
                            cuenta.rol_codigo = DataUtil.DbValueToDefault<int>(reader["rol_codigo"]);
                            cuenta.det_usu_tip_doc = DataUtil.DbValueToDefault<int>(reader["det_usu_tip_doc"]);
                            cuenta.tipo_det_usu_tipo = DataUtil.DbValueToDefault<int>(reader["tipo_det_usu_tipo"]);
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

        public List<T_usuario> sp_Consultar_Lista_Todos_Usuarios()
        {
            var l_cuentas = new List<T_usuario>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Todos_Usuarios"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var cuenta = new T_usuario();
                            cuenta.usu_codigo = DataUtil.DbValueToDefault<Int32>(reader["usu_codigo"]);
                            cuenta.usu_usuario = DataUtil.DbValueToDefault<string>(reader["usu_usuario"]);
                            
                            l_cuentas.Add(cuenta);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e + " ");
                return new List<T_usuario>();
            }
            return l_cuentas;
        }

        public OperationResult sp_Insertar_Cuenta_Usuario_Detalle(T_usuario_cuentas_aux usuario, string det_usu_tip_doc, string det_usu_sexo, string tipo_det_usu_tipo, T_usuario usuarios_cuentas)
        {
            try
            {
                var operation = new OperationResult();
                using(DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Cuenta_Usuario_Detalle"))
                {
                    Database.AddInParameter(command, "usu_codigo", DbType.Int32, usuarios_cuentas.usu_codigo);
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

        public OperationResult sp_Insertar_Cuenta_Usuario(T_usuario_cuentas_aux usuario, string rol_codigo, string usu_contraseña)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Cuenta_Usuario"))
                {
                    Database.AddInParameter(command, "@usu_usuario", DbType.String, usuario.usu_usuario);
                    Database.AddInParameter(command, "@usu_contraseña", DbType.String, usu_contraseña);
                    Database.AddInParameter(command, "@usu_estado", DbType.Int32, 1);
                    Database.AddInParameter(command, "@usu_trabajador", DbType.Int32, 1);
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

        public OperationResult sp_Insertar_Cuenta_Usuario_Rol(T_usuario_cuentas_aux usuario, string rol_codigo, T_usuario usuarios_cuentas)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Cuenta_Usuario_Rol"))
                {
                    Database.AddInParameter(command, "@usu_codigo", DbType.String, usuarios_cuentas.usu_codigo);
                    Database.AddInParameter(command, "@rol_codigo", DbType.String, rol_codigo);
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

        public OperationResult sp_Insertar_Imagen_Usuario(T_usuario_cuentas_aux usuario, T_usuario usuarios_cuentas)
        {
            try
            {
                var operationresult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Imagen_Usuario"))
                {
                    Database.AddInParameter(command, "@imagen", DbType.Binary, usuario.det_usu_imagem);
                    Database.AddInParameter(command, "@codigo", DbType.Int32, usuarios_cuentas.usu_codigo);
                    Database.ExecuteScalar(command);
                    operationresult.NewId = 1;
                }
                return operationresult;
            }
            catch (Exception ex)
            {

                return new OperationResult();
            }
        }

        public byte[] sp_Consultar_Imagen_Usuario(int codigo)
        {
            byte[] count = new byte[6000];
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Imagen_Usuario"))
                {
                    Database.AddInParameter(command, "@codigo", DbType.Int32, codigo);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            count = DataUtil.DbValueToDefault<byte[]>(reader["det_usu_imagem"]);

                        }
                    }
                }
            }
            catch (Exception)
            {

                return new byte[6000];
            }
            return count;
        }

        public List<T_usuario_cuentas_aux> sp_Consultar_Lista_Nombre_Usuario_Cuenta(string usu_usuario)
        {
            List<T_usuario_cuentas_aux> T_Empresa = new List<T_usuario_cuentas_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Usuario_Cuenta"))
                {

                    Database.AddInParameter(command, "@usu_usuario", DbType.String, usu_usuario);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_usuario_cuentas_aux cuenta = new T_usuario_cuentas_aux();

                            cuenta.usu_codigo = DataUtil.DbValueToDefault<Int32>(reader["usu_codigo"]);
                            cuenta.usu_usuario = DataUtil.DbValueToDefault<string>(reader["usu_usuario"]);
                            cuenta.usu_contraseña = DataUtil.DbValueToDefault<string>(reader["usu_contraseña"]);
                            cuenta.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            cuenta.rol_nombre = DataUtil.DbValueToDefault<string>(reader["rol_nombre"]);
                            T_Empresa.Add(cuenta);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_usuario_cuentas_aux>();
            }
            return T_Empresa;
        }

        public List<T_Roles> sp_Consultar_Lista_Roles()
        {
            var l_cuentas = new List<T_Roles>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Roles"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var cuenta = new T_Roles();
                            cuenta.rol_codigo = DataUtil.DbValueToDefault<Int32>(reader["rol_codigo"]);
                            cuenta.rol_nombre = DataUtil.DbValueToDefault<string>(reader["rol_nombre"]);

                            l_cuentas.Add(cuenta);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e + " ");
                return new List<T_Roles>();
            }
            return l_cuentas;
        }

        public OperationResult sp_Actualizar_Usuario(T_usuario_cuentas_aux usuarios)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Usuario"))
                {
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, usuarios.usu_codigo);
                    Database.AddInParameter(command, "@usu_usuario", DbType.String, usuarios.usu_usuario);
                    Database.AddInParameter(command, "@usu_contraseña", DbType.String, usuarios.usu_contraseña);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception e)
            {
                Console.Write(" " + e);
                return new OperationResult();
            }
            
        }

        public OperationResult sp_Actualizar_Usuario_Rol(T_usuario_cuentas_aux usuarios)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Usuario_Rol"))
                {
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, usuarios.usu_codigo);
                    Database.AddInParameter(command, "@rol_codigo", DbType.Int32, usuarios.rol_codigo);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception e)
            {
                Console.Write(" " + e);
                return new OperationResult();
            }

        }

        public OperationResult sp_Eliminar_Usuario(int usu_codigo)
        {
            try
            {
                var operation = new OperationResult();
                using(DbCommand command = Database.GetStoredProcCommand("sp_Eliminar_Usuario"))
                {
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, usu_codigo);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception e)
            {
                Console.Write(" " + e);
                return new OperationResult();
            }
        }

    }


}
