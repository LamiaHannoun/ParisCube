using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportDataInParisCube
{
    class Program
    {
        static void Main(string[] args)
        {
            CsvHelper helper = new CsvHelper();
            helper.ReadVelibData("stations-velib-disponibilites-en-temps-reel.csv");
            helper.ReadCoffeShops("liste-des-cafes-a-un-euro.csv");
        }
    }
}
