using API.GymAi.Adapters;
using API.GymAi.Adapters.Interfaces;
using API.GymAi.Builders;
using API.GymAi.Builders.Interfaces;
using API.GymAi.Facades;
using API.GymAi.Options;
using API.GymAi.Repositories;
using API.GymAi.Repositories.Interface;
using API.GymAi.Services;
using API.GymAi.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOptions();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IChatService, ChatService>();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }