using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace Seeder { 
public class DbInitializer
{
    private readonly ModelBuilder _modelBuilder;

    public DbInitializer(ModelBuilder modelBuilder)
    {
        _modelBuilder = modelBuilder;
    }

    public void Seed()
    {
        ///////////Rol////////////
        _modelBuilder.Entity<Role>(p =>
        {
           p.HasData(
                new Role
                {
                    Id = 1,
                    Name = "Administrador",
                    Description = "Admin",
                },
                new Role
                {
                    Id = 2,
                    Name = "Standard",
                    Description = "Usuario con algunos permisos de escritura",
                },
                new Role
                {
                    Id = 3,
                    Name = "Invitado",
                    Description = "Solo permisos de lectura",
                }
            );        
        });

        ///////////Usuarios////////////
        _modelBuilder.Entity<User>(p => {
        p.HasData(
                new User
                {
                    Id = 1,
                    First_name = "Clint",
                    Last_name = "Eastwood",
                    Email = "clint@eastwood.com",
                    Password = "Clint",
                    Points = 30,
                    Rol_id = 2
                },
                new User
                {
                    Id = 2,
                    First_name = "Arnold",
                    Last_name = "Schwarzenegger",
                    Email = "arnoldsg@skynet.com",
                    Password = "Arnold",
                    Points = 2000,
                    Rol_id = 1
                },
                new User
                {
                    Id = 3,
                    First_name = "Sylvester",
                    Last_name = "Stallone",
                    Email = "sylvesters@hollywood.com",
                    Password = "Sylvester",
                    Points = 2000,
                    Rol_id = 3
                }
            );
        });

        ///////////Accounts////////////
        _modelBuilder.Entity<Account>(p => {
        p.HasData(
                new Account
                {
                    Id = 1,
                    CreationDate = new DateTime(1995, 11, 19),
                    Money = 150000,
                    IsBlocked = false,
                    User_id = 1,
                },
                new Account
                {
                    Id = 2,
                    CreationDate = new DateTime(1995, 11, 19),
                    Money = 50,
                    IsBlocked = true,
                    User_id = 1,
                }
              );
        });

        ///////////Transaction(transferencias)////////////
        _modelBuilder.Entity<Transaction>(p => {
        p.HasData(
                new Transaction
                {
                    Transaction_id = 1,
                    Amount = 2000,
                    Concept = "crédito",
                    Date = new DateTime(2020, 01, 25),
                    Type = "Efectivo",
                    Account_id = 1,
                    User_id = 2,
                }
              
              );
        });

        ///////////FixedTermDeposit(plazo fijo etc.)////////////
        _modelBuilder.Entity<FixedTermDeposit>(p => {
        p.HasData(
                new FixedTermDeposit
                {
                    Id = 1,
                    User_id = 1,
                    Account_id = 1,
                    Creation_date = new DateTime(2020, 01, 25),
                    Amount = 23000,
                    Closing_date = new DateTime(2021, 03, 22)
                }
               
              );
        });

        ///////////Catalogue(catalogo de productos?,puede ser prestamo personal a tienda de electrodomesticos tmb, hay que consultar.)////////////

        _modelBuilder.Entity<Catalogue>(p => {
        p.HasData(
                new Catalogue
                {
                    Id = 1,
                    Product_description = "cocina",
                    Image = "",
                    Points = 300,
                }
              );
        });
      
    }
}
}