using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImportDataInParisCube
{
    public class CsvHelper
    {
        private DataRepository _dataRepository;
        public CsvHelper()
        {
            _dataRepository = new DataRepository();
        }
        public void ReadVelibData(string path)
        {
            var lines = File.ReadAllLines(path).Select(a => a.Split(';'));
            var zones = new List<ZoneTemp>();
            int i = 0;
            foreach (var line in lines)
            {
                if (i > 0)
                {
                    var position = line[3];
                    var test = position.Split(',');
                    var latitude = double.Parse(test[0], CultureInfo.InvariantCulture);
                    var longitude = double.Parse(test[1], CultureInfo.InvariantCulture);
                    var zone = new ZoneTemp()
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        Numbers = int.Parse(line[8]),
                       
                    };
                    zones.Add(zone);
                }
                i++;
            }
            _dataRepository.ImportData(ServiceType.Velib, zones);
        }

        public void ReadCoffeShops(string path)
        {
            var lines = File.ReadAllLines(path).Select(a => a.Split(';'));
            var zones = new List<ZoneTemp>();
            int i = 0;
            foreach (var line in lines)
            {
                if (i > 0)
                {
                    if (line.Length > 6 && !string.IsNullOrWhiteSpace(line[7]))
                    {
                    var position = line[7];
                    var test = position.Split(',');
                    var latitude = double.Parse(test[0], CultureInfo.InvariantCulture);
                    var longitude = double.Parse(test[1], CultureInfo.InvariantCulture);
                    var zone = new ZoneTemp()
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        Name = line[1],

                    };
                    zones.Add(zone);
                }
                }
                i++;
            }
            _dataRepository.ImportData(ServiceType.CoffeeShops,zones);
        }

        public void ReadTrees(string path)
        {
            var lines = File.ReadAllLines(path).Select(a => a.Split(';'));
            var zones = new List<ZoneTemp>();
            int i = 0;
            foreach (var line in lines)
            {
                if (i > 0)
                {
                    var position = line[0];
                    var test = position.Split(',');
                    var latitude = double.Parse(test[0], CultureInfo.InvariantCulture);
                    var longitude = double.Parse(test[1], CultureInfo.InvariantCulture);
                    var zone = new ZoneTemp()
                    {
                        Latitude = latitude,
                        Longitude = longitude,
                        
                    };
                    zones.Add(zone);
                }
                i++;
            }
            _dataRepository.ImportData(ServiceType.Trees,zones);
        }
        public void ReadCinemas(string path)
        {
            var lines = File.ReadAllLines(path).Select(a => a.Split(';'));
            var zones = new List<ZoneTemp>();
            int i = 0;
            foreach (var line in lines)
            {
                if (i > 0)
                {
                    if (!string.IsNullOrWhiteSpace(line[7]) && !string.IsNullOrWhiteSpace(line[3])
                        && !string.IsNullOrWhiteSpace(line[1]))
                    {
                        var test = line[7].Split(',');
                        var latitude = double.Parse(test[0], CultureInfo.InvariantCulture);
                        var longitude = double.Parse(test[1], CultureInfo.InvariantCulture);
                        var zone = new ZoneTemp()
        {
                            Latitude = latitude,
                            Longitude = longitude,
                            Name = line[1]
                        };
                        zones.Add(zone);
                    }
                }
                i++;
            }
            _dataRepository.ImportData(ServiceType.Cinemas,zones);
        }

        }



    public class ZoneTemp
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Numbers { get; set; }
        public string Name { get; set; }
      
    }



    public enum ServiceType
    {
        Velib = 1,
        CoffeeShops,
        Restaurant,
        Trees,
        Cinemas
    }
}
