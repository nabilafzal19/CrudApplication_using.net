using MongoDB.Bson.Serialization.Attributes;
namespace CrudApplication.DatabaseController
{
    public class Movies
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        public string Id { get; set; } = String.Empty;
        [BsonElement("title")]
        public string Title { get; set; } = String.Empty;
        [BsonElement("year")]
        public int Year { get; set; }
     
    }
}
