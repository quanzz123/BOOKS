using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace BOOKS
{
    class DBServices
    {
        private string Str = @"Data Source=NEYAQUAN\HONGQUAN;Initial Catalog=BOOKRENTAILS;Integrated Security=True;";
        private SqlConnection conn;

        public DBServices() {
            conn = new SqlConnection(Str);

        }


        public DataTable getData(string ssql) {
            try
            {
                SqlDataAdapter dta = new SqlDataAdapter(ssql, conn);
                DataTable dtbl = new DataTable();
                dta.Fill(dtbl);
                return dtbl;
                //đoạn code thay thế cho sqlDataAdapter 
                /*SqlCommand cmd = new SqlCommand(ssql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dtbl = new DataTable();
                dtbl.Load(reader);
                conn.Close();
                return dtbl;*/

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void runquery(string ssql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(ssql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
           
       
    } 
       
}