using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CrudApplication.DatabaseController
{
    public class MoviesService
    {
        private readonly IMongoCollection<Movies> _movies;

        public MoviesService(IOptions<MoviesDatabaseSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _movies = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Movies>(options.Value.MoviesCollectionName);
        }

        public async Task<List<Movies>> Get() =>
            await _movies.Find(_ => true).ToListAsync();

        public async Task<Movies> Get(string id) =>
            await _movies.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task Create(Movies newMovie) =>
            await _movies.InsertOneAsync(newMovie);

        public async Task Update(string id, Movies updateMovie) =>
            await _movies.ReplaceOneAsync(m => m.Id == id, updateMovie);

        public async Task Remove(string id) =>
            await _movies.DeleteOneAsync(m => m.Id == id);
    }
}