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
    public class TrabajadorDataAccess : BaseConexion
    {
        
        public OperationResult sp_Insertar_Imagen_Usuario(T_detalle_usuario usuario)
        {
            try
            {
                var operationresult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Imagen_Usuario"))
                {
                    Database.AddInParameter(command, "@imagen", DbType.Binary, usuario.det_usu_imagem);
                    Database.AddInParameter(command, "@codigo", DbType.Int32, usuario.usu_codigo);
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
        public List<T_actividades_desarrollar> sp_listar_plan_por_usuario_asignado(int usu_asignado)
        {
            List<T_actividades_desarrollar> lista_actividades_desarrollar = new List<T_actividades_desarrollar>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_plan_por_usuario_asignado"))
                {
                    Database.AddInParameter(command, "@usu_asignado", DbType.Int32, usu_asignado);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_actividades_desarrollar t_Act_des = new T_actividades_desarrollar();
                            t_Act_des.act_desa_id = DataUtil.DbValueToDefault<int>(reader["act_desa_id"]);
                            t_Act_des.act_desa_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_nombre"]);
                            t_Act_des.act_desa_fecha_inicio = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_inicio"]);
                            t_Act_des.act_desa_fecha_fin = DataUtil.DbValueToDefault<DateTime>(reader["act_desa_fecha_fin"]);
                            t_Act_des.act_desa_archivo_nombre = DataUtil.DbValueToDefault<string>(reader["act_desa_archivo_nombre"]);
                            lista_actividades_desarrollar.Add(t_Act_des);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_actividades_desarrollar>();
            }
            return lista_actividades_desarrollar;
        }
        public OperationResult sp_Agregar_Tarea_Asignada(T_actividades_desarrollar t_act_desa)
        {
            try
            {
                var operationresult = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Agregar_Tarea_Asignada"))
                {
                    Database.AddInParameter(command, "@act_desa_id", DbType.Int32, t_act_desa.act_desa_id);
                    Database.AddInParameter(command, "@act_desa_archivo_nombre", DbType.String, t_act_desa.act_desa_archivo_nombre);
                    Database.AddInParameter(command, "@act_desa_archivourl", DbType.String, t_act_desa.act_desa_archivourl);
                    Database.AddInParameter(command, "@act_desa_comentario", DbType.String, t_act_desa.act_desa_comentario);
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
