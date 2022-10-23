using Microsoft.AspNetCore.Mvc;
using SwaDemoApi;
using SwaDemoApi.Core;
using SwaDemoApi.Models;
using SwaDemoApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<AgendaDataManager>()
    .AddScoped<AgendaServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/agenda", (AgendaServices services) => AgendaEndpoints.GetAgenda(services)).WithName("Agenda");
app.MapGet("/api/talk/{id}", (Guid id, AgendaServices services) => TalkEndpoints.GetTalkDetail(id, services)).WithName("TalkDetail");
app.MapPost("/api/talk", ([FromBody] CreateTalkModel model, AgendaServices services) => TalkEndpoints.CreateTalk(model, services)).WithName("CreateTalk");
app.MapDelete("/api/talk/{id}", (Guid id, AgendaServices services) => TalkEndpoints.DeleteTalk(id, services)).WithName("DeleteTalk");
app.MapPut("/api/talk/{id}/rate", (Guid id, [FromBody] TalkRateModel model, AgendaServices services) => TalkEndpoints.RateTalk(id, model, services)).WithName("RateTalk");

SeedInitialData(app);

app.Run();

#region Seed
void SeedInitialData(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var agenda = scope.ServiceProvider.GetRequiredService<AgendaServices>();
    var start = 8;

    for (int i = 1; i <= 5; i++)
    {
        ++start;
        agenda.CreateTalk(new SwaDemoApi.Models.CreateTalkModel
        {
            Title = $"talk #{i}",
            Abstract = $"Abstract of talk #{i}",
            StartingTime = new TimeSpan(start, 0, 0),
            EndingTime = new TimeSpan(start + 1, 0, 0),
            IsBreakSlot = i == 3,
            Speaker = $"Speaker #{i}"
        });
    }
}
#endregion