using IntelReport.DataBase;
using IntelReport.Models;
using MySql.Data.MySqlClient;

namespace IntelReport.DAL
{
    public class DalIntelReports : BaseConnetion
    {
        private MySqlConnection? _conn;

        public List<Report> GetReportList(string query = "SELECT * FROM intelreports")
        {
            List<Report> reportList = new List<Report>();

            try
            {
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int reportId = reader.GetInt32("reporter_id");
                    int targetId = reader.GetInt32("target_id");
                    string text = reader.GetString("text");
                    DateTime timestamp = reader.GetDateTime("timestamp");
                    Report report = new Report(reportId, targetId, text);
                    reportList.Add(report);
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
            return reportList;
        }

        public void AddIntelReport(int reportId, int targetId, string text)
        {
            Report report = new Report(reportId, targetId, text);
            string query = $"INSERT INTO intelreports (reporter_id, target_id, text) VALUES ({reportId}, {targetId}, '{text}');";

            try
            {
                _conn = openConnection();
                MySqlCommand cmd = new MySqlCommand(query, _conn);

                cmd.Parameters.AddWithValue("@reporter_id", report.ReportId);
                cmd.Parameters.AddWithValue("@target_id", report.TargetId);
                cmd.Parameters.AddWithValue("@text", report.Text);
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
    }
}