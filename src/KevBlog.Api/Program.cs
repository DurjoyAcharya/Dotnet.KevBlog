using KevBlog.Application.Automapper;
using KevBlog.Infrastructure.Data;
using KevBlog.Infrastructure.Extensions;
using KevBlog.Infrastructure.Middleware;
using KevBlog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


if(builder.Environment.IsDevelopment()){
    app.UseDeveloperExceptionPage();
}


app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
    await Seed.SeedPosts(context);
}
catch(Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}


app.Run();