namespace IntelReport.Models
{
    public class People
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string SecretCode { get; set; }
        string Type { get; set; }
        int NumReports { get; set; }
        int NumMention { get; set; }

        public People(string firstName, string lastName, string secretCode, string type, int numReports, int numMention)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SecretCode = secretCode;
            this.Type = type;
            this.NumReports = numReports;
            this.NumMention = numMention;
        }

        public void printDetails()
        {
            Console.WriteLine(FirstName, LastName);
        }

    }
}