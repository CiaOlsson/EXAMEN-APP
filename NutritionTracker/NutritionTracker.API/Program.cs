using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NutritionTracker.Infrastructure.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Lägg till JWT eller cookie-auth senare?? 
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<NutritionTrackerDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<NutritionTrackerDbContext>().AddDefaultTokenProviders();

// For better database related exceptions in browser during development. 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI( c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "NutritionTracker API V1");
		c.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
