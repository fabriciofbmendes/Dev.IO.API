using DevIO.Api.Configuration;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using DevIO.Api.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddWebApiConfig();

builder.Services.AddSwaggerConfig();

//Caso queira fazer o Loggin, criar uma conta no Elmah.IO e
//inserir suas credenciais na classe LoggerConfig na pasta Configuration
//e descomentar a linha abaixo


//builder.Services.AddLoggingConfiguration();

builder.Services.AddHealthChecks()
    .AddCheck("Produtos", new SqlServerHealthCheck(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), name: "BancoSQL");
    //.AddElmahIoPublisher("ApiKey",new Guid("LogId"),"API Fornecedores");

builder.Services.AddHealthChecksUI()
    .AddSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Development",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


builder.Services.ResolveDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseCors("Production");
    app.UseHsts();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseWebApiConfig();

//Caso queira fazer o Loggin, criar uma conta no Elmah.IO e
//inserir suas credenciais na classe LoggerConfig na pasta Configuration
//e descomentar a linha abaixo

//app.UseLoggingConfiguration();


var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
app.UseSwaggerConfig(provider);

app.UseHealthChecks("/api/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecksUI(options =>
{
    options.UIPath = "/api/hc-ui";
    options.ResourcesPath = $"{options.UIPath}/resources";
    options.UseRelativeApiPath = false;
    options.UseRelativeResourcesPath = false;
    options.UseRelativeWebhookPath = false;
});

app.Run();
