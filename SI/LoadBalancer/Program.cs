using LoadBalancer.LoadBalancer;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("Running ON");
Console.WriteLine(Environment.MachineName);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ILoadBalancer,LoadBalancer.LoadBalancer.LoadBalancer>(); //I love this reference path

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

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();