using System.Data;
using System.Xml;
using MySql.Data.MySqlClient;


namespace IntelReport.DataBase
{
    public class BaseConnetion
    {
        private string strConnection = "server=localhost;user=root;password=;database=malshinon";
        private MySqlConnection? _conn;
        public void CreateConnection()
        {
            try
            {
                this._conn = new MySqlConnection(strConnection);
                this._conn.Open();
                Console.WriteLine("Connection successpul!");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
                {
                    _conn.Close();
                    _conn = null;
                }
            }
        }

        public MySqlConnection openConnection()
        {
            if (_conn == null)
            {
                _conn = new MySqlConnection(strConnection);
            }

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                _conn.Open();
                Console.WriteLine("Connection successful.");
            }

            return _conn;
        }

        public void closeConnection()
        {
            if (_conn != null && _conn.State == System.Data.ConnectionState.Open)
            {
                _conn.Close();
                _conn = null;
            }
        }

    }
}