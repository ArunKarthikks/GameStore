using JobAPI.Endpoints;
using JobAPI.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GameContext>(options => options.UseSqlServer(connectionString)); 

var app = builder.Build();
app.MapGameEndpoints();
//app.MapGet("/", () => "Hello World!");

app.Run(); 
