using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductApp;
using ProductApp.Interfaces;
using ProductData;
using ProductData.Repositories;
using ProductData.Repositories.Interfaces;
using ProductDomain.Mappers;

namespace ProductAPI
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
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductAPI", Version = "v1" });
			});
			services.AddDbContext<ProductContext>(cfg =>
			{
				cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddScoped<IProductApplication, ProductApplication>();
			services.AddScoped<IProductRepository, ProductRepository>();

			var mapperConfiguration = new MapperConfiguration(mapCfg =>
			{
				mapCfg.AddProfile(new ProductMapper());
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
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductAPI v1"));
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
