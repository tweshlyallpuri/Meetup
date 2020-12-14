using Meetup.Models;
using Microsoft.EntityFrameworkCore;

namespace Meetup
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
        }

        public DbSet<Participant> Participants { get; set; }
    }
}
