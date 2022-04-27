using MongoDB.Driver;
using MusCalendar02.Models;
using System;

namespace MusCalendar02.Data
{
    public class ConnectMongoDb : IDbCSettings
    {
        public string CollectionName { get; set; } = Environment.GetEnvironmentVariable("DbConfig:CollectionName");
        public string ConnectionString { get; set; } = Environment.GetEnvironmentVariable("DbConfig:ConnectionString");
        public string DbName { get; set; } = Environment.GetEnvironmentVariable("DbConfig:DbName");

    }
    public interface IDbCSettings
    {
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DbName { get; set; }
    }

}
