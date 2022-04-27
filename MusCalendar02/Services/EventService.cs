using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MusCalendar02.Data;
using MusCalendar02.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusCalendar02.Services
{
    public class EventService
    {
        private readonly IMongoCollection<Event> _event;
         
        public EventService(
            IOptions<ConnectMongoDb> dbcsettings)
        {
            var mongoClient = new MongoClient(dbcsettings.Value.ConnectionString);

            var mongoDB = mongoClient.GetDatabase(dbcsettings.Value.DbName);

            _event = mongoDB.GetCollection<Event>(dbcsettings.Value.CollectionName);
        }

        // Find All
        public async Task<List<Event>> GetAsync() =>
            await _event.Find(_ => true).ToListAsync();

        // Find by Id
        public async Task<Event> GetByIdAsync(string id) =>
            await _event.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Find by Title
        public async Task<Event?> GetByTitleAsync(string title) =>
            await _event.Find(x => x.Titulo == title).FirstOrDefaultAsync();

        // Find by Date Month
        //public async Task<List<Event>> GetByDateMonthAsync(int month)
        //{
        //    //var construtor = Builders<Event>;
        //    //var condiction = construtor.();

        //    return await _event.Find(x => x.Data.Month == month).SortBy(x => x.Data.Day).ToListAsync();
        //}

        // Create an event
        public async Task InsertOneAsync(Event newModel) =>
            await _event.InsertOneAsync(newModel);

        // Delete an event
        public async Task DeleteAsync(string id) =>
            await _event.DeleteOneAsync(x => x.Id == id);

        // Alter an event
        public async Task UpdateOneAsync(string id, Event alterEvent) =>
            await _event.ReplaceOneAsync(x => x.Id == id, alterEvent);
    }
}
