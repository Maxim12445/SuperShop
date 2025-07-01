using SuperShopApi.Context;
using Microsoft.EntityFrameworkCore;
using SuperShopApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ⬇️ DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ⬇️ Serviço de Clientes
builder.Services.AddScoped<IClienteService, ClientesService>();

// ⬇️ Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// Serviços básicos
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ⬇️ Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⬇️ Use o CORS antes de authorization!
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();