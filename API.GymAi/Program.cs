using API.GymAi.Adapters;
using API.GymAi.Adapters.Interfaces;
using API.GymAi.Builders;
using API.GymAi.Builders.Interfaces;
using API.GymAi.Facades;
using API.GymAi.Options;
using API.GymAi.Repositories;
using API.GymAi.Repositories.Interface;
using API.GymAi.RespostaSwaggerExample;
using API.GymAi.Services;
using API.GymAi.Services.Interface;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

var circuitBreakerPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

builder.Services.AddHttpClient<IChatRepository, CohereRepository>()
    .AddPolicyHandler(retryPolicy)
    .AddPolicyHandler(circuitBreakerPolicy);

builder.Services.AddControllers();
builder.Services.AddOptions();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerExamplesFromAssemblyOf<TreinoBadRequestExample>();
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
     {
         Title = "GymAi API",
         Version = "v1",
         Description = "API para geração de treinos inteligentes"
     });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<ITreinoAdapter, TreinoAdapter>();
builder.Services.AddScoped<IPromptAdapter, PromptAdapter>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<ITreinoBuilder, TreinoBuilder>();
builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
builder.Services.AddScoped<IRetornoChatAdapter, RetornoChatAdapter>();
builder.Services.AddScoped<IChatRepository, CohereRepository>();

builder.Services.AddOptions<ChatOptions>()
    .Bind(builder.Configuration
        .GetSection("CohereConfigs"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<PeriodoDeTreinoOptions>()
    .Bind(builder.Configuration
        .GetSection("PeriodoEmDiasPorCondicionamentoFisico"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<InformacoesPromptOptions>()
    .Bind(builder.Configuration
        .GetSection("InformacoesPrompt"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "GymAi API v1");
    options.RoutePrefix = "swagger";
});
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }