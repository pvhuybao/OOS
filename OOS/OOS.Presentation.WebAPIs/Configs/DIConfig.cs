using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OOS.Domain.Users.Models;
using OOS.Domain.Users.Services;
using OOS.Infrastructure.Identity.MongoDB;
using OOS.Infrastructure.Mongodb;
using OOS.Presentation.ApplicationLogic.Categories;
using OOS.Presentation.ApplicationLogic.Configurations;
using OOS.Presentation.ApplicationLogic.Contacts;
using OOS.Presentation.ApplicationLogic.Order;
using OOS.Presentation.ApplicationLogic.Products;
using OOS.Presentation.ApplicationLogic.SocialNetwork;
using OOS.Presentation.ApplicationLogic.SocialNetworks;
using OOS.Presentation.ApplicationLogic.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OOS.Presentation.WebAPIs.Configs
{
    public class DIConfig
    {
        public static void Configure(IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IConfigurationsBusinessLogic, ConfigurationsBusinessLogic>();
            services.AddTransient<IUsersBusinessLogic, UsersBusinessLogic>();
            services.AddTransient<IProductsBusinessLogic, ProductsBusinessLogic>();
            services.AddTransient<IOrderBusinessLogic, OrderBusinessLogic>();
            services.AddTransient<ICategoriesBusinessLogic, CategoriesBusinessLogic>();
            services.AddTransient<IUsersBusinessLogic, UsersBusinessLogic>();
            services.AddTransient<IEmailBusinessLogic, EmailBusinessLogic>();
            services.AddTransient<ISocialNetworkBusinessLogic, SocialNetworkBusinessLogic>();
            services.AddTransient<IMongoDbRepository, MongoDbRepository>(n => new MongoDbRepository(config.GetValue<string>("MongoDb:DefaultConnectionString")));
            

            services.AddIdentity<User, Role>(
                identityOptions =>
                {
                    identityOptions.User.RequireUniqueEmail = true;
                })
                .AddDefaultTokenProviders();


            services.AddTransient<IUserStore<User>, UserStore<User, Role>>
                (n => new UserStore<User, Role>(
                        n.GetService<IMongoDbRepository>().GetCollection<User>(),
                        n.GetService<IMongoDbRepository>().GetCollection<Role>()
                ));

            services.AddTransient<IRoleStore<Role>, RoleStore<Role>>
                (n => new RoleStore<Role>(
                        n.GetService<IMongoDbRepository>().GetCollection<Role>()
                ));

            services.AddTransient<IUserService, UserService>();
        }
    }
}
