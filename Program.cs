using MediatR;
using System.Reflection;
using Tutorial.EntityFrameworkUpdate.Infrastructure.Registration;

namespace Tutorial.EntityFrameworkUpdate;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();


        // Application services
        builder.Services.AddInfrastructure(builder.Configuration);

        // MediatR and Fluent Validation self discovery
        // Load selected assemblies
        Assembly[] assemblies = new Assembly[]
                {
                    typeof(Tutorial.EntityFrameworkUpdate.Program).Assembly,
                };


        // FluentValidation
        // Add all public Validators automatically.  Classes scoped as internal should not be added
        //services.AddValidatorsFromAssemblies(assemblies, ServiceLifetime.Transient);

        // configure MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));



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
    }
}
