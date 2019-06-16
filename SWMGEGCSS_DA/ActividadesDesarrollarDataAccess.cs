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
    public class ActividadesDesarrollarDataAccess : BaseConexion
    {
        public List<T_actividades_desarrollar> sp_Listar_Actividades_Desarrollar()
        {
            List<T_actividades_desarrollar> listaActividadesDesarrollar = new List<T_actividades_desarrollar>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Actividades_Desarrollar"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar tad = new T_actividades_desarrollar();
                            tad.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            tad.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
                            tad.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            tad.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            tad.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            tad.act_desa_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_finalizada"]);
                            tad.est_act_id = DataUtil.DbValueToDefault<int>(reader["est_act_id"]);
                            tad.usu_revisor = DataUtil.DbValueToDefault<int>(reader["usu_revisor"]);
                            tad.usu_asignado = DataUtil.DbValueToDefault<int>(reader["usu_asignado"]);
                            tad.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            tad.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            tad.act_desa_archivourl = DataUtil.DbValueToDefault<string>(reader["act_desa_archivourl"]);
                            tad.act_desa_revisor_obs = DataUtil.DbValueToDefault<string>(reader["act_desa_revisor_obs"]);
                            tad.act_desa_comentario = DataUtil.DbValueToDefault<string>(reader["act_desa_comentario"]);
                            tad.act_desa_archivo_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_archivo_nombre"]);
                            listaActividadesDesarrollar.Add(tad);
                        }
                    }
                }
            }
            catch
            {
                return new List<T_actividades_desarrollar>();
            }
            return listaActividadesDesarrollar;
        }
        public List<T_actividades_desarrollar> sp_Listar_Actividades_Desarrollar_por_ExpedienteId(int exp_id)
        {
            List<T_actividades_desarrollar> listaActividadesDesarrollar = new List<T_actividades_desarrollar>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Actividades_Desarrollar_por_ExpedienteId"))
                {
                    Database.AddInParameter(command, "@exp_id", DbType.Int32, exp_id);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar tad = new T_actividades_desarrollar();
                            tad.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            tad.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
                            tad.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            tad.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            tad.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            tad.act_desa_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_finalizada"]);
                            tad.est_act_id = DataUtil.DbValueToDefault<int>(reader["est_act_id"]);
                            tad.usu_revisor = DataUtil.DbValueToDefault<int>(reader["usu_revisor"]);
                            tad.usu_asignado = DataUtil.DbValueToDefault<int>(reader["usu_asignado"]);
                            tad.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            tad.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            tad.act_desa_archivourl = DataUtil.DbValueToDefault<string>(reader["act_desa_archivourl"]);
                            tad.act_desa_revisor_obs = DataUtil.DbValueToDefault<string>(reader["act_desa_revisor_obs"]);
                            tad.act_desa_comentario = DataUtil.DbValueToDefault<string>(reader["act_desa_comentario"]);
                            tad.act_desa_archivo_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_archivo_nombre"]);
                            listaActividadesDesarrollar.Add(tad);
                        }
                    }
                }
            }
            catch
            {
                return new List<T_actividades_desarrollar>();
            }
            return listaActividadesDesarrollar;
        }
        /*no completo*/
        public List<T_actividades_desarrollar_aux> sp_Listar_Actividades_Desarrollar_Aux()
        {
            List<T_actividades_desarrollar_aux> listaActividadesDesarrollarAux = new List<T_actividades_desarrollar_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Listar_Actividades_Desarrollar_Aux"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar_aux tad = new T_actividades_desarrollar_aux();
                            tad.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            tad.exp_nombre = DataUtil.DbValueToDefault<string>(reader["exp_nombre"]);
                            tad.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            tad.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            tad.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            tad.act_desa_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_finalizada"]);
                            tad.est_act_nombre = DataUtil.DbValueToDefault<string>(reader["est_act_nombre"]);
                            //cambiar en la actualizacion
                            tad.usu_revisor = DataUtil.DbValueToDefault<int>(reader["usu_revisor"]);
                            tad.usu_asignado = DataUtil.DbValueToDefault<int>(reader["usu_asignado"]);
                            tad.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            tad.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            listaActividadesDesarrollarAux.Add(tad);
                        }
                    }
                }
            }
            catch
            {
                return new List<T_actividades_desarrollar_aux>();
            }
            return listaActividadesDesarrollarAux;
        }


        public OperationResult sp_actualizar_actividades_Desarrollar_asignacion(T_actividades_desarrollar act_desa)
        {
            var OperationResult = new OperationResult();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Actividades_Desarrollar"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, act_desa.act_desa_id);
                    Database.AddInParameter(command, "@act_desa_fecha_inicio", DbType.Date, act_desa.act_desa_fecha_inicio);
                    Database.AddInParameter(command, "@act_desa_fecha_fin", DbType.Date, act_desa.act_desa_fecha_fin);
                    Database.AddInParameter(command, "@usu_revisor", DbType.Int32, act_desa.usu_revisor);
                    Database.AddInParameter(command, "@usu_asignado", DbType.Int32, act_desa.usu_asignado);
                    Database.ExecuteScalar(command);
                    OperationResult.NewId = 1;
                }
                return OperationResult;
            } 
            catch(Exception)
            {
                return new OperationResult();
            }
        }
        public List<T_usuario> sp_listar_usuarios()
        {
            List<T_usuario> listaUsuarios = new List<T_usuario>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_usuario"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_usuario usuario = new T_usuario();
                            usuario.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            usuario.usu_usuario = DataUtil.DbValueToDefault<string>(reader["usu_usuario"]);
                            usuario.usu_contraseña = DataUtil.DbValueToDefault<string>(reader["usu_contraseña"]);
                            usuario.estado = DataUtil.DbValueToDefault<bool>(reader["usu_estado"]);
                            usuario.usu_trabajador = DataUtil.DbValueToDefault<bool>(reader["usu_trabajador"]);
                            listaUsuarios.Add(usuario);
                        }
                        return listaUsuarios;
                    }
                }
            }
            catch(Exception)
            {
                return new List<T_usuario>();
            }
        }
        public List<T_detalle_usuario> sp_listar_detalle_usuario_trabajador()
        {
            List<T_detalle_usuario> listaDetalleUsuario = new List<T_detalle_usuario>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_detalle_trabajador"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_detalle_usuario det_usu = new T_detalle_usuario();
                            det_usu.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            det_usu.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            det_usu.det_usu_correo = DataUtil.DbValueToDefault<string>(reader["det_usu_correo"]);
                            det_usu.det_usu_direccion = DataUtil.DbValueToDefault<string>(reader["det_usu_direccion"]);
                            det_usu.det_usu_telefono = DataUtil.DbValueToDefault<string>(reader["det_usu_telefono"]);
                            det_usu.det_usu_sexo = DataUtil.DbValueToDefault<string>(reader["det_usu_sexo"]);
                            det_usu.det_usu_tip_doc = DataUtil.DbValueToDefault<int>(reader["det_usu_tip_doc"]);
                            det_usu.det_usu_tip_doc_numero = DataUtil.DbValueToDefault<string>(reader["det_usu_tip_doc_numero"]);
                            //det_usu.det_usu_imagem = DataUtil.DbValueToDefault<byte[]>(reader["det_usu_imagen"]);
                            det_usu.tipo_det_usu_tipo = DataUtil.DbValueToDefault<int>(reader["tipo_det_usu_tipo"]);
                            det_usu.det_usu_especialidad = DataUtil.DbValueToDefault<string>(reader["det_usu_especialidad"]);
                            listaDetalleUsuario.Add(det_usu);
                        }
                        return listaDetalleUsuario;
                    }
                }
            }
            catch
            {
                return new List<T_detalle_usuario>();
            }
        }
        public List<T_rol_usuario_Aux> sp_listar_roles_usuario()
        {
            List<T_rol_usuario_Aux> listaRolUsuario = new List<T_rol_usuario_Aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_roles_usuario"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_rol_usuario_Aux usu_rol = new T_rol_usuario_Aux();
                            usu_rol.usu_codigo = DataUtil.DbValueToDefault<int>(reader["usu_codigo"]);
                            usu_rol.rol_nombre = DataUtil.DbValueToDefault<string>(reader["rol_nombre"]);
                            listaRolUsuario.Add(usu_rol);
                        }
                        return listaRolUsuario;
                    }
                }
            }
            catch(Exception)
            {
                return new List<T_rol_usuario_Aux>();
            }
        }
        public OperationResult sp_registrar_actividades_desarrollar_auditoria(T_auditoria_actividades_desarrollo act_desa_audi)
        {
            var OperationResult = new OperationResult();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_registrar_actividades_desarrollar_auditoria"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, act_desa_audi.act_desa_id);
                    Database.AddInParameter(command, "@audi_act_comentario", DbType.String, act_desa_audi.audi_act_comentario);
                    Database.AddInParameter(command, "@audi_act_revisor", DbType.Int32, act_desa_audi.audi_act_revisor);
                    Database.AddInParameter(command, "@audi_act_fecha_inicio", DbType.Date, act_desa_audi.audi_act_fecha_inicio);
                    Database.AddInParameter(command, "@audi_act_fecha_fin", DbType.Date, act_desa_audi.audi_act_fecha_fin);
                    Database.AddInParameter(command, "@usu_asignado", DbType.Int32, act_desa_audi.usu_asignado);//@audi_act_fecha_auditoria
                    Database.AddInParameter(command, "@audi_act_nombre", DbType.String, act_desa_audi.audi_act_nombre);

                    Database.ExecuteScalar(command);
                    OperationResult.NewId = 1;
                }
                return OperationResult;
            }
            catch (Exception)
            {
                return new OperationResult();
            }
        }

        public List<T_actividades_desarrollar> sp_Consultar_Lista_Actividades_Desarrollar_Revisar_Gerente(int usucod)
        {
            try
            {
                List<T_actividades_desarrollar> list_actividades_desarrollar = new List<T_actividades_desarrollar>();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Actividades_Desarrollar_Revisar_Gerente"))
                {
                    Database.AddInParameter(command, "@usu_creador", DbType.Int32, usucod);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar actividades_desarrollar = new T_actividades_desarrollar();
                            actividades_desarrollar.exp_id = DataUtil.DbValueToDefault<int>(reader["exp_id"]);
                            actividades_desarrollar.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            actividades_desarrollar.usu_creador = DataUtil.DbValueToDefault<int>(reader["usu_creador"]);
                            actividades_desarrollar.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            actividades_desarrollar.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            actividades_desarrollar.act_desa_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_finalizada"]);
                            actividades_desarrollar.est_act_id = DataUtil.DbValueToDefault<int>(reader["est_act_id"]);
                            actividades_desarrollar.usu_revisor = DataUtil.DbValueToDefault<int>(reader["usu_revisor"]);
                            actividades_desarrollar.usu_asignado = DataUtil.DbValueToDefault<int>(reader["usu_asignado"]);
                            actividades_desarrollar.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            actividades_desarrollar.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            actividades_desarrollar.act_desa_archivourl = DataUtil.DbValueToDefault<string>(reader["act_desa_archivourl"]);
                            actividades_desarrollar.act_desa_revisor_obs = DataUtil.DbValueToDefault<string>(reader["act_desa_revisor_obs"]);
                            actividades_desarrollar.act_desa_comentario = DataUtil.DbValueToDefault<string>(reader["act_desa_comentario"]);
                            actividades_desarrollar.act_desa_archivo_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_archivo_nombre"]);
                            actividades_desarrollar.act_desa_fecha_revision = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_revision"]);
                            list_actividades_desarrollar.Add(actividades_desarrollar);
                        }
                    }
                }
                return list_actividades_desarrollar;
            }
            catch (Exception)
            {
                return new List<T_actividades_desarrollar>();
            }
        }

        public T_actividades_desarrollar_gerente sp_Consultar_Actividad_Desarrollar_Gerente(int idactividadesarrollar)
        {
            T_actividades_desarrollar_gerente actividades_desarrollar = new T_actividades_desarrollar_gerente();
            try
            {

                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Actividad_Desarrollar_Gerente"))
                {                                                   //Consultar el id q se guarda usa en la sesion
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, idactividadesarrollar);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {

                        while (reader.Read())
                        {
                            actividades_desarrollar.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            actividades_desarrollar.est_act_id = DataUtil.DbValueToDefault<int>(reader["est_act_id"]);
                            actividades_desarrollar.act_desa_descripcion = DataUtil.DbValueToDefault<string>(reader["act_desa_descripcion"]);
                            actividades_desarrollar.act_desa_revisor_obs = DataUtil.DbValueToDefault<string>(reader["act_desa_revisor_obs"]);
                            actividades_desarrollar.act_desa_comentario = DataUtil.DbValueToDefault<string>(reader["act_desa_comentario"]);
                            actividades_desarrollar.act_desa_archivo_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_archivo_nombre"]);
                            actividades_desarrollar.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            actividades_desarrollar.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            actividades_desarrollar.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            actividades_desarrollar.act_desa_fecha_finalizada = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_finalizada"]);
                            actividades_desarrollar.emp_razon_social = DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);

                        }
                    }
                }
                return actividades_desarrollar;
            }
            catch (Exception)
            {
                return new T_actividades_desarrollar_gerente();
            }
        }
        public OperationResult sp_Actualizar_Actividades_Desarrollar_Gerente(T_actividades_desarrollar_gerente actividades_desarrollar)
        {
            var operation = new OperationResult();
            using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Actividades_Desarrollar_Gerente"))
            {
                Database.AddInParameter(command, "@act_desa_id", DbType.Int32, actividades_desarrollar.act_desa_id);
                Database.AddInParameter(command, "@act_desa_revisor_obs", DbType.String, actividades_desarrollar.act_desa_revisor_obs);
                Database.AddInParameter(command, "@est_act_id", DbType.Int32, actividades_desarrollar.est_act_id);
                Database.AddInParameter(command, "@fechafin", DbType.Date, actividades_desarrollar.act_desa_fecha_fin);
                Database.ExecuteScalar(command);
                operation.NewId = 1;
            }
            return operation;
        }



    }
}
