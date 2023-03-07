using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Models.Models;

namespace TicketProcessor.Microservice.Entity;

public class TicketContext : DbContext
{
    public TicketContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
}
