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
                    People onePeople = new People(firstName, lastName, secretCode, type);
                    peopleList.Add(onePeople);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return peopleList;
        }

        public People GetPeopleBySecretCode(string Code)
        {
            string query = $"SELECT * FROM people p WHERE p.secret_code = '{Code}'";
            MySqlCommand? cmd = null;
            MySqlDataReader? reader = null;
            People people = null!;

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
                    people = new People(firstName, lastName, secretCode, type);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                closeConnection();
            }

            return people;
        }
        public void AddPeople(string firstName, string lastName, string secretCode, string type)
        {
            People people = new People(firstName, lastName, secretCode, type);
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
                Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                closeConnection();
            }

        }
        
        public void Update(string code, string columnName, int value = 1)
        {
            if (columnName != "num_reports" && columnName != "num_mentions")
            {
                Console.WriteLine("Invalid column inserted!");
            }
            else
            {
                try
                {
                    People people = GetPeopleBySecretCode(code);
                    int resultValue;
                    if (columnName == "num_reports")
                    {
                        resultValue = people.NumReports++;
                    }
                    else
                    {
                        resultValue = people.NumMention++;
                    }
                    string query = $"UPDATE people SET {columnName} = {resultValue} WHERE secret_code = '{code}';";
                    _conn = openConnection();
                    MySqlCommand cmd = new MySqlCommand(query, _conn);

                    switch (columnName)
                    {
                        case "num_reports":
                            people.NumReports = value;
                            cmd.ExecuteNonQuery();
                            break;
                        case "num_mentions":
                            people.NumReports = value;
                            cmd.ExecuteNonQuery();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    closeConnection();
                }
            }
        }

        public void deletePerson(string secretCode)
        {
            string query = $"DELETE FROM people WHERE secret_code = '{secretCode}';";

            
        }
    }
}