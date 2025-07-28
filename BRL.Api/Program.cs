using System.Text.Json.Serialization;
using BRL.Business.Services; // Add this using statement

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ClaimValidator>(); // Register your service

builder.Services.AddControllers().AddJsonOptions(options =>
{
    // This converter allows the API to handle enums as strings
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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