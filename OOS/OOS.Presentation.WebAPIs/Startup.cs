using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OOS.Presentation.ApplicationLogic.Products;
using OOS.Infrastructure.Mongodb;
using Swashbuckle.AspNetCore.Swagger;
using OOS.Presentation.WebAPIs.Configs;
using OOS.Presentation.ApplicationLogic.Categories;
using OOS.Presentation.ApplicationLogic.Users;
using OOS.Presentation.ApplicationLogic.Order;
using OOS.Presentation.WebAPIs.Filters;
using OOS.Presentation.ApplicationLogic.Contacts;

namespace OOS.Presentation.WebAPIs
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
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidationFilter));
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });

            services.AddTransient<IUsersBusinessLogic, UsersBusinessLogic>();
            services.AddTransient<IProductsBusinessLogic, ProductsBusinessLogic>();
            services.AddTransient<IOrderBusinessLogic, OrderBusinessLogic>();
            services.AddTransient<ICategoriesBusinessLogic, CategoriesBusinessLogic>();
            services.AddTransient<IUsersBusinessLogic, UsersBusinessLogic>();
            services.AddTransient<IEmailBusinessLogic, EmailBusinessLogic>();
            services.AddTransient<IMongoDbRepository, MongoDbRepository>(n => new MongoDbRepository(Configuration.GetValue<string>("MongoDb:DefaultConnectionString")));            

            AutoMapperConfig.Configure(services);

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseCors("MyPolicy");

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
