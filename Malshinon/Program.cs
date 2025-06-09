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
            List<People> p = dp.GetPeopleList();
            foreach (People person in p)
            {
                person.printDetails();
            }
        }
    }
}