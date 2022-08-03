using CrudApplication.DatabaseController;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.Configure<MoviesDatabaseSettings>(builder.Configuration.GetSection("MoviesDatabaseSettings"));
builder.Services.AddSingleton<MoviesService>();

var app = builder.Build();


//get at home page

app.MapGet("/", () => "Movies API!");

// Get all movies

app.MapGet("/api/movies", async (MoviesService moviesService) => await moviesService.Get());

// Get a movie by id

app.MapGet("/api/movies/{id}", async (MoviesService moviesService, string id) =>
{
    var movie = await moviesService.Get(id);
    return movie is null ? Results.NotFound() : Results.Ok(movie);
});

//Create a new movie

app.MapPost("/api/movies", async (MoviesService moviesService, Movies movie) =>
{
    await moviesService.Create(movie);
    return Results.Ok();
});

// Update a movie

app.MapPut("/api/movies/{id}", async (MoviesService moviesService, string id, Movies updatedMovie) =>
{
    var movie = await moviesService.Get(id);
    if (movie is null) return Results.NotFound();

    updatedMovie.Id = movie.Id;
    await moviesService.Update(id, updatedMovie);

    return Results.NoContent();
});


// Delete a movie

app.MapDelete("/api/movies/{id}", async (MoviesService moviesService, string id) =>
{
    var movie = await moviesService.Get(id);
    if (movie is null) return Results.NotFound();

    await moviesService.Remove(movie.Id);

    return Results.NoContent();
});

app.Run();
