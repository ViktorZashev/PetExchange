using BusinessLayer.Functions;
using BusinessLayer.Models;

namespace ConsolePresentationLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var country = new Country(Guid.NewGuid(), "Bulgaria");
            //var Pet = new Pet(Guid.NewGuid(), null ,"Tropcho", string.Empty, 3, "Gerbil", "He is a cute gerbil who sleeps a lot", true);
            CountryController.Create(country);
        }
    }
}
