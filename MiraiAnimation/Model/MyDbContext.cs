using Microsoft.EntityFrameworkCore;
using MiraiAnimation.Model;
using MongoDB.EntityFrameworkCore.Extensions;

public class MyDbContext : DbContext {
	public DbSet<User> User { get; init; }
	public DbSet<Animation> Animation { get; init; }
	public DbSet<BluRay> BluRay { get; init; }
	public DbSet<Staff> Staff { get; init; }
	public MyDbContext(DbContextOptions options) : base(options) {
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<User>().ToCollection("users");
		modelBuilder.Entity<Animation>().ToCollection("animations");
		modelBuilder.Entity<BluRay>().ToCollection("blu-rays");
		modelBuilder.Entity<Staff>().ToCollection("staffs");
	}
}
