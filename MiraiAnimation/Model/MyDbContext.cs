using Microsoft.EntityFrameworkCore;
using MiraiAnimation.Model;
using MongoDB.EntityFrameworkCore.Extensions;

public class MyDbContext : DbContext {
	public DbSet<User> User { get; init; }
	public MyDbContext(DbContextOptions options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<User>().ToCollection("users");
	}
}
