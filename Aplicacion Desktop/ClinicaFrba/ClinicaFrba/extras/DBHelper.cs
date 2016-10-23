using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL.Classes
{
    public class DBHelper
    {
        private static SqlConnection _conexion = new SqlConnection();

        public DBHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataSet ExecuteDataSet(string sqlSpName, SqlParameter[] dbParams)
        {
            DataSet ds = null;
            //try
            //{


            ds = new DataSet();
            SqlConnection cn = getConexion();

            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = 600;

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    da.SelectCommand.Parameters.Add(dbParam);
                }
            }
            da.Fill(ds);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return ds;
        }

        private static SqlConnection getConexion()
        {
            {
                _conexion = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
                _conexion.Open();
            }


            return _conexion;

        }

        public static bool ExecuteXml(string sqlSpName, SqlParameter[] dbParams, System.Xml.XmlDocument dXml)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
            cn.Open();
            bool bReturn;
            try
            {
                //dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dr.Read())
                    {
                        System.Data.SqlTypes.SqlXml oXml = dr.GetSqlXml(dr.GetOrdinal("Xml"));
                        dXml.LoadXml(oXml.Value);
                        bReturn = true;
                    }
                    else
                    {
                        bReturn = false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bReturn;
        }
        public static SqlDataReader ExecuteDataReader(string sqlSpName, SqlParameter[] dbParams)
        {
            SqlDataReader dr;

            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }
            cn.Open();

            try
            {
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
            return dr;
        }

        public static void ExecuteNonQuery(string sqlSpName, SqlParameter[] dbParams)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();

            }
        }

        public static object ExecuteScalar(string sqlSpName, SqlParameter[] dbParams)
        {
            object retVal = null;
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                {
                    cmd.Parameters.Add(dbParam);
                }
            }

            cn.Open();

            try
            {
                retVal = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();
            }

            return retVal;
        }

        public static SqlParameter MakeParam(string paramName, SqlDbType dbType, int size, object objValue)
        {
            SqlParameter param;

            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);
            else
                param = new SqlParameter(paramName, dbType);

            param.Value = objValue;

            return param;
        }

        public static SqlParameter MakeParamOutput(string paramName, SqlDbType dbType, int size)
        {
            SqlParameter param;

            if (size > 0)
                param = new SqlParameter(paramName, dbType, size);
            else
                param = new SqlParameter(paramName, dbType);

            param.Direction = ParameterDirection.Output;

            return param;
        }

        public static int ExecuteNonQueryOutput(string sqlSpName, SqlParameter[] dbParams, string paramName, SqlDbType dbType, int size)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings.Get("connectionString"));
            SqlCommand cmd = new SqlCommand(sqlSpName, cn);
            cmd.CommandTimeout = Convert.ToInt16(ConfigurationManager.AppSettings.Get("connectionCommandTimeout"));
            cmd.CommandType = CommandType.StoredProcedure;

            if (dbParams != null)
            {
                foreach (SqlParameter dbParam in dbParams)
                    cmd.Parameters.Add(dbParam);
            }
            SqlParameter OutParam = MakeParamOutput(paramName, dbType, size);
            cmd.Parameters.Add(OutParam);

            cn.Open();

            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (null != cn)
                    cn.Close();

            }
            if (OutParam.Value == null) return 0;
            else return System.Convert.ToInt16(OutParam.Value);
        }

        public static SqlDataReader ExecuteQuery_DR(string sqlQuery)    //Este retorna un data reader y se lee con indice
        {
            SqlConnection conexion = getConexion();

            SqlCommand comando = new SqlCommand(sqlQuery, conexion);
            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                return registro;

            }
            else
                //MessageBox.Show("No existe");
                conexion.Close();
            return null;
        }

        public static DataSet ExecuteQuery_DS(string sqlQuery)      //Este retorna un Data Set y se inserta directo en DataSource
        {
            SqlConnection conexion = getConexion();

            SqlCommand comando2 = new SqlCommand(sqlQuery, conexion);

            SqlDataAdapter adapter2 = new SqlDataAdapter(comando2);

            DataSet ds = new DataSet();

            adapter2.Fill(ds);

            return ds;

        }





        #region Example
        //public static DataSet Get(CGasto gasto)
        //{
        //    SqlParameter[] dbParams = new SqlParameter[]
        //        {
        //            DBHelper.MakeParam("@Id", SqlDbType.Int, 0, gasto.Id),
        //        };
        //    return DBHelper.ExecuteDataSet("usp_CListGasto_Get", dbParams);

        //} 
        #endregion

    }
}
