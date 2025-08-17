using CoreMvcLab.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcLab.Data;
public class CoreMvcLabContext : DbContext
{
    public CoreMvcLabContext(DbContextOptions<CoreMvcLabContext> options) : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<Reply> Replies { get; set; }
}
