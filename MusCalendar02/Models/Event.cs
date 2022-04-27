using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MusCalendar02.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Data { get; set; }
        //public string Data { get; set; } = string.Empty;
        public string Descricao { get; set; }
        public bool Importante { get; set; }
    }
}
