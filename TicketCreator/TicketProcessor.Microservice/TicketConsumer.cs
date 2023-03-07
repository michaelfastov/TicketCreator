using MassTransit;
using Shared.Models.Models;
using TicketProcessor.Microservice.Entity;

namespace TicketProcessor.Microservice;

public class TicketConsumer : IConsumer<Ticket>
{
    private readonly TicketContext _context;

    public TicketConsumer(TicketContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<Ticket> context)
    {
        try
        {
            var data = context.Message;
            _context.Add(data);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw;
        }
    }
}