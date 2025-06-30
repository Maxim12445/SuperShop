using SuperShopApi.Context; // substitua pelo namespace onde está seu DbContext
using Microsoft.EntityFrameworkCore;
using SuperShopApi.Context;

var builder = WebApplication.CreateBuilder(args);

// ⬇️ Aqui você adiciona o DbContext e lê a string de conexão do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();