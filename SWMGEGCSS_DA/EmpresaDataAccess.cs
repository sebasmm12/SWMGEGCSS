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
    public class EmpresaDataAccess : BaseConexion
    {
        public List<T_empresa> sp_Consultar_Lista_Empresas()
        {
            var l_empresa = new List<T_empresa>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Empresas"))
                {
                    
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            var empresa = new T_empresa();
                            empresa.emp_direccion = DataUtil.DbValueToDefault<string>(reader["emp_direccion"]);
                            empresa.emp_email = DataUtil.DbValueToDefault<string>(reader["emp_email"]);
                            empresa.emp_estado = DataUtil.DbValueToDefault<bool>(reader["emp_estado"]);
                            empresa.emp_fax = DataUtil.DbValueToDefault<string>(reader["emp_fax"]);
                            empresa.emp_razon_social= DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);
                            empresa.emp_representante = DataUtil.DbValueToDefault<string>(reader["emp_representante"]);
                            empresa.emp_ruc = DataUtil.DbValueToDefault<string>(reader["emp_ruc"]);
                            empresa.emp_sigla = DataUtil.DbValueToDefault<string>(reader["emp_sigla"]);
                            empresa.emp_telefono = DataUtil.DbValueToDefault<string>(reader["emp_telefono"]);
                            l_empresa.Add(empresa);
                        }

                    }
                }
            }
            catch (Exception)
            {

                return new List<T_empresa>();
            }
            return l_empresa;
        }
        public List<T_empresa> sp_Consultar_Lista_Nombre_Empresa(string emp_razon_social)
        {
            List<T_empresa> T_Empresa = new List<T_empresa>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Nombre_Empresa"))
                {

                    Database.AddInParameter(command, "@emp_nombre", DbType.String, emp_razon_social);
                    using (IDataReader reader = Database.ExecuteReader(command))
                    {
                        while (reader.Read())
                        {
                            T_empresa empresa = new T_empresa();

                            empresa.emp_direccion = DataUtil.DbValueToDefault<string>(reader["emp_direccion"]);
                            empresa.emp_email = DataUtil.DbValueToDefault<string>(reader["emp_email"]);
                            empresa.emp_estado = DataUtil.DbValueToDefault<bool>(reader["emp_estado"]);
                            empresa.emp_fax = DataUtil.DbValueToDefault<string>(reader["emp_fax"]);
                            empresa.emp_razon_social = DataUtil.DbValueToDefault<string>(reader["emp_razon_social"]);
                            empresa.emp_representante = DataUtil.DbValueToDefault<string>(reader["emp_representante"]);
                            empresa.emp_ruc = DataUtil.DbValueToDefault<string>(reader["emp_ruc"]);
                            empresa.emp_sigla = DataUtil.DbValueToDefault<string>(reader["emp_sigla"]);
                            empresa.emp_telefono = DataUtil.DbValueToDefault<string>(reader["emp_telefono"]);
                            T_Empresa.Add(empresa);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new List<T_empresa>();
            }
            return T_Empresa;
        }

       public OperationResult sp_Insertar_Empresa(T_empresa Empresa)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Insertar_Empresa"))
                {
                    Database.AddInParameter(command, "@emp_ruc", DbType.String,Empresa.emp_ruc);
                    Database.AddInParameter(command, "@emp_razon_social", DbType.String, Empresa.emp_razon_social);
                    Database.AddInParameter(command, "@emp_sigla", DbType.String, Empresa.emp_sigla);
                    Database.AddInParameter(command, "@emp_representante", DbType.String, Empresa.emp_representante);
                    Database.AddInParameter(command, "@emp_direccion", DbType.String, Empresa.emp_direccion);
                    Database.AddInParameter(command, "@emp_telefono", DbType.String, Empresa.emp_telefono);
                    Database.AddInParameter(command, "@emp_fax", DbType.String, Empresa.emp_fax);
                    Database.AddInParameter(command, "@emp_email", DbType.String, Empresa.emp_email);
                    Database.AddInParameter(command, "@usu_codigo", DbType.Int32, Empresa.usu_codigo);
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

        public OperationResult sp_Actualizar_Empresa(T_empresa Empresa)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Actualizar_Empresa"))
                {
                    Database.AddInParameter(command, "@emp_ruc", DbType.String, Empresa.emp_ruc);
                    Database.AddInParameter(command, "@emp_razon_social", DbType.String, Empresa.emp_razon_social);
                    Database.AddInParameter(command, "@emp_sigla", DbType.String, Empresa.emp_sigla);
                    Database.AddInParameter(command, "@emp_representante", DbType.String, Empresa.emp_representante);
                    Database.AddInParameter(command, "@emp_direccion", DbType.String, Empresa.emp_direccion);
                    Database.AddInParameter(command, "@emp_telefono", DbType.String, Empresa.emp_telefono);
                    Database.AddInParameter(command, "@emp_fax", DbType.String, Empresa.emp_fax);
                    Database.AddInParameter(command, "@emp_email", DbType.String, Empresa.emp_email);
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


        public OperationResult sp_Eliminar_Empresa(T_empresa Empresa)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Eliminar_Empresa"))
                {

                    Database.AddInParameter(command, "@emp_ruc", DbType.String, Empresa.emp_ruc);

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

        public OperationResult sp_Activar_Empresa(T_empresa Empresa)
        {
            try
            {
                var operation = new OperationResult();
                using (DbCommand command = Database.GetStoredProcCommand("sp_Activar_Empresa"))
                {

                    Database.AddInParameter(command, "@emp_ruc", DbType.String, Empresa.emp_ruc);

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

    }


}






