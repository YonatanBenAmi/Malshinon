namespace IntelReport.Models
{
    public class IntelReport
    {
        int Id { get; }
        string ReportId { get; set; }
        string TargetId { get; set; }
        string Text { get; set; }
        string Timestamp { get; set; }
    }
}