using BusinessLayer;
using BusinessLayer.Database_Functions;
using BusinessLayer.Functions;
using BusinessLayer.Models;

namespace ConsolePresentationLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DatabaseFunctions.ReturnTownsAndCountries();
            var consoleController = new ConsoleController();
        }
    }
}
