using DataAccess.Context;
using DataAccess.Implementation;
using DataAccess.Validation;
using Domin.Entity;
using Domin.Repository;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core;
using FluentEmail.Smtp;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using CloudinaryDotNet;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();


// Add Cors
builder.Services.AddCors(o =>
{
    o.AddPolicy("CoresPolicy", build =>
    {
        build.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

// For Fluent Validation 
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RoleValidator>();
builder.Services.AddScoped(typeof(IValidator<User>), typeof(UserValidator));
builder.Services.AddScoped(typeof(IValidator<Role>), typeof(RoleValidator));
builder.Services.AddScoped(typeof(IEmailSender), typeof(EmailSender)) ;

// for API Versions
builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
});

// For Cloudinary Services
//builder.Services.AddSingleton<Cloudinary>();
builder.Services.AddScoped<Cloudinary>((sp) =>
{
    Account account = new Account(
            configuration.GetSection("CloudName").Value,
            configuration.GetSection("CloudApiKey").Value,
            configuration.GetSection("CloudApiSecret").Value
        );
    return new Cloudinary(account);
});
builder.Services.AddScoped<ICloudinaryServices, CloudinaryServices>();

//builder.Services.AddSingleton(typeof(IWebConfigurationRepository), typeof(WebConfigurationRepository));
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(configuration.GetSection("Defult").Value, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
});

// Jwt Bearer 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("secret_Key").Value)),
            ClockSkew = TimeSpan.Zero
        };
    });
// Repositories Services.
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CoresPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


