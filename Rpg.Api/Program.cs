using Rpg.Api.Extensions;
using Rpg.Api.Middlewares;
using Rpg.Application.Extensions;
using Rpg.Data.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureMediatR();
builder.Services.ResolveServices();
builder.Services.ConfigureAuthorizationPolicy(configuration);
builder.Services.ConfigureDbContext(configuration);
builder.Services.ConfigureRouting();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger(configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureUnitsOfWork();
builder.Services.ConfigureMvc().ConfigureNewtonsoftJson();
builder.Services.ConfigureSerilog(builder.Host);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage().ConfigureSwaggerUse();
}

app.ConfigureCors();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseSerilogRequestLogging();
app.UseEndpoints(endpointRouteBuilder =>
 {
     endpointRouteBuilder.MapControllers();
 });
app.Run();
