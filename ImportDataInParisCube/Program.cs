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
            //helper.ReadVelibData("stations-velib-disponibilites-en-temps-reel.csv");
            //helper.ReadCoffeShops("liste-des-cafes-a-un-euro.csv");
            //helper.ReadTrees("les-arbres.csv");
            //helper.ReadCinemas("cinemas-a-paris.csv");
            helper.ReadHotSpot("liste_des_sites_des_hotspots_paris_wifi.csv");
            helper.ReadSubways("positions-geographiques-des-stations-du-reseau-ratp.csv");
        }
    }
}
