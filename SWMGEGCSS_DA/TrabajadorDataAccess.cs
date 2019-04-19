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
        
    }
}
