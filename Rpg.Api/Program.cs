using Rpg.Api.Extensions;
using Rpg.Api.Middlewares;
using Rpg.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.ConfigureApiVersioning();
builder.Services.ConfigureAuthorizationPolicy(configuration);
builder.Services.ConfigureDbContext(configuration);
builder.Services.ConfigureRouting();
builder.Services.ConfigureSwagger(configuration);
builder.Services.AddRepositories();
builder.Services.AddUnitsOfWork();
builder.Services.ConfigureMvc().AddNewtonsoftJsonConfiguration();
//TODO: Add Services configuration.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage().ConfigureSwaggerUse(configuration);
}

app.ConfigureCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.UseEndpoints(endpointRouteBuilder =>
 {
     endpointRouteBuilder.MapControllers();
 });
app.Run();
