using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Agregar servicios al contenedor.
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Configurar CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
                policy.WithOrigins("https://frontend21.web.app", "http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials()); // Incluye credenciales si es necesario
        });

        // Agregar servicios para Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Agregar servicios para controladores
        builder.Services.AddControllers();

        var app = builder.Build();

        // Configuración de Swagger en modo desarrollo
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Backend API v1");
                c.RoutePrefix = string.Empty; // Hace que Swagger esté en la raíz
            });
        }

        app.UseHttpsRedirection();

        // Aplicar CORS antes de la autorización
        app.UseCors("AllowSpecificOrigin");

        app.UseAuthorization();

        // Mapear controladores con las rutas
        app.MapControllers();

        app.Run();
    }
}
