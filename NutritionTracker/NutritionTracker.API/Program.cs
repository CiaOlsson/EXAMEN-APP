using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NutritionTracker.API.Services;
using NutritionTracker.Infrastructure.DbContext;
using System.Text;

var builder = WebApplication.CreateBuilder(args); 

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy.WithOrigins("http://localhost:5173") 
			  .AllowAnyHeader()
			  .AllowAnyMethod();
	});
});
// Add services to the container.
//Lägg till JWT eller cookie-auth senare?? 
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = @"JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Name = "Authorization",
				In = ParameterLocation.Header,
			},
			new List<string>()
		}
	});
});


builder.Services.AddDbContext<NutritionTrackerDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<NutritionTrackerDbContext>().AddDefaultTokenProviders();

// For better database related exceptions in browser during development. 
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

string key = builder.Configuration["JWT_key"];

//Authentication sätts upp
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
	//Konfiguration av JWT
	opt.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		ValidateLifetime = true,
		IssuerSigningKey =
		 new SymmetricSecurityKey(Convert.FromBase64String(key!)),
		ValidateIssuerSigningKey = true,

	};
});

builder.Services.AddAuthorization();
builder.Services.AddTransient<IAuthService, AuthService>();

var app = builder.Build();

 //Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI( c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "NutritionTracker API V1");
		c.RoutePrefix = string.Empty;
	});

	app.UseDeveloperExceptionPage(); 
	
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

	string[] roles = ["Admin", "Customer"];
	foreach (var role in roles)
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
}

app.Run();
