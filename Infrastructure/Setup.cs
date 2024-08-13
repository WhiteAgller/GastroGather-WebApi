using System.Diagnostics;
using Common.Interfaces.IRepositories.Order;
using Common.Interfaces.IRepositories.Product;
using Common.Interfaces.IRepositories.Table;
using Common.Interfaces.IRepositories.User;
using Infrastructure.Repositories.Order;
using Infrastructure.Repositories.Product;
using Infrastructure.Repositories.Table;
using Infrastructure.Repositories.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using Order.Domain;
using Quartz;
using User.Domain;

namespace Infrastructure;

public static class Setup
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string? connectionString,
        LogLevel logLevel = LogLevel.Information
    )
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
            options
                .UseNpgsql(connectionString)
                .LogTo(s => Debug.WriteLine(s), logLevel)
                .EnableServiceProviderCaching()
                .UseOpenIddict()
        );
        // Product
        services.AddScoped<IProductRepository<Product.Domain.Product>, ProductRepository<Product.Domain.Product>>();
        
        // User
        services.AddScoped<IGroupRepository<Group>, GroupRepository<Group>>();
        services.AddScoped<IInviteRepository<Invite>, InviteRepository<Invite>>();
        services.AddScoped<IFriendInviteRepository<FriendInvite>, FriendInviteRepository<FriendInvite>>();
        
        // Order
        services.AddScoped<IOrderItemRepository<OrderItem>, OrderItemRepository<OrderItem>>();
        services.AddScoped<IOrderRepository<Order.Domain.Order>, OrderRepository<Order.Domain.Order>>();
        
        // Table
        services.AddScoped<ITableRepository<Table.Domain.Table>, TableRepository<Table.Domain.Table>>();
        services.AddScoped<IPlaceRepository<Table.Domain.Place>, PlaceRepository<Table.Domain.Place>>();

        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();
            options.UseSimpleTypeLoader();
            options.UseInMemoryStore();
        });
        
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
        
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationDbContext>();
            })
            .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow();
                options.AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                    .EnableStatusCodePagesIntegration()
                    .EnableRedirectionEndpointPassthrough();
                
                options.UseSystemNetHttp()
                    .SetProductInformation(typeof(Setup).Assembly);

                options.UseWebProviders()
                    .AddGitHub(options =>
                    {
                        options
                            .SetClientId("c4ade52327b01ddacff3")
                            .SetClientSecret("da6bed851b75e317bf6b2cb67013679d9467c122")
                            .SetRedirectUri("callback/login/github");
                    })
                    .AddGoogle(options =>
                    {
                        options
                        .SetClientId("c4ade52327b01ddacff3")
                         .SetClientSecret("da6bed851b75e317bf6b2cb67013679d9467c122")
                         .SetRedirectUri("callback/login/google");
                    });
            })
            .AddServer(options =>
            {
                options.SetAuthorizationEndpointUris("connect/authorize")
                    .SetLogoutEndpointUris("connect/logout")
                    .SetTokenEndpointUris("connect/token")
                    .SetUserinfoEndpointUris("connect/userinfo");
                
                options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile, OpenIddictConstants.Scopes.Roles);

                options.AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();
                
                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableStatusCodePagesIntegration()
                    .EnableTokenEndpointPassthrough();
            });
        
      
        services.AddIdentity<User.Domain.User, IdentityRole>(options =>
        {
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#";
            options.User.RequireUniqueEmail = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = false;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        services.AddControllers();
        
        return services;
    }
}