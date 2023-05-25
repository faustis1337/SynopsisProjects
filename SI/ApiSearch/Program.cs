using k8s;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

string kubernetesServiceHost = Environment.GetEnvironmentVariable("KUBERNETES_SERVICE_HOST");

Console.WriteLine(Environment.MachineName);


RestClient restClient;

if (!string.IsNullOrEmpty(kubernetesServiceHost))
{
    restClient = new RestClient("http://load-balancer-service:9020");
    restClient.Post(new RestRequest($"LoadBalancer/AddServer/?url=http://{Environment.MachineName}.searchapi-service.default.svc.cluster.local", Method.Post));
}
else
{
    restClient = new RestClient("http://load-balancer");
    restClient.Post(new RestRequest("LoadBalancer/AddServer/?url=http://" + Environment.MachineName, Method.Post));  
}




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