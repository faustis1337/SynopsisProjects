using Sql.Interfaces;
using Sql.Repository;

var builder = WebApplication.CreateBuilder(args);
var dbPopulate = new PopulateTable();
dbPopulate.CreateDatabase();
dbPopulate.PopulateDatabase(); //todo enable this if u want to create the DB code wise, it might not work


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IClassRepo, ClassRepo>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();
builder.Services.AddScoped<IEnrollmentRepo, EnrollmentRepo>();
builder.Services.AddScoped<IQueryRepo, QueryRepo>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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