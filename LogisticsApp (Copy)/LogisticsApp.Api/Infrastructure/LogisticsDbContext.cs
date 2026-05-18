using LogisticsApp.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;

namespace LogisticsApp.Api.Infrastructure;

public class LogisticsDbContext : DbContext /* Isto é o context da DB, portanto, a DB em si */
{
    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
    {
        
    }
    public DbSet<Order> Orders {get; set;}/* Aqui passamos as tabelas das DB's configuradas nos domains*/

}
