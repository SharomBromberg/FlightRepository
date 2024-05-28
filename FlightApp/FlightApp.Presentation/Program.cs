
using FlightAPP.Application.Interfaces;
using FlightAPP.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// // Uncomment if needed
// builder.Services.AddDbContext<FlightContext>(options =>
//     options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "flightFrontEnd", configurePolicy: policybuilder =>
    {
        policybuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("flightFrontEnd");

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();