using MongoDB.Driver;
using NoSql.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("nosqlproject");

builder.Services.AddSingleton<IMongoDatabase>(database);
builder.Services.AddScoped<MongoDBContext>();
builder.Services.AddScoped<IStudentsRepo,StudentsRepo>();
builder.Services.AddScoped<IClassesRepo,ClassesRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();