﻿using System;
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
    public class SecretariaDataAccess : BaseConexion
    {

        public List<T_Citas_aux> sp_Consultar_Lista_Citas()
        {
            var l_citas = new List<T_Citas_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Citas"))
                {

                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var cita = new T_Citas_aux();
                            cita.cita_id = DataUtil.DbValueToDefault<Int32>(reader["cita_id"]);
                            cita.estado_cita_nombre = DataUtil.DbValueToDefault<string>(reader["estado_cita_nombre"]);
                            cita.usu_citado = DataUtil.DbValueToDefault<Int32>(reader["usu_citado"]);
                            cita.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            cita.cita_comentario = DataUtil.DbValueToDefault<string>(reader["cita_comentario"]);
                            cita.estado_cita_id = DataUtil.DbValueToDefault<Int32>(reader["estado_cita_id"]);
                            cita.cita_fecha = DataUtil.DbValueToDefault<DateTime>(reader["cita_fecha"]);
                            cita.cita_empresa = DataUtil.DbValueToDefault<string>(reader["cita_empresa"]);
                            cita.cita_representante = DataUtil.DbValueToDefault<string>(reader["cita_representante"]);
                            cita.cita_correo = DataUtil.DbValueToDefault<string>(reader["citas_correo"]);
                            cita.cita_fecha_atendido = DataUtil.DbValueToDefault<DateTime>(reader["cita_fecha_atendido"]);
                            cita.cita_telefono = DataUtil.DbValueToDefault<string>(reader["cita_telefono"]);
                            cita.usu_usuario = DataUtil.DbValueToDefault<string>(reader["usu_usuario"]);
                            l_citas.Add(cita);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e + " ");
                return new List<T_Citas_aux>();
            }
            return l_citas;
        }
        //Busca todos los estados
        public List<T_estado_cita> sp_Consultar_Lista_Estado_Cita()
        {
            var l_estado_cita = new List<T_estado_cita>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Estado_Cita"))
                {
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var estado = new T_estado_cita();
                            estado.estado_cita_id = DataUtil.DbValueToDefault<int>(reader["estado_cita_id"]);
                            estado.estado_cita_descripcion = DataUtil.DbValueToDefault<string>(reader["estado_cita_descripcion"]);
                            estado.estado_cita_nombre = DataUtil.DbValueToDefault<string>(reader["estado_cita_nombre"]);
                            l_estado_cita.Add(estado);
                        }
                    }
                }
            }
            catch (Exception)
            {

                return new List<T_estado_cita>();
            }
            return l_estado_cita;

        }

        //Busca por estados
        public List<T_Citas_aux> sp_Consultar_Lista_Cita_Estado(string estado)
        {
            var l_cita_estado = new List<T_Citas_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Citas_Estado"))
                {
                    Database.AddInParameter(command, "@estado_cita_id", DbType.Int32, Convert.ToInt32(estado));
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var cita = new T_Citas_aux();
                            cita.cita_id = DataUtil.DbValueToDefault<Int32>(reader["cita_id"]);
                            cita.estado_cita_nombre = DataUtil.DbValueToDefault<string>(reader["estado_cita_nombre"]);
                            cita.usu_citado = DataUtil.DbValueToDefault<Int32>(reader["usu_citado"]);
                            cita.det_usu_nombre = DataUtil.DbValueToDefault<string>(reader["det_usu_nombre"]);
                            cita.cita_comentario = DataUtil.DbValueToDefault<string>(reader["cita_comentario"]);
                            cita.estado_cita_id = DataUtil.DbValueToDefault<Int32>(reader["estado_cita_id"]);
                            cita.cita_fecha = DataUtil.DbValueToDefault<DateTime>(reader["cita_fecha"]);
                            cita.cita_empresa = DataUtil.DbValueToDefault<string>(reader["cita_empresa"]);
                            cita.cita_representante = DataUtil.DbValueToDefault<string>(reader["cita_representante"]);
                            cita.cita_correo = DataUtil.DbValueToDefault<string>(reader["citas_correo"]);
                            cita.cita_telefono = DataUtil.DbValueToDefault<string>(reader["cita_telefono"]);
                            cita.usu_usuario = DataUtil.DbValueToDefault<string>(reader["usu_usuario"]);
                            l_cita_estado.Add(cita);
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.Write(e + " ");
                return new List<T_Citas_aux>();
            }
            return l_cita_estado;
        }


        public OperationResult sp_Insertar_Cita(T_Citas citas, string cita_empresa, string usu_citado, string cita_hora)
        {

            try
            {
                var operation = new OperationResult();
                var fecha = citas.cita_fecha.ToString("yyyy-MM-dd") + " " + cita_hora +":00";
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Cita"))
                {
                    Database.AddInParameter(command, "@cita_representante", DbType.String, citas.cita_representante);
                    Database.AddInParameter(command, "@cita_fecha", DbType.DateTime, fecha);
                    Database.AddInParameter(command, "@usu_citado", DbType.Int32, usu_citado);
                    Database.AddInParameter(command, "@cita_comentario", DbType.String, citas.cita_comentario);
                    Database.AddInParameter(command, "@cita_empresa", DbType.String, cita_empresa);
                    Database.AddInParameter(command, "@cita_correo", DbType.String, citas.cita_correo);
                    Database.AddInParameter(command, "@cita_telefono", DbType.String, citas.cita_telefono);
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

        public OperationResult sp_Modificar_Cita(T_Citas_aux citas, string cita_hora_atendido, string cita_hora, int usu_citado)
        {
            try
            {
                var operation = new OperationResult();
                var fecha = citas.cita_fecha.ToString("yyyy-MM-dd") + " " + cita_hora;
                var fecha_atendido = citas.cita_fecha_atendido.ToString("yyyy-MM-dd") + " " + cita_hora_atendido + ":00";
                if(fecha_atendido == "0001-01-01 00:00:00")
                {
                    fecha_atendido = null;
                }
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Cita"))
                {
                    Database.AddInParameter(command, "@cita_id", DbType.Int32, citas.cita_id);
                    Database.AddInParameter(command, "@cita_representante", DbType.String, citas.cita_representante);
                    Database.AddInParameter(command, "@cita_fecha", DbType.DateTime, fecha);
                    Database.AddInParameter(command, "@cita_comentario", DbType.String, citas.cita_comentario);
                    Database.AddInParameter(command, "@usu_citado", DbType.Int32, usu_citado);
                    Database.AddInParameter(command, "@cita_empresa", DbType.String, citas.cita_empresa);
                    Database.AddInParameter(command, "@citas_correo", DbType.String, citas.cita_correo);
                    Database.AddInParameter(command, "@cita_fecha_atendido", DbType.DateTime, fecha_atendido);
                    Database.AddInParameter(command, "@cita_telefono", DbType.String, citas.cita_telefono);
                    Database.AddInParameter(command, "@estado_cita_id", DbType.Int32, citas.estado_cita_id);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                    
                }
                return operation;
            }
            catch (Exception e)
            {

                return new OperationResult();
            }
            
        }


        public OperationResult sp_Eliminar_Cita(T_Citas citas)
        {
            try
            {
                var operation = new OperationResult();
                using(DbCommand command = Database.GetStoredProcCommand("sp_Eliminar_Cita"))
                {
                    Database.AddInParameter(command, "@cita_id", DbType.Int32, citas.cita_id);
                    Database.AddInParameter(command, "@estado_cita_id", DbType.Int32, citas.estado_cita_id);
                    Database.ExecuteScalar(command);
                    operation.NewId = 1;
                }
                return operation;
            }
            catch (Exception e)
            {

                return new OperationResult();
            }
        }

    }
}
