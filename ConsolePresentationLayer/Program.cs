using BusinessLayer.Database_Functions;
using BusinessLayer.Functions;
using BusinessLayer.Models;

namespace ConsolePresentationLayer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            
            var Countries = CountryController.ReadAll();
            foreach(var country in Countries)
            {
                Console.WriteLine(country.Id + " " + country.Name);
            }
            DatabaseFunctions.DeleteAllEntries();
        }
    }
}
