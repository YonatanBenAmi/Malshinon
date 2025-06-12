using MySql.Data.MySqlClient;
using IntelReport.DataBase;
using IntelReport.Models;
using IntelReport.DAL;
using System.Runtime.ConstrainedExecution;

namespace IntelReport.StartReport
{
    public class Start : BaseConnetion
    {
        dalPeople people = new dalPeople();
        DalIntelReports report = new DalIntelReports();
        MySqlConnection _conn = null!;
        public void Play()
        {
            PrintMenu();
            Console.ForegroundColor = ConsoleColor.Cyan;
            string reporterCode = GetSecretCodeInformer();//get secret code 
            Console.ResetColor();
            if (!IsPeopleExists(reporterCode))
            {
                AddPepoleWithCorrectType("reporter", reporterCode);
            }
            string targetCode = GetSecretCodeTarget();
            if (!IsPeopleExists(targetCode))
            {
                AddPepoleWithCorrectType("target", targetCode);
            }
            Console.WriteLine("Enter roport body:");
            string reportBody = Console.ReadLine()!;
            int reporterId = GetId(reporterCode);
            int targetId = GetId(targetCode);
            EnterReport(reporterId, targetId, reportBody);
            IncreaseNumReports(reporterCode);
            IncreaseNumMentions(targetCode);
            ChangeType(reporterCode);
            ChangeType(targetCode);


        }

        public string GetSecretCodeInformer()
        {
            Console.WriteLine("Enter your secret code:");
            string yourCode = Console.ReadLine()!;
            return yourCode;
        }

        public string GetSecretCodeTarget()
        {
            Console.WriteLine("Enter the target's code:");
            string targetCode = Console.ReadLine()!;
            return targetCode;
        }

        public bool IsPeopleExists(string code)
        {
            bool isExists = (people.GetPeopleBySecretCode(code) != null) ? true : false;
            return isExists;
        }

        public void AddPepoleWithCorrectType(string type, string code)
        {
            string currentTypePeople = (type == "reporter") ? "your" : "the target's";
            Console.WriteLine($"Enter {currentTypePeople} first name:");
            string firstName = Console.ReadLine()!;
            //check is valid name(method)
            Console.WriteLine($"Enter {currentTypePeople} last name:");
            string lastName = Console.ReadLine()!;
            //check is valid name(method)
            people.AddPeople(firstName, lastName, code, type);
        }

        public int GetId(string secret_code)
        {
            string query = $"SELECT id FROM people p WHERE p.secret_code = '{secret_code}'";
            MySqlDataReader reader;
            int idResult = 0;// Q - Is it legal to write this way?
            try
            {
                MySqlConnection _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    idResult = reader.GetInt32("id");
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
            return idResult;

        }
        public void EnterReport(int reporterId, int targetId, string reportText)
        {
            report.AddIntelReport(reporterId, targetId, reportText);
        }

        public int GetNumReports(string code)
        {
            int result = 0;
            try
            {
                string query = $"SELECT p.num_reports FROM people p WHERE p.secret_code = '{code}'";
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32("num_reports");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                closeConnection();
            }
            return result;
        }

        public int GetNumMentions(string code)
        {
            int result = 0;
            try
            {
                string query = $"SELECT p.num_mentions FROM people p WHERE p.secret_code = '{code}'";
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    result = reader.GetInt32("num_mentions");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                closeConnection();
            }
            return result;
        }

        public void IncreaseNumReports(string code)
        {
            int result = GetNumReports($"{code}") + 1;
            string query = $"UPDATE people SET num_reports = {result} WHERE secret_code = '{code}';";

            try
            {
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                closeConnection();
            }

        }

        public void IncreaseNumMentions(string code)
        {
            int result = GetNumMentions($"{code}") + 1;
            string query = $"UPDATE people SET num_mentions = {result} WHERE secret_code = '{code}';";

            try
            {
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                closeConnection();
            }

        }

        public bool IsBoth(string code)
        {
            if (GetNumMentions(code) > 0 && GetNumReports(code) > 0)
            {
                return true;
            }
            return false;
        }



        public void ChangeType(string code)
        {
            if (IsBoth(code))
            {
                string query = $"UPDATE people SET type = 'Both' WHERE secret_code = '{code}';";

                try
                {
                    _conn = openConnection();
                    MySqlCommand cmd = new MySqlCommand(query, _conn);
                    cmd.ExecuteNonQuery();
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

            if (GetNumReports(code) > 20)
            {


            }

            if (GetNumMentions(code) > 10)
            {
                string query = $"UPDATE people SET type = 'potential_agent' WHERE secret_code = '{code}';";

                try
                {
                    _conn = openConnection();
                    MySqlCommand cmd = new MySqlCommand(query, _conn);
                    cmd.ExecuteNonQuery();
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

        public void PrintMenu()
        {
            ConsoleKeyInfo key;
            int option = 1;
            bool isSelected = false;
            (int left, int top) = Console.GetCursorPosition();
            string color = "\u001b[32m";

            while (!isSelected)
            {
                Console.GetCursorPosition();
                Console.WriteLine($"{(option == 1 ? color : "")}option 1\u001b[0m");
                Console.WriteLine($"{(option == 2 ? color : "")}option 2\u001b[0m");
                Console.WriteLine($"{(option == 3 ? color : "")}option 3\u001b[0m");

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        option = (option == 3 ? 1 : option + 1);
                        break;
                    case ConsoleKey.UpArrow:
                        option = (option == 1 ? 3 : option - 1);
                        break;
                    case ConsoleKey.Enter:
                        isSelected = true;
                        break;
                }
                
                System.Console.WriteLine(option);   
            }
        }
        
    }
}