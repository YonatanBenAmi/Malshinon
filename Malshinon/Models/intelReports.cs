namespace IntelReport.Models
{
    public class Report
    {
        int Id { get; }
        public int ReportId;
        public int TargetId;
        public string Text;
        public DateTime Timestamp;

        public Report(int repoetId, int targetId, string text)
        {
            this.ReportId = repoetId;
            this.TargetId = targetId;
            this.Text = text;
        }
    }
}