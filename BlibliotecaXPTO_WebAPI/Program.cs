using System.Text;
using BibliotecaXPTOLibs.Helpers;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using BlibliotecaXPTO_WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Helpers;
using Microsoft.AspNetCore.Mvc;
using BibliotecaXPTOLibs.DTOs;





Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();


builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


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
        ValidIssuer = "BibliotecaXPTO",
        ValidAudience = "BibliotecaXPTO",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});


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
        Description = "Exemplo: \"Bearer {token}\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});


builder.Services.AddScoped<IConnectionHelper, ConnectionHelper>();


builder.Services.AddScoped<IObrasRepository, ObrasRepository>();
builder.Services.AddScoped<IObraService, ObraService>();
builder.Services.AddScoped<IEmprestimosRepository, EmprestimosRepository>();
builder.Services.AddScoped<IEmprestimosService, EmprestimosService>();
builder.Services.AddScoped<ILeitoresRepository, LeitoresRepository>();
builder.Services.AddScoped<ILeitoresService, LeitoresService>();



builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("cors");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();




app.MapGet("/obras/disponiveis", (string? nucleo, string? assunto, IObraService service) =>
{
    var obras = service.PesquisarObrasDisponiveis(nucleo, assunto);
    return Results.Ok(obras);
}).RequireAuthorization();



app.MapGet("/emprestimos/situacao/{leitorId:int}", (int leitorId, IEmprestimosService service) =>
{
    try
    {
        var situacao = service.ObterSituacaoLeitor(leitorId);
        return Results.Ok(situacao);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { mensagem = ex.Message });
    }
}).RequireAuthorization()
;

app.MapPost("/emprestimos/requisicao", (EmprestimoDTO dto, IEmprestimosService service) =>
{
    try
    {
        service.RealizarRequisicao(dto);
        return Results.Ok(new { mensagem = "Requisição realizada com sucesso." });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { mensagem = ex.Message });
    }
}).RequireAuthorization()
;

app.MapPost("/emprestimos/devolucao", (DevolucaoDTO dto, IEmprestimosService service) =>
{
    try
    {
        service.RealizarDevolucao(dto);
        return Results.Ok(new { mensagem = "Devolução registada com sucesso." });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { mensagem = ex.Message });
    }
}).RequireAuthorization()
;

app.MapPost("/leitores/{leitorId:int}/inscricao/cancelar", (int leitorId,  ILeitoresService service) =>
{
    try
    {
        service.CancelarInscricao(leitorId);
        return Results.Ok(new { mensagem = "Inscrição cancelada com sucesso. Todos os exemplares foram devolvidos." });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { mensagem = ex.Message });
    }
}).RequireAuthorization()
;

app.Run();