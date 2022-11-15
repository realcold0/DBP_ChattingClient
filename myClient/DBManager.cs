using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myClient
{
    internal class DBManager
    {
        private static DBManager instance = new DBManager();
        public static DBManager GetInstance() { return instance; }
        string strconn = "server=115.85.181.212; Database=s5645730; Uid=s5645730; Pwd=s5645730; Charset=utf8";
        
        private DBManager() { }

        public void InsertOrUpdate(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(strconn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public void Delete(string query)
        {
            using (MySqlConnection conn = new MySqlConnection(strconn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }
        public ArrayList Select(string query, string column)
        {
            using (MySqlConnection conn = new MySqlConnection(strconn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                ArrayList result = new ArrayList();

                while (rdr.Read())
                {
                    result.Add(rdr[column]);
                }
                return result;
            }
        }
    }
}
