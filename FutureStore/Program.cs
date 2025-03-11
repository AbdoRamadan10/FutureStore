
using FutureStore.Authentication;
using FutureStore.Authentication.BearerAuthentication.Jwt;
using FutureStore.Authorization.CustomRoleBased;
using FutureStore.Authorization.CustomRolePermissionBased;
using FutureStore.Authorization.PermissionBased;
using FutureStore.Data;
using FutureStore.GenericRepository;
using FutureStore.Interfaces;
using FutureStore.Mapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllers(options =>
            {
                //options.Filters.Add<PermissionBasedAuthorizationFilter>();
                //options.Filters.Add<RoleBasedAuthorizationFilter>();
                options.Filters.Add<RolePermissionBasedAuthorizationFilter>();
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //Jwt----------------------------------------------------------------------------------------------------------
            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(jwtOptions);
            builder.Services.AddAuthentication()
                .AddJwtBearer("Bearer", options =>
                {
                   options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
                        ValidateLifetime = true,
                        
                        
                        

                    };
                });
            //Jwt-------------------------------------------------------------------------------------------------------------
            //.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var futureStorecontext = services.GetRequiredService<FutureStoreContext>();
            //    futureStorecontext.Database.EnsureCreated();
            //    FutureStoreInitializer.Initializer(futureStorecontext);
            //}
            app.UseHttpsRedirection();

            app.UseAuthorization();  


            app.MapControllers();

            app.Run();
        }
    }
}
