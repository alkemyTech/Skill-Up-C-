using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AlkemyWallet.Entities;
using Seeder;


public class WalletDbContext : IdentityDbContext<ApplicationUser>
{
    public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
    {
    }

    public new DbSet<Role>? Roles { get; set; }
    public new DbSet<User>? Users { get; set; }
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<FixedTermDeposit>? FixedTermDeposits { get; set; }
    public DbSet<Catalogue>? Catalogues { get; set; }



    //en caso de usar CodeFirst a Sql
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        base.OnModelCreating(modelBuilder);
        foreach (var foreingKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            foreingKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
        base.OnModelCreating(modelBuilder);
        new DbInitializer(modelBuilder).Seed();

        //en construccion
        ///////////////////////
        modelBuilder.Entity<Role>().ToTable("Role");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Account>().ToTable("Account");
        modelBuilder.Entity<Transaction>().ToTable("Transaction");
        modelBuilder.Entity<FixedTermDeposit>().ToTable("FixedTermDeposit");
        modelBuilder.Entity<Catalogue>().ToTable("Catalogue");
        base.OnModelCreating(modelBuilder);

    }

}
