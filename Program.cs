global using BlogAPI.Models;
global using BlogAPI.Data;
global using BlogAPI.Dtos.RequestDtos;
global using BlogAPI.Dtos.ResponseDtos;
global using BlogAPI.Services;
global using BlogAPI.Helpers;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using AutoMapper;
global using BCrypt.Net;
global using System.Security.Claims;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using System.IdentityModel.Tokens.Jwt;
global using System.Text;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value!));


builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters() {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Jwt:SecretKey").Value!))
    };
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IBlogService, BlogService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
