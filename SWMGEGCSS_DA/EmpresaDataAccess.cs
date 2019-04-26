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
    public class EmpresaDataAccess : BaseConexion
    {
        public List<T_empresa> sp_Consultar_Lista_Empresa()
        {
            var l_empresa = new List<T_empresa>();
            try
            {
                using (DbCommand command = Database.GetStoredProcCommand("sp_Consultar_Lista_Empresa"))
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
                            empresa.emp_id = DataUtil.DbValueToDefault<int>(reader["emp_id"]);
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
    }


    }

