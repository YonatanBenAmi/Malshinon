using IntelReport.DAL;
using IntelReport.DataBase;
using IntelReport.Models;

namespace IntelReport
{
    public class Malshinon
    {
        static void Main(string[] args)
        {
            dalPeople dp = new dalPeople();
            // List<People> p = dp.GetPeopleList();
            dp.AddPeople("avi", "sgakom", "4444", "both", 2, 8);
            // foreach (People person in p)
            // {
            //     person.printDetails();
            // }
        }
    }
}