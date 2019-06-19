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
    public class Ing_EgrDataAccess : BaseConexion
    {
        public List<T_ingresos_egresos> sp_Consultar_Lista_Ing_Egr()
        {
            var l_ing_egr = new List<T_ingresos_egresos>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Ing_Egr"))
                {

                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var ing_egr = new T_ingresos_egresos();
                            ing_egr.ing_egr_nombre = DataUtil.DbValueToDefault<string>(reader["ing_egr_nombre"]);
                            ing_egr.ing_egr_descripcion = DataUtil.DbValueToDefault<string>(reader["ing_egr_descripcion"]);
                            ing_egr.ing_egr_fecha = DataUtil.DbValueToDefault<DateTime>(reader["ing_egr_fecha"]);
                            ing_egr.ing_egr_ingrso = DataUtil.DbValueToDefault<bool>(reader["ing_egr_ingreso"]);
                            ing_egr.ing_egr_monto = DataUtil.DbValueToDefault<double>(reader["ing_egr_monto"]);
                            ing_egr.ing_egr_id = DataUtil.DbValueToDefault<int>(reader["ing_egr_id"]);
                            l_ing_egr.Add(ing_egr);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return new List<T_ingresos_egresos>();
            }
            return l_ing_egr;
        }


        public List<T_ingresos_egresos> sp_Consultar_Lista_Ing_Egr_X_fecha(DateTime fechaIni, DateTime fechafin)
        {
            var l_ing_egr = new List<T_ingresos_egresos>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_listar_I_E_x_fecha_aux"))
                {
                    Database.AddInParameter(command, "@fechaini", DbType.Date, fechaIni);
                    Database.AddInParameter(command, "@fechafin", DbType.Date, fechafin);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var ing_egr = new T_ingresos_egresos();
                            ing_egr.ing_egr_nombre = DataUtil.DbValueToDefault<string>(reader["ing_egr_nombre"]);
                            ing_egr.ing_egr_descripcion = DataUtil.DbValueToDefault<string>(reader["ing_egr_descripcion"]);
                            ing_egr.ing_egr_fecha = DataUtil.DbValueToDefault<DateTime>(reader["ing_egr_fecha"]);
                            ing_egr.ing_egr_ingrso = DataUtil.DbValueToDefault<bool>(reader["ing_egr_ingreso"]);
                            ing_egr.ing_egr_monto = DataUtil.DbValueToDefault<double>(reader["ing_egr_monto"]);
                            ing_egr.ing_egr_id = DataUtil.DbValueToDefault<int>(reader["ing_egr_id"]);
                            l_ing_egr.Add(ing_egr);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return new List<T_ingresos_egresos>();
            }
            return l_ing_egr;
        }



        public List<T_ingresos_egresos> sp_Consultar_Lista_Ing_Egr_Estado(string estado)
        {
            var l_ing_egr = new List<T_ingresos_egresos>();
            try
            {

                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Ing_Egr_Estado"))
                {
                    Database.AddInParameter(command, "@estado", DbType.String, estado);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var ing_egr = new T_ingresos_egresos();
                            ing_egr.ing_egr_nombre = DataUtil.DbValueToDefault<string>(reader["ing_egr_nombre"]);
                            ing_egr.ing_egr_descripcion = DataUtil.DbValueToDefault<string>(reader["ing_egr_descripcion"]);
                            ing_egr.ing_egr_fecha = DataUtil.DbValueToDefault<DateTime>(reader["ing_egr_fecha"]);
                            ing_egr.ing_egr_ingrso = DataUtil.DbValueToDefault<bool>(reader["ing_egr_ingreso"]);
                            ing_egr.ing_egr_monto = DataUtil.DbValueToDefault<double>(reader["ing_egr_monto"]);
                            ing_egr.ing_egr_id = DataUtil.DbValueToDefault<int>(reader["ing_egr_id"]);
                            l_ing_egr.Add(ing_egr);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return new List<T_ingresos_egresos>();
            }
            return l_ing_egr;
        }



        public OperationResult sp_Insertar_Ing_Egr(T_ingresos_egresos Ingresos_Egresos)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Ing_Egr"))
                {
                    Database.AddInParameter(command, "@ing_egr_nombre", DbType.String, Ingresos_Egresos.ing_egr_nombre);
                    Database.AddInParameter(command, "@ing_egr_descripcion", DbType.String, Ingresos_Egresos.ing_egr_descripcion);
                    Database.AddInParameter(command, "@ing_egr_fecha", DbType.Date, Ingresos_Egresos.ing_egr_fecha);
                    Database.AddInParameter(command, "@ing_egr_ingreso", DbType.Int32, Ingresos_Egresos.ing_egr_ingrso);
                    Database.AddInParameter(command, "@ing_egr_monto", DbType.Double, Ingresos_Egresos.ing_egr_monto);
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, Ingresos_Egresos.usu_codigo);

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

        public OperationResult sp_Actualizar_Ing_Egr(T_ingresos_egresos Ingresos_Egresos)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Ing_Egr"))
                {
                    Database.AddInParameter(command, "@ing_egr_nombre", DbType.String, Ingresos_Egresos.ing_egr_nombre);
                    Database.AddInParameter(command, "@ing_egr_descripcion", DbType.String, Ingresos_Egresos.ing_egr_descripcion);
                    Database.AddInParameter(command, "@ing_egr_fecha", DbType.Date, Ingresos_Egresos.ing_egr_fecha);
                    Database.AddInParameter(command, "@ing_egr_id", DbType.Int32, Ingresos_Egresos.ing_egr_id);
                    Database.AddInParameter(command, "@ing_egr_monto", DbType.Double, Ingresos_Egresos.ing_egr_monto);
                 
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
        public List<T_ingresos_egresos> sp_Consultar_Lista_Ing_Egr_Nombre(string ing_egr_nombre)
        {
            List<T_ingresos_egresos> T_ingresos_egresos = new List<T_ingresos_egresos>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Ing_Egr_Nombre"))
                {

                    Database.AddInParameter(command, "@ing_egr_nombre", DbType.String, ing_egr_nombre);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_ingresos_egresos ing_egr = new T_ingresos_egresos();


                            ing_egr.ing_egr_nombre = DataUtil.DbValueToDefault<string>(reader["ing_egr_nombre"]);
                            ing_egr.ing_egr_descripcion = DataUtil.DbValueToDefault<string>(reader["ing_egr_descripcion"]);
                            ing_egr.ing_egr_fecha = DataUtil.DbValueToDefault<DateTime>(reader["ing_egr_fecha"]);
                            ing_egr.ing_egr_ingrso = DataUtil.DbValueToDefault<bool>(reader["ing_egr_ingreso"]);
                            ing_egr.ing_egr_monto = DataUtil.DbValueToDefault<double>(reader["ing_egr_monto"]);
                            ing_egr.ing_egr_id = DataUtil.DbValueToDefault<int>(reader["ing_egr_id"]);
                            T_ingresos_egresos.Add(ing_egr);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_ingresos_egresos>();
            }
            return T_ingresos_egresos;
        }

        public List<T_ingresos_egresos> sp_Consultar_Lista_Ing_Egr_Nombre_Estado(string ing_egr_nombre, string estado)
        {
            List<T_ingresos_egresos> T_ingresos_egresos = new List<T_ingresos_egresos>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Ing_Egr_Nombre_Estado"))
                {

                    Database.AddInParameter(command, "@ing_egr_nombre", DbType.String, ing_egr_nombre);
                    Database.AddInParameter(command, "@estado", DbType.String, estado);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_ingresos_egresos ing_egr = new T_ingresos_egresos();


                            ing_egr.ing_egr_nombre = DataUtil.DbValueToDefault<string>(reader["ing_egr_nombre"]);
                            ing_egr.ing_egr_descripcion = DataUtil.DbValueToDefault<string>(reader["ing_egr_descripcion"]);
                            ing_egr.ing_egr_fecha = DataUtil.DbValueToDefault<DateTime>(reader["ing_egr_fecha"]);
                            ing_egr.ing_egr_ingrso = DataUtil.DbValueToDefault<bool>(reader["ing_egr_ingreso"]);
                            ing_egr.ing_egr_monto = DataUtil.DbValueToDefault<double>(reader["ing_egr_monto"]);
                            ing_egr.ing_egr_id = DataUtil.DbValueToDefault<int>(reader["ing_egr_id"]);
                            T_ingresos_egresos.Add(ing_egr);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_ingresos_egresos>();
            }
            return T_ingresos_egresos;
        }
    }
}
