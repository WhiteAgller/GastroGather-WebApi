using Common;
using Common.Interfaces;
using Common.Services;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Order.Infrastructure;
using Product;
using Table;
using User;
using WebApi.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:8100")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddApplication();

builder.Services.AddSingleton<IDomainEventService, DomainEventService>();
builder.Services.AddSingleton<IDateTime, DateTimeService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHostedService<Worker>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.InstallProductDependencies();
builder.Services.InstallUserDependencies();
builder.Services.InstallTableDependencies();
builder.Services.InstallOrderDependencies();
builder.Services.AddInfrastructure(connectionString);


builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiPlayground", Version = "v1" });
        c.AddSecurityDefinition(
            "oauth",
            new OpenApiSecurityScheme
            {
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                      Scopes  = new Dictionary<string, string>()
                      {
                        ["api"] = "api scope"  
                      },
                      TokenUrl = new Uri("https://localhost:7054/connect/token"),
                      AuthorizationUrl = new Uri("https://localhost:7054/connect/authorize"),
                      RefreshUrl = new Uri("https://localhost:7054/connect/token")
                    }
                },
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.OAuth2
            }
        );
        c.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                            { Type = ReferenceType.SecurityScheme, Id = "oauth" },
                    },
                    new[] { "api" }
                }
            }
        );
    }
);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await Task.Run(async ()=> Seed.SeedCategories(context));
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();