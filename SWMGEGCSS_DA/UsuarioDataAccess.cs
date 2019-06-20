using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWMGEGCSS_EN;
using SWMGEGCSS_DA.Base;
using System.Data;
using System.Data.Common;
using System.IO;
namespace SWMGEGCSS_DA
{
    public class UsuarioDataAccess:BaseConexion
    {

        public List<T_detalle_usuario> sp_Consultar_Lista_Usuario()
        {
            var l_usu = new List<T_detalle_usuario>();
            try
            {
                using(DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Usuario"))
                {
                    using(IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var usu = new T_detalle_usuario();
                            usu.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            usu.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            l_usu.Add(usu);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                return new List<T_detalle_usuario>();
            }
            return l_usu;
        }
        public List<T_detalle_usuario> sp_Consultar_Lista_Detalle_Usuarios()
        {
            var l_detalle = new List<T_detalle_usuario>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Detalle_Usuarios"))
                {

                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var detalle = new T_detalle_usuario();
                            detalle.usu_codigo= DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            detalle.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            detalle.det_usu_correo = DataUtil.DbValueToDefault<string>(reader["det_usu_correo"]);
                            detalle.det_usu_direccion = DataUtil.DbValueToDefault<string>(reader["det_usu_direccion"]);
                            detalle.det_usu_telefono = DataUtil.DbValueToDefault<string>(reader["det_usu_telefono"]);
                            detalle.det_usu_sexo = DataUtil.DbValueToDefault<string>(reader["det_usu_sexo"]);
                            detalle.det_usu_tip_doc= DataUtil.DbValueToDefault<int>(reader["det_usu_tip_doc"]);
                            detalle.det_usu_tip_doc_numero = DataUtil.DbValueToDefault<string>(reader["det_usu_tip_doc_numero"]);
                            detalle.tipo_det_usu_tipo = DataUtil.DbValueToDefault<int>(reader["tipo_det_usu_tipo"]);
                            detalle.det_usu_codigoColegio = DataUtil.DbValueToDefault<string>(reader["det_usu_codigoColegio"]);
                            detalle.det_usu_especialidad = DataUtil.DbValueToDefault<string>(reader["det_usu_especialidad"]);
                            l_detalle.Add(detalle);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(" " + e);
                return new List<T_detalle_usuario>();
            }
            return l_detalle;
        }
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

        public OperationResult sp_Actualizar_Datos_Personales(T_detalle_usuario Usuario)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Datos_Personales"))
                {
                    Database.AddInParameter(command, "@det_usu_nombre", DbType.String, Usuario.det_usu_nombre);
                    Database.AddInParameter(command, "@det_usu_direccion", DbType.String, Usuario.det_usu_direccion);
                    Database.AddInParameter(command, "@det_usu_telefono", DbType.String, Usuario.det_usu_telefono);
                    Database.AddInParameter(command, "@det_usu_correo", DbType.String, Usuario.det_usu_correo);
                    Database.AddInParameter(command, "@det_usu_codigoColegio", DbType.String, Usuario.det_usu_codigoColegio);
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, Usuario.usu_codigo);
                    Database.AddInParameter(command, "@det_usu_especialidad", DbType.String, Usuario.det_usu_especialidad);
               //     Database.AddInParameter(command, "@det_usu_imagem", DbType.Byte, Usuario.det_usu_imagem);

                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception)
            {
                return new OperationResult();
            }
        }

        public OperationResult sp_Actualizar_Imagen_Usuario(T_detalle_usuario usuario, T_usuario usuarios_cuentas)
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
    }
}
