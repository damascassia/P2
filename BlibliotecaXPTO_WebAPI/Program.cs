using System.Text;
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
// Add this using directive at the top of your file

// No changes needed in the existing code, just ensure the above namespace is included.




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


//builder.Services.AddScoped<IEmprestimosRepository, EmprestimosRepository>();
//builder.Services.AddScoped<IEmprestimosService, EmprestimosService>();

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
    var (sucesso, mensagem, dados) = service.PesquisarObrasDisponiveis(nucleo, assunto);

    if (!sucesso)
        return Results.BadRequest(new { mensagem });

    if (dados.Count == 0)
        return Results.Ok(new { mensagem, dados });

    return Results.Ok(dados);
})
.WithName("PesquisarObrasDisponiveis");



/*app.MapPost("/emprestimos/requisicao", (RequisicaoDTO dto, IEmprestimosService service) =>
{
    var (sucesso, mensagem) = service.RealizarRequisicao(dto);
    return sucesso
        ? Results.Ok(new { mensagem })
        : Results.BadRequest(new { mensagem });
})
.RequireAuthorization()
.WithName("RealizarRequisicao")
.WithOpenApi();


app.MapPost("/emprestimos/devolucao", (DevolucaoDTO dto, IEmprestimosService service) =>
{
    var (sucesso, mensagem) = service.RealizarDevolucao(dto);
    return sucesso
        ? Results.Ok(new { mensagem })
        : Results.BadRequest(new { mensagem });
})
.RequireAuthorization()
.WithName("RealizarDevolucao")
.WithOpenApi();



app.MapGet("/emprestimos/ativos/{leitorId:int}", (int leitorId, IEmprestimosService service) =>
{
    var (sucesso, mensagem, dados) = service.ObterEmprestimosAtivos(leitorId);
    return sucesso
        ? Results.Ok(dados)
        : Results.BadRequest(new { mensagem });
})
.RequireAuthorization()
.WithName("ObterEmprestimosAtivos")
.WithOpenApi();*/


app.Run();