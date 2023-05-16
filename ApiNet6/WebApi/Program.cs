using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Injectors;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddUnversioned_Services();
builder.Services.AddServices_v1();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSwaggerFeature();
builder.Services.AddAuthentication("Bearer");
builder.Services.AddOptions();

builder.Services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version"));
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new HttpResponseFilter());

}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(
        JsonNamingPolicy.CamelCase));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test Api v1");
        //c.SwaggerEndpoint("/swagger/v2/swagger.json", "Test Api v2");

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
//app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TokenValidatorMiddleware>();

app.MapControllers();

app.Run();
