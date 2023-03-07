using MassTransit;
using Microsoft.EntityFrameworkCore;
using TicketProcessor.Microservice;
using TicketProcessor.Microservice.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TicketContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketDatabase")));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TicketConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        //cfg.UseHealthCheck(provider);
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("ticketQueue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<TicketConsumer>(provider);
        });
    }));
});
//services.AddMassTransitHostedService();
builder.Services.AddControllers();

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
