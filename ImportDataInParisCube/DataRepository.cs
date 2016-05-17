using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace ImportDataInParisCube
{

    public class ZoneDataForService
    {
        public int LongitudeIndex { get; set; }
        public int LatitudeIndex { get; set; }
        public int Count { get; set; }
    }

    public class DataRepository
    {

        private const double LongStart = 2.227392;
        private const double LatStart = 48.901736;
        private const double LongDelta = 0.012085;
        private const double LatDelta = -0.0041787;


        private const string _url = "https://pariscube.documents.azure.com:443";
        private DocumentClient _client;
        private Uri _collectionUri;

        public DataRepository()
        {
            var primaryKey = File.ReadAllText("key.txt");


            _client = new DocumentClient(new Uri(_url), primaryKey);

            _collectionUri = UriFactory.CreateDocumentCollectionUri("pariscubedata", "data");

        }


        public void ImportData(ServiceType serviceType, IEnumerable<ZoneTemp> zones)
        {
            var q = from z in zones
                    let zoneIndexes = Tuple.Create(GetLongitudeIndex(z.Longitude), GetLatitudeIndex(z.Latitude))
                    where zoneIndexes.Item1 >= 0 && zoneIndexes.Item2 >= 0 && z.Numbers > 0
                    group z by zoneIndexes
                into gp
                    select new ZoneDataForService()
                    {
                        LongitudeIndex = gp.Key.Item1,
                        LatitudeIndex = gp.Key.Item2,
                        Count = gp.Sum((_ => _.Numbers))
                    };


            var data = q.ToList();


            var queryOptions = new FeedOptions { MaxItemCount = -1 };
            var serverData =
                _client.CreateDocumentQuery<ZoneData>(_collectionUri, "SELECT * FROM data", queryOptions).ToList();




            foreach (var dbData in serverData)
            {
                var dataToImport =
                    data.FirstOrDefault(
                        _ => (_.LatitudeIndex == dbData.LatitudeIndex) && (_.LongitudeIndex == dbData.LongitudeIndex));


                if (dataToImport == null)
                {
                    continue;
                }


                switch (serviceType)
                {
                    case ServiceType.Velib:
                        dbData.VelibCount = dataToImport.Count;
                        break;
                    case ServiceType.CoffeeShops:
                        dbData.CoffeeShops = dataToImport.Count;
                        break;
                    case ServiceType.Restaurant:
                        break;
                    case ServiceType.Trees:
                        break;
                    case ServiceType.Subway:
                        break;
                    default:
                        continue;
                }

                try
                {

                    var result = _client.ReplaceDocumentAsync(
                        UriFactory.CreateDocumentUri("pariscubedata", "data", dbData.Id.ToString()), dbData).Result;
                }
                catch (Exception)
                {


                }

            }
        }


        private int GetLongitudeIndex(double longitude)
        {
            var index = (int)Math.Floor((longitude - LongStart) / LongDelta);

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


    public class ZoneData
    {
        public int LongitudeIndex { get; set; }

        public int LatitudeIndex { get; set; }


        public int? VelibCount { get; set; }

        public int? CoffeeShops { get; set; }


        public int? TreeCount { get; set; }

        public int? CinemaCount { get; set; }

        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
    }
}
