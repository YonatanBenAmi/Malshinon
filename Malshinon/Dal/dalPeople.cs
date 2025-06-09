using IntelReport.DataBase;
using IntelReport.Models;
using MySql.Data.MySqlClient;

namespace IntelReport.DAL
{
    public class dalPeople: BaseConnetion
    {
        private MySqlConnection? _conn;

        public List<People> GetPeopleList(string query = "SELECT * FROM people")
        {
            List<People> peopleList = new List<People>();
            MySqlCommand? cmd = null;
            MySqlDataReader? reader = null;

            try
            {
                _conn = openConnection();
                cmd = new MySqlCommand(query, _conn);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string firstName = reader.GetString("firstName");
                    string lastName = reader.GetString("lastName");
                    string secretCode = reader.GetString("secret_code");
                    string type = reader.GetString("type");
                    int numReports = reader.GetInt32("num_reports");
                    int numMention = reader.GetInt32("num_mentions");
                    People onePeople = new People(firstName, lastName, secretCode, type, numReports, numMention);
                    peopleList.Add(onePeople);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return peopleList;
        }

        

    }
}