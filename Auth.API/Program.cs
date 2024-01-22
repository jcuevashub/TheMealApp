using Auth.API.Extensions;
using Auth.API.Notification;
using Auth.Application;
using Auth.Infrastructure;
using Auth.Infrastructure.Context;
using Auth.Shared;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.AddSharedInfrastructure(builder.Configuration);

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();
builder.Services.AddJwtTokenAuthentication(builder.Configuration);
builder.Services.AddSignalR();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

var serviceScope = app.Services.CreateScope();
var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
dataContext?.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapHub<NotificationHub>("/notificationHub");
app.MapControllers();

app.Run();
