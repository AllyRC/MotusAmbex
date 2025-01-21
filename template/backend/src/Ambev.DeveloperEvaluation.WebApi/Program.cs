using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            ConfigureCorsService(builder.Services);
            //builder.Services.AddSwaggerGen();
            ConfigureSwaggerService(builder.Services, "Ambev.DeveloperEvaluation");

            builder.Services.AddDbContext<DefaultContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DefaultContext>();
                    context.Database.Migrate(); // Aplica migra  es pendentes
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while migrating or initializing the database.");
                    throw; // Re-throw para finalizar a aplica  o caso algo d  errado
                }
            }
            app.UseCors("CorsPolicy");
            app.UseMiddleware<ValidationExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static void ConfigureCorsService(IServiceCollection services)
    {
        services.AddCors(o =>
            o.AddPolicy(
                "CorsPolicy",
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200") // Especifique as origens permitidas
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); // Permite credenciais, mas não pode ser usado com AllowAnyOrigin()
                }));
    }



    public static void ConfigureSwaggerService(IServiceCollection service, string apiName)
    {
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio
        // Register the Swagger generator, defining 1 or more Swagger documents
        service.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1", Description = "Ambev.DeveloperEvaluation" });
            c.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                        Enter 'Bearer' [space] and then your token in the text input below.
                                        Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                        {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                      },
                      new List<string>()
                    }
                  });
        });
    }
}
