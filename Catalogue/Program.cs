using Catalogue.Application.Commands.ItemAggregate;
using Catalogue.Application.Configurations;
using Catalogue.Application.Queries.ItemQueries;
using Catalogue.Domain.Entities.ItemAggregate;
using Catalogue.Domain.SeedWork;
using Catalogue.Infrastructure.Database;
using Catalogue.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CatalogueDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogueDbConnection")
        ?? throw new ArgumentNullException("CatalogueDbConnection")));

//TODO: resolve this warning
using var serviceProvider = builder.Services.BuildServiceProvider();

builder.Services.AddMediatR(typeof(CreateItemCommandHandler));

builder.Services.AddOptions();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemQueries, ItemQueries>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


//using var scope = serviceProvider.CreateScope();
//using var dbContext = scope.ServiceProvider.GetService<CatalogueDbContext>()
//    ?? throw new ArgumentNullException(nameof(CatalogueDbContext));

app.Run();
using var dbContext = app.Services.GetRequiredService<CatalogueDbContext>()
    ?? throw new ArgumentNullException(nameof(CatalogueDbContext));
dbContext.Database.Migrate();