using TaskManagerBackend.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Frontend URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();  // Allow credentials (cookies, authorization headers, etc.)
    });
});
builder.Services.AddControllers();
builder.Services.AddSignalR();  // Add SignalR services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAllOrigins");

app.UseRouting();

app.MapControllers();
app.MapHub<TaskHub>("/hubs/taskhub");  

app.Run();
