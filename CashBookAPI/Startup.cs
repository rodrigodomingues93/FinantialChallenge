using AutoMapper;
using CashBookAPIClient.Configuration;
using CashBookApp;
using CashBookApp.Interfaces;
using CashBookData;
using CashBookData.Repositories;
using CashBookData.Repositories.Interfaces;
using CashBookDomain.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CashBookAPI
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CashBookAPI", Version = "v1" });
            });

            services.AddCashBookConfiguration(Configuration);
            
            services.AddDbContext<CashBookContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<ICashBookApplication, CashBookApplication>();
            services.AddScoped<ICashBookRepository, CashBookRepository>();

            var mapperConfiguration = new MapperConfiguration(mapCfg =>
            {
                mapCfg.AddProfile(new CashBookMapper());
            });
            IMapper mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CashBookAPI v1"));
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
