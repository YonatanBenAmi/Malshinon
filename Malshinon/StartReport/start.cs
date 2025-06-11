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
        public void Play()
        {
            string reporterCode = GetSecretCodeInformer();//get secret code 
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
            Increase(reporterCode, "num_reports");
            Increase(targetCode, "num_mentions");
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

        public void Increase(string code, string nameCol)
        {
            people.Update(code, nameCol);
        }

    }
}