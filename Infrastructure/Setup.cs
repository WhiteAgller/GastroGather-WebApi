using System.Diagnostics;
using Common.Interfaces.IRepositories.Order;
using Common.Interfaces.IRepositories.Product;
using Common.Interfaces.IRepositories.Table;
using Common.Interfaces.IRepositories.User;
using Infrastructure.Repositories.Order;
using Infrastructure.Repositories.Product;
using Infrastructure.Repositories.Table;
using Infrastructure.Repositories.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;
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
        services.AddScoped<IGroupInviteRepository<GroupInvite>, GroupGroupInviteRepository<GroupInvite>>();
        services.AddScoped<IFriendInviteRepository<FriendInvite>, FriendInviteRepository<FriendInvite>>();
        services.AddScoped<IUserRepository<User.Domain.User>, UserRepository<User.Domain.User>>();
        
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

                options.SetRedirectionEndpointUris("callback/login/ahoj");

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
                    .SetUserinfoEndpointUris("api/connect/userinfo")
                    .SetIssuer("https://localhost:7054/");

                options.RegisterScopes(OpenIddictConstants.Scopes.Email, OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.Roles, OpenIddictConstants.Scopes.OpenId);

                options.AllowAuthorizationCodeFlow()
                    .AllowRefreshTokenFlow();

                options.AddDevelopmentEncryptionCertificate()
                    .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableLogoutEndpointPassthrough()
                    .EnableStatusCodePagesIntegration()
                    .EnableTokenEndpointPassthrough();
                
                options.RequireProofKeyForCodeExchange();
            })

            .AddValidation(options =>
            {
                // Import the configuration from the local OpenIddict server instance.
                options.UseLocalServer();

                // Register the ASP.NET Core host.
                options.UseAspNetCore();
            });
        
        services.AddIdentity<User.Domain.User, IdentityRole>(options =>
        {
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+#";
            options.User.RequireUniqueEmail = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 1;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        services.AddControllers();
        
        return services;
    }
}