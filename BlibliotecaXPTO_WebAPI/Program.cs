using System.Text;
using BibliotecaXPTOLibs.DTOs;
using BibliotecaXPTOLibs.Helpers;
using BibliotecaXPTOLibs.Helpers.Interfaces;
using BibliotecaXPTOLibs.Models;
using BibliotecaXPTOLibs.Repositories;
using BibliotecaXPTOLibs.Repositories.Interfaces;
using BlibliotecaXPTO_WebAPI.Services;
using BlibliotecaXPTO_WebAPI.Services.Interfaces;
using DalProLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;
using System.Text;







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
var issuer = builder.Configuration["App:JWT:ISSUER"];

var key = Encoding.UTF8.GetBytes(secret_key);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.MapInboundClaims = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = issuer,
        ValidAudience = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        NameClaimType = "name",
        RoleClaimType = "role"
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
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Biblioteca Pazu API",
        Version = "v1",
        Description = "API para gestão de acervo, empréstimos e utilizadores da Biblioteca."
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
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
    }
    
    );
});


builder.Services.AddScoped<IConnectionHelper, ConnectionHelper>();


builder.Services.AddScoped<IObrasRepository, ObrasRepository>();
builder.Services.AddScoped<IObraService, ObraService>();
builder.Services.AddScoped<IConnectionHelper, ConnectionHelper>();
builder.Services.AddScoped<IUtilizadoresRepository, UtilizadoresRepository>();
builder.Services.AddScoped<IUtilizadoresService, UtilizadoresService>();
builder.Services.AddScoped<ILoginHelper, LoginHelper>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();
builder.Services.AddScoped<IMetricsService, MetricsService>();
builder.Services.AddScoped<IAssuntoRepository, AssuntoRepository>();
builder.Services.AddScoped<IExemplarService, ExemplarService>();    
builder.Services.AddScoped<IExemplaresRepository,ExemplaresRepository>();
builder.Services.AddScoped<IEmprestimosRepository, EmprestimosRepository>();
builder.Services.AddScoped<IEmprestimosService, EmprestimosService>();
builder.Services.AddScoped<ILeitoresRepository, LeitoresRepository>();
builder.Services.AddScoped<ILeitoresService, LeitoresService>();



builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("cors");
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapGet("/", () => "Biblioteca API")
    .WithSummary("Entrada da API")
    .WithDescription("Este endpoint é o Health Check da api, testa se a aplicação subiu, se a porta está correta e se a.");


app.MapPost("/Obras", (CreateObraDTO dto, IObraService service) =>
{
    service.Create(dto);
    return Results.Ok(new { mensagem = "Obra CRIADA com Sucesso." });

})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Insere uma nova obra")
    .WithDescription("Este endpoint recebe Autor, Título e Assunto e insere uma nova obra na bilbioteca.");

app.MapDelete("/Obras/{id}", (int id, IObraService service) =>
{
    service.Delete(id);
    return Results.Ok(new { mensagem = "Obra DELETADA com Sucesso." });

})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Deleta uma obra existente")
    .WithDescription("Este endpoint recebe o id de uma obra existente e deleta a mesma no Banco de Dados da Biblioteca.");

app.MapPut("/Obras/{id}", (int id, CreateObraDTO dto, IObraService service) =>
{
    service.Update(id, dto);
    return Results.Ok(new { mensagem = "Obra ATUALIZADA com Sucesso." });
})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Atualiza uma obra existente")
    .WithDescription("Este endepoint recebe o id de uma obra existente e os dados desejados para atualizar a mesma no Banco de Dados da Biblioteca.");

app.MapGet("/obras/disponiveis", (string? nucleo, string? assunto, IObraService service) =>
{
    var obras = service.PesquisarObrasDisponiveis(nucleo, assunto);
    return Results.Ok(obras);
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Pesquisa as obras por núcleo")
    .WithDescription("Este endepoint recebe nome do núcleo e o assunto tipo string e retorna uma lista das obras daquele assunto disponíveis naquele núcleo.");

app.MapGet("/obras/disponiveis", (string? nucleo, string? assunto, IObraService service) =>
{
    var obras = service.PesquisarObrasDisponiveis(nucleo, assunto);
    return Results.Ok(obras);
})
.RequireAuthorization();

app.MapPut("/Exemplares/ChangeNucleo", (ChangeNucleoDTO dto, IExemplarService service) =>
{
    service.ChangeNucleo(dto);
    return Results.Ok(new { mensagem = "Obra ALTERADA para novo núcleo com Sucesso." });
})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Altera uma Obra para outro Núcleo")
    .WithDescription("Este endepoint recebe o id do núcleo de destino e id de um exemplar para alterar este para um novo núcleo.");

app.MapGet("/Obras/UpdateCount", (int id, IObraService service) =>
{
    return service.UpdateCount(id);
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Atualiza o número de exemplares")
    .WithDescription("Este endepoint recebe o id da obra e atualiza os exemplares disponíveis.");

 app.MapPost("/Obras/Historico", (RequestHistObrasDTO dto, IObraService service) =>
{
    return service.GetHistorico(dto);
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Histórico obras requisitadas")
    .WithDescription("Este endepoint recebe o documento do Leitor, o tipo de documento (sendo esses CC, NIF e Passaporte e um intervalo e retorna um histórico das obras requisitas.");

app.MapPost("/login", (LoginDTO login, IAuthService auth) =>
{
    var token = auth.Login(login.UserName, login.Password);
    if (string.IsNullOrEmpty(token))
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new { token });
})
    .WithSummary("Realiza a autenticação do utilizador")
    .WithDescription("Este endpoint recebe as credenciais e retorna um token JWT se o acesso for válido.");

app.MapPut("/Utilizadores/alterar_status", (AlterarStatusDTO dto, IUtilizadoresService service) =>
{
    service.AlterarStatus(dto.UtilizadorId, dto);

    return Results.Ok(new { Mensagem = "Status alterado com sucesso" }); ;
})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Altera Status Utilizador")
    .WithDescription("Este endepoint recebe id do Utilizadoe e id do status conforme a necessidade (sendo 1 - Ativo, 2 - Inativo, 3 - Suspenso.");

app.MapDelete("/Utilizadores/limpar-antigos", (IUtilizadoresService service) =>
{
    service.DeleteLeitorAntigo();

    return Results.Ok(new { Mensagem = "Leitores antigos deletados com sucesso" }); 
})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Deletar Leitores Antigos")
    .WithDescription("Este endepoint deleta os leitores que estejam há mais de um ano sem atividade, desde que não tenham nenhuma requisição ativa."); 

app.MapPost("/Utilizadores/registrar-utilizador",
    (RegistrarUtilizadorDTO dto, IUtilizadoresService service) =>
    {
        service.RegistrarUtilizador(dto);

        return Results.Ok(new { Mensagem = "Utilizador registrado com sucesso" });
    })
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Registar Utilizador")
    .WithDescription("Este endepoint registar novo utilizador, recebendo nome completo, número de documento, id do tipo de documento (sendo 1 - CC, 2 - NIF e 3 - Passaporte)," +
                    " email, morada, telefone, id do tipo User (sendo 1 - Leitor, 2 - Bibliotecário e 3 - Administrador), sendo que Leitor só consegue registar Leitor. ");

app.MapGet("/TopBooks", (DateTime dataInicio, DateTime dataFim, IMetricsService service) =>
{
    return Results.Ok(service.GetTopBooks(dataInicio, dataFim));
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Consultar obras mais requisitadas")
    .WithDescription("Este endepoint recebe um intervalo de data e retorna os 10 livros mais requisitados nesse intervalo.");

    
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
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Consultar Situação do Leitor")
    .WithDescription("Este endepoint recebe um id do Leitor e devolve a situação do mesmo perante aos livros ainda não devolvidos. Respeitando 15 dias para devolução, a consulta retorna " +
                "4 status diferente: Dentro do prazo, Devolver em Breve ( 5 dias o menos para data de entrega), Devolver Urgente (menos de 3 dias para a data de entreta) e Atraso (ultrapassou" +
                " a data de entrega.");

app.MapGet("/BottomBooks", (DateTime dataInicio, DateTime dataFim, IMetricsService service) =>
{
    return Results.Ok(service.GetBottomBooks(dataInicio, dataFim));
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Consultar obras menos requisitadas")
    .WithDescription("Este endepoint recebe um intervalo de data e retorna os 10 livros menos requisitados nesse intervalo.");

app.MapGet("/BottomEmprestimosNucleo", (DateTime dataInicio, DateTime dataFim, IMetricsService service) =>
{
    return Results.Ok(service.GetBottomEmprestimos(dataInicio, dataFim));
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Consultar obras menos requisitadas por núcleo")
    .WithDescription("Este endepoint recebe um intervalo de data e retorna as obras menos requisitadas nesse intervaloe e agrupadas por núcleo.");

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
})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Realizar Requisição")
    .WithDescription("Este endepoint recebe o número do documento do leitor e a Id do Exemplar e registra um emprestimo junto a base de dados.");

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
})
    .RequireAuthorization("AdminOuBibli")
    .WithSummary("Realizar Requisição")
    .WithDescription("Este endepoint recebe a Id do Exemplar e registra uma devolução junto a base de dados.");

app.MapGet("/TopEmprestimosNucleo", (DateTime dataInicio, DateTime dataFim, IMetricsService service) =>
{
    return Results.Ok(service.GetTopEmprestimos(dataInicio, dataFim));
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Consultar obras mais requisitadas por núcleo")
    .WithDescription("Este endepoint recebe um intervalo de data e retorna as obras mais requisitadas nesse intervalo e agrupadas por núcleo.");

app.MapGet("/TopLeitores", (DateTime dataInicio, DateTime dataFim, IMetricsService service) =>
{
    return Results.Ok(service.GetTopLeitores(dataInicio, dataFim));
})
    .RequireAuthorization("TodosAutenticados")
    .WithSummary("Consultar top 3 Leitores")
    .WithDescription("Este endepoint recebe um intervalo de data e retorna o Top 3 Leitores que realizaram o maior número de empréstimos neste intervalo.");

app.MapPost("/leitores/{numeroDocumento}/inscricao/cancelar", (string numeroDocumento, ILeitoresService service) =>
{
    try
    {
        service.CancelarInscricao(numeroDocumento);
        return Results.Ok(new { mensagem = "Inscrição cancelada com sucesso" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { mensagem = ex.Message });
    }
}).RequireAuthorization("TodosAutenticados")
    .WithSummary("Cancelar Registro")
    .WithDescription("Este endepoint recebe o Id do Leitor e permite cancelar o registro desde que não haja nenhum empréstimo em aberto.");

app.Run();