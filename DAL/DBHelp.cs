using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelp
    {
        static string qwe = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;
        /// <summary>
        /// 添加 修改 删除
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="isProc">是存储过程默认false</param>
        /// <param name="pare">参数 参数集合</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, bool isProc = false, params SqlParameter[] pare)
        {
            SqlConnection con = new SqlConnection(qwe);

            SqlCommand com = new SqlCommand(sql, con);
            if (isProc)
            {
                com.CommandType = CommandType.StoredProcedure;
            }
            if (pare.Length > 0)
            {
                com.Parameters.AddRange(pare);
            }
            try
            {
                con.Open();
                return com.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                con.Close();
            }

        }
        public static SqlCommand GetCom(string sql, bool isProc, params SqlParameter[] pare)
        {
            SqlConnection con = new SqlConnection(qwe);
            SqlCommand com = new SqlCommand(sql, con);
            if (isProc)
            {
                com.CommandType = CommandType.StoredProcedure;
            }
            if (pare.Length > 0)
            {
                com.Parameters.AddRange(pare);
            }
            return com;
        }
        public static DataTable GetSelect(string sql, bool isProc = false, params SqlParameter[] pare)
        {
            SqlConnection con = new SqlConnection(qwe);
            SqlCommand com = GetCom(sql, isProc, pare);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable daTble = new DataTable();
            try
            {
                con.Open();
                da.Fill(daTble);
                return daTble;
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
