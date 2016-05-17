using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;

namespace ImportDataInParisCube
{
    public class DataRepository
    {

        private const double LongStart =  2.227392;
        private const double LatStart = 48.901736;
        private const double LongDelta = 0.012085;
        private const double LatDelta = -0.0041787;

        string connectionString { get; }
        public DataRepository()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        }

        public void ImportData(IEnumerable<ZoneTemp> zones)
        {
            var q = from z in zones
                let zoneIndexes = Tuple.Create(GetLongitudeIndex(z.Longitude), GetLatitudeIndex(z.Latitude))
                where zoneIndexes.Item1 >= 0 && zoneIndexes.Item2 >= 0
                group z by zoneIndexes
                into gp
                select new ZoneData()
                {
                    LongitudeIndex = gp.Key.Item1,
                    LatitudeIndex = gp.Key.Item2,
                    Count = gp.Sum((_ => _.Numbers))
                };


            var data = q.ToList();


        }


        private int GetLongitudeIndex(double longitude)
        {
            var index = (int)Math.Floor((longitude - LongStart)/LongDelta);

            if (index >= 20)
            {
                return -1;
            }

            return index;

        }

        private int GetLatitudeIndex(double longitude)
        {
            var index = (int)Math.Floor((longitude - LatStart) / LatDelta);

            if (index >= 20)
            {
                return -1;
            }


            return index;
        }
    }
}
