using ApiPeliculasAWS.Data;
using ApiPeliculasAWS.Helpers;
using ApiPeliculasAWS.Models;
using ApiPeliculasAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace ApiPeliculasAWS;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        string json = HelperSecretManager.GetSecretsAsync().GetAwaiter().GetResult();
        KeysModel keys = JsonConvert.DeserializeObject<KeysModel>(json);
        services.AddSingleton<KeysModel>(x => keys);

        string connectionString = keys.MySql;
        services.AddTransient<RepositoryPeliculas>();
        services.AddDbContext<PeliculasContext>
            (options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });

        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Peliculas AWS",
                Version = "v1"
            });
        });
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options => options.AllowAnyOrigin());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("swagger/v1/swagger.json", "ApiPeliculasAWS");
            options.RoutePrefix = "";
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}