using api_gym_ai.Adapters;
using api_gym_ai.Builders;
using api_gym_ai.Facades;
using api_gym_ai.Interfaces.Adapters;
using api_gym_ai.Interfaces.Builders;
using api_gym_ai.Interfaces.Services;
using api_gym_ai.Options;
using api_gym_ai.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOptions();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICohereService, CohereService>();
builder.Services.AddScoped<ITreinoAdapter, TreinoAdapter>();
builder.Services.AddScoped<IPromptAdapter, PromptAdapter>();
builder.Services.AddScoped<ICohereService, CohereService>();
builder.Services.AddScoped<ITreinoBuilder, TreinoBuilder>();
builder.Services.AddScoped<IPromptBuilder, PromptBuilder>();
builder.Services.AddScoped<IRetornoChatAdapter, RetornoChatAdapter>();

builder.Services.Configure<CohereServiceOptions>(
    builder.Configuration.GetSection("CohereConfigs"));
builder.Services.Configure<PeriodoDeTreinoOptions>(
    builder.Configuration.GetSection("PeriodoEmDiasPorCondicionamentoFisico"));

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
