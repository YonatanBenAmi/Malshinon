using IntelReport.DataBase;
using IntelReport.Models;
using MySql.Data.MySqlClient;

namespace IntelReport.DAL
{
    public class dalPeople : BaseConnetion
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

        public void AddPeople(string firstName, string lastName, string secretCode, string type, int numReports, int numMention)
        {
            People people = new People(firstName, lastName, secretCode, type, numReports, numMention);
            string query = $"INSERT INTO people (firstName, lastName, secret_code, type, num_reports, num_mentions) VALUES ('{firstName}', '{lastName}', '{secretCode}', '{type}', 0, 0)";
            try
            {
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);

                cmd.Parameters.AddWithValue("@firstName", people.FirstName);
                cmd.Parameters.AddWithValue("@lastName", people.LastName);
                cmd.Parameters.AddWithValue("@secret_code", people.SecretCode);
                cmd.Parameters.AddWithValue("@type", people.Type);
                cmd.Parameters.AddWithValue("@num_reports", people.NumReports);
                cmd.Parameters.AddWithValue("@num_mentions", people.NumMention);

                cmd.ExecuteNonQuery();
                System.Console.WriteLine("hhf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Update(People people, string columnName, int value)
        {
            
        }
    }
}