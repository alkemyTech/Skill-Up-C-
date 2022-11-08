using AlkemyWallet.Core.Interfaces;
using AlkemyWallet.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AlkemyWallet.Core.Models;
using AlkemyWallet.Repositories.Interfaces;
using AlkemyWallet.Repositories;
using challenge.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Inyecta el servicio de JWT
builder.Services.AddIdentityJwt(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WalletDbContext>
    (options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContext")); });

//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//                  .AddEntityFrameworkStores<WalletDbContext>()
//                  .AddDefaultTokenProviders();

// Agrego los servicios
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICatalogueService, CatalogueService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Agrego los repositorios
builder.Services.AddScoped<ICatalogueRepository, CatalogueRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IFixedTermDepositRepository, FixedTermDepositRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));  


// Agrego los servicios
builder.Services.AddScoped<IUserService, UserService>();

//MAPPER
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

//utilizar los comandos de abajo para crear una Migracion ,recordar borrar la carpeta migration entre cada creacion y tambien la Database(Wallet)
//Visual Studio
//EntityFrameworkCore\Add-Migration Migrations
//EntityFrameworkCore\Update - database

//Visual Code - CLI:
//dotnet ef migrations add Migra
//dotnet ef database update



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WalletDbContext>();
    context.Database.Migrate();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();