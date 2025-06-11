

namespace IntelReport.Models
{
    public class People
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecretCode { get; set; }
        public string Type { get; set; }
        public int NumReports { get; set; }
        public int NumMention { get; set; }

        public People(string firstName, string lastName, string secretCode, string type)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SecretCode = secretCode;
            this.Type = type;
        }

        public void printDetails()
        {
            Console.WriteLine($"First name: {FirstName}, Last name: {LastName}, Secret code: {SecretCode},  Type: {Type}, Num reports: {NumReports}, Num mention: {NumMention}");
        }

    }
}