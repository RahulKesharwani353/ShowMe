using Microsoft.EntityFrameworkCore;
using ShowMe.Models;

namespace ShowMe.Data;

public class DataContext : DbContext {
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	public DbSet<Movie> Movies { get; set; }
	public DbSet<Screen> Screens { get; set; }
	public DbSet<Theater> Theaters { get; set; }
	public DbSet<Show> Shows { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		// one-to-many relationships
		modelBuilder.Entity<Movie>()
			.HasMany(m => m.Shows)
			.WithOne(s => s.Movie);

		modelBuilder.Entity<Screen>()
			.HasMany(m => m.Shows)
			.WithOne(s => s.Screen);

		modelBuilder.Entity<Theater>()
			.HasMany(t => t.Screens)
			.WithOne(s => s.Theater);

		// keys and default value generation
		modelBuilder.Entity<Movie>()
			.Property(b => b.Id)
			// Defines the default value for the column
			.HasDefaultValueSql("gen_random_uuid()")
			// Value is generated on add or update by database itself, and hence not by EF Core
			// This is the default behavior for properties which are restricted to be added or updated by user
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Movie>()
			.Property(b => b.CreatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Movie>()
			.Property(b => b.UpdatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAddOrUpdate();

		modelBuilder.Entity<Screen>()
			.Property(b => b.Id)
			.HasDefaultValueSql("gen_random_uuid()")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Screen>()
			.Property(b => b.CreatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Screen>()
			.Property(b => b.UpdatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAddOrUpdate();

		modelBuilder.Entity<Theater>()
			.Property(b => b.Id)
			.HasDefaultValueSql("gen_random_uuid()")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Theater>()
			.Property(b => b.CreatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Theater>()
			.Property(b => b.UpdatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAddOrUpdate();

		modelBuilder.Entity<Show>()
			.Property(b => b.Id)
			.HasDefaultValueSql("gen_random_uuid()")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Show>()
			.Property(b => b.CreatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAdd();
		modelBuilder.Entity<Show>()
			.Property(b => b.UpdatedOn)
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAddOrUpdate();
	}
}