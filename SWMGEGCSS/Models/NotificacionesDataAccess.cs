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
                T_notificaciones.usu_codigo="+usu_codigo+"", connectionString);
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

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }
    }
}