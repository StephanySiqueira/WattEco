using WattEco.Models;
using WattEco.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WattEco.Repositories;
using WattEco.Services;
using WattEco.Training;


var builder = WebApplication.CreateBuilder(args);

// Configure o DbContext com a string de conex�o correta
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

// Configure a inje��o de depend�ncia para os reposit�rios e services
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMissaoRepository, MissaoRepository>();
builder.Services.AddScoped<IMissaoService, MissaoService>();
builder.Services.AddScoped<IRecompensaRepository, RecompensaRepository>();
builder.Services.AddScoped<IRecompensaService, RecompensaService>();

// Adicione o servi�o de an�lise de sentimentos
builder.Services.AddScoped<SentimentAnalysisService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WattEco",
        Version = "v1",
        Description = "API voltada para o monitoramento, gerenciamento e conscientiza��o sobre o consumo de energia, permitindo que usu�rios n�o apenas acompanhem e analisem seu uso de energia, mas tamb�m promovam pr�ticas mais sustent�veis e eficientes para reduzir o impacto ambiental.",
        Contact = new OpenApiContact
        {
            Name = "Stephany",
            Email = "rm98258@fiap.com.br"
        }
    });
});


var app = builder.Build();

// Treinar e salvar o modelo antes de iniciar a aplica��o
var trainer = new SentimentAnalysisTrainer();
trainer.TrainAndSaveModel(@"C:\Users\tesiq\OneDrive\Documentos\FIAP PROJETOS\2TDSPK\WattEco\WattEco\TrainedModels\energyConsumptionModel.zip");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WattEco API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
