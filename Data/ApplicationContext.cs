using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SearchEngine.Data.Models;

namespace SearchEngine.Data;

public class ApplicationContext : DbContext
{
	public DbSet<Site> Sites => Set<Site>();
	public DbSet<Page> Pages => Set<Page>();
	public DbSet<Token> Tokens => Set<Token>();

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

			token.HasMany<Site>(t => t.Urls);
		});

		builder.Entity<Page>(page =>
		{
			page.HasKey(p => p.Url);

			page.Property(p => p.Text).IsRequired();

			page.Property(p => p.LastUpdate).HasDefaultValue(DateOnly.FromDateTime(DateTime.Now));


			page.HasOne<Site>(p => p.Site);
		});

		builder.Entity<Site>(site =>
		{
			site.HasKey(p => p.Url);

			site.Property(p => p.LastUpdate).IsRequired();

			site.Property(p => p.Name).IsRequired();

			site.Property(p => p.LastUpdate).HasDefaultValue(DateOnly.FromDateTime(DateTime.Now));
		});
	}
}