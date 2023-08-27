using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SearchEngine.Models;

namespace SearchEngine.Data;

public class ApplicationContext : DbContext
{
	public DbSet<Site> Sites => Set<Site>();
	public DbSet<Page> Pages => Set<Page>();
	public DbSet<Token> Tokens => Set<Token>();
	public DbSet<Counter> Counters => Set<Counter>();

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql("User Id=postgres;Password=&8L$7%m=J?n4pc+;Server=db.rdvgoujabhusgngnkiux.supabase.co;Port=5432;Database=postgres");
	}

	//TODO: secure the password 
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Token>(token =>
		{
			token.HasKey(t => t.Type);

			token.HasMany(t => t.Pages);
		});

		builder.Entity<Page>(page =>
		{
			page.HasKey(p => p.Url);

			page.Property(p => p.Html).IsRequired();

			page.Property(p => p.LastUpdate).HasDefaultValue(DateOnly.FromDateTime(DateTime.Now)).IsRequired();

			page.HasOne(p => p.Site);
		});

		builder.Entity<Site>(site =>
		{
			site.HasKey(p => p.Url);

			site.Property(p => p.IpAddress);

			site.Property(p => p.Name).IsRequired();

			site.Property(p => p.LastUpdate).HasDefaultValue(DateOnly.FromDateTime(DateTime.Now)).IsRequired();
		});

		builder.Entity<Counter>(counter =>
		{
			counter.HasKey(c => c.Id);

			counter.Property(c => c.Entries).HasDefaultValue(1).IsRequired();

			counter.HasOne(c => c.Page).WithMany().HasForeignKey(c => c.Url);
		});
	}
}