using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Server;
using Server.Models;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

var settings = new Settings();
var jwtOptionsSection = new JwtOptionsSection();
builder.Configuration.Bind("Settings", settings);
builder.Configuration.Bind("jwtOptionsSection", jwtOptionsSection);
builder.Services.AddSingleton(settings);

// Add services to the container.
builder.Services.AddDbContext<ServerDbContext>(o
    => o.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddControllers().AddNewtonsoftJson(o =>
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please insert JWT with Bearer into field",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    }
);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ILevelService, LevelService>();
builder.Services.AddScoped<IFriendsService, FriendsService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.BearerKey)),
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false
    }
);

// builder.Services.AddAuthentication(o =>
// {
//     o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// }).AddJwtBearer(o =>
// {
//     o.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidIssuer = jwtOptionsSection.Issuer,
//         ValidAudience = jwtOptionsSection.Audience,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.BearerKey)),
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = false,
//         ValidateIssuerSigningKey = true,
//     };
// });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();