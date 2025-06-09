namespace IntelReport.Models
{
    public class People
    {
        int Id { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string SecretCode { get; set; }
        string Type { get; set; }
        int NumReports { get; set; }
        int NumMention { get; set; }

    }
}