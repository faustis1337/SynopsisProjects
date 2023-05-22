using Microsoft.EntityFrameworkCore;
using RestSharp;
using UserApi.Data;

var restClient = new RestClient("http://user-load-balancer");
restClient.Post(new RestRequest("UserLoadBalancer?url=http://" + Environment.MachineName, Method.Post));  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDBInitializer,DBInitializer>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

builder.Services.AddControllers();
builder.Services.AddDbContext<UserApiContext>(opt => opt.UseInMemoryDatabase("UsersDb"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<UserApiContext>();
    var dbInitializer = services.GetService<IDBInitializer>();
    dbInitializer.initialize(dbContext);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();