
using FutureStore.Data;
using FutureStore.GenericRepository;
using FutureStore.Interfaces;
using FutureStore.Mapper;
using FutureStore.Middlewares;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FutureStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FutureStoreContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddControllers(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.WithOrigins() // Specify your allowed origin
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowAnyOrigin());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var futureStorecontext = services.GetRequiredService<FutureStoreContext>();
                futureStorecontext.Database.EnsureCreated();
                FutureStoreInitializer.Initializer(futureStorecontext);
            }
            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();  


            app.MapControllers();

            app.UseMiddleware<RateLimitingMidleware>();
            app.UseMiddleware<ProfilingMiddleware>();

            app.Run();
        }
    }
}
