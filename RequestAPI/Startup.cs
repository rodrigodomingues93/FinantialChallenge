using CashBookAPIClient.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RequestApp;
using RequestApp.Interfaces;
using RequestData;
using RequestData.Repository;
using RequestData.Repository.Interfaces;
using System.Text.Json.Serialization;

namespace RequestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RequestAPI", Version = "v1" });
            });
            services.AddDbContext<RequestContext>(cfg =>
            {
                cfg.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddCashBookConfiguration(Configuration);
            services.AddScoped<IRequestApplication, RequestApplication>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IProductRequestApplication, ProductRequestApplication>();
            services.AddScoped<IProductRequestRepository, ProductRequestRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RequestAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
