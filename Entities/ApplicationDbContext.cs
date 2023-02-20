using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<User> Users { get; set; }
		public DbSet<Todo> Todos { get; set; }
	}
}
