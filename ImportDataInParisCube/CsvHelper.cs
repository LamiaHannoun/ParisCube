using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            foreach (var line in lines)
            {

            }
        }

        public void ReadRestaurants(string path)
        {

        }

        public void ReadSubwayStations(string path)
        {

        }
    }
}
