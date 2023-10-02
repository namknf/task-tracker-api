using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Api.ActionFilters;
using TaskTracker.Api.Extensions;
using TaskTracker.Contract;
using TaskTracker.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddControllers(conf =>
{
    conf.RespectBrowserAcceptHeader = true;
    conf.ReturnHttpNotAcceptable = true;
});

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.AddAuthorization();

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigureSwagger();

builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskTracker.Api v1");
});

app.UseHttpsRedirection();

app.ConfigureExceptionHandler();

app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.Run();