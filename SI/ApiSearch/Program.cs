using RestSharp;

var restClient = new RestClient("http://load-balancer");
restClient.Post(new RestRequest("LoadBalancer?url=http://" + Environment.MachineName, Method.Post));  //todo maybe an issue is here, api search always shuts down 
Console.WriteLine("this is a test"+restClient);
Console.WriteLine("Hostname: " + Environment.MachineName);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();