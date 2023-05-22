using UserLoadBalancer.LoadBalancer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ILoadBalancer,UserLoadBalancer.LoadBalancer.LoadBalancer>(); //I love this reference path

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
Console.WriteLine("Testing if cors works?");

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();