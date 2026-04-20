using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Models;
using BibliotecaXPTOLibs.Helpers;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using DalProLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

//Logger

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt",
        rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.-------------------------------------------------------------

builder.Host.UseSerilog();

//CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

//JWT Bearer

var secret_key = builder.Configuration["App:JWT:SECRET_KEY"];
var key = Encoding.UTF8.GetBytes(secret_key);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuers = new[] { "BibliotecaPazu", "BibliotecaXPTO" },
        ValidAudiences = new[] { "BibliotecaPazu", "BibliotecaXPTO" },

        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

//Autorization

builder.Services.AddAuthorization(options =>
{
options.AddPolicy("ApenasAdmin", policy =>
    policy.RequireRole(EnumRoles.Admin.ToString()));

options.AddPolicy("AdminOuLeitor", policy =>
    policy.RequireRole(
        EnumRoles.Admin.ToString(),
        EnumRoles.Leitor.ToString()
    ));
options.AddPolicy("TodosAutenticados", policy =>
    policy.RequireRole(

        EnumRoles.Admin.ToString(),
        EnumRoles.Bibli.ToString(),
        EnumRoles.Leitor.ToString()
        ));
options.AddPolicy("AdminOuBibli", policy =>
    policy.RequireRole(
        EnumRoles.Admin.ToString(),
        EnumRoles.Bibli.ToString()
        ));
});



//Swagger



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JSON Web Token baseado no esquema Bearer. Exemplo: \"Bearer {token}\""
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
            new string[] {}
        }
    }
    
    );
});

//DI (Dependency Injection)

builder.Services.AddScoped<IObrasRepository, ObrasRepository>();
builder.Services.AddScoped<IConnectionHelper, ConnectionHelper>();
builder.Services.AddScoped<IUtilizadoresRepository, UtilizadoresRepository>();
builder.Services.AddScoped<IUtilizadoresService, UtilizadoresService>();
builder.Services.AddScoped<ILoginHelper, LoginHelper>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseCors("cors");

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline. (Configurção de endpoints)

app.UseHttpsRedirection();

app.MapPost("/login", (LoginDTO login, IAuthService auth) =>
{
    var token = auth.Login(login.UserName, login.Password);
    if (string.IsNullOrEmpty(token))
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new { token });
});

app.MapPut("/Utilizadores/alterar_status", (AlterarStatusDTO dto, IUtilizadoresService service) =>
{
    service.AlterarStatus(dto.UtilizadorId, dto);

    return Results.Ok(new { Mensagem = "Status alterado com sucesso" }); ;
})
    .RequireAuthorization("AdminOuBibli");

app.MapDelete("/Utilizadores/limpar-antigos", (IUtilizadoresService service) =>
{
    service.DeleteLeitorAntigo();

    return Results.Ok(new { Mensagem = "Leitores antigos deletados com sucesso" }); 
})
    .RequireAuthorization("AdminOuBibli"); ;

app.MapPost("/Utilizadores/registrar-utilizador",
    (RegistrarUtilizadorDTO dto, IUtilizadoresService service) =>
    {
        service.RegistrarUtilizador(dto);

        return Results.Ok(new { Mensagem = "Utilizador registrado com sucesso" });
    })
.RequireAuthorization("TodosAutenticados");



app.Run();

