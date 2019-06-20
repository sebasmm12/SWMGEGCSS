using SWMGEGCSS.Hubs;
using SWMGEGCSS_EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SWMGEGCSS_DA.Base;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace SWMGEGCSS.Models
{
    public class NotificacionesDataAccess:BaseConexion
    {
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["cndatabase"].ConnectionString;
        public List<T_notificaciones> sp_Consultar_Notificaciones(int usu_codigo)
        {
            var messages = new List<T_notificaciones>();
          using (var connectionString = new SqlConnection(_connectionString))
            {
                connectionString.Open();
                SqlCommand command = new SqlCommand(@"SELECT [not_id],[not_nombre],[not_descripcion],[usu_codigo],[noti_fecha],[estado],[usu_envio] FROM [dbo].[T_notificaciones] where 
                T_notificaciones.usu_codigo="+usu_codigo+" ORDER BY not_id  DESC", connectionString);
                command.Notification = null;
                var dependency = new SqlDependency(command);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                if (connectionString.State == System.Data.ConnectionState.Closed)
                {
                    connectionString.Open();
                }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(item: new T_notificaciones
                    {
                        not_id = (int)reader["not_id"],
                        not_nombre = (string)reader["not_nombre"],
                        not_descripcion = (string)reader["not_descripcion"],
                        usu_codigo = (int)reader["usu_codigo"],
                        usu_envio = (int)reader["usu_envio"]
                    });
                }
            }
            return messages;
        }
        public OperationResult sp_Insertar_Notificaciones(T_notificaciones notificacion)
        {
            try
            {
                var operationResult = new OperationResult();
                using (DbCommand command=Database.GetStoredProcCommand("sp_Insertar_Notificaciones"))
                {
                    Database.AddInParameter(command, "@not_descripcion", DbType.String, notificacion.not_descripcion);
                    Database.AddInParameter(command, "@not_nombre", DbType.String, notificacion.not_nombre);
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, notificacion.usu_codigo);
                    Database.AddInParameter(command, "@usu_envio", DbType.Int32, notificacion.usu_envio);
                    operationResult.NewId = 1;
                    Database.ExecuteScalar(command);
                }
                return operationResult;
            }
            catch (Exception)
            {

                return new OperationResult();
            }
        }
        public List<T_notificaciones> sp_Consultar_Notificaciones_Top(int usu_codigo)
        {
            var messages = new List<T_notificaciones>();
            using (var connectionString = new SqlConnection(_connectionString))
            {
                connectionString.Open();
                SqlCommand command = new SqlCommand(@"SELECT TOP 3 [not_id],[not_nombre],[not_descripcion],[usu_codigo],[noti_fecha],[estado],[usu_envio] FROM [dbo].[T_notificaciones] where 
                T_notificaciones.usu_codigo=" + usu_codigo + " ORDER BY not_id  DESC", connectionString);
                command.Notification = null;
                var dependency = new SqlDependency(command);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                if (connectionString.State == System.Data.ConnectionState.Closed)
                {
                    connectionString.Open();
                }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    messages.Add(item: new T_notificaciones
                    {
                        not_id = (int)reader["not_id"],
                        not_nombre = (string)reader["not_nombre"],
                        not_descripcion = (string)reader["not_descripcion"],
                        usu_codigo = (int)reader["usu_codigo"],
                        usu_envio = (int)reader["usu_envio"],
                        noti_fecha=(DateTime)reader["noti_fecha"]
                    });
                }
            }
            return messages;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
    }
}