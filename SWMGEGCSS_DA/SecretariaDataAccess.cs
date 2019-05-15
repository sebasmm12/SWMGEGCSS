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
    public class SecretariaDataAccess : BaseConexion
    {
        public List<T_Citas_aux> sp_Consultar_Lista_Citas()
        {
            var l_citas = new List<T_Citas_aux>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Citas"))
                {
                    using(IDataReader reader = Database.ExecuteReader(command))
                    {
                        while(reader.Read())
                        {
                            var cita = new T_Citas_aux();
                            cita.cita_id = DataUtil.DbValueToDefault<int>(reader["cita_id"]);
                            cita.cita_representante = DataUtil.DbValueToDefault<string>(reader["cita_representante"]);
                            cita.estado_cita_id = DataUtil.DbValueToDefault<int>(reader["estado_cita_id"]);
                            cita.cita_comentario = DataUtil.DbValueToDefault<string>(reader["cita_comentario"]);
                            cita.cita_fecha = DataUtil.DbValueToDefault<DateTime>(reader["cita_fecha"]);
                            cita.usu_citado = DataUtil.DbValueToDefault<int>(reader["usu_citado"]);
                            cita.cita_empresa = DataUtil.DbValueToDefault<string>(reader["cita_empresa"]);
                            cita.cita_fecha_atendido = DataUtil.DbValueToDefault<DateTime>(reader["cita_fecha_atendido"]);
                            cita.cita_correo = DataUtil.DbValueToDefault<string>(reader["citas_correo"]);
                            cita.cita_telefono = DataUtil.DbValueToDefault<string>(reader["cita_telefono"]);
                            l_citas.Add(cita);
                        }
                        
                    }
                }
            }
            catch(Exception)
            {
                return new List<T_Citas_aux>();
            }
            return l_citas;
        }

        public OperationResult sp_Insertar_Cita(T_Citas citas)
        {

            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Cita"))
                {

                    Database.AddInParameter(command, "@cita_representante", DbType.String, citas.cita_representante);
                    Database.AddInParameter(command, "@cita_fecha", DbType.DateTime, citas.cita_fecha);
                    Database.AddInParameter(command, "@cita_comentario", DbType.String, citas.cita_comentario);
                    Database.AddInParameter(command, "@cita_empresa", DbType.String, citas.cita_empresa);
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

    }
}
